<?php
include 'koneksi.php';

header('Content-Type: application/json');
$data = json_decode(file_get_contents("php://input"), true);

$no_nota = strtoupper($data['no_nota'] ?? '');
$tanggal = $data['tanggal'] ?? '';
$kode_sup = strtoupper($data['kode_sup'] ?? '');
$tgltempo = $data['jt_tempo'] ?? '';
$totaljmlh = $data['totaljmlh'] ?? 0;
$detail = $data['detail'] ?? [];

if (!$no_nota || !$tanggal || !$kode_sup || empty($detail)) {
    echo json_encode(['success' => false, 'message' => 'Data tidak lengkap']);
    exit;
}

$cek = $conn->prepare("SELECT COUNT(*) FROM zbeli WHERE nonota = ?");
$cek->bind_param("s", $no_nota);
$cek->execute();
$cek->bind_result($jumlah);
$cek->fetch();
$cek->close();

if ($jumlah > 0) {
    echo json_encode(['success' => false, 'message' => 'Nomor nota sudah terdaftar!']);
    exit;
}

$conn->begin_transaction();

try {
    // Simpan ke zbeli (header)
    $stmt = $conn->prepare("INSERT INTO zbeli (nonota, tgl, kodesup, nilai, lunas, tgltempo) VALUES (?, ?, ?, ?, 0, ?)");
    $stmt->bind_param("sssds", $no_nota, $tanggal, $kode_sup, $totaljmlh, $tgltempo);
    $stmt->execute();
    $stmt->close();

    // Simpan ke zbelim (detail)
    $stmt = $conn->prepare("
        INSERT INTO zbelim (nonota, kodebrg, jlh1, jlh2, jlh3, harga, disca, discb, discc, discrp, jumlah, hdisca, hdiscb, hdiscc)
        VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, 0, 0, 0)
    ");

    foreach ($detail as $item) {
        $kodebrg = strtoupper($item['kodebrg'] ?? '-');
        $jlh1    = intval($item['jlh1'] ?? 0);
        $jlh2    = intval($item['jlh2'] ?? 0);
        $jlh3    = intval($item['jlh3'] ?? 0);
        $harga   = floatval($item['harga'] ?? 0);
        $disca   = floatval($item['disca'] ?? 0);
        $discb   = floatval($item['discb'] ?? 0);
        $discc   = floatval($item['discc'] ?? 0);
        $discrp  = floatval($item['discrp'] ?? 0);
        $jumlah  = floatval($item['jumlah'] ?? 0);

        $stmt->bind_param(
            "ssiiidddddd",
            $no_nota,
            $kodebrg,
            $jlh1,
            $jlh2,
            $jlh3,
            $harga,
            $disca,
            $discb,
            $discc,
            $discrp,
            $jumlah
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
