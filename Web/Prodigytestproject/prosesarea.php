<?php
include 'koneksi.php';

header('Content-Type: application/json');

if ($_SERVER['REQUEST_METHOD'] === 'POST') {
    error_reporting(E_ALL);
    ini_set('display_errors', 1);

    $aksi = $_POST['aksi'] ?? '';
    $kodearea = strtoupper(mysqli_real_escape_string($conn, trim($_POST['kodearea'] ?? '')));
    $kodearea_lama = strtoupper(mysqli_real_escape_string($conn, trim($_POST['kodearea_lama'] ?? $kodearea)));
    $namaarea = strtoupper(mysqli_real_escape_string($conn, trim($_POST['namaarea'] ?? '')));

    if (!$kodearea || !$namaarea) {
        echo json_encode(['status' => 'error', 'message' => 'Data tidak lengkap']);
        exit;
    }

    switch ($aksi) {
        case 'tambah':
            $cek = mysqli_query($conn, "SELECT * FROM zarea WHERE kodear = '$kodearea'");
            if (mysqli_num_rows($cek) > 0) {
                echo json_encode(['status' => 'duplikat', 'message' => 'Kode area sudah ada']);
                exit;
            }
            $query = "INSERT INTO zarea (kodear, namaar) VALUES ('$kodearea', '$namaarea')";
            break;

        case 'update':
            $query = "UPDATE zarea SET kodear = '$kodearea', namaar = '$namaarea' WHERE kodear = '$kodearea_lama'";
            break;

        case 'hapus':
            $query = "DELETE FROM zarea WHERE kodear = '$kodearea'";
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
