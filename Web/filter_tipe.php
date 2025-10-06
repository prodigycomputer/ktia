<?php
include 'koneksi.php';

$keyword = strtoupper($_GET['keyword']);
$data = [];

if ($keyword === '*') {
    // Tampilkan semua data jika keyword adalah *
    $query = "SELECT * FROM ztipe ORDER BY kodetipe ASC";
    $result = $conn->query($query);

    while ($row = $result->fetch_assoc()) {
        $data[] = [
            'kodetipe' => $row['kodetipe'],
            'namatipe' => $row['namatipe']
        ];
    }
} else {
    $search = "%$keyword%";

    $stmt = $conn->prepare("SELECT * FROM ztipe WHERE UPPER(kodetipe) LIKE ? OR UPPER(namatipe) LIKE ? ORDER BY kodetipe ASC");
    $stmt->bind_param("ss", $search, $search);
    $stmt->execute();
    $result = $stmt->get_result();

    while ($row = $result->fetch_assoc()) {
        $data[] = [
            'kodetipe' => $row['kodetipe'],
            'namatipe' => $row['namatipe']
        ];
    }
}

header('Content-Type: application/json');
echo json_encode($data);
