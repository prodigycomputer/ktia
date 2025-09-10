<div class="kop-surat" style="margin-bottom: 10px;">
    <div style="display: flex; justify-content: space-between; align-items: flex-start;">
        <!-- Kiri: Nama dan alamat perusahaan -->
        <div>
            <h2 style="margin: 0; font-size: 22px;">Prodigy Computer</h2>
            <p style="margin: 0; font-size: 16px;">Gajah Mada, Jl. Setia Budi No 21/115, Pontianak</p>
        </div>

        <!-- Kanan: Judul Nota -->
        <div>
            <?php
            $filename = basename($_SERVER['PHP_SELF']); // nama file php yang sedang dibuka
            if ($filename === 'notaprintpem.php') {
                echo '<h2 style="margin: 0; font-size: 22px;">Nota Pembelian</h2>';
            } elseif ($filename === 'notaprintpnj.php') {
                echo '<h2 style="margin: 0; font-size: 22px;">Nota Penjualan</h2>';
            } else {
                echo '<h2 style="margin: 0; font-size: 22px;">Nota</h2>'; // default
            }
            ?>
        </div>
    </div>
    
</div>

