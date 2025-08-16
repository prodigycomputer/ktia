<?php
include 'koneksi.php';

$keyword = strtoupper($_GET['keyword']);
$data = [];

if ($keyword === '*') {
    // Tampilkan semua data jika keyword adalah *
    $query = "SELECT * FROM zmerek ORDER BY kodemerk ASC";
    $result = $conn->query($query);

    while ($row = $result->fetch_assoc()) {
        $data[] = [
            'kodemerek' => $row['kodemerk'],
            'namamerek' => $row['namamerk']
        ];
    }
} else {
    $search = "%$keyword%";

    $stmt = $conn->prepare("SELECT * FROM zmerek WHERE UPPER(kodemerk) LIKE ? OR UPPER(namamerk) LIKE ? ORDER BY kodemerk ASC");
    $stmt->bind_param("ss", $search, $search);
    $stmt->execute();
    $result = $stmt->get_result();

    while ($row = $result->fetch_assoc()) {
        $data[] = [
            'kodemerek' => $row['kodemerk'],
            'namamerek' => $row['namamerk']
        ];
    }
}

header('Content-Type: application/json');
echo json_encode($data);
