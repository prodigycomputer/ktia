<?php
include 'koneksi.php';

header('Content-Type: application/json');

if ($_SERVER['REQUEST_METHOD'] === 'POST') {
    error_reporting(E_ALL);
    ini_set('display_errors', 1);

    $aksi = $_POST['aksi'] ?? '';

    // --- Ambil data dan sanitasi ---
    $kodearea       = strtoupper(mysqli_real_escape_string($conn, trim($_POST['kodearea'] ?? '')));
    $kodetipe       = strtoupper(mysqli_real_escape_string($conn, trim($_POST['kodetipe'] ?? '')));
    $kodekust       = strtoupper(mysqli_real_escape_string($conn, trim($_POST['kodekust'] ?? '')));
    $kodekust_lama  = strtoupper(mysqli_real_escape_string($conn, trim($_POST['kodekust_lama'] ?? $kodekust)));
    $namakust       = strtoupper(mysqli_real_escape_string($conn, trim($_POST['namakust'] ?? '')));
    $alamat         = strtoupper(mysqli_real_escape_string($conn, trim($_POST['alamat'] ?? '-')));
    $kota           = strtoupper(mysqli_real_escape_string($conn, trim($_POST['kota'] ?? '-')));
    $kodehrg        = strtoupper(mysqli_real_escape_string($conn, trim($_POST['kodehrg'] ?? '-')));
    $ktp            = strtoupper(mysqli_real_escape_string($conn, trim($_POST['ktp'] ?? '-')));
    $npwp           = strtoupper(mysqli_real_escape_string($conn, trim($_POST['npwp'] ?? '-')));

    // --- Validasi wajib isi minimal ---
    if (!$kodearea || !$kodetipe || !$kodekust || !$namakust) {
        echo json_encode(['status' => 'error', 'message' => 'Data wajib diisi']);
        exit;
    }

    // --- Proses berdasarkan aksi ---
    switch ($aksi) {
        case 'tambah':
            $cek = $conn->query("SELECT * FROM zkustomer WHERE kodekust = '$kodekust'");
            if ($cek && $cek->num_rows > 0) {
                echo json_encode(['status' => 'duplikat', 'message' => 'Kode pelanggan sudah ada']);
                exit;
            }
            $query = "INSERT INTO zkustomer 
                (kodekust, kodear, kodetipe, namakust, alamat, kota, kodehrg, ktp, npwp) VALUES 
                ('$kodekust', '$kodearea', '$kodetipe', '$namakust', '$alamat', '$kota', '$kodehrg', '$ktp', '$npwp')";
            break;

        case 'update':
            $query = "UPDATE zkustomer SET 
                kodekust = '$kodekust',
                kodear = '$kodearea',
                kodetipe = '$kodetipe',
                namakust = '$namakust',
                alamat = '$alamat',
                kota = '$kota',
                kodehrg = '$kodehrg',
                ktp = '$ktp',
                npwp = '$npwp'
                WHERE kodekust = '$kodekust_lama'";
            break;

        case 'hapus':
            $query = "DELETE FROM zkustomer WHERE kodekust = '$kodekust'";
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
