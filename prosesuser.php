<?php
include 'koneksi.php';
session_start();

header('Content-Type: application/json');
mysqli_report(MYSQLI_REPORT_ERROR | MYSQLI_REPORT_STRICT); // supaya error kelihatan

if ($_SERVER['REQUEST_METHOD'] === 'POST') {
    error_reporting(E_ALL);
    ini_set('display_errors', 1);

    $aksi = $_POST['aksi'] ?? '';
    $kodeuser = strtoupper(mysqli_real_escape_string($conn, trim($_POST['kodeuser'] ?? '')));
    $kodeuser_lama = strtoupper(mysqli_real_escape_string($conn, trim($_POST['kodeuser_lama'] ?? $kodeuser)));
    $namauser = strtoupper(mysqli_real_escape_string($conn, trim($_POST['namauser'] ?? '')));
    $kunci = strtoupper(mysqli_real_escape_string($conn, trim($_POST['passworduser'] ?? '')));

    // Ambil data akses dari JSON
    $aksesData = json_decode($_POST['akses'] ?? '[]', true);

    if (!$kodeuser || !$namauser || !$kunci) {
        echo json_encode(['status' => 'error', 'message' => 'Data tidak lengkap']);
        exit;
    }

    mysqli_begin_transaction($conn);

    try {
        switch ($aksi) {
            case 'tambah':
                $cek = mysqli_query($conn, "SELECT 1 FROM zusers WHERE kodeuser = '$kodeuser'");
                if (mysqli_num_rows($cek) > 0) {
                    echo json_encode(['status' => 'duplikat', 'message' => 'Kode user sudah ada']);
                    exit;
                }
                $query = "INSERT INTO zusers (kodeuser, username, kunci) VALUES ('$kodeuser', '$namauser', '$kunci')";
                break;

            case 'update':
                $query = "UPDATE zusers SET kodeuser = '$kodeuser', username = '$namauser', kunci = '$kunci' 
                          WHERE kodeuser = '$kodeuser_lama'";
                break;

            case 'hapus':
                // Hapus semua akses berdasarkan kodeuser_lama agar aman
                mysqli_query($conn, "DELETE FROM zakses WHERE kodeuser = '$kodeuser_lama'");
                
                // Hapus user dari zusers
                $query = "DELETE FROM zusers WHERE kodeuser = '$kodeuser_lama'";
                break;

            default:
                echo json_encode(['status' => 'error', 'message' => 'Aksi tidak valid']);
                exit;
        }

        // Eksekusi query user
        mysqli_query($conn, $query);

        // Simpan data akses kalau tambah/update
        if (in_array($aksi, ['tambah', 'update']) && is_array($aksesData)) {
            // Hapus akses lama
            mysqli_query($conn, "DELETE FROM zakses WHERE kodeuser = '".($aksi == 'update' ? $kodeuser_lama : $kodeuser)."'");

            $stmt = mysqli_prepare($conn, "INSERT INTO zakses (kodeuser, idmenu, tambah, ubah, hapus) VALUES (?, ?, ?, ?, ?)");
            foreach ($aksesData as $row) {
                $idmenu = $row['idmenu'] ?? '';
                $tambah = isset($row['tambah']) ? (int)$row['tambah'] : 0;
                $ubah   = isset($row['ubah']) ? (int)$row['ubah'] : 0;
                $hapus  = isset($row['hapus']) ? (int)$row['hapus'] : 0;
                mysqli_stmt_bind_param($stmt, "ssiii", $kodeuser, $idmenu, $tambah, $ubah, $hapus);
                mysqli_stmt_execute($stmt);
                if (mysqli_stmt_errno($stmt)) {
                    throw new Exception(mysqli_stmt_error($stmt));
                }
            }
            mysqli_stmt_close($stmt);
        }

        mysqli_commit($conn);
        echo json_encode(['status' => 'success', 'aksi' => $aksi]);
    } catch (Exception $e) {
        mysqli_rollback($conn);
        echo json_encode(['status' => 'error', 'message' => $e->getMessage()]);
    }
    exit;
}
