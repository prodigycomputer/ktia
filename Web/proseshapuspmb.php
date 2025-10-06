<?php
include 'koneksi.php';

if (isset($_GET['nonota'])) {
    $nonota = $_GET['nonota'];
    
    $hapusDetail = mysqli_query($conn, "DELETE FROM zbelim WHERE nonota = '$nonota'");
    $hapusMaster = mysqli_query($conn, "DELETE FROM zbeli WHERE nonota = '$nonota'");

    if ($hapusDetail && $hapusMaster) {
        echo json_encode(['success' => true]);
    } else {
        echo json_encode(['success' => false]);
    }
}
?>
