    <?php
    include 'koneksi.php';

    header('Content-Type: application/json');
    $data = json_decode(file_get_contents("php://input"), true);

    $no_nota = strtoupper($data['no_nota'] ?? '');
    $no_nota_lama = strtoupper($data['no_nota_lama'] ?? $no_nota);
    $tanggal = $data['tanggal'] ?? '';
    $kode_sup = strtoupper($data['kode_sup'] ?? '');
    $jt_tempo = $data['jt_tempo'] ?? '';
    $totaljmlh = $data['totaljmlh'] ?? 0;
    $detail = $data['detail'] ?? [];

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
            (nonota, tgl, kodesup, nilai, tgltempo)
            VALUES (?, ?, ?, ?, ?)
        ");

        $stmtHeader->bind_param("sssds", $no_nota, $tanggal, $kode_sup, $totaljmlh, $jt_tempo);
        $stmtHeader->execute();
        $stmtHeader->close();

        $stmt = $conn->prepare("
            INSERT INTO zbelim 
            (nonota, kodebrg, jlh1, jlh2, jlh3, harga, disca, discb, discc, discrp, jumlah, hdisca, hdiscb, hdiscc)
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
                $no_nota, // perbaikan: gunakan nonota baru
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
        echo json_encode(['success' => false, 'message' => 'Gagal update: ' . $e->getMessage()]);
    }
    ?>
