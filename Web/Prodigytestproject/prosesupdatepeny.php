    <?php
    include 'koneksi.php';

    header('Content-Type: application/json');
    $data = json_decode(file_get_contents("php://input"), true);

    $detail = $data['detail'] ?? [];

    $no_nota = strtoupper($data['no_nota'] ?? '');
    $no_nota_lama = strtoupper($data['no_nota_lama'] ?? $no_nota);
    $tanggal = $data['tanggal'] ?? '';
    $kodegd = strtoupper($data['kodegd'] ?? '');
    $kodebrg = isset($detail[0]['kodebrg']) ? strtoupper($detail[0]['kodebrg']) : '';
    $operator = $data['operator'] ?? '';
    

    if (!$no_nota || !$tanggal || !$kodegd || empty($detail)) {
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
        $result = $conn->prepare("SELECT kodebrg, kodegd, jlh1, jlh2, jlh3, qty, harga, logtime, operator 
                                FROM zpenyesuaianm WHERE nonota = ?");
        $result->bind_param("s", $no_nota_lama);
        $result->execute();
        $res = $result->get_result();
        while ($row = $res->fetch_assoc()) {
            $key = $row['kodebrg'].'|'.$row['kodegd'];
            $oldData[$key] = $row;
        }
        $result->close();

        $oldHeader = [];
        $stmtOldHeader = $conn->prepare("SELECT operator, logtime FROM zpenyesuaian WHERE nonota = ?");
        $stmtOldHeader->bind_param("s", $no_nota_lama);
        $stmtOldHeader->execute();
        $resHeader = $stmtOldHeader->get_result();
        if ($row = $resHeader->fetch_assoc()) {
            $oldHeader = $row;
        }
        $stmtOldHeader->close();

        $hapusDetail = $conn->prepare("DELETE FROM zpenyesuaianm WHERE nonota = ?");
        $hapusDetail->bind_param("s", $no_nota_lama);
        $hapusDetail->execute();
        $hapusDetail->close();
        // Hapus detail lama
        $hapusHeader = $conn->prepare("DELETE FROM zpenyesuaian WHERE nonota = ?");
        $hapusHeader->bind_param("s", $no_nota_lama);
        $hapusHeader->execute();
        $hapusHeader->close();

        $operatorToSave = $oldHeader['operator'] ?? $operator;
        $logtimeToSave  = $oldHeader['logtime'] ?? date('Y-m-d H:i:s');
        
        // Insert detail baru
        $stmtHeader = $conn->prepare("
            INSERT INTO zpenyesuaian 
            (nonota, tgl, kodegd, operator, logtime)
            VALUES (?, ?, ?, ?, ?)
        ");

        $stmtHeader->bind_param("sssss", $no_nota, $tanggal, $kodegd, $operatorToSave, $logtimeToSave);
        $stmtHeader->execute();
        $stmtHeader->close();

        if ($ada == 0) {
            $stmt = $conn->prepare("INSERT INTO zsaldo (kodebrg, kodegd) VALUES (?, ?)");
            $stmt->bind_param("ss", $kodebrg, $kodegd);
            $stmt->execute();
            $stmt->close();
        }

        $stmt = $conn->prepare("
            INSERT INTO zpenyesuaianm 
            (nonota, kodebrg, kodegd, jlh1, jlh2, jlh3, qty, harga, operator, logtime)
            VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?)
        ");

        foreach ($detail as $item) {
            $kodebrg = strtoupper($item['kodebrg'] ?? '-');
            $jlh1    = intval($item['jlh1'] ?? 0);
            $jlh2    = intval($item['jlh2'] ?? 0);
            $jlh3    = intval($item['jlh3'] ?? 0);
            $qty    = intval($item['qty'] ?? 0);
            $harga    = intval($item['harga'] ?? 0);

            $key = $kodebrg.'|'.$kodegd;
            $logtimeItem = date('Y-m-d H:i:s');
            $operatorItem = $operator;

            if (isset($oldData[$key])) {
            $old = $oldData[$key];
            if (
                    $old['jlh1'] == $jlh1 && $old['jlh2'] == $jlh2 && $old['jlh3'] == $jlh3 &&
                    $old['qty'] == $qty && $old['harga'] == $harga
                ) {
                    $logtimeItem = $old['logtime'];
                    $operatorItem = $old['operator'];
                }
            }

            $stmt->bind_param(
                "sssiiiiiss",
                $no_nota, // perbaikan: gunakan nonota baru
                $kodebrg,
                $kodegd,
                $jlh1,
                $jlh2,
                $jlh3,
                $qty,
                $harga,
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
