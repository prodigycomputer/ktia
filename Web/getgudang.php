<?php
include 'koneksi.php';

$query = $conn->query("SELECT kodegd, namagd FROM zgudang ORDER BY kodegd");

$hasil = [];
while ($row = $query->fetch_assoc()) {
    $hasil[] = $row;
}

header('Content-Type: application/json');
echo json_encode($hasil);
?>
