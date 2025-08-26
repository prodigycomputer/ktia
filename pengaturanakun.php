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

  <form id="userForm" enctype="multipart/form-data" onsubmit="return false;">
    <input type="hidden" name="aksi" id="aksi" value="">
    <div class="form-atas">
      <label for="kodeuser">Kode User</label>
      <input type="text" name="kodeuser" id="kodeuser" class="long-input" required style="text-transform: uppercase;" readonly>

      <label for="namauser">Nama User</label>
      <input type="text" name="namauser" id="namauser" class="verylong-input" required style="text-transform: uppercase;" readonly>
    </div>
    <div class="form-penjualan-tengah">
        <div style="margin-top:10px; display:flex; gap:8px;">
            <button id="btnSave" type="button">Simpan</button>
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
                    <!-- Data forms dimuat dari JS -->
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
                <thead style="background:#f2f2f2;"></thead>
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
function showToast(message, color = '#28a745') {
  const toast = document.getElementById('toast');
  toast.textContent = message;
  toast.style.backgroundColor = color;
  toast.style.display = 'block';
  setTimeout(() => toast.style.display = 'none', 2000);
}

let inputSearch = null;
function handleInput(type) {
    const kodeInput = document.getElementById('searchKode');
    const namaInput = document.getElementById('searchNama');

    if (type === 'kode') {
        namaInput.disabled = kodeInput.value.trim() !== '';
        inputSearch = kodeInput.value.trim() !== '' ? kodeInput : null;
    } else {
        kodeInput.disabled = namaInput.value.trim() !== '';
        inputSearch = namaInput.value.trim() !== '' ? namaInput : null;
    }
}

function showFilterPopup(dataList) {
    const list = document.getElementById('popupList');
    list.innerHTML = '';

    const table = list.closest('table');
    const thead = table.querySelector('thead');
    thead.innerHTML = '';

    if (!dataList || dataList.length === 0) return;

    const headerRow = document.createElement('tr');
    ['kodeuser', 'namauser'].forEach(field => {
        const th = document.createElement('th');
        th.textContent = field.toUpperCase();
        th.style.padding = '4px';
        th.style.border = '1px solid #ccc';
        th.style.background = '#f9f9f9';
        headerRow.appendChild(th);
    });
    thead.appendChild(headerRow);

    dataList.forEach(item => {
        const tr = document.createElement('tr');
        tr.style.cursor = 'pointer';
        tr.addEventListener('click', () => {
            closeFilterPopup();
            pilihUser(item);
        });
        ['kodeuser', 'namauser'].forEach(field => {
            const td = document.createElement('td');
            td.textContent = item[field] || '';
            td.style.padding = '4px';
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
        showToast('Isi kode atau nama terlebih dahulu!', '#dc3545');
        return;
    }
    const keyword = inputSearch.value.trim().toUpperCase();
    if (!keyword) {
        showToast('Kolom pencarian tidak boleh kosong!', '#dc3545');
        return;
    }
    fetch(`filter_user.php?keyword=${encodeURIComponent(keyword)}`)
        .then(res => res.json())
        .then(data => {
            if (data.length === 0) {
                showToast('Data tidak ada!', '#dc3545');
            } else {
                showFilterPopup(data);
            }
        });
}

function pilihUser(data) {
    document.getElementById('kodeuser').value = data.kodeuser;
    document.getElementById('namauser').value = data.namauser;
    closeFilterPopup();
}

// List forms & DB fields
const formsList = [
  "Stok","Area","Tipe","Kustomer","Supplier","Sales","Gudang","Merek","Golongan","Grup",
  "Regis Akun","Pengaturan Akun","Pembelian","Penjualan","Mutasi Barang","Penyesuaian",
  "Laporan Stok","Laporan Kustomer","Laporan Supplier","Laporan Pembelian","Laporan Penjualan"
];
const dbFields = [
  "sstok","sarea","stipe","skust","ssupp","ssales","sgudang",
  "smerek","sgol","sgrup","sregakun","spakun","sbeli","sjual",
  "smutasi","spenyesuaian","slstok","slkust","slsup","slbeli","sljual"
];

// Load forms ke tabel
function loadFormsTable() {
  const tbody = document.querySelector("#tabelUser tbody");
  tbody.innerHTML = "";
  formsList.forEach((formName, i) => {
    const tr = document.createElement("tr");

    // Nama Form
    const tdForm = document.createElement("td");
    tdForm.textContent = formName;
    tr.appendChild(tdForm);

    // Dropdown + hidden input
    const tdSelect = document.createElement("td");
    const select = document.createElement("select");
    select.innerHTML = `
      <option value="1">Bisa</option>
      <option value="0">Tidak Bisa</option>
    `;
    select.setAttribute("data-field", dbFields[i]);

    // Hidden input untuk simpan value dropdown
    const hidden = document.createElement("input");
    hidden.type = "hidden";
    hidden.name = dbFields[i];
    hidden.value = "1"; // default "Bisa"
    tdSelect.appendChild(select);
    tdSelect.appendChild(hidden);
    tr.appendChild(tdSelect);

    // Update hidden setiap kali dropdown berubah
    select.addEventListener("change", () => {
      hidden.value = select.value;
    });

    tbody.appendChild(tr);
  });
}
document.addEventListener("DOMContentLoaded", loadFormsTable);

// Tombol Simpan -> ambil data dari hidden inputs
document.getElementById("btnSave").addEventListener("click", () => {
  const kodeuser = document.getElementById("kodeuser").value;
  if (!kodeuser) {
    showToast("Pilih user dulu!", "#dc3545");
    return;
  }

  const data = { kodeuser };
  const hiddenInputs = document.querySelectorAll("#tabelUser input[type=hidden]");
  hiddenInputs.forEach(inp => {
    data[inp.name] = inp.value;
  });

  fetch("prosesaturuser.php", {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(data)
  })
  .then(res => res.json())
  .then(res => {
    if (res.status === "ok") {
      showToast("Data berhasil disimpan!");
    } else {
      showToast("Gagal menyimpan!", "#dc3545");
      console.log(res);
    }
  })
  .catch(err => {
    console.error(err);
    showToast("Error koneksi!", "#dc3545");
  });
});
</script>
<script src="notif.js"></script>
</body>
</html>
