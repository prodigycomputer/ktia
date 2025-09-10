<?php
require_once 'init.php';

// Query data pembelian awal
$query = mysqli_query($conn, "
    SELECT zjual.tgl, zjual.nonota, zkustomer.namakust, zsales.namasls, zjual.nilai, zjual.lunas 
    FROM zjual
    JOIN zkustomer ON zjual.kodekust = zkustomer.kodekust
    JOIN zsales ON zjual.kodesls = zsales.kodesls
    ORDER BY zjual.tgl DESC, zjual.nonota DESC
    LIMIT 10
");

// Ambil data supplier untuk dropdown}

?>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
    <title>Data Penjualan</title>
    <link rel="stylesheet" href="navbar.css" />
    <link rel="stylesheet" href="form.css" />
</head>
<body>
    <button class="hamburger" onclick="toggleSidebar()">☰</button>
    <div class="overlay" id="sidebarOverlay" onclick="toggleSidebar()"></div>
    <?php 
    include 'navbar.php'; 

    $aksesInput = $_SESSION['aksesSemua']['inputpenjualan.php'] ?? ['tambah'=>0];
    $aksesEdit  = $_SESSION['aksesSemua']['editpenjualan.php'] ?? ['tambah'=>0];

    $hakTambah = $aksesInput['tambah'];
    $hakUbah   = $aksesEdit['tambah'];?>

    <main>
        <input type="hidden" name="kodeuser" value="<?= $kodeuser ?>">
        
        <h2>Data Penjualan</h2>

        <div class="action-bar">
            <button type="button" name="btnTambah" id="btnTambah" class="action-btn tambah" onclick="window.location.href='inputpenjualan.php'">Tambah</button>

            <input type="text" name="noNota" id="inputNoNota" placeholder="No Nota" style="text-transform: uppercase;">

            <input type="date" name="tgl1" id="inputTgl1">
            <span>%</span>
            <input type="date" name="tgl2" id="inputTgl2">

            <input type="hidden" name="kodekust" id="inputKodekust" style="text-transform: uppercase;">

            <input type="text" name="namakust" id="inputNamakust" placeholder="Nama Kustomer" autocomplete="off" style="text-transform: uppercase;">

            <input type="hidden" name="kodesls" id="inputKodesls" style="text-transform: uppercase;">

            <input type="text" name="namasls" id="inputNamasls" placeholder="Nama Sales" autocomplete="off" style="text-transform: uppercase;">

            <select name="statusBayar" id="statusBayar">
                <option value="all">ALL</option>
                <option value="lunas">Lunas</option>
                <option value="belum">Belum Lunas</option>
            </select>

            <button type="button" name="btnSearch" id="btnSearch" class="action-btn cari" onclick="triggerSearch()">🔍 Cari</button>
        </div>

        <form id="penjualanForm" action="prosespenjualan.php" method="POST" enctype="multipart/form-data" onsubmit="return validateForm()"> 
            <div id="tabelHasil" class="form-grid" style="margin-top: 20px;">
                <table class="tabel-hasil">
                    <thead>
                        <tr>
                            <th>Tanggal</th>
                            <th>No. Nota</th>
                            <th>Nama Kustomer</th>
                            <th>Nama Sales</th>
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
                                <td><?= $row['namakust'] ?></td>
                                <td><?= $row['namasls'] ?></td>
                                <td style="text-align: right;"><?= number_format($row['nilai'], 0, ',', '.') ?></td>
                                <td><?= $row['lunas'] ? 'LUNAS' : 'BELUM' ?></td>
                            </tr>
                        <?php endwhile; ?>
                    </tbody>
                </table>
            </div>
        </form>
    </main>

    <div id="popupCariKustomer" class="popup-pb-cari" style="display: none;">
        <div class="popup-pb-contentcari">
            <h3>Pilih Kustomer</h3>
            <table class="tabel-hasil" style="min-width: 700px;">
                <thead>
                    <tr>
                        <th>Kode</th>
                        <th>Nama</th>
                        <th>Alamat</th>
                        <th>Kode Harga</th>
                        <th>Aksi</th>
                    </tr>
                </thead>
                <tbody id="tbodyHasilKustomer">
                    <!-- hasil dari filter_barang.php -->
                </tbody>
            </table>
            <div style="text-align: right; margin-top: 10px;">
                <button type="button" onclick="tutupPopupKustomer()">Tutup</button>
            </div>
        </div>
    </div>
    <div id="popupCariSales" class="popup-pb-cari" style="display: none;">
        <div class="popup-pb-contentcari">
            <h3>Pilih Sales</h3>
            <table class="tabel-hasil" style="min-width: 700px;">
                <thead>
                    <tr>
                        <th>Kode</th>
                        <th>Nama</th>
                        <th>Aksi</th>
                    </tr>
                </thead>
                <tbody id="tbodyHasilSales">
                    <!-- hasil dari filter_barang.php -->
                </tbody>
            </table>
            <div style="text-align: right; margin-top: 10px;">
                <button type="button" onclick="tutupPopupSales()">Tutup</button>
            </div>
        </div>
    </div>

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

        const hakTambah = <?php echo $hakTambah; ?>;
        const hakUbah   = <?php echo $hakUbah; ?>;

        function cekAkses() {
            if (hakTambah == 1) {
                window.location.href = 'inputpenjualan.php';
            } else {
                showToast("Anda tidak memiliki hak akses untuk Tambah!", "#dc3545");
            }
        }

        const kodekustInput = document.getElementById('inputKodekust');
        const namakustInput = document.getElementById('inputNamakust');

        const kodeslsInput = document.getElementById('inputKodesls');
        const namaslsInput = document.getElementById('inputNamasls');

        [kodekustInput, namakustInput].forEach(input => {
            const tipe = input.id === 'inputNamakust' ? 'kode' : 'nama';

            input.addEventListener('keypress', function(e) {
                if (e.key === 'Enter') {
                    e.preventDefault();
                    const val = this.value.trim();
                    if (val) cariKustomer(tipe, val);
                }
            });
        });

        [kodeslsInput, namaslsInput].forEach(input => {
            const tipe = input.id === 'inputNamasls' ? 'kode' : 'nama';

            input.addEventListener('keypress', function(e) {
                if (e.key === 'Enter') {
                    e.preventDefault();
                    const val = this.value.trim();
                    if (val) cariSales(tipe, val);
                }
            });
        });

        function cariKustomer(mode, keyword) {
            if (!keyword) return;

            fetch('filter_itemkustomer.php', {
                method: 'POST',
                headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
                body: `mode=${mode}&keyword=${encodeURIComponent(keyword)}`
            })
            .then(res => res.json())
            .then(data => {
                const tbody = document.getElementById('tbodyHasilKustomer');
                tbody.innerHTML = '';

                if (data.length === 0) {
                    tbody.innerHTML = '<tr><td colspan="6" style="text-align:center;">Data tidak ditemukan!</td></tr>';
                } else {
                    data.forEach(item => {
                        const tr = document.createElement('tr');
                        tr.innerHTML = `
                            <td>${item.kodekust}</td>
                            <td>${item.namakust}</td>
                            <td>${item.alamat}</td>
                            <td>${item.kodehrg}</td>
                            <td><button type="button" onclick='pilihKustomer(${JSON.stringify(item)})'>Pilih</button></td>
                        `;
                        tbody.appendChild(tr);
                    });
                }

                document.getElementById('popupCariKustomer').style.display = 'flex';
            })
            .catch(() => {
                showToast('Terjadi kesalahan saat mencari barang', '#dc3545');
            });
        }

        function cariSales(mode, keyword) {
            if (!keyword) return;

            fetch('filter_itemsales.php', {
                method: 'POST',
                headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
                body: `mode=${mode}&keyword=${encodeURIComponent(keyword)}`
            })
            .then(res => res.json())
            .then(data => {
                const tbody = document.getElementById('tbodyHasilSales');
                tbody.innerHTML = '';

                if (data.length === 0) {
                    tbody.innerHTML = '<tr><td colspan="6" style="text-align:center;">Data tidak ditemukan!</td></tr>';
                } else {
                    data.forEach(item => {
                        const tr = document.createElement('tr');
                        tr.innerHTML = `
                            <td>${item.kodesls}</td>
                            <td>${item.namasls}</td>
                            <td><button type="button" onclick='pilihSales(${JSON.stringify(item)})'>Pilih</button></td>
                        `;
                        tbody.appendChild(tr);
                    });
                }

                document.getElementById('popupCariSales').style.display = 'flex';
            })
            .catch(() => {
                showToast('Terjadi kesalahan saat mencari barang', '#dc3545');
            });
        }

        function pilihKustomer(item) {
            kodekustInput.value = item.kodekust;
            namakustInput.value = item.namakust;
            tutupPopupKustomer();
        }

        function pilihSales(item) {
            kodeslsInput.value = item.kodesls;
            namaslsInput.value = item.namasls;
            tutupPopupSales();
        }

        function tutupPopupKustomer() {
            document.getElementById('popupCariKustomer').style.display = 'none';
        }

        function tutupPopupSales() {
            document.getElementById('popupCariSales').style.display = 'none';
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
            const namakust = document.getElementById('inputNamakust').value;
            const namasls = document.getElementById('inputNamasls').value;
            const status = document.getElementById('statusBayar').value;

            const params = new URLSearchParams({
                nonota: noNota,
                tgl1: tgl1,
                tgl2: tgl2,
                namakust: namakust,
                namasls: namasls,
                status: status,
            });

            fetch('fillpenjualan.php?' + params.toString())
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
                        <td>${row.namakust}</td>
                        <td>${row.namasls}</td>
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
                window.location.href = 'editpenjualan.php?nonota=' + encodeURIComponent(nonota);
            } else {
                showToast("Anda tidak memiliki hak akses untuk Edit!", "#dc3545");
            };
        }


        document.getElementById('inputNoNota').addEventListener('input', () => {
            const noNota = document.getElementById('inputNoNota').value.trim();
            const isFilled = noNota !== '';

            document.getElementById('inputTgl1').disabled = isFilled;
            document.getElementById('inputTgl2').disabled = isFilled;
            document.getElementById('inputNamakust').disabled = isFilled;
            document.getElementById('inputNamasls').disabled = isFilled;
            document.getElementById('statusBayar').disabled = isFilled;
        });

        // Autocomplete supplier
        

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
