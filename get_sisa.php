<?php
include 'koneksi.php';

$kodegd = $_GET['kodegd'] ?? '';
$kodebrg = $_GET['kodebrg'] ?? '';

// Query dengan filter gudang dan barang
$sql = "SELECT zsaldo.sisa1, zstok.satuan1, zsaldo.sisa2, zstok.satuan2, zsaldo.sisa3, zstok.satuan3 
        FROM zsaldo 
        JOIN zstok ON zsaldo.kodebrg = zstok.kodebrg
        WHERE zsaldo.kodegd = '$kodegd' AND zsaldo.kodebrg = '$kodebrg'
        LIMIT 1";

$result = mysqli_query($conn, $sql);
$data = mysqli_fetch_assoc($result);

if ($data) {
    echo json_encode([
        'sisa1' => (float)$data['sisa1'],
        'sisa2' => (float)$data['sisa2'],
        'sisa3' => (float)$data['sisa3']
    ]);
} else {
    echo json_encode([
        'sisa1' => 0,
        'sisa2' => 0,
        'sisa3' => 0
    ]);
}
?>
