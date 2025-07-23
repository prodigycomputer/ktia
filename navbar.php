<!-- navbar.php -->
<button class="hamburger" onclick="toggleSidebar()">☰</button>

<div class="overlay" onclick="toggleSidebar()" id="sidebarOverlay"></div>

<div class="sidebar" id="sidebarMenu">
    <h1>Prodigy Computer</h1>

    <input type="search" placeholder="Search...">

    <div class="nav-section">
        <a href="dashboard.php" class="nav-link active">Dashboard</a>
    </div>
    <div class="nav-section">
        <h3>File</h3>
        <a href="barang.php">Stok</a>
        <a href="supplier.php">Supplier</a>
        <a href="pelanggan.php">Pelanggan</a>
        <a href="group.php">Grup</a>
    </div>
    <div class="nav-section">
        <h3>Transaksi</h3>
        <a href="pembelian.php">Pembelian</a>
        <a href="penjualan.php">Penjualan</a>
    </div>
    <div class="nav-section">
        <h3>Laporan</h3>
        <a href="LaporanStok.php">Laporan Stok</a>
        <a href="LaporanPelanggan.php">Laporan Pelanggan</a>
        <a href="LaporanSupplier.php">Laporan Supplier</a>
        <a href="LaporanPembelian.php">Laporan Pembelian</a>
        <a href="LaporanPenjualan.php">Laporan Penjualan</a>
    </div>
</div>

<script>
function toggleSidebar() {
    const sidebar = document.getElementById('sidebarMenu');
    const overlay = document.getElementById('sidebarOverlay');
    sidebar.classList.toggle('active');
    overlay.classList.toggle('active');
}
</script>
