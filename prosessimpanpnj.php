<?php
include 'koneksi.php';

header('Content-Type: application/json');
$data = json_decode(file_get_contents("php://input"), true);

$detail = $data['detail'] ?? [];

$nonota = strtoupper($data['nonota'] ?? '');
$tanggal = $data['tanggal'] ?? '';
$kodekust = strtoupper($data['kodekust'] ?? '');
$kodesls = strtoupper($data['kodesls'] ?? '');
$kodegd = isset($detail[0]['kodegd']) ? strtoupper($detail[0]['kodegd']) : '';
$tgljt = $data['jt_tempo'] ?? '';
$totaljmlh = $data['totaljmlh'] ?? 0;


if (!$nonota || !$tanggal || !$kodekust || !$kodesls || !$tgljt || !$kodegd || empty($detail)) {
    echo json_encode(['success' => false, 'message' => 'Data tidak lengkap']);
    exit;
}

$cek = $conn->prepare("SELECT COUNT(*) FROM zjual WHERE nonota = ?");
$cek->bind_param("s", $nonota);
$cek->execute();
$cek->bind_result($jumlah);
$cek->fetch();
$cek->close();

$conn->begin_transaction();

try {
    // Simpan ke zbeli (header) 
    $stmt = $conn->prepare("INSERT INTO zjual (nonota, tgl, kodekust, kodesls, kodegd, nilai, tgltempo) VALUES (?, ?, ?, ?, ?, ?, ?)");
    $stmt->bind_param("sssssds", $nonota, $tanggal, $kodekust, $kodesls, $kodegd, $totaljmlh, $tgljt);
    $stmt->execute();
    $stmt->close();

    // Simpan ke zbelim (detail)
    $stmt = $conn->prepare("
        INSERT INTO zjualm (nonota, kodebrg, kodegd, jlh1, jlh2, jlh3, harga, disca, discb, discc, discrp, jumlah, hdisca, hdiscb, hdiscc)
        VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)
    ");

    foreach ($detail as $item) {
        $kodebrg = strtoupper($item['kodebrg'] ?? '-');
        $kodegd = strtoupper($item['kodegd'] ?? '-');
        $jlh1    = intval($item['jlh1'] ?? 0);
        $jlh2    = intval($item['jlh2'] ?? 0);
        $jlh3    = intval($item['jlh3'] ?? 0);
        $harga   = floatval($item['harga'] ?? 0);
        $disca   = floatval($item['disca'] ?? 0);
        $discb   = floatval($item['discb'] ?? 0);
        $discc   = floatval($item['discc'] ?? 0);
        $discrp  = floatval($item['discrp'] ?? 0);
        $jumlah  = floatval($item['jumlah'] ?? 0);
        $hdisca = floatval($item['hdisca'] ?? 0);
        $hdiscb = floatval($item['hdiscb'] ?? 0);
        $hdiscc = floatval($item['hdiscc'] ?? 0);

        $stmt->bind_param(
            "sssiiiddddddddd",
            $nonota,
            $kodebrg,
            $kodegd,
            $jlh1,
            $jlh2,
            $jlh3,
            $harga,
            $disca,
            $discb,
            $discc,
            $discrp,
            $jumlah,
            $hdisca,
            $hdiscb,
            $hdiscc
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
