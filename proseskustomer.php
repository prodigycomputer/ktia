<?php
include 'koneksi.php';

if ($_SERVER['REQUEST_METHOD'] === 'POST') {
    error_reporting(E_ALL);
    ini_set('display_errors', 1);

    $aksi = $_POST['aksi'] ?? '';

    // --- Ambil data utama dari form ---
    $kodekust       = mysqli_real_escape_string($conn, trim($_POST['kodekust'] ?? ''));
    $kodekust_lama  = mysqli_real_escape_string($conn, trim($_POST['kodekust_lama'] ?? $kodekust));
    $namakust       = mysqli_real_escape_string($conn, trim($_POST['namakust'] ?? ''));
    $alamat       = mysqli_real_escape_string($conn, trim($_POST['alamat'] ?? '-'));
    $kota       = mysqli_real_escape_string($conn, trim($_POST['kota'] ?? '-'));
    $kodehrg       = mysqli_real_escape_string($conn, trim($_POST['kodehrg'] ?? '-'));
    $ktp       = mysqli_real_escape_string($conn, trim($_POST['ktp'] ?? '-'));
    $npwp       = mysqli_real_escape_string($conn, trim($_POST['npwp'] ?? '-'));


    // --- Proses sesuai aksi ---
    if ($aksi === 'tambah') {
        $cek = $conn->query("SELECT * FROM zkustomer WHERE kodekust='$kodekust'");
        if ($cek && $cek->num_rows > 0) {
            header("Location: kustomer.php?status=duplikat");
            exit;
        }

        $query = "INSERT INTO zkustomer 
        (kodekust, namakust, alamat, kota, kodehrg, ktp, npwp)
        VALUES 
        ('$kodekust', '$namakust', '$alamat', '$kota', '$kodehrg', '$ktp', '$npwp')";

    } elseif ($aksi === 'update') {
        $query = "UPDATE zkustomer SET 
            kodekust = '$kodekust',
            namakust = '$namakust',
            alamat = '$alamat',
            kota = '$kota',
            kodehrg = '$kodehrg',
            ktp = '$ktp',
            npwp = '$npwp'
            WHERE kodekust = '$kodekust_lama'";

    } elseif ($aksi === 'hapus') {
        $query = "DELETE FROM zkustomer WHERE kodekust='$kodekust'";
    } else {
        header("Location: kustomer.php?status=error");
        exit;
    }

    if (mysqli_query($conn, $query)) {
        header("Location: kustomer.php?status=$aksi");
    } else {
        echo "<h3>Query Gagal:</h3>";
        echo "<pre>$query</pre>";
        echo "<h4>MySQL Error:</h4>";
        echo mysqli_error($conn);
    }
    exit;
}
?>
