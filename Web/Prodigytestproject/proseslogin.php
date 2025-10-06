<?php
session_start();
include 'koneksi.php';

$username = $_POST['username'];
$password = $_POST['password'];

// Gunakan prepared statement untuk keamanan
$stmt = mysqli_prepare($conn, "SELECT kodeuser, username, kunci FROM zusers WHERE username = ? AND kunci = ?");
mysqli_stmt_bind_param($stmt, "ss", $username, $password);
mysqli_stmt_execute($stmt);

$result = mysqli_stmt_get_result($stmt);
$data = mysqli_fetch_assoc($result);

// Cek user dan password
if ($data) {
    $_SESSION['username'] = $data['username'];
    $_SESSION['kodeuser'] = $data['kodeuser'];

    header("Location: dashboard.php");
    exit;
} else {
    $_SESSION['error'] = "Username atau password salah.";
    header("Location: index.php");
    exit;
}
?>
