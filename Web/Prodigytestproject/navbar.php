<?php
include 'koneksi.php';

$current = basename($_SERVER['PHP_SELF']);
$kodeuser = $_SESSION['kodeuser'] ?? '';

// Ambil menu berdasarkan hak akses user
$sql = "SELECT m.idmenu, m.mainmenu, m.submenu, m.urutan, a.tambah, a.ubah, a.hapus 
        FROM zmenu m
        JOIN zakses a ON m.idmenu = a.idmenu
        WHERE a.kodeuser = '$kodeuser' AND a.tambah = 1
        ORDER BY m.urutan";
$result = mysqli_query($conn, $sql);

$menus = [];
$aksesSemua = [];

while ($row = mysqli_fetch_assoc($result)) {
    $main = $row['mainmenu'] ?: 'DASHBOARD';
    $menus[$main][] = $row['submenu'];

    $aksesSemua[$row['submenu']] = [
        'tambah' => (int)$row['tambah'],
        'ubah'   => (int)$row['ubah'],
        'hapus'  => (int)$row['hapus']
    ];
}

$_SESSION['aksesSemua'] = $aksesSemua;

function isActiveDropdown($submenus, $current) {
    return in_array($current, $submenus) ? 'active-dropdown' : '';
}
function isActiveLink($page, $current) {
    return $page == $current ? 'active-link' : '';
}
?>

<div class="sidebar" id="sidebarMenu">
    <h1>Prodigy Computer</h1>
    <input type="search" placeholder="Search...">

    <div class="nav-section">
        <a href="dashboard.php" class="<?= isActiveLink('dashboard.php', $current) ?>">Dashboard</a>
    </div>

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
                    if ($mainmenu == 'TRANSAKSI') {
                        // Group berdasarkan menu utama (pembelian, penjualan, dll.)
                        $groups = [];
                        foreach ($submenus as $submenu) {
                            $base = preg_replace('/^(input|edit)/', '', str_replace('.php','',$submenu));
                            $groups[$base][] = $submenu;
                        }

                        foreach ($groups as $base => $pages) {
                            $utama = $base . '.php'; // menu utama default
                            $active = in_array($current, $pages) ? 'active-link' : (isActiveLink($utama,$current));
                            $label = ucfirst($base);
                            echo "<a href='$utama' data-pages='".json_encode($pages)."' class='$active transaksi-link'>$label</a>";
                        }
                    } else {
                        foreach ($submenus as $submenu) {
                            $active = isActiveLink($submenu,$current);
                            $label = ucfirst(str_replace('.php','',$submenu));
                            echo "<a href='$submenu' class='$active'>$label</a>";
                        }
                    }
                    ?>
                </div>
            </div>
        <?php endif; ?>
    <?php endforeach; ?>
</div>

<script>
function toggleDropdown(id) {
    document.getElementById(id).classList.toggle('show');
}

document.addEventListener('DOMContentLoaded', () => {
    const current = window.location.pathname.split("/").pop();

    // Cek semua link transaksi
    document.querySelectorAll('.transaksi-link').forEach(link => {
        const main = link.getAttribute('href');      // contoh: pembelian.php
        const pages = JSON.parse(link.dataset.pages); // semua halaman terkait

        // Jika halaman sekarang adalah salah satu dari pages, simpan di localStorage
        if (pages.includes(current)) {
            localStorage.setItem('last_'+main, current);
        }

        // Cek halaman terakhir yang tersimpan
        const lastPage = localStorage.getItem('last_'+main);
        if (lastPage && lastPage !== main) {
            link.setAttribute('href', lastPage); // arahkan ke halaman terakhir
        }
    });
});
</script>
