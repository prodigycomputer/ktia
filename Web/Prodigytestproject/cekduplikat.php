<?php
include 'koneksi.php';

$table = $_GET['table'] ?? '';
$field = $_GET['field'] ?? '';
$keyword = $_GET['value'] ?? '';

$result = [];

if ($table && $field && $keyword) {
    $keyword = strtoupper($keyword);
    $query = $conn->prepare("SELECT * FROM $table WHERE $field LIKE CONCAT('%', ?, '%') LIMIT 10");
    $query->bind_param('s', $keyword);
    $query->execute();
    $res = $query->get_result();
    
    while ($row = $res->fetch_assoc()) {
        $result[] = $row;
    }
}

echo json_encode(['data' => $result]);
?>
