<?php
session_start();
include 'koneksi.php';
header('Content-Type: application/json');

if ($_SERVER['REQUEST_METHOD'] === 'POST') {
    $aksi = $_POST['aksi'] ?? '';

    if ($aksi === 'hapus') {
        $nonota = $_POST['nonota'] ?? '';
        if (!empty($nonota)) {
            $stmt = $conn->prepare("DELETE FROM zbeli WHERE nonota = ?");
            $stmt->bind_param("s", $nonota);
            $success = $stmt->execute();
            $stmt->close();

            echo json_encode([
                'success' => $success,
                'message' => $success ? 'Data berhasil dihapus!' : 'Gagal menghapus data.'
            ]);
        } else {
            echo json_encode([
                'success' => false,
                'message' => 'Nomor nota tidak ditemukan.'
            ]);
        }
        exit;
    }

    // Tambahkan aksi lain: simpan/edit
}
