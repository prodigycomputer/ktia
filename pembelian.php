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

    <div id="popupConfirmEdit" style="
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
            <p style="font-size: 14px; margin-bottom: 20px;">Apakah Anda yakin ingin mengubah data ini?</p>
            <button onclick="konfirmasiEdit(true)" style="margin-right: 10px; padding: 6px 12px; background: #35c3dcff; color: white; border: none; border-radius: 4px;">Ya</button>
            <button onclick="konfirmasiEdit(false)" style="padding: 6px 12px; background: #6c757d; color: white; border: none; border-radius: 4px;">Tidak</button>
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
            const popup = document.getElementById('popupConfirmEdit');
            popup.style.display = 'block';
            popup.setAttribute('data-nonota', nonota);
        }

        function konfirmasiEdit(ya) {
            const popup = document.getElementById('popupConfirmEdit');
            const nonota = popup.getAttribute('data-nonota');
            popup.style.display = 'none';

            if (ya && nonota) {
                window.location.href = 'editpembelian.php?nonota=' + encodeURIComponent(nonota);
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

        const inputKode = document.getElementById("kode_sup");
        const inputNama = document.getElementById("nama_sup");
        const dropdownKode = document.getElementById("dropdownKodeSup");
        const dropdownNama = document.getElementById("dropdownNamaSup");
        const alamatField = document.getElementById("alamat");

        // Autocomplete berdasarkan KODE
        inputKode.addEventListener("input", function () {
            const val = this.value.trim().toUpperCase();
            dropdownKode.innerHTML = "";

            if (val === "") {
                dropdownKode.style.display = "none";
                return;
            }

            const filtered = supplierList.filter(sup => sup.kodesup.toUpperCase().includes(val));

            if (filtered.length === 0) {
                dropdownKode.innerHTML = "<div class='dropdown-item'>Tidak ditemukan</div>";
            } else {
                filtered.forEach(sup => {
                    const item = document.createElement("div");
                    item.classList.add("dropdown-item");
                    item.textContent = `${sup.kodesup} - ${sup.namasup}`;
                    item.dataset.kodesup = sup.kodesup;
                    item.dataset.namasup = sup.namasup;
                    item.dataset.alamat = sup.alamat ?? '';
                    dropdownKode.appendChild(item);
                });
            }

            dropdownKode.style.display = "block";
        });

        // Autocomplete berdasarkan NAMA
        inputNama.addEventListener("input", function () {
            const val = this.value.trim().toUpperCase();
            dropdownNama.innerHTML = "";

            if (val === "") {
                dropdownNama.style.display = "none";
                return;
            }

            const filtered = supplierList.filter(sup => sup.namasup.toUpperCase().includes(val));

            if (filtered.length === 0) {
                dropdownNama.innerHTML = "<div class='dropdown-item'>Tidak ditemukan</div>";
            } else {
                filtered.forEach(sup => {
                    const item = document.createElement("div");
                    item.classList.add("dropdown-item");
                    item.textContent = `${sup.namasup} (${sup.kodesup})`;
                    item.dataset.kodesup = sup.kodesup;
                    item.dataset.namasup = sup.namasup;
                    item.dataset.alamat = sup.alamat ?? '';
                    dropdownNama.appendChild(item);
                });
            }

            dropdownNama.style.display = "block";
        });

        // Pilih dari dropdown KODE
        dropdownKode.addEventListener("click", function (e) {
            if (e.target.classList.contains("dropdown-item")) {
                inputKode.value = e.target.dataset.kodesup;
                inputNama.value = e.target.dataset.namasup;
                alamatField.value = e.target.dataset.alamat;
                dropdownKode.style.display = "none";
            }
        });

        // Pilih dari dropdown NAMA
        dropdownNama.addEventListener("click", function (e) {
            if (e.target.classList.contains("dropdown-item")) {
                inputNama.value = e.target.dataset.namasup;
                inputKode.value = e.target.dataset.kodesup;
                alamatField.value = e.target.dataset.alamat;
                dropdownNama.style.display = "none";
            }
        });

        // Tutup dropdown jika klik di luar
        document.addEventListener("click", function (e) {
            if (!inputKode.contains(e.target) && !dropdownKode.contains(e.target)) {
                dropdownKode.style.display = "none";
            }
            if (!inputNama.contains(e.target) && !dropdownNama.contains(e.target)) {
                dropdownNama.style.display = "none";
            }
        });

    </script>
</body>
</html>
