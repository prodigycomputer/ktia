<?php
include 'koneksi.php';
$kodeuser = $_GET['kodeuser'] ?? '';
$akses = $_POST['akses'] ?? [];

// Ambil semua idmenu
$menus = mysqli_query($conn, "SELECT idmenu FROM zmenu");

// Simpan semua menu, kalau tidak dicentang → 0
while($m = mysqli_fetch_assoc($menus)){
    $idmenu = $m['idmenu'];
    $val = isset($akses[$idmenu]) ? 1 : 0;
    $cek = mysqli_query($conn, "SELECT * FROM zakses WHERE kodeuser='$kodeuser' AND idmenu='$idmenu'");
    if(mysqli_num_rows($cek) > 0){
        mysqli_query($conn, "UPDATE zakses SET akses='$val' WHERE kodeuser='$kodeuser' AND idmenu='$idmenu'");
    } else {
        mysqli_query($conn, "INSERT INTO zakses (kodeuser, idmenu, akses) VALUES ('$kodeuser','$idmenu','$val')");
    }
}

// Kirim pesan balik, bukan redirect
echo "Perubahan disimpan!";
?>
