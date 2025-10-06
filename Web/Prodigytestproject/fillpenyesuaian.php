<?php
include 'koneksi.php';

$nonota = $_GET['nonota'] ?? '';
$tgl = $_GET['tgl'] ?? '';  // hanya satu tanggal
$namagd = $_GET['namagd'] ?? '';
$kodegd = '';
$qty = $_GET['qty'] ?? '';
$harga = $_GET['harga'] ?? '';


$limit = intval($_GET['limit'] ?? 10); 

if (!empty($namagd)) {
    $stmt = $conn->prepare("SELECT kodegd FROM zgudang WHERE namagd LIKE ?");
    $likeNama = '%' . $namagd . '%';
    $stmt->bind_param("s", $likeNama);
    $stmt->execute();
    $stmt->bind_result($resultKode);
    if ($stmt->fetch()) {
        $kodegd = $resultKode;
    }
    $stmt->close();
}

// JOIN dua kali ke zgudang, alias g1 untuk kodegd1, g2 untuk kodegd2
$sql = "SELECT 
            zpenyesuaian.tgl, zpenyesuaian.nonota, zgudang.namagd, zpenyesuaian.qty, zpenyesuaian.harga 
            FROM zpenyesuaian
            JOIN zgudang ON zpenyesuaian.kodegd = zgudang.kodegd";

$where = [];

if (empty($nonota)) {
    // filter berdasarkan tanggal jika diisi
    if (!empty($tgl)) {
        $where[] = "zpenyesuaian.tgl = '" . $conn->real_escape_string($tgl) . "'";
    }

    // filter kode gudang jika diisi
    if (!empty($namagd)) {
        $where[] = "zpenyesuaian.namagd LIKE '%" . $conn->real_escape_string($namagd) . "%'";
    }

} else {
    $where[] = "zpenyesuaian.nonota LIKE '%" . $conn->real_escape_string($nonota) . "%'";
}

if (count($where) > 0) {
    $sql .= " WHERE " . implode(" AND ", $where);
}

$sql .= " ORDER BY zpenyesuaian.tgl DESC, zpenyesuaian.nonota DESC";
$result = mysqli_query($conn, $sql);

$data = [];
while ($row = mysqli_fetch_assoc($result)) {
    $data[] = $row;
}

header('Content-Type: application/json');
echo json_encode($data);
?>
