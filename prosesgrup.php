<?php
include 'koneksi.php';
session_start();

if ($_SERVER['REQUEST_METHOD'] === 'POST') {
    error_reporting(E_ALL);
    ini_set('display_errors', 1);
    $aksi = $_POST['aksi'];
    $kodegrup = strtoupper(mysqli_real_escape_string($conn, trim($_POST['kodegrup'] ?? '')));
    $kodegrup_lama = strtoupper(mysqli_real_escape_string($conn, trim($_POST['kodegrup_lama'] ?? $kodegrup)));
    $namagrup = strtoupper(mysqli_real_escape_string($conn, trim($_POST['namagrup'] ?? '')));

    if (!$kodegrup || !$namagrup) {
        header("Location: group.php?status=error");
        exit;
    }

    switch ($aksi) {
        case 'tambah':
            $cek = mysqli_query($conn, "SELECT * FROM zgrup WHERE kodegrup = '$kodegrup'");
            if (mysqli_num_rows($cek) > 0) {
                header("Location: group.php?status=duplikat");
                exit;
            }
            $query = "INSERT INTO zgrup (kodegrup, namagrup) VALUES ('$kodegrup', '$namagrup')";
            break;

        case 'update':
            $query = "UPDATE zgrup SET kodegrup = '$kodegrup', namagrup = '$namagrup' WHERE kodegrup = '$kodegrup_lama'";
            break;

        case 'hapus':
            $query = "DELETE FROM zgrup WHERE kodegrup = '$kodegrup'";
            break;

        default:
            header("Location: group.php?status=error");
            exit;
    }

    if (mysqli_query($conn, $query)) {
        header("Location: group.php?status=$aksi");
    } else {
        header("Location: group.php?status=error");
    }
    exit;
}
?>
