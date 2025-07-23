<?php
session_start();
include 'koneksi.php';

function get_count($conn, $table) {
    $result = @mysqli_query($conn, "SELECT COUNT(*) FROM $table");
    if ($result) {
        $row = mysqli_fetch_row($result);
        return $row[0];
    } else {
        return 0; // Jika tabel tidak ditemukan, anggap jumlahnya 0
    }
}

$jumlah_barang     = get_count($conn, 'zbarang');
$jumlah_pelanggan  = get_count($conn, 'zpelanggan');
$jumlah_supplier   = get_count($conn, 'zsupplier');
$jumlah_penjualan  = get_count($conn, 'zpenjualan');
$jumlah_pembelian  = get_count($conn, 'zpembelian');

?>



<!DOCTYPE html>
<html lang="id">
<head>
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>Dashboard</title>
    <style>
        /* Reset & dasar */
        * {
            box-sizing: border-box;
            margin: 0; padding: 0;
        }
        body {
            font-family: "Segoe UI", Tahoma, Geneva, Verdana, sans-serif;
            display: flex;
            height: 100vh;
            background: #f6f8fb;
            color: #333;
        }
        a {
            text-decoration: none;
            color: inherit;
        }

        /* Main content */
        main {
            flex: 1;
            padding: 30px;
            overflow-y: auto;
        }
        main h2 {
            margin-bottom: 15px;
            font-weight: 700;
        }
        .breadcrumb {
            font-size: 14px;
            color: #666;
            margin-bottom: 20px;
        }
        .breadcrumb a {
            color: #27ae60;
        }
        /* Grid container */
        .grid-container {
            display: grid;
            grid-template-columns: repeat(auto-fit, minmax(300px, 1fr));
            gap: 20px;
        }

        /* Cards */
        .card {
            background: white;
            border-radius: 6px;
            box-shadow: 0 2px 6px rgb(0 0 0 / 0.1);
            padding: 20px;
        }
        .card-header {
            background: #13294b;
            color: white;
            font-weight: 700;
            padding: 10px 15px;
            border-radius: 4px 4px 0 0;
            margin: -20px -20px 20px;
            font-size: 14px;
            text-transform: uppercase;
        }
        .btn {
            background: #2980b9;
            color: white;
            padding: 8px 14px;
            border: none;
            border-radius: 3px;
            cursor: pointer;
            font-size: 13px;
            margin-top: 10px;
            display: inline-block;
            transition: background 0.3s;
        }
        .btn:hover {
            background: #1f6391;
        }
        .btn-primary {
            background: #2980b9;
        }
        .btn-secondary {
            background: #27ae60;
        }
        /* Info colored boxes */
        .info-yellow {
            background: #f1c40f;
            color: #222;
            padding: 15px 20px;
            border-radius: 4px;
            font-weight: 600;
            display: flex;
            align-items: center;
            gap: 10px;
            margin-bottom: 12px;
        }
        .info-green {
            background: #10dd65ff;
            color: white;
            padding: 15px 20px;
            border-radius: 4px;
            font-weight: 600;
            margin-bottom: 12px;
        }
        /* Table */
        table {
            width: 100%;
            border-collapse: collapse;
            margin-top: 10px;
        }
        th, td {
            border: 1px solid #ddd;
            padding: 8px 10px;
            text-align: center;
            font-size: 13px;
        }
        th {
            background: #777;
            color: white;
        }

    </style>
    <link rel="stylesheet" href="navbar.css">
    <script src="https://cdn.jsdelivr.net/npm/chart.js"></script>
</head>
<body>

<?php include 'navbar.php'; ?>

<main>
    <h2>Dashboard</h2>
    <div class="breadcrumb"><a href="#">&#8962;</a> / Dashboard</div>

    <div class="grid-container">
        <!-- Baris 1: Grafik Penjualan dan Pembelian -->
        <div class="card">
            <div class="card-header">Grafik Penjualan</div>
            <canvas id="chartPenjualan" height="220"></canvas>
        </div>

        <div class="card">
            <div class="card-header">Grafik Pembelian</div>
            <canvas id="chartPembelian" height="220"></canvas>
        </div>

        <!-- Baris 2: Info Barang, Pelanggan, Supplier -->
        <div class="card">
            <div class="card-header">Barang</div>
            <div class="info-green">Jumlah Barang: <?= $jumlah_barang ?></div>
            <a href="barang.php" class="btn">Lihat Barang</a>
        </div>

        <div class="card">
            <div class="card-header">Pelanggan</div>
            <div class="info-green">Jumlah Pelanggan: <?= $jumlah_pelanggan ?></div>
            <a href="pelanggan.php" class="btn">Lihat Pelanggan</a>
        </div>

        <div class="card">
            <div class="card-header">Supplier</div>
            <div class="info-green">Jumlah Supplier: <?= $jumlah_supplier ?></div>
            <a href="supplier.php" class="btn">Lihat Supplier</a>
        </div>
    </div>


</main>


</body>
</html>
