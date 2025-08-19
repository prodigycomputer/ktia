// === Helper untuk format modulus ===
function formatModulus(num, suffix = '') {
    const angka = parseFloat(num) || 0;
    if (angka === 0) return '';

    if (angka % 1 === 0) {
        // bulat → tanpa desimal
        return angka.toLocaleString('id-ID', { minimumFractionDigits: 0, maximumFractionDigits: 0 }) + suffix;
    } else {
        // ada pecahan → 2 desimal
        return angka.toLocaleString('id-ID', { minimumFractionDigits: 2, maximumFractionDigits: 2 }) + suffix;
    }
}

// === QTY ===
function tampilKuantitas(item) {
    let para_qty = '';

    const para_jlh1 = parseFloat(item.jlh1) || 0;
    const para_jlh2 = parseFloat(item.jlh2) || 0;
    const para_jlh3 = parseFloat(item.jlh3) || 0;

    const para_sat1 = item.satuan1 || '';
    const para_sat2 = item.satuan2 || '';
    const para_sat3 = item.satuan3 || '';

    if (para_jlh1 !== 0) para_qty = formatModulus(para_jlh1, ' ' + para_sat1);
    if (para_jlh2 !== 0) para_qty += ' ' + formatModulus(para_jlh2, ' ' + para_sat2);
    if (para_jlh3 !== 0) para_qty += ' ' + formatModulus(para_jlh3, ' ' + para_sat3);

    return para_qty.trim();
}

// === Diskon ===
function tampilDiskon(item) {
    const disA = parseFloat(item.disca) || 0;
    const disB = parseFloat(item.discb) || 0;
    const disC = parseFloat(item.discc) || 0;

    let result = [];

    if (disA !== 0) result.push(formatModulus(disA));
    if (disB !== 0) result.push(formatModulus(disB));
    if (disC !== 0) result.push(formatModulus(disC));

    return result.length > 0 ? result.join(' + ') : '-';
}
