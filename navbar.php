<?php
include 'koneksi.php';
$current = basename($_SERVER['PHP_SELF']);

// Ambil data menu dari database
$sql = "SELECT mainmenu, submenu FROM zmenu ORDER BY urutan";
$result = mysqli_query($conn, $sql);

$menus = [];
while ($row = mysqli_fetch_assoc($result)) {
    $main = $row['mainmenu'] ?: 'DASHBOARD'; // Kosong = dashboard
    $menus[$main][] = $row['submenu'];
}

// Fungsi untuk cek aktif di dropdown
function isActiveDropdown($submenus, $current) {
    return in_array($current, $submenus) ? 'active-dropdown' : '';
}

// Fungsi untuk cek aktif di link
function isActiveLink($page, $current) {
    return $page == $current ? 'active-link' : '';
}
?>

<button class="hamburger" onclick="toggleSidebar()">☰</button>
<div class="overlay" onclick="toggleSidebar()" id="sidebarOverlay"></div>

<div class="sidebar" id="sidebarMenu">
    <h1>Prodigy Computer</h1>
    <input type="search" placeholder="Search...">

    <div class="nav-section">
        <a href="dashboard.php" class="<?= isActiveLink('dashboard.php', $current) ?>">Dashboard</a>
    </div>

    <!-- Loop menu dari DB -->
    <?php foreach ($menus as $mainmenu => $submenus): ?>
        <?php if ($mainmenu != 'DASHBOARD'): ?>
            <div class="nav-section">
                <button class="dropdown-toggle <?= isActiveDropdown($submenus, $current) ?>"
                        onclick="toggleDropdown('<?= $mainmenu ?>Dropdown', this)">
                    <?= ucfirst(strtolower($mainmenu)) ?> ▾
                </button>
                <div class="dropdown-content <?= isActiveDropdown($submenus, $current) ? 'show' : '' ?>"
                     id="<?= $mainmenu ?>Dropdown">
                    <?php 
                    // Khusus Transaksi: hanya tampilkan menu utama, input/edit di-hide
                    if ($mainmenu == 'TRANSAKSI') {
                        $kelompok = [
                            'pembelian.php' => ['pembelian.php','inputpembelian.php','editpembelian.php'],
                            'penjualan.php' => ['penjualan.php','inputpenjualan.php','editpenjualan.php'],
                            'mutasi.php' => ['mutasi.php','inputmutasi.php','editmutasi.php'],
                            'penyesuaian.php' => ['penyesuaian.php','inputpenyesuaian.php','editpenyesuaian.php']
                        ];
                        foreach ($kelompok as $utama => $all) {
                            if (array_intersect($all, $submenus)) {
                                echo '<a href="'.$utama.'" class="'.(in_array($current,$all)?'active-link':'').'">'.ucfirst(str_replace('.php','',$utama)).'</a>';
                            }
                        }
                    } else {
                        foreach ($submenus as $submenu) {
                            echo '<a href="'.$submenu.'" class="'.isActiveLink($submenu,$current).'">'.ucfirst(str_replace('.php','',$submenu)).'</a>';
                        }
                    }
                    ?>
                </div>
            </div>
        <?php endif; ?>
    <?php endforeach; ?>
</div>

<script>
function toggleSidebar() {
    document.getElementById('sidebarMenu').classList.toggle('active');
    document.getElementById('sidebarOverlay').classList.toggle('active');
}
function toggleDropdown(id) {
    document.getElementById(id).classList.toggle('show');
}

document.addEventListener('DOMContentLoaded', () => {
    const current = window.location.pathname.split("/").pop();
    const pageGroups = {
        'pembelian.php': ['pembelian.php','inputpembelian.php','editpembelian.php'],
        'penjualan.php': ['penjualan.php','inputpenjualan.php','editpenjualan.php'],
        'mutasi.php': ['mutasi.php','inputmutasi.php','editmutasi.php'],
        'penyesuaian.php': ['penyesuaian.php','inputpenyesuaian.php','editpenyesuaian.php']
    };

    Object.entries(pageGroups).forEach(([main, pages]) => {
        if (pages.includes(current)) localStorage.setItem('last_'+main, current);
        if (current === main) localStorage.removeItem('last_'+main);
        const link = document.querySelector('a[href="'+main+'"]');
        const lastPage = localStorage.getItem('last_'+main);
        if (link && lastPage && lastPage !== main) link.setAttribute('href', lastPage);
    });
});
</script>
