// button.js

let currentMode = ""; // untuk membedakan antara 'tambah' dan 'update'

function setInitialState() {
    disableFields(true);
    toggleButtons({ tambah: false, simpan: true, ubah: true, hapus: true, batal: true });
    setHargaButtonState(false);
    setReadonlyAll(true);
}

function onTambahClick() {
    currentMode = "tambah";
    disableFields(false);
    clearFields();
    disableSearch(true);
    toggleButtons({ tambah: true, simpan: false, ubah: true, hapus: true, batal: false });
    setHargaButtonState(true);
    setReadonlyAll(false);
}

function onEditClick() {
    currentMode = "update";
    disableFields(false);
    disableSearch(true);
    toggleButtons({ tambah: true, simpan: false, ubah: true, hapus: true, batal: false });
    setHargaButtonState(true);
    setReadonlyAll(false);
}

function onSimpanClick() {
    disableFields(true);
    disableSearch(false);
    toggleButtons({ tambah: false, simpan: true, ubah: true, hapus: true, batal: true });
    setHargaButtonState(false);
    setReadonlyAll(true);
}

function onCancelClick() {
    currentMode = "";
    disableFields(true);
    clearFields();
    disableSearch(false);
    toggleButtons({ tambah: false, simpan: true, ubah: true, hapus: true, batal: true });
    setHargaButtonState(false);
    setReadonlyAll(true);
}

function onItemSearchClick() {
    disableFields(true);
    disableSearch(true);
    toggleButtons({ tambah: true, simpan: true, ubah: false, hapus: false, batal: false });
    setHargaButtonState(true);
    setReadonlyAll(true);
}

function disableFields(disabled) {
    const ids = ['searchGrup', 'kodebrg', 'namabrg', 'satuan1', 'isi1'];
    ids.forEach(id => document.getElementById(id).disabled = disabled);
}

function disableSearch(disabled) {
    document.getElementById('searchKode').disabled = disabled;
    document.getElementById('searchNama').disabled = disabled;
    document.getElementById('searchbtn').disabled = disabled;
}

function toggleButtons({ tambah, simpan, ubah, hapus, batal }) {
    document.getElementById('btnTambah').disabled = tambah;
    document.getElementById('btnSave').disabled = simpan;
    document.getElementById('btnEdit').disabled = ubah;
    document.getElementById('btnHapus').disabled = hapus;
    document.getElementById('btnCancel').disabled = batal;
}

function setHargaButtonState(enable) {
    const hargaBtn = document.querySelector('button[onclick="openHargaPopup()"]');
    if (hargaBtn) hargaBtn.disabled = !enable;
}

function setReadonlyAll(readonly) {
    const fields = ['searchGrup', 'kodebrg', 'namabrg', 'satuan1', 'isi1', 'satuan2', 'isi2', 'satuan3'];
    fields.forEach(id => {
        const el = document.getElementById(id);
        if (el) {
            if (readonly) {
                el.setAttribute('readonly', true);
            } else {
                el.removeAttribute('readonly');
            }
        }
    });
}

function clearFields() {
    document.getElementById('barangForm').reset();
    document.getElementById('gambar').value = '';
    hargaData = {};
    generateHargaInputs();
}

function getCurrentMode() {
    return currentMode;
}
