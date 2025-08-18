<?php
include 'koneksi.php';

header('Content-Type: application/json');

$nonota = $_GET['nonota'] ?? '';

if (!$nonota) {
    echo json_encode(['status' => 'error', 'message' => 'Nomor nota tidak diberikan']);
    exit;
}

// Ambil data header dari zjual dan supplier
$queryHeader = mysqli_query($conn, "
    SELECT j.nonota, j.tgl, j.kodekust, j.kodesls, j.kodegd, j.ket, j.ppn, j.hppn, j.disc1, j.hdisc1, j.disc2, j.hdisc2, j.disc3, j.hdisc3, j.nilai, j.tgltempo, k.namakust, k.kodehrg, s.namasls, k.alamat
    FROM zjual j
    JOIN zkustomer k ON j.kodekust = k.kodekust
    JOIN zsales s ON j.kodesls = s.kodesls
    WHERE j.nonota = '$nonota'
");

$dataHeader = mysqli_fetch_assoc($queryHeader);

// Ambil detail pemjualan dari zjualm, dan join ke zstok untuk nama & satuan
$queryDetail = mysqli_query($conn, "
    SELECT i.kodebrg, i.kodegd, z.namabrg, i.jlh1, i.jlh2, i.jlh3,
           i.harga, i.disca, i.discb, i.discc, i.hdisca, i.hdiscb, i.hdiscc, i.discrp, i.jumlah,
           z.isi1, z.isi2, z.satuan1, z.satuan2, z.satuan3
    FROM zjualm i
    JOIN zstok z ON i.kodebrg = z.kodebrg
    WHERE i.nonota = '$nonota'
");

$dataPenjualan = [];
while ($row = mysqli_fetch_assoc($queryDetail)) {
    $dataPenjualan[] = $row;
}

echo json_encode([
    'status' => 'success',
    'header' => [
        'no_nota' => $dataHeader['nonota'],
        'tanggal' => $dataHeader['tgl'],
        'kode_kust' => $dataHeader['kodekust'],
        'nama_kust' => $dataHeader['namakust'],
        'kode_sls' => $dataHeader['kodesls'],
        'nama_sls' => $dataHeader['namasls'],
        'alamat' => $dataHeader['alamat'],
        'kodehrg' => $dataHeader['kodehrg'],
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
    'detail' => $dataPenjualan
]);
?>
