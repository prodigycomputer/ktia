<?php
include 'koneksi.php';

header('Content-Type: application/json');

$nonota = $_GET['nonota'] ?? '';

if (!$nonota) {
    echo json_encode(['status' => 'error', 'message' => 'Nomor nota tidak diberikan']);
    exit;
}

// Ambil data header dari zbeli dan supplier
$queryHeader = mysqli_query($conn, "
    SELECT b.nonota, b.tgl, b.kodesup, b.kodegd, b.ket, b.ppn, b.hppn, b.disc1, b.hdisc1, b.disc2, b.hdisc2, b.disc3, b.hdisc3, b.nilai, b.tgltempo, s.namasup, s.alamat
    FROM zbeli b
    JOIN zsupplier s ON b.kodesup = s.kodesup
    WHERE b.nonota = '$nonota'
");

$dataHeader = mysqli_fetch_assoc($queryHeader);

// Ambil detail pembelian dari zbelim, dan join ke zstok untuk nama & satuan
$queryDetail = mysqli_query($conn, "
    SELECT d.kodebrg, d.kodegd, z.namabrg, d.jlh1, d.jlh2, d.jlh3,
           d.harga, d.disca, d.discb, d.discc, d.hdisca, d.hdiscb, d.hdiscc, d.discrp, d.jumlah,
           z.isi1, z.isi2, z.satuan1, z.satuan2, z.satuan3, z.hrgbeli
    FROM zbelim d
    JOIN zstok z ON d.kodebrg = z.kodebrg
    WHERE d.nonota = '$nonota'
");

$dataPembelian = [];
while ($row = mysqli_fetch_assoc($queryDetail)) {
    $dataPembelian[] = $row;
}

echo json_encode([
    'status' => 'success',
    'header' => [
        'no_nota' => $dataHeader['nonota'],
        'tanggal' => $dataHeader['tgl'],
        'kode_sup' => $dataHeader['kodesup'],
        'nama_sup' => $dataHeader['namasup'],
        'alamat' => $dataHeader['alamat'],
        'keterangan' => $dataHeader['ket'],
        'jt_tempo' => $dataHeader['tgltempo'],
        'prsnppn' => $dataHeader['ppn'],
        'hrgppn' => $dataHeader['hppn'],
        'disk1' => $dataHeader['disc1'],
        'hdisk1' => $dataHeader['hdisc1'],
        'disk2' => $dataHeader['disc2'],
        'hdisk2' => $dataHeader['hdisc2'],
        'disk3' => $dataHeader['disc3'],
        'hdisk3' => $dataHeader['hdisc3'],
        'totaljmlh' => $dataHeader['nilai']
    ],
    'detail' => $dataPembelian
]);
?>
