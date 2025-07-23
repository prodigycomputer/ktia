<?php
session_start();

include 'koneksi.php';

?>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Data Barang</title>
    <link rel="stylesheet" href="navbar.css">
    <style>
    * {
    box-sizing: border-box;
    margin: 0;
    padding: 0;
    }
    body {
        font-family: "Segoe UI", Tahoma, Geneva, Verdana, sans-serif;
        display: flex;
        height: 100vh;
        background: #f6f8fb;
        color: #333;
    }
    main {
        padding: 1rem;
        flex: 1;
        overflow-y: auto;
    }
    h2 {
        margin-bottom: 10px;
        font-size: 20px;
    }

    form {
        background: #fff;
        padding: 12px;
        border-radius: 6px;
        box-shadow: 0 0 6px rgba(0, 0, 0, 0.05);
    }

    form button {
        background-color: #007bff; /* Warna biru default */
        color: white;
        border: none;
        cursor: pointer;
        transition: 0.3s ease;
        padding: 6px;
        font-size: 13px;
        border-radius: 4px; /* opsional agar terlihat lebih modern */
    }

    button:disabled {
        background-color: #ccc !important;
        color: #666 !important;
        cursor: not-allowed !important;
    }

    .form-atas {
        display: grid;
        grid-template-columns: 0.3fr 3fr;
        gap: 4px 10px;
        background: #fff;
        padding: 12px;
    }

    .form-atas label {
        display: flex;
        align-items: center;
        font-size: 12px;
        font-weight: 600;
    }
    .form-atas input {
        padding: 4px;
        font-size: 12px;
        width: 100%;
    }
    .form-atas input.short-input { width: 80px; }
    .form-atas input.medium-input { width: 120px; }
    .form-atas input.long-input { width: 220px; }
    .form-atas input.lesslong-input { width: 250px; }
    .form-atas input.verylong-input { width: 300px; }
    .form-atas button {
        grid-column: span 2;
        background-color: #0acf45ff;
        color: white;
        border: none;
        cursor: pointer;
        transition: 0.3s ease;
        padding: 5px;
        font-size: 12px;
    }

    .form-bawah {
        display: flex;
        gap: 4px 60px;
        background: #fff;
        padding: 12px;
        margin-top: -20px;
    }

    .harga-container {
        display: grid;
        grid-template-columns: 2.35fr 3fr;
        gap: 4px 10px;
        background: #fff;
    }

    .harga-container label {
        display: flex;
        align-items: center;
        font-size: 12px;
        font-weight: 600;
    }
    .harga-container input {
        padding: 4px;
        font-size: 12px;
        width: 100%;
    }
    .harga-container input.short-input { width: 80px; }
    .harga-container input.medium-input { width: 120px; }
    .harga-container input.long-input { width: 220px; }
    .harga-container input.lesslong-input { width: 250px; }
    .harga-container input.verylong-input { width: 300px; }
    a {
        text-decoration: none;
        color: #007bff;
    }
    a:hover {
        text-decoration: underline;
    }
    .no-data {
        text-align: center;
        padding: 20px;
        color: #888;
    }

    .search-bar-horizontal {
        display: flex;
        gap: 10px;
        align-items: center;
        margin-bottom: 15px;
    }

    .search-bar-horizontal input {
        padding: 4px 8px;
        font-size: 12px;
        border: 1px solid #ccc;
        border-radius: 4px;
    }

    .search-kode {
        width: 150px;
    }

    .search-nama {
        width: 250px;
    }

    #searchbtn {
        padding: 4px 12px;
        font-size: 12px;
        border-radius: 4px;
        background-color: #28a745;
        color: white;
        border: none;
        cursor: pointer;
    }

    #searchbtn:hover {
        background-color: #218838;
    }


    .hamburger {
        display: none;
        font-size: 20px;
        background-color: #ffffff;
        color: #333;
        border: 1px solid #ccc;
        border-radius: 4px;
        padding: 4px 10px;
        margin-bottom: 10px;
        box-shadow: 0 1px 3px rgba(0,0,0,0.1);
        cursor: pointer;
    }

    .overlay {
        display: none;
        position: fixed;
        top: 0; left: 0;
        height: 100%;
        width: 100%;
        background: rgba(0,0,0,0.3);
        z-index: 1000;
    }

    .sidebar {
        transition: left 0.3s ease;
    }

    .sidebar.active {
        left: 0 !important;
    }

    .overlay.active {
        display: block;
    }

    @media (max-width: 768px) {
        .hamburger {
            display: inline-block;
            margin-left: 0px;
        }

        .sidebar {
            position: fixed;
            left: -250px;
            top: 0;
            height: 100%;
            width: 220px;
            background: #2c3e50;
            z-index: 1100;
            padding-top: 60px;
        }

        main {
            padding-top: 20px;
        }

        .form-atas,
        .form-bawah,
        .harga-container,
        .search-bar-horizontal {
            display: flex !important;
            flex-direction: column !important;
        }

        .form-atas input,
        .harga-container input {
            width: 100% !important;
        }

        #btnTambah, #btnEdit, #btnHapus {
            width: 100%;
            margin-bottom: 5px;
        }
    }

    </style>
</head>
<body>
    <button class="hamburger" onclick="toggleSidebar()">☰</button>
    <div class="overlay" id="sidebarOverlay" onclick="toggleSidebar()"></div>
    <?php include 'navbar.php'; ?>

    <main>
        <h2>Data Barang</h2>

            <div class="search-bar-horizontal">
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
                        <button type="button" onclick="document.getElementById('uploadInput').click();">Upload</button>
                    </div>

                </div>
                <div class="form-bawah">
                    <div class="harga-container">
                        <label for="harga1">Harga 1</label>
                        <input type="number" step="0.01" name="harga1" id="harga1" class="medium-input">

                        <label for="harga2">Harga 2</label>
                        <input type="number" step="0.01" name="harga2" id="harga2" class="medium-input">

                        <label for="harga3">Harga 3</label>
                        <input type="number" step="0.01" name="harga3" id="harga3" class="medium-input">

                        <label for="harga4">Harga 4</label>
                        <input type="number" step="0.01" name="harga4" id="harga4" class="medium-input">

                        <label for="harga5">Harga 5</label>
                        <input type="number" step="0.01" name="harga5" id="harga5" class="medium-input">

                        <label for="harga6">Harga 6</label>
                        <input type="number" step="0.01" name="harga6" id="harga6" class="medium-input">

                    </div>

                    <div class="harga-container">
                        <label for="harga7">Harga 7</label>
                        <input type="number" step="0.01" name="harga7" id="harga7" class="medium-input">

                        <label for="harga8">Harga 8</label>
                        <input type="number" step="0.01" name="harga8" id="harga8" class="medium-input">

                        <label for="harga9">Harga 9</label>
                        <input type="number" step="0.01" name="harga9" id="harga9" class="medium-input">

                        <label for="harga10">Harga 10</label>
                        <input type="number" step="0.01" name="harga10" id="harga10" class="medium-input">

                        <label for="harga11">Harga 11</label>
                        <input type="number" step="0.01" name="harga11" id="harga11" class="medium-input">

                        <label for="harga12">Harga 12</label>
                        <input type="number" step="0.01" name="harga12" id="harga12" class="medium-input">
                    </div>

                </div>
    
            <button id="btnTambah" type="submit" onclick="document.getElementById('aksi').value='tambah'">Tambah</button>
            <button id="btnEdit" type="submit" onclick="document.getElementById('aksi').value='update'">Edit</button>
            <button id="btnHapus" type="submit" onclick="document.getElementById('aksi').value='hapus'">Hapus</button>

        </form>
    </main>
       <!-- Overlay Popup Pilih Barang -->
    <div id="popupFilter" style="display:none; position:fixed; top:0; left:0; width:100%; height:100%; background:rgba(0,0,0,0.4); z-index:1000;">
    <div style="position:absolute; top:50%; left:50%; transform:translate(-50%, -50%); width:90%; max-width:500px; background:#fff; border-radius:8px; box-shadow:0 4px 15px rgba(0,0,0,0.3); padding:16px 16px 10px; max-height:70vh; overflow-y:auto;">
        
        <div style="display: flex; justify-content: space-between; align-items: center; margin-bottom: 10px;">
            <h3 style="font-size:14px; margin:0;">Pilih Data</h3>
            <span onclick="closeFilterPopup()" style="cursor:pointer; font-weight:bold; font-size:16px; color:#666;">&times;</span>
        </div>

        <ul id="popupList" style="list-style:none; padding:0; margin:0;"></ul>
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
        }
        initializeFormButtons(); 
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
            const popup = document.getElementById('popupFilter');
            const list = document.getElementById('popupList');
            list.innerHTML = '';

            dataList.forEach(item => {
                const li = document.createElement('li');
                li.textContent = `${item.kodebrg} - ${item.namabrg}`;
                li.style.padding = '10px 14px';
                li.style.cursor = 'pointer';
                li.style.borderBottom = '1px solid #eee';
                li.style.transition = 'background 0.2s';

                li.addEventListener('mouseover', () => {
                    li.style.backgroundColor = '#f5f5f5';
                });
                li.addEventListener('mouseout', () => {
                    li.style.backgroundColor = '#fff';
                });
                li.addEventListener('click', () => {
                    closeFilterPopup(); 
                    pilihBarang(item);     // isi form
                      // tutup popup
                });
                 
                list.appendChild(li);
            });

            popup.style.display = 'block';
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
                        pilihBarang(data[0]);
                    } else {
                        showFilterPopup(data); // tampilkan pilihan
                    }
                })

        }


        function pilihBarang(data) {
            document.getElementById('kodebrg').value = data.kodebrg;
            document.getElementById('namabrg').value = data.namabrg;
            document.getElementById('harga1').value = data.harga1;
            document.getElementById('harga2').value = data.harga2;
            document.getElementById('harga3').value = data.harga3;
            document.getElementById('harga4').value = data.harga4;
            document.getElementById('harga5').value = data.harga5;
            document.getElementById('harga6').value = data.harga6;
            document.getElementById('harga7').value = data.harga7;
            document.getElementById('harga8').value = data.harga8;
            document.getElementById('harga9').value = data.harga9;
            document.getElementById('harga10').value = data.harga10;
            document.getElementById('harga11').value = data.harga11;
            document.getElementById('harga12').value = data.harga12;

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
        
        function showToast(message, bgColor = '#28a745') {
            const toast = document.getElementById('toast');
            toast.innerText = message;
            toast.style.backgroundColor = bgColor;
            toast.style.display = 'block';

            setTimeout(() => {
                toast.style.display = 'none';
            }, 3000);
        }

        window.addEventListener('DOMContentLoaded', () => {
            const params = new URLSearchParams(window.location.search);
            const status = params.get('status');

            if (status) {
                let message = '';
                let color = '#28a745';

                switch (status) {
                    case 'tambah':
                        message = 'Data berhasil ditambahkan!';
                        break;
                    case 'update':
                        message = 'Data berhasil diperbarui!';
                        break;
                    case 'hapus':
                        message = 'Data berhasil dihapus!';
                        break;
                    case 'error':
                        message = 'Terjadi kesalahan saat memproses data.';
                        color = '#dc3545';
                        break;
                    case 'duplikat':
                        message = 'Kode Barang sudah terdaftar!';
                        color = '#ffc107';
                        break;
                    default:
                        return;
                }

                showToast(message, color);

                // Hapus parameter dari URL
                history.replaceState(null, '', window.location.pathname);
            }
        });
    </script>

</body>
</html>
