<?php
include 'koneksi.php';

$keyword = strtoupper($_GET['keyword']);
$data = [];

if ($keyword === '*') {
    // Tampilkan semua data jika keyword adalah *
    $query = "SELECT * FROM zgrup ORDER BY kodegrup ASC";
    $result = $conn->query($query);

    while ($row = $result->fetch_assoc()) {
        $data[] = [
            'kodegrup' => $row['kodegrup'],
            'namagrup' => $row['namagrup']
        ];
    }
} else {
    $search = "%$keyword%";

    $stmt = $conn->prepare("SELECT * FROM zgrup WHERE UPPER(kodegrup) LIKE ? OR UPPER(namagrup) LIKE ? ORDER BY kodegrup ASC");
    $stmt->bind_param("ss", $search, $search);
    $stmt->execute();
    $result = $stmt->get_result();

    while ($row = $result->fetch_assoc()) {
        $data[] = [
            'kodegrup' => $row['kodegrup'],
            'namagrup' => $row['namagrup']
        ];
    }
}

header('Content-Type: application/json');
echo json_encode($data);
