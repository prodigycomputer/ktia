<?php
require_once 'init.php';
$query = $conn->query("SELECT jmlharga FROM zconfig LIMIT 1");
$row = $query ? $query->fetch_assoc() : null;
$jmlharga = ($row && is_numeric($row['jmlharga'])) ? (int)$row['jmlharga'] : 0;

?>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Data Barang</title>
    <link rel="stylesheet" href="navbar.css">
    <link rel="stylesheet" href="form.css">
    <script src="button.js"></script>
</head>
<body>
    <button class="hamburger" onclick="toggleSidebar()">☰</button>
    <div class="overlay" id="sidebarOverlay" onclick="toggleSidebar()"></div>
    <?php 
        require_once 'akses.php';
    ?>

    <main>
        <h2>Data Barang</h2>

            <div class="search-bar">
                <input type="text" id="searchKode" class="search-kode" placeholder="Kode barang" oninput="handleInput('kode')" style="text-transform: uppercase;">
                <input type="text" id="searchNama" class="search-nama" placeholder="Nama barang" oninput="handleInput('nama')" style="text-transform: uppercase;">
                <button type="button" id="searchbtn" onclick="triggerSearch()">🔍 Cari</button>
            </div>

            <form id="barangForm" action="prosesbarang.php" method="POST" enctype="multipart/form-data" onsubmit="return validateForm()">
                <input type="hidden" name="aksi" id="aksi" value="">
                <input type="hidden" name="kodebrg_lama" id="kodebrg_lama" value=""> 

                <div class="form-atas">
                    <label for="kodemerek">Kode Merek</label>
                    <input type="text" name="kodemerek" id="kodemerek" class="medium-input" required style="text-transform: uppercase;">

                    <label for="kodegolongan">Kode Golongan</label>
                    <input type="text" name="kodegolongan" id="kodegolongan" class="medium-input" required style="text-transform: uppercase;">

                    <label for="kodegrup">Kode Grup</label>
                    <input type="text" name="kodegrup" id="kodegrup" class="medium-input" required style="text-transform: uppercase;">

                    <label for="kodebrg">Kode Barang</label>
                    <input type="text" name="kodebrg" id="kodebrg" class="long-input" required style="text-transform: uppercase;">

                    <label for="namabrg">Nama Barang</label>
                    <input type="text" name="namabrg" id="namabrg" class="verylong-input" required style="text-transform: uppercase;">

                    <label for="satuan1">Satuan 1</label>
                    <input type="text" name="satuan1" id="satuan1" class="lesslong-input" required style="text-transform: uppercase;">

                    <label for="isi1">Isi 1</label>
                    <input type="number" step="0.01" name="isi1" id="isi1" class="medium-input" oninput="checkIsi1()" >

                    <label for="satuan2">Satuan 2</label>
                    <input type="text" name="satuan2" id="satuan2" class="lesslong-input" disabled style="text-transform: uppercase;">

                    <label for="isi2">Isi 2</label>
                    <input type="number" step="0.01" name="isi2" id="isi2" class="medium-input" disabled oninput="checkIsi2()">

                    <label for="satuan3">Satuan 3</label>
                    <input type="text" name="satuan3" id="satuan3" class="lesslong-input" disabled style="text-transform: uppercase;">

                    <label for="hargabeli">Harga Beli</label>
                    <input type="number" step="0.01" name="hargabeli" id="hargabeli" class="medium-input" disabled>

                    <label for="gambar">Gambar</label>
                    <div style="display: flex; gap: 5px; align-items: center;">
                        <input type="text" name="gambar" id="gambar" class="lesslong-input" readonly>
                        <input type="file" id="uploadInput" accept=".png" style="display: none;" onchange="handleFileUpload(this)">
                        <button type="button" id="upload" style="background: #218838;" onclick="document.getElementById('uploadInput').click();">Upload</button>
                    </div>

                </div>
                <div class="form-bawah">
                    <button type="button" id= "btnHarga" onclick="openHargaPopup()" style="margin-top: 10px; background-color:#17a2b8; color:white; padding:6px 14px; border:none; border-radius:4px;">💰 Harga</button>
                    <div id="hiddenHargaFields"></div>
                </div>

            <button id="btnSave" type="submit">Simpan</button>
            <button id="btnTambah" type="button" onclick="initializeTambah() ">Tambah</button>
            <button id="btnEdit" type="button" onclick="initializeUbah()">Ubah</button>
            <button id="btnHapus" type="button" onclick="tampilkanKonfirmasiHapus()">Hapus</button>
            <button id="btnCancel" type="button" onclick="cancelEdit()">Batal</button>

        </form>
        <div id="popupCariGrup" class="popup-pb-cari" style="display: none;">
            <div class="popup-pb-contentcari">
                <h3>Pilih Sales</h3>
                <table class="tabel-hasil" style="min-width: 700px;">
                    <thead>
                        <tr>
                            <th>Kode</th>
                            <th>Nama</th>
                            <th>Aksi</th>
                        </tr>
                    </thead>
                    <tbody id="tbodyHasilGrup">
                        <!-- hasil dari filter_barang.php -->
                    </tbody>
                </table>
                <div style="text-align: right; margin-top: 10px;">
                    <button type="button" onclick="tutupPopupGrup()">Tutup</button>
                </div>
            </div>
        </div>
        <div id="popupCariMerek" class="popup-pb-cari" style="display: none;">
            <div class="popup-pb-contentcari">
                <h3>Pilih Sales</h3>
                <table class="tabel-hasil" style="min-width: 700px;">
                    <thead>
                        <tr>
                            <th>Kode</th>
                            <th>Nama</th>
                            <th>Aksi</th>
                        </tr>
                    </thead>
                    <tbody id="tbodyHasilMerek">
                        <!-- hasil dari filter_barang.php -->
                    </tbody>
                </table>
                <div style="text-align: right; margin-top: 10px;">
                    <button type="button" onclick="tutupPopupMerek()">Tutup</button>
                </div>
            </div>
        </div>
        <div id="popupCariGolongan" class="popup-pb-cari" style="display: none;">
            <div class="popup-pb-contentcari">
                <h3>Pilih Sales</h3>
                <table class="tabel-hasil" style="min-width: 700px;">
                    <thead>
                        <tr>
                            <th>Kode</th>
                            <th>Nama</th>
                            <th>Aksi</th>
                        </tr>
                    </thead>
                    <tbody id="tbodyHasilGolongan">
                        <!-- hasil dari filter_barang.php -->
                    </tbody>
                </table>
                <div style="text-align: right; margin-top: 10px;">
                    <button type="button" onclick="tutupPopupGolongan()">Tutup</button>
                </div>
            </div>
        </div>
    </main>
    
        <!-- Popup Pilih Data -->
    <div id="popupFilter" style="display:none; position:fixed; top:0; left:0; width:100%; height:100%; background:rgba(0,0,0,0.4); z-index:1000;">
        <div id="popupContent" style="
            position:absolute; 
            top:50%; left:50%; transform:translate(-50%, -50%); 
            width:90%; 
            max-width:1300px; 
            background:#fff; 
            border-radius:8px; 
            box-shadow:0 4px 15px rgba(0,0,0,0.3); 
            padding:16px 16px 10px; 
            max-height:80vh; 
            overflow:auto;
        ">

            <div style="display:flex; justify-content:space-between; align-items:center; margin-bottom:10px;">
            <h3 style="font-size:14px; margin:0;">Hasil Pencarian</h3>
            <span onclick="closeFilterPopup()" style="cursor:pointer; font-weight:bold; font-size:16px; color:#666;">&times;</span>
            </div>

            <div style="max-height: 60vh; overflow: auto; border: 1px solid #ccc;">
                <table style="min-width: 800px; border-collapse: collapse; font-size: 12px;">
                    <thead style="background:#f2f2f2;">
                    </thead>
                    <tbody id="popupList"></tbody>
                </table>
            </div>
        </div>
    </div>

    <!-- ✅ POPUP HARGA -->
    <div id="popupHarga" style="display:none; position:fixed; top:0; left:0; width:100%; height:100%; background:rgba(0,0,0,0.4); z-index:1000;">
        <div style="
            position: absolute;
            top: 50%; left: 50%;
            transform: translate(-50%, -50%);
            width: 95%;
            max-width: 60vw;
            max-height: 90vh;
            background: #fff;
            padding: 20px;
            border-radius: 8px;
            box-shadow: 0 4px 10px rgba(0,0,0,0.3);
            overflow: hidden;
            display: flex;
            flex-direction: column;
        ">
            <!-- Header -->
            <div style="display:flex; justify-content:space-between; align-items:center; margin-bottom:10px;">
                <h3 style="margin:0; font-size:16px;">Atur Harga</h3>
                <span onclick="closeHargaPopup()" style="cursor:pointer; font-size:18px; font-weight:bold;">&times;</span>
            </div>

            <!-- Scrollable container -->
            <div style="flex:1; overflow-x:auto; overflow-y:auto;">
                <div id="hargaInputsContainer" style="
                    display: flex;
                    gap: 20px;
                    min-width: max-content;
                "></div>
            </div>

            <!-- Tombol simpan -->
            <div style="margin-top:15px; text-align:right;">
                <button type="button" onclick="simpanHarga()" style="padding:6px 14px; background:#28a745; color:white; border:none; border-radius:4px;">Simpan</button>
            </div>
        </div>
    </div>


    <div id="toast" style="
        display: none;
        position: fixed;
        top: 20px;
        right: 20px;
        background-color: #333;
        color: #fff;
        padding: 12px 20px;
        border-radius: 6px;
        font-size: 14px;
        z-index: 9999;
        box-shadow: 0 2px 6px rgba(0,0,0,0.2);
        opacity: 0.95;
    ">
    </div>

    <!-- ✅ POPUP KONFIRMASI HAPUS -->
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
        let previousFormData = {};
        let inputSearch = null;
        let searchBtn = document.getElementById('searchbtn')
        let hargaData = []

        const kodeMerkInput = document.getElementById('kodemerek');
        const kodeGolInput = document.getElementById('kodegolongan');
        const kodeGrupInput = document.getElementById('kodegrup');

        const hakUbah  = <?php echo $hakUbah; ?>;
        const hakHapus = <?php echo $hakHapus; ?>;

        function cekAkses(aksi) {
            if (aksi === 'ubah' && hakUbah === 0) {
                showToast("Anda tidak bisa mengakses Edit!", "#dc3545");
                return false;
            }
            if (aksi === 'hapus' && hakHapus === 0) {
                showToast("Anda tidak bisa mengakses Hapus!", "#dc3545");
                return false;
            }
            return true;
        }

        initializeFormButtons({
            extraButtons: ["btnHarga"],
            fields: [
                "kodemerek","kodegolongan","kodegrup","kodebrg","namabrg",
                "satuan1","isi1","satuan2","isi2","satuan3",
                "hargabeli","gambar","upload"
            ]
        });

        function initializeFormButtonsCancel() {
            currentstat = null;
            document.getElementById('kodebrg').value = previousFormData.kodebrg;
            document.getElementById('kodebrg_lama').value = previousFormData.kodebrg_lama;
            document.getElementById('namabrg').value = previousFormData.namabrg;
            document.getElementById('kodegrup').value = previousFormData.kodegrup;
            document.getElementById('kodemerek').value = previousFormData.kodemerek;
            document.getElementById('kodegolongan').value = previousFormData.kodegolongan;
            document.getElementById('satuan1').value = previousFormData.satuan1;
            document.getElementById('isi1').value = previousFormData.isi1;
            document.getElementById('satuan2').value = previousFormData.satuan2;
            document.getElementById('isi2').value = previousFormData.isi2;
            document.getElementById('satuan3').value = previousFormData.satuan3;
            document.getElementById('hargabeli').value = previousFormData.hargabeli;
            hargaData = JSON.parse(JSON.stringify(previousFormData.hargaData));
            generateHargaInputs(<?= $jmlharga ?>);

            document.getElementById('btnTambah').disabled = true;
            document.getElementById('btnEdit').disabled = false;
            document.getElementById('btnHapus').disabled = false;
            document.getElementById('btnCancel').disabled = false; 
            document.getElementById('btnSave').disabled = true;

            document.getElementById('kodebrg').disabled = true;
            document.getElementById('namabrg').disabled = true;
            document.getElementById('kodegrup').disabled = true;
            document.getElementById('kodemerek').disabled = true;
            document.getElementById('kodegolongan').disabled = true;
            document.getElementById('satuan1').disabled = true;
            document.getElementById('satuan2').disabled = true;
            document.getElementById('satuan3').disabled = true;
            document.getElementById('isi1').disabled = true;
            document.getElementById('isi2').disabled = true;
            document.getElementById('hargabeli').disabled = true;
            document.getElementById('gambar').disabled = true;
            document.getElementById('upload').disabled = true;

            document.getElementById('searchKode').value = '';
            document.getElementById('searchNama').value = '';
            document.getElementById('searchKode').disabled = false;
            document.getElementById('searchNama').disabled = false;
            document.getElementById('searchbtn').disabled = false;

        resetButtonStyles();

        }

        function initializeTambah() {
            currentstat = 'tambah';
            showToast('Kamu sedang menambah data...', '#ffc107');

            document.getElementById('barangForm').reset();

            document.getElementById('btnTambah').disabled = true;
            document.getElementById('btnEdit').disabled = true;
            document.getElementById('btnHapus').disabled = true;
            document.getElementById('btnCancel').disabled = false;
            document.getElementById('btnSave').disabled = false;
            document.getElementById('btnHarga').disabled = false; 

            document.getElementById('kodemerek').disabled = false;
            document.getElementById('kodegolongan').disabled = false;
            document.getElementById('kodegrup').disabled = false;
            document.getElementById('kodebrg').disabled = false;
            document.getElementById('kodebrg').readOnly = false;
            document.getElementById('namabrg').disabled = false;
            document.getElementById('satuan1').disabled = false;
            document.getElementById('isi1').disabled = false;
            document.getElementById('hargabeli').disabled = false;
            document.getElementById('gambar').disabled = false;
            document.getElementById('upload').disabled = false;

            document.getElementById('searchKode').disabled = true;
            document.getElementById('searchNama').disabled = true;
            document.getElementById('searchKode').value = '';
            document.getElementById('searchNama').value = '';
            document.getElementById('searchbtn').disabled = true;

            resetButtonStyles();
            setActiveButtonStyle(document.getElementById('btnTambah'));
        }


        function initializeUbah() {
            if (!cekAkses('ubah')) return;
            currentstat = 'update';
            
            showToast('Kamu sedang mengubah data...', '#ffc107');

            document.getElementById('btnTambah').disabled = true;
            document.getElementById('btnEdit').disabled = true;
            document.getElementById('btnHapus').disabled = false;
            document.getElementById('btnCancel').disabled = false;
            document.getElementById('btnSave').disabled = false;

            document.getElementById('kodemerek').disabled = false;
            document.getElementById('kodegolongan').disabled = false;
            document.getElementById('kodegrup').disabled = false;
            document.getElementById('kodebrg').disabled = false;
            document.getElementById('kodebrg').readOnly = false;
            document.getElementById('namabrg').disabled = false;
            document.getElementById('satuan1').disabled = false;
            document.getElementById('isi1').disabled = false;
            document.getElementById('hargabeli').disabled = false;
            document.getElementById('gambar').disabled = false;
            document.getElementById('upload').disabled = false;

            document.getElementById('searchKode').disabled = true;
            document.getElementById('searchNama').disabled = true;
            document.getElementById('searchKode').value = '';
            document.getElementById('searchNama').value = '';
            document.getElementById('searchbtn').disabled = true;
            const isi1 = parseFloat(document.getElementById('isi1').value);
            const isi2 = parseFloat(document.getElementById('isi2').value);

            if (isi1 > 1) {
                document.getElementById('satuan2').disabled = false;
                document.getElementById('isi2').disabled = false;

                if (isi2 > 1) {
                    document.getElementById('satuan3').disabled = false;
                } else {
                    document.getElementById('satuan3').disabled = true;
                    document.getElementById('satuan3').value = '';
                }
            } else {
                document.getElementById('satuan2').disabled = true;
                document.getElementById('isi2').disabled = true;
                document.getElementById('satuan3').disabled = true;
            }


            setHargaInputsDisabled(false);


            resetButtonStyles();
            setActiveButtonStyle(document.getElementById('btnEdit'));
        }

        function prepareSave() {
            if (!currentstat) {
                showToast('Tidak ada data yang sedang diubah atau ditambah.', '#dc3545');
                return false;
            }
            document.getElementById('aksi').value = currentstat;
        }

        //Search
        [kodeMerkInput].forEach(input => {
            const tipe = input.id === 'kodemerek' ? 'kode' : 'nama';

            input.addEventListener('keypress', function(e) {
                if (e.key === 'Enter') {
                    e.preventDefault();
                    const val = this.value.trim();
                    if (val) cariMerek(tipe, val);
                }
            });

            input.addEventListener('blur', function() {
                const val = this.value.trim();
                if (val) cariMerek(tipe, val);
            });
        });

        [kodeGolInput].forEach(input => {
            const tipe = input.id === 'kodegolongan' ? 'kode' : 'nama';

            input.addEventListener('keypress', function(e) {
                if (e.key === 'Enter') {
                    e.preventDefault();
                    const val = this.value.trim();
                    if (val) cariGolongan(tipe, val);
                }
            });

            input.addEventListener('blur', function() {
                const val = this.value.trim();
                if (val) cariGolongan(tipe, val);
            });
        });

        [kodeGrupInput].forEach(input => {
            const tipe = input.id === 'kodegrup' ? 'kode' : 'nama';

            input.addEventListener('keypress', function(e) {
                if (e.key === 'Enter') {
                    e.preventDefault();
                    const val = this.value.trim();
                    if (val) cariGrup(tipe, val);
                }
            });

            input.addEventListener('blur', function() {
                const val = this.value.trim();
                if (val) cariGrup(tipe, val);
            });
        });

        function cariMerek(mode, keyword) {
            if (!keyword) return;

            fetch('filter_itemmerek.php', {
                method: 'POST',
                headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
                body: `mode=${mode}&keyword=${encodeURIComponent(keyword)}`
            })
            .then(res => res.json())
            .then(data => {
                const tbody = document.getElementById('tbodyHasilMerek');
                tbody.innerHTML = '';

                if (data.length === 0) {
                    tbody.innerHTML = '<tr><td colspan="6" style="text-align:center;">Data tidak ditemukan!</td></tr>';
                    document.getElementById('kodemerek').value = '';
                    document.getElementById('kodemerek').focus();
                } else {
                    data.forEach(item => {
                        const tr = document.createElement('tr');
                        tr.innerHTML = `
                            <td>${item.kodemerk}</td>
                            <td>${item.namamerk}</td>
                            <td><button type="button" onclick='pilihMerek(${JSON.stringify(item)})'>Pilih</button></td>
                        `;
                        tbody.appendChild(tr);
                    });
                }

                document.getElementById('popupCariMerek').style.display = 'flex';
            })
            .catch(() => {
                showToast('Terjadi kesalahan saat mencari barang', '#dc3545');
            });
        }

        function cariGolongan(mode, keyword) {
            if (!keyword) return;

            fetch('filter_itemgolongan.php', {
                method: 'POST',
                headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
                body: `mode=${mode}&keyword=${encodeURIComponent(keyword)}`
            })
            .then(res => res.json())
            .then(data => {
                const tbody = document.getElementById('tbodyHasilGolongan');
                tbody.innerHTML = '';

                if (data.length === 0) {
                    tbody.innerHTML = '<tr><td colspan="6" style="text-align:center;">Data tidak ditemukan!</td></tr>';
                    document.getElementById('kodegolongan').value = '';
                    document.getElementById('kodegolongan').focus();
                } else {
                    data.forEach(item => {
                        const tr = document.createElement('tr');
                        tr.innerHTML = `
                            <td>${item.kodegol}</td>
                            <td>${item.namagol}</td>
                            <td><button type="button" onclick='pilihGolongan(${JSON.stringify(item)})'>Pilih</button></td>
                        `;
                        tbody.appendChild(tr);
                    });
                }

                document.getElementById('popupCariGolongan').style.display = 'flex';
            })
            .catch(() => {
                showToast('Terjadi kesalahan saat mencari barang', '#dc3545');
            });
        }

        function cariGrup(mode, keyword) {
            if (!keyword) return;

            fetch('filter_itemgrup.php', {
                method: 'POST',
                headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
                body: `mode=${mode}&keyword=${encodeURIComponent(keyword)}`
            })
            .then(res => res.json())
            .then(data => {
                const tbody = document.getElementById('tbodyHasilGrup');
                tbody.innerHTML = '';

                if (data.length === 0) {
                    tbody.innerHTML = '<tr><td colspan="6" style="text-align:center;">Data tidak ditemukan!</td></tr>';
                    document.getElementById('kodegrup').value = '';
                    document.getElementById('kodegrup').focus();
                } else {
                    data.forEach(item => {
                        const tr = document.createElement('tr');
                        tr.innerHTML = `
                            <td>${item.kodegrup}</td>
                            <td>${item.namagrup}</td>
                            <td><button type="button" onclick='pilihGrup(${JSON.stringify(item)})'>Pilih</button></td>
                        `;
                        tbody.appendChild(tr);
                    });
                }

                document.getElementById('popupCariGrup').style.display = 'flex';
            })
            .catch(() => {
                showToast('Terjadi kesalahan saat mencari barang', '#dc3545');
            });
        }

        function pilihMerek(item) {
            kodeMerkInput.value = item.kodemerk;
            tutupPopupMerek();
        }

        function pilihGolongan(item) {
            kodeGolInput.value = item.kodegol;
            tutupPopupGolongan();
        }

        function pilihGrup(item) {
            kodeGrupInput.value = item.kodegrup;
            tutupPopupGrup();
        }

        function tutupPopupMerek() {
            document.getElementById('popupCariMerek').style.display = 'none';
        }

        function tutupPopupGolongan() {
            document.getElementById('popupCariGolongan').style.display = 'none';
        }

        function tutupPopupGrup() {
            document.getElementById('popupCariGrup').style.display = 'none';
        }

        // panggil saat halaman dimuat
        function handleInput(type) {
            const kodeInput = document.getElementById('searchKode');
            const namaInput = document.getElementById('searchNama');

            if (type === 'kode') {
                if (kodeInput.value.trim() !== '') {
                    namaInput.disabled = true;
                    inputSearch = kodeInput;
                } else {
                    namaInput.disabled = false;
                    inputSearch = null;
                }
            } else {
                if (namaInput.value.trim() !== '') {
                    kodeInput.disabled = true;
                    inputSearch = namaInput;
                } else {
                    kodeInput.disabled = false;
                    inputSearch = null;
                }
            }
        }

        function showFilterPopup(dataList) {
            const list = document.getElementById('popupList');
            list.innerHTML = '';

            const table = list.closest('table');
            const thead = table.querySelector('thead');
            thead.innerHTML = ''; // Kosongkan

            if (!dataList || dataList.length === 0) return;

            // Deteksi semua kolom harga yang ada dalam data
            const hargaFields = Object.keys(dataList[0])
                .filter(key => key.toLowerCase().startsWith('harga') && dataList[0][key] !== undefined);

            const fixedFields = ['kodebrg', 'kodegrup', 'namabrg', 'satuan1', 'isi1', 'satuan2', 'isi2', 'satuan3', 'hrgbeli'];

            // Header
            const headerRow = document.createElement('tr');
            [...fixedFields, ...hargaFields].forEach(field => {
                const th = document.createElement('th');
                th.textContent = field.toUpperCase();
                th.style.padding = '4px';
                th.style.border = '1px solid #ccc';
                th.style.background = '#f9f9f9';
                th.style.fontSize = '11px';
                headerRow.appendChild(th);
            });
            thead.appendChild(headerRow);

            // Data
            dataList.forEach(item => {
                const tr = document.createElement('tr');
                tr.style.cursor = 'pointer';
                tr.addEventListener('click', () => {
                    closeFilterPopup();
                    pilihBarang(item);
                });

                [...fixedFields, ...hargaFields].forEach(field => {
                    const td = document.createElement('td');
                    td.textContent = item[field] || '';
                    td.style.padding = '4px';
                    td.style.border = '1px solid #ccc';
                    td.style.fontSize = '11px';
                    tr.appendChild(td);
                });

                list.appendChild(tr);
            });

            document.getElementById('popupFilter').style.display = 'block';
        }


        function closeFilterPopup() {
            document.getElementById('popupFilter').style.display = 'none';
        }


        function triggerSearch() {
            if (!inputSearch) {
                showToast('Isi kode atau nama terlebih dahulu!', '#dc3545');
                return;
            }

            const keyword = inputSearch.value.trim().toUpperCase();
            if (!keyword) {
                showToast('Kolom pencarian tidak boleh kosong!', '#dc3545');
                return;
            }

            fetch(`filter_barang.php?keyword=${encodeURIComponent(keyword)}`)
                .then(response => response.json())
                .then(data => {
                    if (data.length === 0) {
                        showToast('Data tidak ada!', '#dc3545');
                        return;
                    }

                    if (data.length === 1) {
                        showFilterPopup(data);
                    } else {
                        showFilterPopup(data); // tampilkan pilihan
                    }
                })

        }

        previousFormData = {
            kodebrg: document.getElementById('kodebrg').value,
            kodebrg_lama: document.getElementById('kodebrg_lama').value,
            namabrg: document.getElementById('namabrg').value,
            kodemerk: document.getElementById('kodemerek').value,
            kodegol: document.getElementById('kodegolongan').value,
            kodegrup: document.getElementById('kodegrup').value,
            satuan1: document.getElementById('satuan1').value,
            isi1: document.getElementById('isi1').value,
            satuan2: document.getElementById('satuan2').value,
            isi2: document.getElementById('isi2').value,
            satuan3: document.getElementById('satuan3').value,
            hargabeli: document.getElementById('hargabeli').value,
            hargaData: JSON.parse(JSON.stringify(hargaData)) // deep copy supaya tidak berubah
        };


        function pilihBarang(data) {
            document.getElementById('kodemerek').value = data.kodemerk;
            document.getElementById('kodegolongan').value = data.kodegol;
            document.getElementById('kodegrup').value = data.kodegrup;
            document.getElementById('kodebrg').value = data.kodebrg;
            document.getElementById('kodebrg_lama').value = data.kodebrg; 
            document.getElementById('namabrg').value = data.namabrg;

            // Ambil dan simpan data harga
            hargaData = {};
            Object.keys(data).forEach(key => {
                if (key.startsWith('harga')) {
                    hargaData[key] = data[key];
                }
            });

            document.getElementById('satuan1').value = data.satuan1;
            document.getElementById('isi1').value = data.isi1;
            document.getElementById('satuan2').value = data.satuan2;
            document.getElementById('isi2').value = data.isi2;
            document.getElementById('satuan3').value = data.satuan3;
            document.getElementById('hargabeli').value = data.hrgbeli;

            // Tombol state
            document.getElementById('btnTambah').disabled = true;
            document.getElementById('btnEdit').disabled = false;
            document.getElementById('btnHapus').disabled = false;
            document.getElementById('btnCancel').disabled = false;
            document.getElementById('btnHarga').disabled = false;

            // Simpan kondisi sebelumnya
            previousFormData = {
                kodebrg: data.kodebrg,
                kodebrg_lama: data.kodebrg,
                namabrg: data.namabrg,
                kodemerk: data.kodemerk,
                kodegol: data.kodegol,
                kodegrup: finalKodeGrup,
                satuan1: data.satuan1,
                isi1: data.isi1,
                satuan2: data.satuan2,
                isi2: data.isi2,
                satuan3: data.satuan3,
                hrgbeli: data.hrgbeli,
                hargaData: JSON.parse(JSON.stringify(hargaData)) // deep copy
            };

            // Reset dan tampil
            document.getElementById('searchKode').value = '';
            document.getElementById('searchNama').value = '';
            document.getElementById('searchKode').disabled = false;
            document.getElementById('searchNama').disabled = false;
            document.getElementById('searchbtn').disabled = false;
            inputSearch = null;

            dropdown.style.display = 'none';
            generateHargaInputs(<?= $jmlharga ?>);
            setHargaInputsDisabled(true);
            closeFilterPopup();
        }

        function checkIsi1() {
            const isi1 = parseFloat(document.getElementById('isi1').value);
            const satuan2 = document.getElementById('satuan2');
            const isi2 = document.getElementById('isi2');

            if (isi1 > 1) {
                satuan2.disabled = false;
                isi2.disabled = false;
            } else {
                satuan2.disabled = true;
                satuan2.value = '';
                isi2.disabled = true;
                isi2.value = '';
                document.getElementById('satuan3').disabled = true;
                document.getElementById('satuan3').value = '';
            }
        }

        function checkIsi2() {
            const isi2 = parseFloat(document.getElementById('isi2').value);
            const satuan3 = document.getElementById('satuan3');
            if (isi2 > 1) {
                satuan3.disabled = false;
            } else {
                satuan3.disabled = true;
                satuan3.value = '';
            }
        }

        function validateForm() {
            const kodemerk = document.getElementById('kodemerk').value.trim();
            const kodegol = document.getElementById('kodegol').value.trim();
            const kodegrup = document.getElementById('kodegrup').value.trim();
            const kodebrg = document.getElementById('kodebrg').value.trim();
            const namabrg = document.getElementById('namabrg').value.trim();
            const satuan1 = document.getElementById('satuan1').value.trim();
            const satuan2 = document.getElementById('satuan2');
            const satuan3 = document.getElementById('satuan3');

            if (!kodemerk) {
                showToast('Kode Merek wajib diisi!', '#dc3545');
                return false;
            }
            if (!kodegol) {
                showToast('Kode Golongan wajib diisi!', '#dc3545');
                return false;
            }
            if (!kodegrup) {
                showToast('Kode Grup wajib diisi!', '#dc3545');
                return false;
            }
            if (!kodebrg) {
                showToast('Kode Barang wajib diisi!', '#dc3545');
                return false;
            }
            if (!namabrg) {
                showToast('Nama Barang wajib diisi!', '#dc3545');
                return false;
            }
            if (!satuan1) {
                showToast('Satuan 1 wajib diisi!', '#dc3545');
                return false;
            }

            // Validasi tambahan untuk satuan2 jika aktif
            if (!satuan2.disabled && satuan2.value.trim() === '') {
                showToast('Satuan 2 wajib diisi jika aktif!', '#dc3545');
                return false;
            }

            // Validasi tambahan untuk satuan3 jika aktif
            if (!satuan3.disabled && satuan3.value.trim() === '') {
                showToast('Satuan 3 wajib diisi jika aktif!', '#dc3545');
                return false;
            }

            return true;
        }

        function cancelEdit() {
            initializeFormButtons();

            // Reset search input
            const kodeInput = document.getElementById('searchKode');
            const namaInput = document.getElementById('searchNama');
            kodeInput.disabled = false;
            namaInput.disabled = false;
            kodeInput.value = '';
            namaInput.value = '';
            document.getElementById('kodebrg').readOnly = true;
            document.getElementById('namabrg').disabled = true;
            document.getElementById('searchbtn').disabled = false;
            document.getElementById('kodemerek').disabled = true;
            document.getElementById('kodegolongan').disabled = true;
            document.getElementById('kodegrup').disabled = true;
            document.getElementById('satuan1').disabled = true;
            document.getElementById('isi1').disabled = true;
            document.getElementById('satuan2').disabled = true;
            document.getElementById('isi2').disabled = true;
            document.getElementById('hargabeli').disabled = true;
            document.getElementById('satuan3').disabled = true;
            document.getElementById('gambar').value = '';

            hargaData = {};
            generateHargaInputs(<?= $jmlharga ?>);
            if (currentstat === 'tambah' ) {
                initializeFormButtons();
                document.getElementById('barangForm').reset();
                currentstat = null;
            } else if (currentstat === 'update') {
                initializeFormButtonsCancel();
                currentstat = null;
            } else if (currentstat === null) {
                initializeFormButtons();
                document.getElementById('barangForm').reset();
            }
            resetButtonStyles();
        }


        function handleFileUpload(input) {
            const file = input.files[0];
            if (file) {
                if (!file.name.toLowerCase().endsWith('.png')) {
                    showToast('Hanya file JPG yang diperbolehkan!','#dc3545');
                    input.value = '';
                    return;
                }

                document.getElementById('gambar').value = file.name;
            }
        }

        document.getElementById('kodebrg').addEventListener('blur', function () {
            const kode = this.value.trim();
            if (kode) {
                fetch('cek_kode.php?kodebrg=' + encodeURIComponent(kode))
                    .then(response => response.json())
                    .then(data => {
                        if (data.exists) {
                            showToast('Kode Barang sudah terdaftar!', '#dc3545');
                            this.value = '';
                            this.focus();
                        }
                    });
            }
        });

        document.getElementById('barangForm').addEventListener('submit', function (e) {
            e.preventDefault();

            const formData = new FormData(this);
            formData.set('aksi', currentstat); // 'tambah', 'update', 'hapus'
            document.querySelectorAll('[name^="harga"]').forEach(input => {
                formData.append(input.name, input.value);
            });

            fetch('prosesbarang.php', {
                method: 'POST',
                body: formData
            })
            .then(res => res.json())
            .then(response => {
                if (response.status === 'success') {
                showToast(`Data berhasil ${response.aksi === 'tambah' ? 'ditambahkan' : response.aksi === 'update' ? 'diupdate' : 'dihapus'}`);

                if (currentstat === 'tambah' || currentstat === 'update') {
                previousFormData = {
                    kodebrg: document.getElementById('kodebrg').value.trim(),
                    kodebrg_lama: document.getElementById('kodebrg').value.trim(),
                    namabrg: document.getElementById('namabrg').value.trim(),
                    kodegrup: document.getElementById('kodegrup').value.trim(),
                    kodemerek: document.getElementById('kodemerek').value.trim(),
                    kodegolongan: document.getElementById('kodegolongan').value.trim(),
                    satuan1: document.getElementById('satuan1').value.trim(),
                    isi1: document.getElementById('isi1').value.trim(),
                    satuan2: document.getElementById('satuan2').value.trim(),
                    isi2: document.getElementById('isi2').value.trim(),
                    satuan3: document.getElementById('satuan3').value.trim(),
                    hargabeli: document.getElementById('hargabeli').value.trim(),
                    hargaData: JSON.parse(JSON.stringify(hargaData)) // deep copy agar tidak berubah saat diedit
                };

                if (currentstat === 'tambah') {
                    initializeFormButtons();
                } else {
                    initializeFormButtonsCancel();
                }
                }
                

                // Kamu bisa isi ulang form dengan data sebelumnya kalau mau
                } else if (response.status === 'duplikat') {
                showToast('Kode grup sudah ada!', '#dc3545');
                } else {
                showToast('Gagal menyimpan data!', '#dc3545');
                }
            })
            .catch(err => {
                console.error(err);
                showToast('Gagal koneksi ke server!', '#dc3545');
            });
        });
        

        function openHargaPopup() {
            const jumlah = parseInt("<?= $jmlharga ?>");
            generateHargaInputs(jumlah);    
            const isEditable = currentstat === 'tambah' || currentstat === 'update'; // hanya mode edit yang bisa ubah harga
            setHargaInputsDisabled(!isEditable);
            document.getElementById('popupHarga').style.display = 'block';
        }

        

        function closeHargaPopup() {
            document.getElementById('popupHarga').style.display = 'none';
        }

        function generateHargaInputs(jumlah) {
            const container = document.getElementById('hargaInputsContainer');
            container.innerHTML = '';

            container.style.display = 'grid';
            container.style.gridTemplateColumns = 'repeat(3, auto)';
            container.style.gap = '4px';

            for (let i = 1; i <= jumlah; i++) {
                ['' + i, '' + i + i, '' + i + i + i].forEach(suffix => {
                    const key = 'harga' + suffix;
                    if (!(key in hargaData)) hargaData[key] = '';

                    const wrapper = document.createElement('div');
                    wrapper.style.display = 'flex';
                    wrapper.style.alignItems = 'center';
                    wrapper.style.gap = '10px';

                    const label = document.createElement('label');
                    label.textContent = key.charAt(0).toUpperCase() + key.slice(1);
                    label.style.fontSize = '12px';
                    label.style.width = '60px';

                    const input = document.createElement('input');
                    input.type = 'number';
                    input.step = '0.01';
                    input.id = key;
                    input.name = key;
                    input.value = hargaData[key] !== undefined ? hargaData[key] : '';
                    input.style.padding = '4px 4px';
                    input.style.fontSize = '12px';
                    input.style.border = '1px solid #ccc';
                    input.style.borderRadius = '4px';
                    input.style.width = '120px';

                    wrapper.appendChild(label);
                    wrapper.appendChild(input);
                    container.appendChild(wrapper);
                });
            }
        }

        function setHargaInputsDisabled(disabled) {
            const jumlah = parseInt("<?= $jmlharga ?>");

            const isi1 = parseInt(document.getElementById('isi1').value) || 0;
            const isi2 = parseInt(document.getElementById('isi2').value) || 0;

            for (let base = 1; base <= jumlah; base++) {
                for (let level = 1; level <= 3; level++) {
                    const key = 'harga' + String(base).repeat(level);
                    const input = document.getElementById(key);
                    if (!input) continue;

                    // Tentukan status disable berdasarkan isi1 dan isi2
                    let isDisabled = false;
                    if (level === 1) {
                        isDisabled = disabled;
                    } else if (level === 2) {
                        isDisabled = disabled || isi1 < 2;
                    } else if (level === 3) {
                        isDisabled = disabled || isi1 < 2 || isi2 < 2;
                    }

                    // Set status disable
                    input.disabled = isDisabled;

                    // Terapkan styling berdasarkan status
                    if (isDisabled) {
                        input.style.backgroundColor = '#999';
                    } else {
                        input.style.backgroundColor = '';
                    }
                }
            }
        }

        function simpanHarga() {
            const jumlah = parseInt("<?= $jmlharga ?>");
            hargaData = {};
            let adaIsi = false;

            for (let base = 1; base <= jumlah; base++) {
                for (let level = 1; level <= 3; level++) {
                    const key = 'harga' + String(base).repeat(level);
                    const input = document.getElementById(key);
                    if (input) {
                        const val = input.value.trim();
                        if (val !== '') adaIsi = true;
                        hargaData[key] = val || 0; // Nilai kosong = 0
                    }
                }
            }

            if (!adaIsi) {
                showToast('Minimal satu harga harus diisi!', '#dc3545');
                return;
            }

            closeHargaPopup();
        }

        function tampilkanKonfirmasiHapus() {
            if (!cekAkses('hapus')) return;
            document.getElementById('popupConfirmHapus').style.display = 'block';
        }

        function konfirmasiHapus(setuju) {
                const popup = document.getElementById('popupConfirmHapus');
                popup.style.display = 'none';

                if (setuju) {
                    const formData = new FormData(document.getElementById('barangForm'));
                    formData.set('aksi', 'hapus');

                    fetch('prosesbarang.php', {
                        method: 'POST',
                        body: formData
                    })
                    .then(res => res.json())
                    .then(response => {
                        if (response.status === 'success') {
                            showToast('Data berhasil dihapus');
                            initializeFormButtons(); // reset tampilan
                            document.getElementById('barangForm').reset();
                        } else {
                            showToast('Gagal menghapus data!', '#dc3545');
                        }
                    })
                    .catch(err => {
                        console.error(err);
                        showToast('Gagal koneksi ke server!', '#dc3545');
            });
            } else {
                showToast('Penghapusan dibatalkan.', '#6c757d');
            }
        }

        function resetButtonStyles() {
            const buttons = ['btnTambah', 'btnEdit'];
            buttons.forEach(id => {
                const btn = document.getElementById(id);
                btn.style.backgroundColor = '';
                btn.style.color = '';
                btn.style.border = '';
            });
        }

    </script>
    <script src="notif.js"></script>
</body>
</html>