    <?php
    include 'koneksi.php';

    header('Content-Type: application/json');
    $data = json_decode(file_get_contents("php://input"), true);

    $detail = $data['detail'] ?? [];

    $no_nota = strtoupper($data['no_nota'] ?? '');
    $no_nota_lama = strtoupper($data['no_nota_lama'] ?? $no_nota);
    $tanggal = $data['tanggal'] ?? '';
    $kodegd = strtoupper($data['kodegd'] ?? '');
    

    if (!$no_nota || !$tanggal || !$kodegd || empty($detail)) {
        echo json_encode(['success' => false, 'message' => 'Data tidak lengkap']);
        exit;
    }

    try {
        $conn->begin_transaction();

        $hapusDetail = $conn->prepare("DELETE FROM zpenyesuaianm WHERE nonota = ?");
        $hapusDetail->bind_param("s", $no_nota_lama);
        $hapusDetail->execute();
        $hapusDetail->close();
        // Hapus detail lama
        $hapusHeader = $conn->prepare("DELETE FROM zpenyesuaian WHERE nonota = ?");
        $hapusHeader->bind_param("s", $no_nota_lama);
        $hapusHeader->execute();
        $hapusHeader->close();
        
        // Insert detail baru
        $stmtHeader = $conn->prepare("
            INSERT INTO zpenyesuaian 
            (nonota, tgl, kodegd)
            VALUES (?, ?, ?)
        ");

        $stmtHeader->bind_param("sss", $no_nota, $tanggal, $kodegd);
        $stmtHeader->execute();
        $stmtHeader->close();

        $stmt = $conn->prepare("
            INSERT INTO zpenyesuaianm 
            (nonota, kodebrg, kodegd, jlh1, jlh2, jlh3, qty, harga)
            VALUES (?, ?, ?, ?, ?, ?, ?, ?)
        ");

        foreach ($detail as $item) {
            $kodebrg = strtoupper($item['kodebrg'] ?? '-');
            $jlh1    = intval($item['jlh1'] ?? 0);
            $jlh2    = intval($item['jlh2'] ?? 0);
            $jlh3    = intval($item['jlh3'] ?? 0);
            $qty    = intval($item['qty'] ?? 0);
            $harga    = intval($item['harga'] ?? 0);

            $stmt->bind_param(
                "sssiiiii",
                $no_nota, // perbaikan: gunakan nonota baru
                $kodebrg,
                $kodegd,
                $jlh1,
                $jlh2,
                $jlh3,
                $qty,
                $harga
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
