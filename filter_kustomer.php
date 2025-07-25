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
    $where = "ORDER BY kodekust ASC";
} else {
    $keyword = mysqli_real_escape_string($conn, $keyword);
    $where = "WHERE kodekust LIKE '%$keyword%' OR namakust LIKE '%$keyword%' ORDER BY kodekust ASC";
}

$query = mysqli_query($conn, "SELECT * FROM zkustomer $where");

$result = [];
while ($row = mysqli_fetch_assoc($query)) {
    $result[] = $row;
}

header('Content-Type: application/json');
echo json_encode($result);
?>
