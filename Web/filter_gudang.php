<?php
include 'koneksi.php';

$keyword = strtoupper($_GET['keyword']);
$data = [];

if ($keyword === '*') {
    // Tampilkan semua data jika keyword adalah *
    $query = "SELECT * FROM zgudang ORDER BY kodegd ASC";
    $result = $conn->query($query);

    while ($row = $result->fetch_assoc()) {
        $data[] = [
            'kodegudang' => $row['kodegd'],
            'namagudang' => $row['namagd']
        ];
    }
} else {
    $search = "%$keyword%";

    $stmt = $conn->prepare("SELECT * FROM zgudang WHERE UPPER(kodegd) LIKE ? OR UPPER(namagd) LIKE ? ORDER BY kodegd ASC");
    $stmt->bind_param("ss", $search, $search);
    $stmt->execute();
    $result = $stmt->get_result();

    while ($row = $result->fetch_assoc()) {
        $data[] = [
            'kodegudang' => $row['kodegd'],
            'namagudang' => $row['namagd']
        ];
    }
}

header('Content-Type: application/json');
echo json_encode($data);
