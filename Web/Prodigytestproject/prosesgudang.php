<?php
include 'koneksi.php';

header('Content-Type: application/json');

if ($_SERVER['REQUEST_METHOD'] === 'POST') {
    error_reporting(E_ALL);
    ini_set('display_errors', 1);

    $aksi = $_POST['aksi'] ?? '';
    $kodegudang = strtoupper(mysqli_real_escape_string($conn, trim($_POST['kodegudang'] ?? '')));
    $kodegudang_lama = strtoupper(mysqli_real_escape_string($conn, trim($_POST['kodegudang_lama'] ?? $kodegudang)));
    $namagudang = strtoupper(mysqli_real_escape_string($conn, trim($_POST['namagudang'] ?? '')));

    if (!$kodegudang || !$namagudang) {
        echo json_encode(['status' => 'error', 'message' => 'Data tidak lengkap']);
        exit;
    }

    switch ($aksi) {
        case 'tambah':
            $cek = mysqli_query($conn, "SELECT * FROM zgudang WHERE kodegd = '$kodegudang'");
            if (mysqli_num_rows($cek) > 0) {
                echo json_encode(['status' => 'duplikat', 'message' => 'Kode grup sudah ada']);
                exit;
            }
            $query = "INSERT INTO zgudang (kodegd, namagd) VALUES ('$kodegudang', '$namagudang')";
            break;

        case 'update':
            $query = "UPDATE zgudang SET kodegd = '$kodegudang', namagd = '$namagudang' WHERE kodegd = '$kodegudang_lama'";
            break;

        case 'hapus':
            $query = "DELETE FROM zgudang WHERE kodegd = '$kodegudang'";
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
