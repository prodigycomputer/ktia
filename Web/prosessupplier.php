<?php
include 'koneksi.php';

header('Content-Type: application/json');

if ($_SERVER['REQUEST_METHOD'] === 'POST') {
    error_reporting(E_ALL);
    ini_set('display_errors', 1);

    $aksi = $_POST['aksi'] ?? '';

    // --- Ambil data dan sanitasi ---
    $kodesup       = strtoupper(mysqli_real_escape_string($conn, trim($_POST['kodesup'] ?? '')));
    $kodesup_lama  = strtoupper(mysqli_real_escape_string($conn, trim($_POST['kodesup_lama'] ?? $kodesup)));
    $namasup       = strtoupper(mysqli_real_escape_string($conn, trim($_POST['namasup'] ?? '')));
    $alamat         = strtoupper(mysqli_real_escape_string($conn, trim($_POST['alamat'] ?? '-')));
    $kota           = strtoupper(mysqli_real_escape_string($conn, trim($_POST['kota'] ?? '-')));
    $ktp            = strtoupper(mysqli_real_escape_string($conn, trim($_POST['ktp'] ?? '-')));
    $npwp           = strtoupper(mysqli_real_escape_string($conn, trim($_POST['npwp'] ?? '-')));

    // --- Validasi wajib isi minimal ---
    if (!$kodesup || !$namasup) {
        echo json_encode(['status' => 'error', 'message' => 'Kode dan Nama pelanggan wajib diisi']);
        exit;
    }

    // --- Proses berdasarkan aksi ---
    switch ($aksi) {
        case 'tambah':
            $cek = $conn->query("SELECT * FROM zsupplier WHERE kodesup = '$kodesup'");
            if ($cek && $cek->num_rows > 0) {
                echo json_encode(['status' => 'duplikat', 'message' => 'Kode pelanggan sudah ada']);
                exit;
            }
            $query = "INSERT INTO zsupplier 
                (kodesup, namasup, alamat, kota, ktp, npwp) VALUES 
                ('$kodesup', '$namasup', '$alamat', '$kota', '$ktp', '$npwp')";
            break;

        case 'update':
            $query = "UPDATE zsupplier SET 
                kodesup = '$kodesup',
                namasup = '$namasup',
                alamat = '$alamat',
                kota = '$kota',
                ktp = '$ktp',
                npwp = '$npwp'
                WHERE kodesup = '$kodesup_lama'";
            break;

        case 'hapus':
            $query = "DELETE FROM zsupplier WHERE kodesup = '$kodesup'";
            break;

        default:
            echo json_encode(['status' => 'error', 'message' => 'Aksi tidak valid']);
            exit;
    }

    // --- Eksekusi query dan kirim respons ---
    if (mysqli_query($conn, $query)) {
        echo json_encode(['status' => 'success', 'aksi' => $aksi]);
    } else {
        echo json_encode(['status' => 'error', 'message' => mysqli_error($conn)]);
    }

    exit;
}
?>
