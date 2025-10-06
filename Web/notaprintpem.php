<?php
include 'koneksi.php';

// --- Ambil config max item per halaman ---
$q = mysqli_query($conn, "SELECT qbrsbeli FROM zconfig LIMIT 1");
$row = mysqli_fetch_assoc($q);
$maxPerPage = (int)$row['qbrsbeli'];
if ($maxPerPage <= 0) $maxPerPage = 10; // default kalau kosong
?>
<!DOCTYPE html>
<html>
<head>
    <meta charset="UTF-8">
    <title>Cetak Nota</title>
    <script src="fungsi.js"></script>
    <script src="https://printjs-4de6.kxcdn.com/print.min.js"></script>
    <style>
        html, body {
            margin: 0;
            padding: 0;
            font-family: Verdana;
            font-weight: normal;  
            font-size: 12px;
            background-color: #eee;
        }

        .container-a5 {
            width: 210mm;   /* ukuran A5 landscape */
            height: 148mm;
            background-color: #fff;
            margin: 20px auto;
            padding: 5mm;
            box-shadow: 0 0 5px rgba(0,0,0,0.2);
            box-sizing: border-box;
            overflow: hidden;
        }

        /* Tambahan baru */
        .scale-wrapper {
            transform-origin: top left;
            width: 100%;
            height: 100%;
        }

        /* hanya halaman selain terakhir yg dipaksa pecah */
        .container-a5:not(:last-child) {
            page-break-after: always;
        }

        h2 { margin-bottom: 10px; }

        .form-header { display: flex; justify-content: space-between; gap: 20px; margin-bottom: 5px; font-size: 12px; }
        .foarm-kiri, .form-kanan { display: flex; flex-direction: column; gap: 2px; }
        .form-kiri { width: 30%; margin-right: 10px; }
        .form-kanan { width: 60%; margin-left: 10px; }
        .field { display: flex; align-items: center; }
        .field label:first-child { width: 100px;}
        .field label:nth-child(2) { margin-right: 4px; width: 10px; }
        .field span { flex: 1;}

        .form-footer { display: flex; justify-content: space-between; margin-top: 5px; font-size: 12px; }

        .footer-kiri { width: 60%; display: flex; flex-direction: column; align-items: center; gap: 5px; font-size: 12px; }
        .box-perhatian { border: 1px solid; padding: 12px; font-size: 12px; font-style: italic; text-align: center; width: 80%; }
        .footer-ttd-row { display: flex; justify-content: space-between; width: 100%; margin-top: 10px; }
        .footer-ttd-row .kolom-ttd { width: 45%; text-align: center; }

        .footer-kanan { width: 35%; display: flex; flex-direction: column; align-items: flex-end; gap: 4px; font-size: 12px; }
        .footer-kanan div { display: flex; align-items: center; justify-content: flex-start; width: 100%; }
        .footer-kanan label { flex: 1; text-align: right; }
        .footer-kanan span { flex: none; text-align: right; padding: 1px 2px; }

        .span-kecil { width: 36px; }
        .span-sedang { width: 90px; }
        .span-besar  { width: 130px; }

        table { border-collapse: collapse; width: 100%; margin-bottom: 3px; }
        th { border: 1px solid; padding: 3px; text-align: center; font-size: 12px; }
        td { border: none; padding: 3px; text-align: left; font-size: 11px; white-space: nowrap; overflow: hidden; text-overflow: ellipsis; }
        td:nth-child(1) { max-width: 50px; }   /* kode */
        td:nth-child(2) { max-width: 160px; }  /* nama barang */
        td:nth-child(3) { max-width: 100px; text-align:right; } /* qty */
        td:nth-child(4) { max-width: 50px; text-align:right; } /* disc % */
        td:nth-child(5) { max-width: 70px; text-align:right; } /* discrp */
        td:nth-child(6) { max-width: 70px; text-align:right; } /* harga */
        td:nth-child(7) { max-width: 70px; text-align:right; } /* jumlah */

        tr:last-child td { border-bottom: 1px solid black; }

        .action-buttons {
            display: flex;
            justify-content: center;
            gap: 10px;
            margin: 10px auto;
            padding: 10px;
            max-width: 210mm;
            position: sticky;
            top: 0;
            z-index: 999;
        }
        .action-buttons button { padding: 6px 14px; font-size: 12px; cursor: pointer; }

        @page { size: A5 landscape; margin: 0; }
        @media print {
            html, body { margin: 0; padding: 0; background: none; width: 210mm; height: 168mm; }
            body * { visibility: hidden; }
            .container-a5, .container-a5 * { visibility: visible; }
            .container-a5 { position: relative; margin: 0; box-shadow: none; padding: 10mm; background: white; box-sizing: border-box; overflow: hidden; }
            .action-buttons { display: none; }
        }
    </style>
</head>
<body>

<div class="action-buttons">
    <button onclick="goBack()">← Kembali</button>
    <button onclick="printNota()">🖨️ Print</button>
</div>

<div id="pages"></div>

<script>
    const noNota = new URLSearchParams(window.location.search).get('nonota');
    const maxPerPage = <?php echo $maxPerPage; ?>;

    fetch(`getpembelian.php?nonota=${encodeURIComponent(noNota)}`)
    .then(res => res.json())
    .then(data => {
        if (data.status !== 'success') {
            alert('Data tidak ditemukan!');
            return;
        }

        const h = data.header;
        const d = data.detail;

        let subtotal = 0;
        d.forEach(item => { subtotal += parseFloat(item.jumlah || 0); });

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

        let lainlain = totaljmlh - totalppn;

        const totalPages = Math.ceil(d.length / maxPerPage);
        const pagesDiv = document.getElementById('pages');

        for (let page = 0; page < totalPages; page++) {
            const container = document.createElement('div');
            container.className = 'container-a5';

            const isLastPage = (page === totalPages - 1);

            container.innerHTML = `
                <div class="scale-wrapper">
                    <?php include 'header.php'; ?>

                    <div class="form-header">
                        <div class="form-kiri">
                            <div class="field"><label>Tanggal</label><label>:</label><span>${h.tanggal}</span></div>
                            <div class="field"><label>No Nota</label><label>:</label><span>${h.no_nota}</span></div>
                            <div class="field"><label>Jatuh Tempo</label><label>:</label><span>${h.jt_tempo}</span></div>
                        </div>
                        <div class="form-kanan">
                            <div class="field"><label>Kode Supplier</label><label>:</label><span>${h.kode_sup}</span></div>
                            <div class="field"><label>Nama Supplier</label><label>:</label><span>${h.nama_sup}</span></div>
                            <div class="field"><label>Alamat</label><label>:</label><span>${h.alamat}</span></div>
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
                        <div class="footer-kiri">
                            <div class="box-perhatian">Perhatian: Barang yang sudah dibeli tidak bisa ditukar / dikembalikan.</div>
                            <div class="footer-ttd-row">
                                <div class="kolom-ttd"><strong>Tanda Terima</strong><br><br><br>(______________)</div>
                                <div class="kolom-ttd"><strong>Hormat Kami</strong><br><br><br>(______________)</div>
                            </div>
                        </div>
                        <div class="footer-kanan">
                            <div><label>Subtotal :</label><span class="span-besar">${subtotal.toLocaleString('id-ID',{minimumFractionDigits:2})}</span></div>
                            <div><label>Diskon 1 :</label><span class="span-kecil">${dc1}%</span><span class="span-sedang">${hrgdc1.toLocaleString('id-ID',{minimumFractionDigits:2})}</span></div>
                            <div><label>Diskon 2 :</label><span class="span-kecil">${dc2}%</span><span class="span-sedang">${hrgdc2.toLocaleString('id-ID',{minimumFractionDigits:2})}</span></div>
                            <div><label>Diskon 3 :</label><span class="span-kecil">${dc3}%</span><span class="span-sedang">${hrgdc3.toLocaleString('id-ID',{minimumFractionDigits:2})}</span></div>
                            <div><label>Lain-Lain :</label><span class="span-besar">${lainlain.toLocaleString('id-ID',{minimumFractionDigits:2})}</span></div>
                            <div ${persenppn===0?'style="display:none"':''}><label>PPN :</label><span class="span-kecil">${persenppn}%</span><span class="span-sedang">${hrppn.toLocaleString('id-ID',{minimumFractionDigits:2})}</span></div>
                            <div style="border-top: 1px solid #000; padding-top: 4px;"><label><strong>Total Jumlah :</strong></label><span class="span-besar">${totaljmlh.toLocaleString('id-ID',{minimumFractionDigits:2})}</span></div>
                        </div>
                    </div>
                    ` : ''}
                </div>
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
            fitToA5(container); // <<=== scale disini
        }
    })
    .catch(err => { console.error(err); alert("Gagal mengambil data"); });

    function fitToA5(container) {
        const wrapper = container.querySelector(".scale-wrapper");
        if (!wrapper) return;

        const maxWidth = container.clientWidth;
        const maxHeight = container.clientHeight;

        const contentWidth = wrapper.scrollWidth;
        const contentHeight = wrapper.scrollHeight;

        const scaleX = maxWidth / contentWidth;
        const scaleY = maxHeight / contentHeight;
        const scale = Math.min(1, scaleX, scaleY);

        wrapper.style.transform = `scale(${scale})`;
    }

    function goBack() {
        const from = new URLSearchParams(window.location.search).get('from');
        if (from) window.location.href = from;
        else window.history.back();
    }

    function printNota() {
        printJS({
            printable: 'pages',
            type: 'html',
            targetStyles: ['*'],
            style: `
                @page { size: A5 landscape; margin: 0; }
                body { font-family: Verdana; font-size: 12px; background: #fff; margin: 0; padding: 0; }
                .container-a5 {
                    width: 210mm;
                    height: 148mm;
                    padding: 10mm;
                    box-sizing: border-box;
                    overflow: hidden;
                }
                h2 { margin-bottom: 10px; }
                .form-header { display: flex; justify-content: space-between; gap: 20px; margin-bottom: 5px; font-size: 12px; }
                .form-kiri, .form-kanan { display: flex; flex-direction: column; gap: 2px; }
                .field { display: flex; align-items: center; }
                .field label:first-child { width: 100px;}
                .field label:nth-child(2) { margin-right: 4px; width: 10px; }
                .field span { flex: 1;}
                table { border-collapse: collapse; width: 100%; margin-bottom: 3px; }
                th { border: 1px solid #000; padding: 3px; text-align: center; font-size: 12px; }
                td { border: none; padding: 3px; font-size: 11px; white-space: nowrap; overflow: hidden; text-overflow: ellipsis; }
                td:nth-child(1) { max-width: 50px; }
                td:nth-child(2) { max-width: 160px; }
                td:nth-child(3) { max-width: 100px; text-align: right; }
                td:nth-child(4) { max-width: 50px; text-align: right; }
                td:nth-child(5) { max-width: 70px; text-align: right; }
                td:nth-child(6) { max-width: 70px; text-align: right; }
                td:nth-child(7) { max-width: 70px; text-align: right; }
                tr:last-child td { border-bottom: 1px solid black; }
                .form-footer { display: flex; justify-content: space-between; margin-top: 5px; font-size: 12px; }
                .footer-kiri { width: 60%; display: flex; flex-direction: column; align-items: center; gap: 5px; }
                .box-perhatian { border: 1px solid; padding: 12px; font-size: 12px; font-style: italic; text-align: center; width: 80%; }
                .footer-ttd-row { display: flex; justify-content: space-between; width: 100%; margin-top: 10px; }
                .footer-ttd-row .kolom-ttd { width: 45%; text-align: center; }
                .footer-kanan { width: 35%; display: flex; flex-direction: column; align-items: flex-end; gap: 4px; font-size: 12px; }
                .footer-kanan div { display: flex; align-items: center; justify-content: flex-start; width: 100%; }
                .footer-kanan label { flex: 1; text-align: right; }
                .footer-kanan span { flex: none; text-align: right; padding: 1px 2px; }
                .span-kecil { width: 36px; }
                .span-sedang { width: 90px; }
                .span-besar  { width: 130px; }
            `
        });
    }
</script>
</body>
</html>
