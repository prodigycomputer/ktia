<?php
session_start();
include 'koneksi.php';
?>

<!DOCTYPE html>
<html lang="en">
<head>
  <meta charset="UTF-8" />
  <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
  <title>Data Group</title>
  <link rel="stylesheet" href="navbar.css" />
  <link rel="stylesheet" href="form.css" />
</head>
<body>
<?php include 'navbar.php'; ?>
<main>
  <h2>Group Barang</h2>

  <div class="search-bar">
    <div class="search-group">
      <input type="text" id="searchGrup" placeholder="Cari kode/nama grup..." class="input-group" oninput="filterDropdown()" style="text-transform: uppercase;">
      <div id="dropdownGrup" class="dropdown-result"></div>
    </div>
  </div>

  <form id="grupForm" action="prosesgrup.php" method="POST" enctype="multipart/form-data" onsubmit="return prepareSave()">
    <input type="hidden" name="aksi" id="aksi" value="">
    <input type="hidden" name="kodegrup_lama" id="kodegrup_lama" value="">

    <div class="form-atas">
      <label for="kodegrup">Kode</label>
      <input type="text" name="kodegrup" id="kodegrup" class="long-input" required style="text-transform: uppercase;">

      <label for="namagrup">Nama</label>
      <input type="text" name="namagrup" id="namagrup" class="verylong-input" required style="text-transform: uppercase;">
    </div>
    <div style="margin-top:10px; display:flex; gap:8px;">
      <button id="btnSave" type="submit">Simpan</button>
      <button id="btnTambah" type="button" onclick="initializeTambah()">Tambah</button>
      <button id="btnEdit" type="button" onclick="initializeUbah()">Ubah</button>
      <button id="btnHapus" type="submit" onclick="document.getElementById('aksi').value='hapus'">Hapus</button>
      <button id="btnCancel" type="button" onclick="cancelForm()">Batal</button>
    </div>
  </form>
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

<script>
let currentstat = null;

function showToast(message, color = '#28a745') {
  const toast = document.getElementById('toast');
  toast.textContent = message;
  toast.style.backgroundColor = color;
  toast.style.display = 'block';
  setTimeout(() => toast.style.display = 'none', 2000);
}

function initializeFormButtons() {
  currentstat = null;

  document.getElementById('btnTambah').disabled = false;
  document.getElementById('btnEdit').disabled = true;
  document.getElementById('btnHapus').disabled = true;
  document.getElementById('btnCancel').disabled = true;
  document.getElementById('btnSave').disabled = true;

  document.getElementById('searchGrup').disabled = false;
  document.getElementById('kodegrup').disabled = true;
  document.getElementById('namagrup').disabled = true;
}
initializeFormButtons();

function initializeTambah() {
  currentstat = 'tambah';
  showToast('Kamu sedang menambah data...', '#ffc107');

  document.getElementById('btnTambah').disabled = true;
  document.getElementById('btnEdit').disabled = true;
  document.getElementById('btnHapus').disabled = true;
  document.getElementById('btnCancel').disabled = false;
  document.getElementById('btnSave').disabled = false;

  document.getElementById('searchGrup').disabled = true;
  document.getElementById('kodegrup').disabled = false;
  document.getElementById('namagrup').disabled = false;

  document.getElementById('grupForm').reset();
  document.getElementById('searchGrup').value = '';

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

  document.getElementById('searchGrup').disabled = true;
  document.getElementById('kodegrup').disabled = false;
  document.getElementById('namagrup').disabled = false;
  setActiveButtonStyle(document.getElementById('btnEdit'))
}

function cancelForm() {
  document.getElementById('grupForm').reset();
  document.getElementById('aksi').value = '';
  document.getElementById('searchGrup').value = '';
  resetButtonStyles();
  initializeFormButtons();
}

function prepareSave() {
  if (!currentstat) {
    showToast('Tidak ada data yang sedang diubah atau ditambah.', '#dc3545');
    return false;
  }
  document.getElementById('aksi').value = currentstat;
  return true;
}

function forceUppercase(id) {
  const input = document.getElementById(id);
  input.addEventListener('input', () => {
    input.value = input.value.toUpperCase();
  });
}
forceUppercase('kodegrup');
forceUppercase('namagrup');

function filterDropdown() {
  const keyword = document.getElementById('searchGrup').value.toUpperCase().trim();
  const dropdown = document.getElementById('dropdownGrup');

  if (keyword === '') {
    dropdown.style.display = 'none';
    return;
  }

  fetch(`filter_grup.php?keyword=${encodeURIComponent(keyword)}`)
    .then(res => res.json())
    .then(data => {
      dropdown.innerHTML = '';
      if (data.length === 0) {
        dropdown.style.display = 'none';
        return;
      }

      data.forEach(grup => {
        const div = document.createElement('div');
        div.textContent = `${grup.kodegrup} - ${grup.namagrup}`;
        div.onclick = () => {
          document.getElementById('kodegrup').value = grup.kodegrup;
           document.getElementById('kodegrup_lama').value = grup.kodegrup;
          document.getElementById('namagrup').value = grup.namagrup;
          document.getElementById('dropdownGrup').style.display = 'none';
          document.getElementById('searchGrup').value = '';

          // Aktifkan tombol edit
          document.getElementById('btnTambah').disabled = true;
          document.getElementById('btnEdit').disabled = false;
          document.getElementById('btnHapus').disabled = true;
          document.getElementById('btnCancel').disabled = false;
          document.getElementById('btnSave').disabled = true;

          document.getElementById('searchGrup').disabled = true;
          document.getElementById('kodegrup').disabled = true;
          document.getElementById('namagrup').disabled = true;
        };
        dropdown.appendChild(div);
      });

      dropdown.style.display = 'block';
    });
}

document.getElementById('grupForm').addEventListener('submit', function () {
  setTimeout(() => {
    initializeFormButtons();
    document.getElementById('grupForm').reset();
  }, 100);
});

function setActiveButtonStyle(button) {
    button.style.backgroundColor = 'white';
    button.style.color = 'black';
    button.style.border = '1px solid #999';
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
