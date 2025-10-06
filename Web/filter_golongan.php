<?php
include 'koneksi.php';

$keyword = strtoupper($_GET['keyword']);
$data = [];

if ($keyword === '*') {
    // Tampilkan semua data jika keyword adalah *
    $query = "SELECT * FROM zgolongan ORDER BY kodegol ASC";
    $result = $conn->query($query);

    while ($row = $result->fetch_assoc()) {
        $data[] = [
            'kodegolongan' => $row['kodegol'],
            'namagolongan' => $row['namagol']
        ];
    }
} else {
    $search = "%$keyword%";

    $stmt = $conn->prepare("SELECT * FROM zgolongan WHERE UPPER(kodegol) LIKE ? OR UPPER(namagol) LIKE ? ORDER BY kodegol ASC");
    $stmt->bind_param("ss", $search, $search);
    $stmt->execute();
    $result = $stmt->get_result();

    while ($row = $result->fetch_assoc()) {
        $data[] = [
            'kodegolongan' => $row['kodegol'],
            'namagolongan' => $row['namagol']
        ];
    }
}

header('Content-Type: application/json');
echo json_encode($data);
