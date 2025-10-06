<?php
require_once 'init.php';
$nonota = $_GET['nonota'] ?? '';
$q = mysqli_query($conn, "SELECT qppn FROM zconfig LIMIT 1");
$data = mysqli_fetch_assoc($q);
$default_ppn = $data['qppn'] ?? 0; // fallback 0 jika tidak ada

?>
<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Edit Penjualan</title>
    <link rel="stylesheet" href="navbar.css">
    <link rel="stylesheet" href="form.css">
    <script src="hitung.js"></script>
</head>
<body data-kodeuser="<?= $kodeuser ?>">
    <button class="hamburger" onclick="toggleSidebar()">☰</button>
    <div class="overlay" id="sidebarOverlay" onclick="toggleSidebar()"></div>
    <?php 
        require_once 'akses.php';
    ?>

    <main>
        <h2>Edit Penjualan</h2>
        <div class="action-pb-bar" style="display: flex; justify-content: space-between; align-items: center; margin-top: 10px;">
            <div style="display: flex; gap: 8px;">
                <button id="btnSave" type="submit">Simpan</button>
                <button id="btnEdit" type="button" onclick="initializeEdit()">Edit</button>
                <button id="btnHapus" type="button">Hapus</button>    
                <button id="btnCancel" type="button" onclick="cancelForm()">Batal</button>
                <button id="btnPrint" type="button">Print</button>
            </div>
            <button id="btnKembali" type="button" onclick="window.location.href='penjualan.php'">Kembali</button>
        </div>

        <form id="formPenjualan" action="prosespenjualan.php" method="POST">
            <input type="hidden" name="no_nota_lama" id="no_nota_lama" />
            <div id="form-penjualan-atas">
                <div class="form-pj-row">
                    <div class="form-pj-col">
                        <label for="tanggal">Tanggal</label>
                        <input type="date" id="tanggal" name="tanggal" class="short-input" style="text-transform: uppercase;">
                    </div>
                    <div class="form-pj-col">
                        <label for="kode_kust">Kode Kustomer</label>
                        <div style="position: relative; flex: 1;" class="short-input">
                            <input type="text" id="kode_kust" data-table="zkustomer" data-field="kodekust" data-check="eksistensi" data-reset="nama_kust,alamat" onblur="cekValidasi(this)" name="kode_kust" style="text-transform: uppercase;">
                        </div>
                    </div>
                    <div class="form-pj-col">
                        <label for="kode_sls">Kode Sales</label>
                        <div style="position: relative; flex: 1;" class="short-input">
                            <input type="text" id="kode_sls" data-table="zsales" data-field="kodesls" data-check="eksistensi" data-reset="nama_sls,alamat" onblur="cekValidasi(this)" name="kode_sls" style="text-transform: uppercase;">
                        </div>
                    </div>
                </div>

                <div class="form-pj-row">
                    <div class="form-pj-col">
                        <label for="no_nota">No Nota</label>
                        <input type="text" id="no_nota" data-table="zbeli" data-field="nonota" data-check="duplikat" onblur="cekValidasi(this)" name="no_nota" class="short-input" style="text-transform: uppercase;">
                        
                    </div>
                    <div class="form-pj-col" style="position: relative;">
                        <label for="nama_kust">Nama Kustomer</label>
                        <div style="position: relative; flex: 1;">
                            <input type="text" id="nama_kust" data-table="zkustomer" data-field="namakust" data-check="eksistensi" data-reset="kode_kust,alamat" onblur="cekValidasi(this)" name="nama_kust" class="medlesshort-input" style="text-transform: uppercase;">
                        </div>
                    </div>
                    <div class="form-pj-col" style="position: relative;">
                        <label for="nama_sls">Nama Sales</label>
                        <div style="position: relative; flex: 1;">
                            <input type="text" id="nama_sls" data-table="zsales" data-field="namasls" data-check="eksistensi" data-reset="kode_sls,alamat" onblur="cekValidasi(this)" name="nama_sls" class="medlesshort-input" style="text-transform: uppercase;">
                        </div>
                    </div>
                </div>

                <div class="form-pj-row">
                    <div class="form-pj-col">
                        <label for="jt_tempo">Jatuh Tempo</label>
                        <input type="date" id="jt_tempo" name="jt_tempo" class="short-input" style="text-transform: uppercase;">
                    </div>
                    <div class="form-pj-col">
                        <label for="alamat">Alamat</label>
                        <input type="text" id="alamat" name="alamat" class="long-input" style="text-transform: uppercase;" disabled>
                    </div>
                </div>
                <div class="form-pj-row">
                    <div class="form-pj-col">
                        <label for="kodehrg">Kode Harga</label>
                        <input type="text" id="kodehrg" name="kodehrg" class="short-input" style="text-transform: uppercase;">
                    </div>
                </div>
                <div class="form-pb-col">
                    <label for="keterangan">Keterangan</label>
                    <input type="text" id="keterangan" name="keterangan" class="long-input" style="text-transform: uppercase;">
                </div>
            </div>
            <!-- FORM BAWAH: RINCIAN -->
            <div class="form-penjualan-tengah">
                <div style="display: flex; justify-content: flex-end;">
                    <button id="btnTambahItem" type="button">+</button>
                </div>
                <div style="overflow-x: auto;">
                    <table class="tabel-hasil" id="tabelPenjualan" style="min-width: 1200px;">
                        <thead>
                            <tr>
                                <th style="min-width: 100px;">Kode brg</th>
                                <th style="min-width: 200px;">Nama brg</th>
                                <th style="min-width: 100px;">Kode Gudang</th>
                                <th style="min-width: 50px;">Jlh 1</th>
                                <th style="min-width: 80px;">Satuan 1</th>
                                <th style="min-width: 50px;">Jlh 2</th>
                                <th style="min-width: 80px;">Satuan 2</th>
                                <th style="min-width: 50px;">Jlh 3</th>
                                <th style="min-width: 80px;">Satuan 3</th>
                                <th style="min-width: 100px;">Harga</th>
                                <th style="min-width: 50px;">Disca</th>
                                <th style="min-width: 50px;">Discb</th>
                                <th style="min-width: 50px;">Discc</th>
                                <th style="min-width: 100px;">Disc Rp</th>
                                <th style="min-width: 100px;">Jumlah</th>
                                <th style="min-width: 120px; display: none;" id="thAksi">Aksi</th>
                            </tr>
                        </thead>
                        <tbody>
                            <!-- isi dari popupitempenjualan-->
                        </tbody>
                    </table>
                </div>
            </div>

            <div id="form-pembelian-bawah">
                <div class="form-pb-pos">
                    <div class="form-pb-col">
                        <label for="subtotal">Subtotal</label>
                        <input type="text" id="subtotal" name="subtotal" style="text-align: right;" class="medshort-input">
                    </div>

                    <div class="form-pb-col">
                        <label for="diskon1">Diskon 1</label>
                        <input type="text" id="diskon1" name="diskon1" style="text-align: right;" value="0" class="veryshort-input">
                        <input type="text" id="hdiskon1" name="hdiskon1" style="text-align: right;" class="lesshort-input">
                    </div>

                    <div class="form-pb-col">
                        <label for="diskon2">Diskon 2</label>
                        <input type="text" id="diskon2" name="diskon2" style="text-align: right;" value="0" class="veryshort-input">
                        <input type="text" id="hdiskon2" name="hdiskon2" style="text-align: right;" class="lesshort-input">
                    </div>

                    <div class="form-pb-col">
                        <label for="diskon3">Diskon 3</label>
                        <input type="text" id="diskon3" name="diskon3" style="text-align: right;" value="0" class="veryshort-input">
                        <input type="text" id="hdiskon3" name="hdiskon3" style="text-align: right;" class="lesshort-input">
                    </div>

                    <div class="form-pb-col">
                        <label for="lain_lain">Lain-Lain</label>
                        <input type="text" id="lain_lain" name="lain_lain" style="text-align: right;" value="0" class="medshort-input">
                    </div>

                    <div class="form-pb-col">
                        <label for="ppn">PPN</label>
                        <input type="text" id="ppn" name="ppn" style="text-align: right;" value="<?= $default_ppn ?>" class="veryshort-input">
                        <input type="text" id="hppn" name="hppn" style="text-align: right;" class="lesshort-input">
                    </div>

                    <div class="form-pb-col">
                        <label for="totaljmlh">Total Jumlah</label>
                        <input type="text" id="totaljmlh" name="totaljmlh" style="text-align: right;" class="medshort-input">
                    </div>
                </div>
            </div>
        </form>
        <div id="popupCariBarang" class="popup-pb-cari" style="display: none;">
            <div class="popup-pb-contentcari">
                <h3>Pilih Barang</h3>
                <table class="tabel-hasil" style="min-width: 700px;">
                    <thead>
                        <tr>
                            <th>Kode</th>
                            <th>Nama</th>
                            <th>Satuan 1</th>
                            <th>Satuan 2</th>
                            <th>Satuan 3</th>
                            <th>Harga</th>
                            <th>Pilih</th>
                        </tr>
                    </thead>
                    <tbody id="tbodyHasilBarang">
                        <!-- hasil dari filter_barang.php -->
                    </tbody>
                </table>
                <div style="text-align: right; margin-top: 10px;">
                    <button type="button" onclick="tutupPopupBarang()">Tutup</button>
                </div>
            </div>
        </div>
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
        <div id="popupPilihHarga" class="popup-pb-cari" style="display:none;">
            <div class="popup-pb-contentcari">
                <h3>Pilih Harga</h3>
                <table class="tabel-hasil" style="min-width: 300px;">
                    <thead>
                        <tr>
                            <th>Tipe Harga</th>
                            <th>Nominal</th>
                            <th>Pilih</th>
                        </tr>
                    </thead>
                    <tbody id="tbodyPilihanHarga">
                        <!-- diisi via JS -->
                    </tbody>
                </table>
                <div style="text-align: right; margin-top: 10px;">
                    <button type="button" onclick="tutupPopupHarga()">Tutup</button>
                </div>
            </div>
        </div>
        <div id="popupForm" class="popup-pb-overlay" style="display: none;">
            <div class="popup-pb-content">
                <h3>Tambah Data Penjualan</h3>
                <form id="formDetailPenjualan">
                    <input type="hidden" name="popup_isi1" id="popup_isi1" value=""> 
                    <input type="hidden" name="popup_isi2" id="popup_isi2" value="">
                    <input type="hidden" name="popup_hdiskon1" id="popup_hdiskon1" value=""> 
                    <input type="hidden" name="popup_hdiskon2" id="popup_hdiskon2" value="">
                    <input type="hidden" name="popup_hdiskon3" id="popup_hdiskon3" value="">
                    <input type="hidden" name="popup_sisa1" id="popup_sisa1" value="">
                    <input type="hidden" name="popup_sisa2" id="popup_sisa2" value="">
                    <input type="hidden" name="popup_sisa3" id="popup_sisa3" value="">  
                    <div class="popup-pb-row">
                        <label for="popup_kodegd">Kode Gudang</label>
                        <select id="popup_kodegd" name="popup_kodegd" required>
                            <option value="">Pilih Gudang</option>
                            <!-- Akan diisi via JavaScript -->
                        </select>
                    </div>
                    <div class="popup-pb-row">
                        <label for="popup_kodebrg">Kode Barang</label>
                        <input type="text" id="popup_kodebrg" data-table="zstok" data-field="kodebrg" data-check="eksistensi" data-reset="popup_namabrg" onblur="cekValidasi(this)" name="popup_kodebrg" style="text-transform: uppercase;">
                    </div>
                    <div class="popup-pb-row">
                        <label for="popup_namabrg">Nama Barang</label>
                        <input type="text" id="popup_namabrg" data-table="zstok" data-field="namabrg" data-check="eksistensi" data-reset="popup_kodebrg" onblur="cekValidasi(this)" name="popup_namabrg" style="text-transform: uppercase;">
                    </div>

                    <div class="popup-pb-row">
                        <label for="popup_sisa">Sisa Stok</label>
                        <input type="text" id="popup_sisa" name="popup_sisa" style="background-color: #e94141ff; color: #fffafaff; " disabled>
                    </div>

                    <div class="popup-pb-row">
                        <label for="popup_jlh1">Jumlah 1</label>
                        <input type="number" id="popup_jlh1" name="popup_jlh1" style="text-align: right;" min="0">
                    </div>
                    <div class="popup-pb-row">
                        <label for="popup_satuan1">Satuan 1</label>
                        <input type="text" id="popup_satuan1" name="popup_satuan1" disabled>
                    </div>

                    <div class="popup-pb-row">
                        <label for="popup_jlh2">Jumlah 2</label>
                        <input type="number" id="popup_jlh2" name="popup_jlh2" style="text-align: right;" min="0" disabled>
                    </div>
                    <div class="popup-pb-row">
                        <label for="popup_satuan2">Satuan 2</label>
                        <input type="text" id="popup_satuan2" name="popup_satuan2" disabled>
                    </div>

                    <div class="popup-pb-row">
                        <label for="popup_jlh3">Jumlah 3</label>
                        <input type="number" id="popup_jlh3" name="popup_jlh3" style="text-align: right;" min="0" disabled>
                    </div>
                    
                    <div class="popup-pb-row">
                        <label for="popup_satuan3">Satuan 3</label>
                        <input type="text" id="popup_satuan3" name="popup_satuan3" disabled>
                    </div>

                    <div class="popup-pb-row">
                        <label for="popup_harga">Harga</label>
                        <input type="number" id="popup_harga" name="popup_harga" style="text-align: right;" value="0">
                    </div>

                    <div class="popup-pb-row">
                        <label for="popup_disca">Disca</label>
                        <input type="number" id="popup_disca" name="popup_disca" style="text-align: right;" value="0">
                    </div>

                    <div class="popup-pb-row">
                        <label for="popup_discb">Discb</label>
                        <input type="number" id="popup_discb" name="popup_discb" style="text-align: right;" value="0">
                    </div>

                    <div class="popup-pb-row">
                        <label for="popup_discc">Discc</label>
                        <input type="number" id="popup_discc" name="popup_discc" style="text-align: right;" value="0">
                    </div>

                    <div class="popup-pb-row">
                        <label for="popup_discrp">Disc Rp</label>
                        <input type="number" id="popup_discrp" name="popup_discrp" style="text-align: right;" value="0">
                    </div>

                    <div class="popup-pb-row">
                        <label for="popup_jumlah">Jumlah</label>
                        <input type="number" id="popup_jumlah" name="popup_jumlah" style="text-align: right;" required>
                    </div>

                    <div class="popup-pb-row" style="justify-content: flex-end; gap: 10px;">
                        <button id="btnSaveItem" type="submit">Oke</button>
                        <button id="btnCancelItem" type="button" onclick="tutupPopup()">Batal</button>
                    </div>
                </form>
            </div>
        </div>
    </main>

    <div id="toast" style="
        display: none;
        position: fixed;
        top: 20px;
        right: 20px;
        padding: 10px 16px;
        border-radius: 6px;
        color: white;
        font-size: 13px;
        z-index: 1000;
        box-shadow: 0 2px 6px rgba(0,0,0,0.2);
    "></div>

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
        let currentstat = null;
        let currentMode = "input";
        let dataPenjualan = [];
        let indexToDelete = null;
        let hapusTipe = 'item';
        function loadPenjualan(nonota) {
            const noNota = new URLSearchParams(window.location.search).get('nonota');

            fetch(`getpenjualan.php?nonota=${noNota}`)
            .then(res => res.json())
            .then(data => {
                if (data.status === 'success') {
                    // Isi form header
                    document.getElementById('tanggal').value = data.header.tanggal;
                    document.getElementById('no_nota').value = data.header.no_nota;
                    document.getElementById('no_nota_lama').value = data.header.no_nota;
                    document.getElementById('kode_kust').value = data.header.kode_kust;
                    document.getElementById('nama_kust').value = data.header.nama_kust;
                    document.getElementById('kode_sls').value = data.header.kode_sls;
                    document.getElementById('nama_sls').value = data.header.nama_sls;
                    document.getElementById('alamat').value = data.header.alamat;
                    document.getElementById('jt_tempo').value = data.header.jt_tempo;
                    document.getElementById('kodehrg').value = data.header.kodehrg;
                    document.getElementById('keterangan').value = data.header.keterangan;
                    document.getElementById('diskon1').value = data.header.disk1;
                    document.getElementById('hdiskon1').value = data.header.hdisk1;
                    document.getElementById('diskon2').value = data.header.disk2;
                    document.getElementById('hdiskon2').value = data.header.hdisk2;
                    document.getElementById('diskon3').value = data.header.disk3;
                    document.getElementById('hdiskon3').value = data.header.hdisk3;
                    document.getElementById('totaljmlh').value = data.header.totaljmlh;

                    dataPenjualan = data.detail;
                    
                    loadPerhitunganJual();
                    renderTabelPenjualan();
                    if (callback) callback();
                } else {
                    alert(data.message);
                }
            });
        }

        loadPenjualan();
        const kodeInput = document.getElementById('kode_kust');
        const namaInput = document.getElementById('nama_kust');
        const alamatInput = document.getElementById('alamat');
        const kodehrgInput = document.getElementById('kodehrg');
        const ketInput = document.getElementById('keterangan');

        const kodeSlsInput = document.getElementById('kode_sls');
        const namaSlsInput = document.getElementById('nama_sls');

        const popupKodeInput = document.getElementById('popup_kodebrg');
        const popupNamaInput = document.getElementById('popup_namabrg');
        
        const popupKodeGd = document.getElementById('popup_kodegd');
        const popupSatuan1 = document.getElementById('popup_satuan1');
        const popupSatuan2 = document.getElementById('popup_satuan2');
        const popupSatuan3 = document.getElementById('popup_satuan3');
        const popupIsi1 = document.getElementById('popup_isi1');
        const popupIsi2 = document.getElementById('popup_isi2');
        const popupJlh1 = document.getElementById('popup_jlh1');
        const popupJlh2 = document.getElementById('popup_jlh2');
        const popupJlh3 = document.getElementById('popup_jlh3');
        const popupHarga = document.getElementById('popup_harga');
        const popupDisca = document.getElementById('popup_disca');
        const popupDiscb = document.getElementById('popup_discb');
        const popupDiscc = document.getElementById('popup_discc');
        const popupDiscrp = document.getElementById('popup_discrp');
        const popupJumlah = document.getElementById('popup_jumlah');
        const popupHdiskon1 = document.getElementById('popup_hdiskon1');
        const popupHdiskon2 = document.getElementById('popup_hdiskon2');
        const popupHdiskon3= document.getElementById('popup_hdiskon3');

        const hakUbah  = <?php echo $hakUbah; ?>;
        const hakHapus = <?php echo $hakHapus; ?>;

        function cekAkses(aksi) {
            if (aksi === 'ubah' && hakUbah === 0) {
                showToast("Anda tidak bisa mengakses Edit!", "#dc3545");
                return false;
            }
            if (aksi === 'hapus' && hakHapus === 0) {
                showToast("Anda tidak bisa mengakses Hapus!", "#dc3545");
                return false;
            }
            return true;
        }

        let triggerBarang = null;   // untuk barang
        let triggerKustomer = null; // untuk supplier
        let triggerSales = null; // untuk supplier

        // Trigger cari saat tekan Enter
        [popupKodeInput, popupNamaInput].forEach(input => {
            const tipe = input.id === 'popup_kodebrg' ? 'kode' : 'nama';

            input.addEventListener('keypress', function (e) {
                if (e.key === 'Enter') {
                    e.preventDefault();
                    triggerBarang = "keypress"; // tandai dari keypress
                    const val = this.value.trim();
                    if (val) cariBarang(tipe, val);
                }
            });

            input.addEventListener('blur', function () {
                // hanya jalan jika sebelumnya tidak dari keypress
                if (triggerBarang === "keypress") return;
                const val = this.value.trim();
                if (val) {
                    triggerBarang = "blur"; // tandai dari blur
                    cariBarang(tipe, val);
                }
            });
        });

        [kodeInput, namaInput].forEach(input => {
            const tipe = input.id === 'kode_kust' ? 'kode' : 'nama';

            input.addEventListener('keypress', function(e) {
                if (e.key === 'Enter') {
                    e.preventDefault();
                    triggerKustomer = "keypress";
                    const val = this.value.trim();
                    if (val) cariKustomer(tipe, val);
                }
            });

            input.addEventListener('blur', function () {
                // hanya jalan jika sebelumnya tidak dari keypress
                if (triggerKustomer=== "keypress") return;
                const val = this.value.trim();
                if (val) {
                    triggerKustomer = "blur"; // tandai dari blur
                    cariKustomer(tipe, val);
                }
            });
        });

        [kodeSlsInput, namaSlsInput].forEach(input => {
            const tipe = input.id === 'kode_sls' ? 'kode' : 'nama';

            input.addEventListener('keypress', function(e) {
                if (e.key === 'Enter') {
                    e.preventDefault();
                    triggerSales = "keypress";
                    const val = this.value.trim();
                    if (val) cariSales(tipe, val);
                }
            });

            input.addEventListener('blur', function () {
                // hanya jalan jika sebelumnya tidak dari keypress
                if (triggerSales=== "keypress") return;
                const val = this.value.trim();
                if (val) {
                    triggerSales = "blur"; // tandai dari blur
                    cariSales(tipe, val);
                }
            })
        });

        function cariBarang(mode, keyword) {
            if (!keyword) return;

            fetch('filter_itembarang.php', {
                method: 'POST',
                headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
                body: `mode=${mode}&keyword=${encodeURIComponent(keyword)}`
            })
            .then(res => res.json())
            .then(data => {
                const tbody = document.getElementById('tbodyHasilBarang');
                tbody.innerHTML = '';

                if (data.length === 0) {
                    tbody.innerHTML = '<tr><td colspan="6" style="text-align:center;">Data tidak ditemukan!</td></tr>';
                } else {
                    data.forEach(item => {
                        const tr = document.createElement('tr');
                        tr.innerHTML = `
                            <td>${item.kodebrg}</td>
                            <td>${item.namabrg}</td>
                            <td>${item.satuan1}</td>
                            <td>${item.satuan2}</td>
                            <td>${item.satuan3}</td>
                            <td>${item.hrgbeli}</td>
                            <td><button type="button" onclick='pilihBarang(${JSON.stringify(item)})'>Pilih</button></td>
                        `;
                        tbody.appendChild(tr);
                    });
                }

                document.getElementById('popupCariBarang').style.display = 'flex';
            })
            .catch(() => {
                showToast('Terjadi kesalahan saat mencari barang', '#dc3545');
            })
            .finally(() => {
                setTimeout(() => triggerBarang = null, 200);
            });
        }

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
                showToast('Terjadi kesalahan saat mencari Kustomer', '#dc3545');
            })
            .finally(() => {
                setTimeout(() => triggerKustomer = null, 200);
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
                showToast('Terjadi kesalahan saat mencari sales', '#dc3545');
            })
            .finally(() => {
                setTimeout(() => triggerSales = null, 200);
            });
        }

        function cekValidasi(input) {
            const value = input.value.trim();
            const table = input.dataset.table;
            const field = input.dataset.field;
            const checkType = input.dataset.check; // 'duplikat' atau 'eksistensi'
            const resetTargets = input.dataset.reset ? input.dataset.reset.split(',') : [];

            // 🔹 Jika kosong atau *, langsung keluar supaya tidak validasi
            if (!value || value === '*') {
                input.dataset.prev = value; // simpan supaya tidak terus-terusan validasi saat blur
                return;
            }

            if (!table || !field || !checkType) return;

            const prevValue = input.dataset.prev || '';
            if (value.toLowerCase() === prevValue.toLowerCase()) return;

            input.dataset.prev = value; // simpan nilai terakhir
            
            fetch(`cekduplikat.php?table=${table}&field=${field}&value=${encodeURIComponent(value)}`)
                .then(res => res.json())
                .then(data => {
                    const hasil = data.data || [];

                    // ======== CEK DUPLIKAT ========
                    if (checkType === 'duplikat' && hasil.length > 0) { 
                        showToast(`Data sudah ada!`, '#dc3545');
                        input.value = '';
                        input.focus();
                        input.dataset.prev = '';
                        resetTargets.forEach(id => {
                            const el = document.getElementById(id.trim());
                            if (el) el.value = '';
                        });
                    }

                    // ======== CEK EKSISTENSI ========
                    if (checkType === 'eksistensi' && hasil.length === 0) {
                        showToast(`Data tidak ditemukan!`, '#dc3545');
                        input.value = '';
                        input.focus();
                        input.dataset.prev = '';
                        resetTargets.forEach(id => {
                            const el = document.getElementById(id.trim());
                            if (el) el.value = '';
                        });
                    }
                })
                .catch(err => {
                    console.error('Gagal cek validasi:', err);
                });
        }

        function pilihBarang(item) {
            popupKodeInput.value = item.kodebrg;
            popupNamaInput.value = item.namabrg;
            popupSatuan1.value = item.satuan1;
            popupSatuan2.value = item.satuan2;
            popupSatuan3.value = item.satuan3;
            popupHarga.value = item.hrgbeli;
            popupIsi1.value = item.isi1;
            popupIsi2.value = item.isi2;
            if (popupIsi1.value > 1) {
                popupJlh2.disabled = false;
                if (popupIsi2.value > 1) {
                    popupJlh3.disabled = false;
                } else {
                    popupJlh3.disabled = true;
                }
            } else {
                popupJlh2.disabled = true;
                popupJlh3.disabled = true;
            }
            getSisa();
            tutupPopupBarang();
        }

        function pilihKustomer(item) {
            kodeInput.value = item.kodekust;
            namaInput.value = item.namakust;
            alamatInput.value = item.alamat;
            kodehrgInput.value = item.kodehrg;
            tutupPopupKustomer();
        }

        function pilihSales(item) {
            kodeSlsInput.value = item.kodesls;
            namaSlsInput.value = item.namasls;
            tutupPopupSales();
        }

        function tutupPopupBarang() {
            document.getElementById('popupCariBarang').style.display = 'none';
        }

        function tutupPopupKustomer() {
            document.getElementById('popupCariKustomer').style.display = 'none';
        }

        function tutupPopupSales() {
            document.getElementById('popupCariSales').style.display = 'none';
        }

        function bukaPopupHarga(targetInputId) {
            let kodehrg = document.getElementById("kodehrg").value.trim();
            let kodebrg = document.getElementById(targetInputId.includes("edit") ? "edit_popup_kodebrg" : "popup_kodebrg").value.trim();

            if (!kodehrg) {
                showToast("Isi Kode Harga terlebih dahulu.", '#dc3545');
                return;
            }
            if (!kodebrg) {
                showToast("Isi Kode Barang terlebih dahulu.", '#dc3545');
                return;
            }

            // Ambil data harga dari PHP
            fetch(`get_harga_barang.php?kodebrg=${encodeURIComponent(kodebrg)}&kodehrg=${encodeURIComponent(kodehrg)}`)
                .then(res => res.json())
                .then(data => {
                    let tbody = document.getElementById("tbodyPilihanHarga");
                    tbody.innerHTML = "";
                    data.forEach(item => {
                        let tr = document.createElement("tr");
                        tr.innerHTML = `
                            <td>${item.label}</td>
                            <td>${parseFloat(item.value).toLocaleString('id-ID')}</td>
                            <td><button type="button" onclick="pilihHarga('${item.value}', '${targetInputId}')">Pilih</button></td>
                        `;
                        tbody.appendChild(tr);
                    });
                    document.getElementById("popupPilihHarga").style.display = "flex";
                });
        }

        function tutupPopupHarga() {
            document.getElementById("popupPilihHarga").style.display = "none";
        }

        function pilihHarga(val, targetInputId) {
            document.getElementById(targetInputId).value = val;
            tutupPopupHarga();
        }

        // Event listener saat tekan Enter di popup_harga atau edit_popup_harga
        ["popup_harga"].forEach(id => {
            let el = document.getElementById(id);
            if (el) {
                el.addEventListener("keydown", function(e) {
                    if (e.key === "Enter") {
                        e.preventDefault();
                        bukaPopupHarga(id);
                    }
                });
            }
        });

        function renderTabelPenjualan() {
            const tbody = document.querySelector('#tabelPenjualan tbody');
            tbody.innerHTML = '';

            dataPenjualan.forEach((item, index) => {
                const tr = document.createElement('tr');
                tr.innerHTML = `
                    <td style="display: none;">${item.isi1}</td>
                    <td style="display: none;">${item.isi2}</td>
                    <td>${item.kodebrg}</td>
                    <td>${item.namabrg}</td>
                    <td>${item.kodegd}</td>
                    <td style="text-align: right;">${item.jlh1}</td>
                    <td>${item.satuan1}</td>
                    <td style="text-align: right;">${item.jlh2 || ''}</td>
                    <td>${item.satuan2 || ''}</td>
                    <td style="text-align: right;">${item.jlh3 || ''}</td>
                    <td>${item.satuan3 || ''}</td>
                    <td style="text-align: right;">${parseInt(item.harga).toLocaleString('id-ID', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</td>
                    <td style="text-align: right;">${parseInt(item.disca).toLocaleString('id-ID', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</td>
                    <td style="text-align: right;">${parseInt(item.discb).toLocaleString('id-ID', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</td>
                    <td style="text-align: right;">${parseInt(item.discc).toLocaleString('id-ID', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</td>
                    <td style="display: none; text-align: right;">${item.hdisca}</td>
                    <td style="display: none; text-align: right;">${item.hdiscb}</td>
                    <td style="display: none; text-align: right;">${item.hdiscc}</td>
                    <td style="text-align: right;">${parseInt(item.discrp).toLocaleString('id-ID', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</td>
                    <td style="text-align: right;">${parseInt(item.jumlah).toLocaleString('id-ID')}</td>
                    <td style="display: none;" id="td-btn-${index}">
                        <button type="button" onclick="editItem(${index})">Edit</button>
                        <button type="button" onclick="hapusItem(${index})">Hapus</button>
                    </td>
                `;
                tbody.appendChild(tr);
            });
        }

        document.getElementById('formDetailPenjualan').addEventListener('submit', function (e) {
            e.preventDefault();

            const item = {
                isi1: parseInt(popupIsi1.value) || 0,
                isi2: parseInt(popupIsi2.value) || 0,
                kodebrg: popupKodeInput.value.trim().toUpperCase(),
                namabrg: popupNamaInput.value.trim().toUpperCase(),
                kodegd: popupKodeGd.value.trim().toUpperCase(),
                jlh1: parseInt(popupJlh1.value) || 0,
                satuan1: popupSatuan1.value.trim(),
                jlh2: parseInt(popupJlh2.value) || null,
                satuan2: popupSatuan2.value.trim() || null,
                jlh3: parseInt(popupJlh3.value) || null,
                satuan3: popupSatuan3.value.trim() || null,
                harga: parseFloat(popupHarga.value) || 0,
                disca: parseFloat(popupDisca.value) || 0,
                discb: parseFloat(popupDiscb.value) || 0,
                discc: parseFloat(popupDiscc.value) || 0,
                discrp: parseFloat(popupDiscrp.value) || 0,
                hdisca: parseFloat(popupHdiskon1.value) || 0,
                hdiscb: parseFloat(popupHdiskon2.value) || 0,
                hdiscc: parseFloat(popupHdiskon3.value) || 0,
                jumlah: parseFloat(popupJumlah.value) || 0
            };

            if (currentMode === "edit" && this.dataset.editingIndex !== undefined) {
                // UPDATE item
                const idx = parseInt(this.dataset.editingIndex, 10);
                dataPenjualan[idx] = item;
                tutupPopup();
                showToast('Item berhasil diupdate!');
            } else {
                // TAMBAH item baru
                dataPenjualan.push(item);
                showToast('Item berhasil disimpan!');
            }

            hitungSubtotalDariArrayJual();
            renderTabelPenjualan();
            resetitem();
            currentMode = "input";
            delete this.dataset.editingIndex;
        });

        function resetitem() {
            document.getElementById('formDetailPenjualan').reset();
            document.getElementById('thAksi').style.display = '';
            const allTdAksi = document.querySelectorAll('[id^="td-btn-"]');
            allTdAksi.forEach(td => {
                td.style.display = '';
            });
            popupJlh2.disabled = true;
            popupJlh3.disabled = true;
        }

        function editItem(index) {
            currentMode = "edit";
            const item = dataPenjualan[index];
            popupIsi1.value       = item.isi1;
            popupIsi2.value       = item.isi2;
            popupKodeInput.value  = item.kodebrg;
            popupNamaInput.value  = item.namabrg;
            popupKodeGd.value     = item.kodegd;
            popupJlh1.value       = item.jlh1;
            popupSatuan1.value    = item.satuan1;
            popupJlh2.value       = item.jlh2 || '';
            popupSatuan2.value    = item.satuan2 || '';
            popupJlh3.value       = item.jlh3 || '';
            popupSatuan3.value    = item.satuan3 || '';
            popupHarga.value      = item.harga;
            popupDisca.value      = item.disca;
            popupDiscb.value      = item.discb;
            popupDiscc.value      = item.discc;
            popupDiscrp.value     = item.discrp;
            popupHdiskon1.value   = item.hdisca;
            popupHdiskon2.value   = item.hdiscb;
            popupHdiskon3.value   = item.hdiscc;
            popupJumlah.value     = item.jumlah;

            if (popupIsi1.value > 1) {
                popupJlh2.disabled = false;
                if (popupIsi2.value > 1) {
                    popupJlh3.disabled = false;
                } else {
                    popupJlh3.disabled = true;
                }
            } else {
                popupJlh2.disabled = true;
                popupJlh3.disabled = true;
            }

            // Simpan index
            const formEdit = document.getElementById('formDetailPenjualan');
            formEdit.dataset.editingIndex = index;

            getSisa();
            // Tampilkan popup edit
            document.getElementById('popupForm').style.display = 'flex';
        }

        function hapusItem(index) {
            showPopupKonfirmasiHapus('item', index);
        }

        document.getElementById('btnHapus').addEventListener('click', () => {
            if (!cekAkses('hapus')) return;
            showPopupKonfirmasiHapus('nota');
        });

        function showPopupKonfirmasiHapus(tipe, index = null) {
            hapusTipe = tipe;
            indexToDelete = index;
            document.getElementById('popupConfirmHapus').style.display = 'block';
        }

        // Fungsi ketika user klik "Ya" atau "Tidak"
        function konfirmasiHapus(ya) {
            document.getElementById('popupConfirmHapus').style.display = 'none';
            
            if (ya) {
                if (hapusTipe === 'item' && indexToDelete !== null) {
                    // Hapus satu item dari array
                    dataPenjualan.splice(indexToDelete, 1);
                    renderTabelPenjualan();
                    hitungSubtotalDariArrayJual();
                    document.getElementById('thAksi').style.display = '';
                    const allTdAksi = document.querySelectorAll('[id^="td-btn-"]');
                    allTdAksi.forEach(td => td.style.display = '');
                    showToast('Item berhasil dihapus!', '#dc3545');
                }

                if (hapusTipe === 'nota') {
                    const noNota = document.getElementById('no_nota').value;
                    fetch(`proseshapuspnj.php?nonota=${encodeURIComponent(noNota)}`, {
                        method: 'GET'
                    })
                    .then(res => res.json())
                    .then(response => {
                        if (response.success) {
                            showToast('Penjualan berhasil dihapus!', '#dc3545');
                            // redirect atau reset halaman
                            setTimeout(() => window.location.href = 'penjualan.php', 1000);
                        } else {
                            showToast('Gagal menghapus penjualan!', '#dc3545');
                        }
                    })
                    .catch(() => showToast('Terjadi kesalahan server!', '#dc3545'));
                }
            }

            indexToDelete = null;
            hapusTipe = 'item';
        }

        const inputFields = [
            'popup_jlh1', 'popup_jlh2', 'popup_jlh3',
            'popup_harga', 'popup_disca', 'popup_discb',
            'popup_discc', 'popup_discrp', 'popup_satuan2', 'popup_satuan3'
        ];

        inputFields.forEach(id => {
            document.getElementById(id).addEventListener('input', () => {
                hitungJumlahPenjualan(currentMode);
            });
        });

        const hitungFields = [
            'subtotal','diskon1','hdiskon1','diskon2','hdiskon2',
            'diskon3','hdiskon3','lain_lain','ppn','hppn','totaljmlh'
        ];

        hitungFields.forEach(id => attachNumberFormatter(id, () => {
            hitungSubtotalDariArrayJual();
        }));

        function formatNumberID(value) {
            let number = parseFloat(value) || 0;
            return number.toLocaleString('id-ID', {
                minimumFractionDigits: 2,
                maximumFractionDigits: 2
            });
        }

        function attachNumberFormatter(id, onChange) {
            const el = document.getElementById(id);
            if (!el) return;

            el.addEventListener('input', () => {
                // Hapus semua karakter non-digit
                let raw = el.value.replace(/[^0-9]/g, '');
                // Ubah ke number
                let num = parseFloat(raw) / 100; // bagi 100 biar langsung ada 2 desimal
                el.value = formatNumberID(num);
                if (onChange) onChange();
            });

            // Saat fokus, kembalikan ke angka mentah supaya gampang edit
            el.addEventListener('focus', () => {
                el.value = el.value.replace(/\./g, '').replace(',', '.');
            });

            // Saat blur, format lagi
            el.addEventListener('blur', () => {
                let num = parseFloat(el.value.replace(/\./g, '').replace(',', '.')) || 0;
                el.value = formatNumberID(num);
            });
        }

        popupKodeInput.addEventListener('change', getSisa);
        popupKodeGd.addEventListener('change', getSisa);

        function initializeFormButtons() {
            currentstat = null;

            document.getElementById('btnEdit').disabled = false;
            document.getElementById('btnTambahItem').disabled = true;
            document.getElementById('btnCancel').disabled = true;
            document.getElementById('btnHapus').disabled = false;
            document.getElementById('btnSave').disabled = true;
            document.getElementById('btnPrint').disabled = false;

            document.getElementById('thAksi').style.display = 'none';
            const allTdAksi = document.querySelectorAll('[id^="td-btn-"]');
            allTdAksi.forEach(td => {
                td.style.display = 'none';
            });

            document.getElementById('tanggal').disabled = true;
            document.getElementById('no_nota').disabled = true;
            document.getElementById('jt_tempo').disabled = true;
            document.getElementById('nama_kust').disabled = true;
            document.getElementById('kode_kust').disabled = true;
            document.getElementById('nama_sls').disabled = true;
            document.getElementById('kode_sls').disabled = true;
            document.getElementById('alamat').disabled = true;
            document.getElementById('kodehrg').disabled = true;
            document.getElementById('keterangan').disabled = true;
            document.getElementById('subtotal').disabled = true;
            document.getElementById('lain_lain').disabled = true;
            document.getElementById('diskon1').disabled = true;
            document.getElementById('hdiskon1').disabled = true;
            document.getElementById('diskon2').disabled = true;
            document.getElementById('hdiskon2').disabled = true;
            document.getElementById('diskon3').disabled = true;
            document.getElementById('hdiskon3').disabled = true;
            document.getElementById('ppn').disabled = true;
            document.getElementById('hppn').disabled = true;
            document.getElementById('totaljmlh').disabled = true;
        }
        //initializeFormButtons();

        function initializeEdit() {
            if (!cekAkses('ubah')) return;
            currentstat = 'update';
            showToast('Kamu sedang menambah data...', '#ffc107');
            const saved = JSON.parse(localStorage.getItem('formPenjualanEdit') || '{}');
            saved.currentstat = 'update';
            localStorage.setItem('formPenjualanEdit', JSON.stringify(saved));
            laststat();

            // ✅ Tambahkan ini untuk menampilkan kolom Aksi
            document.getElementById('thAksi').style.display = '';
            const allTdAksi = document.querySelectorAll('[id^="td-btn-"]');
            allTdAksi.forEach(td => {
                td.style.display = '';
            });

            document.getElementById('btnEdit').disabled = true;
            document.getElementById('btnCancel').disabled = false;
            document.getElementById('btnHapus').disabled = true;
            document.getElementById('btnSave').disabled = false;
            document.getElementById('btnTambahItem').disabled = false;
            document.getElementById('btnPrint').disabled = true;

            document.getElementById('tanggal').disabled = false;
            document.getElementById('no_nota').disabled = false;
            document.getElementById('jt_tempo').disabled = false;
            document.getElementById('nama_kust').disabled = false;
            document.getElementById('kode_kust').disabled = false;
            document.getElementById('nama_sls').disabled = false;
            document.getElementById('kode_sls').disabled = false;
            document.getElementById('alamat').disabled = false;
            document.getElementById('kodehrg').disabled = false;
            document.getElementById('keterangan').disabled = false;
            document.getElementById('subtotal').disabled = false;
            document.getElementById('lain_lain').disabled = false;
            document.getElementById('diskon1').disabled = false;
            document.getElementById('hdiskon1').disabled = false;
            document.getElementById('diskon2').disabled = false;
            document.getElementById('hdiskon2').disabled = false;
            document.getElementById('diskon3').disabled = false;
            document.getElementById('hdiskon3').disabled = false;
            document.getElementById('ppn').disabled = false;
            document.getElementById('hppn').disabled = false;
            document.getElementById('totaljmlh').disabled = false;
        }

        function laststat() {
            document.getElementById('btnEdit').disabled = true;
            document.getElementById('btnCancel').disabled = false;
            document.getElementById('btnHapus').disabled = true;
            document.getElementById('btnSave').disabled = false;
            document.getElementById('btnTambahItem').disabled = false;

            document.getElementById('tanggal').disabled = false;
            document.getElementById('no_nota').disabled = false;
            document.getElementById('jt_tempo').disabled = false;
            document.getElementById('nama_kust').disabled = false;
            document.getElementById('kode_kust').disabled = false;
            document.getElementById('nama_sls').disabled = false;
            document.getElementById('kode_sls').disabled = false;
            document.getElementById('alamat').disabled = false;
            document.getElementById('kodehrg').disabled = false;
            document.getElementById('keterangan').disabled = false;
            document.getElementById('subtotal').disabled = false;
            document.getElementById('lain_lain').disabled = false;
            document.getElementById('ppn').disabled = false;
            document.getElementById('totaljmlh').disabled = false;
            document.getElementById('thAksi').style.display = '';
            const allTdAksi = document.querySelectorAll('[id^="td-btn-"]');
            allTdAksi.forEach(td => {
                td.style.display = '';
            });
        }

        function cancelForm() {
            initializeFormButtons();
                // Reset form
            // Set kembali tanggal dan jatuh tempo ke hari ini
            const today = new Date().toISOString().split('T')[0];
            //document.getElementById("tanggal").value = today;
            //document.getElementById("jt_tempo").value = today;

            // ✅ Tambahkan ini untuk menampilkan kolom Aksi
            document.getElementById('thAksi').style.display = 'none';
            const allTdAksi = document.querySelectorAll('[id^="td-btn-"]');
            allTdAksi.forEach(td => {
                td.style.display = 'none';
            });
            localStorage.removeItem('formPenjualanEdit');
        }

        document.getElementById('btnTambahItem').addEventListener('click', () => {
            document.getElementById('popupForm').style.display = 'flex';
        });

        // Tutup popup
        function tutupPopup() {
            document.getElementById('formDetailPenjualan').reset();
            delete formDetailPenjualan.dataset.editingIndex;
            document.getElementById('popupForm').style.display = 'none';
            popupJlh2.disabled = true;
            popupJlh3.disabled = true;
        }

        async function isiDropdownGudang() {
            try {
                const response = await fetch('getgudang.php');
                const data = await response.json();

                const dropdownTambah = document.getElementById('popup_kodegd');

                // Kosongkan dulu
                dropdownTambah.innerHTML = '<option value="">Pilih Gudang</option>';

                // Tambahkan opsi dari database
                data.forEach(gd => {
                    const teksTampil = `${gd.kodegd} - ${gd.namagd}`;
                    const opt = new Option(teksTampil, gd.kodegd);
                    dropdownTambah.add(opt);
                });
            } catch (err) {
                console.error('Gagal mengambil data gudang:', err);
            }
        }
        // Panggil setelah dropdown selesai diisi
        document.addEventListener('DOMContentLoaded', () => {
            isiDropdownGudang();
        });

        document.addEventListener('DOMContentLoaded', async () => {
            // 1. Isi dropdown dulu baru load data
            await isiDropdownGudang();

            const saved = JSON.parse(localStorage.getItem('formPenjualanEdit') || '{}');
            currentstat = saved.currentstat || null;

            // 2. Panggil loadPenjualan hanya jika ada nonota
            const noNota = new URLSearchParams(window.location.search).get('nonota');
            if (noNota) {
                await loadPenjualan(noNota, () => {
                    if (currentstat === 'update') laststat();
                }); // menunggu data penyesuaian selesai di-load
            }

            // 3. Pulihkan data form dari localStorage (hanya jika ada data)
            if (Object.keys(saved).length > 0) {
                Object.keys(saved).forEach(id => {
                    if (id !== 'dataPenjualan' && id !== 'currentstat' && id !== 'currentmode') {
                        const el = document.getElementById(id);
                        if (el) {
                            el.value = saved[id].value || el.value;
                            el.disabled = saved[id].disabled ?? false;
                        }
                    }
                });

                // Pulihkan data tabel
                if (saved.dataPenjualan) {
                    dataPenjualan = saved.dataPenjualan;
                    renderTabelPenjualan();
                }

                // Pulihkan status form
                if (saved.currentstat === 'update') {
                    laststat();
                } else {
                    initializeFormButtons();
                }
            }
        });

        // Enable btnTambahItem jika no_nota terisi
        document.getElementById('no_nota').addEventListener('input', function () {
            const noNota = this.value.trim();
            document.getElementById('btnTambahItem').disabled = (noNota === '');
        });

        function parseIDNumber(str) {
            if (!str) return 0;
            return parseFloat(str.replace(/\./g, '').replace(',', '.')) || 0;
        }

        document.getElementById('btnSave').addEventListener('click', () => {
            const data = {
                no_nota: document.getElementById('no_nota').value,
                no_nota_lama: document.getElementById('no_nota_lama').value,
                tanggal: document.getElementById('tanggal').value,
                kode_kust: document.getElementById('kode_kust').value,
                kode_sls: document.getElementById('kode_sls').value,
                jt_tempo: document.getElementById('jt_tempo').value,
                keterangan: document.getElementById('keterangan').value,
                prsnppn: parseFloat(document.getElementById('ppn').value) || 0,
                hrgppn: parseIDNumber(document.getElementById('hppn').value) || 0,
                disk1: parseFloat(document.getElementById('diskon1').value) || 0,
                hdisk1: parseIDNumber(document.getElementById('hdiskon1').value) || 0,
                disk2: parseFloat(document.getElementById('diskon2').value) || 0,
                hdisk2: parseIDNumber(document.getElementById('hdiskon2').value) || 0,
                disk3: parseFloat(document.getElementById('diskon3').value) || 0,
                hdisk3: parseIDNumber(document.getElementById('hdiskon3').value) || 0,
                totaljmlh: parseIDNumber(document.getElementById('totaljmlh').value) || 0,
                detail: dataPenjualan, // array yang sudah kamu simpan saat tambah item

                operator: document.body.dataset.kodeuser || ""  
            };

            fetch('prosesupdatepnj.php', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(data)
            })
            .then(res => res.json())
            .then(res => {
                if (res.success) {
                    showToast('Data berhasil disimpan!');
                    localStorage.removeItem('formPenjualanEdit');
                    initializeFormButtons();
                } else {
                    showToast('Gagal menyimpan: ' + kode_kust, '#dc3545');
                }
            })
            .catch(err => {
                showToast('Error: ' + err, '#dc3545');
            });
        });

        document.getElementById('btnPrint').addEventListener('click', () => {
            const noNota = document.getElementById('no_nota').value;

            if (!noNota) {
                alert("Nomor nota tidak boleh kosong!");
                return;
            }

            // Buka halaman nota dalam tab baru
            const url = `notaprintpnj.php?nonota=${encodeURIComponent(noNota)}&from=editpenjualan.php`;
            window.location.href = url;
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
            setTimeout(() => toast.remove(), 1500);
        }
        window.addEventListener('beforeunload', () => {
            const form = document.getElementById('formPenjualan');
            const formData = {};

            form.querySelectorAll('input, select, option, textarea, button').forEach(el => {
                formData[el.id] = {
                    value: el.value,
                    disabled: el.disabled
                };
            });

            // Simpan currentstat
            formData['currentstat'] = currentstat;

            // Simpan dataPenjualan juga jika penting
            formData['dataPenjualan'] = dataPenjualan;

            // Simpan ke localStorage
            localStorage.setItem('formPenjualanEdit', JSON.stringify(formData));
        });
    </script>
</body>
</html>
