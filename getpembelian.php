<?php
include 'koneksi.php';

header('Content-Type: application/json');

$nonota = $_GET['nonota'] ?? '';

if (!$nonota) {
    echo json_encode(['status' => 'error', 'message' => 'Nomor nota tidak diberikan']);
    exit;
}

// Ambil data header dari zbeli dan supplier
$queryHeader = mysqli_query($conn, "
    SELECT b.nonota, b.tgl, b.kodesup, b.tgltempo, b.nilai, s.namasup, s.alamat
    FROM zbeli b
    JOIN zsupplier s ON b.kodesup = s.kodesup
    WHERE b.nonota = '$nonota'
");

$dataHeader = mysqli_fetch_assoc($queryHeader);

// Ambil detail pembelian dari zbelim, dan join ke zstok untuk nama & satuan
$queryDetail = mysqli_query($conn, "
    SELECT d.kodebrg, z.namabrg, d.jlh1, d.jlh2, d.jlh3,
           d.harga, d.disca, d.discb, d.discc, d.discrp, d.jumlah,
           z.isi1, z.isi2, z.satuan1, z.satuan2, z.satuan3
    FROM zbelim d
    JOIN zstok z ON d.kodebrg = z.kodebrg
    WHERE d.nonota = '$nonota'
");

$dataPembelian = [];
while ($row = mysqli_fetch_assoc($queryDetail)) {
    $dataPembelian[] = $row;
}

echo json_encode([
    'status' => 'success',
    'header' => [
        'no_nota' => $dataHeader['nonota'],
        'tanggal' => $dataHeader['tgl'],
        'kode_sup' => $dataHeader['kodesup'],
        'nama_sup' => $dataHeader['namasup'],
        'alamat' => $dataHeader['alamat'],
        'jt_tempo' => $dataHeader['tgltempo'],
        'totaljmlh' => $dataHeader['nilai']
    ],
    'detail' => $dataPembelian
]);
?>
