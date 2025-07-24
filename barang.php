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
    <title>Data Barang</title>
    <link rel="stylesheet" href="navbar.css">
    <link rel="stylesheet" href="form.css">
</head>
<body>
    <button class="hamburger" onclick="toggleSidebar()">☰</button>
    <div class="overlay" id="sidebarOverlay" onclick="toggleSidebar()"></div>
    <?php include 'navbar.php'; ?>

    <main>
        <h2>Data Barang</h2>

            <div class="search-bar">
                <input type="text" id="searchKode" class="search-kode" placeholder="Kode barang" oninput="handleInput('kode')" style="text-transform: uppercase;">
                <input type="text" id="searchNama" class="search-nama" placeholder="Nama barang" oninput="handleInput('nama')" style="text-transform: uppercase;">
                <button type="button" id="searchbtn" onclick="triggerSearch()">🔍 Cari</button>
            </div>

            <form id="barangForm" action="prosesbarang.php" method="POST" enctype="multipart/form-data" onsubmit="return validateForm()">
                <input type="hidden" name="aksi" id="aksi" value="">    

                <div class="form-atas">
                    <label for="kodebrg">Kode Barang</label>
                    <div style="display: flex; gap: 5px; align-items: center;">
                        <input type="text" name="kodebrg" id="kodebrg" class="long-input" required style="text-transform: uppercase;">
                    </div>

                    <label for="namabrg">Nama Barang</label>
                    <div style="display: flex; gap: 5px; align-items: center;">
                        <input type="text" name="namabrg" id="namabrg" class="verylong-input" required style="text-transform: uppercase;">
                    </div>

                    <label for="satuan1">Satuan 1</label>
                    <input type="text" name="satuan1" id="satuan1" class="lesslong-input" required style="text-transform: uppercase;">

                    <label for="isi1">Isi 1</label>
                    <input type="number" step="0.01" name="isi1" id="isi1" class="medium-input" required oninput="checkIsi1()" >

                    <label for="satuan2">Satuan 2</label>
                    <input type="text" name="satuan2" id="satuan2" class="lesslong-input" disabled style="text-transform: uppercase;">

                    <label for="isi2">Isi 2</label>
                    <input type="number" step="0.01" name="isi2" id="isi2" class="medium-input" disabled oninput="checkIsi2()">

                    <label for="satuan3">Satuan 3</label>
                    <input type="text" name="satuan3" id="satuan3" class="lesslong-input" disabled style="text-transform: uppercase;">

                    <label for="gambar">Gambar</label>
                    <div style="display: flex; gap: 5px; align-items: center;">
                        <input type="text" name="gambar" id="gambar" class="lesslong-input" readonly>
                        <input type="file" id="uploadInput" accept=".png" style="display: none;" onchange="handleFileUpload(this)">
                        <button type="button" style="background: #218838;" onclick="document.getElementById('uploadInput').click();">Upload</button>
                    </div>

                </div>
                <div class="form-bawah">
                    <button type="button" onclick="openHargaPopup()" style="margin-top: 10px; background-color:#17a2b8; color:white; padding:6px 14px; border:none; border-radius:4px;">💰 Harga</button>
                    <div id="hiddenHargaFields"></div>
                </div>

    
            <button id="btnTambah" type="submit" onclick="document.getElementById('aksi').value='tambah'">Tambah</button>
            <button id="btnEdit" type="submit" onclick="document.getElementById('aksi').value='update'">Edit</button>
            <button id="btnHapus" type="submit" onclick="document.getElementById('aksi').value='hapus'">Hapus</button>
            <button id="btnCancel" type="button" onclick="cancelEdit()">Cancel</button>

        </form>
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

            <div style="
                max-height: 60vh;
                overflow-y: auto;
                overflow-x: auto;
                border: 1px solid #ccc;
            ">
                <table style="min-width: 1200px; border-collapse: collapse; font-size: 12px;">
                <thead style="background:#f2f2f2;">
                <tr>
                    <th style="padding:6px; border:1px solid #ccc;">Kode</th>
                    <th style="padding:6px; border:1px solid #ccc;">Nama</th>
                    <th style="padding:6px; border:1px solid #ccc;">Satuan1</th>
                    <th style="padding:6px; border:1px solid #ccc;">Isi1</th>
                    <th style="padding:6px; border:1px solid #ccc;">Satuan2</th>
                    <th style="padding:6px; border:1px solid #ccc;">Isi2</th>
                    <th style="padding:6px; border:1px solid #ccc;">Satuan3</th>
                    <th style="padding:6px; border:1px solid #ccc;">Harga1</th>
                    <th style="padding:6px; border:1px solid #ccc;">Harga2</th>
                    <th style="padding:6px; border:1px solid #ccc;">Harga3</th>
                    <th style="padding:6px; border:1px solid #ccc;">Harga4</th>
                    <th style="padding:6px; border:1px solid #ccc;">Harga5</th>
                    <th style="padding:6px; border:1px solid #ccc;">Harga6</th>
                    <th style="padding:6px; border:1px solid #ccc;">Harga7</th>
                    <th style="padding:6px; border:1px solid #ccc;">Harga8</th>
                    <th style="padding:6px; border:1px solid #ccc;">Harga9</th>
                    <th style="padding:6px; border:1px solid #ccc;">Harga10</th>
                    <th style="padding:6px; border:1px solid #ccc;">Harga11</th>
                    <th style="padding:6px; border:1px solid #ccc;">Harga12</th>

                </tr>
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
            <div style="flex:1; overflow-x:auto; overflow-y:auto; padding-bottom:10px;">
                <div id="hargaInputsContainer" style="
                    display: flex;
                    gap: 40px;
                    min-width: max-content;
                    padding-bottom: 10px;
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
    <script>
        function initializeFormButtons() {
            document.getElementById('btnTambah').disabled = false;
            document.getElementById('btnEdit').disabled = true;
            document.getElementById('btnHapus').disabled = true;
            document.getElementById('btnCancel').disabled = true;
        }
        initializeFormButtons(); 
        // panggil saat halaman dimuat
        let inputSearch = null;
        let searchBtn = document.getElementById('searchbtn')
        let hargaData = []

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

            // Ambil jumlah harga tertinggi dari data pertama
            const hargaFields = [];
            for (let i = 1; i <= 50; i++) {
                if (`harga${i}` in dataList[0]) {
                    hargaFields.push(`harga${i}`);
                } else {
                    break;
                }
            }

            const fixedFields = ['kodebrg', 'namabrg', 'satuan1', 'isi1', 'satuan2', 'isi2', 'satuan3'];

            // Buat header baru
            const headerRow = document.createElement('tr');
            [...fixedFields, ...hargaFields].forEach(field => {
                const th = document.createElement('th');
                th.textContent = field.toUpperCase();
                th.style.padding = '6px';
                th.style.border = '1px solid #ccc';
                th.style.background = '#f2f2f2';
                headerRow.appendChild(th);
            });
            thead.appendChild(headerRow);

            // Isi data row
            dataList.forEach(item => {
                const tr = document.createElement('tr');
                tr.style.cursor = 'pointer';
                tr.style.transition = 'background 0.2s';

                tr.addEventListener('mouseover', () => {
                    tr.style.backgroundColor = '#f0f0f0';
                });
                tr.addEventListener('mouseout', () => {
                    tr.style.backgroundColor = '#fff';
                });
                tr.addEventListener('click', () => {
                    closeFilterPopup();
                    pilihBarang(item);
                });

                [...fixedFields, ...hargaFields].forEach(field => {
                    const td = document.createElement('td');
                    td.textContent = item[field] || '';
                    td.style.padding = '6px';
                    td.style.border = '1px solid #ccc';
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
                showToast('Isi KODE atau NAMA terlebih dahulu!', '#ffc107');
                return;
            }

            const keyword = inputSearch.value.trim().toUpperCase();
            if (!keyword) {
                showToast('Kolom pencarian tidak boleh kosong!', '#ffc107');
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


        function pilihBarang(data) {
            document.getElementById('kodebrg').value = data.kodebrg;
            document.getElementById('namabrg').value = data.namabrg;
            for (let i = 1; i <= 50; i++) {
                if (data[`harga${i}`] !== undefined) {
                    hargaData.push(data[`harga${i}`]);
                } else {
                    break;
                }
            }

            document.getElementById('satuan1').value = data.satuan1;
            document.getElementById('isi1').value = data.isi1;

            // Logika enable/disable berdasarkan isi1 dan isi2
            if (parseFloat(data.isi1) > 1) {
                document.getElementById('satuan2').disabled = false;
                document.getElementById('isi2').disabled = false;
                document.getElementById('satuan2').value = data.satuan2;
                document.getElementById('isi2').value = data.isi2;

                if (parseFloat(data.isi2) > 1) {
                    document.getElementById('satuan3').disabled = false;
                    document.getElementById('satuan3').value = data.satuan3;
                } else {
                    document.getElementById('satuan3').disabled = true;
                    document.getElementById('satuan3').value = '';
                }
            } else {
                // disable semua turunan
                document.getElementById('satuan2').disabled = true;
                document.getElementById('isi2').disabled = true;
                document.getElementById('satuan3').disabled = true;

                document.getElementById('satuan2').value = '';
                document.getElementById('isi2').value = '';
                document.getElementById('satuan3').value = '';
            }
            document.getElementById('btnTambah').disabled = true;
            document.getElementById('btnEdit').disabled = false;
            document.getElementById('btnHapus').disabled = false;
            document.getElementById('btnCancel').disabled = false;

            inputSearch.value = '';
            inputSearch.disabled = true;
            document.getElementById('searchbtn').disabled = true;
            dropdown.style.display = 'none';

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
            const satuan1 = document.getElementById('satuan1').value.trim();
            if (!satuan1) {
                alert('Satuan 1 wajib diisi.');
                return false;
            }
            return true;
        }

        function cancelEdit() {
            document.getElementById('barangForm').reset();

            // Reset tombol
            initializeFormButtons();

            // Reset search input
            const kodeInput = document.getElementById('searchKode');
            const namaInput = document.getElementById('searchNama');
            kodeInput.disabled = false;
            namaInput.disabled = false;
            kodeInput.value = '';
            namaInput.value = '';
            document.getElementById('searchbtn').disabled = false;

            // Reset input yang di-disable manual
            document.getElementById('satuan2').disabled = true;
            document.getElementById('isi2').disabled = true;
            document.getElementById('satuan3').disabled = true;
            document.getElementById('gambar').value = '';

            inputSearch = null;

            // ✅ Reset hargaData dan jumlahHarga ke default
            hargaData = Array(<?= $jmlharga ?>).fill("");
            generateHargaInputs(<?= $jmlharga ?>);


            // Jika popup harga sedang terbuka, juga perbarui tampilannya
            generateHargaInputs();
        }

        function handleFileUpload(input) {
            const file = input.files[0];
            if (file) {
                if (!file.name.toLowerCase().endsWith('.png')) {
                    alert("Hanya file JPG yang diperbolehkan!");
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
                            alert('Kode Barang sudah terdaftar!');
                            this.value = '';
                            this.focus();
                        }
                    });
            }
        });

        document.getElementById('barangForm').addEventListener('submit', function () {
            const hiddenDiv = document.getElementById('hiddenHargaFields');
            hiddenDiv.innerHTML = '';
            hargaData.forEach((val, index) => {
                const input = document.createElement('input');
                input.type = 'hidden';
                input.name = `harga${index + 1}`;
                input.value = val;
                hiddenDiv.appendChild(input);
            });

            setTimeout(() => {
                initializeFormButtons();
                document.getElementById('barangForm').reset();

                // Aktifkan kembali search bar dan tombol
                document.getElementById('searchKode').disabled = false;
                document.getElementById('searchNama').disabled = false;
                document.getElementById('searchKode').value = '';
                document.getElementById('searchNama').value = '';
                document.getElementById('searchbtn').disabled = false;
            }, 100);
        });
        

        function openHargaPopup() {
            const jumlah = parseInt("<?= $jmlharga ?>");
            generateHargaInputs(jumlah);
            document.getElementById('popupHarga').style.display = 'block';
        }

        

        function closeHargaPopup() {
            document.getElementById('popupHarga').style.display = 'none';
        }

        function generateHargaInputs(jumlah) {
            const container = document.getElementById('hargaInputsContainer');
            const perKolom = 6;

            // Paksa kelipatan 6
            if (jumlah % perKolom !== 0) {
                jumlah = Math.ceil(jumlah / perKolom) * perKolom;
            }

            const totalKolom = Math.ceil(jumlah / perKolom);
            const existing = hargaData.slice(0, jumlah);
            while (existing.length < jumlah) existing.push("");

            container.innerHTML = '';
            const kolomArr = [];

            for (let i = 0; i < totalKolom; i++) {
                const col = document.createElement('div');
                col.style.display = 'flex';
                col.style.flexDirection = 'column';
                col.style.gap = '10px';
                kolomArr.push(col);
                container.appendChild(col);
            }

            for (let i = 0; i < jumlah; i++) {
                const colIndex = Math.floor(i / perKolom);
                const wrapper = document.createElement('div');
                wrapper.style.display = 'flex';
                wrapper.style.alignItems = 'center';
                wrapper.style.gap = '8px';

                const label = document.createElement('label');
                label.textContent = `Harga ${i + 1}`;
                label.style.fontSize = '12px';
                label.style.width = '60px';

                const input = document.createElement('input');
                input.type = 'number';
                input.step = '0.01';
                input.id = `hargaInput${i}`;
                input.value = existing[i];
                input.style.padding = '6px 8px';
                input.style.fontSize = '12px';
                input.style.border = '1px solid #ccc';
                input.style.borderRadius = '4px';
                input.style.width = '120px';

                wrapper.appendChild(label);
                wrapper.appendChild(input);
                kolomArr[colIndex].appendChild(wrapper);
            }
        }

        function simpanHarga() {
            const jumlah = parseInt("<?= $jmlharga ?>");
            let kosong = false;

            for (let i = 0; i < jumlah; i++) {
                const val = document.getElementById(`hargaInput${i}`).value.trim();
                if (val === '') kosong = true;
                hargaData.push(val);
            }

            if (kosong) {
                alert("Semua harga harus diisi!");
                return;
            }
            closeHargaPopup();
        }
    </script>
    <script src="notif.js"></script>
</body>
</html>
