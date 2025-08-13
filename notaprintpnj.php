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

        .form-kiri {
            width: 30%;
            margin-right: 10px; /* Jarak ke kanan */
        }

        .form-kanan {
            width: 60%;
            margin-left: 10px; /* Jarak ke kiri */
        }


        .field {
            display: flex;
            align-items: center;
        }

        .field label:first-child {
            width: 100px;
            font-weight: bold;
        }

        .field label:nth-child(2) {
            margin-right: 4px;
            width: 10px;
        }

        .field span {
            flex: 1;
        
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
        }

        .form-footer label {
            flex: 1;
            text-align: left;
        }

        .form-footer span {
            flex: none;
            text-align: right;
            border: 1px #ccc;
            padding: 2px 6px;
            font-family: monospace;
        }

        .span-kecil { width: 30px; }
        .span-sedang { width: 70px; }
        .span-besar  { width: 112px; }

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
            font-size: 11px;
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
            <div class="form-kiri">
                <div class="field">
                    <label>Tanggal</label>
                    <label>:</label>
                    <span id="tanggal"></span>
                </div>
                <div class="field">
                    <label>No Nota</label>
                    <label>:</label>
                    <span id="no_nota"></span>
                </div>
                <div class="field">
                    <label>Jatuh Tempo</label>
                    <label>:</label>
                    <span id="jt_tempo"></span>
                </div>
            </div>

            <div class="form-kanan">
                <div class="field">
                    <label>Kode Supplier</label>
                    <label>:</label>
                    <span id="kode_sup"></span>
                </div>
                <div class="field">
                    <label>Nama Supplier</label>
                    <label>:</label>
                    <span id="nama_sup"></span>
                </div>
                <div class="field">
                    <label>Alamat</label>
                    <label>:</label>
                    <span id="alamat"></span>
                </div>
            </div>
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
            <div><label>Subtotal :</label> <span id="subtotal" class="span-besar"></span></div>
            <div><label>Diskon 1 :</label> <span id="diskon1" class="span-kecil"></span><span id="hdiskon1" class="span-sedang"></span></div>
            <div><label>Diskon 2 :</label> <span id="diskon2" class="span-kecil"></span><span id="hdiskon2" class="span-sedang"></span></div>
            <div><label>Diskon 3 :</label> <span id="diskon3" class="span-kecil"></span><span id="hdiskon3" class="span-sedang"></span></div>
            <div><label>Lain-Lain :</label> <span id="lain" class="span-besar"></span></div>
            <div><label>PPN :</label> <span id="ppn" class="span-kecil">></span><span id="hppn" class="span-sedang"></span></div>
            <div style="border-top: 1px solid #000; padding-top: 4px;">
                <label><strong>Total Jumlah :</strong></label>
                <span id="totaljmlh" style="font-weight: bold;" class="span-besar"></span>
            </div>
        </div>

        <div class="action-buttons">
            <button onclick="goBack()">← Kembali</button>
            <button onclick="window.print()">🖨️ Print</button>
        </div>

        <script>
            const noNota = new URLSearchParams(window.location.search).get('nonota');

            fetch(`getpenjualan.php?nonota=${encodeURIComponent(noNota)}`)
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
                            <td style="text-align: right;">${parseFloat(item.discrp).toLocaleString('id-ID', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</td>
                            <td style="text-align: right;">${parseFloat(item.harga).toLocaleString('id-ID', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</td>
                            <td style="text-align: right;">${parseFloat(jumlah).toLocaleString('id-ID', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</td>
                        `;
                        tbody.appendChild(tr);
                    });

                    const ppn = parseFloat(h.prsnppn);
                    const hppn = parseFloat(h.hrgppn);
                    const diskon1 = parseFloat(h.disk1);
                    const hdiskon1 = parseFloat(h.hdisk1);
                    const diskon2 = parseFloat(h.disk2);
                    const hdiskon2 = parseFloat(h.hdisk2);
                    const diskon3 = parseFloat(h.disk3);
                    
                    const hdiskon3 = parseFloat(h.hdisk3);
                    const total = parseFloat(h.totaljmlh);

                    let totalhasil = subtotal - hdiskon1 - hdiskon2 - hdiskon3 + hppn;
                    let lain = h.totaljmlh - totalhasil; // langsung dari DB, bukan subtotal hitungan ulang

                    if (parseFloat(ppn) === 0 && parseFloat(hppn) === 0) {
                        document.getElementById('ppn').parentElement.style.display = 'none';
                        document.getElementById('hppn').parentElement.style.display = 'none';
                    }
                    document.getElementById('subtotal').textContent = subtotal.toLocaleString('id-ID', { minimumFractionDigits: 2, maximumFractionDigits: 2 });
                    document.getElementById('ppn').textContent = ppn.toLocaleString('id-ID', { minimumFractionDigits: 2, maximumFractionDigits: 2 }) + '%' ;
                    document.getElementById('hppn').textContent = hppn.toLocaleString('id-ID', { minimumFractionDigits: 2, maximumFractionDigits: 2 });
                    document.getElementById('lain').textContent = lain.toLocaleString('id-ID', { minimumFractionDigits: 2, maximumFractionDigits: 2 });
                    document.getElementById('diskon1').textContent = diskon1.toLocaleString('id-ID', { minimumFractionDigits: 2, maximumFractionDigits: 2 }) + '%';
                    document.getElementById('hdiskon1').textContent = hdiskon1.toLocaleString('id-ID', { minimumFractionDigits: 2, maximumFractionDigits: 2 });
                    document.getElementById('diskon2').textContent = diskon2.toLocaleString('id-ID', { minimumFractionDigits: 2, maximumFractionDigits: 2 }) + '%';
                    document.getElementById('hdiskon2').textContent = hdiskon2.toLocaleString('id-ID', { minimumFractionDigits: 2, maximumFractionDigits: 2 });
                    document.getElementById('diskon3').textContent = diskon3.toLocaleString('id-ID', { minimumFractionDigits: 2, maximumFractionDigits: 2 }) + '%';
                    document.getElementById('hdiskon3').textContent = hdiskon3.toLocaleString('id-ID', { minimumFractionDigits: 2, maximumFractionDigits: 2 });
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

                function goBack() {
                    const from = new URLSearchParams(window.location.search).get('from');
                    if (from) {
                        window.location.href = from;
                    } else {
                        window.history.back();
                    }
                }
        </script>
    </div>



</body>
</html>
