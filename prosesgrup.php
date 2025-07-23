<?php
include 'koneksi.php';
session_start();

if ($_SERVER['REQUEST_METHOD'] === 'POST') {
    $aksi = $_POST['aksi'] ?? '';
    $kodegrup = trim($_POST['kodegrup'] ?? '');
    $namagrup = trim($_POST['namagrup'] ?? '');

    try {
        if ($aksi === 'tambah') {
            // Cek apakah kodegrup sudah ada
            $cek = mysqli_query($conn, "SELECT * FROM zgrup WHERE kodegrup = '$kodegrup'");
            if (mysqli_num_rows($cek) > 0) {
                throw new Exception("Kode grup '$kodegrup' sudah digunakan.");
            }

            $query = "INSERT INTO zgrup (kodegrup, namagrup) VALUES ('$kodegrup', '$namagrup')";
            mysqli_query($conn, $query);
            $_SESSION['notif'] = ['type' => 'success', 'message' => 'Data berhasil ditambahkan.'];

        } elseif ($aksi === 'update') {
            $query = "UPDATE zgrup SET namagrup='$namagrup' WHERE kodegrup='$kodegrup'";
            mysqli_query($conn, $query);
            $_SESSION['notif'] = ['type' => 'success', 'message' => 'Data berhasil diperbarui.'];

        } elseif ($aksi === 'hapus') {
            $query = "DELETE FROM zgrup WHERE kodegrup='$kodegrup'";
            mysqli_query($conn, $query);
            $_SESSION['notif'] = ['type' => 'success', 'message' => 'Data berhasil dihapus.'];

        } else {
            throw new Exception("Aksi tidak valid!");
        }

    } catch (Exception $e) {
        $_SESSION['notif'] = ['type' => 'error', 'message' => 'Terjadi kesalahan: ' . $e->getMessage()];
    }

    header("Location: group.php");
    exit;
}
?>
