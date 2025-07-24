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

  <form id="grupForm" action="prosesgrup.php" method="POST" enctype="multipart/form-data" onsubmit="return validateForm()">
    <input type="hidden" name="aksi" id="aksi" value="">
    <div class="form-atas">
      <label for="kodegrup">Kode</label>
      <input type="text" name="kodegrup" id="kodegrup" class="long-input" required style="text-transform: uppercase;">

      <label for="namagrup">Nama</label>
      <input type="text" name="namagrup" id="namagrup" class="verylong-input" required style="text-transform: uppercase;">
    </div>
    <div style="margin-top:10px; display:flex; gap:8px;">
      <button id="btnTambah" type="submit" onclick="document.getElementById('aksi').value='tambah'">Tambah</button>
      <button id="btnEdit" type="submit" onclick="document.getElementById('aksi').value='update'">Edit</button>
      <button id="btnHapus" type="submit" onclick="document.getElementById('aksi').value='hapus'">Hapus</button>
      <button id="btnCancel" type="button" onclick="cancelForm()">Cancel</button>
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
  function initializeFormButtons() {
    document.getElementById('btnTambah').disabled = false;
    document.getElementById('btnEdit').disabled = true;
    document.getElementById('btnHapus').disabled = true;
    document.getElementById('btnCancel').disabled = true;

    document.getElementById('searchKode').disabled = false;
    document.getElementById('searchNama').disabled = false;
    document.getElementById('searchbtnkode').disabled = false;
    document.getElementById('searchbtnnama').disabled = false;

    document.getElementById('dropdownKode').style.display = 'none';
    document.getElementById('dropdownNama').style.display = 'none';
  }

  function setEditModeButtons() {
    document.getElementById('btnTambah').disabled = true;
    document.getElementById('btnEdit').disabled = false;
    document.getElementById('btnHapus').disabled = false;
    document.getElementById('btnCancel').disabled = false;
    document.getElementById('searchKode').disabled = true;
    document.getElementById('searchNama').disabled = true;
    document.getElementById('searchbtnkode').disabled = true;
    document.getElementById('searchbtnnama').disabled = true;

    document.getElementById('dropdownKode').style.display = 'none';
    document.getElementById('dropdownNama').style.display = 'none';
  }

  initializeFormButtons();

    function cancelForm() {
    document.getElementById('grupForm').reset();             // Reset semua input
    document.getElementById('aksi').value = '';              // Kosongkan aksi
    document.getElementById('searchGrup').value = '';        // Kosongkan input search
    initializeFormButtons();                                 // Kembali ke mode default
  }

  function forceUppercase(id) {
    const input = document.getElementById(id);
    input.addEventListener('input', () => {
      input.value = input.value.toUpperCase();
    });
  }

  // Terapkan ke field kodegrup dan namagrup
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
            // Isi form input
            document.getElementById('kodegrup').value = grup.kodegrup;
            document.getElementById('namagrup').value = grup.namagrup;
            document.getElementById('dropdownGrup').style.display = 'none';
            document.getElementById('searchGrup').value = '';

            // Reset tombol mode Edit
            setEditModeButtons();

            // Sembunyikan dropdown dan kosongkan input search

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

  const popup = document.getElementById('popupNotif');
  if (popup) {
    popup.addEventListener('click', () => popup.style.display = 'none');
  }
</script>
<script src="notif.js"></script>
</body>
</html>
