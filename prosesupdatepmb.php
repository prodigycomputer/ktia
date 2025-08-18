    <?php
    include 'koneksi.php';

    header('Content-Type: application/json');
    $data = json_decode(file_get_contents("php://input"), true);

    $detail = $data['detail'] ?? [];

    $no_nota = strtoupper($data['no_nota'] ?? '');
    $no_nota_lama = strtoupper($data['no_nota_lama'] ?? $no_nota);
    $tanggal = $data['tanggal'] ?? '';
    $kode_sup = strtoupper($data['kode_sup'] ?? '');
    $kodegd = isset($detail[0]['kodegd']) ? strtoupper($detail[0]['kodegd']) : '';
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
    

    if (!$no_nota || !$tanggal || !$kode_sup || !$jt_tempo || empty($detail)) {
        echo json_encode(['success' => false, 'message' => 'Data tidak lengkap']);
        exit;
    }

    try {
        $conn->begin_transaction();

        $hapusDetail = $conn->prepare("DELETE FROM zbelim WHERE nonota = ?");
        $hapusDetail->bind_param("s", $no_nota_lama);
        $hapusDetail->execute();
        $hapusDetail->close();
        // Hapus detail lama
        $hapusHeader = $conn->prepare("DELETE FROM zbeli WHERE nonota = ?");
        $hapusHeader->bind_param("s", $no_nota_lama);
        $hapusHeader->execute();
        $hapusHeader->close();
        
        // Insert detail baru
        $stmtHeader = $conn->prepare("
            INSERT INTO zbeli 
            (nonota, tgl, kodesup, kodegd, nilai, tgltempo, ket, ppn, hppn, disc1, hdisc1, disc2, hdisc2, disc3, hdisc3)
            VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)
        ");

        $stmtHeader->bind_param("ssssdssdddddddd", $no_nota, $tanggal, $kode_sup, $kodegd, $totaljmlh, $jt_tempo, $ket, $prsnppn, $hrgppn, $disk1, $hdisk1, $disk2, $hdisk2, $disk3, $hdisk3);
        $stmtHeader->execute();
        $stmtHeader->close();

        $stmt = $conn->prepare("
            INSERT INTO zbelim 
            (nonota, kodebrg, kodegd, jlh1, jlh2, jlh3, harga, disca, discb, discc, discrp, jumlah, hdisca, hdiscb, hdiscc)
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
                $hdiscc
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
