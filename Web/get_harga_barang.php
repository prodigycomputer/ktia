<?php
include 'koneksi.php';

$kodebrg = $_GET['kodebrg'] ?? '';
$kodehrg = $_GET['kodehrg'] ?? '';

if (!$kodebrg || !$kodehrg) {
    echo json_encode([]);
    exit;
}

// Ambil semua kolom harga dari zstok
$q = $conn->prepare("SELECT harga1, harga2, harga3, harga4, harga5, harga6,
                            harga11, harga22, harga33, harga44, harga55, harga66,
                            harga111, harga222, harga333, harga444, harga555, harga666
                     FROM zstok WHERE kodebrg = ?");
$q->bind_param("s", $kodebrg);
$q->execute();
$result = $q->get_result()->fetch_assoc();

$opsi = [];

// buat list harga berdasarkan kodehrg yang diinput user
for ($i = 1; $i <= 3; $i++) {
    $kolom = "harga" . str_repeat($kodehrg, $i);
    if (isset($result[$kolom])) {
        $opsi[] = [
            "label" => $kolom,
            "value" => $result[$kolom]
        ];
    }
}

echo json_encode($opsi);
