<?php
$host     = "192.168.1.123";
$username = "kita";
$password = "kita";
$database = "kita";

$conn = mysqli_connect($host, $username, $password, $database);

if (!$conn) {
    die("Koneksi gagal: " . mysqli_connect_error());
}
?>
