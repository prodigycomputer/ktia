<?php
include 'koneksi.php';
session_start();

if ($_SERVER['REQUEST_METHOD'] === 'POST') {
    $aksi = $_POST['aksi'] ?? '';
    $kodegrup = trim($_POST['kodegrup'] ?? '');
    $namagrup = trim($_POST['namagrup'] ?? '');

    if (!$kodegrup || !$namagrup) {
        header("Location: group.php?status=error");
        exit;
    }

    switch ($aksi) {
        case 'tambah':
            $cek = mysqli_query($conn, "SELECT 1 FROM zgrup WHERE kodegrup = '$kodegrup'");
            if (mysqli_num_rows($cek) > 0) {
                header("Location: group.php?status=duplikat");
                exit;
            }
            $query = "INSERT INTO zgrup (kodegrup, namagrup) VALUES ('$kodegrup', '$namagrup')";
            break;

        case 'update':
            $query = "UPDATE zgrup SET namagrup = '$namagrup' WHERE kodegrup = '$kodegrup'";
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
