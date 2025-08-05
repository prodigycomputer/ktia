<!DOCTYPE html>
<html>
<head>
    <meta charset="UTF-8">
    <title>Cetak Nota </title>
    <script src="fungsi.js"></script>
    <style>
        html, body {
            margin: 0;
            padding: 0;
            font-family: Arial, sans-serif;
            font-size: 12px;
            display: flex;
            justify-content: center;
            background-color: #eee; /* agar terlihat batas A5 */
        }

        .container-a5 {
            width: 794px; /* A5 landscape width */
            height: 559px; /* A5 l*/
            background-color: #fff;
            margin: 20px auto;
            padding: 30px;
            box-shadow: 0 0 5px rgba(0,0,0,0.2);
            box-sizing: border-box;
        }

        h2 {
            margin-bottom: 10px;
        }
  
        .form-header {
            display: grid;
            grid-template-columns: 1fr 1.6fr; /* 2 kolom */
            gap: 3px 0px;
            margin-bottom: 3px;
            font-size: 12px;
        }

        .form-header .field {
            display: flex;
            align-items: center;
        }

        .form-header label {
            min-width: 100px;
            font-weight: bold;
        }

        .form-header span {
            flex: 1;
            border: 1px #ccc;
            padding: 2px 6px;
            font-family: monospace;
        }

        .form-footer {
            display: flex;
            flex-direction: column;
            align-items: flex-end;
            gap: 4px;
            font-size: 12px;
        }

        .form-footer div {
            display: flex;
            align-items: center;
            justify-content: flex-start;
            min-width: 100px;
        }

        .form-footer label {
            flex: 1;
            text-align: left;
        }

        .form-footer span {
            flex: none;
            min-width: 100px;
            text-align: right;
            border: 1px #ccc;
            padding: 2px 6px;
            font-family: monospace;
        }

        table {
            border-collapse: collapse;
            width: 100%;
            margin-bottom: 3px;
        }

        th {
            border: 1px solid #aaa;
            padding: 3px;
            text-align: center;
            font-size: 12px;
        }

        td {
            border: 1px solid #aaa;
            padding: 3px;
            text-align: left;
            font-size: 12px;
        }

        .action-buttons {
            display: flex;
            justify-content: flex-start;
            margin-top: 10px;
            gap: 10px;
        }

        .action-buttons button {
            padding: 8px 16px;
            font-size: 12px;
            cursor: pointer;
        }

        /* Optional: cetak tetap sesuai A5 */   
        @page {
            size: A5 landscape;
            margin: 0;
        }

        @media print {
            html, body {
                margin: 0;
                padding: 0;
                background: none;
                width: 100%;
                height: 100%;
            }

            body * {
                visibility: hidden;
            }

            .container-a5, .container-a5 * {
                visibility: visible;
            }

            .container-a5 {
                position: absolute;
                left: 0;
                top: 0;
                width: 100vw;
                height: 100vh;
                box-shadow: none;
                margin: 0;
                padding: 10mm;
                background: white;
                box-sizing: border-box;
                overflow: hidden;
            }
            .action-buttons {
                display: none;
            }
        }

    </style>
</head>
<body>
    <div class="container-a5">
        <?php include 'header.php'; ?>

        <div class="form-header">
            <div class="field"><label>Tanggal :</label> <span id="tanggal"></span></div>
            <div class="field"><label>Kode Supplier :</label> <span id="kode_sup"></span></div>
            <div class="field"><label>No Nota :</label> <span id="no_nota"></span></div>
            <div class="field"><label>Nama Supplier :</label> <span id="nama_sup"></span></div>
            <div class="field"><label>Jatuh Tempo :</label> <span id="jt_tempo"></span></div>
            <div class="field"><label>Alamat :</label> <span id="alamat"></span></div>
        </div>

        <table>
            <thead>
                <tr>
                    <th style="width: 50px;">Kode brg</th>
                    <th style="width: 150px;">Nama brg</th>
                    <th style="width: 70px;">QTY</th>
                    <th style="width: 70px;">Disc %</th>
                    <th style="width: 10px;">Disrp</th>
                    <th style="width: 10px;">Harga</th>
                    <th style="width: 10px;">Jumlah</th>
                </tr>
            </thead>
            <tbody id="table-detail">
                <!-- diisi via JS -->
            </tbody>
        </table>

        <div class="form-footer">
            <div><label>Subtotal :</label> <span id="subtotal"></span></div>
            <div><label>Lain-Lain :</label> <span id="lain">0</span></div>
            <div><label>PPN :</label> <span id="ppn"></span></div>
            <div style="border-top: 1px solid #000; padding-top: 4px;">
                <label><strong>Total Jumlah :</strong></label>
                <span id="totaljmlh" style="font-weight: bold;"></span>
            </div>
        </div>

        <div class="action-buttons">
            <button onclick="window.location.href='inputpembelian.php'">← Kembali</button>
            <button onclick="window.print()">🖨️ Print</button>
        </div>

        <script>
            const noNota = new URLSearchParams(window.location.search).get('nonota');

            fetch(`getpembelian.php?nonota=${encodeURIComponent(noNota)}`)
                .then(response => response.json())
                .then(data => {
                    if (data.status !== 'success') {
                        alert('Data tidak ditemukan!');
                        return;
                    }

                    const h = data.header;
                    const d = data.detail;

                    document.getElementById('tanggal').textContent = h.tanggal;
                    document.getElementById('no_nota').textContent = h.no_nota;
                    document.getElementById('jt_tempo').textContent = h.jt_tempo;
                    document.getElementById('kode_sup').textContent = h.kode_sup;
                    document.getElementById('nama_sup').textContent = h.nama_sup;
                    document.getElementById('alamat').textContent = h.alamat;
                    let subtotal = 0;
                    const tbody = document.getElementById('table-detail');
                    d.forEach(item => {
                        const qty = tampilKuantitas(item);
                        const disc = tampilDiskon(item);
                        const jumlah = parseFloat(item.jumlah) || 0;
                        subtotal += jumlah;

                        const tr = document.createElement('tr');
                        tr.innerHTML = `
                            <td>${item.kodebrg}</td>
                            <td>${item.namabrg}</td>
                            <td style="text-align: right;">${qty}</td>
                            <td style="text-align: right;">${disc}</td>
                            <td style="text-align: right;">${parseInt(item.discrp).toLocaleString('id-ID', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</td>
                            <td style="text-align: right;">${parseInt(item.harga).toLocaleString('id-ID', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</td>
                            <td style="text-align: right;">${parseInt(jumlah).toLocaleString('id-ID', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</td>
                        `;
                        tbody.appendChild(tr);
                    });

                    const lain = 0; // atau ambil dari data jika dinamis
                    const ppn = Math.round(subtotal * 0.11);
                    const total = subtotal + lain + ppn;

                    document.getElementById('subtotal').textContent = subtotal.toLocaleString('id-ID', { minimumFractionDigits: 2, maximumFractionDigits: 2 });
                    document.getElementById('ppn').textContent = ppn.toLocaleString('id-ID', { minimumFractionDigits: 2, maximumFractionDigits: 2 });
                    document.getElementById('totaljmlh').textContent = total.toLocaleString('id-ID', { minimumFractionDigits: 2, maximumFractionDigits: 2 });



                    // otomatis cetak
                    setTimeout(() => {
                        window.print();
                    }, 200);
                })
                .catch(err => {
                    console.error(err);
                    alert("Gagal mengambil data");
                });
        </script>
    </div>



</body>
</html>
