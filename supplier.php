<?php
session_start();

include 'koneksi.php';
$query = $conn->query("SELECT jmlharga FROM zconfig LIMIT 1");
$row = $query ? $query->fetch_assoc() : null;
$jmlharga = ($row && is_numeric($row['jmlharga'])) ? (int)$row['jmlharga'] : 0;

?>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Data Supplier</title>
    <link rel="stylesheet" href="navbar.css">
    <link rel="stylesheet" href="form.css">
</head>
<body>
    <button class="hamburger" onclick="toggleSidebar()">☰</button>
    <div class="overlay" id="sidebarOverlay" onclick="toggleSidebar()"></div>
    <?php include 'navbar.php'; ?>

    <main>
        <h2>Data Supplier</h2>

            <div class="search-bar">
                <input type="text" id="searchKode" class="search-kode" placeholder="Kode Supplier" oninput="handleInput('kode')" style="text-transform: uppercase;">
                <input type="text" id="searchNama" class="search-nama" placeholder="Nama Supplier" oninput="handleInput('nama')" style="text-transform: uppercase;">
                <button type="button" id="searchbtn" onclick="triggerSearch()">🔍 Cari</button>
            </div>

            <form id="supplierForm" action="prosessupplier.php" method="POST" enctype="multipart/form-data" onsubmit="return validateForm()">
                <input type="hidden" name="aksi" id="aksi" value="">
                <input type="hidden" name="kodesup_lama" id="kodesup_lama" value=""> 

                <div class="form-atas">
                    <label for="kodesup">Kode Supplier</label>
                    <input type="text" name="kodesup" id="kodesup" class="long-input" required style="text-transform: uppercase;">

                    <label for="namasup">Nama Supplier</label>
                    <input type="text" name="namasup" id="namasup" class="verylong-input" required style="text-transform: uppercase;">

                    <label for="alamat">Alamat</label>
                    <textarea name="alamat" id="alamat" class="verylong-textarea" required style="text-transform: uppercase;"></textarea>

                    <label for="kota">Kota</label>
                    <input type="text" name="kota" id="kota" class="lesslong-input" required style="text-transform: uppercase;">

                    <label for="ktp">KTP</label>
                    <input type="text" name="ktp" id="ktp" class="lesslong-input" required style="text-transform: uppercase;">

                    <label for="npwp">NPWP</label>
                    <input type="text" name="npwp" id="npwp" class="lesslong-input" required style="text-transform: uppercase;">

                    <label for="gambar">Gambar</label>
                    <div style="display: flex; gap: 5px; align-items: center;">
                        <input type="text" name="gambar" id="gambar" class="lesslong-input" readonly>
                        <input type="file" id="uploadInput" accept=".png" style="display: none;" onchange="handleFileUpload(this)">
                        <button type="button" id="upload" style="background: #218838;" onclick="document.getElementById('uploadInput').click();">Upload</button>
                    </div>

                </div>
                <div class="form-bawah">
                </div>

            <button id="btnSave" type="submit" onclick="prepareSave()">Simpan</button>
            <button id="btnTambah" type="submit" onclick="initializeTambah() ">Tambah</button>
            <button id="btnEdit" type="submit" onclick="initializeUbah()">Ubah</button>
            <button id="btnHapus" type="button" onclick="tampilkanKonfirmasiHapus()">Hapus</button>
            <button id="btnCancel" type="button" onclick="cancelEdit()">Batal</button>


        </form>
    </main>
        <!-- Popup Pilih Data -->
    <div id="popupFilter" style="display:none; position:fixed; top:0; left:0; width:100%; height:100%; background:rgba(0,0,0,0.4); z-index:1000;">
        <div id="popupContent" style="
            position:absolute; 
            top:50%; left:50%; transform:translate(-50%, -50%); 
            width:90%; 
            max-width:835px; 
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
        function initializeFormButtons() {
            document.getElementById('btnTambah').disabled = false;
            document.getElementById('btnEdit').disabled = true;
            document.getElementById('btnHapus').disabled = true;
            document.getElementById('btnCancel').disabled = true;
            document.getElementById('btnSave').disabled = true;      

            document.getElementById('kodesup').disabled = true;
            document.getElementById('namasup').disabled = true;
            document.getElementById('alamat').disabled = true;
            document.getElementById('kota').disabled = true;
            document.getElementById('ktp').disabled = true;
            document.getElementById('npwp').disabled = true;
            document.getElementById('gambar').disabled = true;
            document.getElementById('upload').disabled = true;
        }
        initializeFormButtons();

        let currentstat = null;

        function initializeTambah() {
            currentstat = 'tambah';
            showToast('Kamu sedang menambah data...', '#ffc107');

            document.getElementById('btnTambah').disabled = true;
            document.getElementById('btnEdit').disabled = true;
            document.getElementById('btnHapus').disabled = true;
            document.getElementById('btnCancel').disabled = false;
            document.getElementById('btnSave').disabled = false;

            document.getElementById('kodesup').disabled = false;
            document.getElementById('namasup').disabled = false;
            document.getElementById('alamat').disabled = false;
            document.getElementById('kota').disabled = false;
            document.getElementById('ktp').disabled = false;
            document.getElementById('npwp').disabled = false;
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
            currentstat = 'update';
            showToast('Kamu sedang mengubah data...', '#ffc107');

            document.getElementById('btnTambah').disabled = true;
            document.getElementById('btnEdit').disabled = true;
            document.getElementById('btnHapus').disabled = false;
            document.getElementById('btnCancel').disabled = false;
            document.getElementById('btnSave').disabled = false;

            document.getElementById('kodesup').disabled = false;
            document.getElementById('namasup').disabled = false;
            document.getElementById('alamat').disabled = false;
            document.getElementById('kota').disabled = false;
            document.getElementById('ktp').disabled = false;
            document.getElementById('npwp').disabled = false;
            document.getElementById('gambar').disabled = false;
            document.getElementById('upload').disabled = false;

            document.getElementById('searchKode').disabled = true;
            document.getElementById('searchNama').disabled = true;
            document.getElementById('searchKode').value = '';
            document.getElementById('searchNama').value = '';
            document.getElementById('searchbtn').disabled = true;

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

        // panggil saat halaman dimuat
        let inputSearch = null;
        let searchBtn = document.getElementById('searchbtn')

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

            const fixedFields = ['kodesup', 'namasup', 'alamat', 'kota', 'ktp', 'npwp'];

            // Header
            const headerRow = document.createElement('tr');
            [...fixedFields].forEach(field => {
                const th = document.createElement('th');
                th.textContent = field.toUpperCase();
                th.style.padding = '4px';
                th.style.border = '1px solid #ccc';
                th.style.background = '#f9f9f9';
                th.style.fontSize = '12px';
                headerRow.appendChild(th);
            });
            thead.appendChild(headerRow);

            // Data
            dataList.forEach(item => {
                const tr = document.createElement('tr');
                tr.style.cursor = 'pointer';
                tr.addEventListener('click', () => {
                    closeFilterPopup();
                    pilihSupplier(item);
                });

                [...fixedFields].forEach(field => {
                    const td = document.createElement('td');
                    td.textContent = item[field] || '';
                    td.style.padding = '4px';
                    td.style.border = '1px solid #ccc';
                    td.style.fontSize = '14px';
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
               console.log("Search button clicked");
            if (!inputSearch) {
                showToast('Isi kode atau nama terlebih dahulu!', '#dc3545');
                return;
            }

            const keyword = inputSearch.value.trim().toUpperCase();
            if (!keyword) {
                showToast('Kolom pencarian tidak boleh kosong!', '#dc3545');
                return;
            }

            fetch(`filter_suplier.php?keyword=${encodeURIComponent(keyword)}`)
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

        function validateForm() {
            const kodesup = document.getElementById('kodesup').value.trim();
            const namasup = document.getElementById('namasup').value.trim();
            const alamat = document.getElementById('alamat').value.trim();
            const kota = document.getElementById('kota').value.trim();
            const ktp = document.getElementById('ktp').value.trim();
            const npwp = document.getElementById('npwp').value.trim();

            if (!kodesup) {
                showToast('Kode Supplier wajib diisi!.', '#dc3545');
                return false;
            }
            if (!namasup) {
                showToast('Nama Supplier wajib diisi!.', '#dc3545');
                return false;
            }
            if (!alamat) {
                showToast('Alamat wajib diisi!.', '#dc3545');
                return false;
            }
            if (!kota) {
                showToast('Kota wajib diisi!.', '#dc3545');
                return false;
            }
            if (!ktp) {
                showToast('Ktp wajib diisi!.', '#dc3545');
                return false;
            }
            if (!npwp) {
                showToast('Npwp wajib diisi!.', '#dc3545');
                return false;
            }            
            return true;
        }

        function pilihSupplier(data) {
            document.getElementById('kodesup').value = data.kodesup;
            document.getElementById('kodesup_lama').value = data.kodesup; 
            document.getElementById('namasup').value = data.namasup;

            document.getElementById('alamat').value = data.alamat;
            document.getElementById('kota').value = data.kota;
            document.getElementById('ktp').value = data.ktp;
            document.getElementById('npwp').value = data.npwp;           


            // Logika enable/disable berdasarkan isi1 dan isi2
            document.getElementById('btnTambah').disabled = true;
            document.getElementById('btnEdit').disabled = false;
            document.getElementById('btnHapus').disabled = true;
            document.getElementById('btnCancel').disabled = false; 

            document.getElementById('searchKode').value = '';
            document.getElementById('searchNama').value = '';
            document.getElementById('searchKode').disabled = false;
            document.getElementById('searchNama').disabled = false;
            document.getElementById('searchbtn').disabled = false;
            inputSearch = null;
            dropdown.style.display = 'none';
            closeFilterPopup();


        }

        function cancelEdit() {
            document.getElementById('supplierForm').reset();
            initializeFormButtons();

            // Reset search input
            const kodeInput = document.getElementById('searchKode');
            const namaInput = document.getElementById('searchNama');
            kodeInput.disabled = false;
            namaInput.disabled = false;
            kodeInput.value = '';
            namaInput.value = '';
            document.getElementById('kodesup').readOnly = true;
            document.getElementById('searchbtn').disabled = false;
            document.getElementById('alamat').disabled = true;
            document.getElementById('kota').disabled = true;
            document.getElementById('ktp').disabled = true;
            document.getElementById('npwp').disabled = true;
            document.getElementById('gambar').value = '';

            currentstat = null;
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

        document.getElementById('kodesup').addEventListener('blur', function () {
            const kode = this.value.trim();
            if (kode) {
                fetch('cek_kode.php?kodesup=' + encodeURIComponent(kode))
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


        const popup = document.getElementById('popupNotif');
        if (popup) {
            popup.addEventListener('click', () => popup.style.display = 'none');
        }

        function tampilkanKonfirmasiHapus() {
            document.getElementById('popupConfirmHapus').style.display = 'block';
        }

        function konfirmasiHapus(setuju) {
            const popup = document.getElementById('popupConfirmHapus');
            popup.style.display = 'none';

            if (setuju) {
                document.getElementById('aksi').value = 'hapus';
                document.getElementById('supplierForm').submit();
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
