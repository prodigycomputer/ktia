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
  <style>
    /* CSS yang sudah kamu buat (tidak berubah) */
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
      background-color: #007bff;
      color: white;
      border: none;
      cursor: pointer;
      transition: 0.3s ease;
      padding: 6px;
      font-size: 13px;
      border-radius: 4px;
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
    .form-atas input.verylong-input { width: 300px; }
    .form-atas button {
      background-color: #0acf45ff;
      color: white;
      border: none;
      cursor: pointer;
      padding: 5px;
      font-size: 12px;
    }
    .search-bar {
      display: flex;
      gap: 10px;
      margin-bottom: 12px;
      position: relative;
      flex-wrap: wrap;
    }
    .search-group {
      display: flex;
      flex-direction: column;
      gap: 5px;
      position: relative;
    }
    .search-group input {
      padding: 5px;
      font-size: 12px;
      border: 1px solid #ccc;
      border-radius: 4px;
    }
    .input-group {
      display: flex;
      position: relative;
      width: 250px;
      background: white;
      border-radius: 6px;
      box-shadow: 0 1px 3px rgba(0,0,0,0.1);
      overflow: hidden;
      border: 1px solid #ccc;
    }
    .input-group input {
      border: none;
      padding: 4px 6px;
      font-size: 12px;
      flex: 1;
      outline: none;
    }
    .input-group button {
      background-color: #007bff;
      border: none;
      color: white;
      padding: 0 12px;
      font-size: 14px;
      cursor: pointer;
      display: flex;
      align-items: center;
      justify-content: center;
      transition: background-color 0.2s ease-in-out;
    }
    .input-group button:hover {
      background-color: #0056b3;
    }
    .dropdown-result {
      position: absolute;
      top: 100%;
      left: 0;
      background: white;
      border: 1px solid #ccc;
      z-index: 100;
      max-height: 200px;
      overflow-y: auto;
      width: 100%;
      font-size: 13px;
      display: none;
      border-radius: 0 0 6px 6px;
      box-shadow: 0 2px 6px rgba(0,0,0,0.1);
    }
    .dropdown-result div {
      padding: 8px 10px;
      cursor: pointer;
      transition: background 0.2s;
    }
    .dropdown-result div:hover {
      background-color: #f5f5f5;
    }
    .popup {
      position: fixed;
      top: 20px;
      right: 20px;
      background: #4caf50;
      color: white;
      padding: 10px 16px;
      border-radius: 6px;
      box-shadow: 0 2px 6px rgba(0,0,0,0.15);
      z-index: 1000;
      animation: fadeOut 3s forwards;
      font-size: 13px;
    }
    .popup.error {
      background: #e74c3c;
    }
    @keyframes fadeOut {
      0%   { opacity: 1; }
      80%  { opacity: 1; }
      100% { opacity: 0; display: none; }
    }
    @media (max-width: 768px) {
      .form-atas {
        grid-template-columns: 1fr;
      }
    }
  </style>
</head>
<body>
<?php include 'navbar.php'; ?>
<main>
  <h2>Group Barang</h2>

  <div class="search-bar">
    <div class="search-group">
      <div class="input-group">
        <input type="text" id="searchGrup" placeholder="Cari kode/nama grup..." oninput="filterDropdown()" style="text-transform: uppercase;">
        <button type="button" id="searchbtn" onclick="triggerSearch()">🔍</button>
      </div>
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
    </div>
  </form>
</main>

<?php if (isset($_SESSION['notif'])): ?>
  <div id="popupNotif" class="popup <?= $_SESSION['notif']['type'] ?>">
    <?= $_SESSION['notif']['message'] ?>
  </div>
  <?php unset($_SESSION['notif']); ?>
<?php endif; ?>

<script>
  function initializeFormButtons() {
    document.getElementById('btnTambah').disabled = false;
    document.getElementById('btnEdit').disabled = true;
    document.getElementById('btnHapus').disabled = true;

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

    document.getElementById('searchKode').disabled = true;
    document.getElementById('searchNama').disabled = true;
    document.getElementById('searchbtnkode').disabled = true;
    document.getElementById('searchbtnnama').disabled = true;

    document.getElementById('dropdownKode').style.display = 'none';
    document.getElementById('dropdownNama').style.display = 'none';
  }

  initializeFormButtons();

  function triggerSearch() {
    const input = document.getElementById('searchGrup');
    input.focus();
    filterDropdown();
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
</body>
</html>
