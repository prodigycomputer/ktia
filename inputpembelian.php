<?php
session_start();
include 'koneksi.php';
$nonota = $_GET['nonota'] ?? '';
?>
<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Input Pembelian</title>
    <link rel="stylesheet" href="navbar.css">
    <link rel="stylesheet" href="form.css">
</head>
<body>
    <button class="hamburger" onclick="toggleSidebar()">☰</button>
    <div class="overlay" id="sidebarOverlay" onclick="toggleSidebar()"></div>
    <?php include 'navbar.php'; ?>

    <main>
        <h2>Input Pembelian</h2>
        <div class="action-pb-bar" style="display: flex; justify-content: space-between; align-items: center; margin-top: 10px;">
            <div style="display: flex; gap: 8px;">
                <button id="btnSave" type="submit">Simpan</button>
                <button id="btnTambah" type="button" onclick="initializeTambah()">Tambah</button>
                <!-- <button id="btnEdit" type="button" onclick="initializeUbah()">Ubah</button> -->    
                <button id="btnCancel" type="button" onclick="cancelForm()">Batal</button>
            </div>
            <button id="btnKembali" type="button">Kembali</button>
        </div>

        <form id="formPembelian" action="prosespembelian.php" method="POST">
            <div id="form-pembelian-atas">
                <div class="form-pb-row">
                    <div class="form-pb-col">
                        <label for="tanggal">Tanggal</label>
                        <input type="date" id="tanggal" name="tanggal" class="short-input">
                    </div>
                    <div class="form-pb-col">
                        <label for="kode_supplier">Kode Supplier</label>
                        <input type="text" id="kode_supplier" name="kode_supplier" class="short-input">
                    </div>
                </div>

                <div class="form-pb-row">
                    <div class="form-pb-col">
                        <label for="no_nota">No Nota</label>
                        <input type="text" id="no_nota" name="no_nota" class="short-input">
                    </div>
                    <div class="form-pb-col">
                        <label for="nama_supplier">Nama Supplier</label>
                        <input type="text" id="nama_supplier" name="nama_supplier" class="medium-input">
                    </div>
                </div>

                <div class="form-pb-row">
                    <div class="form-pb-col">
                        <label for="jt_tempo">Jatuh Tempo</label>
                        <input type="date" id="jt_tempo" name="jt_tempo" class="short-input">
                    </div>
                    <div class="form-pb-col">
                        <label for="alamat">Alamat</label>
                        <input type="text" id="alamat" name="alamat" class="long-input">
                    </div>
                </div>
            </div>
            <!-- FORM BAWAH: RINCIAN -->
            <div class="form-pembelian-tengah">
                <table class="tabel-hasil" id="tabelPembelian">
                    <thead>
                        <tr>
                            <th>Kode brg</th>
                            <th>Nama brg</th>
                            <th>Jlh 1</th>
                            <th>Satuan 1</th>
                            <th>Jlh 2</th>
                            <th>Satuan 2</th>
                            <th>Jlh 3</th>
                            <th>Satuan 3</th>
                            <th>Harga</th>
                            <th>Disca</th>
                            <th>Discb</th>
                            <th>Discc</th>
                            <th>Disc Rp</th>
                            <th>Jumlah</th>
                        </tr>
                    </thead>
                    <tbody>
                        <!-- Baris contoh (bisa diduplikat lewat JS) -->
                        <?php
                            $nonota = $_GET['nonota'] ?? ''; // Ambil no nota dari parameter atau variabel yang sesuai

                            if ($nonota !== '') {
                                $query = mysqli_query($conn, "SELECT * FROM zbelim WHERE nonota = '$nonota'");
                                while ($row = mysqli_fetch_assoc($query)) {
                                    echo "<tr>";
                                    echo "<td>{$row['kodebrg']}</td>";
                                    echo "<td>{$row['jlh1']}</td>";
                                    echo "<td>{$row['jlh2']}</td>";
                                    echo "<td>{$row['jlh3']}</td>";
                                    echo "<td>" . number_format($row['harga'], 0, ',', '.') . "</td>";
                                    echo "<td>{$row['disca']}</td>";
                                    echo "<td>{$row['discb']}</td>";
                                    echo "<td>{$row['discc']}</td>";
                                    echo "<td>{$row['discrp']}</td>";
                                    echo "<td>" . number_format($row['jumlah'], 0, ',', '.') . "</td>";
                                    echo "</tr>";
                                }
                            }
                        ?>
                    </tbody>
                </table>
            </div>
            <div id="form-pembelian-bawah">
                <div class="form-pb-pos">
                    <div class="form-pb-col">
                        <label for="subtotal">Subtotal</label>
                        <input type="number" id="subtotal" name="subtotal" class="short-input">
                    </div>
                    <div class="form-pb-col">
                        <label for="lain_lain">Lain-Lain</label>
                        <input type="number" id="lain_lain" name="lain_lain" class="short-input">
                    </div>
                    <div class="form-pb-col">
                        <label for="ppn">PPN</label>
                        <input type="number" id="ppn" name="ppn" class="short-input">
                    </div>
                </div>
            </div>
        </form>
    </main>

    <script>
        function tambahBaris() {
            const tbody = document.querySelector('#tabelPembelian tbody');
            const row = tbody.rows[0].cloneNode(true);
            row.querySelectorAll('input').forEach(input => input.value = '');
            tbody.appendChild(row);
        }
    </script>
</body>
</html>
