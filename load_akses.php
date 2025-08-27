<?php
include 'koneksi.php';
$kodeuser = $_GET['kodeuser'] ?? '';

$menus = mysqli_query($conn, "SELECT idmenu, mainmenu, submenu FROM zmenu ORDER BY urutan");
$akses = [];
$res = mysqli_query($conn, "SELECT * FROM zakses WHERE kodeuser='$kodeuser'");
while($r = mysqli_fetch_assoc($res)) $akses[$r['idmenu']] = $r['akses'];

echo "<form id='aksesForm' onsubmit='return false;'>";
echo "<button type='button' class='save-btn' onclick=\"saveAkses('$kodeuser')\">Simpan</button>";
echo "<table class='akses-table'>";
echo "<tr><th>ID</th><th>Main Menu</th><th>Submenu</th><th>Akses</th></tr>";

while($m = mysqli_fetch_assoc($menus)){
    $cek = isset($akses[$m['idmenu']]) ? $akses[$m['idmenu']] : 1;
    $checked = ($cek == 1) ? "checked" : "";
    echo "<tr>
        <td>{$m['idmenu']}</td>
        <td>{$m['mainmenu']}</td>
        <td>{$m['submenu']}</td>
        <td><input type='checkbox' name='akses[{$m['idmenu']}]' value='1' $checked></td>
    </tr>";
}

echo "</table>";
echo "</form>";

?>
