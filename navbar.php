<!-- navbar.php -->
<?php
$current = basename($_SERVER['PHP_SELF']);
$isFile = in_array($current, ['barang.php', 'supplier.php', 'kustomer.php', 'sales.php', 'group.php']);
$isTransaksi = in_array($current, ['pembelian.php', 'penjualan.php', 'inputpembelian.php', 'editpembelian.php']);
$isLaporan = in_array($current, [
  'LaporanStok.php', 'LaporanPenjualan.php', 'LaporanSupplier.php',
  'LaporanPembelian.php', 'LaporanKustomer.php'
]);
?>


<button class="hamburger" onclick="toggleSidebar()">☰</button>

<div class="overlay" onclick="toggleSidebar()" id="sidebarOverlay"></div>

<div class="sidebar" id="sidebarMenu">
    <h1>Prodigy Computer</h1>

    <input type="search" placeholder="Search...">

    <div class="nav-section">
        <a href="dashboard.php" class="nav-link-dasboard">Dashboard</a>
    </div>

    <!-- File Section -->
    <div class="nav-section">
    <button class="dropdown-toggle <?= $isFile ? 'active-dropdown' : '' ?>" onclick="toggleDropdown('fileDropdown', this)">File ▾</button>
    <div class="dropdown-content <?= $isFile ? 'show' : '' ?>" id="fileDropdown">
        <a href="barang.php" class="<?= $current == 'barang.php' ? 'active-link' : '' ?>">Stok</a>
        <a href="kustomer.php" class="<?= $current == 'kustomer.php' ? 'active-link' : '' ?>">Kustomer</a>
        <a href="supplier.php" class="<?= $current == 'supplier.php' ? 'active-link' : '' ?>">Supplier</a>
        <a href="sales.php" class="<?= $current == 'sales.php' ? 'active-link' : '' ?>">Sales</a>
        <a href="group.php" class="<?= $current == 'group.php' ? 'active-link' : '' ?>">Grup</a>
    </div>
    </div>

    <div class="nav-section">
    <button class="dropdown-toggle <?= $isTransaksi ? 'active-dropdown' : '' ?>" onclick="toggleDropdown('transaksiDropdown', this)">Transaksi ▾</button>
    <div class="dropdown-content <?= $isTransaksi ? 'show' : '' ?>" id="transaksiDropdown">
        <a href="pembelian.php" class="<?= in_array($current, ['pembelian.php', 'inputpembelian.php', 'editpembelian.php']) ? 'active-link' : '' ?>">Pembelian</a>
        <a href="penjualan.php" class="<?= $current == 'penjualan.php' ? 'active-link' : '' ?>">Penjualan</a>
    </div>
    </div>
    
    <div class="nav-section">
    <button class="dropdown-toggle <?= $isLaporan ? 'active-dropdown' : '' ?>" onclick="toggleDropdown('laporanDropdown', this)">Laporan ▾</button>
    <div class="dropdown-content <?= $isLaporan ? 'show' : '' ?>" id="laporanDropdown">
        <a href="LaporanStok.php" class="<?= $current == 'LaporanStok.php' ? 'active-link' : '' ?>">Laporan Stok</a>
        <a href="LaporanKustomer.php" class="<?= $current == 'LaporanKustomer.php' ? 'active-link' : '' ?>">Laporan Kustomer</a>
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

document.addEventListener('DOMContentLoaded', () => {
    const current = window.location.pathname.split("/").pop();

    // Grup pembelian saja (bukan seluruh transaksi)
    const pembelianPages = ['pembelian.php', 'inputpembelian.php', 'editpembelian.php'];

    // Simpan halaman terakhir dari grup pembelian
    if (pembelianPages.includes(current)) {
        localStorage.setItem('lastPembelianPage', current);
    }

    // Reset jika user kembali ke pembelian.php
    if (current === 'pembelian.php') {
        localStorage.removeItem('lastPembelianPage');
    }

    // Ganti href dari link Pembelian
    const pembelianLink = document.querySelector('a[href="pembelian.php"]');
    const lastPage = localStorage.getItem('lastPembelianPage');

    if (pembelianLink && lastPage && lastPage !== 'pembelian.php') {
        pembelianLink.setAttribute('href', lastPage);
    }
}); 

</script>
