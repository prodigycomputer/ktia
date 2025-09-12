<?php
include 'koneksi.php';
header('Content-Type: application/json');

$nonota = $_GET['nonota'] ?? '';

if (!$nonota) {
    echo json_encode(['status' => 'error', 'message' => 'Nomor nota tidak diberikan']);
    exit;
}

// Ambil data header dari zmutasi
$queryHeader = mysqli_query($conn, "
    SELECT p.nonota, p.tgl, p.kodegd, g.namagd
    FROM zpenyesuaian p
    JOIN zgudang g ON p.kodegd = g.kodegd
    WHERE p.nonota = '$nonota'
");

$dataHeader = mysqli_fetch_assoc($queryHeader);
if (!$dataHeader) {
    echo json_encode(['status' => 'error', 'message' => 'Data tidak ditemukan']);
    exit;
}

// Ambil detail mutasi dari zmutasim dan join ke zstok untuk nama & satuan
$queryDetail = mysqli_query($conn, "
    SELECT q.kodebrg, z.namabrg, q.jlh1, q.jlh2, q.jlh3, q.qty, q.harga,
           z.isi1, z.isi2, z.satuan1, z.satuan2, z.satuan3
    FROM zpenyesuaianm q
    JOIN zstok z ON q.kodebrg = z.kodebrg
    WHERE q.nonota = '$nonota'
");

$dataPenyesuaian = [];
while ($row = mysqli_fetch_assoc($queryDetail)) {
    $dataPenyesuaian[] = $row;
}

echo json_encode([
    'status' => 'success',
    'header' => [
        'no_nota' => $dataHeader['nonota'],
        'tanggal' => $dataHeader['tgl'],
        'kodegd' => $dataHeader['kodegd'],
        'namagd' => $dataHeader['namagd']
    ],
    'detail' => $dataPenyesuaian
]);
?>
