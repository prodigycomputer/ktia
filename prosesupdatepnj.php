<?php
include 'koneksi.php';

header('Content-Type: application/json');
$data = json_decode(file_get_contents("php://input"), true);

$detail = $data['detail'] ?? [];

$no_nota = strtoupper($data['no_nota'] ?? '');
$no_nota_lama = strtoupper($data['no_nota_lama'] ?? $no_nota);
$tanggal = $data['tanggal'] ?? '';
$kodekust = strtoupper($data['kode_kust'] ?? '');
$kodesls = strtoupper($data['kode_sls'] ?? '');
$kodegd = isset($detail[0]['kodegd']) ? strtoupper($detail[0]['kodegd']) : '';
$kodebrg = isset($detail[0]['kodebrg']) ? strtoupper($detail[0]['kodebrg']) : '';
$jt_tempo = $data['jt_tempo'] ?? '';
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

if (!$no_nota || !$tanggal || !$kodekust || !$kodesls || !$jt_tempo || empty($detail)) {
    echo json_encode(['success' => false, 'message' => 'Data tidak lengkap']);
    exit;
}

$cekSaldo = $conn->prepare("SELECT COUNT(*) FROM zsaldo WHERE kodebrg = ? AND kodegd = ?");
$cekSaldo->bind_param("ss", $kodebrg, $kodegd);
$cekSaldo->execute();
$cekSaldo->bind_result($ada);
$cekSaldo->fetch();
$cekSaldo->close();

try {
    $conn->begin_transaction();

    $oldData = [];
    $result = $conn->prepare("SELECT kodebrg, kodegd, jlh1, jlh2, jlh3, harga, disca, discb, discc, discrp, jumlah, hdisca, hdiscb, hdiscc, logtime, operator 
                              FROM zjualm WHERE nonota = ?");
    $result->bind_param("s", $no_nota_lama);
    $result->execute();
    $res = $result->get_result();
    while ($row = $res->fetch_assoc()) {
        $key = $row['kodebrg'].'|'.$row['kodegd'];
        $oldData[$key] = $row;
    }
    $result->close();

    // Ambil data lama header (operator & logtime)
    $oldHeader = [];
    $stmtOldHeader = $conn->prepare("SELECT operator, logtime FROM zjual WHERE nonota = ?");
    $stmtOldHeader->bind_param("s", $no_nota_lama);
    $stmtOldHeader->execute();
    $resHeader = $stmtOldHeader->get_result();
    if ($row = $resHeader->fetch_assoc()) {
        $oldHeader = $row;
    }
    $stmtOldHeader->close();

    $hapusDetail = $conn->prepare("DELETE FROM zjualm WHERE nonota = ?");
    $hapusDetail->bind_param("s", $no_nota_lama);
    $hapusDetail->execute();
    $hapusDetail->close();
    // Hapus detail lama
    $hapusHeader = $conn->prepare("DELETE FROM zjual WHERE nonota = ?");
    $hapusHeader->bind_param("s", $no_nota_lama);
    $hapusHeader->execute();
    $hapusHeader->close();

    $operatorToSave = $oldHeader['operator'] ?? $operator;
    $logtimeToSave  = $oldHeader['logtime'] ?? date('Y-m-d H:i:s');
    
    // Insert detail baru
    $stmtHeader = $conn->prepare("
        INSERT INTO zjual 
        (nonota, tgl, kodekust, kodesls, kodegd, nilai, tgltempo, ket, ppn, hppn, disc1, hdisc1, disc2, hdisc2, disc3, hdisc3, operator, logtime)
        VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)
    ");

    $stmtHeader->bind_param("sssssdssddddddddss", $no_nota, $tanggal, $kodekust, $kodesls, $kodegd, $totaljmlh, $jt_tempo, $ket, $prsnppn, $hrgppn, $disk1, $hdisk1, $disk2, $hdisk2, $disk3, $hdisk3, $operatorToSave, $logtimeToSave);
    $stmtHeader->execute();
    $stmtHeader->close();

    if ($ada == 0) {
        $stmt = $conn->prepare("INSERT INTO zsaldo (kodebrg, kodegd) VALUES (?, ?)");
        $stmt->bind_param("ss", $kodebrg, $kodegd);
        $stmt->execute();
        $stmt->close();
    }

    $stmt = $conn->prepare("
        INSERT INTO zjualm 
        (nonota, kodebrg, kodegd, jlh1, jlh2, jlh3, harga, disca, discb, discc, discrp, jumlah, hdisca, hdiscb, hdiscc, operator, logtime)
        VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)
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

        $key = $kodebrg.'|'.$kodegd;
        $logtimeItem = date('Y-m-d H:i:s');
        $operatorItem = $operator;

        if (isset($oldData[$key])) {
            $old = $oldData[$key];
            if (
                $old['jlh1'] == $jlh1 && $old['jlh2'] == $jlh2 && $old['jlh3'] == $jlh3 &&
                $old['harga'] == $harga && $old['disca'] == $disca && $old['discb'] == $discb &&
                $old['discc'] == $discc && $old['discrp'] == $discrp && $old['jumlah'] == $jumlah &&
                $old['hdisca'] == $hdisca && $old['hdiscb'] == $hdiscb && $old['hdiscc'] == $hdiscc
            ) {
                $logtimeItem = $old['logtime'];
                $operatorItem = $old['operator'];
            }
        }

        $stmt->bind_param(
            "sssiiidddddddddss",
            $no_nota, // perbaikan: gunakan nonota baru
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
            $operatorItem, 
            $logtimeItem
        );
        $stmt->execute();
    }

    $stmt->close();
    $conn->commit();

    echo json_encode(['success' => true]);
} catch (Exception $e) {
    $conn->rollback();
    echo json_encode(['success' => false, 'message' => 'Gagal update: ' . $e->getMessage()]);
}
?>
