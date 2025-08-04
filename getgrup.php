<?php
require 'koneksi.php';

$keyword = $_GET['q'] ?? '';
$sql = "SELECT kodegrup, namagrup FROM zgroup WHERE kodegrup LIKE ? OR namagrup LIKE ?";
$stmt = $conn->prepare($sql);
$search = "%$keyword%";
$stmt->bind_param('ss', $search, $search);
$stmt->execute();
$result = $stmt->get_result();

$data = [];
while ($row = $result->fetch_assoc()) {
    $data[] = $row;
}

echo json_encode($data);
?>
