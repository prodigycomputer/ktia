<?php
include 'koneksi.php';

// --- Ambil config max item per halaman ---
$q = mysqli_query($conn, "SELECT qbrsjual FROM zconfig LIMIT 1");
$row = mysqli_fetch_assoc($q);
$maxPerPage = (int)$row['qbrsjual'];
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
            font-size: 12px;
            background-color: #eee;
        }

        .container-a5 {
            width: 210mm;   /* ukuran A5 landscape */
            height: 168mm;
            background-color: #fff;
            margin: 20px auto;
            padding: 10mm;  /* aman untuk printer */
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

        .form-kiri, .form-tengah, .form-kanan {
            display: flex;
            flex-direction: column;
            gap: 2px;
        }

        .form-kiri { width: 40%; margin-right: 5px; }
        .form-tengah {width: 50%; margin-left: 5px;}
        .form-kanan { width: 30%; margin-left: 5px; }

        .field { 
            flex-basis: 10%;
            display: flex;
            align-items: center;
            gap: 2px;
        }
        .field label:first-child { width: 120px; font-weight: bold; }
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
            width: 60%;
            display: flex;
            flex-direction: column;
            align-items: center;
            gap: 12px;
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
        .footer-kanan span { flex: none; text-align: right; padding: 1px 2px;}

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
            font-size: 10px;
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

    fetch(`getpenjualan.php?nonota=${encodeURIComponent(noNota)}`)
    .then(res => res.json())
    .then(data => {
        if (data.status !== 'success') {
            alert('Data tidak ditemukan!');
            return;
        }

        const h = data.header;
        const d = data.detail;

        // ==============================
        // Hitung perhitungan seperti di form
        // ==============================
        let subtotal = 0;
        d.forEach(item => {
            subtotal += parseFloat(item.jumlah || 0);
        });

        let dc1 = parseFloat(h.disk1 || 0);
        let dc2 = parseFloat(h.disk2 || 0);
        let dc3 = parseFloat(h.disk3 || 0);
        let persenppn = parseFloat(h.prsnppn || 0);
        let totaljmlh = parseFloat(h.totaljmlh || 0);

        let hrgdc1 = subtotal * dc1 / 100;
        let smntarahrgdc1 = subtotal - hrgdc1;

        let hrgdc2 = smntarahrgdc1 * dc2 / 100;
        let smntarahrgdc2 = smntarahrgdc1 - hrgdc2;

        let hrgdc3 = smntarahrgdc2 * dc3 / 100;
        let smntarahrgdc3 = smntarahrgdc2 - hrgdc3;

        let hrppn = smntarahrgdc3 * persenppn / 100;
        let totalppn = smntarahrgdc3 + hrppn;

        // 👉 lain-lain dihitung dari total akhir yg sudah disimpan
        let lainlain = totaljmlh - totalppn;

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
                        <div class="field"><label>Jatuh Tempo</label><label>:</label><span>${h.jt_tempo}</span></div>
                    </div>
                    <div class="form-tengah">
                        <div class="field"><label>Kode Kustomer</label><label>:</label><span>${h.kode_kust}</span></div>
                        <div class="field"><label>Nama Kustomer</label><label>:</label><span>${h.nama_kust}</span></div>
                        <div class="field"><label>Alamat</label><label>:</label><span>${h.alamat}</span></div>
                    </div>
                    <div class="form-kanan">
                        <div class="field"><label>Kode Sales</label><label>:</label><span>${h.kode_sls}</span></div>
                        <div class="field"><label>Nama Sales</label><label>:</label><span>${h.nama_sls}</span></div>
                    </div>
                </div>

                <table>
                    <thead>
                        <tr>
                            <th style="width: 20px;">Kode</th>
                            <th style="width: 150px;">Nama brg</th>
                            <th style="width: 80px;">QTY</th>
                            <th style="width: 50px;">Disc %</th>
                            <th style="width: 10px;">Disrp</th>
                            <th style="width: 10px;">Harga</th>
                            <th style="width: 10px;">Jumlah</th>
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

                    <!-- KANAN -->
                    <div class="footer-kanan">
                        <div><label>Subtotal :</label><span class="span-besar">${subtotal.toLocaleString('id-ID',{minimumFractionDigits:2})}</span></div>
                        <div><label>Diskon 1 :</label><span class="span-kecil">${dc1}%</span><span class="span-sedang">${hrgdc1.toLocaleString('id-ID',{minimumFractionDigits:2})}</span></div>
                        <div><label>Diskon 2 :</label><span class="span-kecil">${dc2}%</span><span class="span-sedang">${hrgdc2.toLocaleString('id-ID',{minimumFractionDigits:2})}</span></div>
                        <div><label>Diskon 3 :</label><span class="span-kecil">${dc3}%</span><span class="span-sedang">${hrgdc3.toLocaleString('id-ID',{minimumFractionDigits:2})}</span></div>
                        <div><label>Lain-Lain :</label><span class="span-besar">${lainlain.toLocaleString('id-ID',{minimumFractionDigits:2})}</span></div>
                        <div ${persenppn===0?'style="display:none"':''}>
                            <label>PPN :</label><span class="span-kecil">${persenppn}%</span><span class="span-sedang">${hrppn.toLocaleString('id-ID',{minimumFractionDigits:2})}</span>
                        </div>
                        <div style="border-top: 1px solid #000; padding-top: 4px;">
                            <label><strong>Total Jumlah :</strong></label>
                            <span style="font-weight:bold;" class="span-besar">${totaljmlh.toLocaleString('id-ID',{minimumFractionDigits:2})}</span>
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
                const disc = tampilDiskon(item);
                const tr = document.createElement('tr');
                tr.innerHTML = `
                    <td>${item.kodebrg}</td>
                    <td>${item.namabrg}</td>
                    <td style="text-align:right;">${qty}</td>
                    <td style="text-align:right;">${disc}</td>
                    <td style="text-align:right;">${parseFloat(item.discrp).toLocaleString('id-ID',{minimumFractionDigits:2})}</td>
                    <td style="text-align:right;">${parseFloat(item.harga).toLocaleString('id-ID',{minimumFractionDigits:2})}</td>
                    <td style="text-align:right;">${parseFloat(item.jumlah).toLocaleString('id-ID',{minimumFractionDigits:2})}</td>
                `;
                tbody.appendChild(tr);
            });

            for (let i = pageItems.length; i < maxPerPage; i++) {
                const tr = document.createElement('tr');
                tr.innerHTML = `<td>&nbsp;</td><td></td><td></td><td></td><td></td><td></td><td></td>`;
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
