<?php
include 'koneksi.php';
session_start();

header('Content-Type: application/json');

if ($_SERVER['REQUEST_METHOD'] === 'POST') {
    error_reporting(E_ALL);
    ini_set('display_errors', 1);

    $aksi = $_POST['aksi'] ?? '';
    $kodeuser = strtoupper(mysqli_real_escape_string($conn, trim($_POST['kodeuser'] ?? '')));
    $kodeuser_lama = strtoupper(mysqli_real_escape_string($conn, trim($_POST['kodeuser_lama'] ?? $kodeuser)));
    $namauser = strtoupper(mysqli_real_escape_string($conn, trim($_POST['namauser'] ?? '')));
    $kunci = strtoupper(mysqli_real_escape_string($conn, trim($_POST['passworduser'] ?? '')));

    if (!$kodeuser || !$namauser || !$kunci) {
        echo json_encode(['status' => 'error', 'message' => 'Data tidak lengkap']);
        exit;
    }

    switch ($aksi) {
        case 'tambah':
            $cek = mysqli_query($conn, "SELECT * FROM zusers WHERE kodeuser = '$kodeuser'");
            if (mysqli_num_rows($cek) > 0) {
                echo json_encode(['status' => 'duplikat', 'message' => 'Kode user sudah ada']);
                exit;
            }
            $query = "INSERT INTO zusers (kodeuser, username, kunci) VALUES ('$kodeuser', '$namauser', '$kunci')";
            break;

        case 'update':
            $query = "UPDATE zusers SET kodeuser = '$kodeuser', username = '$namauser', kunci = '$kunci' WHERE kodeuser = '$kodeuser_lama'";
            break;

        case 'hapus':
            $query = "DELETE FROM zusers WHERE kodeuser = '$kodeuser'";
            break;

        default:
            echo json_encode(['status' => 'error', 'message' => 'Aksi tidak valid']);
            exit;
    }

    if (mysqli_query($conn, $query)) {
        echo json_encode(['status' => 'success', 'aksi' => $aksi]);
    } else {
        echo json_encode(['status' => 'error', 'message' => 'Query gagal']);
    }
    exit;
}
