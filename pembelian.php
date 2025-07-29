<?php
session_start();

include 'koneksi.php';

?>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Data Pembelian</title>
    <link rel="stylesheet" href="navbar.css">
    <link rel="stylesheet" href="form.css">
</head>
<body>
    <button class="hamburger" onclick="toggleSidebar()">☰</button>
    <div class="overlay" id="sidebarOverlay" onclick="toggleSidebar()"></div>
    <?php include 'navbar.php'; ?>

    <main>
        <h2>Data Pembelian</h2>

            <div class="action-bar">
                <button type="button" name="btnTambah" id="btnTambah" class="action-btn tambah">➕ Tambah</button>

                <input type="text" name="noNota" id="inputNoNota" placeholder="No Nota" style="text-transform: uppercase;">

                <input type="date" name="tgl1" id="inputTgl1">
                <span>%</span>
                <input type="date" name="tgl2" id="inputTgl2">

                <input type="hidden" name="kodesup" id="inputKodesup" style="text-transform: uppercase;">

                <input type="text" name="namasup" id="inputNamasup" placeholder="Nama sup" style="text-transform: uppercase;">

                <select name="statusBayar" id="statusBayar">
                    <option value="all">ALL</option>
                    <option value="lunas">Lunas</option>
                    <option value="belum">Belum Lunas</option>
                </select>

                <button type="button" name="btnSearch" id="btnSearch" class="action-btn cari" onclick="triggerSearch()">🔍 Cari</button>
            </div>

            <form id="pembelianForm" action="prosespembelian.php" method="POST" enctype="multipart/form-data" onsubmit="return validateForm()"> 

                <div class="form-tabel">
                    <div id="tabelHasil" style="overflow-x: auto;"></div>
                </div>
        </form>
    </main>
    <script>
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
                status: status
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
            // Lanjutkan dengan fetch atau isi form
        }

        function hapusPembelian(nonota) {
            if (confirm("Yakin ingin menghapus nota " + nonota + "?")) {
                // Kirim ke PHP hapus atau fetch untuk hapus
                alert("Hapus: " + nonota);
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
    </script>
</body>
</html>