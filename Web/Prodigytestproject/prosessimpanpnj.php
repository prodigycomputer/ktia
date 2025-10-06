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
$kodebrg = isset($detail[0]['kodebrg']) ? strtoupper($detail[0]['kodebrg']) : '';
$tgljt = $data['jt_tempo'] ?? '';
$ket = strtoupper($data['keterangan'] ?? '');
$totaljmlh = floatval($data['totaljmlh']) ?? 0;
$prsnppn = $data['prsnppn'] ?? 0;
$hrgppn = floatval($data['hrgppn']) ?? 0;
$disk1 = $data['disk1'] ?? 0;
$hdisk1 = floatval($data['hdisk1']) ?? 0;
$disk2 = $data['disk2'] ?? 0;
$hdisk2 = floatval($data['hdisk2']) ?? 0;
$disk3 = $data['disk3'] ?? 0;
$hdisk3 = floatval($data['hdisk3']) ?? 0;
$operator = $data['operator'] ?? '';


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

$cekSaldo = $conn->prepare("SELECT COUNT(*) FROM zsaldo WHERE kodebrg = ? AND kodegd = ?");
$cekSaldo->bind_param("ss", $kodebrg, $kodegd);
$cekSaldo->execute();
$cekSaldo->bind_result($ada);
$cekSaldo->fetch();
$cekSaldo->close();

$conn->begin_transaction();

try {
    $stmt = $conn->prepare("INSERT INTO zjual (nonota, tgl, kodekust, kodesls, kodegd, nilai, tgltempo, ket, ppn, hppn, disc1, hdisc1, disc2, hdisc2, disc3, hdisc3, operator, logtime) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, NOW())");
    $stmt->bind_param("sssssdssdddddddds", $nonota, $tanggal, $kodekust, $kodesls, $kodegd, $totaljmlh, $tgljt, $ket, $prsnppn, $hrgppn, $disk1, $hdisk1, $disk2, $hdisk2, $disk3, $hdisk3, $operator);
    $stmt->execute();
    $stmt->close();

    if ($ada == 0) {
        $stmt = $conn->prepare("INSERT INTO zsaldo (kodebrg, kodegd) VALUES (?, ?)");
        $stmt->bind_param("ss", $kodebrg, $kodegd);
        $stmt->execute();
        $stmt->close();
    }
    
    $stmt = $conn->prepare("
        INSERT INTO zjualm (nonota, kodebrg, kodegd, jlh1, jlh2, jlh3, harga, disca, discb, discc, discrp, jumlah, hdisca, hdiscb, hdiscc, operator, logtime)
        VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, NOW())
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
            "sssiiiddddddddds",
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
            $hdiscc,
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
