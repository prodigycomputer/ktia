<?php
include 'koneksi.php';
session_start();

header('Content-Type: application/json');

if ($_SERVER['REQUEST_METHOD'] === 'POST') {
    error_reporting(E_ALL);
    ini_set('display_errors', 1);

    $aksi = $_POST['aksi'] ?? '';

    // --- Ambil semua field kolom harga (harga1, harga11, dst.) ---
    $hargaData = [];
    $resField = $conn->query("SHOW COLUMNS FROM zstok WHERE Field LIKE 'harga%'");
    while ($row = $resField->fetch_assoc()) {
        $field = $row['Field'];
        $hargaData[$field] = isset($_POST[$field]) && is_numeric($_POST[$field]) ? floatval($_POST[$field]) : 0;
    }

    // --- Ambil data utama dari form ---
    $kodebrg       = strtoupper(mysqli_real_escape_string($conn, trim($_POST['kodebrg'] ?? '')));
    $kodebrg_lama  = strtoupper(mysqli_real_escape_string($conn, trim($_POST['kodebrg_lama'] ?? $kodebrg)));
    $kodegrup      = strtoupper(mysqli_real_escape_string($conn, trim($_POST['searchGrup'] ?? '')));
    $namabrg       = strtoupper(mysqli_real_escape_string($conn, trim($_POST['namabrg'] ?? '')));
    $satuan1       = strtoupper(mysqli_real_escape_string($conn, trim($_POST['satuan1'] ?? '-')));
    $satuan2       = strtoupper(mysqli_real_escape_string($conn, trim($_POST['satuan2'] ?? '-')));
    $satuan3       = strtoupper(mysqli_real_escape_string($conn, trim($_POST['satuan3'] ?? '-')));
    $isi1          = is_numeric($_POST['isi1'] ?? '') ? floatval($_POST['isi1']) : 1;
    $isi2          = is_numeric($_POST['isi2'] ?? '') ? floatval($_POST['isi2']) : 1;

    /*// Validasi minimal
    if (!$kodebrg || !$namabrg || !$kodegrup) {
        echo json_encode(['status' => 'error', 'message' => 'Data tidak lengkap']);
        exit;
    }*/

    $fieldHarga = '';
    $valueHarga = '';
    $updateHarga = '';
    foreach ($hargaData as $field => $val) {
        $fieldHarga  .= ", `$field`";
        $valueHarga  .= ", $val";
        $updateHarga .= ", `$field` = $val";
    }

    // --- Proses sesuai aksi ---
    switch ($aksi) {
        case 'tambah':
            $cek = $conn->query("SELECT * FROM zstok WHERE kodebrg='$kodebrg'");
            if ($cek && $cek->num_rows > 0) {
                echo json_encode(['status' => 'duplikat', 'message' => 'Kode barang sudah ada']);
                exit;
            }
            $query = "INSERT INTO zstok 
                (kodebrg, kodegrup, namabrg, satuan1, satuan2, satuan3, isi1, isi2$fieldHarga)
                VALUES 
                ('$kodebrg', '$kodegrup', '$namabrg', '$satuan1', '$satuan2', '$satuan3', $isi1, $isi2$valueHarga)";
            break;

        case 'update':
            $query = "UPDATE zstok SET 
                kodebrg = '$kodebrg',
                kodegrup = '$kodegrup',
                namabrg = '$namabrg',
                satuan1 = '$satuan1',
                satuan2 = '$satuan2',
                satuan3 = '$satuan3',
                isi1 = $isi1,
                isi2 = $isi2
                $updateHarga
                WHERE kodebrg = '$kodebrg_lama'";
            break;

        case 'hapus':
            $query = "DELETE FROM zstok WHERE kodebrg='$kodebrg'";
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
