<?php
include 'koneksi.php';

// --- Ambil config max item per halaman ---
$q = mysqli_query($conn, "SELECT qbrsmutasi FROM zconfig LIMIT 1");
$row = mysqli_fetch_assoc($q);
$maxPerPage = (int)$row['qbrsmutasi'];
if ($maxPerPage <= 0) $maxPerPage = 10; // default kalau kosong

?>
<!DOCTYPE html>
<html>
<head>
    <meta charset="UTF-8">
    <title>Cetak Nota</title>
    <script src="fungsi.js"></script>
    <style>
        html, body {
            margin: 0;
            padding: 0;
            font-family: 'Verdana';
            font-weight: 20;
            font-size: 12px;
            background-color: #eee;
        }

        .container-a5 {
            width: 210mm;   /* ukuran A5 landscape */
            height: 168mm;
            background-color: #fff;
            margin: 20px auto;
            padding: 5mm;  /* aman untuk printer */
            box-shadow: 0 0 5px rgba(0,0,0,0.2);
            box-sizing: border-box;
            /* HAPUS page-break-after di sini */
        }

        /* hanya halaman selain terakhir yg dipaksa pecah */
        .container-a5:not(:last-child) {
            page-break-after: always;
        }

        h2 { margin-bottom: 10px; }

        .form-header {
            display: flex;
            justify-content: space-between;
            gap: 20px;
            margin-bottom: 5px;
            font-size: 12px;
        }

        .form-kiri, .form-kanan {
            display: flex;
            flex-direction: column;
            gap: 2px;
        }

        .form-kiri { width: 30%; margin-right: 10px; }
        .form-kanan { width: 60%; margin-left: 10px; }

        .field { display: flex; align-items: center; }
        .field label:first-child { width: 100px;}
        .field label:nth-child(2) { margin-right: 4px; width: 10px; }
        .field span { flex: 1;}

        .form-footer {
            display: flex;
            justify-content: space-between;
            margin-top: 5px;
            font-size: 12px;
        }

        /* kiri: tanda tangan + perhatian */
        .footer-kiri {
            width: 100%;
            display: flex;
            flex-direction: column;
            align-items: center;
            gap: 5px;
            font-size: 12px;
        }

        .box-perhatian {
            border: 1px solid;
            padding: 12px;
            font-size: 12px;
            font-style: italic;
            text-align: center;
            width: 80%;
        }

        .footer-ttd-row {
            display: flex;
            justify-content: space-between;
            width: 100%;
            margin-top: 10px;
        }

        .footer-ttd-row .kolom-ttd {
            width: 45%;
            text-align: center;
        }

        /* kanan: perhitungan */
        .footer-kanan {
            width: 35%;
            display: flex;
            flex-direction: column;
            align-items: flex-end;
            gap: 4px;
            font-size: 12px;
        }
        .footer-kanan div { display: flex; align-items: center; justify-content: flex-start; width: 100%; }
        .footer-kanan label { flex: 1; text-align: right; }
        .footer-kanan span { flex: none; text-align: right; padding: 1px 2px; }

        .span-kecil { width: 36px; }
        .span-sedang { width: 90px; }
        .span-besar  { width: 130px; }

        table { border-collapse: collapse; width: 100%; margin-bottom: 3px; }
        th {
            border: 1px solid;
            padding: 3px;
            text-align: center;
            font-size: 12px;
        }
        td {
            border: none; /* default tanpa border */
            padding: 3px;
            text-align: left;
            font-size: 12px;
            white-space: nowrap;   /* Supaya teks tidak turun baris */
            overflow: hidden;      /* Sembunyikan teks yang kepanjangan */
            text-overflow: ellipsis; /* Tambah "..." kalau teks terpotong */
        }

        /* Kasih border bawah hanya di row terakhir */
        tr:last-child td {
            border-bottom: 1px solid black;
        }


        .action-buttons {
            display: flex;
            /* justify-content: flex-start; kalau mau kiri */
            justify-content: center;   /*<-- kalau mau di tengah */
            gap: 10px;
            margin: 10px auto;
            padding: 10px;
            max-width: 210mm;/* biar sejajar dengan container A5 */
            position: sticky;
            top: 0;
            z-index: 999;
        }
        .action-buttons button {
            padding: 6px 14px;
            font-size: 12px;
            cursor: pointer;
        }

        /* force print ke A5 landscape */
        @page {
            size: A5 landscape;
            margin: 0;
        }

        @media print {
            html, body {
                margin: 0;
                padding: 0;
                background: none;
                width: 210mm;
                height: 168mm;
            }
            body * { visibility: hidden; }
            .container-a5, .container-a5 * { visibility: visible; }
            .container-a5 {
                position: relative;
                margin: 0;
                box-shadow: none;
                padding: 10mm;
                background: white;
                box-sizing: border-box;
                overflow: hidden;
            }
            .action-buttons { display: none; }
        }
    </style>

</head>
<body>

<div class="action-buttons">
    <button onclick="goBack()">← Kembali</button>
    <button onclick="window.print()">🖨️ Print</button>
</div>

<div id="pages"></div>

<script>
    const noNota = new URLSearchParams(window.location.search).get('nonota');
    const maxPerPage = <?php echo $maxPerPage; ?>;

    fetch(`getmutasi.php?nonota=${encodeURIComponent(noNota)}`)
    .then(res => res.json())
    .then(data => {
        if (data.status !== 'success') {
            alert('Data tidak ditemukan!');
            return;
        }

        const h = data.header;
        const d = data.detail;

        // ==============================
        // Buat halaman nota
        // ==============================
        const totalPages = Math.ceil(d.length / maxPerPage);
        const pagesDiv = document.getElementById('pages');

        for (let page = 0; page < totalPages; page++) {
            const container = document.createElement('div');
            container.className = 'container-a5';

            const isLastPage = (page === totalPages - 1);

            container.innerHTML = `
                <?php include 'header.php'; ?>

                <div class="form-header">
                    <div class="form-kiri">
                        <div class="field"><label>Tanggal</label><label>:</label><span>${h.tanggal}</span></div>
                        <div class="field"><label>No Nota</label><label>:</label><span>${h.no_nota}</span></div>
                    </div>
                    <div class="form-kanan">
                        <div class="field"><label>Kode Gudang 1</label><label>:</label><span>${h.kodegd1} ${h.namagd1}</span></div>
                        <div class="field"><label>Kode Gudang 2</label><label>:</label><span>${h.kodegd2} ${h.namagd2}</span></div>
                    </div>
                </div>

                <table>
                    <thead>
                        <tr>
                            <th>Kode</th>
                            <th>Nama brg</th>
                            <th>Qty</th>
                        </tr>
                    </thead>
                    <tbody></tbody>
                </table>

                ${isLastPage ? `
                <div class="form-footer">
                    <!-- KIRI -->
                    <div class="footer-kiri">
                        <!-- Atas -->
                        <div class="box-perhatian">
                            Perhatian: Barang yang sudah dibeli tidak bisa ditukar / dikembalikan.
                        </div>

                        <!-- Bawah: kiri-kanan -->
                        <div class="footer-ttd-row">
                            <div class="kolom-ttd">
                                <strong>Tanda Terima</strong>
                                <br><br><br> <!-- ruang tanda tangan -->
                                (______________)
                            </div>
                            <div class="kolom-ttd">
                                <strong>Hormat Kami</strong>
                                <br><br><br> <!-- ruang tanda tangan -->
                                (______________)
                            </div>
                        </div>
                    </div>
                </div>
                ` : ''}
            `;

            const tbody = container.querySelector('tbody');
            const start = page * maxPerPage;
            const end = start + maxPerPage;
            const pageItems = d.slice(start, end);

            pageItems.forEach(item => {
                const qty = tampilKuantitas(item);
                const tr = document.createElement('tr');
                tr.innerHTML = `
                    <td>${item.kodebrg}</td>
                    <td>${item.namabrg}</td>
                    <td style="text-align:right;">${qty}</td>
                `;
                tbody.appendChild(tr);
            });

            for (let i = pageItems.length; i < maxPerPage; i++) {
                const tr = document.createElement('tr');
                tr.innerHTML = `<td>&nbsp;</td><td></td><td></td>`;
                tbody.appendChild(tr);
            }

            pagesDiv.appendChild(container);
        }

        setTimeout(() => { window.print(); }, 300);
    })
    .catch(err => {
        console.error(err);
        alert("Gagal mengambil data");
    });

    function goBack() {
        const from = new URLSearchParams(window.location.search).get('from');
        if (from) window.location.href = from;
        else window.history.back();
    }
</script>
</body>
</html>