<?php
session_start();
include 'koneksi.php';

$sql = "SELECT * FROM zusers";

$result = mysqli_query($conn, $sql);

$row = mysqli_fetch_all($result, MYSQLI_ASSOC);
echo json_encode($row);
?>
