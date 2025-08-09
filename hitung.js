// Hitung.js

function hitungJumlahPembelian() {
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

    let hasil1 = jlh1 * harga;
    let hasil2 = (jlh2 > 0 && isi1 > 0) ? (harga / isi1) * jlh2 : 0;
    let hasil3 = (jlh3 > 0 && isi1 > 0 && isi2 > 0) ? (harga / (isi1 * isi2)) * jlh3 : 0;

    let smntarajlmh = hasil1 + hasil2 + hasil3;

    let afterDisca = smntarajlmh * disca / 100;
    let smntaradis1 = smntarajlmh - afterDisca;
    popupHdiskon1.value = smntaradis1.toFixed(2);

    let afterDiscb = smntaradis1 * discb / 100;
    let smntaradis2 = smntaradis1 - afterDiscb;
    popupHdiskon2.value = smntaradis2.toFixed(2);

    let afterDiscc = smntaradis2 * discc / 100;
    let smntaradis3 = smntaradis2 - afterDiscc;
    popupHdiskon3.value = smntaradis3.toFixed(2);

    let finalJumlah = smntaradis3 - discrp;
    document.getElementById('popup_jumlah').value = Math.round(finalJumlah);
}

function hitungJumlahPembelianEdit() {
    let jlh1 = parseFloat(jlh1Edit.value) || 0;
    let jlh2 = parseFloat(jlh2Edit.value) || 0;
    let jlh3 = parseFloat(jlh3Edit.value) || 0;
    let harga = parseFloat(hargaEdit.value) || 0;

    let isi1 = parseFloat(isi1Edit.value) || 0;
    let isi2 = parseFloat(isi2Edit.value) || 0;

    let disca = parseFloat(discaEdit.value) || 0;
    let discb = parseFloat(discbEdit.value) || 0;
    let discc = parseFloat(disccEdit.value) || 0;
    let discrp = parseFloat(discrpEdit.value) || 0;

    let hasil1 = jlh1 * harga;
    let hasil2 = (jlh2 > 0 && isi1 > 0) ? (harga / isi1) * jlh2 : 0;
    let hasil3 = (jlh3 > 0 && isi1 > 0 && isi2 > 0) ? (harga / (isi1 * isi2)) * jlh3 : 0;

    let smntarajlmh = hasil1 + hasil2 + hasil3;

    let afterDisca = smntarajlmh * disca / 100;
    let smntaradis1 = smntarajlmh - afterDisca;

    let afterDiscb = smntaradis1 * discb / 100;
    let smntaradis2 = smntaradis1 - afterDiscb;

    let afterDiscc = smntaradis2 * discc / 100;
    let smntaradis3 = smntaradis2 - afterDiscc;

    let finalJumlah = smntaradis3 - discrp;
    jumlahEdit.value = Math.round(finalJumlah);

    hdiskon1Edit.value = smntaradis1.toFixed(2);
    hdiskon2Edit.value = smntaradis2.toFixed(2);
    hdiskon3Edit.value = smntaradis3.toFixed(2);

    if (indexEdit !== undefined && dataPembelian[indexEdit]) {
        dataPembelian[indexEdit].jumlah = Math.round(finalJumlah);
        dataPembelian[indexEdit].hdisca = Math.round(afterDisca);
        dataPembelian[indexEdit].hdiscb = Math.round(afterDiscb);
        dataPembelian[indexEdit].hdiscc = Math.round(afterDiscc);
    }

    hitungSubtotalDariArray();
}

// Event listeners
document.getElementById('diskon1').addEventListener('input', hitungSubtotalDariArray);
document.getElementById('diskon2').addEventListener('input', hitungSubtotalDariArray);
document.getElementById('diskon3').addEventListener('input', hitungSubtotalDariArray);
document.getElementById('ppn').addEventListener('input', hitungSubtotalDariArray);
document.getElementById('lain_lain').addEventListener('input', hitungSubtotalDariArray);

function loadPerhitungan() {
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

    document.getElementById('subtotal').value = subtotal.toFixed(2);
    document.getElementById('hdiskon1').value = hrgdc1.toFixed(2);
    document.getElementById('hdiskon2').value = hrgdc2.toFixed(2);
    document.getElementById('hdiskon3').value = hrgdc3.toFixed(2);
    document.getElementById('hppn').value = hrppn.toFixed(2);
    document.getElementById('lain_lain').value = lainlain.toFixed(2);
    document.getElementById('totaljmlh').value = totaljmlh.toFixed(2);
}

function hitungSubtotalDariArray() {
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

    let lainLain = parseFloat(document.getElementById('lain_lain').value) || 0;
    let totaljmlh = totalppn + lainLain;

    document.getElementById('subtotal').value = subtotal.toFixed(2);
    document.getElementById('hdiskon1').value = hrgdc1.toFixed(2);
    document.getElementById('hdiskon2').value = hrgdc2.toFixed(2);
    document.getElementById('hdiskon3').value = hrgdc3.toFixed(2);
    document.getElementById('hppn').value = hrppn.toFixed(2);
    document.getElementById('totaljmlh').value = totaljmlh.toFixed(2);
}
