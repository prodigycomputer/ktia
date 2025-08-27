<?php
include 'koneksi.php';

header('Content-Type: application/json');

if ($_SERVER['REQUEST_METHOD'] === 'POST') {
    error_reporting(E_ALL);
    ini_set('display_errors', 1);

    $aksi = $_POST['aksi'] ?? '';
    $kodetipe = strtoupper(mysqli_real_escape_string($conn, trim($_POST['kodetipe'] ?? '')));
    $kodetipe_lama = strtoupper(mysqli_real_escape_string($conn, trim($_POST['kodetipe_lama'] ?? $kodetipe)));
    $namatipe = strtoupper(mysqli_real_escape_string($conn, trim($_POST['namatipe'] ?? '')));

    if (!$kodetipe || !$namatipe) {
        echo json_encode(['status' => 'error', 'message' => 'Data tidak lengkap']);
        exit;
    }

    switch ($aksi) {
        case 'tambah':
            $cek = mysqli_query($conn, "SELECT * FROM ztipe WHERE kodetipe = '$kodetipe'");
            if (mysqli_num_rows($cek) > 0) {
                echo json_encode(['status' => 'duplikat', 'message' => 'Kode tipe sudah ada']);
                exit;
            }
            $query = "INSERT INTO ztipe (kodetipe, namatipe) VALUES ('$kodetipe', '$namatipe')";
            break;

        case 'update':
            $query = "UPDATE ztipe SET kodetipe = '$kodetipe', namatipe = '$namatipe' WHERE kodetipe = '$kodetipe_lama'";
            break;

        case 'hapus':
            $query = "DELETE FROM ztipe WHERE kodetipe = '$kodetipe'";
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
