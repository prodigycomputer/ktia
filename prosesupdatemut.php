    <?php
    include 'koneksi.php';

    header('Content-Type: application/json');
    $data = json_decode(file_get_contents("php://input"), true);

    $detail = $data['detail'] ?? [];

    $no_nota = strtoupper($data['no_nota'] ?? '');
    $no_nota_lama = strtoupper($data['no_nota_lama'] ?? $no_nota);
    $tanggal = $data['tanggal'] ?? '';
    $kodegd1 = strtoupper($data['kodegd1'] ?? '');
    $kodegd2 = strtoupper($data['kodegd2'] ?? '');
    

    if (!$no_nota || !$tanggal || !$kodegd1 || !$kodegd2 || empty($detail)) {
        echo json_encode(['success' => false, 'message' => 'Data tidak lengkap']);
        exit;
    }

    try {
        $conn->begin_transaction();

        $hapusDetail = $conn->prepare("DELETE FROM zmutasim WHERE nonota = ?");
        $hapusDetail->bind_param("s", $no_nota_lama);
        $hapusDetail->execute();
        $hapusDetail->close();
        // Hapus detail lama
        $hapusHeader = $conn->prepare("DELETE FROM zmutasi WHERE nonota = ?");
        $hapusHeader->bind_param("s", $no_nota_lama);
        $hapusHeader->execute();
        $hapusHeader->close();
        
        // Insert detail baru
        $stmtHeader = $conn->prepare("
            INSERT INTO zmutasi 
            (nonota, tgl, kodegd1, kodegd2)
            VALUES (?, ?, ?, ?)
        ");

        $stmtHeader->bind_param("ssss", $no_nota, $tanggal, $kodegd1, $kodegd2);
        $stmtHeader->execute();
        $stmtHeader->close();

        $stmt = $conn->prepare("
            INSERT INTO zmutasim 
            (nonota, kodebrg, kodegd1, kodegd2, jlh1, jlh2, jlh3)
            VALUES (?, ?, ?, ?, ?, ?, ?)
        ");

        foreach ($detail as $item) {
            $kodebrg = strtoupper($item['kodebrg'] ?? '-');
            $jlh1    = intval($item['jlh1'] ?? 0);
            $jlh2    = intval($item['jlh2'] ?? 0);
            $jlh3    = intval($item['jlh3'] ?? 0);

            $stmt->bind_param(
                "ssssiii",
                $no_nota, // perbaikan: gunakan nonota baru
                $kodebrg,
                $kodegd1,
                $kodegd2,
                $jlh1,
                $jlh2,
                $jlh3
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
