<?php
include 'koneksi.php';

$keyword = strtoupper($_GET['keyword']);
$data = [];

if ($keyword === '*') {
    // Tampilkan semua data jika keyword adalah *
    $query = "SELECT * FROM zusers ORDER BY kodeuser ASC";
    $result = $conn->query($query);

    while ($row = $result->fetch_assoc()) {
        $data[] = [
            'kodeuser' => $row['kodeuser'],
            'namauser' => $row['username'],
            'passworduser' => $row['kunci']
        ];
    }
} else {
    $search = "%$keyword%";

    $stmt = $conn->prepare("SELECT * FROM zusers WHERE UPPER(kodeuser) LIKE ? OR UPPER(namauser) LIKE ? OR UPPER(passworduser) LIKE ? ORDER BY kodeuser ASC");
    $stmt->bind_param("sss", $search, $search);
    $stmt->execute();
    $result = $stmt->get_result();

    while ($row = $result->fetch_assoc()) {
        $data[] = [
            'kodeuser' => $row['kodeuser'],
            'namauser' => $row['username'],
            'passworduser' => $row['kunci']
        ];
    }
}

header('Content-Type: application/json');
echo json_encode($data);
