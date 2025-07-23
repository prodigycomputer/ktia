<!-- navbar.php -->
<?php
$current = basename($_SERVER['PHP_SELF']);
$isFile = in_array($current, ['barang.php', 'supplier.php', 'pelanggan.php', 'group.php']);
$isTransaksi = in_array($current, ['pembelian.php', 'penjualan.php']);
$isLaporan = in_array($current, [
  'LaporanStok.php', 'LaporanPelanggan.php', 'LaporanSupplier.php',
  'LaporanPembelian.php', 'LaporanPenjualan.php'
]);
?>


<button class="hamburger" onclick="toggleSidebar()">☰</button>

<div class="overlay" onclick="toggleSidebar()" id="sidebarOverlay"></div>

<div class="sidebar" id="sidebarMenu">
    <h1>Prodigy Computer</h1>

    <input type="search" placeholder="Search...">

    <div class="nav-section">
        <a href="dashboard.php" class="nav-link active">Dashboard</a>
    </div>

    <!-- File Section -->
    <div class="nav-section">
    <button class="dropdown-toggle <?= $isFile ? 'active-dropdown' : '' ?>" onclick="toggleDropdown('fileDropdown', this)">File ▾</button>
    <div class="dropdown-content <?= $isFile ? 'show' : '' ?>" id="fileDropdown">
        <a href="barang.php" class="<?= $current == 'barang.php' ? 'active-link' : '' ?>">Stok</a>
        <a href="supplier.php" class="<?= $current == 'supplier.php' ? 'active-link' : '' ?>">Supplier</a>
        <a href="pelanggan.php" class="<?= $current == 'pelanggan.php' ? 'active-link' : '' ?>">Pelanggan</a>
        <a href="group.php" class="<?= $current == 'group.php' ? 'active-link' : '' ?>">Grup</a>
    </div>
    </div>

    <div class="nav-section">
    <button class="dropdown-toggle <?= $isTransaksi ? 'active-dropdown' : '' ?>" onclick="toggleDropdown('transaksiDropdown', this)">Transaksi ▾</button>
    <div class="dropdown-content <?= $isTransaksi ? 'show' : '' ?>" id="transaksiDropdown">
        <a href="pembelian.php" class="<?= $current == 'pembelian.php' ? 'active-link' : '' ?>">Pembelian</a>
        <a href="penjualan.php" class="<?= $current == 'penjualan.php' ? 'active-link' : '' ?>">Penjualan</a>
    </div>
    </div>
    
    <div class="nav-section">
    <button class="dropdown-toggle <?= $isLaporan ? 'active-dropdown' : '' ?>" onclick="toggleDropdown('laporanDropdown', this)">Laporan ▾</button>
    <div class="dropdown-content <?= $isLaporan ? 'show' : '' ?>" id="laporanDropdown">
        <a href="LaporanStok.php" class="<?= $current == 'LaporanStok.php' ? 'active-link' : '' ?>">Laporan Stok</a>
        <a href="LaporanPelanggan.php" class="<?= $current == 'LaporanPelanggan.php' ? 'active-link' : '' ?>">Laporan Pelanggan</a>
        <a href="LaporanSupplier.php" class="<?= $current == 'LaporanSupplier.php' ? 'active-link' : '' ?>">Laporan Supplier</a>
        <a href="LaporanPembelian.php" class="<?= $current == 'LaporanPembelian.php' ? 'active-link' : '' ?>">Laporan Pembelian</a>
        <a href="LaporanPenjualan.php" class="<?= $current == 'LaporanPenjualan.php' ? 'active-link' : '' ?>">Laporan Penjualan</a>
    </div>
    </div>

</div>

<script>
function toggleSidebar() {
    const sidebar = document.getElementById('sidebarMenu');
    const overlay = document.getElementById('sidebarOverlay');
    sidebar.classList.toggle('active');
    overlay.classList.toggle('active');
}

function toggleDropdown(id) {
    const content = document.getElementById(id);
    content.classList.toggle('show');
}
</script>
