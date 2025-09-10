<?php
require_once 'init.php';

// Query data pembelian awal
$query = mysqli_query($conn, "
    SELECT zbeli.tgl, zbeli.nonota, zsupplier.namasup, zbeli.nilai, zbeli.lunas 
    FROM zbeli
    JOIN zsupplier ON zbeli.kodesup = zsupplier.kodesup
    ORDER BY zbeli.tgl DESC, zbeli.nonota DESC
    LIMIT 10
");

?>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
    <title>Data Pembelian</title>
    <link rel="stylesheet" href="navbar.css" />
    <link rel="stylesheet" href="form.css" />
</head>
<body>
    <button class="hamburger" onclick="toggleSidebar()">☰</button>
    <div class="overlay" id="sidebarOverlay" onclick="toggleSidebar()"></div>
    <?php 
    include 'navbar.php';

    $aksesInput = $_SESSION['aksesSemua']['inputpembelian.php'] ?? ['tambah'=>0];
    $aksesEdit  = $_SESSION['aksesSemua']['editpembelian.php'] ?? ['tambah'=>0];

    $hakTambah = $aksesInput['tambah'];
    $hakUbah   = $aksesEdit['tambah'];?>

    <main>
        <input type="hidden" name="kodeuser" value="<?= $kodeuser ?>">

        <h2>Data Pembelian</h2>

        <div class="action-bar">
            <button type="button" name="btnTambah" id="btnTambah" class="action-btn tambah" onclick="cekAkses()">Tambah</button>

            <input type="text" name="noNota" id="inputNoNota" placeholder="No Nota" style="text-transform: uppercase;">

            <input type="date" name="tgl1" id="inputTgl1">
            <span>%</span>
            <input type="date" name="tgl2" id="inputTgl2">

            <input type="hidden" name="kodesup" id="inputKodesup" style="text-transform: uppercase;">

            <input type="text" name="namasup" id="inputNamasup" placeholder="Nama supplier" class="medium-input" style="text-transform: uppercase;">

            <select name="statusBayar" id="statusBayar">
                <option value="all">ALL</option>
                <option value="lunas">Lunas</option>
                <option value="belum">Belum Lunas</option>
            </select>

            <button type="button" name="btnSearch" id="btnSearch" class="action-btn cari" onclick="triggerSearch()">🔍 Cari</button>
        </div>

        <form id="pembelianForm" action="prosespembelian.php" method="POST" enctype="multipart/form-data" onsubmit="return validateForm()"> 
            <div id="tabelHasil" class="form-grid" style="margin-top: 20px;">
                <table class="tabel-hasil">
                    <thead>
                        <tr>
                            <th>Tanggal</th>
                            <th>No. Nota</th>
                            <th>Nama Supplier</th>
                            <th>Nilai</th>
                            <th>Status</th>
                        </tr>
                    </thead>
                    <tbody>
                        <!--Tabel Isi -->
                        <?php while ($row = mysqli_fetch_assoc($query)): ?>
                            <tr onclick="tampilkanKonfirmasiEdit('<?= $row['nonota'] ?>')">
                                <td><?= $row['tgl'] ?></td>
                                <td><?= $row['nonota'] ?></td>
                                <td><?= $row['namasup'] ?></td>
                                <td style="text-align: right;"><?= number_format($row['nilai'], 0, ',', '.') ?></td>
                                <td><?= $row['lunas'] ? 'LUNAS' : 'BELUM' ?></td>
                            </tr>
                        <?php endwhile; ?>
                    </tbody>
                </table>
            </div>
        </form>
    </main>

    <div id="popupCariSupplier" class="popup-pb-cari" style="display: none;">
        <div class="popup-pb-contentcari">
            <h3>Pilih Supplier</h3>
            <table class="tabel-hasil" style="min-width: 700px;">
                <thead>
                    <tr>
                        <th>Kode</th>
                        <th>Nama</th>
                        <th>Alamat</th>
                        <th>Aksi</th>
                    </tr>
                </thead>
                <tbody id="tbodyHasilSupplier">
                    <!-- hasil dari filter_barang.php -->
                </tbody>
            </table>
            <div style="text-align: right; margin-top: 10px;">
                <button type="button" onclick="tutupPopupSupplier()">Tutup</button>
            </div>
        </div>
    </div>

    <script>
        const hakTambah = <?php echo $hakTambah; ?>;
        const hakUbah   = <?php echo $hakUbah; ?>;

        function cekAkses() {
            if (hakTambah == 1) {
                window.location.href = 'inputpembelian.php';
            } else {
                showToast("Anda tidak memiliki hak akses untuk Tambah!", "#dc3545");
            }
        }
        const kodeInput = document.getElementById('inputKodesup');
        const namaInput = document.getElementById('inputNamasup');

        [kodeInput, namaInput].forEach(input => {
            const tipe = input.id === 'inputNamasup' ? 'kode' : 'nama';

            input.addEventListener('keypress', function(e) {
                if (e.key === 'Enter') {
                    e.preventDefault();
                    const val = this.value.trim();
                    if (val) cariSupplier(tipe, val);
                }
            });
        });

        function cariSupplier(mode, keyword) {
            if (!keyword) return;

            fetch('filter_itemsupplier.php', {
                method: 'POST',
                headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
                body: `mode=${mode}&keyword=${encodeURIComponent(keyword)}`
            })
            .then(res => res.json())
            .then(data => {
                const tbody = document.getElementById('tbodyHasilSupplier');
                tbody.innerHTML = '';

                if (data.length === 0) {
                    tbody.innerHTML = '<tr><td colspan="6" style="text-align:center;">Data tidak ditemukan!</td></tr>';
                } else {
                    data.forEach(item => {
                        const tr = document.createElement('tr');
                        tr.innerHTML = `
                            <td>${item.kodesup}</td>
                            <td>${item.namasup}</td>
                            <td>${item.alamat}</td>
                            <td><button type="button" onclick='pilihSupplier(${JSON.stringify(item)})'>Pilih</button></td>
                        `;
                        tbody.appendChild(tr);
                    });
                }

                document.getElementById('popupCariSupplier').style.display = 'flex';
            })
            .catch(() => {
                showToast('Terjadi kesalahan saat mencari barang', '#dc3545');
            });
        }

        function pilihSupplier(item) {
            kodeInput.value = item.kodesup;
            namaInput.value = item.namasup;
            tutupPopupSupplier();
        }

        function tutupPopupSupplier() {
            document.getElementById('popupCariSupplier').style.display = 'none';
        }

        window.addEventListener("DOMContentLoaded", () => {
            const today = new Date().toISOString().split('T')[0];
            document.getElementById("inputTgl1").value = today;
            document.getElementById("inputTgl2").value = today;
        });

        function triggerSearch() {
            const noNota = document.getElementById('inputNoNota').value.trim();
            const tgl1 = document.getElementById('inputTgl1').value;
            const tgl2 = document.getElementById('inputTgl2').value;
            const namasup = document.getElementById('inputNamasup').value;
            const status = document.getElementById('statusBayar').value;

            const params = new URLSearchParams({
                nonota: noNota,
                tgl1: tgl1,
                tgl2: tgl2,
                namasup: namasup,
                status: status,
            });

            fetch('fillpembelian.php?' + params.toString())
                .then(res => res.json())
                .then(data => {
                    const tbody = document.querySelector(".tabel-hasil tbody");
                    tbody.innerHTML = ''; // kosongkan dulu

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
                        <td>${row.namasup}</td>
                        <td style="text-align:right">${parseInt(row.nilai).toLocaleString('id-ID')}</td>
                        <td>${row.lunas === "1" ? "LUNAS" : "BELUM"}</td>
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
                window.location.href = 'editpembelian.php?nonota=' + encodeURIComponent(nonota);
            } else {
                showToast("Anda tidak memiliki hak akses untuk Edit!", "#dc3545");
            };
        }


        document.getElementById('inputNoNota').addEventListener('input', () => {
            const noNota = document.getElementById('inputNoNota').value.trim();
            const isFilled = noNota !== '';

            document.getElementById('inputTgl1').disabled = isFilled;
            document.getElementById('inputTgl2').disabled = isFilled;
            document.getElementById('inputNamasup').disabled = isFilled;
            document.getElementById('statusBayar').disabled = isFilled;
        });

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
