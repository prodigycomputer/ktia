function setInitialState() {
    const form = document.forms['barangForm'];
    ['searchGrup', 'kodebrg', 'namabrg', 'satuan1', 'satuan2', 'satuan3', 'isi1', 'isi2'].forEach(id => {
        if (form[id]) {
            form[id].disabled = true;
            form[id].readOnly = false;
            form[id].value = '';
        }
    });

    toggleButtons({ simpan: false, ubah: false, hapus: false, batal: false, tambah: true });

    document.getElementById('searchKode').disabled = false;
    document.getElementById('searchNama').disabled = false;
    document.getElementById('searchbtn').disabled = false;

    const hargaBtn = document.querySelector('button[onclick="openHargaPopup()"]');
    if (hargaBtn) hargaBtn.disabled = true;
}

function onTambahClick() {
    const form = document.forms['barangForm'];
    ['searchGrup', 'kodebrg', 'namabrg', 'satuan1', 'satuan2', 'satuan3', 'isi1', 'isi2'].forEach(id => {
        if (form[id]) {
            form[id].disabled = false;
            form[id].readOnly = false;
            form[id].value = '';
        }
    });

    document.getElementById('searchKode').disabled = true;
    document.getElementById('searchNama').disabled = true;
    document.getElementById('searchbtn').disabled = true;

    toggleButtons({ simpan: true, batal: true, tambah: false, ubah: false, hapus: false });

    const hargaBtn = document.querySelector('button[onclick="openHargaPopup()"]');
    if (hargaBtn) hargaBtn.disabled = false;
}

function onSearchSelectState() {
    const form = document.forms['barangForm'];
    [...form.elements].forEach(el => {
        if (el.tagName === "INPUT") {
            el.readOnly = true;
            el.disabled = false;
        }
    });

    toggleButtons({ simpan: false, tambah: false, ubah: true, hapus: true, batal: true });

    const hargaBtn = document.querySelector('button[onclick="openHargaPopup()"]');
    if (hargaBtn) hargaBtn.disabled = false;
}

function onEditClick() {
    const form = document.forms['barangForm'];
    [...form.elements].forEach(el => {
        if (el.tagName === "INPUT" && el.id !== 'kodebrg') {
            el.readOnly = false;
            el.disabled = false;
        }
    });

    document.getElementById('searchKode').disabled = true;
    document.getElementById('searchNama').disabled = true;
    document.getElementById('searchbtn').disabled = true;

    toggleButtons({ simpan: true, ubah: false, hapus: false, batal: true });

    const hargaBtn = document.querySelector('button[onclick="openHargaPopup()"]');
    if (hargaBtn) hargaBtn.disabled = false;
}

function onSimpanSuccess() {
    const form = document.forms['barangForm'];
    form.reset();
    ['searchGrup', 'kodebrg', 'namabrg', 'satuan1', 'satuan2', 'satuan3', 'isi1', 'isi2'].forEach(id => {
        if (form[id]) {
            form[id].disabled = true;
            form[id].readOnly = false;
        }
    });

    document.getElementById('searchKode').disabled = false;
    document.getElementById('searchNama').disabled = false;
    document.getElementById('searchbtn').disabled = false;

    toggleButtons({ simpan: false, batal: false, ubah: false, hapus: false, tambah: true });

    const hargaBtn = document.querySelector('button[onclick="openHargaPopup()"]');
    if (hargaBtn) hargaBtn.disabled = true;
}

function onBatalClick() {
    const form = document.forms['barangForm'];
    form.reset();
    ['searchGrup', 'kodebrg', 'namabrg', 'satuan1', 'satuan2', 'satuan3', 'isi1', 'isi2'].forEach(id => {
        if (form[id]) {
            form[id].disabled = true;
            form[id].readOnly = false;
        }
    });

    document.getElementById('searchKode').disabled = false;
    document.getElementById('searchNama').disabled = false;
    document.getElementById('searchbtn').disabled = false;

    toggleButtons({ simpan: false, batal: false, ubah: false, hapus: false, tambah: true });

    const hargaBtn = document.querySelector('button[onclick="openHargaPopup()"]');
    if (hargaBtn) hargaBtn.disabled = true;
}

function toggleButtons(state) {
    document.getElementById('btnSave').style.display = state.simpan ? 'inline-block' : 'none';
    document.getElementById('btnEdit').style.display = state.ubah ? 'inline-block' : 'none';
    document.getElementById('btnHapus').style.display = state.hapus ? 'inline-block' : 'none';
    document.getElementById('btnBatal').style.display = state.batal ? 'inline-block' : 'none';
    document.getElementById('btnTambah').style.display = state.tambah ? 'inline-block' : 'none';
}

window.addEventListener('DOMContentLoaded', setInitialState);
