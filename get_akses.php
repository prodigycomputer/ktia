<?php
include 'koneksi.php';

$kode = $_GET['kodeuser'] ?? '';

$sql = "SELECT m.idmenu, 
        REPLACE(m.submenu, '.php', '') AS submenu,
        IFNULL(a.tambah, 1) AS tambah,
        IFNULL(a.ubah, 1) AS ubah,
        IFNULL(a.hapus, 1) AS hapus
        FROM zmenu m
        LEFT JOIN zakses a ON m.idmenu = a.idmenu AND a.kodeuser = '$kode'
        ORDER BY m.urutan";

$result = $conn->query($sql);
$data = [];

while ($row = $result->fetch_assoc()) {
    $data[] = [
        'idmenu' => $row['idmenu'],
        'submenu' => ucwords($row['submenu']),
        'tambah' => (int)$row['tambah'],
        'ubah' => (int)$row['ubah'],
        'hapus' => (int)$row['hapus']
    ];
}

// Hapus data pertama (Dashboard) sebelum dikirim
if (!empty($data)) {
    $data = array_slice($data, 1);
}

header('Content-Type: application/json');
echo json_encode($data);
