<?php
include 'koneksi.php';

header('Content-Type: application/json');

if ($_SERVER['REQUEST_METHOD'] === 'POST') {
    error_reporting(E_ALL);
    ini_set('display_errors', 1);

    $aksi = $_POST['aksi'] ?? '';
    $kodegrup = strtoupper(mysqli_real_escape_string($conn, trim($_POST['kodegrup'] ?? '')));
    $kodegrup_lama = strtoupper(mysqli_real_escape_string($conn, trim($_POST['kodegrup_lama'] ?? $kodegrup)));
    $namagrup = strtoupper(mysqli_real_escape_string($conn, trim($_POST['namagrup'] ?? '')));

    if (!$kodegrup || !$namagrup) {
        echo json_encode(['status' => 'error', 'message' => 'Data tidak lengkap']);
        exit;
    }

    switch ($aksi) {
        case 'tambah':
            $cek = mysqli_query($conn, "SELECT * FROM zgrup WHERE kodegrup = '$kodegrup'");
            if (mysqli_num_rows($cek) > 0) {
                echo json_encode(['status' => 'duplikat', 'message' => 'Kode grup sudah ada']);
                exit;
            }
            $query = "INSERT INTO zgrup (kodegrup, namagrup) VALUES ('$kodegrup', '$namagrup')";
            break;

        case 'update':
            $query = "UPDATE zgrup SET kodegrup = '$kodegrup', namagrup = '$namagrup' WHERE kodegrup = '$kodegrup_lama'";
            break;

        case 'hapus':
            $query = "DELETE FROM zgrup WHERE kodegrup = '$kodegrup'";
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
