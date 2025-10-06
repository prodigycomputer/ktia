<?php
require_once 'init.php';

?>

<!DOCTYPE html>
<html lang="en">
<head>
  <meta charset="UTF-8" />
  <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
  <title>Regis Akun</title>
  <link rel="stylesheet" href="navbar.css" />
  <link rel="stylesheet" href="form.css" />
  <script src="button.js"></script>
</head>
<body>
<?php 
    require_once 'akses.php';
?>
<main>
  <h2>Regis Akun</h2>

  <div class="search-bar">
      <input type="text" id="searchKode" class="search-kode" placeholder="Kode User" oninput="handleInput('kode')" style="text-transform: uppercase;">
      <input type="text" id="searchNama" class="search-nama" placeholder="Nama User" oninput="handleInput('nama')" style="text-transform: uppercase;">
      <button type="button" id="searchbtn" onclick="triggerSearch()">🔍 Cari</button>
  </div>

  <form id="userForm" action="prosesuser.php" enctype="multipart/form-data" onsubmit="return false;">
    <input type="hidden" name="aksi" id="aksi" value="">
    <input type="hidden" name="kodeuser_lama" id="kodeuser_lama" value="">

    <div class="form-atas">
      <label for="kodeuser">Kode User</label>
      <input type="text" name="kodeuser" id="kodeuser" class="long-input" required style="text-transform: uppercase;">

      <label for="namauser">Nama User</label>
      <input type="text" name="namauser" id="namauser" class="verylong-input" required style="text-transform: uppercase;">

      <label for="passworduser">Password</label>
      <input type="text" name="passworduser" id="passworduser" class="verylong-input" required style="text-transform: uppercase;">

      <label for="konfirmpass">Konfirm Password</label>
      <input type="text" name="konfirmpass" id="konfirmpass" class="verylong-input" required style="text-transform: uppercase;">
    </div>
    <div style="margin-top:10px; display:flex; gap:8px;">
      <button id="btnSave" type="submit">Simpan</button>
      <button id="btnTambah" type="button" onclick="initializeTambah()">Tambah</button>
      <button id="btnEdit" type="button" onclick="initializeUbah()">Ubah</button>
      <button id="btnHapus" type="button" onclick="tampilkanKonfirmasiHapus()">Hapus</button>
      <button id="btnCancel" type="button" onclick="cancelForm()">Batal</button>
      <button id="btnAkses" type="button">Akses</button>
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

<!-- Popup Hak Akses -->
<div id="popupAkses" style="
    display: none;
    position: fixed; top: 0; left: 0;
    width: 100%; height: 100%;
    background: rgba(0,0,0,0.4);
    z-index: 1002;
">
    <div style="
        position: absolute;
        top: 50%; left: 50%;
        transform: translate(-50%, -50%);
        background: white;
        padding: 20px;
        border-radius: 8px;
        box-shadow: 0 4px 15px rgba(0,0,0,0.3);
        width: 90%;
        max-width: 850px;
        max-height: 80vh;
        overflow: auto;
    ">
        <div style="display:flex;justify-content:space-between;align-items:center;">
            <h3>Atur Hak Akses</h3>
            <span style="cursor:pointer;font-size:18px;font-weight:bold;color:#666;" onclick="closeAksesPopup()">&times;</span>
        </div>
        <div style="margin-top:15px;text-align:left;">
            <button onclick="saveAkses()" style="padding:6px 14px;background:#007bff;color:white;border:none;border-radius:4px;">Simpan</button>
        </div>
        <table style="width:100%;border-collapse:collapse;margin-top:10px;font-size:13px;">
            <thead style="background:#f2f2f2;">
                <tr>
                    <th style="border:1px solid #ddd;padding:5px;">Nama Form</th>
                    <th style="border:1px solid #ddd;padding:5px;">Tambah</th>
                    <th style="border:1px solid #ddd;padding:5px;">Ubah</th>
                    <th style="border:1px solid #ddd;padding:5px;">Hapus</th>
                </tr>
            </thead>
            <tbody id="aksesList"></tbody>
        </table>
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
let tempAkses = [];

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

function saveAkses() {
    const data = [];
    document.querySelectorAll('#aksesList tr').forEach(tr => {
        const idmenu = tr.querySelector('.tambah').dataset.id;
        data.push({
            idmenu,
            tambah: tr.querySelector('.tambah').checked ? 1 : 0,
            ubah: tr.querySelector('.ubah').checked ? 1 : 0,
            hapus: tr.querySelector('.hapus').checked ? 1 : 0
        });
    });

    tempAkses = data; // simpan sementara
    showToast('Akses tersimpan sementara. Klik Simpan di form utama untuk final.', '#17a2b8');
    closeAksesPopup();
}

function showToast(message, color = '#28a745') {
  const toast = document.getElementById('toast');
  toast.textContent = message;
  toast.style.backgroundColor = color;
  toast.style.display = 'block';
  setTimeout(() => toast.style.display = 'none', 2000);
}

initializeFormButtons({
    fields: ["kodeuser", "namauser", "passworduser", "konfirmpass"]
});

function initializeFormButtons() {
  currentstat = null;

  document.getElementById('btnTambah').disabled = false;
  document.getElementById('btnEdit').disabled = true;
  document.getElementById('btnHapus').disabled = true;
  document.getElementById('btnCancel').disabled = true;
  document.getElementById('btnSave').disabled = true;
  document.getElementById('btnAkses').disabled = true;

  document.getElementById('kodeuser').disabled = true;
  document.getElementById('namauser').disabled = true;
  document.getElementById('passworduser').disabled = true;
  document.getElementById('konfirmpass').disabled = true;
  

  document.getElementById('searchKode').value = '';
  document.getElementById('searchNama').value = '';
  document.getElementById('searchKode').disabled = false;
  document.getElementById('searchNama').disabled = false;
  document.getElementById('searchbtn').disabled = false;

  resetButtonStyles();

}
initializeFormButtons();

function initializeFormButtonsCancel() {
    currentstat = null;

    document.getElementById('kodeuser').value = previousFormData.kodeuser;
    document.getElementById('namauser').value = previousFormData.namauser;
    document.getElementById('passworduser').value = previousFormData.passworduser;
    document.getElementById('konfirmpass').value = previousFormData.konfirmpass;
    document.getElementById('btnTambah').disabled = true;
    document.getElementById('btnEdit').disabled = false;
    document.getElementById('btnHapus').disabled = false;
    document.getElementById('btnCancel').disabled = false; 
    document.getElementById('btnSave').disabled = true;
    document.getElementById('btnAkses').disabled = true;


    document.getElementById('kodeuser').disabled = true;
    document.getElementById('namauser').disabled = true;
    document.getElementById('passworduser').disabled = true;
    document.getElementById('konfirmpass').disabled = true;

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

  document.getElementById('userForm').reset();

  document.getElementById('btnTambah').disabled = true;
  document.getElementById('btnEdit').disabled = true;
  document.getElementById('btnHapus').disabled = true;
  document.getElementById('btnCancel').disabled = false;
  document.getElementById('btnSave').disabled = false;
  document.getElementById('btnAkses').disabled = false;


  document.getElementById('kodeuser').disabled = false;
  document.getElementById('kodeuser').readOnly = false;
  document.getElementById('namauser').disabled = false;
  document.getElementById('passworduser').disabled = false;
  document.getElementById('konfirmpass').disabled = false;

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
  document.getElementById('btnAkses').disabled = false;


  document.getElementById('kodeuser').disabled = false;
  document.getElementById('kodeuser').readOnly = false;
  document.getElementById('namauser').disabled = false;
  document.getElementById('passworduser').disabled = false;
  document.getElementById('konfirmpass').disabled = false;

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
  document.getElementById('kodeuser').readOnly = true;
  document.getElementById('searchbtn').disabled = false;

  if (currentstat === 'tambah' ) {
      initializeFormButtons();
      document.getElementById('userForm').reset();
      currentstat = null;
  } else if (currentstat === 'update') {
    initializeFormButtonsCancel();
    currentstat = null;
  } else if (currentstat === null) {
    initializeFormButtons();
    document.getElementById('userForm').reset();
  }
  resetButtonStyles();
}

let previousFormData = {};

previousFormData = {
  kodeuser: document.getElementById('kodeuser').value,
  namauser: document.getElementById('namauser').value,
  kodeuser_lama: document.getElementById('kodeuser_lama').value,
  passworduser: document.getElementById('passworduser').value,
  konfirmpass: document.getElementById('konfirmpass').value
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
forceUppercase('kodeuser');
forceUppercase('namauser');
forceUppercase('passworduser');
forceUppercase('konfirmpass');

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
    document.getElementById('kodeuser_lama').value = data.kodeuser; 
    document.getElementById('namauser').value = data.namauser;
    document.getElementById('passworduser').value = data.passworduser;
    document.getElementById('konfirmpass').value = data.passworduser;
    // Logika enable/disable berdasarkan isi1 dan isi2
    document.getElementById('btnTambah').disabled = true;   
    document.getElementById('btnEdit').disabled = false;
    document.getElementById('btnHapus').disabled = false;
    document.getElementById('btnCancel').disabled = false; 

    previousFormData = {
      kodeuser: data.kodeuser,
      namauser: data.namauser,
      kodeuser_lama: data.kodeuser,
      passworduser: data.passworduser,
      konfirmpass: data.passworduser
    };

    document.getElementById('searchKode').value = '';
    document.getElementById('searchNama').value = '';
    document.getElementById('searchKode').disabled = false;
    document.getElementById('searchNama').disabled = false;
    document.getElementById('searchbtn').disabled = false;
    inputSearch = null;
    closeFilterPopup();


}

document.getElementById('userForm').addEventListener('submit', function (e) {
  e.preventDefault();

  const password = document.getElementById('passworduser').value.trim();
  const confirm = document.getElementById('konfirmpass').value.trim();

  if (password !== confirm) {
    showToast('Konfirmasi password tidak sama!', '#dc3545');
    document.getElementById('konfirmpass').focus();
    return;
  }

  const formData = new FormData(this);
  formData.set('aksi', currentstat);
  
  // kirim akses ke server sebagai JSON
  formData.append('akses', JSON.stringify(tempAkses));

  fetch('prosesuser.php', {
    method: 'POST',
    body: formData
  })
  .then(res => res.json())
  .then(response => {
    if (response.status === 'success') {
      showToast(`Data berhasil ${response.aksi === 'tambah' ? 'ditambahkan' : response.aksi === 'update' ? 'diupdate' : 'dihapus'}`);
      if (currentstat === 'tambah') {
        initializeFormButtons();
      } else {
        initializeFormButtonsCancel();
      }
      tempAkses = []; // reset setelah tersimpan
    } else {
      showToast('Gagal menyimpan data!', '#dc3545');
    }
  })
  .catch(() => showToast('Gagal koneksi ke server!', '#dc3545'));
});

document.getElementById('btnAkses').addEventListener('click', function () {
    const kode = document.getElementById('kodeuser').value.trim();
    if (!kode) {
        showToast('Pilih user dulu!', '#dc3545');
        return;
    }

    fetch(`get_akses.php?kodeuser=${kode}`)
        .then(res => res.json())
        .then(data => {
            const tbody = document.getElementById('aksesList');
            tbody.innerHTML = '';
            data.forEach(row => {
                const tr = document.createElement('tr');
                tr.innerHTML = `
                    <td style="border:1px solid #ddd;padding:5px;">${row.submenu}</td>
                    <td style="border:1px solid #ddd;padding:5px;text-align:center;">
                        <input type="checkbox" class="tambah" data-id="${row.idmenu}" ${row.tambah==1?'checked':''}>
                    </td>
                    <td style="border:1px solid #ddd;padding:5px;text-align:center;">
                        <input type="checkbox" class="ubah" data-id="${row.idmenu}" ${row.ubah==1?'checked':''}>
                    </td>
                    <td style="border:1px solid #ddd;padding:5px;text-align:center;">
                        <input type="checkbox" class="hapus" data-id="${row.idmenu}" ${row.hapus==1?'checked':''}>
                    </td>
                `;
                tbody.appendChild(tr);
            });

            // Aturan: jika tambah=0, ubah+hapus ikut 0
            tbody.querySelectorAll('.tambah').forEach(chk => {
                chk.addEventListener('change', function() {
                    const id = this.dataset.id;
                    const ubah = document.querySelector(`.ubah[data-id="${id}"]`);
                    const hapus = document.querySelector(`.hapus[data-id="${id}"]`);
                    if (!this.checked) {
                        ubah.checked = false;
                        hapus.checked = false;
                    }
                });
            });

            // Jika ubah=0, hapus bebas
            // Jika hapus=0, ubah bebas -> tidak ada aturan tambahan
            document.getElementById('popupAkses').style.display = 'block';
        });
});

function closeAksesPopup() {
    document.getElementById('popupAkses').style.display = 'none';
}


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
        const formData = new FormData(document.getElementById('userForm'));
        formData.set('aksi', 'hapus');
        formData.set('kodeuser_lama', document.getElementById('kodeuser_lama').value);

        fetch('prosesuser.php', {
            method: 'POST',
            body: formData
        })
        .then(res => res.json())
        .then(response => {
            if (response.status === 'success') {
                showToast('Data berhasil dihapus');
                initializeFormButtons(); // reset tampilan
                document.getElementById('userForm').reset();
            } else {
                console.log(formData);
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
