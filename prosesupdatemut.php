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
    $kodebrg = isset($detail[0]['kodebrg']) ? strtoupper($detail[0]['kodebrg']) : '';
    $operator = $data['operator'] ?? '';
    

    if (!$no_nota || !$tanggal || !$kodegd1 || !$kodegd2 || empty($detail)) {
        echo json_encode(['success' => false, 'message' => 'Data tidak lengkap']);
        exit;
    }

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

    try {
        $conn->begin_transaction();

        $oldData = [];
        $result = $conn->prepare("SELECT kodebrg, kodegd1, kodegd2, jlh1, jlh2, jlh3, logtime, operator 
                                FROM zmutasim WHERE nonota = ?");
        $result->bind_param("s", $no_nota_lama);
        $result->execute();
        $res = $result->get_result();
        while ($row = $res->fetch_assoc()) {
            $key = $row['kodebrg'].'|'.$row['kodegd1'].'|'.$row['kodegd2'];
            $oldData[$key] = $row;
        }
        $result->close();

        $oldHeader = [];
        $stmtOldHeader = $conn->prepare("SELECT operator, logtime FROM zmutasi WHERE nonota = ?");
        $stmtOldHeader->bind_param("s", $no_nota_lama);
        $stmtOldHeader->execute();
        $resHeader = $stmtOldHeader->get_result();
        if ($row = $resHeader->fetch_assoc()) {
            $oldHeader = $row;
        }
        $stmtOldHeader->close();

        $hapusDetail = $conn->prepare("DELETE FROM zmutasim WHERE nonota = ?");
        $hapusDetail->bind_param("s", $no_nota_lama);
        $hapusDetail->execute();
        $hapusDetail->close();
        // Hapus detail lama
        $hapusHeader = $conn->prepare("DELETE FROM zmutasi WHERE nonota = ?");
        $hapusHeader->bind_param("s", $no_nota_lama);
        $hapusHeader->execute();
        $hapusHeader->close();

        $operatorToSave = $oldHeader['operator'] ?? $operator;
        $logtimeToSave  = $oldHeader['logtime'] ?? date('Y-m-d H:i:s');
        
        // Insert detail baru
        $stmtHeader = $conn->prepare("
            INSERT INTO zmutasi 
            (nonota, tgl, kodegd1, kodegd2, operator, logtime)
            VALUES (?, ?, ?, ?, ?, ?)
        ");

        $stmtHeader->bind_param("ssssss", $no_nota, $tanggal, $kodegd1, $kodegd2, $operatorToSave, $logtimeToSave);
        $stmtHeader->execute();
        $stmtHeader->close();

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

        $stmt = $conn->prepare("
            INSERT INTO zmutasim 
            (nonota, kodebrg, kodegd1, kodegd2, jlh1, jlh2, jlh3, operator, logtime)
            VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?)
        ");

        foreach ($detail as $item) {
            $kodebrg = strtoupper($item['kodebrg'] ?? '-');
            $jlh1    = intval($item['jlh1'] ?? 0);
            $jlh2    = intval($item['jlh2'] ?? 0);
            $jlh3    = intval($item['jlh3'] ?? 0);

            $key = $kodebrg.'|'.$kodegd1.'|'.$kodegd2;
            $logtimeItem = date('Y-m-d H:i:s');
            $operatorItem = $operator;

            if (isset($oldData[$key])) {
            $old = $oldData[$key];
            if (
                    $old['jlh1'] == $jlh1 && $old['jlh2'] == $jlh2 && $old['jlh3'] == $jlh3
                ) {
                    $logtimeItem = $old['logtime'];
                    $operatorItem = $old['operator'];
                }
            }

            $stmt->bind_param(
                "ssssiiiss",
                $no_nota, // perbaikan: gunakan nonota baru
                $kodebrg,
                $kodegd1,
                $kodegd2,
                $jlh1,
                $jlh2,
                $jlh3,
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
