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
    SELECT 
        l.nonota, 
        l.tgl, 
        l.kodegd1, 
        g1.namagd AS namagd1,
        l.kodegd2, 
        g2.namagd AS namagd2
    FROM zmutasi l
    LEFT JOIN zgudang g1 ON l.kodegd1 = g1.kodegd
    LEFT JOIN zgudang g2 ON l.kodegd2 = g2.kodegd
    WHERE l.nonota = '$nonota'
");


$dataHeader = mysqli_fetch_assoc($queryHeader);
if (!$dataHeader) {
    echo json_encode(['status' => 'error', 'message' => 'Data tidak ditemukan']);
    exit;
}

// Ambil detail mutasi dari zmutasim dan join ke zstok untuk nama & satuan
$queryDetail = mysqli_query($conn, "
    SELECT n.kodebrg, z.namabrg, n.jlh1, n.jlh2, n.jlh3,
           z.isi1, z.isi2, z.satuan1, z.satuan2, z.satuan3
    FROM zmutasim n
    JOIN zstok z ON n.kodebrg = z.kodebrg
    WHERE n.nonota = '$nonota'
");

$dataMutasi = [];
while ($row = mysqli_fetch_assoc($queryDetail)) {
    $dataMutasi[] = $row;
}

echo json_encode([
    'status' => 'success',
    'header' => [
        'no_nota'   => $dataHeader['nonota'],
        'tanggal'   => $dataHeader['tgl'],
        'kodegd1'   => $dataHeader['kodegd1'],
        'namagd1'   => $dataHeader['namagd1'],
        'kodegd2'   => $dataHeader['kodegd2'],
        'namagd2'   => $dataHeader['namagd2']
    ],
    'detail' => $dataMutasi
]);

?>
