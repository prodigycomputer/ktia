<?php
session_start();
include 'koneksi.php';

$current = basename($_SERVER['PHP_SELF']);
$kodeuser = $_SESSION['kodeuser'] ?? '';

// Ambil menu berdasarkan hak akses user
$sql = "SELECT m.idmenu, m.mainmenu, m.submenu, m.urutan 
        FROM zmenu m
        JOIN zakses a ON m.idmenu = a.idmenu
        WHERE a.kodeuser = '$kodeuser' AND a.tambah = 1
        ORDER BY m.urutan";
$result = mysqli_query($conn, $sql);

$menus = [];
while ($row = mysqli_fetch_assoc($result)) {
    $main = $row['mainmenu'] ?: 'DASHBOARD';
    $menus[$main][] = $row['submenu'];
}

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
                        // Grup otomatis berdasarkan nama file sebelum "input" / "edit"
                        $groups = [];
                        foreach ($submenus as $submenu) {
                            $base = preg_replace('/^(input|edit)/', '', $submenu); 
                            $groups[$base][] = $submenu;
                        }

                        foreach ($groups as $base => $pages) {
                            $utama = $base; // file utama tanpa prefix
                            $active = in_array($current, $pages) ? 'active-link' : '';
                            $label = ucfirst(str_replace('.php','',$utama));
                            echo "<a href='$utama' class='$active'>$label</a>";
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

    // Cari semua link transaksi dari menu
    const transaksiDropdown = document.querySelector('#TRANSAKSI_Dropdown');
    if (transaksiDropdown) {
        transaksiDropdown.querySelectorAll('a').forEach(link => {
            const main = link.getAttribute('href');
            const prefix = main.replace('.php','');
            const related = Array.from(transaksiDropdown.querySelectorAll('a'))
                .map(a => a.getAttribute('href'))
                .filter(a => a.includes(prefix));

            if (related.includes(current)) localStorage.setItem('last_'+prefix, current);
            if (current === main) localStorage.removeItem('last_'+prefix);

            const lastPage = localStorage.getItem('last_'+prefix);
            if (lastPage && lastPage !== main) link.setAttribute('href', lastPage);
        });
    }
});
</script>
