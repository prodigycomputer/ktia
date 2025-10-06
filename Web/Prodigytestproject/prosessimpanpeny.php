<?php
include 'koneksi.php';

header('Content-Type: application/json');
$data = json_decode(file_get_contents("php://input"), true);

$detail = $data['detail'] ?? [];

$no_nota = strtoupper($data['no_nota'] ?? '');
$tanggal = $data['tanggal'] ?? '';
$kodegd = strtoupper($data['kodegd'] ?? '');
$kodebrg = isset($detail[0]['kodebrg']) ? strtoupper($detail[0]['kodebrg']) : '';
$operator = $data['operator'] ?? '';


if (!$no_nota || !$tanggal || !$kodegd || empty($detail)) {
    echo json_encode(['success' => false, 'message' => 'Data tidak lengkap']);
    exit;
}

$cek = $conn->prepare("SELECT COUNT(*) FROM zpenyesuaian WHERE nonota = ?");
$cek->bind_param("s", $no_nota);
$cek->execute();
$cek->bind_result($jumlah);
$cek->fetch();
$cek->close();

$cekSaldo = $conn->prepare("SELECT COUNT(*) FROM zsaldo WHERE kodebrg = ? AND kodegd = ?");
$cekSaldo->bind_param("ss", $kodebrg, $kodegd);
$cekSaldo->execute();
$cekSaldo->bind_result($ada);
$cekSaldo->fetch();
$cekSaldo->close();

$conn->begin_transaction();

try {
    // Simpan ke zbeli (header) 
    $stmt = $conn->prepare("INSERT INTO zpenyesuaian (nonota, tgl, kodegd, operator, logtime) VALUES (?, ?, ?, ?, NOW())");
    $stmt->bind_param("ssss", $no_nota, $tanggal, $kodegd, $operator);
    $stmt->execute();
    $stmt->close();

    if ($ada == 0) {
        $stmt = $conn->prepare("INSERT INTO zsaldo (kodebrg, kodegd) VALUES (?, ?)");
        $stmt->bind_param("ss", $kodebrg, $kodegd);
        $stmt->execute();
        $stmt->close();
    }

    // Simpan ke zbelim (detail)
    $stmt = $conn->prepare("
        INSERT INTO zpenyesuaianm (nonota, kodebrg, kodegd, jlh1, jlh2, jlh3, qty, harga, operator, logtime)
        VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, NOW())
    ");

    foreach ($detail as $item) {
        $kodebrg = strtoupper($item['kodebrg'] ?? '-');
        $jlh1    = intval($item['jlh1'] ?? 0);
        $jlh2    = intval($item['jlh2'] ?? 0);
        $jlh3    = intval($item['jlh3'] ?? 0);
        $qty    = intval($item['qty'] ?? 0);
        $harga    = intval($item['harga'] ?? 0);

        $stmt->bind_param(
            "sssiiiiis",
            $no_nota,
            $kodebrg,
            $kodegd,
            $jlh1,
            $jlh2,
            $jlh3,
            $qty,
            $harga,
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
