<?php
include 'koneksi.php';

$nonota = $_GET['nonota'] ?? '';
$tgl1 = $_GET['tgl1'] ?? '';
$tgl2 = $_GET['tgl2'] ?? '';
$namakust = $_GET['namakust'] ?? '';
$kodekust = '';
$namasls = $_GET['namasls'] ?? '';
$kodesls = '';
$status = $_GET['status'] ?? 'all';
$limit = intval($_GET['limit'] ?? 10); 

if (!empty($namakust)) {
    $stmt = $conn->prepare("SELECT kodekust FROM zkustomer WHERE namakust LIKE ?");
    $likeNama = '%' . $namakust . '%';
    $stmt->bind_param("s", $likeNama);
    $stmt->execute();
    $stmt->bind_result($resultKode);
    if ($stmt->fetch()) {
        $kodekust = $resultKode;
    }
    $stmt->close();
}

if (!empty($namasls)) {
    $stmt = $conn->prepare("SELECT kodesls FROM zsales WHERE namasls LIKE ?");
    $likeNama = '%' . $namasls . '%';
    $stmt->bind_param("s", $likeNama);
    $stmt->execute();
    $stmt->bind_result($resultKode);
    if ($stmt->fetch()) {
        $kodesls = $resultKode;
    }
    $stmt->close();
}

$sql = "SELECT zjual.tgl, zjual.nonota, zkustomer.namakust, zsales.namasls, zjual.nilai, zjual.lunas
        FROM zjual 
        JOIN zkustomer ON zjual.kodekust = zkustomer.kodekust
        JOIN zsales ON zjual.kodesls = zsales.kodesls";

$where = [];

if (empty($nonota)) {
    if (!empty($tgl1) && !empty($tgl2)) {
        $where[] = "zjual.tgl BETWEEN '" . $conn->real_escape_string($tgl1) . "' AND '" . $conn->real_escape_string($tgl2) . "'";
    }

    if (!empty($kodekust)) {
        $where[] = "zjual.kodekust = '" . $conn->real_escape_string($kodekust) . "'";
    }

    if (!empty($kodesls)) {
        $where[] = "zjual.kodesls = '" . $conn->real_escape_string($kodesls) . "'";
    }

    if ($status == 'lunas') {
        $where[] = "zjual.lunas = 1";
    } elseif ($status == 'belum') {
        $where[] = "zjual.lunas = 0";
    }

} else {
        $where[] = "zjual.nonota LIKE '%" . $conn->real_escape_string($nonota) . "%'";
    }

    if (count($where) > 0) {
        $sql .= " WHERE " . implode(" AND ", $where);
    }

    $sql .= " ORDER BY zjual.tgl DESC, zjual.nonota DESC";
    $result = mysqli_query($conn, $sql);

    $data = [];
    while ($row = mysqli_fetch_assoc($result)) {
        $data[] = $row;
    }

    header('Content-Type: application/json');
    echo json_encode($data);
?>