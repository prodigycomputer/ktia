<?php
include 'koneksi.php';

$data = json_decode(file_get_contents("php://input"), true);

$kodeuser = $data['kodeuser'] ?? '';
$akses = $data['akses'] ?? [];

if ($kodeuser == '' || empty($akses)) {
    echo json_encode(['status' => 'error', 'message' => 'Data tidak valid']);
    exit;
}

// Hapus data lama
$conn->query("DELETE FROM zakses WHERE kodeuser = '$kodeuser'");

// Insert data baru
$stmt = $conn->prepare("INSERT INTO zakses (kodeuser, idmenu, tambah, ubah, hapus) VALUES (?, ?, ?, ?, ?)");

foreach ($akses as $row) {
    $idmenu = $row['idmenu'];
    $tambah = $row['tambah'];
    $ubah = $row['ubah'];
    $hapus = $row['hapus'];

    $stmt->bind_param("ssiii", $kodeuser, $idmenu, $tambah, $ubah, $hapus);
    $stmt->execute();
}

$stmt->close();
$conn->close();

echo json_encode(['status' => 'success']);
