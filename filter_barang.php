<?php
include 'koneksi.php';

$keyword = strtoupper(trim($_GET['keyword'] ?? ''));

// Jika kosong, langsung return kosong
if ($keyword === '') {
    header('Content-Type: application/json');
    echo json_encode([]);
    exit;
}

$where = '';
if ($keyword === '*') {
    $where = "ORDER BY kodebrg ASC";
} else {
    $keyword = mysqli_real_escape_string($conn, $keyword);
    $where = "WHERE kodebrg LIKE '%$keyword%' OR namabrg LIKE '%$keyword%' ORDER BY kodebrg ASC";
}

$query = mysqli_query($conn, "SELECT * FROM zstok $where");

$result = [];
while ($row = mysqli_fetch_assoc($query)) {
    $result[] = $row;
}

header('Content-Type: application/json');
echo json_encode($result);
?>
