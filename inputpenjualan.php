<?php
session_start();
include 'koneksi.php';
$nonota = $_GET['nonota'] ?? '';
/*$suppliers = [];
$supplierQuery = $conn->query("SELECT kodekust, namasls, alamat FROM zsales ORDER BY kodekust");
while ($row = $supplierQuery->fetch_assoc()) {
    $suppliers[] = $row;
}*/
?>
<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Input Penjualan</title>
    <link rel="stylesheet" href="navbar.css">
    <link rel="stylesheet" href="form.css">
</head>
<body>
    <button class="hamburger" onclick="toggleSidebar()">☰</button>
    <div class="overlay" id="sidebarOverlay" onclick="toggleSidebar()"></div>
    <?php include 'navbar.php'; ?>

    <main>
        <h2>Input Penjualan</h2>
        <div class="action-pb-bar" style="display: flex; justify-content: space-between; align-items: center; margin-top: 10px;">
            <div style="display: flex; gap: 8px;">
                <button id="btnSave" type="submit">Simpan</button>
                <button id="btnTambah" type="button" onclick="initializeTambah()">Tambah</button>    
                <button id="btnCancel" type="button" onclick="cancelForm()">Batal</button>
                <button id="btnPrint" type="button">Print</button>
  
            </div>
            <button id="btnKembali" type="button" onclick="window.location.href='penjualan.php'">List Nota</button>
        </div>

        <form id="formPenjualan" action="prosespenjualan.php" method="POST">
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

            <div id="form-penjualan-bawah">
                <div class="form-pj-pos">
                    <div class="form-pj-col">
                        <label for="subtotal">Subtotal</label>
                        <input type="number" id="subtotal" name="subtotal" class="short-input">
                    </div>
                    <div class="form-pj-col">
                        <label for="lain_lain">Lain-Lain</label>
                        <input type="number" id="lain_lain" name="lain_lain" class="short-input">
                    </div>
                    <div class="form-pj-col">
                        <label for="ppn">PPN</label>
                        <input type="number" id="ppn" name="ppn" class="short-input">
                    </div>

                    <div class="form-pj-col">
                        <label for="totaljmlh">Total Jumlah</label>
                        <input type="number" id="totaljmlh" name="totaljmlh" class="short-input">
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
                    <div class="popup-pb-row">
                        <label for="popup_kodegd">Kode Gudang</label>
                        <select id="popup_kodegd" name="popup_gd" required>
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
                        <label for="popup_jlh1">Jumlah 1</label>
                        <input type="number" id="popup_jlh1" name="popup_jlh1" min="0">
                    </div>
                    <div class="popup-pb-row">
                        <label for="popup_satuan1">Satuan 1</label>
                        <input type="text" id="popup_satuan1" name="popup_satuan1" disabled>
                    </div>

                    <div class="popup-pb-row">
                        <label for="popup_jlh2">Jumlah 2</label>
                        <input type="number" id="popup_jlh2" name="popup_jlh2" min="0" disabled>
                    </div>
                    <div class="popup-pb-row">
                        <label for="popup_satuan2">Satuan 2</label>
                        <input type="text" id="popup_satuan2" name="popup_satuan2" disabled>
                    </div>

                    <div class="popup-pb-row">
                        <label for="popup_jlh3">Jumlah 3</label>
                        <input type="number" id="popup_jlh3" name="popup_jlh3" min="0" disabled>
                    </div>
                    <div class="popup-pb-row">
                        <label for="popup_satuan3">Satuan 3</label>
                        <input type="text" id="popup_satuan3" name="popup_satuan3" disabled>
                    </div>

                    <div class="popup-pb-row">
                        <label for="popup_harga">Harga</label>
                        <input type="number" id="popup_harga" name="popup_harga" value="0">
                    </div>
                    <div class="popup-pb-row">
                        <label for="popup_disca">Disca</label>
                        <input type="number" id="popup_disca" name="popup_disca" value="0">
                    </div>
                    <div class="popup-pb-row">
                        <label for="popup_discb">Discb</label>
                        <input type="number" id="popup_discb" name="popup_discb" value="0">
                    </div>
                    <div class="popup-pb-row">
                        <label for="popup_discc">Discc</label>
                        <input type="number" id="popup_discc" name="popup_discc" value="0">
                    </div>
                    <div class="popup-pb-row">
                        <label for="popup_discrp">Disc Rp</label>
                        <input type="number" id="popup_discrp" name="popup_discrp" value="0">
                    </div>
                    <div class="popup-pb-row">
                        <label for="popup_jumlah">Jumlah</label>
                        <input type="number" id="popup_jumlah" name="popup_jumlah"required>
                    </div>

                    <div class="popup-pb-row" style="justify-content: flex-end; gap: 10px;">
                        <button id="btnSaveItem" type="submit">Oke</button>
                        <button id="btnCancelItem" type="button" onclick="tutupPopup()">Tutup</button>
                    </div>
                </form>
            </div>
        </div>
        <div id="popupFormEdit" class="popup-pb-overlay" style="display: none;">
            <div class="popup-pb-content">
                <h3>Edit Data Penjualan</h3>
                <form id="formDetailPenjualanEdit">
                    <input type="hidden" name="edit_popup_isi1" id="edit_popup_isi1" value=""> 
                    <input type="hidden" name="edit_popup_isi2" id="edit_popup_isi2" value="">
                    <input type="hidden" name="edit_popup_hdiskon1" id="edit_popup_hdiskon1" value=""> 
                    <input type="hidden" name="edit_popup_hdiskon2" id="edit_popup_hdiskon2" value="">
                    <input type="hidden" name="edit_popup_hdiskon3" id="edit_popup_hdiskon3" value=""> 
                    <div class="popup-pb-row">
                        <label for="edit_popup_kodegd">Kode Gudang</label>
                        <select id="edit_popup_kodegd" name="edit_popup_kodegd" required>
                            <option value="">Pilih Gudang</option>
                            <!-- Akan diisi via JavaScript -->
                        </select>
                    </div>
                    <div class="popup-pb-row">
                        <label for="popup_kodebrg">Kode Barang</label>
                        <input type="text" id="edit_popup_kodebrg" data-table="zstok" data-field="kodebrg" data-check="eksistensi" data-reset="edit_popup_namabrg" onblur="cekValidasi(this)" name="edit_popup_kodebrg" style="text-transform: uppercase;">
                    </div>
                    <div class="popup-pb-row">
                        <label for="popup_namabrg">Nama Barang</label>
                        <input type="text" id="edit_popup_namabrg" data-table="zstok" data-field="namabrg" data-check="eksistensi" data-reset="edit_popup_kodebrg" onblur="cekValidasi(this)" name="edit_popup_namabrg" style="text-transform: uppercase;">
                    </div>

                    <div class="popup-pb-row">
                        <label for="popup_jlh1">Jumlah 1</label>
                        <input type="number" id="edit_popup_jlh1" name="edit_popup_jlh1" min="0">
                    </div>
                    <div class="popup-pb-row">
                        <label for="popup_satuan1">Satuan 1</label>
                        <input type="text" id="edit_popup_satuan1" name="edit_popup_satuan1" disabled>
                    </div>

                    <div class="popup-pb-row">
                        <label for="popup_jlh2">Jumlah 2</label>
                        <input type="number" id="edit_popup_jlh2" name="edit_popup_jlh2" min="0" disabled>
                    </div>
                    <div class="popup-pb-row">
                        <label for="popup_satuan2">Satuan 2</label>
                        <input type="text" id="edit_popup_satuan2" name="edit_popup_satuan2" disabled>
                    </div>

                    <div class="popup-pb-row">
                        <label for="popup_jlh3">Jumlah 3</label>
                        <input type="number" id="edit_popup_jlh3" name="edit_popup_jlh3" min="0" disabled>
                    </div>
                    <div class="popup-pb-row">
                        <label for="popup_satuan3">Satuan 3</label>
                        <input type="text" id="edit_popup_satuan3" name="edit_popup_satuan3" disabled>
                    </div>

                    <div class="popup-pb-row">
                        <label for="popup_harga">Harga</label>
                        <input type="number" id="edit_popup_harga" name="edit_popup_harga" value="0">
                    </div>
                    <div class="popup-pb-row">
                        <label for="popup_disca">Disca</label>
                        <input type="number" id="edit_popup_disca" name="edit_popup_disca" value="0">
                    </div>
                    <div class="popup-pb-row">
                        <label for="popup_discb">Discb</label>
                        <input type="number" id="edit_popup_discb" name="edit_popup_discb" value="0">
                    </div>
                    <div class="popup-pb-row">
                        <label for="popup_discc">Discc</label>
                        <input type="number" id="edit_popup_discc" name="edit_popup_discc" value="0">
                    </div>
                    <div class="popup-pb-row">
                        <label for="popup_discrp">Disc Rp</label>
                        <input type="number" id="edit_popup_discrp" name="edit_popup_discrp" value="0">
                    </div>
                    <div class="popup-pb-row">
                        <label for="popup_jumlah">Jumlah</label>
                        <input type="number" id="edit_popup_jumlah" name="edit_popup_jumlah"required>
                    </div>

                    <div class="popup-pb-row" style="justify-content: flex-end; gap: 10px;">
                        <button id="btnSaveItem" type="submit">Oke</button>
                        <button id="btnCancelItem" type="button" onclick="tutupPopupEdit()">Batal</button>
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
        let dataPenjualan = [];
        let indexToDelete = null;
        const kodeInput = document.getElementById('kode_kust');
        const namaInput = document.getElementById('nama_kust');
        const alamatInput = document.getElementById('alamat');
        const kodehrgInput = document.getElementById('kodehrg');

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

        const isi1Edit       = document.getElementById('edit_popup_isi1');
        const isi2Edit       = document.getElementById('edit_popup_isi2');
        const kodebrgEdit    = document.getElementById('edit_popup_kodebrg');
        const namabrgEdit    = document.getElementById('edit_popup_namabrg');
        const kodegdEdit    = document.getElementById('edit_popup_kodegd');
        const jlh1Edit       = document.getElementById('edit_popup_jlh1');
        const satuan1Edit    = document.getElementById('edit_popup_satuan1');
        const jlh2Edit       = document.getElementById('edit_popup_jlh2');
        const satuan2Edit    = document.getElementById('edit_popup_satuan2');
        const jlh3Edit       = document.getElementById('edit_popup_jlh3');
        const satuan3Edit    = document.getElementById('edit_popup_satuan3');
        const hargaEdit      = document.getElementById('edit_popup_harga');
        const discaEdit      = document.getElementById('edit_popup_disca');
        const discbEdit      = document.getElementById('edit_popup_discb');
        const disccEdit      = document.getElementById('edit_popup_discc');
        const discrpEdit     = document.getElementById('edit_popup_discrp');
        const jumlahEdit     = document.getElementById('edit_popup_jumlah');
        const hdiskon1Edit = document.getElementById('edit_popup_hdiskon1');
        const hdiskon2Edit = document.getElementById('edit_popup_hdiskon2');
        const hdiskon3Edit = document.getElementById('edit_popup_hdiskon3');
        

        // Trigger cari saat tekan Enter
        [popupKodeInput, popupNamaInput].forEach(input => {
            const tipe = input.id === 'popup_kodebrg' ? 'kode' : 'nama';

            input.addEventListener('keypress', function(e) {
                if (e.key === 'Enter') {
                    e.preventDefault();
                    const val = this.value.trim();
                    if (val) cariBarang(tipe, val);
                }
            });

            input.addEventListener('blur', function() {
                const val = this.value.trim();
                if (val) cariBarang(tipe, val);
            });
        });

        [kodeInput, namaInput].forEach(input => {
            const tipe = input.id === 'kode_kust' ? 'kode' : 'nama';

            input.addEventListener('keypress', function(e) {
                if (e.key === 'Enter') {
                    e.preventDefault();
                    const val = this.value.trim();
                    if (val) cariKustomer(tipe, val);
                }
            });

            input.addEventListener('blur', function() {
                const val = this.value.trim();
                if (val) cariKustomer(tipe, val);
            });
        });

        [kodeSlsInput, namaSlsInput].forEach(input => {
            const tipe = input.id === 'kode_sls' ? 'kode' : 'nama';

            input.addEventListener('keypress', function(e) {
                if (e.key === 'Enter') {
                    e.preventDefault();
                    const val = this.value.trim();
                    if (val) cariSales(tipe, val);
                }
            });

            input.addEventListener('blur', function() {
                const val = this.value.trim();
                if (val) cariSales(tipe, val);
            });
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

        function cekValidasi(input) {
            const value = input.value.trim();
            const table = input.dataset.table;
            const field = input.dataset.field;
            const checkType = input.dataset.check; // 'duplikat' atau 'eksistensi'
            const resetTargets = input.dataset.reset ? input.dataset.reset.split(',') : [];

            if (!value || !table || !field || !checkType) return;

            const prevValue = input.dataset.prev || '';
            if (value.toLowerCase() === prevValue.toLowerCase()) return;

            input.dataset.prev = value;

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
            document.getElementById('formDetailPenjualan').reset();
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
        ["popup_harga", "edit_popup_harga"].forEach(id => {
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
                    <td style="text-align: right;">${item.harga}</td>
                    <td style="text-align: right;">${item.disca}</td>
                    <td style="text-align: right;">${item.discb}</td>
                    <td style="text-align: right;">${item.discc}</td>
                    <td style="display: none; text-align: right;">${item.hdisca}</td>
                    <td style="display: none; text-align: right;">${item.hdiscb}</td>
                    <td style="display: none; text-align: right;">${item.hdiscc}</td>
                    <td style="text-align: right;">${item.discrp}</td>
                    <td style="text-align: right;">${item.jumlah}</td>
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
                isi1: parseInt(document.getElementById('popup_isi1').value),
                isi2: parseInt(document.getElementById('popup_isi2').value),
                kodebrg: document.getElementById('popup_kodebrg').value.trim().toUpperCase(),
                namabrg: document.getElementById('popup_namabrg').value.trim().toUpperCase(),
                kodegd: document.getElementById('popup_kodegd').value.trim().toUpperCase(),
                jlh1: parseInt(document.getElementById('popup_jlh1').value),
                satuan1: document.getElementById('popup_satuan1').value.trim(),
                jlh2: parseInt(document.getElementById('popup_jlh2').value) || null,
                satuan2: document.getElementById('popup_satuan2').value.trim() || null,
                jlh3: parseInt(document.getElementById('popup_jlh3').value) || null,
                satuan3: document.getElementById('popup_satuan3').value.trim() || null,
                harga: parseFloat(document.getElementById('popup_harga').value),
                disca: parseFloat(document.getElementById('popup_disca').value),
                discb: parseFloat(document.getElementById('popup_discb').value),
                discc: parseFloat(document.getElementById('popup_discc').value),
                discrp: parseFloat(document.getElementById('popup_discrp').value),
                jumlah: parseFloat(document.getElementById('popup_jumlah').value)
            };

            // Tambah atau update
            if (formDetailPenjualan.dataset.editingIndex) {
                dataPenjualan[formDetailPenjualan.dataset.editingIndex] = item;
                delete formDetailPenjualan.dataset.editingIndex;
            } else {
                dataPenjualan.push(item);
                hitungSubtotalDariArray();
            }

            renderTabelPenjualan();
            resetitem();
            showToast('Item berhasil disimpan!');
        });

        document.getElementById('formDetailPenjualanEdit').addEventListener('submit', function (e) {
            e.preventDefault();

            const item = {
                isi1: parseInt(document.getElementById('edit_popup_isi1').value),
                isi2: parseInt(document.getElementById('edit_popup_isi2').value),
                kodebrg: document.getElementById('edit_popup_kodebrg').value.trim().toUpperCase(),
                namabrg: document.getElementById('edit_popup_namabrg').value.trim().toUpperCase(),
                kodegd: document.getElementById('edit_popup_kodegd').value.trim().toUpperCase(),
                jlh1: parseInt(document.getElementById('edit_popup_jlh1').value),
                satuan1: document.getElementById('edit_popup_satuan1').value.trim(),
                jlh2: parseInt(document.getElementById('edit_popup_jlh2').value) || null,
                satuan2: document.getElementById('edit_popup_satuan2').value.trim() || null,
                jlh3: parseInt(document.getElementById('edit_popup_jlh3').value) || null,
                satuan3: document.getElementById('edit_popup_satuan3').value.trim() || null,
                harga: parseFloat(document.getElementById('edit_popup_harga').value),
                disca: parseFloat(document.getElementById('edit_popup_disca').value),
                discb: parseFloat(document.getElementById('edit_popup_discb').value),
                discc: parseFloat(document.getElementById('edit_popup_discc').value),
                discrp: parseFloat(document.getElementById('edit_popup_discrp').value),
                jumlah: parseFloat(document.getElementById('edit_popup_jumlah').value)
            };

            const editIndex = formDetailPenjualanEdit.dataset.editingIndex;
            if (editIndex !== undefined && editIndex !== null) {
                dataPenjualan[editIndex] = item;
                delete formDetailPenjualanEdit.dataset.editingIndex;
                renderTabelPenjualan();
                hitungSubtotalDariArray();
                showToast('Item berhasil diperbarui!');
            }

            // Tutup popup edit
            document.getElementById('popupFormEdit').style.display = 'none';
            document.getElementById('thAksi').style.display = '';
            const allTdAksi = document.querySelectorAll('[id^="td-btn-"]');
            allTdAksi.forEach(td => {
                td.style.display = '';
            });
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
            const item = dataPenjualan[index];
            isi1Edit.value     = item.isi1;
            isi2Edit.value     = item.isi2;
            kodebrgEdit.value  = item.kodebrg;
            namabrgEdit.value  = item.namabrg;
            kodegdEdit.value   = item.kodegd;
            jlh1Edit.value     = item.jlh1;
            satuan1Edit.value  = item.satuan1;
            jlh2Edit.value     = item.jlh2 || '';
            satuan2Edit.value  = item.satuan2 || '';
            jlh3Edit.value     = item.jlh3 || '';
            satuan3Edit.value  = item.satuan3 || '';
            hargaEdit.value    = item.harga;
            discaEdit.value    = item.disca;
            discbEdit.value    = item.discb;
            disccEdit.value    = item.discc;
            discrpEdit.value   = item.discrp;
            jumlahEdit.value   = item.jumlah;

            if (isi1Edit.value > 1) {
                jlh2Edit.disabled = false;
                if (isi2Edit.value > 1) {
                    jlh3Edit.disabled = false;
                } else {
                    jlh3Edit.disabled = true;
                }
            } else {
                jlh2Edit.disabled = true;
                jlh3Edit.disabled = true;
            }

            // Simpan index
            const formEdit = document.getElementById('formDetailPenjualanEdit');
            formEdit.dataset.editingIndex = index;

            // Tampilkan popup edit
            document.getElementById('popupFormEdit').style.display = 'flex';
        }

        function hapusItem(index) {
            indexToDelete = index;
            document.getElementById('popupConfirmHapus').style.display = 'block';
        }

        // Fungsi ketika user klik "Ya" atau "Tidak"
        function konfirmasiHapus(ya) {
            document.getElementById('popupConfirmHapus').style.display = 'none';
            if (ya && indexToDelete !== null) {
                dataPenjualan.splice(indexToDelete, 1);
                renderTabelPenjualan();
                hitungSubtotalDariArray();
                document.getElementById('thAksi').style.display = '';
                const allTdAksi = document.querySelectorAll('[id^="td-btn-"]');
                allTdAksi.forEach(td => {
                    td.style.display = '';
                });
                showToast('Item berhasil dihapus!', '#dc3545');
            }
            indexToDelete = null;
        }

        const inputFields = [
            'popup_jlh1', 'popup_jlh2', 'popup_jlh3',
            'popup_harga', 'popup_disca', 'popup_discb',
            'popup_discc', 'popup_discrp', 'popup_satuan2', 'popup_satuan3',
            'edit_popup_jlh1', 'edit_popup_jlh2', 'edit_popup_jlh3',
            'edit_popup_harga', 'edit_popup_disca', 'edit_popup_discb',
            'edit_popup_discc', 'edit_popup_discrp', 'edit_popup_satuan2', 'edit_popup_satuan3'
        ];

        inputFields.forEach(id => {
            document.getElementById(id).addEventListener('input', hitungJumlahPenjualan);
            document.getElementById(id).addEventListener('input', hitungJumlahPenjualanEdit);
        });

        function hitungJumlahPenjualan() {
            let jlh1 = parseFloat(popupJlh1.value) || 0;
            let jlh2 = parseFloat(popupJlh2.value) || 0;
            let jlh3 = parseFloat(popupJlh3.value) || 0;
            let harga = parseFloat(popupHarga.value) || 0;

            let isi1 = parseFloat(popupIsi1.value) || 0;
            let isi2 = parseFloat(popupIsi2.value) || 0;

            let disca = parseFloat(popupDisca.value) || 0;
            let discb = parseFloat(popupDiscb.value) || 0;
            let discc = parseFloat(popupDiscc.value) || 0;
            let discrp = parseFloat(popupDiscrp.value) || 0;

            let hasil1 = jlh1 * harga;
            let hasil2 = (jlh2 > 0 && isi1 > 0) ? (harga / isi1) * jlh2 : 0;
            let hasil3 = (jlh3 > 0 && isi1 > 0 && isi2 > 0) ? (harga / (isi1 * isi2)) * jlh3 : 0;

            let smntarajlmh = hasil1 + hasil2 + hasil3;

            let afterDisca = smntarajlmh * disca / 100;
            let smntaradis1 = smntarajlmh - afterDisca;

            let afterDiscb = smntaradis1 * discb / 100;
            let smntaradis2 = smntaradis1 - afterDiscb;

            let afterDiscc = smntaradis2 * discc / 100;
            let smntaradis3 = smntaradis2 - afterDiscc;

            let finalJumlah = smntaradis3 - discrp;

            popupJumlah.value = Math.round(finalJumlah);
        }

        function hitungJumlahPenjualanEdit() {
            let jlh1 = parseFloat(jlh1Edit.value) || 0;
            let jlh2 = parseFloat(jlh2Edit.value) || 0;
            let jlh3 = parseFloat(jlh3Edit.value) || 0;
            let harga = parseFloat(hargaEdit.value) || 0;

            let isi1 = parseFloat(isi1Edit.value) || 0;
            let isi2 = parseFloat(isi2Edit.value) || 0;

            let disca = parseFloat(discaEdit.value) || 0;
            let discb = parseFloat(discbEdit.value) || 0;
            let discc = parseFloat(disccEdit.value) || 0;
            let discrp = parseFloat(discrpEdit.value) || 0;

            let hasil1 = jlh1 * harga;
            let hasil2 = (jlh2 > 0 && isi1 > 0) ? (harga / isi1) * jlh2 : 0;
            let hasil3 = (jlh3 > 0 && isi1 > 0 && isi2 > 0) ? (harga / (isi1 * isi2)) * jlh3 : 0;

            let smntarajlmh = hasil1 + hasil2 + hasil3;

            let afterDisca = smntarajlmh * disca / 100;
            let smntaradis1 = smntarajlmh - afterDisca;

            let afterDiscb = smntaradis1 * discb / 100;
            let smntaradis2 = smntaradis1 - afterDiscb;

            let afterDiscc = smntaradis2 * discc / 100;
            let smntaradis3 = smntaradis2 - afterDiscc;

            let finalJumlah = smntaradis3 - discrp;

            jumlahEdit.value = Math.round(finalJumlah);
            if (indexEdit !== undefined && dataPenjualan[indexEdit]) {
                dataPenjualan[indexEdit].jumlah = Math.round(finalJumlah);
            }

            hitungSubtotalDariArray();
        }

        document.getElementById('lain_lain').addEventListener('input', function() {
            hitungSubtotalDariArray();
        });

        function hitungSubtotalDariArray() {
            let subtotal = 0;
            dataPenjualan.forEach(item => {
                subtotal += parseFloat(item.jumlah) || 0;
            });

            let ppn = subtotal * 11 / 100;
            let lainLain = parseFloat(document.getElementById('lain_lain').value) || 0;
            let totaljmlh = subtotal + ppn + lainLain;

            document.getElementById('subtotal').value = Math.round(subtotal);
            document.getElementById('ppn').value = Math.round(ppn);
            document.getElementById('totaljmlh').value = Math.round(totaljmlh);
        }

        function initializeFormButtons() {
            currentstat = null;

            document.getElementById('btnTambah').disabled = false;
            document.getElementById('btnTambahItem').disabled = true;
            document.getElementById('btnCancel').disabled = true;
            document.getElementById('btnSave').disabled = true;
            document.getElementById('btnPrint').disabled = false;

            document.getElementById('tanggal').disabled = true;
            document.getElementById('kodehrg').disabled = true;
            document.getElementById('no_nota').disabled = true;
            document.getElementById('jt_tempo').disabled = true;
            document.getElementById('nama_kust').disabled = true;
            document.getElementById('kode_kust').disabled = true;
            document.getElementById('nama_sls').disabled = true;
            document.getElementById('kode_sls').disabled = true;
            document.getElementById('subtotal').disabled = true;
            document.getElementById('lain_lain').disabled = true;
            document.getElementById('ppn').disabled = true;
            document.getElementById('totaljmlh').disabled = true;
            document.getElementById('thAksi').style.display = 'none';
            const allTdAksi = document.querySelectorAll('[id^="td-btn-"]');
            allTdAksi.forEach(td => {
                td.style.display = 'none';
            });
        }
        //initializeFormButtons();
        function initializeTambah() {
            currentstat = 'tambah';
            showToast('Kamu sedang menambah data...', '#ffc107');

            document.getElementById('formPenjualan').reset();
            
            const today = new Date().toISOString().split('T')[0];
            document.getElementById("tanggal").value = today;
            document.getElementById("jt_tempo").value = today;

            document.getElementById('btnTambah').disabled = true;
            document.getElementById('btnCancel').disabled = false;
            document.getElementById('btnSave').disabled = false;
            document.getElementById('btnPrint').disabled = true;

            document.getElementById('tanggal').disabled = false;
            document.getElementById('kodehrg').disabled = false;
            document.getElementById('no_nota').disabled = false;
            document.getElementById('jt_tempo').disabled = false;
            document.getElementById('nama_kust').disabled = false;
            document.getElementById('kode_kust').disabled = false;
            document.getElementById('nama_sls').disabled = false;
            document.getElementById('kode_sls').disabled = false;
            document.getElementById('subtotal').disabled = false;
            document.getElementById('lain_lain').disabled = false;
            document.getElementById('ppn').disabled = false;
            document.getElementById('totaljmlh').disabled = false;

            const nota = document.getElementById('no_nota').value.trim();
            document.getElementById('btnTambahItem').disabled = (nota === '');
            document.getElementById('thAksi').style.display = '';
            const allTdAksi = document.querySelectorAll('[id^="td-btn-"]');
            allTdAksi.forEach(td => {
                td.style.display = '';
            });

            dataPenjualan = [];
            renderTabelPenjualan();
        }

        function laststat() {
            const today = new Date().toISOString().split('T')[0];
            document.getElementById("tanggal").value = today;
            document.getElementById("jt_tempo").value = today;

            document.getElementById('btnTambah').disabled = true;
            document.getElementById('btnCancel').disabled = false;
            document.getElementById('btnSave').disabled = false;

            document.getElementById('tanggal').disabled = false;
            document.getElementById('kodehrg').disabled = false;
            document.getElementById('no_nota').disabled = false;
            document.getElementById('jt_tempo').disabled = false;
            document.getElementById('nama_kust').disabled = false;
            document.getElementById('kode_kust').disabled = false;
            document.getElementById('nama_sls').disabled = false;
            document.getElementById('kode_sls').disabled = false;
            document.getElementById('subtotal').disabled = false;
            document.getElementById('lain_lain').disabled = false;
            document.getElementById('ppn').disabled = false;
            document.getElementById('totaljmlh').disabled = false;

            const nota = document.getElementById('no_nota').value.trim();
            document.getElementById('btnTambahItem').disabled = (nota === '');
            document.getElementById('thAksi').style.display = '';
            const allTdAksi = document.querySelectorAll('[id^="td-btn-"]');
            allTdAksi.forEach(td => {
                td.style.display = '';
            });

        }

        function cancelForm() {
            initializeFormButtons();
                // Reset form
            document.getElementById('formPenjualan').reset();
            // Set kembali tanggal dan jatuh tempo ke hari ini
            const today = new Date().toISOString().split('T')[0];
            //document.getElementById("tanggal").value = today;
            //document.getElementById("jt_tempo").value = today;
            document.getElementById('thAksi').style.display = '';
            const allTdAksi = document.querySelectorAll('[id^="td-btn-"]');
            allTdAksi.forEach(td => {
                td.style.display = '';
            });
            dataPenjualan = [];
            renderTabelPenjualan();
            localStorage.removeItem('formPenjualanState');
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

        function tutupPopupEdit() {
            document.getElementById('formDetailPenjualanEdit').reset();
            delete formDetailPenjualan.dataset.editingIndex;
            document.getElementById('popupFormEdit').style.display = 'none';
            jlh2Edit.disabled = true;
            jlh3Edit.disabled = true;
        }

        function isiDropdownGudang() {
            fetch('getgudang.php')
                .then(response => response.json())
                .then(data => {
                    const dropdownTambah = document.getElementById('popup_kodegd');
                    const dropdownEdit = document.getElementById('edit_popup_kodegd');

                    data.forEach(gd => {
                        const teksTampil = `${gd.kodegd} - ${gd.namagd}`;
                        const optTambah = new Option(teksTampil, gd.kodegd);
                        const optEdit = new Option(teksTampil, gd.kodegd);

                        dropdownTambah.add(optTambah);
                        dropdownEdit.add(optEdit);
                    });
                })
                .catch(error => console.error('Gagal ambil data gudang:', error));
        }

        document.addEventListener('DOMContentLoaded', isiDropdownGudang);

        window.addEventListener('DOMContentLoaded', () => {
            const saved = JSON.parse(localStorage.getItem('formPenjualanState') || '{}');
            const form = document.getElementById('formPenjualan');

            currentstat = saved.currentstat || null;

            Object.keys(saved).forEach(id => {
                const el = document.getElementById(id);
                if (el) {
                    el.value = saved[id].value;
                    el.disabled = saved[id].disabled;
                }
            });

            if (saved.dataPenjualan) {
                dataPenjualan = saved.dataPenjualan;
                renderTabelPenjualan();
                hitungSubtotalDariArray();
            }

            // Pulihkan status tombol/form berdasarkan currentstat
            if (saved.currentstat === 'tambah') {
                laststat();
            } else {
                initializeFormButtons();
            }
        });

        // Enable btnTambahItem jika no_nota terisi
        document.getElementById('no_nota').addEventListener('input', function () {
            const noNota = this.value.trim();
            document.getElementById('btnTambahItem').disabled = (noNota === '');
        });

        document.getElementById('btnSave').addEventListener('click', () => {
            const data = {
                nonota: document.getElementById('no_nota').value,
                tanggal: document.getElementById('tanggal').value,
                kodekust: document.getElementById('kode_kust').value,
                kodesls: document.getElementById('kode_sls').value,
                jt_tempo: document.getElementById('jt_tempo').value,
                totaljmlh: parseFloat(document.getElementById('totaljmlh').value) || 0,
                detail: dataPenjualan // array yang sudah kamu simpan saat tambah item
            };

            fetch('prosessimpanpnj.php', {
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
                    localStorage.removeItem('formPenjualanState');
                    initializeFormButtons();
                } else {
                    showToast('Gagal menyimpan: ' + res.message, '#dc3545');
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
            const url = `notaprintpem.php?nonota=${encodeURIComponent(noNota)}`;
            window.open(url, '_blank');
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

            form.querySelectorAll('input, select, textarea, button').forEach(el => {
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
            localStorage.setItem('formPenjualanState', JSON.stringify(formData));
        });

    </script>
</body>
</html>
