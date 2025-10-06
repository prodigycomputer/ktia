<?php 
include 'navbar.php'; 

$current = basename($_SERVER['PHP_SELF']);

$akses = $_SESSION['aksesSemua'][$current] ?? ['ubah'=>0,'hapus'=>0];

$hakUbah   = $akses['ubah'];
$hakHapus  = $akses['hapus'];
?>