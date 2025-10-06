<?php
include 'koneksi.php';

header('Content-Type: application/json');
$data = json_decode(file_get_contents("php://input"), true);

$detail = $data['detail'] ?? [];

$no_nota = strtoupper($data['no_nota'] ?? '');
$tanggal = $data['tanggal'] ?? '';
$kodegd1 = strtoupper($data['kodegd1'] ?? '');
$kodegd2 = strtoupper($data['kodegd2'] ?? '');
$kodebrg = isset($detail[0]['kodebrg']) ? strtoupper($detail[0]['kodebrg']) : '';
$operator = $data['operator'] ?? '';


if (!$no_nota || !$tanggal || !$kodegd1 || !$kodegd2 || empty($detail)) {
    echo json_encode(['success' => false, 'message' => 'Data tidak lengkap']);
    exit;
}

$cek = $conn->prepare("SELECT COUNT(*) FROM zmutasi WHERE nonota = ?");
$cek->bind_param("s", $no_nota);
$cek->execute();
$cek->bind_result($jumlah);
$cek->fetch();
$cek->close();

$cekSaldo = $conn->prepare("SELECT COUNT(*) FROM zsaldo WHERE kodebrg = ? AND kodegd = ?");
$cekSaldo->bind_param("ss", $kodebrg, $kodegd1);
$cekSaldo->execute();
$cekSaldo->bind_result($ada1);
$cekSaldo->fetch();
$cekSaldo->close();

$cekSaldo = $conn->prepare("SELECT COUNT(*) FROM zsaldo WHERE kodebrg = ? AND kodegd = ?");
$cekSaldo->bind_param("ss", $kodebrg, $kodegd2);
$cekSaldo->execute();
$cekSaldo->bind_result($ada2);
$cekSaldo->fetch();
$cekSaldo->close();

$conn->begin_transaction();

try {
    // Simpan ke zbeli (header) 
    $stmt = $conn->prepare("INSERT INTO zmutasi (nonota, tgl, kodegd1, kodegd2, operator, logtime) VALUES (?, ?, ?, ?, ?, NOW())");
    $stmt->bind_param("sssss", $no_nota, $tanggal, $kodegd1, $kodegd2, $operator);
    $stmt->execute();
    $stmt->close();

    if ($ada1 == 0) {
        $stmt = $conn->prepare("INSERT INTO zsaldo (kodebrg, kodegd) VALUES (?, ?)");
        $stmt->bind_param("ss", $kodebrg, $kodegd1);
        $stmt->execute();
        $stmt->close();
    }

    if ($ada2 == 0) {
        $stmt = $conn->prepare("INSERT INTO zsaldo (kodebrg, kodegd) VALUES (?, ?)");
        $stmt->bind_param("ss", $kodebrg, $kodegd2);
        $stmt->execute();
        $stmt->close();
    }

    // Simpan ke zbelim (detail)
    $stmt = $conn->prepare("
        INSERT INTO zmutasim (nonota, kodebrg, kodegd1, kodegd2, jlh1, jlh2, jlh3, operator, logtime)
        VALUES (?, ?, ?, ?, ?, ?, ?, ?, NOW())
    ");

    foreach ($detail as $item) {
        $kodebrg = strtoupper($item['kodebrg'] ?? '-');
        $jlh1    = intval($item['jlh1'] ?? 0);
        $jlh2    = intval($item['jlh2'] ?? 0);
        $jlh3    = intval($item['jlh3'] ?? 0);

        $stmt->bind_param(
            "ssssiiis",
            $no_nota,
            $kodebrg,
            $kodegd1,
            $kodegd2,
            $jlh1,
            $jlh2,
            $jlh3,
            $operator
        );
        $stmt->execute();
    }

    $stmt->close();
    $conn->commit();
    echo json_encode(['success' => true]);
} catch (Exception $e) {
    $conn->rollback();
    echo json_encode(['success' => false, 'message' => $e->getMessage()]);
}
?>
