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
  <style>
    .akses-table {
        border-collapse: collapse;
        width: 100%;
        margin-top: 15px;
        font-size: 13px;
    }
    .akses-table th, .akses-table td {
        border: 1px solid #ccc;
        padding: 4px 6px;
        text-align: center;
    }
    .akses-table th {
        background: #f4f4f4;
    }
    .save-btn {
        margin-top: 10px;
        padding: 6px 12px;
        background: #28a745;
        border: none;
        color: white;
        border-radius: 4px;
        cursor: pointer;
    }
    .save-btn:hover {
        background: #218838;
    }
  </style>
</head>
<body>
<?php include 'navbar.php'; ?>
<main>
  <h2>Pengaturan Akun</h2>

  <div class="search-bar">
      <input type="text" id="searchKode" placeholder="Kode User" oninput="handleInput('kode')" style="text-transform: uppercase;">
      <input type="text" id="searchNama" placeholder="Nama User" oninput="handleInput('nama')" style="text-transform: uppercase;">
      <button type="button" id="searchbtn" onclick="triggerSearch()">🔍 Cari</button>
  </div>

  <form id="userForm" onsubmit="return false;">
    <input type="hidden" name="aksi" id="aksi" value="">
    <div class="form-atas">
      <label for="kodeuser">Kode User</label>
      <input type="text" name="kodeuser" id="kodeuser" class="long-input" readonly style="text-transform: uppercase;">

      <label for="namauser">Nama User</label>
      <input type="text" name="namauser" id="namauser" class="long-input" readonly style="text-transform: uppercase;">
    </div>
  </form>

  <!-- TABEL AKSES -->
  <div id="aksesContainer"></div>
</main>

<!-- POPUP PENCARIAN USER -->
<div id="popupFilter" style="display:none; position:fixed; top:0; left:0; width:100%; height:100%; background:rgba(0,0,0,0.4); z-index:1000;">
    <div id="popupContent" style="position:absolute; top:50%; left:50%; transform:translate(-50%, -50%);
        width:90%; max-width:600px; background:#fff; border-radius:8px; padding:16px; box-shadow:0 4px 15px rgba(0,0,0,0.3);">
        <div style="display:flex; justify-content:space-between; align-items:center; margin-bottom:10px;">
            <h3 style="font-size:14px; margin:0;">Hasil Pencarian</h3>
            <span onclick="closeFilterPopup()" style="cursor:pointer; font-weight:bold; font-size:16px; color:#666;">&times;</span>
        </div>
        <div style="max-height: 60vh; overflow: auto;">
            <table style="width:100%; border-collapse: collapse; font-size: 12px;">
                <thead style="background:#f2f2f2;"></thead>
                <tbody id="popupList"></tbody>
            </table>
        </div>
    </div>
</div>

<!-- TOAST -->
<div id="toast" style="display:none; position:fixed; top:20px; right:20px; padding:10px 16px; border-radius:6px; color:white; font-size:13px; z-index:1000;"></div>

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
    const thead = list.closest('table').querySelector('thead');
    thead.innerHTML = '';

    if (!dataList || dataList.length === 0) return;

    const headerRow = document.createElement('tr');
    ['kodeuser', 'namauser'].forEach(field => {
        const th = document.createElement('th');
        th.textContent = field.toUpperCase();
        th.style.padding = '4px';
        th.style.border = '1px solid #ccc';
        headerRow.appendChild(th);
    });
    thead.appendChild(headerRow);

    dataList.forEach(item => {
        const tr = document.createElement('tr');
        tr.style.cursor = 'pointer';
        tr.onclick = () => { pilihUser(item); closeFilterPopup(); };
        ['kodeuser', 'namauser'].forEach(field => {
            const td = document.createElement('td');
            td.textContent = item[field];
            td.style.border = '1px solid #ccc';
            td.style.padding = '4px';
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
    if (!inputSearch) return showToast('Isi kode atau nama!', '#dc3545');
    const keyword = inputSearch.value.trim().toUpperCase();
    fetch(`filter_user.php?keyword=${encodeURIComponent(keyword)}`)
        .then(res => res.json())
        .then(data => data.length ? showFilterPopup(data) : showToast('Data tidak ada!', '#dc3545'));
}

function pilihUser(data) {
    document.getElementById('kodeuser').value = data.kodeuser;
    document.getElementById('namauser').value = data.namauser;
    loadMenuAkses(data.kodeuser);
}

// Muat menu dan akses user
function loadMenuAkses(kodeuser) {
    fetch(`load_akses.php?kodeuser=${kodeuser}`)
        .then(res => res.text())
        .then(html => document.getElementById('aksesContainer').innerHTML = html);
}

// Simpan perubahan akses
function saveAkses(kodeuser) {
    const formData = new FormData(document.getElementById('aksesForm'));
    fetch(`save_akses.php?kodeuser=${kodeuser}`, {method:'POST', body:formData})
        .then(res => res.text())
        .then(msg => {
            showToast(msg);        // tampilkan pesan
            loadMenuAkses(kodeuser); // reload tabel akses dengan data terbaru
        });
}

</script>
</body>
</html>
