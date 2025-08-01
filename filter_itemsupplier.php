<?php
include 'koneksi.php';

$mode = $_POST['mode'] ?? '';
$keyword = strtoupper(trim($_POST['keyword'] ?? ''));

if (!$keyword || !in_array($mode, ['kode', 'nama'])) {
    echo json_encode([]);
    exit;
}

$field = $mode === 'kode' ? 'kodesup' : 'namasup';

$query = $conn->prepare("SELECT kodesup, namasup, alamat FROM zsupplier WHERE $field LIKE CONCAT('%', ?, '%') LIMIT 50");
$query->bind_param("s", $keyword);
$query->execute();
$result = $query->get_result();

$data = [];
while ($row = $result->fetch_assoc()) {
    $data[] = $row;
}

echo json_encode($data);
?>
