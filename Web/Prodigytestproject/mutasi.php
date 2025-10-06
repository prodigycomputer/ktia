<?php
require_once 'init.php';

// Query data mutasi awal
$query = mysqli_query($conn, "
    SELECT 
        zmutasi.tgl, 
        zmutasi.nonota, 
        g1.namagd AS namagd1, 
        g2.namagd AS namagd2
    FROM zmutasi
    LEFT JOIN zgudang g1 ON zmutasi.kodegd1 = g1.kodegd
    LEFT JOIN zgudang g2 ON zmutasi.kodegd2 = g2.kodegd
    ORDER BY zmutasi.tgl DESC, zmutasi.nonota DESC
    LIMIT 10
");

if (!$query) {
    die("Query error: " . mysqli_error($conn));
}
?>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
    <title>Data Mutasi</title>
    <link rel="stylesheet" href="navbar.css" />
    <link rel="stylesheet" href="form.css" />
</head>
<body>
    <button class="hamburger" onclick="toggleSidebar()">☰</button>
    <div class="overlay" id="sidebarOverlay" onclick="toggleSidebar()"></div>
    <?php 
    include 'navbar.php'; 

    $aksesInput = $_SESSION['aksesSemua']['inputmutasi.php'] ?? ['tambah'=>0];
    $aksesEdit  = $_SESSION['aksesSemua']['editmutasi.php'] ?? ['tambah'=>0];

    $hakTambah = $aksesInput['tambah'];
    $hakUbah   = $aksesEdit['tambah'];?>

    <main>
        <input type="hidden" name="kodeuser" value="<?= $kodeuser ?>">
        
        <h2>Data Mutasi</h2>

        <div class="action-bar">
            <button type="button" class="action-btn tambah" onclick="cekAkses()">Tambah</button>

            <input type="text" id="inputNoNota" placeholder="No Nota" style="text-transform: uppercase;">
            <input type="date" id="inputTgl">

            <span> Dari </span>
            <input type="hidden" name="kodegd1" id="inputKodegd1" style="text-transform: uppercase;">

            <input type="text" name="namagd1" id="inputNamagd1" placeholder="Nama gudang 1" class="medium-input" style="text-transform: uppercase;">

            <span> Ke </span>
            <input type="hidden" name="kodegd2" id="inputKodegd2" style="text-transform: uppercase;">

            <input type="text" name="namagd2" id="inputNamagd2" placeholder="Nama gudang 2" class="medium-input" style="text-transform: uppercase;">

            <button type="button" class="action-btn cari" onclick="triggerSearch()">🔍 Cari</button>
        </div>

        <div id="tabelHasil" class="form-grid" style="margin-top: 20px;">
            <table class="tabel-hasil">
                <thead>
                    <tr>
                        <th>Tanggal</th>
                        <th>No. Nota</th>
                        <th>Gudang Awal</th>
                        <th>Gudang Sekarang</th>
                    </tr>
                </thead>
                <tbody>
                    <?php while ($row = mysqli_fetch_assoc($query)): ?>
                        <tr onclick="tampilkanKonfirmasiEdit('<?= $row['nonota'] ?>')">
                            <td><?= $row['tgl'] ?></td>
                            <td><?= $row['nonota'] ?></td>
                            <td><?= $row['namagd1'] ?></td>
                            <td><?= $row['namagd2'] ?></td>
                        </tr>
                    <?php endwhile; ?>
                </tbody>
            </table>
        </div>
    </main>

    <script>
    const hakTambah = <?php echo $hakTambah; ?>;
    const hakUbah   = <?php echo $hakUbah; ?>;

    function cekAkses() {
        if (hakTambah == 1) {
            window.location.href = 'inputmutasi.php';
        } else {
            showToast("Anda tidak memiliki hak akses untuk Tambah!", "#dc3545");
        }
    }

    function triggerSearch() {
        const noNota = document.getElementById('inputNoNota').value.trim();
        const tgl = document.getElementById('inputTgl').value;
        const namagd1 = document.getElementById('inputNamagd1').value.trim();
        const namagd2 = document.getElementById('inputNamagd2').value.trim();

        const params = new URLSearchParams({
            nonota: noNota,
            tgl: tgl,
            namagd1: namagd1,
            namagd2: namagd2
        });

        fetch('fillmutasi.php?' + params.toString())
            .then(res => res.json())
            .then(data => {
                const tbody = document.querySelector(".tabel-hasil tbody");
                tbody.innerHTML = '';

                if (data.length === 0) {
                    showToast("Data tidak ditemukan!", "#dc3545");
                    return;
                }

                data.forEach(row => {
                    const tr = document.createElement("tr");
                    tr.setAttribute("onclick", `tampilkanKonfirmasiEdit('${row.nonota}')`);

                    tr.innerHTML = `
                        <td>${row.tgl}</td>
                        <td>${row.nonota}</td>
                        <td>${row.namagd1}</td>
                        <td>${row.namagd2}</td>
                    `;

                    tbody.appendChild(tr);
                });
            })
            .catch(err => {
                alert("Gagal mengambil data: " + err);
            });
    }

    function tampilkanKonfirmasiEdit(nonota) {
        if (hakUbah == 1) {
            window.location.href = 'editmutasi.php?nonota=' + encodeURIComponent(nonota);
        } else {
            showToast("Anda tidak memiliki hak akses untuk Edit!", "#dc3545");
        }
    }

    function showToast(pesan, warna = '#28a745') {
        const toast = document.createElement('div');
        toast.textContent = pesan;
        toast.style.position = 'fixed';
        toast.style.top = '20px';   
        toast.style.right = '20px';
        toast.style.background = warna;
        toast.style.color = 'white';
        toast.style.padding = '10px 15px';
        toast.style.borderRadius = '6px';
        toast.style.boxShadow = '0 2px 6px rgba(0,0,0,0.2)';
        toast.style.zIndex = 1000;
        document.body.appendChild(toast);
        setTimeout(() => toast.remove(), 3000);
    }
    </script>
</body>
</html>
