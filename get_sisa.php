<?php
include 'koneksi.php';

$kodegd = $_GET['kodegd'] ?? '';
$kodest = $_GET['kodest'] ?? '';

$sql = "SELECT sisa1, sisa2, sisa3 
        FROM zsaldo 
        WHERE kodegd = '$kodegd' AND kodest = '$kodest'
        LIMIT 1";
$result = mysqli_query($conn, $sql);

$data = mysqli_fetch_assoc($result);
if ($data) {
    echo json_encode([
        'sisa1' => $data['sisa1'],
        'sisa2' => $data['sisa2'],
        'sisa3' => $data['sisa3']
    ]);
} else {
    echo json_encode([
        'sisa1' => 0,
        'sisa2' => 0,
        'sisa3' => 0
    ]);
}
?>
