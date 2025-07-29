<?php
session_start();
include 'koneksi.php';

// Query data pembelian awal
$query = mysqli_query($conn, "
    SELECT zbeli.tgl, zbeli.nonota, zsupplier.namasup, zbeli.nilai, zbeli.lunas 
    FROM zbeli
    JOIN zsupplier ON zbeli.kodesup = zsupplier.kodesup
    ORDER BY zbeli.tgl DESC, zbeli.nonota DESC
    LIMIT 10
");

// Ambil data supplier untuk dropdown
$suppliers = [];
$supplierQuery = $conn->query("SELECT kodesup, namasup FROM zsupplier ORDER BY namasup");
while ($row = $supplierQuery->fetch_assoc()) {
    $suppliers[] = $row;
}
?>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
    <title>Data Pembelian</title>
    <link rel="stylesheet" href="navbar.css" />
    <link rel="stylesheet" href="form.css" />
    <style>
        .dropdown-result {
            position: absolute;
            background: white;
            border: 1px solid #ccc;
            max-height: 150px;
            overflow-y: auto;
            width: 100%;
            z-index: 999;
            display: none;
        }
        .dropdown-item {
            padding: 8px;
            cursor: pointer;
        }
        .dropdown-item:hover {
            background-color: #f0f0f0;
        }
    </style>
</head>
<body>
    <button class="hamburger" onclick="toggleSidebar()">☰</button>
    <div class="overlay" id="sidebarOverlay" onclick="toggleSidebar()"></div>
    <?php include 'navbar.php'; ?>

    <main>
        <h2>Data Pembelian</h2>

        <div class="action-bar">
            <button type="button" name="btnTambah" id="btnTambah" class="action-btn tambah" onclick="window.location.href='inputpembelian.php'">Tambah</button>

            <input type="text" name="noNota" id="inputNoNota" placeholder="No Nota" style="text-transform: uppercase;">

            <input type="date" name="tgl1" id="inputTgl1">
            <span>%</span>
            <input type="date" name="tgl2" id="inputTgl2">

            <input type="hidden" name="kodesup" id="inputKodesup" style="text-transform: uppercase;">

            <div style="position: relative;">
                <input type="text" name="namasup" id="inputNamasup" placeholder="Nama supplier" autocomplete="off" style="text-transform: uppercase;">
                <div id="dropdownSupplier" class="dropdown-result"></div>
            </div>

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
                            <th>Aksi</th>
                        </tr>
                    </thead>
                    <tbody>
                        <?php while($row = mysqli_fetch_assoc($query)): ?>
                            <tr>
                                <td><?= date('d-m-Y', strtotime($row['tgl'])) ?></td>
                                <td><?= htmlspecialchars($row['nonota']) ?></td>
                                <td><?= htmlspecialchars($row['namasup']) ?></td>
                                <td style="text-align:right"><?= number_format($row['nilai']) ?></td>
                                <td><?= $row['lunas'] ? 'Lunas' : 'Belum' ?></td>
                                <td>
                                    <button onclick="editPembelian('<?= $row['nonota'] ?>')">Edit</button>
                                    <button type="button" onclick="hapusPembelian('<?= $row['nonota'] ?>')">Hapus</button>
                                </td>
                            </tr>
                        <?php endwhile; ?>
                    </tbody>
                </table>
            </div>
        </form>
    </main>

    <div id="popupConfirmHapus" style="
        display: none;
        position: fixed;
        top: 0; left: 0;
        width: 100%; height: 100%;
        background: rgba(0,0,0,0.4);
        z-index: 1001;
    ">
        <div style="
            position: absolute;
            top: 50%; left: 50%;
            transform: translate(-50%, -50%);
            background: white;
            padding: 20px 25px;
            border-radius: 8px;
            box-shadow: 0 4px 15px rgba(0,0,0,0.3);
            width: 320px;
            max-width: 90%;
            text-align: center;
        ">
            <p style="font-size: 14px; margin-bottom: 20px;">Apakah Anda yakin ingin menghapus data ini?</p>
            <button onclick="konfirmasiHapus(true)" style="margin-right: 10px; padding: 6px 12px; background: #dc3545; color: white; border: none; border-radius: 4px;">Ya</button>
            <button onclick="konfirmasiHapus(false)" style="padding: 6px 12px; background: #6c757d; color: white; border: none; border-radius: 4px;">Tidak</button>
        </div>
    </div>

    <script>
        const supplierList = <?= json_encode($suppliers) ?>;

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
                limit: 10 
            });

            fetch('fillpembelian.php?' + params.toString())
                .then(res => res.text())
                .then(html => {
                    document.getElementById("tabelHasil").innerHTML = html;
                })
                .catch(err => {
                    alert("Gagal mengambil data: " + err);
                });
        }

        function editPembelian(nonota) {
            alert("Edit: " + nonota);
        }

        let nonotaUntukDihapus = '';

        function hapusPembelian(nonota) {
            nonotaUntukDihapus = nonota;
            document.getElementById('popupConfirmHapus').style.display = 'block';
        }

        function konfirmasiHapus(ya) {
            document.getElementById('popupConfirmHapus').style.display = 'none';

            if (ya && nonotaUntukDihapus !== '') {
                const formData = new URLSearchParams();
                formData.append("aksi", "hapus");
                formData.append("nonota", nonotaUntukDihapus);

                fetch('prosespembelian.php', {
                    method: 'POST',
                    body: formData
                })
                .then(res => res.json())
                .then(data => {
                    if (data.success) {
                        showToast(data.message);
                        triggerSearch();
                    } else {
                        alert("Gagal menghapus: " + data.message);
                    }
                })
                .catch(err => {
                    alert("Terjadi kesalahan saat menghapus: " + err);
                });

                nonotaUntukDihapus = '';
            }
        }

        document.getElementById('inputNoNota').addEventListener('input', () => {
            const noNota = document.getElementById('inputNoNota').value.trim();
            const isFilled = noNota !== '';

            document.getElementById('inputTgl1').disabled = isFilled;
            document.getElementById('inputTgl2').disabled = isFilled;
            document.getElementById('inputNamasup').disabled = isFilled;
            document.getElementById('statusBayar').disabled = isFilled;
        });

        // Autocomplete supplier
        const inputNamasup = document.getElementById("inputNamasup");
        const kodesupField = document.getElementById("inputKodesup");
        const dropdown = document.getElementById("dropdownSupplier");

        inputNamasup.addEventListener("input", function () {
            const val = this.value.trim().toUpperCase();
            dropdown.innerHTML = "";

            if (val === "") {
                dropdown.style.display = "none";
                return;
            }

            const filtered = supplierList.filter(sup =>
                sup.namasup.toUpperCase().includes(val)
            );

            if (filtered.length === 0) {
                dropdown.innerHTML = "<div class='dropdown-item'>Tidak ditemukan</div>";
            } else {
                filtered.forEach(sup => {
                    const item = document.createElement("div");
                    item.classList.add("dropdown-item");
                    item.textContent = `${sup.namasup}`;
                    item.dataset.kodesup = sup.kodesup;
                    item.dataset.namasup = sup.namasup;
                    dropdown.appendChild(item);
                });
            }

            dropdown.style.display = "block";
        });

        dropdown.addEventListener("click", function (e) {
            if (e.target.classList.contains("dropdown-item")) {
                const kodesup = e.target.dataset.kodesup;
                const namasup = e.target.dataset.namasup;

                inputNamasup.value = namasup;
                kodesupField.value = kodesup;
                dropdown.innerHTML = "";
                dropdown.style.display = "none";
            }
        });

        document.addEventListener("click", function (e) {
            if (!inputNamasup.contains(e.target) && !dropdown.contains(e.target)) {
                dropdown.style.display = "none";
            }
        });

        function showToast(pesan) {
                const toast = document.createElement('div');
                toast.textContent = pesan;
                toast.style.position = 'fixed';
                toast.style.top = '20px';   
                toast.style.right = '20px';
                toast.style.background = '#28a745';
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
