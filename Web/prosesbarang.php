<?php
include 'koneksi.php';

header('Content-Type: application/json');

if ($_SERVER['REQUEST_METHOD'] === 'POST') {
    error_reporting(E_ALL);
    ini_set('display_errors', 1);

    $aksi = $_POST['aksi'] ?? '';

    // --- Ambil dan sanitasi data utama ---
    $kodebrg       = strtoupper(mysqli_real_escape_string($conn, trim($_POST['kodebrg'] ?? '')));
    $kodebrg_lama  = strtoupper(mysqli_real_escape_string($conn, trim($_POST['kodebrg_lama'] ?? $kodebrg)));
    $kodegrup      = strtoupper(mysqli_real_escape_string($conn, trim($_POST['kodegrup'] ?? '')));
    $kodemerek     = strtoupper(mysqli_real_escape_string($conn, trim($_POST['kodemerek'] ?? '')));
    $kodegolongan      = strtoupper(mysqli_real_escape_string($conn, trim($_POST['kodegolongan'] ?? '')));
    $namabrg       = strtoupper(mysqli_real_escape_string($conn, trim($_POST['namabrg'] ?? '')));
    $satuan1       = strtoupper(mysqli_real_escape_string($conn, trim($_POST['satuan1'] ?? '-')));
    $satuan2       = strtoupper(mysqli_real_escape_string($conn, trim($_POST['satuan2'] ?? '-')));
    $satuan3       = strtoupper(mysqli_real_escape_string($conn, trim($_POST['satuan3'] ?? '-')));
    $isi1          = is_numeric($_POST['isi1'] ?? '') ? floatval($_POST['isi1']) : 0;
    $isi2          = is_numeric($_POST['isi2'] ?? '') ? floatval($_POST['isi2']) : 0;
    $hargabeli     = is_numeric($_POST['hargabeli'] ?? '') ? floatval($_POST['hargabeli']) : 0;

    // --- Validasi minimal wajib isi ---
    if (!$kodebrg || !$namabrg || !$kodegrup || !$kodemerek || !$kodegolongan) {
        echo json_encode(['status' => 'error', 'message' => 'Kode, Nama, Merek, Golongan dan Grup wajib diisi']);
        exit;
    }

    // --- Ambil semua kolom harga dari DB dan isi datanya dari POST ---
    $hargaData = [];
    $resField = $conn->query("SHOW COLUMNS FROM zstok WHERE Field LIKE 'harga%'");
    while ($row = $resField->fetch_assoc()) {
        $field = $row['Field'];
        if (isset($_POST[$field]) && is_numeric($_POST[$field])) {
            $hargaData[$field] = floatval($_POST[$field]);
        } else {
            $hargaData[$field] = 0;
        }
    }

    // --- Susun bagian harga untuk query ---
    $fieldHarga = '';
    $valueHarga = '';
    $updateHarga = '';
    foreach ($hargaData as $field => $val) {
        $fieldHarga  .= ", `$field`";
        $valueHarga  .= ", $val";
        $updateHarga .= ", `$field` = $val";
    }

    // --- Proses berdasarkan aksi ---
    switch ($aksi) {
        case 'tambah':
            $cek = $conn->query("SELECT 1 FROM zstok WHERE kodebrg = '$kodebrg'");
            if ($cek && $cek->num_rows > 0) {
                echo json_encode(['status' => 'duplikat', 'message' => 'Kode barang sudah ada']);
                exit;
            }
            $query = "INSERT INTO zstok 
                (kodebrg, kodemerk, kodegol, kodegrup, namabrg, satuan1, satuan2, satuan3, isi1, isi2, hrgbeli$fieldHarga)
                VALUES 
                ('$kodebrg', '$kodemerek', '$kodegolongan', '$kodegrup', '$namabrg', '$satuan1', '$satuan2', '$satuan3', $isi1, $isi2, $hargabeli$valueHarga)";
            break;

        case 'update':
            $query = "UPDATE zstok SET 
                kodebrg = '$kodebrg',
                kodemerk = '$kodemerek',
                kodegol = '$kodegolongan',
                kodegrup = '$kodegrup',
                namabrg = '$namabrg',
                satuan1 = '$satuan1',
                satuan2 = '$satuan2',
                satuan3 = '$satuan3',
                isi1 = $isi1,
                isi2 = $isi2,
                hrgbeli = $hargabeli
                $updateHarga
                WHERE kodebrg = '$kodebrg_lama'";
            break;

        case 'hapus':
            $query = "DELETE FROM zstok WHERE kodebrg = '$kodebrg'";
            break;

        default:
            echo json_encode(['status' => 'error', 'message' => 'Aksi tidak valid']);
            exit;
    }

    // --- Eksekusi query dan kirim respon ---
    if (mysqli_query($conn, $query)) {
        echo json_encode(['status' => 'success', 'aksi' => $aksi]);
    } else {
        echo json_encode(['status' => 'error', 'message' => mysqli_error($conn)]);
    }

    exit;
}
?>
