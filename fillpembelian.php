<?php
include 'koneksi.php';

$nonota = $_GET['nonota'] ?? '';
$tgl1 = $_GET['tgl1'] ?? '';
$tgl2 = $_GET['tgl2'] ?? '';
$namasup = $_GET['namasup'] ?? '';
$kodesup = '';
$status = $_GET['status'] ?? 'all';
$limit = intval($_GET['limit'] ?? 10); 

if (!empty($namasup)) {
    $stmt = $conn->prepare("SELECT kodesup FROM zsupplier WHERE namasup LIKE ?");
    $likeNama = '%' . $namasup . '%';
    $stmt->bind_param("s", $likeNama);
    $stmt->execute();
    $stmt->bind_result($resultKode);
    if ($stmt->fetch()) {
        $kodesup = $resultKode;
    }
    $stmt->close();
}

$sql = "SELECT zbeli.tgl, zbeli.nonota, zsupplier.namasup, zbeli.nilai, zbeli.lunas
        FROM zbeli 
        JOIN zsupplier ON zbeli.kodesup = zsupplier.kodesup";

$where = [];

if (empty($nonota)) {
    if (!empty($tgl1) && !empty($tgl2)) {
        $where[] = "zbeli.tgl BETWEEN '" . $conn->real_escape_string($tgl1) . "' AND '" . $conn->real_escape_string($tgl2) . "'";
    }

    if (!empty($kodesup)) {
        $where[] = "zbeli.kodesup = '" . $conn->real_escape_string($kodesup) . "'";
    }

    if ($status == 'lunas') {
        $where[] = "zbeli.lunas = 1";
    } elseif ($status == 'belum') {
        $where[] = "zbeli.lunas = 0";
    }

} else {
        $where[] = "zbeli.nonota LIKE '%" . $conn->real_escape_string($nonota) . "%'";
    }

    if (count($where) > 0) {
        $sql .= " WHERE " . implode(" AND ", $where);
    }

    $sql .= " ORDER BY zbeli.tgl DESC, zbeli.nonota DESC";
    $result = mysqli_query($conn, $sql);

    $data = [];
    while ($row = mysqli_fetch_assoc($result)) {
        $data[] = $row;
    }

    header('Content-Type: application/json');
    echo json_encode($data);
?>