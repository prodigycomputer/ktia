<?php
include 'koneksi.php';

if ($_SERVER['REQUEST_METHOD'] === 'POST') {
    error_reporting(E_ALL);
    ini_set('display_errors', 1);

    $aksi = $_POST['aksi'] ?? '';

    // --- Ambil semua field kolom harga (harga1, harga11, dst.) ---
    // ---$hargaData = [];
    //$resField = $conn->query("SHOW COLUMNS FROM zkustomer WHERE Field LIKE 'harga%'");
    //while ($row = $resField->fetch_assoc()) {
    //    $field = $row['Field'];
    //    if (isset($_POST[$field]) && is_numeric($_POST[$field])) {
    //        $hargaData[$field] = floatval($_POST[$field]);
    //    } else {
    //        $hargaData[$field] = 0;
    //    }
    //}

    // --- Ambil data utama dari form ---
    $kodebrg       = mysqli_real_escape_string($conn, trim($_POST['kodebrg'] ?? ''));
    $kodebrg_lama  = mysqli_real_escape_string($conn, trim($_POST['kodebrg_lama'] ?? $kodebrg));
    $kodegrup      = mysqli_real_escape_string($conn, trim($_POST['searchGrup'] ?? ''));
    $namabrg       = mysqli_real_escape_string($conn, trim($_POST['namabrg'] ?? ''));
    $satuan1       = mysqli_real_escape_string($conn, trim($_POST['satuan1'] ?? '-'));
    $satuan2       = mysqli_real_escape_string($conn, trim($_POST['satuan2'] ?? '-'));
    $satuan3       = mysqli_real_escape_string($conn, trim($_POST['satuan3'] ?? '-'));
    $isi1          = is_numeric($_POST['isi1']) ? floatval($_POST['isi1']) : 1;
    $isi2          = is_numeric($_POST['isi2']) ? floatval($_POST['isi2']) : 1;

    $fieldHarga = '';
    $valueHarga = '';
    $updateHarga = '';
    foreach ($hargaData as $field => $val) {
        $fieldHarga  .= ", `$field`";
        $valueHarga  .= ", $val";
        $updateHarga .= ", `$field` = $val";
    }

    // --- Proses sesuai aksi ---
    if ($aksi === 'tambah') {
        $cek = $conn->query("SELECT * FROM zstok WHERE kodebrg='$kodebrg'");
        if ($cek && $cek->num_rows > 0) {
            header("Location: barang.php?status=duplikat");
            exit;
        }

        $query = "INSERT INTO zstok 
        (kodebrg, kodegrup, namabrg, satuan1, satuan2, satuan3, isi1, isi2$fieldHarga)
        VALUES 
        ('$kodebrg', '$kodegrup', '$namabrg', '$satuan1', '$satuan2', '$satuan3', $isi1, $isi2$valueHarga)";

    } elseif ($aksi === 'update') {
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

    } elseif ($aksi === 'hapus') {
        $query = "DELETE FROM zstok WHERE kodebrg='$kodebrg'";
    } else {
        header("Location: barang.php?status=error");
        exit;
    }

    if (mysqli_query($conn, $query)) {
        header("Location: barang.php?status=$aksi");
    } else {
        echo "<h3>Query Gagal:</h3>";
        echo "<pre>$query</pre>";
        echo "<h4>MySQL Error:</h4>";
        echo mysqli_error($conn);
    }
    exit;
}
?>
