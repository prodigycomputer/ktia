<?php
include 'koneksi.php';

$keyword = strtoupper($_GET['keyword']);
$data = [];

if ($keyword === '*') {
    // Tampilkan semua data jika keyword adalah *
    $query = "SELECT * FROM zarea ORDER BY kodear ASC";
    $result = $conn->query($query);

    while ($row = $result->fetch_assoc()) {
        $data[] = [
            'kodearea' => $row['kodear'],
            'namaarea' => $row['namaar']
        ];
    }
} else {
    $search = "%$keyword%";

    $stmt = $conn->prepare("SELECT * FROM zarea WHERE UPPER(kodear) LIKE ? OR UPPER(namaar) LIKE ? ORDER BY kodear ASC");
    $stmt->bind_param("ss", $search, $search);
    $stmt->execute();
    $result = $stmt->get_result();

    while ($row = $result->fetch_assoc()) {
        $data[] = [
            'kodearea' => $row['kodear'],
            'namaarea' => $row['namaar']
        ];
    }
}

header('Content-Type: application/json');
echo json_encode($data);
