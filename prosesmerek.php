<?php
include 'koneksi.php';

header('Content-Type: application/json');

if ($_SERVER['REQUEST_METHOD'] === 'POST') {
    error_reporting(E_ALL);
    ini_set('display_errors', 1);

    $aksi = $_POST['aksi'] ?? '';
    $kodemerek = strtoupper(mysqli_real_escape_string($conn, trim($_POST['kodemerek'] ?? '')));
    $kodemerek_lama = strtoupper(mysqli_real_escape_string($conn, trim($_POST['kodemerek_lama'] ?? $kodemerek)));
    $namamerek = strtoupper(mysqli_real_escape_string($conn, trim($_POST['namamerek'] ?? '')));

    if (!$kodemerek || !$namamerek) {
        echo json_encode(['status' => 'error', 'message' => 'Data tidak lengkap']);
        exit;
    }

    switch ($aksi) {
        case 'tambah':
            $cek = mysqli_query($conn, "SELECT * FROM zmerek WHERE kodemerk = '$kodemerek'");
            if (mysqli_num_rows($cek) > 0) {
                echo json_encode(['status' => 'duplikat', 'message' => 'Kode grup sudah ada']);
                exit;
            }
            $query = "INSERT INTO zmerek (kodemerk, namamerk) VALUES ('$kodemerek', '$namamerek')";
            break;

        case 'update':
            $query = "UPDATE zmerek SET kodemerk = '$kodemerek', namamerk = '$namamerek' WHERE kodemerk = '$kodemerek_lama'";
            break;

        case 'hapus':
            $query = "DELETE FROM zmerek WHERE kodemerk = '$kodemerek'";
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
