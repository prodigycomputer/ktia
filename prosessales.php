<?php
include 'koneksi.php';

if ($_SERVER['REQUEST_METHOD'] === 'POST') {
    error_reporting(E_ALL);
    ini_set('display_errors', 1);

    $aksi = $_POST['aksi'] ?? '';

    // --- Ambil data utama dari form ---
    $kodesls       = mysqli_real_escape_string($conn, trim($_POST['kodesls'] ?? ''));
    $kodesls_lama  = mysqli_real_escape_string($conn, trim($_POST['kodesls_lama'] ?? $kodesls));
    $namasls       = mysqli_real_escape_string($conn, trim($_POST['namasls'] ?? ''));
    $alamat       = mysqli_real_escape_string($conn, trim($_POST['alamat'] ?? '-'));
    $kota       = mysqli_real_escape_string($conn, trim($_POST['kota'] ?? '-'));
    $ktp       = mysqli_real_escape_string($conn, trim($_POST['ktp'] ?? '-'));
    $npwp       = mysqli_real_escape_string($conn, trim($_POST['npwp'] ?? '-'));


    // --- Proses sesuai aksi ---
    if ($aksi === 'tambah') {
        $cek = $conn->query("SELECT * FROM zsales WHERE kodesls='$kodesls'");
        if ($cek && $cek->num_rows > 0) {
            header("Location: sales.php?status=duplikat");
            exit;
        }

        $query = "INSERT INTO zsales 
        (kodesls, namasls, alamat, kota, ktp, npwp)
        VALUES 
        ('$kodesls', '$namasls', '$alamat', '$kota', '$ktp', '$npwp')";

    } elseif ($aksi === 'update') {
        $query = "UPDATE zsales SET 
            kodesls = '$kodesls',
            namasls = '$namasls',
            alamat = '$alamat',
            kota = '$kota',
            ktp = '$ktp',
            npwp = '$npwp'
            WHERE kodesls = '$kodesls_lama'";

    } elseif ($aksi === 'hapus') {
        $query = "DELETE FROM zsales WHERE kodesls='$kodesls'";
    } else {
        header("Location: sales.php?status=error");
        exit;
    }

    if (mysqli_query($conn, $query)) {
        header("Location: sales.php?status=$aksi");
    } else {
        echo "<h3>Query Gagal:</h3>";
        echo "<pre>$query</pre>";
        echo "<h4>MySQL Error:</h4>";
        echo mysqli_error($conn);
    }
    exit;
}
?>
