<?php
include 'koneksi.php';

if ($_SERVER['REQUEST_METHOD'] === 'POST') {
    error_reporting(E_ALL);
    ini_set('display_errors', 1);

    $aksi = $_POST['aksi'] ?? '';

    // --- Ambil data utama dari form ---
    $kodesup       = strtoupper(mysqli_real_escape_string($conn, trim($_POST['kodesup'] ?? '')));
    $kodesup_lama  = strtoupper(mysqli_real_escape_string($conn, trim($_POST['kodesup_lama'] ?? $kodesup)));
    $namasup       = strtoupper(mysqli_real_escape_string($conn, trim($_POST['namasup'] ?? '')));
    $alamat       = strtoupper(mysqli_real_escape_string($conn, trim($_POST['alamat'] ?? '-')));
    $kota       = strtoupper(mysqli_real_escape_string($conn, trim($_POST['kota'] ?? '-')));
    $ktp       = strtoupper(mysqli_real_escape_string($conn, trim($_POST['ktp'] ?? '-')));
    $npwp       = strtoupper(mysqli_real_escape_string($conn, trim($_POST['npwp'] ?? '-')));


    // --- Proses sesuai aksi ---
    if ($aksi === 'tambah') {
        $cek = $conn->query("SELECT * FROM zsupplier WHERE kodesup='$kodesup'");
        if ($cek && $cek->num_rows > 0) {
            header("Location: supplier.php?status=duplikat");
            exit;
        }

        $query = "INSERT INTO zsupplier 
        (kodesup, namasup, alamat, kota, ktp, npwp)
        VALUES 
        ('$kodesup', '$namasup', '$alamat', '$kota', '$ktp', '$npwp')";

    } elseif ($aksi === 'update') {
        $query = "UPDATE zsupplier SET 
            kodesup = '$kodesup',
            namasup = '$namasup',
            alamat = '$alamat',
            kota = '$kota',
            ktp = '$ktp',
            npwp = '$npwp'
            WHERE kodesup = '$kodesup_lama'";

    } elseif ($aksi === 'hapus') {
        $query = "DELETE FROM zsupplier WHERE kodesup='$kodesup'";
    } else {
        header("Location: supplier.php?status=error");
        exit;
    }

    if (mysqli_query($conn, $query)) {
        header("Location: supplier.php?status=$aksi");
    } else {
        echo "<h3>Query Gagal:</h3>";
        echo "<pre>$query</pre>";
        echo "<h4>MySQL Error:</h4>";
        echo mysqli_error($conn);
    }
    exit;
}
?>
