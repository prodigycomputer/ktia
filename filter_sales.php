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
    $where = "ORDER BY kodesls ASC";
} else {
    $keyword = mysqli_real_escape_string($conn, $keyword);
    $where = "WHERE kodesls LIKE '%$keyword%' OR namasls LIKE '%$keyword%' ORDER BY kodesls ASC";
}

$query = mysqli_query($conn, "SELECT * FROM zsales $where");

$result = [];
while ($row = mysqli_fetch_assoc($query)) {
    $result[] = $row;
}

header('Content-Type: application/json');
echo json_encode($result);
?>
