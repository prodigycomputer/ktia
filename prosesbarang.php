<?php
include 'koneksi.php';

if ($_SERVER['REQUEST_METHOD'] === 'POST') {
    $aksi = $_POST['aksi'] ?? '';

    $kodebrg  = trim($_POST['kodebrg'] ?? '');
    $namabrg  = trim($_POST['namabrg'] ?? '');
    $harga1   = floatval($_POST['harga1'] ?? 0);
    $harga2   = floatval($_POST['harga2'] ?? 0);
    $harga3   = floatval($_POST['harga3'] ?? 0);
    $harga4   = floatval($_POST['harga4'] ?? 0);
    $harga5   = floatval($_POST['harga5'] ?? 0);
    $harga6   = floatval($_POST['harga6'] ?? 0);

    $satuan1  = trim($_POST['satuan1'] ?? '-');
    $satuan2  = trim($_POST['satuan2'] ?? '-');
    $satuan3  = trim($_POST['satuan3'] ?? '-');
    $isi1     = floatval($_POST['isi1'] ?? 1);
    $isi2     = floatval($_POST['isi2'] ?? 1);

    if ($aksi === 'tambah') {
        $cek = mysqli_query($conn, "SELECT * FROM zstok WHERE kodebrg='$kodebrg'");
        if (mysqli_num_rows($cek) > 0) {
            header("Location: barang.php?status=duplikat");
            exit;
        }

        $query = "INSERT INTO zstok 
        (kodebrg, namabrg, harga1, harga2, harga3, harga4, harga5, harga6, satuan1, satuan2, satuan3, isi1, isi2) 
        VALUES 
        ('$kodebrg', '$namabrg', $harga1, $harga2, $harga3, $harga4, $harga5, $harga6,
        '$satuan1', '$satuan2', '$satuan3', $isi1, $isi2)";
    } elseif ($aksi === 'update') {
        $query = "UPDATE zstok SET 
            namabrg='$namabrg',
            harga1=$harga1, harga2=$harga2, harga3=$harga3,
            harga4=$harga4, harga5=$harga5, harga6=$harga6,
            satuan1='$satuan1', satuan2='$satuan2', satuan3='$satuan3',
            isi1=$isi1, isi2=$isi2
            WHERE kodebrg='$kodebrg'";
    } elseif ($aksi === 'hapus') {
        $query = "DELETE FROM zstok WHERE kodebrg='$kodebrg'";
    } else {
        header("Location: barang.php?status=error");
        exit;
    }

    if (mysqli_query($conn, $query)) {
        header("Location: barang.php?status=$aksi");
    } else {
        header("Location: barang.php?status=error");
    }
    exit;
}
?>
