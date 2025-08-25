<?php
session_start();
include 'koneksi.php';
?>

<!DOCTYPE html>
<html lang="en">
<head>
  <meta charset="UTF-8" />
  <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
  <title>Pengaturan Akun</title>
  <link rel="stylesheet" href="navbar.css" />
  <link rel="stylesheet" href="form.css" />
</head>
<body>
<?php include 'navbar.php'; ?>
<main>
  <h2>Pengaturan Akun</h2>

  <div class="search-bar">
      <input type="text" id="searchKode" class="search-kode" placeholder="Kode User" oninput="handleInput('kode')" style="text-transform: uppercase;">
      <input type="text" id="searchNama" class="search-nama" placeholder="Nama User" oninput="handleInput('nama')" style="text-transform: uppercase;">
      <button type="button" id="searchbtn" onclick="triggerSearch()">🔍 Cari</button>
  </div>

  <form id="userForm" action="prosesuser.php" enctype="multipart/form-data" onsubmit="return false;">
    <input type="hidden" name="aksi" id="aksi" value="">
    <div class="form-atas">
      <label for="kodeuser">Kode User</label>
      <input type="text" name="kodeuser" id="kodeuser" class="long-input" required style="text-transform: uppercase;" readonly>

      <label for="namauser">Nama User</label>
      <input type="text" name="namauser" id="namauser" class="verylong-input" required style="text-transform: uppercase;" readonly>
    </div>
    <div class="form-penjualan-tengah">
        <div style="margin-top:10px; display:flex; gap:8px;">
            <button id="btnSave" type="submit">Simpan</button>
        </div>
        <div style="overflow-x: auto;">
            <table class="tabel-hasil" id="tabelUser" style="min-width: 1200px;">
                <thead>
                    <tr>
                        <th style="min-width: 300px;">Nama Form</th>
                        <th style="min-width: 50px;">Pilih Status</th>
                    </tr>
                </thead>
                <tbody>
                    <!-- isi dari popupitempenjualan-->
                </tbody>
            </table>
        </div>
    </div>
  </form>
  
</main>

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
  padding: 10px 16px;
  border-radius: 6px;
  color: white;
  font-size: 13px;
  z-index: 1000;
  box-shadow: 0 2px 6px rgba(0,0,0,0.2);
"></div>
<script>
let currentstat = null;

function showToast(message, color = '#28a745') {
  const toast = document.getElementById('toast');
  toast.textContent = message;
  toast.style.backgroundColor = color;
  toast.style.display = 'block';
  setTimeout(() => toast.style.display = 'none', 2000);
}

function forceUppercase(id) {
  const input = document.getElementById(id);
  input.addEventListener('input', () => {
    input.value = input.value.toUpperCase();
  });
}
forceUppercase('kodeuser');
forceUppercase('namauser');

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

    const fixedFields = ['kodeuser', 'namauser'];

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
            pilihUser(item);
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

    fetch(`filter_user.php?keyword=${encodeURIComponent(keyword)}`)
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

function pilihUser(data) {
    document.getElementById('kodeuser').value = data.kodeuser;
    document.getElementById('namauser').value = data.namauser;
    // Logika enable/disable berdasarkan isi1 dan isi2

    previousFormData = {
      kodeuser: data.kodeuser,
      namauser: data.namauser,
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

const formsList = [
  "Stok", "Area", "Tipe", "Kustomer", "Supplier", "Sales", "Gudang", "Merek", "Golongan", "Grup",
  "Regis Akun", "Pengaturan Akun", "Pembelian", "Penjualan", "Mutasi Barang", "Penyesuaian",
  "Laporan Stok", "Laporan Kustomer", "Laporan Supplier", "Laporan Pembelian", "Laporan Penjualan"
];

function loadFormsTable() {
  const tbody = document.querySelector("#tabelUser tbody");
  tbody.innerHTML = "";

  formsList.forEach(formName => {
    const tr = document.createElement("tr");

    // Nama Form
    const tdForm = document.createElement("td");
    tdForm.textContent = formName;
    tr.appendChild(tdForm);

    // Pilih Status (Dropdown)
    const tdSelect = document.createElement("td");
    const select = document.createElement("select");
    select.innerHTML = `
      <option value="1">Bisa</option>
      <option value="0">Tidak Bisa</option>
    `;
    tdSelect.appendChild(select);
    tr.appendChild(tdSelect);

    tbody.appendChild(tr);
  });
}

document.addEventListener("DOMContentLoaded", loadFormsTable);
</script>
<script src="notif.js"></script>
</body>
</html>
