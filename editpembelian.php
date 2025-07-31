<?php
session_start();
include 'koneksi.php';
$nonota = $_GET['nonota'] ?? '';
$suppliers = [];
$supplierQuery = $conn->query("SELECT kodesup, namasup, alamat FROM zsupplier ORDER BY kodesup");
while ($row = $supplierQuery->fetch_assoc()) {
    $suppliers[] = $row;
}
?>
<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Edit Pembelian</title>
    <link rel="stylesheet" href="navbar.css">
    <link rel="stylesheet" href="form.css">
</head>
<body>
    <button class="hamburger" onclick="toggleSidebar()">☰</button>
    <div class="overlay" id="sidebarOverlay" onclick="toggleSidebar()"></div>
    <?php include 'navbar.php'; ?>

    <main>
        <h2>Edit Pembelian</h2>
        <div class="action-pb-bar" style="display: flex; justify-content: space-between; align-items: center; margin-top: 10px;">
            <div style="display: flex; gap: 8px;">
                <button id="btnSave" type="submit">Simpan</button>
                <button id="btnEdit" type="button" onclick="initializeEdit()">Edit</button>
                <button id="btnHapus" type="button" onclick="initializeHapus()">Hapus</button>    
                <button id="btnCancel" type="button" onclick="cancelForm()">Batal</button>
            </div>
            <button id="btnKembali" type="button" onclick="window.location.href='pembelian.php'">Kembali</button>
        </div>

        <form id="formPembelian" action="prosespembelian.php" method="POST">
            <div id="form-pembelian-atas">
                <div class="form-pb-row">
                    <div class="form-pb-col">
                        <label for="tanggal">Tanggal</label>
                        <input type="date" id="tanggal" name="tanggal" class="short-input" style="text-transform: uppercase;">
                    </div>
                    <div class="form-pb-col">
                        <label for="kode_supplier">Kode Supplier</label>
                        <div style="position: relative; flex: 1;">
                            <input type="text" id="kode_sup" name="kode_supplier" class="short-input" style="text-transform: uppercase; width: 100%;">
                            <div id="dropdownKodeSup" class="dropdown-result"></div>
                        </div>
                    </div>
                </div>

                <div class="form-pb-row">
                    <div class="form-pb-col">
                        <label for="no_nota">No Nota</label>
                        <input type="text" id="no_nota" name="no_nota" class="short-input" style="text-transform: uppercase;">
                    </div>
                    <div class="form-pb-col" style="position: relative;">
                        <label for="nama_supplier">Nama Supplier</label>
                        <div style="position: relative; flex: 1;" class="medium-input">
                            <input type="text" id="nama_sup" name="nama_supplier" style="text-transform: uppercase; width: 100%;">
                            <div id="dropdownNamaSup" class="dropdown-result"></div>
                        </div>
                    </div>
                </div>

                <div class="form-pb-row">
                    <div class="form-pb-col">
                        <label for="jt_tempo">Jatuh Tempo</label>
                        <input type="date" id="jt_tempo" name="jt_tempo" class="short-input" style="text-transform: uppercase;">
                    </div>
                    <div class="form-pb-col">
                        <label for="alamat">Alamat</label>
                        <input type="text" id="alamat" name="alamat" class="long-input" style="text-transform: uppercase;">
                    </div>
                </div>
            </div>
            <!-- FORM BAWAH: RINCIAN -->
            <div class="form-pembelian-tengah">
                <div style="display: flex; justify-content: flex-end;">
                    <button id="btnEditItem" type="button">+</button>
                </div>
                <div style="overflow-x: auto;">
                    <table class="tabel-hasil" id="tabelPembelian" style="min-width: 1200px;">
                        <thead>
                            <tr>
                                <th style="min-width: 100px;">Kode brg</th>
                                <th style="min-width: 200px;">Nama brg</th>
                                <th style="min-width: 50px;">Jlh 1</th>
                                <th style="min-width: 80px;">Satuan 1</th>
                                <th style="min-width: 50px;">Jlh 2</th>
                                <th style="min-width: 80px;">Satuan 2</th>
                                <th style="min-width: 50px;">Jlh 3</th>
                                <th style="min-width: 80px;">Satuan 3</th>
                                <th style="min-width: 100px;">Harga</th>
                                <th style="min-width: 50px;">Disca</th>
                                <th style="min-width: 50px;">Discb</th>
                                <th style="min-width: 50px;">Discc</th>
                                <th style="min-width: 100px;">Disc Rp</th>
                                <th style="min-width: 100px;">Jumlah</th>
                                <th style="min-width: 120px;">Aksi</th>
                            </tr>
                        </thead>
                        <tbody>
                            <!-- isi dari popupitempembelian-->
                        </tbody>
                    </table>
                </div>
            </div>

            <div id="form-pembelian-bawah">
                <div class="form-pb-pos">
                    <div class="form-pb-col">
                        <label for="subtotal">Subtotal</label>
                        <input type="number" id="subtotal" name="subtotal" class="short-input">
                    </div>
                    <div class="form-pb-col">
                        <label for="lain_lain">Lain-Lain</label>
                        <input type="number" id="lain_lain" name="lain_lain" class="short-input">
                    </div>
                    <div class="form-pb-col">
                        <label for="ppn">PPN</label>
                        <input type="number" id="ppn" name="ppn" class="short-input">
                    </div>

                    <div class="form-pb-col">
                        <label for="totaljmlh">Total Jumlah</label>
                        <input type="number" id="totaljmlh" name="totaljmlh" class="short-input">
                    </div>
                </div>
            </div>
        </form>
        <div id="popupCariBarang" class="popup-pb-cari" style="display: none;">
            <div class="popup-pb-contentcari">
                <h3>Pilih Barang</h3>
                <table class="tabel-hasil" style="min-width: 700px;">
                    <thead>
                        <tr>
                            <th>Kode</th>
                            <th>Nama</th>
                            <th>Satuan 1</th>
                            <th>Satuan 2</th>
                            <th>Satuan 3</th>
                            <th>Pilih</th>
                        </tr>
                    </thead>
                    <tbody id="tbodyHasilBarang">
                        <!-- hasil dari filter_barang.php -->
                    </tbody>
                </table>
                <div style="text-align: right; margin-top: 10px;">
                    <button type="button" onclick="tutupPopupBarang()">Tutup</button>
                </div>
            </div>
        </div>
        <div id="popupForm" class="popup-pb-overlay" style="display: none;">
            <div class="popup-pb-content">
                <h3>Edit Data Pembelian</h3>
                <form id="formDetailPembelian">
                    <input type="hidden" name="popup_isi1" id="popup_isi1" value=""> 
                    <input type="hidden" name="popup_isi2" id="popup_isi2" value=""> 
                    <div class="popup-pb-row">
                        <label for="popup_kodebrg">Kode Barang</label>
                        <input type="text" id="popup_kodebrg" name="popup_kodebrg" style="text-transform: uppercase;">
                    </div>
                    <div class="popup-pb-row">
                        <label for="popup_namabrg">Nama Barang</label>
                        <input type="text" id="popup_namabrg" name="popup_namabrg" style="text-transform: uppercase;">
                    </div>

                    <div class="popup-pb-row">
                        <label for="popup_jlh1">Jumlah 1</label>
                        <input type="number" id="popup_jlh1" name="popup_jlh1" min="0">
                    </div>
                    <div class="popup-pb-row">
                        <label for="popup_satuan1">Satuan 1</label>
                        <input type="text" id="popup_satuan1" name="popup_satuan1" disabled>
                    </div>

                    <div class="popup-pb-row">
                        <label for="popup_jlh2">Jumlah 2</label>
                        <input type="number" id="popup_jlh2" name="popup_jlh2" min="0" disabled>
                    </div>
                    <div class="popup-pb-row">
                        <label for="popup_satuan2">Satuan 2</label>
                        <input type="text" id="popup_satuan2" name="popup_satuan2" disabled>
                    </div>

                    <div class="popup-pb-row">
                        <label for="popup_jlh3">Jumlah 3</label>
                        <input type="number" id="popup_jlh3" name="popup_jlh3" min="0" disabled>
                    </div>
                    <div class="popup-pb-row">
                        <label for="popup_satuan3">Satuan 3</label>
                        <input type="text" id="popup_satuan3" name="popup_satuan3" disabled>
                    </div>

                    <div class="popup-pb-row">
                        <label for="popup_harga">Harga</label>
                        <input type="number" id="popup_harga" name="popup_harga" required>
                    </div>
                    <div class="popup-pb-row">
                        <label for="popup_disca">Disca</label>
                        <input type="number" id="popup_disca" name="popup_disca" value="0">
                    </div>
                    <div class="popup-pb-row">
                        <label for="popup_discb">Discb</label>
                        <input type="number" id="popup_discb" name="popup_discb" value="0">
                    </div>
                    <div class="popup-pb-row">
                        <label for="popup_discc">Discc</label>
                        <input type="number" id="popup_discc" name="popup_discc" value="0">
                    </div>
                    <div class="popup-pb-row">
                        <label for="popup_discrp">Disc Rp</label>
                        <input type="number" id="popup_discrp" name="popup_discrp" value="0">
                    </div>
                    <div class="popup-pb-row">
                        <label for="popup_jumlah">Jumlah</label>
                        <input type="number" id="popup_jumlah" name="popup_jumlah"required>
                    </div>

                    <div class="popup-pb-row" style="justify-content: flex-end; gap: 10px;">
                        <button id="btnSaveItem" type="submit">Oke</button>
                        <button id="btnCancelItem" type="button" onclick="tutupPopup()">Batal</button>
                    </div>
                </form>
            </div>
        </div>
    </main>

    <div id="toast" style="
        display: none;
        position: fixed;
        top: 20px;
        right: 20px;
        padding: 10px 16px;
        border-radius: 6px;
        color: white;
        font-size: 13px;
        z-index: 1000;
        box-shadow: 0 2px 6px rgba(0,0,0,0.2);
    "></div>

    <div id="popupConfirmHapus" style="
        display: none;
        position: fixed;
        top: 0; left: 0;
        width: 100%; height: 100%;
        background: rgba(0,0,0,0.4);
        z-index: 1001;
    ">
        <div style="
            position: absolute;
            top: 50%; left: 50%;
            transform: translate(-50%, -50%);
            background: white;
            padding: 20px 25px;
            border-radius: 8px;
            box-shadow: 0 4px 15px rgba(0,0,0,0.3);
            width: 320px;
            max-width: 90%;
            text-align: center;
        ">
            <p style="font-size: 14px; margin-bottom: 20px;">Apakah Anda yakin ingin menghapus data ini?</p>
            <button onclick="konfirmasiHapus(true)" style="margin-right: 10px; padding: 6px 12px; background: #dc3545; color: white; border: none; border-radius: 4px;">Ya</button>
            <button onclick="konfirmasiHapus(false)" style="padding: 6px 12px; background: #6c757d; color: white; border: none; border-radius: 4px;">Tidak</button>
        </div>
    </div>
    <script>
        let dataPembelian = [];
        function loadPembelian(nonota) {
            const noNota = new URLSearchParams(window.location.search).get('nonota');

            fetch(`getpembelian.php?nonota=${noNota}`)
            .then(res => res.json())
            .then(data => {
                if (data.status === 'success') {
                    // Isi form header
                    document.getElementById('tanggal').value = data.header.tanggal;
                    document.getElementById('no_nota').value = data.header.no_nota;
                    document.getElementById('kode_sup').value = data.header.kode_sup;
                    document.getElementById('nama_sup').value = data.header.nama_sup;
                    document.getElementById('alamat').value = data.header.alamat;
                    document.getElementById('jt_tempo').value = data.header.jt_tempo;

                    // Tampilkan data ke tabel (data.datapembelian)
                    renderTablePembelian(data.detail.dataPembelian);
                } else {
                    alert(data.message);
                }
            });
        }

        loadPembelian();

        let currentstat = null;
        
        let indexToDelete = null;
        const suppliers = <?php echo json_encode($suppliers); ?>;
        const kodeInput = document.getElementById('kode_sup');
        const namaInput = document.getElementById('nama_sup');
        const alamatInput = document.getElementById('alamat');
        const dropdownKode = document.getElementById('dropdownKodeSup');
        const dropdownNama = document.getElementById('dropdownNamaSup');

        const popupKodeInput = document.getElementById('popup_kodebrg');
        const popupNamaInput = document.getElementById('popup_namabrg');
        const popupSatuan1 = document.getElementById('popup_satuan1');
        const popupSatuan2 = document.getElementById('popup_satuan2');
        const popupSatuan3 = document.getElementById('popup_satuan3');
        const popupIsi1 = document.getElementById('popup_isi1');
        const popupIsi2 = document.getElementById('popup_isi2');
        const popupJlh1 = document.getElementById('popup_jlh1');
        const popupJlh2 = document.getElementById('popup_jlh2');
        const popupJlh3 = document.getElementById('popup_jlh3');

        // Trigger cari saat tekan Enter
        [popupKodeInput, popupNamaInput].forEach(input => {
            input.addEventListener('keypress', function(e) {
                if (e.key === 'Enter') {
                    e.preventDefault();
                    cariBarang(input.id === 'popup_kodebrg' ? 'kode' : 'nama', input.value.trim());
                }
            });
        });

        function cariBarang(mode, keyword) {
            if (!keyword) return;

            fetch('filter_itembarang.php', {
                method: 'POST',
                headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
                body: `mode=${mode}&keyword=${encodeURIComponent(keyword)}`
            })
            .then(res => res.json())
            .then(data => {
                const tbody = document.getElementById('tbodyHasilBarang');
                tbody.innerHTML = '';

                if (data.length === 0) {
                    tbody.innerHTML = '<tr><td colspan="6" style="text-align:center;">Data tidak ditemukan!</td></tr>';
                } else {
                    data.forEach(item => {
                        const tr = document.createElement('tr');
                        tr.innerHTML = `
                            <td>${item.kodebrg}</td>
                            <td>${item.namabrg}</td>
                            <td>${item.satuan1}</td>
                            <td>${item.satuan2}</td>
                            <td>${item.satuan3}</td>
                            <td><button type="button" onclick='pilihBarang(${JSON.stringify(item)})'>Pilih</button></td>
                        `;
                        tbody.appendChild(tr);
                    });
                }

                document.getElementById('popupCariBarang').style.display = 'flex';
            })
            .catch(() => {
                showToast('Terjadi kesalahan saat mencari barang', '#dc3545');
            });
        }

        function pilihBarang(item) {
            document.getElementById('formDetailPembelian').reset();
            popupKodeInput.value = item.kodebrg;
            popupNamaInput.value = item.namabrg;
            popupSatuan1.value = item.satuan1;
            popupSatuan2.value = item.satuan2;
            popupSatuan3.value = item.satuan3;
            popupIsi1.value = item.isi1;
            popupIsi2.value = item.isi2;

            if (popupIsi1.value > 1) {
                popupJlh2.disabled = false;
                if (popupIsi2.value > 1) {
                    popupJlh3.disabled = false;
                } else {
                    popupJlh3.disabled = true;
                }
            } else {
                popupJlh2.disabled = true;
                popupJlh3.disabled = true;
            }
            tutupPopupBarang();
        }

        function tutupPopupBarang() {
            document.getElementById('popupCariBarang').style.display = 'none';
        }

        function renderTabelPembelian() {
            const tbody = document.querySelector('#tabelPembelian tbody');
            tbody.innerHTML = '';

            dataPembelian.forEach((item, index) => {
                const tr = document.createElement('tr');
                tr.innerHTML = `
                    <td>${item.kodebrg}</td>
                    <td>${item.namabrg}</td>
                    <td>${item.jlh1}</td>
                    <td>${item.satuan1}</td>
                    <td>${item.jlh2 || ''}</td>
                    <td>${item.satuan2 || ''}</td>
                    <td>${item.jlh3 || ''}</td>
                    <td>${item.satuan3 || ''}</td>
                    <td>${item.harga}</td>
                    <td>${item.disca}</td>
                    <td>${item.discb}</td>
                    <td>${item.discc}</td>
                    <td>${item.discrp}</td>
                    <td>${item.jumlah}</td>
                    <td>
                        <button type="button" onclick="editItem(${index})">Edit</button>
                        <button type="button" onclick="hapusItem(${index})">Hapus</button>
                    </td>
                `;
                tbody.appendChild(tr);
            });
        }

        document.getElementById('formDetailPembelian').addEventListener('submit', function (e) {
            e.preventDefault();

            const item = {
                kodebrg: document.getElementById('popup_kodebrg').value.trim().toUpperCase(),
                namabrg: document.getElementById('popup_namabrg').value.trim().toUpperCase(),
                jlh1: parseInt(document.getElementById('popup_jlh1').value),
                satuan1: document.getElementById('popup_satuan1').value.trim(),
                jlh2: parseInt(document.getElementById('popup_jlh2').value) || null,
                satuan2: document.getElementById('popup_satuan2').value.trim() || null,
                jlh3: parseInt(document.getElementById('popup_jlh3').value) || null,
                satuan3: document.getElementById('popup_satuan3').value.trim() || null,
                harga: parseFloat(document.getElementById('popup_harga').value),
                disca: parseFloat(document.getElementById('popup_disca').value),
                discb: parseFloat(document.getElementById('popup_discb').value),
                discc: parseFloat(document.getElementById('popup_discc').value),
                discrp: parseFloat(document.getElementById('popup_discrp').value),
                jumlah: parseFloat(document.getElementById('popup_jumlah').value)
            };

            // Edit atau update
            if (formDetailPembelian.dataset.editingIndex) {
                dataPembelian[formDetailPembelian.dataset.editingIndex] = item;
                delete formDetailPembelian.dataset.editingIndex;
            } else {
                dataPembelian.push(item);
                hitungSubtotalDariArray();
            }

            renderTabelPembelian();
            showToast('Item berhasil disimpan!');
        });

        function editItem(index) {
            const item = dataPembelian[index];

            document.getElementById('popup_kodebrg').value = item.kodebrg;
            document.getElementById('popup_namabrg').value = item.namabrg;
            document.getElementById('popup_jlh1').value = item.jlh1;
            document.getElementById('popup_satuan1').value = item.satuan1;

            document.getElementById('popup_jlh2').value = item.jlh2 ?? '';
            document.getElementById('popup_satuan2').value = item.satuan2 ?? '';
            document.getElementById('popup_jlh3').value = item.jlh3 ?? '';
            document.getElementById('popup_satuan3').value = item.satuan3 ?? '';

            document.getElementById('popup_harga').value = item.harga;
            document.getElementById('popup_disca').value = item.disca;
            document.getElementById('popup_discb').value = item.discb;
            document.getElementById('popup_discc').value = item.discc;
            document.getElementById('popup_discrp').value = item.discrp;
            document.getElementById('popup_jumlah').value = item.jumlah;

            // Enable input tergantung jumlah
            document.getElementById('popup_jlh2').disabled = !(item.jlh1 > 1);
            document.getElementById('popup_satuan2').disabled = !(item.jlh1 > 1);
            document.getElementById('popup_jlh3').disabled = !(item.jlh2 > 1);
            document.getElementById('popup_satuan3').disabled = !(item.jlh2 > 1);

            formDetailPembelian.dataset.editingIndex = index;
            document.getElementById('popupForm').style.display = 'flex';
        }

        function hapusItem(index) {
            indexToDelete = index;
            document.getElementById('popupConfirmHapus').style.display = 'block';
        }

        // Fungsi ketika user klik "Ya" atau "Tidak"
        function konfirmasiHapus(ya) {
            document.getElementById('popupConfirmHapus').style.display = 'none';
            if (ya && indexToDelete !== null) {
                dataPembelian.splice(indexToDelete, 1);
                renderTabelPembelian();
                hitungSubtotalDariArray();
                showToast('Item berhasil dihapus!', '#dc3545');
            }
            indexToDelete = null;
        }

        const inputFields = [
            'popup_jlh1', 'popup_jlh2', 'popup_jlh3',
            'popup_harga', 'popup_disca', 'popup_discb',
            'popup_discc', 'popup_discrp', 'popup_satuan2', 'popup_satuan3'
        ];

        inputFields.forEach(id => {
            document.getElementById(id).addEventListener('input', hitungJumlahPembelian);
        });

        function hitungJumlahPembelian() {
            let jlh1 = parseFloat(document.getElementById('popup_jlh1').value) || 0;
            let jlh2 = parseFloat(document.getElementById('popup_jlh2').value) || 0;
            let jlh3 = parseFloat(document.getElementById('popup_jlh3').value) || 0;
            let harga = parseFloat(document.getElementById('popup_harga').value) || 0;

            let isi1 = parseFloat(document.getElementById('popup_isi1').value) || 0;
            let isi2 = parseFloat(document.getElementById('popup_isi2').value) || 0;

            let disca = parseFloat(document.getElementById('popup_disca').value) || 0;
            let discb = parseFloat(document.getElementById('popup_discb').value) || 0;
            let discc = parseFloat(document.getElementById('popup_discc').value) || 0;
            let discrp = parseFloat(document.getElementById('popup_discrp').value) || 0;

            let hasil1 = jlh1 * harga;
            let hasil2 = (jlh2 > 0 && isi1 > 0) ? (harga / isi1) * jlh2 : 0;
            let hasil3 = (jlh3 > 0 && isi1 > 0 && isi2 > 0) ? (harga / (isi1 * isi2)) * jlh3 : 0;

            let smntarajlmh = hasil1 + hasil2 + hasil3;

            let afterDisca = smntarajlmh * disca / 100;
            let smntaradis1 = smntarajlmh - afterDisca;

            let afterDiscb = smntaradis1 * discb / 100;
            let smntaradis2 = smntaradis1 - afterDiscb;

            let afterDiscc = smntaradis2 * discc / 100;
            let smntaradis3 = smntaradis2 - afterDiscc;

            let finalJumlah = smntaradis3 - discrp;

            document.getElementById('popup_jumlah').value = Math.round(finalJumlah);
        }

        document.getElementById('lain_lain').addEventListener('input', function() {
            hitungSubtotalDariArray();
        });

        function hitungSubtotalDariArray() {
            let subtotal = 0;
            dataPembelian.forEach(item => {
                subtotal += parseFloat(item.jumlah) || 0;
            });

            let ppn = subtotal * 11 / 100;
            let lainLain = parseFloat(document.getElementById('lain_lain').value) || 0;
            let totaljmlh = subtotal + ppn + lainLain;

            document.getElementById('subtotal').value = Math.round(subtotal);
            document.getElementById('ppn').value = Math.round(ppn);
            document.getElementById('totaljmlh').value = Math.round(totaljmlh);
        }

        function initializeFormButtons() {
            currentstat = null;

            document.getElementById('btnEdit').disabled = false;
            document.getElementById('btnEditItem').disabled = true;
            document.getElementById('btnCancel').disabled = true;
            document.getElementById('btnSave').disabled = true;

            document.getElementById('tanggal').disabled = true;
            document.getElementById('no_nota').disabled = true;
            document.getElementById('jt_tempo').disabled = true;
            document.getElementById('nama_sup').disabled = true;
            document.getElementById('kode_sup').disabled = true;
            document.getElementById('alamat').disabled = true;
            document.getElementById('subtotal').disabled = true;
            document.getElementById('lain_lain').disabled = true;
            document.getElementById('ppn').disabled = true;
            document.getElementById('totaljmlh').disabled = true;
        }
        initializeFormButtons();

        function initializeEdit() {
            currentstat = 'update';
            showToast('Kamu sedang menambah data...', '#ffc107');

            const today = new Date().toISOString().split('T')[0];
            document.getElementById("tanggal").value = today;
            document.getElementById("jt_tempo").value = today;

            document.getElementById('btnEdit').disabled = true;
            document.getElementById('btnCancel').disabled = false;
            document.getElementById('btnSave').disabled = false;

            document.getElementById('tanggal').disabled = false;
            document.getElementById('no_nota').disabled = false;
            document.getElementById('jt_tempo').disabled = false;
            document.getElementById('nama_sup').disabled = false;
            document.getElementById('kode_sup').disabled = false;
            document.getElementById('alamat').disabled = false;
            document.getElementById('subtotal').disabled = false;
            document.getElementById('lain_lain').disabled = false;
            document.getElementById('ppn').disabled = false;
            document.getElementById('totaljmlh').disabled = false;

            const nota = document.getElementById('no_nota').value.trim();
            document.getElementById('btnEditItem').disabled = (nota === '');
        }

        function cancelForm() {
            initializeFormButtons();
                // Reset form
            // Set kembali tanggal dan jatuh tempo ke hari ini
            const today = new Date().toISOString().split('T')[0];
            document.getElementById("tanggal").value = today;
            document.getElementById("jt_tempo").value = today;
        }

        document.getElementById('btnEditItem').addEventListener('click', () => {
            document.getElementById('popupForm').style.display = 'flex';
        });

        // Tutup popup
        function tutupPopup() {
            document.getElementById('formDetailPembelian').reset();
            delete formDetailPembelian.dataset.editingIndex;
            document.getElementById('popupForm').style.display = 'none';
            popupJlh1.disabled = true;
            popupJlh2.disabled = true;
            popupJlh3.disabled = true;
        }

        

        function filterDropdown(input, list, key) {
            const val = input.value.trim().toUpperCase();
            return list.filter(item => item[key].toUpperCase().includes(val));
        }

        function syncDropdownWidth(input, dropdown) {
            dropdown.style.width = input.offsetWidth + 'px';
        }

        function showDropdown(input, dropdown, data, mode) {
            dropdown.innerHTML = '';
            if (data.length === 0 || !input.value.trim()) {
                dropdown.style.display = 'none';
                return;
            }

            data.forEach(item => {
                const div = document.createElement('div');
                div.className = 'dropdown-item';
                if (mode === 'kode') {
                    div.textContent = `${item.kodesup}`;
                    div.onclick = () => {
                        kodeInput.value = item.kodesup;
                        namaInput.value = item.namasup;
                        alamatInput.value = item.alamat;
                        dropdown.innerHTML = '';
                        dropdown.style.display = 'none';
                    };
                } else {
                    div.textContent = `${item.namasup}`;
                    div.onclick = () => {
                        namaInput.value = item.namasup;
                        kodeInput.value = item.kodesup;
                        alamatInput.value = item.alamat;
                        dropdown.innerHTML = '';
                        dropdown.style.display = 'none';
                    };
                }
                dropdown.appendChild(div);
            });
            syncDropdownWidth(input, dropdown); 
            dropdown.style.display = 'block';
        }

        kodeInput.addEventListener('input', () => {
            const result = filterDropdown(kodeInput, suppliers, 'kodesup');
            showDropdown(kodeInput, dropdownKode, result, 'kode');
        });

        namaInput.addEventListener('input', () => {
            const result = filterDropdown(namaInput, suppliers, 'namasup');
            showDropdown(namaInput, dropdownNama, result, 'nama');
        });

        document.addEventListener('click', function(e) {
            if (!kodeInput.contains(e.target)) dropdownKode.style.display = 'none';
            if (!namaInput.contains(e.target)) dropdownNama.style.display = 'none';
        });

        window.addEventListener("DOMContentLoaded", () => {
            const today = new Date().toISOString().split('T')[0];
            document.getElementById("tanggal").value = today;
            document.getElementById("jt_tempo").value = today;
        });

        // Enable btnEditItem jika no_nota terisi
        document.getElementById('no_nota').addEventListener('input', function () {
            const noNota = this.value.trim();
            document.getElementById('btnEditItem').disabled = (noNota === '');
        });

        document.getElementById('btnSave').addEventListener('click', () => {
            const data = {
                no_nota: document.getElementById('no_nota').value,
                tanggal: document.getElementById('tanggal').value,
                kode_sup: document.getElementById('kode_sup').value,
                totaljmlh: parseFloat(document.getElementById('totaljmlh').value) || 0,
                detail: dataPembelian // array yang sudah kamu simpan saat update item
            };

            fetch('simpanpembelian.php', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(data)
            })
            .then(res => res.json())
            .then(res => {
                if (res.success) {
                    showToast('Data berhasil disimpan!');
                    window.location.reload();
                } else {
                    showToast('Gagal menyimpan: ' + res.message, '#dc3545');
                }
            })
            .catch(err => {
                showToast('Error: ' + err, '#dc3545');
            });
        });

        function showToast(pesan, warna = '#28a745') {
            const toast = document.createElement('div');
            toast.textContent = pesan;
            toast.style.position = 'fixed';
            toast.style.top = '20px';   
            toast.style.right = '20px';
            toast.style.background = warna;
            toast.style.color = 'white';
            toast.style.padding = '10px 15px';
            toast.style.borderRadius = '6px';
            toast.style.boxShadow = '0 2px 6px rgba(0,0,0,0.2)';
            toast.style.zIndex = 1000;
            document.body.appendChild(toast);
            setTimeout(() => toast.remove(), 3000);
        }
    </script>
</body>
</html>
