// Hitung.js

function toNumberIDR(str) {
    if (!str) return 0;
    return parseFloat(str.replace(/\./g, '').replace(',', '.')) || 0;
}

function hitungJumlahPembelian(mode = "input") {
    // Ambil nilai dari form popup
    let jlh1 = parseFloat(document.getElementById('popup_jlh1').value) || 0;
    let jlh2 = parseFloat(document.getElementById('popup_jlh2').value) || 0;
    let jlh3 = parseFloat(document.getElementById('popup_jlh3').value) || 0;
    let harga = parseFloat(document.getElementById('popup_harga').value) || 0;

    let isi1 = parseFloat(document.getElementById('popup_isi1').value) || 0;
    let isi2 = parseFloat(document.getElementById('popup_isi2').value) || 0;

    let disca = parseFloat(document.getElementById('popup_disca').value) || 0;
    let discb = parseFloat(document.getElementById('popup_discb').value) || 0;
    let discc = parseFloat(document.getElementById('popup_discc').value) || 0;
    let discrp = parseFloat(document.getElementById('popup_discrp').value) || 0;

    // Perhitungan jumlah
    let hasil1 = jlh1 * harga;
    let hasil2 = (jlh2 > 0 && isi1 > 0) ? (harga / isi1) * jlh2 : 0;
    let hasil3 = (jlh3 > 0 && isi1 > 0 && isi2 > 0) ? (harga / (isi1 * isi2)) * jlh3 : 0;
    let smntarajlmh = hasil1 + hasil2 + hasil3;

    let afterDisca = smntarajlmh * disca / 100;
    let smntaradis1 = smntarajlmh - afterDisca;
    document.getElementById('popup_hdiskon1').value = smntaradis1.toFixed(2);

    let afterDiscb = smntaradis1 * discb / 100;
    let smntaradis2 = smntaradis1 - afterDiscb;
    document.getElementById('popup_hdiskon2').value = smntaradis2.toFixed(2);

    let afterDiscc = smntaradis2 * discc / 100;
    let smntaradis3 = smntaradis2 - afterDiscc;
    document.getElementById('popup_hdiskon3').value = smntaradis3.toFixed(2);

    let finalJumlah = smntaradis3 - discrp;
    document.getElementById('popup_jumlah').value = Math.round(finalJumlah);

    // Jika sedang mode edit → update data array + subtotal
    if (mode === "edit" && indexEdit !== undefined && dataPembelian[indexEdit]) {
        dataPembelian[indexEdit].jumlah = Math.round(finalJumlah);
        dataPembelian[indexEdit].hdisca = Math.round(afterDisca);
        dataPembelian[indexEdit].hdiscb = Math.round(afterDiscb);
        dataPembelian[indexEdit].hdiscc = Math.round(afterDiscc);

        hitungSubtotalDariArrayBeli();
    }
}

function hitungSubtotalDariArrayBeli() {
    let subtotal = 0;
    let dc1 = parseFloat(document.getElementById('diskon1').value) || 0;
    let dc2 = parseFloat(document.getElementById('diskon2').value) || 0;
    let dc3 = parseFloat(document.getElementById('diskon3').value) || 0;
    let persenppn = parseFloat(document.getElementById('ppn').value) || 0;
    let lainLain = toNumberIDR(document.getElementById('lain_lain').value) || 0;

    dataPembelian.forEach(item => {
        subtotal += parseFloat(item.jumlah) || 0;
    });

    let hrgdc1 = subtotal * dc1 / 100;
    let smntarahrgdc1 = subtotal - hrgdc1;
    let hrgdc2 = smntarahrgdc1 * dc2 / 100;
    let smntarahrgdc2 = smntarahrgdc1 - hrgdc2;
    let hrgdc3 = smntarahrgdc2 * dc3 / 100;
    let smntarahrgdc3 = smntarahrgdc2 - hrgdc3;
    let hrppn = smntarahrgdc3 * persenppn / 100;
    let totalppn = smntarahrgdc3 + hrppn;

    
    let totaljmlh = totalppn + lainLain;

    document.getElementById('subtotal').value = parseFloat(subtotal).toLocaleString('id-ID', { minimumFractionDigits: 2, maximumFractionDigits: 2 });
    document.getElementById('hdiskon1').value = parseFloat(hrgdc1).toLocaleString('id-ID', { minimumFractionDigits: 2, maximumFractionDigits: 2 });
    document.getElementById('hdiskon2').value = parseFloat(hrgdc2).toLocaleString('id-ID', { minimumFractionDigits: 2, maximumFractionDigits: 2 });
    document.getElementById('hdiskon3').value = parseFloat(hrgdc3).toLocaleString('id-ID', { minimumFractionDigits: 2, maximumFractionDigits: 2 });
    document.getElementById('hppn').value = parseFloat(hrppn).toLocaleString('id-ID', { minimumFractionDigits: 2, maximumFractionDigits: 2 });
    document.getElementById('totaljmlh').value = parseFloat(totaljmlh).toLocaleString('id-ID', { minimumFractionDigits: 2, maximumFractionDigits: 2 });
}

// Event listeners

function loadPerhitunganBeli() {
    let subtotal = 0;
    let dc1 = parseFloat(document.getElementById('diskon1').value) || 0;
    let dc2 = parseFloat(document.getElementById('diskon2').value) || 0;
    let dc3 = parseFloat(document.getElementById('diskon3').value) || 0;
    let persenppn = parseFloat(document.getElementById('ppn').value) || 0;

    dataPembelian.forEach(item => {
        subtotal += parseFloat(item.jumlah) || 0;
    });

    let hrgdc1 = subtotal * dc1 / 100;
    let smntarahrgdc1 = subtotal - hrgdc1;
    let hrgdc2 = smntarahrgdc1 * dc2 / 100;
    let smntarahrgdc2 = smntarahrgdc1 - hrgdc2;
    let hrgdc3 = smntarahrgdc2 * dc3 / 100;
    let smntarahrgdc3 = smntarahrgdc2 - hrgdc3;
    let hrppn = smntarahrgdc3 * persenppn / 100;
    let totalppn = smntarahrgdc3 + hrppn;

    let totaljmlh = parseFloat(document.getElementById('totaljmlh').value) || 0;
    let lainlain = totaljmlh - totalppn;

    document.getElementById('subtotal').value = parseFloat(subtotal).toLocaleString('id-ID', { minimumFractionDigits: 2, maximumFractionDigits: 2 });
    document.getElementById('hdiskon1').value = parseFloat(hrgdc1).toLocaleString('id-ID', { minimumFractionDigits: 2, maximumFractionDigits: 2 });
    document.getElementById('hdiskon2').value = parseFloat(hrgdc2).toLocaleString('id-ID', { minimumFractionDigits: 2, maximumFractionDigits: 2 });
    document.getElementById('hdiskon3').value = parseFloat(hrgdc3).toLocaleString('id-ID', { minimumFractionDigits: 2, maximumFractionDigits: 2 });
    document.getElementById('hppn').value = parseFloat(hrppn).toLocaleString('id-ID', { minimumFractionDigits: 2, maximumFractionDigits: 2 });
    document.getElementById('lain_lain').value = parseFloat(lainlain).toLocaleString('id-ID', { minimumFractionDigits: 2, maximumFractionDigits: 2 });
    document.getElementById('totaljmlh').value = parseFloat(totaljmlh).toLocaleString('id-ID', { minimumFractionDigits: 2, maximumFractionDigits: 2 });
}

function hitungJumlahPenjualan(mode = "input") {
    // Ambil nilai dari form popup
    let jlh1 = parseFloat(document.getElementById('popup_jlh1').value) || 0;
    let jlh2 = parseFloat(document.getElementById('popup_jlh2').value) || 0;
    let jlh3 = parseFloat(document.getElementById('popup_jlh3').value) || 0;
    let harga = parseFloat(document.getElementById('popup_harga').value) || 0;

    let isi1 = parseFloat(document.getElementById('popup_isi1').value) || 0;
    let isi2 = parseFloat(document.getElementById('popup_isi2').value) || 0;

    let disca = parseFloat(document.getElementById('popup_disca').value) || 0;
    let discb = parseFloat(document.getElementById('popup_discb').value) || 0;
    let discc = parseFloat(document.getElementById('popup_discc').value) || 0;
    let discrp = parseFloat(document.getElementById('popup_discrp').value) || 0;

    // Perhitungan jumlah
    let hasil1 = jlh1 * harga;
    let hasil2 = (jlh2 > 0 && isi1 > 0) ? (harga / isi1) * jlh2 : 0;
    let hasil3 = (jlh3 > 0 && isi1 > 0 && isi2 > 0) ? (harga / (isi1 * isi2)) * jlh3 : 0;
    let smntarajlmh = hasil1 + hasil2 + hasil3;

    let afterDisca = smntarajlmh * disca / 100;
    let smntaradis1 = smntarajlmh - afterDisca;
    document.getElementById('popup_hdiskon1').value = smntaradis1.toFixed(2);

    let afterDiscb = smntaradis1 * discb / 100;
    let smntaradis2 = smntaradis1 - afterDiscb;
    document.getElementById('popup_hdiskon2').value = smntaradis2.toFixed(2);

    let afterDiscc = smntaradis2 * discc / 100;
    let smntaradis3 = smntaradis2 - afterDiscc;
    document.getElementById('popup_hdiskon3').value = smntaradis3.toFixed(2);

    let finalJumlah = smntaradis3 - discrp;
    document.getElementById('popup_jumlah').value = Math.round(finalJumlah);

    // Jika sedang mode edit → update data array + subtotal
    if (mode === "edit" && indexEdit !== undefined && dataPenjualan[indexEdit]) {
        dataPenjualan[indexEdit].jumlah = Math.round(finalJumlah);
        dataPenjualan[indexEdit].hdisca = Math.round(afterDisca);
        dataPenjualan[indexEdit].hdiscb = Math.round(afterDiscb);
        dataPenjualan[indexEdit].hdiscc = Math.round(afterDiscc);

        hitungSubtotalDariArrayJual();
    }
}

function hitungSubtotalDariArrayJual() {
    let subtotal = 0;
    let dc1 = parseFloat(document.getElementById('diskon1').value) || 0;
    let dc2 = parseFloat(document.getElementById('diskon2').value) || 0;
    let dc3 = parseFloat(document.getElementById('diskon3').value) || 0;
    let persenppn = parseFloat(document.getElementById('ppn').value) || 0;
    let lainLain = toNumberIDR(document.getElementById('lain_lain').value) || 0;

    dataPenjualan.forEach(item => {
        subtotal += parseFloat(item.jumlah) || 0;
    });

    let hrgdc1 = subtotal * dc1 / 100;
    let smntarahrgdc1 = subtotal - hrgdc1;
    let hrgdc2 = smntarahrgdc1 * dc2 / 100;
    let smntarahrgdc2 = smntarahrgdc1 - hrgdc2;
    let hrgdc3 = smntarahrgdc2 * dc3 / 100;
    let smntarahrgdc3 = smntarahrgdc2 - hrgdc3;
    let hrppn = smntarahrgdc3 * persenppn / 100;
    let totalppn = smntarahrgdc3 + hrppn;
    let totaljmlh = totalppn + lainLain;

    document.getElementById('subtotal').value = parseFloat(subtotal).toLocaleString('id-ID', { minimumFractionDigits: 2, maximumFractionDigits: 2 });
    document.getElementById('hdiskon1').value = parseFloat(hrgdc1).toLocaleString('id-ID', { minimumFractionDigits: 2, maximumFractionDigits: 2 });
    document.getElementById('hdiskon2').value = parseFloat(hrgdc2).toLocaleString('id-ID', { minimumFractionDigits: 2, maximumFractionDigits: 2 });
    document.getElementById('hdiskon3').value = parseFloat(hrgdc3).toLocaleString('id-ID', { minimumFractionDigits: 2, maximumFractionDigits: 2 });
    document.getElementById('hppn').value = parseFloat(hrppn).toLocaleString('id-ID', { minimumFractionDigits: 2, maximumFractionDigits: 2 });
    document.getElementById('totaljmlh').value = parseFloat(totaljmlh).toLocaleString('id-ID', { minimumFractionDigits: 2, maximumFractionDigits: 2 });
}

// Event listeners

function loadPerhitunganJual() {
    let subtotal = 0;
    let dc1 = parseFloat(document.getElementById('diskon1').value) || 0;
    let dc2 = parseFloat(document.getElementById('diskon2').value) || 0;
    let dc3 = parseFloat(document.getElementById('diskon3').value) || 0;
    let persenppn = parseFloat(document.getElementById('ppn').value) || 0;

    dataPenjualan.forEach(item => {
        subtotal += parseFloat(item.jumlah) || 0;
    });

    let hrgdc1 = subtotal * dc1 / 100;
    let smntarahrgdc1 = subtotal - hrgdc1;
    let hrgdc2 = smntarahrgdc1 * dc2 / 100;
    let smntarahrgdc2 = smntarahrgdc1 - hrgdc2;
    let hrgdc3 = smntarahrgdc2 * dc3 / 100;
    let smntarahrgdc3 = smntarahrgdc2 - hrgdc3;
    let hrppn = smntarahrgdc3 * persenppn / 100;
    let totalppn = smntarahrgdc3 + hrppn;

    let totaljmlh = parseFloat(document.getElementById('totaljmlh').value) || 0;
    let lainlain = totaljmlh - totalppn;

    document.getElementById('subtotal').value = parseFloat(subtotal).toLocaleString('id-ID', { minimumFractionDigits: 2, maximumFractionDigits: 2 });
    document.getElementById('hdiskon1').value = parseFloat(hrgdc1).toLocaleString('id-ID', { minimumFractionDigits: 2, maximumFractionDigits: 2 });
    document.getElementById('hdiskon2').value = parseFloat(hrgdc2).toLocaleString('id-ID', { minimumFractionDigits: 2, maximumFractionDigits: 2 });
    document.getElementById('hdiskon3').value = parseFloat(hrgdc3).toLocaleString('id-ID', { minimumFractionDigits: 2, maximumFractionDigits: 2 });
    document.getElementById('hppn').value = parseFloat(hrppn).toLocaleString('id-ID', { minimumFractionDigits: 2, maximumFractionDigits: 2 });
    document.getElementById('lain_lain').value = parseFloat(lainlain).toLocaleString('id-ID', { minimumFractionDigits: 2, maximumFractionDigits: 2 });
    document.getElementById('totaljmlh').value = parseFloat(totaljmlh).toLocaleString('id-ID', { minimumFractionDigits: 2, maximumFractionDigits: 2 });
}

let sisaData = {
    sisa1: 0, sisa2: 0, sisa3: 0,
    satuan1: '', satuan2: '', satuan3: ''
};

// Ambil data sisa dari server
async function getSisa() {
    const kodegd = document.getElementById('popup_kodegd').value;
    const kodebrg = document.getElementById('popup_kodebrg').value;

    if (!kodegd || !kodebrg) return;

    try {
        const response = await fetch(`get_sisa.php?kodegd=${kodegd}&kodebrg=${kodebrg}`);
        sisaData = await response.json();

        console.log("Data sisa dari server:", sisaData);

        // Tampilkan ke input popup_sisa1/2/3
        document.getElementById('popup_sisa1').value = sisaData.sisa1 || 0;
        document.getElementById('popup_sisa2').value = sisaData.sisa2 || 0;
        document.getElementById('popup_sisa3').value = sisaData.sisa3 || 0;

        // Update ringkasan & cek stok habis
        updatePopupSisa();
    } catch (error) {
        console.error('Error ambil data sisa:', error);
    }
}

function updatePopupSisa() {
    const sisa1 = parseFloat(document.getElementById('popup_sisa1').value) || 0;
    const sisa2 = parseFloat(document.getElementById('popup_sisa2').value) || 0;
    const sisa3 = parseFloat(document.getElementById('popup_sisa3').value) || 0;

    const jlh1 = parseFloat(document.getElementById('popup_jlh1').value) || 0; 
    const jlh2 = parseFloat(document.getElementById('popup_jlh2').value) || 0; 
    const jlh3 = parseFloat(document.getElementById('popup_jlh3').value) || 0;

    let sisaText = [];

    if (jlh1 > 0) sisaText.push(`${sisa1} ${sisaData.satuan1}`);
    if (jlh2 > 0) sisaText.push(`${sisa2} ${sisaData.satuan2}`);
    if (jlh3 > 0) sisaText.push(`${sisa3} ${sisaData.satuan3}`);

    // Jika ada stok kosong → tampilkan notifikasi
    if (sisa1 === 0 || sisa2 === 0 || sisa3 === 0) {
        showToast("Stock Habis");
    }

    // Ringkasan di popup_sisa
    document.getElementById('popup_sisa').value = sisaText.join(', ');
}

// Contoh fungsi showToast
function showToast(pesan) {
    alert(pesan); // Bisa diganti SweetAlert/Bootstrap toast
}







