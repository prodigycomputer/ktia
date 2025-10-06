<?php
include 'koneksi.php';

header('Content-Type: application/json');

if ($_SERVER['REQUEST_METHOD'] === 'POST') {
    error_reporting(E_ALL);
    ini_set('display_errors', 1);

    $aksi = $_POST['aksi'] ?? '';
    $kodegolongan = strtoupper(mysqli_real_escape_string($conn, trim($_POST['kodegolongan'] ?? '')));
    $kodegolongan_lama = strtoupper(mysqli_real_escape_string($conn, trim($_POST['kodegolongan_lama'] ?? $kodegolongan)));
    $namagolongan = strtoupper(mysqli_real_escape_string($conn, trim($_POST['namagolongan'] ?? '')));

    if (!$kodegolongan || !$namagolongan) {
        echo json_encode(['status' => 'error', 'message' => 'Data tidak lengkap']);
        exit;
    }

    switch ($aksi) {
        case 'tambah':
            $cek = mysqli_query($conn, "SELECT * FROM zgolongan WHERE kodegol = '$kodegolongan'");
            if (mysqli_num_rows($cek) > 0) {
                echo json_encode(['status' => 'duplikat', 'message' => 'Kode grup sudah ada']);
                exit;
            }
            $query = "INSERT INTO zgolongan (kodegol, namagol) VALUES ('$kodegolongan', '$namagolongan')";
            break;

        case 'update':
            $query = "UPDATE zgolongan SET kodegol = '$kodegolongan', namagol = '$namagolongan' WHERE kodegol = '$kodegolongan_lama'";
            break;

        case 'hapus':
            $query = "DELETE FROM zgolongan WHERE kodegol = '$kodegolongan'";
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
