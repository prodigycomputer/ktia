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
    <title>Data Kustomer</title>
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
        <h2>Data Kustomer</h2>

            <div class="search-bar">
                <input type="text" id="searchKode" class="search-kode" placeholder="Kode Kustomer" oninput="handleInput('kode')" style="text-transform: uppercase;">
                <input type="text" id="searchNama" class="search-nama" placeholder="Nama Kustomer" oninput="handleInput('nama')" style="text-transform: uppercase;">
                <button type="button" id="searchbtn" onclick="triggerSearch()">🔍 Cari</button>
            </div>

            <form id="kustomerForm" action="proseskustomer.php" method="POST" enctype="multipart/form-data" onsubmit="return validateForm()">
                <input type="hidden" name="aksi" id="aksi" value="">
                <input type="hidden" name="kodekust_lama" id="kodekust_lama" value=""> 

                <div class="form-atas">
                    <label for="kodekust">Kode Kustomer</label>
                    <input type="text" name="kodekust" id="kodekust" class="long-input" required style="text-transform: uppercase;">

                    <label for="namakust">Nama Kustomer</label>
                    <input type="text" name="namakust" id="namakust" class="verylong-input" required style="text-transform: uppercase;">

                    <label for="alamat">Alamat</label>
                    <textarea name="alamat" id="alamat" class="verylong-textarea" required style="text-transform: uppercase;"></textarea>

                    <label for="kota">Kota</label>
                    <input type="text" name="kota" id="kota" class="lesslong-input" required style="text-transform: uppercase;">

                    <label for="kodehrg">Kode Harga</label>
                    <input type="text" name="kodehrg" id="kodehrg" class="medium-input" required style="text-transform: uppercase;">

                    <label for="ktp">KTP</label>
                    <input type="text" name="ktp" id="ktp" class="lesslong-input" required style="text-transform: uppercase;">

                    <label for="npwp">NPWP</label>
                    <input type="text" name="npwp" id="npwp" class="lesslong-input" required style="text-transform: uppercase;">

                    <label for="kodetipe">Kode Tipe</label>
                    <input type="text" name="kodetipe" id="kodetipe" class="long-input" required style="text-transform: uppercase;">

                    <label for="kodearea">Kode Area</label>
                    <input type="text" name="kodearea" id="kodearea" class="long-input" required style="text-transform: uppercase;">

                    <label for="gambar">Gambar</label>
                    <div style="display: flex; gap: 5px; align-items: center;">
                        <input type="text" name="gambar" id="gambar" class="lesslong-input" readonly>
                        <input type="file" id="uploadInput" accept=".png" style="display: none;" onchange="handleFileUpload(this)">
                        <button type="button" id="upload" style="background: #218838;" onclick="document.getElementById('uploadInput').click();">Upload</button>
                    </div>

                </div>
                <div class="form-bawah">
                </div>

                <button id="btnSave" type="submit">Simpan</button>
                <button id="btnTambah" type="button" onclick="initializeTambah()">Tambah</button>
                <button id="btnEdit" type="button" onclick="initializeUbah()">Ubah</button>
                <button id="btnHapus" type="button" onclick="tampilkanKonfirmasiHapus()">Hapus</button>
                <button id="btnCancel" type="button" onclick="cancelEdit()">Batal</button>
            </form>
            <div id="popupCariArea" class="popup-pb-cari" style="display: none;">
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
                        <tbody id="tbodyHasilArea">
                            <!-- hasil dari filter_barang.php -->
                        </tbody>
                    </table>
                    <div style="text-align: right; margin-top: 10px;">
                        <button type="button" onclick="tutupPopupArea()">Tutup</button>
                    </div>
                </div>
            </div>
            <div id="popupCariTipe" class="popup-pb-cari" style="display: none;">
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
                        <tbody id="tbodyHasilTipe">
                            <!-- hasil dari filter_barang.php -->
                        </tbody>
                    </table>
                    <div style="text-align: right; margin-top: 10px;">
                        <button type="button" onclick="tutupPopupTipe()">Tutup</button>
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
        let previousFormData = {};
        const maxHarga = <?= $jmlharga ?>;

        const kodeAreaInput = document.getElementById('kodearea');
        const kodeTipeInput = document.getElementById('kodetipe');

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
            fields: [
                "kodearea","kodetipe",
                {id:"kodekust", disabled:true, readonly:false}, // khusus
                "namakust","alamat","kota","kodehrg",
                "ktp","npwp","gambar","upload"
            ]
        });


        function initializeFormButtonsCancel() {
            currentstat = null;

            document.getElementById('kodearea').value = previousFormData.kodearea;
            document.getElementById('kodetipe').value = previousFormData.kodetipe;
            document.getElementById('kodekust').value = previousFormData.kodekust;
            document.getElementById('namakust').value = previousFormData.namakust;
            document.getElementById('alamat').value = previousFormData.alamat;
            document.getElementById('kota').value = previousFormData.kota;
            document.getElementById('kodehrg').value = previousFormData.kodehrg;
            document.getElementById('ktp').value = previousFormData.ktp;
            document.getElementById('npwp').value = previousFormData.npwp;
            document.getElementById('btnTambah').disabled = true;
            document.getElementById('btnEdit').disabled = false;
            document.getElementById('btnHapus').disabled = false;
            document.getElementById('btnCancel').disabled = false; 
            document.getElementById('btnSave').disabled = true;

            document.getElementById('kodearea').disabled = true;
            document.getElementById('kodetipe').disabled = true;
            document.getElementById('kodekust').disabled = true;
            document.getElementById('namakust').disabled = true;
            document.getElementById('alamat').disabled = true;
            document.getElementById('kota').disabled = true;
            document.getElementById('kodehrg').disabled = true;
            document.getElementById('ktp').disabled = true;
            document.getElementById('npwp').disabled = true;
            document.getElementById('gambar').disabled = true;
            document.getElementById('upload').disabled = true;

            document.getElementById('searchKode').value = '';
            document.getElementById('searchNama').value = '';
            document.getElementById('searchKode').disabled = false;
            document.getElementById('searchNama').disabled = false;
            document.getElementById('searchbtn').disabled = false;

        resetButtonStyles();

        }

        [kodeAreaInput].forEach(input => {
            const tipe = input.id === 'kodearea' ? 'kode' : 'nama';

            input.addEventListener('keypress', function(e) {
                if (e.key === 'Enter') {
                    e.preventDefault();
                    const val = this.value.trim();
                    if (val) cariArea(tipe, val);
                }
            });

            input.addEventListener('blur', function() {
                const val = this.value.trim();
                if (val) cariArea(tipe, val);
            });
        });

        [kodeTipeInput].forEach(input => {
            const tipe = input.id === 'kodetipe' ? 'kode' : 'nama';

            input.addEventListener('keypress', function(e) {
                if (e.key === 'Enter') {
                    e.preventDefault();
                    const val = this.value.trim();
                    if (val) cariTipe(tipe, val);
                }
            });

            input.addEventListener('blur', function() {
                const val = this.value.trim();
                if (val) cariTipe(tipe, val);
            });
        });

        function cariArea(mode, keyword) {
            if (!keyword) return;

            fetch('filter_itemarea.php', {
                method: 'POST',
                headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
                body: `mode=${mode}&keyword=${encodeURIComponent(keyword)}`
            })
            .then(res => res.json())
            .then(data => {
                const tbody = document.getElementById('tbodyHasilArea');
                tbody.innerHTML = '';

                if (data.length === 0) {
                    tbody.innerHTML = '<tr><td colspan="6" style="text-align:center;">Data tidak ditemukan!</td></tr>';
                    document.getElementById('kodearea').value = '';
                    document.getElementById('kodearea').focus();
                } else {
                    data.forEach(item => {
                        const tr = document.createElement('tr');
                        tr.innerHTML = `
                            <td>${item.kodear}</td>
                            <td>${item.namaar}</td>
                            <td><button type="button" onclick='pilihArea(${JSON.stringify(item)})'>Pilih</button></td>
                        `;
                        tbody.appendChild(tr);
                    });
                }

                document.getElementById('popupCariArea').style.display = 'flex';
            })
            .catch(() => {
                showToast('Terjadi kesalahan saat mencari barang', '#dc3545');
            });
        }

        function cariTipe(mode, keyword) {
            if (!keyword) return;

            fetch('filter_itemtipe.php', {
                method: 'POST',
                headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
                body: `mode=${mode}&keyword=${encodeURIComponent(keyword)}`
            })
            .then(res => res.json())
            .then(data => {
                const tbody = document.getElementById('tbodyHasilTipe');
                tbody.innerHTML = '';

                if (data.length === 0) {
                    tbody.innerHTML = '<tr><td colspan="6" style="text-align:center;">Data tidak ditemukan!</td></tr>';
                    document.getElementById('kodetipe').value = '';
                    document.getElementById('kodetipe').focus();
                } else {
                    data.forEach(item => {
                        const tr = document.createElement('tr');
                        tr.innerHTML = `
                            <td>${item.kodetipe}</td>
                            <td>${item.namatipe}</td>
                            <td><button type="button" onclick='pilihTipe(${JSON.stringify(item)})'>Pilih</button></td>
                        `;
                        tbody.appendChild(tr);
                    });
                }

                document.getElementById('popupCariTipe').style.display = 'flex';
            })
            .catch(() => {
                showToast('Terjadi kesalahan saat mencari barang', '#dc3545');
            });
        }

        function pilihArea(item) {
            kodeAreaInput.value = item.kodear;
            tutupPopupArea();
        }

        function pilihTipe(item) {
            kodeTipeInput.value = item.kodetipe;
            tutupPopupTipe();
        }

        function tutupPopupArea() {
            document.getElementById('popupCariArea').style.display = 'none';
        }

        function tutupPopupTipe() {
            document.getElementById('popupCariTipe').style.display = 'none';
        }

        document.getElementById('kodehrg').addEventListener('blur', function() {
            let val = parseFloat(this.value) || 0;
            if (val > maxHarga) {
                showToast(`Harga tidak boleh lebih dari ${maxHarga}`, '#dc3545');
                this.value = maxHarga; // Opsional: set otomatis ke batas max
                this.focus();
                this.select();
            }
        });

        

        function initializeTambah() {
            currentstat = 'tambah';
            showToast('Kamu sedang menambah data...', '#ffc107');

            document.getElementById('kustomerForm').reset();

            document.getElementById('btnTambah').disabled = true;
            document.getElementById('btnEdit').disabled = true;
            document.getElementById('btnHapus').disabled = true;
            document.getElementById('btnCancel').disabled = false;
            document.getElementById('btnSave').disabled = false;

            document.getElementById('kodearea').disabled = false;
            document.getElementById('kodetipe').disabled = false;
            document.getElementById('kodekust').disabled = false;
            document.getElementById('kodekust').readOnly = false;
            document.getElementById('namakust').disabled = false;
            document.getElementById('alamat').disabled = false;
            document.getElementById('kota').disabled = false;
            document.getElementById('kodehrg').disabled = false;
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
            if (!cekAkses('ubah')) return;
            currentstat = 'update';
            showToast('Kamu sedang mengubah data...', '#ffc107');

            document.getElementById('btnTambah').disabled = true;
            document.getElementById('btnEdit').disabled = true;
            document.getElementById('btnHapus').disabled = false;
            document.getElementById('btnCancel').disabled = false;
            document.getElementById('btnSave').disabled = false;

            document.getElementById('kodearea').disabled = false;
            document.getElementById('kodetipe').disabled = false;
            document.getElementById('kodekust').disabled = false;
            document.getElementById('kodekust').readOnly = false;
            document.getElementById('namakust').disabled = false;
            document.getElementById('alamat').disabled = false;
            document.getElementById('kota').disabled = false;
            document.getElementById('kodehrg').disabled = false;
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

            const fixedFields = ['kodekust', 'namakust', 'alamat', 'kota', 'kodehrg', 'ktp', 'npwp'];

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
                    pilihKustomer(item);
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
            if (!inputSearch) {
                showToast('Isi kode atau nama terlebih dahulu!', '#dc3545');
                return;
            }

            const keyword = inputSearch.value.trim().toUpperCase();
            if (!keyword) {
                showToast('Kolom pencarian tidak boleh kosong!', '#dc3545');
                return;
            }

            fetch(`filter_kustomer.php?keyword=${encodeURIComponent(keyword)}`)
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
            kodearea: document.getElementById('kodearea').value,
            kodetipe: document.getElementById('kodetipe').value,
            kodekust: document.getElementById('kodekust').value,
            namakust: document.getElementById('namakust').value,
            kodekust_lama: document.getElementById('kodekust_lama').value,
            alamat: document.getElementById('alamat').value,
            kota: document.getElementById('kota').value,
            kodehrg: document.getElementById('kodehrg').value,
            ktp: document.getElementById('ktp').value,
            npwp: document.getElementById('npwp').value,
        };

        function validateForm() {
            const kodearea = document.getElementById('kodearea').value.trim();
            const kodetipe = document.getElementById('kodetipe').value.trim();
            const kodekust = document.getElementById('kodekust').value.trim();
            const namakust = document.getElementById('namakust').value.trim();
            const alamat = document.getElementById('alamat').value.trim();
            const kota = document.getElementById('kota').value.trim();
            const kodehrg = document.getElementById('kodehrg').value.trim();
            const ktp = document.getElementById('ktp').value.trim();
            const npwp = document.getElementById('npwp').value.trim();
            const maxHarga = <?= $jmlharga ?>;
            const kodehrgNum = parseInt(kodehrg);

            if (isNaN(kodehrgNum) || kodehrgNum < 1 || kodehrgNum > maxHarga) {
                showToast(`Kode Harga harus antara 1 sampai ${maxHarga}`, '#dc3545');
                return false;
            }
            if (!kodetipe) {
                showToast('Kode Tipe wajib diisi!.', '#dc3545');
                return false;
            }
            if (!kodearea) {
                showToast('Kode Area wajib diisi!.', '#dc3545');
                return false;
            }
            if (!kodekust) {
                showToast('Kode Kustomer wajib diisi!.', '#dc3545');
                return false;
            }
            if (!namakust) {
                showToast('Nama Kustomer wajib diisi!.', '#dc3545');
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
            if (!kodehrg) {
                showToast('Kode Harga wajib diisi!.', '#dc3545');
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

        function pilihKustomer(data) {
            document.getElementById('kodearea').value = data.kodear;
            document.getElementById('kodetipe').value = data.kodetipe;
            document.getElementById('kodekust').value = data.kodekust;
            document.getElementById('kodekust_lama').value = data.kodekust; 
            document.getElementById('namakust').value = data.namakust;

            document.getElementById('alamat').value = data.alamat;
            document.getElementById('kota').value = data.kota;
            document.getElementById('kodehrg').value = data.kodehrg;
            document.getElementById('ktp').value = data.ktp;
            document.getElementById('npwp').value = data.npwp;           


            // Logika enable/disable berdasarkan isi1 dan isi2
            document.getElementById('btnTambah').disabled = true;
            document.getElementById('btnEdit').disabled = false;
            document.getElementById('btnHapus').disabled = false;
            document.getElementById('btnCancel').disabled = false; 

            previousFormData = {
                kodearea: data.kodear,
                kodetipe: data.kodetipe,
                kodekust: data.kodekust,
                namakust: data.namakust,
                kodekust_lama: data.kodekust,
                alamat: data.alamat,
                kota: data.kota,
                kodehrg: data.kodehrg,
                ktp: data.ktp,
                npwp: data.npwp
            };

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
            // Reset search input
            const kodeInput = document.getElementById('searchKode');
            const namaInput = document.getElementById('searchNama');
            kodeInput.disabled = false;
            namaInput.disabled = false;
            kodeInput.value = '';
            namaInput.value = '';
            document.getElementById('kodekust').readOnly = true;
            document.getElementById('namakust').disabled = true;
            document.getElementById('searchbtn').disabled = false;
            document.getElementById('kodearea').disabled = true;
            document.getElementById('kodetipe').disabled = true;
            document.getElementById('alamat').disabled = true;
            document.getElementById('kota').disabled = true;
            document.getElementById('kodehrg').disabled = true;
            document.getElementById('ktp').disabled = true;
            document.getElementById('npwp').disabled = true;
            document.getElementById('gambar').value = '';

            if (currentstat === 'tambah' ) {
                initializeFormButtons();
                document.getElementById('kustomerForm').reset();
                currentstat = null;
            } else if (currentstat === 'update') {
                initializeFormButtonsCancel();
                currentstat = null;
            } else if (currentstat === null) {
                initializeFormButtons();
                document.getElementById('kustomerForm').reset();
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

        document.getElementById('kodekust').addEventListener('blur', function () {
            const kode = this.value.trim();
            if (kode) {
                fetch('cek_kode.php?kodekust=' + encodeURIComponent(kode))
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

        document.getElementById('kustomerForm').addEventListener('submit', function (e) {
            e.preventDefault();

            const formData = new FormData(this);
            formData.set('aksi', currentstat); // 'tambah', 'update', 'hapus'

            fetch('proseskustomer.php', {
                method: 'POST',
                body: formData
            })
            .then(res => res.json())
            .then(response => {
                if (response.status === 'success') {
                showToast(`Data berhasil ${response.aksi === 'tambah' ? 'ditambahkan' : response.aksi === 'update' ? 'diupdate' : 'dihapus'}`);

                if (currentstat === 'tambah' || currentstat === 'update') {
                previousFormData = {
                    kodekust: document.getElementById('kodekust').value.trim(),
                    kodearea: document.getElementById('kodearea').value.trim(),
                    kodetipe: document.getElementById('kodetipe').value.trim(),
                    namakust: document.getElementById('namakust').value.trim(),
                    kodekust_lama: document.getElementById('kodekust').value.trim(),
                    alamat: document.getElementById('alamat').value.trim(),
                    kota: document.getElementById('kota').value.trim(),
                    kodehrg: document.getElementById('kodehrg').value.trim(),
                    ktp: document.getElementById('ktp').value.trim(),
                    npwp: document.getElementById('npwp').value.trim()
                };

                if (currentstat === 'tambah') {
                    initializeFormButtons();
                } else {
                    initializeFormButtonsCancel();
                }
                }
                

                // Kamu bisa isi ulang form dengan data sebelumnya kalau mau
                } else if (response.status === 'duplikat') {
                showToast('Kode kustomer sudah ada!', '#dc3545');
                } else {
                showToast('Gagal menyimpan data!', '#dc3545');
                }
            })
            .catch(err => {
                console.error(err);
                showToast('Gagal koneksi ke server!', '#dc3545');
            });
        });

        const popup = document.getElementById('popupNotif');
        if (popup) {
            popup.addEventListener('click', () => popup.style.display = 'none');
        }

        function tampilkanKonfirmasiHapus() {
            if (!cekAkses('hapus')) return;
            document.getElementById('popupConfirmHapus').style.display = 'block';
        }

        function konfirmasiHapus(setuju) {
            const popup = document.getElementById('popupConfirmHapus');
            popup.style.display = 'none';

            if (setuju) {
                const formData = new FormData(document.getElementById('kustomerForm'));
                formData.set('aksi', 'hapus');

                fetch('proseskustomer.php', {
                    method: 'POST',
                    body: formData
                })
                .then(res => res.json())
                .then(response => {
                    if (response.status === 'success') {
                        showToast('Data berhasil dihapus');
                        initializeFormButtons(); // reset tampilan
                        document.getElementById('kustomerForm').reset();
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