<?php
include 'koneksi.php';

$nonota = $_GET['nonota'] ?? '';
$tgl = $_GET['tgl'] ?? '';  // hanya satu tanggal
$namagd1 = $_GET['namagd1'] ?? '';
$kodegd1 = '';
$namagd2 = $_GET['namagd2'] ?? '';
$kodegd2 = '';

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
            zmutasi.tgl, 
            zmutasi.nonota, 
            g1.namagd AS namagd1, 
            g2.namagd AS namagd2 
        FROM zmutasi
        JOIN zgudang g1 ON zmutasi.kodegd1 = g1.kodegd
        JOIN zgudang g2 ON zmutasi.kodegd2 = g2.kodegd";

$where = [];

if (empty($nonota)) {
    // filter berdasarkan tanggal jika diisi
    if (!empty($tgl)) {
        $where[] = "zmutasi.tgl = '" . $conn->real_escape_string($tgl) . "'";
    }

    // filter kode gudang jika diisi
    if (!empty($namagd1)) {
        $where[] = "g1.namagd LIKE '%" . $conn->real_escape_string($namagd1) . "%'";
    }

    // Filter gudang tujuan
    if (!empty($namagd2)) {
        $where[] = "g2.namagd LIKE '%" . $conn->real_escape_string($namagd2) . "%'";
    }

} else {
    $where[] = "zmutasi.nonota LIKE '%" . $conn->real_escape_string($nonota) . "%'";
}

if (count($where) > 0) {
    $sql .= " WHERE " . implode(" AND ", $where);
}

$sql .= " ORDER BY zmutasi.tgl DESC, zmutasi.nonota DESC";
$result = mysqli_query($conn, $sql);

$data = [];
while ($row = mysqli_fetch_assoc($result)) {
    $data[] = $row;
}

header('Content-Type: application/json');
echo json_encode($data);
?>
