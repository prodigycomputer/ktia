<?php
include 'koneksi.php';

$mode = $_POST['mode'] ?? '';
$keyword = strtoupper(trim($_POST['keyword'] ?? ''));

if (!in_array($mode, ['kode', 'nama'])) {
    echo json_encode([]);
    exit;
}

$field = $mode === 'kode' ? 'kodesls' : 'namasls';

if ($keyword === '' || $keyword === '*') {
    // 🔹 Jika kosong atau bintang, ambil semua data
    $query = $conn->prepare("SELECT kodesls, namasls FROM zsales ORDER BY $field LIMIT 50");
} else {
    // 🔹 Jika ada keyword, filter berdasarkan LIKE
    $query = $conn->prepare("SELECT kodesls, namasls FROM zsales WHERE $field LIKE CONCAT('%', ?, '%') LIMIT 50");
    $query->bind_param("s", $keyword);
}

$query->execute();
$result = $query->get_result();

$data = [];
while ($row = $result->fetch_assoc()) {
    $data[] = $row;
}

echo json_encode($data);
?>
