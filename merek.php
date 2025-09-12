<?php
require_once 'init.php';
?>

<!DOCTYPE html>
<html lang="en">
<head>
  <meta charset="UTF-8" />
  <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
  <title>Data Merek</title>
  <link rel="stylesheet" href="navbar.css" />
  <link rel="stylesheet" href="form.css" />
  <script src="button.js"></script>
</head>
<body>
<?php 
    require_once 'akses.php';
?>
<main>
  <h2>Data Merek</h2>

  <div class="search-bar">
      <input type="text" id="searchKode" class="search-kode" placeholder="Kode Merek" oninput="handleInput('kode')" style="text-transform: uppercase;">
      <input type="text" id="searchNama" class="search-nama" placeholder="Nama Merek" oninput="handleInput('nama')" style="text-transform: uppercase;">
      <button type="button" id="searchbtn" onclick="triggerSearch()">🔍 Cari</button>
  </div>

  <form id="merekForm" action="prosesmerek.php" enctype="multipart/form-data" onsubmit="return false;">
    <input type="hidden" name="aksi" id="aksi" value="">
    <input type="hidden" name="kodemerek_lama" id="kodemerek_lama" value="">

    <div class="form-atas">
      <label for="kodemerek">Kode Merek</label>
      <input type="text" name="kodemerek" id="kodemerek" class="long-input" required style="text-transform: uppercase;">

      <label for="namamerek">Nama Merek</label>
      <input type="text" name="namamerek" id="namamerek" class="verylong-input" required style="text-transform: uppercase;">
    </div>
    <div style="margin-top:10px; display:flex; gap:8px;">
      <button id="btnSave" type="submit">Simpan</button>
      <button id="btnTambah" type="button" onclick="initializeTambah()">Tambah</button>
      <button id="btnEdit" type="button" onclick="initializeUbah()">Ubah</button>
      <button id="btnHapus" type="button" onclick="tampilkanKonfirmasiHapus()">Hapus</button>
      <button id="btnCancel" type="button" onclick="cancelForm()">Batal</button>
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

      function showToast(message, color = '#28a745') {
        const toast = document.getElementById('toast');
        toast.textContent = message;
        toast.style.backgroundColor = color;
        toast.style.display = 'block';
        setTimeout(() => toast.style.display = 'none', 2000);
      }

      initializeFormButtons({
            fields: ["kodemerek", "namamerek"]
      });

      function initializeFormButtonsCancel() {
          currentstat = null;

          document.getElementById('kodemerek').value = previousFormData.kodemerek;
          document.getElementById('namamerek').value = previousFormData.namamerek;
          document.getElementById('btnTambah').disabled = true;
          document.getElementById('btnEdit').disabled = false;
          document.getElementById('btnHapus').disabled = false;
          document.getElementById('btnCancel').disabled = false; 
          document.getElementById('btnSave').disabled = true;

          document.getElementById('kodemerek').disabled = true;
          document.getElementById('namamerek').disabled = true;

          document.getElementById('searchKode').value = '';
          document.getElementById('searchNama').value = '';
          document.getElementById('searchKode').disabled = false;
          document.getElementById('searchNama').disabled = false;
          document.getElementById('searchbtn').disabled = false;

        resetButtonStyles();

      }

      function initializeTambah() {
        currentstat = 'tambah';
        showToast('Kamu sedang menambah data...', '#ffc107');

        document.getElementById('merekForm').reset();

        document.getElementById('btnTambah').disabled = true;
        document.getElementById('btnEdit').disabled = true;
        document.getElementById('btnHapus').disabled = true;
        document.getElementById('btnCancel').disabled = false;
        document.getElementById('btnSave').disabled = false;

        document.getElementById('kodemerek').disabled = false;
        document.getElementById('kodemerek').readOnly = false;
        document.getElementById('namamerek').disabled = false;

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

        document.getElementById('kodemerek').disabled = false;
        document.getElementById('kodemerek').readOnly = false;
        document.getElementById('namamerek').disabled = false;

        document.getElementById('searchKode').disabled = true;
        document.getElementById('searchNama').disabled = true;
        document.getElementById('searchKode').value = '';
        document.getElementById('searchNama').value = '';
        document.getElementById('searchbtn').disabled = true;

        resetButtonStyles();
        setActiveButtonStyle(document.getElementById('btnEdit'));
      }



      function cancelForm() {
        // Reset search input
        const kodeInput = document.getElementById('searchKode');
        const namaInput = document.getElementById('searchNama');
        kodeInput.disabled = false;
        namaInput.disabled = false;
        kodeInput.value = '';
        namaInput.value = '';
        document.getElementById('kodemerek').readOnly = true;
        document.getElementById('searchbtn').disabled = false;

        if (currentstat === 'tambah' ) {
            initializeFormButtons();
            document.getElementById('merekForm').reset();
            currentstat = null;
        } else if (currentstat === 'update') {
          initializeFormButtonsCancel();
          currentstat = null;
        } else if (currentstat === null) {
          initializeFormButtons();
          document.getElementById('merekForm').reset();
        }
        resetButtonStyles();
      }

      let previousFormData = {};

      previousFormData = {
        kodemerek: document.getElementById('kodemerek').value,
        namamerek: document.getElementById('namamerek').value,
        kodemerek_lama: document.getElementById('kodemerek_lama').value
      };

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
      forceUppercase('kodemerek');
      forceUppercase('namamerek');

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

          const fixedFields = ['kodemerek', 'namamerek'];

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
                  pilihMerek(item);
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

          fetch(`filter_merek.php?keyword=${encodeURIComponent(keyword)}`)
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

      function pilihMerek(data) {
          document.getElementById('kodemerek').value = data.kodemerek;
          document.getElementById('kodemerek_lama').value = data.kodemerek; 
          document.getElementById('namamerek').value = data.namamerek;

          // Logika enable/disable berdasarkan isi1 dan isi2
          document.getElementById('btnTambah').disabled = true;
          document.getElementById('btnEdit').disabled = false;
          document.getElementById('btnHapus').disabled = false;
          document.getElementById('btnCancel').disabled = false; 

          previousFormData = {
            kodemerek: data.kodemerek,
            namamerek: data.namamerek,
            kodemerek_lama: data.kodemerek
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

      document.getElementById('merekForm').addEventListener('submit', function (e) {
        e.preventDefault();

        const formData = new FormData(this);
        formData.set('aksi', currentstat); // 'tambah', 'update', 'hapus'

        fetch('prosesmerek.php', {
          method: 'POST',
          body: formData
        })
        .then(res => res.json())
        .then(response => {
          if (response.status === 'success') {
            showToast(`Data berhasil ${response.aksi === 'tambah' ? 'ditambahkan' : response.aksi === 'update' ? 'diupdate' : 'dihapus'}`);

            if (currentstat === 'tambah' || currentstat === 'update') {
            previousFormData = {
              kodemerek: document.getElementById('kodemerek').value.trim(),
              namamerek: document.getElementById('namamerek').value.trim(),
              kodemerek_lama: document.getElementById('kodemerek').value.trim()
            };

            if (currentstat === 'tambah') {
              initializeFormButtons();
            } else {
              initializeFormButtonsCancel();
            }
          }
          

            // Kamu bisa isi ulang form dengan data sebelumnya kalau mau
          } else if (response.status === 'duplikat') {
            showToast('Kode merek sudah ada!', '#dc3545');
          } else {
            showToast('Gagal menyimpan data!', '#dc3545');
          }
        })
        .catch(err => {
          console.error(err);
          showToast('Gagal koneksi ke server!', '#dc3545');
        });
      });



      function setActiveButtonStyle(button) {
          button.style.backgroundColor = 'white';
          button.style.color = 'black';
          button.style.border = '1px solid #999';
      }

      function tampilkanKonfirmasiHapus() {
        if (!cekAkses('hapus')) return;
        document.getElementById('popupConfirmHapus').style.display = 'block';
      }

      function konfirmasiHapus(setuju) {
          const popup = document.getElementById('popupConfirmHapus');
          popup.style.display = 'none';

          if (setuju) {
              const formData = new FormData(document.getElementById('merekForm'));
              formData.set('aksi', 'hapus');

              fetch('prosesmerek.php', {
                  method: 'POST',
                  body: formData
              })
              .then(res => res.json())
              .then(response => {
                  if (response.status === 'success') {
                      showToast('Data berhasil dihapus');
                      initializeFormButtons(); // reset tampilan
                      document.getElementById('merekForm').reset();
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
