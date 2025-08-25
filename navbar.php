<!-- navbar.php -->
<?php
$current = basename($_SERVER['PHP_SELF']);
$isFile = in_array($current, ['barang.php', 'supplier.php', 'area.php', 'tipe.php', 'kustomer.php', 'sales.php','gudang.php', 'group.php', 'merek.php', 'golongan.php', 'regisakun.php', 'pengaturanakun.php']);
$isTransaksi = in_array($current, ['pembelian.php', 'penjualan.php', 'mutasi.php', 'penyesuaian.php', 'inputpembelian.php', 'editpembelian.php', 'inputpenjualan.php', 'editpenjualan.php', 'inputmutasi.php', 'editmutasi.php', 'inputpenyesuaian.php', 'editpenyesuaian.php']);
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
        <a href="area.php" class="<?= $current == 'area.php' ? 'active-link' : '' ?>">Area</a>
        <a href="tipe.php" class="<?= $current == 'tipe.php' ? 'active-link' : '' ?>">Tipe</a>
        <a href="kustomer.php" class="<?= $current == 'kustomer.php' ? 'active-link' : '' ?>">Kustomer</a>
        <a href="supplier.php" class="<?= $current == 'supplier.php' ? 'active-link' : '' ?>">Supplier</a>
        <a href="sales.php" class="<?= $current == 'sales.php' ? 'active-link' : '' ?>">Sales</a>
        <a href="gudang.php" class="<?= $current == 'gudang.php' ? 'active-link' : '' ?>">Gudang</a>
        <a href="merek.php" class="<?= $current == 'merek.php' ? 'active-link' : '' ?>">Merek</a>
        <a href="golongan.php" class="<?= $current == 'golongan.php' ? 'active-link' : '' ?>">Golongan</a>
        <a href="group.php" class="<?= $current == 'group.php' ? 'active-link' : '' ?>">Grup</a>
        <a href="regisakun.php" class="<?= $current == 'regisakun.php' ? 'active-link' : '' ?>">Regis Akun</a>
        <a href="pengaturanakun.php" class="<?= $current == 'pengaturanakun.php' ? 'active-link' : '' ?>">Pengaturan Akun</a>
    </div>
    </div>

    <div class="nav-section">
    <button class="dropdown-toggle <?= $isTransaksi ? 'active-dropdown' : '' ?>" onclick="toggleDropdown('transaksiDropdown', this)">Transaksi ▾</button>
    <div class="dropdown-content <?= $isTransaksi ? 'show' : '' ?>" id="transaksiDropdown">
        <a href="pembelian.php" class="<?= in_array($current, ['pembelian.php', 'inputpembelian.php', 'editpembelian.php']) ? 'active-link' : '' ?>">Pembelian</a>
        <a href="penjualan.php" class="<?= in_array($current, ['penjualan.php', 'inputpenjualan.php', 'editpenjualan.php']) ? 'active-link' : '' ?>">Penjualan</a>
        <a href="mutasi.php" class="<?= in_array($current, ['mutasi.php', 'inputmutasi.php', 'editmutasi.php']) ? 'active-link' : '' ?>">Mutasi Barang</a>
        <a href="penyesuaian.php" class="<?= in_array($current, ['penyesuaian.php', 'inputpenyesuaian.php', 'editpenyesuaian.php']) ? 'active-link' : '' ?>">Penyesuaian</a>
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
    const penjualanPages = ['penjualan.php', 'inputpenjualan.php', 'editpenjualan.php'];
    const mutasiPages = ['mutasi.php', 'inputmutasi.php', 'editmutasi.php'];
    const penyesuaianPages = ['penyesuaian.php', 'inputpenyesuaian.php', 'editpenyesuaian.php'];


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

    // Simpan halaman terakhir dari grup pembelian
    if (penjualanPages.includes(current)) {
        localStorage.setItem('lastPenjualanPage', current);
    }

    // Reset jika user kembali ke pembelian.php
    if (current === 'penjualan.php') {
        localStorage.removeItem('lastPenjualanPage');
    }

    // Ganti href dari link Pembelian
    const penjualanLink = document.querySelector('a[href="penjualan.php"]');
    const last2Page = localStorage.getItem('lastPenjualanPage');

    if (penjualanLink && last2Page && last2Page !== 'penjualan.php') {
        penjualanLink.setAttribute('href', last2Page);
    }

    if (mutasiPages.includes(current)) {
        localStorage.setItem('lastMutasiPage', current);
    }

    // Reset jika user kembali ke pembelian.php
    if (current === 'mutasi.php') {
        localStorage.removeItem('lastMutasiPage');
    }

    // Ganti href dari link Pembelian
    const mutasiLink = document.querySelector('a[href="mutasi.php"]');
    const last3Page = localStorage.getItem('lastMutasiPage');

    if (mutasiLink && last3Page && last3Page !== 'mutasi.php') {
        mutasiLink.setAttribute('href', last3Page);
    }

    if (penyesuaianPages.includes(current)) {
        localStorage.setItem('lastPenyesuaianPage', current);
    }

    // Reset jika user kembali ke pembelian.php
    if (current === 'penyesuaian.php') {
        localStorage.removeItem('lastPenyesuaianPage');
    }

    // Ganti href dari link Pembelian
    const penyesuaianLink = document.querySelector('a[href="penyesuaian.php"]');
    const last4Page = localStorage.getItem('lastPenyesuaianPage');

    if (penyesuaianLink && last4Page && last4Page !== 'penyesuaian.php') {
        penyesuaianLink.setAttribute('href', last4Page);
    }
}); 

</script>
