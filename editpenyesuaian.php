<?php
require_once 'init.php';
$nonota = $_GET['nonota'] ?? '';

?>
<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Edit Penyesuaian</title>
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
        <h2>Edit Penyesuaian</h2>
        <div class="action-pb-bar" style="display: flex; justify-content: space-between; align-items: center; margin-top: 10px;">
            <div style="display: flex; gap: 8px;">
                <button id="btnSave" type="submit">Simpan</button>
                <button id="btnEdit" type="button" onclick="initializeEdit()">Edit</button>
                <button id="btnHapus" type="button">Hapus</button>    
                <button id="btnCancel" type="button" onclick="cancelForm()">Batal</button>
                <button id="btnPrint" type="button">Print</button>
            </div>
            <button id="btnKembali" type="button" onclick="window.location.href='penyesuaian.php'">Kembali</button>
        </div>

        <form id="formPenyesuaian" action="prosespembelian.php" method="POST">
            <input type="hidden" name="no_nota_lama" id="no_nota_lama" />
            <div id="form-pembelian-atas">

                <div class="form-pb-row">
                    <div class="form-pb-col">
                        <label for="no_nota">No Nota</label>
                        <input type="text" id="no_nota" data-table="zpenyesuaian" data-field="nonota" data-check="duplikat" onblur="cekValidasi(this)" name="no_nota" class="short-input" style="text-transform: uppercase;">
                    </div>
                </div>
                
                <div class="form-pb-row">
                    <div class="form-pb-col">
                        <label for="tanggal">Tanggal</label>
                        <input type="date" id="tanggal" name="tanggal" class="short-input" style="text-transform: uppercase;">
                    </div>
                </div>

                <div class="form-pb-row">
                    <div class="form-pb-col">
                        <label for="kodegd">Gudang</label>
                        <select id="kodegd" name="kodegd" class="short-input" style="text-transform: uppercase;" required>
                            <option value="">Pilih Gudang</option>
                            <!-- Akan diisi via JavaScript -->
                        </select>
                    </div>
                </div>
            </div>
            <!-- FORM BAWAH: RINCIAN -->
            <div class="form-pembelian-tengah">
                <div style="display: flex; justify-content: flex-end;">
                    <button id="btnTambahItem" type="button">+</button>
                </div>
                <div style="overflow-x: auto;">
                    <table class="tabel-hasil" id="tabelPenyesuaian" style="min-width: 1200px;">
                        <thead>
                            <tr>
                                <th style="min-width: 100px;">Kode brg</th>
                                <th style="min-width: 200px;">Nama brg</th>
                                <th style="min-width: 50px;">Jlh 1</th>
                                <th style="min-width: 80px;">Satuan 1</th>
                                <th style="min-width: 50px;">Jlh 2</th>
                                <th style="min-width: 80px;">Satuan 2</th>
                                <th style="min-width: 50px;">Jlh 3</th>
                                <th style="min-width: 80px;">Satuan 3</th>
                                <th style="min-width: 50px;">Qty</th>
                                <th style="min-width: 80px;">Harga</th>
                                <th style="min-width: 120px; display: none;" id="thAksi">Aksi</th>
                            </tr>
                        </thead>
                        <tbody>
                            <!-- isi dari popupitempembelian-->
                        </tbody>
                    </table>
                </div>
            </div>

            <div id="form-pembelian-bawah">
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
        <div id="popupForm" class="popup-pb-overlay" style="display: none;">
            <div class="popup-pb-content">
                <h3>Tambah Data Penyesuaian</h3>
                <form id="formDetailPenyesuaian">
                    <input type="hidden" name="popup_isi1" id="popup_isi1" value=""> 
                    <input type="hidden" name="popup_isi2" id="popup_isi2" value="">
                    <input type="hidden" name="popup_sisa1" id="popup_sisa1" value="">
                    <input type="hidden" name="popup_sisa2" id="popup_sisa2" value="">
                    <input type="hidden" name="popup_sisa3" id="popup_sisa3" value="">

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
                        <input type="text" id="popup_sisa" name="popup_sisa" style="background-color: #e94141ff; color: #ffffff;" disabled>
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
                        <label for="popup_qty">Qty</label>
                        <input type="number" id="popup_qty" name="popup_qty" style="text-align: right;" min="0">
                    </div>

                    <div class="popup-pb-row">
                        <label for="popup_harga">Harga</label>
                        <input type="number" id="popup_harga" name="popup_harga" style="text-align: right;" min="0">
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
        let dataPenyesuaian = [];
        let indexToDelete = null;
        let hapusTipe = 'item';
        function loadPenyesuaian(nonota) {
            const noNota = new URLSearchParams(window.location.search).get('nonota');

            fetch(`getpenyesuaian.php?nonota=${noNota}`)
            .then(res => res.json())
            .then(data => {
                if (data.status === 'success') {
                    // Isi form header
                    document.getElementById('tanggal').value = data.header.tanggal;
                    document.getElementById('no_nota').value = data.header.no_nota;
                    document.getElementById('no_nota_lama').value = data.header.no_nota;
                    document.getElementById('kodegd').value = data.header.kodegd;

                    dataPenyesuaian = data.detail;
                    
                    renderTabelPenyesuaian();

                    if (callback) callback();
                } else {
                    alert(data.message);
                }
            });
        }
        const nonota = document.getElementById('no_nota');
        const tanggal = document.getElementById('tanggal');
        const KodeGd = document.getElementById('kodegd');

        const popupKodeInput = document.getElementById('popup_kodebrg');
        const popupNamaInput = document.getElementById('popup_namabrg');
        const popupSatuan1 = document.getElementById('popup_satuan1');
        const popupSatuan2 = document.getElementById('popup_satuan2');
        const popupSatuan3 = document.getElementById('popup_satuan3');
        const popupIsi1 = document.getElementById('popup_isi1');
        const popupIsi2 = document.getElementById('popup_isi2');
        const popupJlh1 = document.getElementById('popup_jlh1');
        const popupJlh2 = document.getElementById('popup_jlh2');
        const popupJlh3 = document.getElementById('popup_jlh3');
        const popupQty = document.getElementById('popup_qty');
        const popupHarga = document.getElementById('popup_harga');

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

        function cekValidasi(input) {
            const value = input.value.trim();
            const table = input.dataset.table;
            const field = input.dataset.field;
            const checkType = input.dataset.check; // 'duplikat' atau 'eksistensi'
            const resetTargets = input.dataset.reset ? input.dataset.reset.split(',') : [];

            if (!value || value === '*') {
                input.dataset.prev = value; // simpan supaya tidak terus-terusan validasi saat blur
                return;
            }

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
            document.getElementById('formDetailPenyesuaian').reset();
            popupKodeInput.value = item.kodebrg;
            popupNamaInput.value = item.namabrg;
            popupSatuan1.value = item.satuan1;
            popupSatuan2.value = item.satuan2;
            popupSatuan3.value = item.satuan3;
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
            getSisaPeny();
            tutupPopupBarang();
        }

        function tutupPopupBarang() {
            document.getElementById('popupCariBarang').style.display = 'none';
        }

        function renderTabelPenyesuaian() {
            const tbody = document.querySelector('#tabelPenyesuaian tbody');
            tbody.innerHTML = '';

            dataPenyesuaian.forEach((item, index) => {
                const tr = document.createElement('tr');
                tr.innerHTML = `
                    <td style="display: none;">${item.isi1}</td>
                    <td style="display: none;">${item.isi2}</td>
                    <td>${item.kodebrg}</td>
                    <td>${item.namabrg}</td>
                    <td style="display: none;">${item.kodegd}</td>
                    <td style="text-align: right;">${item.jlh1}</td>
                    <td>${item.satuan1}</td>
                    <td style="text-align: right;">${item.jlh2 || ''}</td>
                    <td>${item.satuan2 || ''}</td>
                    <td style="text-align: right;">${item.jlh3 || ''}</td>
                    <td>${item.satuan3 || ''}</td>
                    <td style="text-align: right;">${item.qty || ''}</td>
                    <td style="text-align: right;">${item.harga || ''}</td>
                    <td style="display: none;" id="td-btn-${index}">
                        <button type="button" onclick="editItem(${index})">Edit</button>
                        <button type="button" onclick="hapusItem(${index})">Hapus</button>
                    </td>
                `;
                tbody.appendChild(tr);
            });
        }

        document.getElementById('formDetailPenyesuaian').addEventListener('submit', function (e) {
            e.preventDefault();

            const item = {
                isi1: parseInt(popupIsi1.value) || 0,
                isi2: parseInt(popupIsi2.value) || 0,
                kodebrg: popupKodeInput.value.trim().toUpperCase(),
                namabrg: popupNamaInput.value.trim().toUpperCase(),
                jlh1: parseInt(popupJlh1.value) || 0,
                satuan1: popupSatuan1.value.trim(),
                jlh2: parseInt(popupJlh2.value) || null,
                satuan2: popupSatuan2.value.trim() || null,
                jlh3: parseInt(popupJlh3.value) || null,
                satuan3: popupSatuan3.value.trim() || null,
                qty: parseInt(popupQty.value) || null,
                harga: parseInt(popupHarga.value) || null
            };

            if (currentMode === "edit" && this.dataset.editingIndex !== undefined) {
                // UPDATE item
                const idx = parseInt(this.dataset.editingIndex, 10);
                dataPenyesuaian[idx] = item;
                tutupPopup();
                showToast('Item berhasil diupdate!');
            } else {
                // TAMBAH item baru
                dataPenyesuaian.push(item);
                showToast('Item berhasil disimpan!');
            }

            renderTabelPenyesuaian();
            resetitem();
            currentMode = "input";
            delete this.dataset.editingIndex;
        });

        function resetitem() {
            document.getElementById('formDetailPenyesuaian').reset();
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
            const item = dataPenyesuaian[index];
            popupIsi1.value       = item.isi1;
            popupIsi2.value       = item.isi2;
            popupKodeInput.value  = item.kodebrg;
            popupNamaInput.value  = item.namabrg;
            popupJlh1.value       = item.jlh1;
            popupSatuan1.value    = item.satuan1;
            popupJlh2.value       = item.jlh2 || '';
            popupSatuan2.value    = item.satuan2 || '';
            popupJlh3.value       = item.jlh3 || '';
            popupSatuan3.value    = item.satuan3 || '';
            popupQty.value       = item.qty || '';
            popupHarga.value       = item.harga || '';

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
            const formEdit = document.getElementById('formDetailPenyesuaian');
            formEdit.dataset.editingIndex = index;

            // Tampilkan popup edit
            getSisaPeny();
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
                    dataPenyesuaian.splice(indexToDelete, 1);
                    renderTabelPenyesuaian();
                    document.getElementById('thAksi').style.display = '';
                    const allTdAksi = document.querySelectorAll('[id^="td-btn-"]');
                    allTdAksi.forEach(td => td.style.display = '');
                    showToast('Item berhasil dihapus!', '#dc3545');
                }

                if (hapusTipe === 'nota') {
                    const noNota = document.getElementById('no_nota').value;
                    fetch(`proseshapuspeny.php?nonota=${encodeURIComponent(noNota)}`, {
                        method: 'GET'
                    })
                    .then(res => res.json())
                    .then(response => {
                        if (response.success) {
                            showToast('Penyesuaian berhasil dihapus!', '#dc3545');
                            // redirect atau reset halaman
                            setTimeout(() => window.location.href = 'penyesuaian.php', 1000);
                        } else {
                            showToast('Gagal menghapus penyesuaian!', '#dc3545');
                        }
                    })
                    .catch(() => showToast('Terjadi kesalahan server!', '#dc3545'));
                }
            }

            indexToDelete = null;
            hapusTipe = 'item';
        }          

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
            document.getElementById('kodegd').disabled = true;
        }
        //initializeFormButtons();

        function initializeEdit() {
            if (!cekAkses('ubah')) return;
            currentstat = 'update';
            showToast('Kamu sedang menambah data...', '#ffc107');
            const saved = JSON.parse(localStorage.getItem('formPenyesuaianEdit') || '{}');
            saved.currentstat = 'update';
            localStorage.setItem('formPenyesuaianEdit', JSON.stringify(saved));
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
            document.getElementById('kodegd').disabled = false;
        }

        function laststat() {
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

            document.getElementById('tanggal').disabled = false;
            document.getElementById('no_nota').disabled = false;
            document.getElementById('kodegd').disabled = false;
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
            localStorage.removeItem('formPenyesuaianEdit');
        }

        document.getElementById('btnTambahItem').addEventListener('click', () => {
            document.getElementById('popupForm').style.display = 'flex';
        });

        // Tutup popup
        function tutupPopup() {
            document.getElementById('formDetailPenyesuaian').reset();
            delete formDetailPenyesuaian.dataset.editingIndex;
            document.getElementById('popupForm').style.display = 'none';
            popupJlh2.disabled = true;
            popupJlh3.disabled = true;
        }

        async function isiDropdownGudang() {
            try {
                const response = await fetch('getgudang.php');
                const data = await response.json();

                const dropdownTambah = document.getElementById('kodegd');

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

            const saved = JSON.parse(localStorage.getItem('formPenyesuaianEdit') || '{}');
            currentstat = saved.currentstat || null;

            // 2. Panggil loadPenyesuaian hanya jika ada nonota
            const noNota = new URLSearchParams(window.location.search).get('nonota');
            if (noNota) {
                await loadPenyesuaian(noNota, () => {
                if (currentstat === 'update') laststat();
            }); // menunggu data penyesuaian selesai di-load
            }

            // 3. Pulihkan data form dari localStorage (hanya jika ada data)
            if (Object.keys(saved).length > 0) {
                Object.keys(saved).forEach(id => {
                    if (id !== 'dataPenyesuaian' && id !== 'currentstat' && id !== 'currentmode') {
                        const el = document.getElementById(id);
                        if (el) {
                            el.value = saved[id].value || el.value;
                            el.disabled = saved[id].disabled ?? false;
                        }
                    }
                });

                // Pulihkan data tabel
                if (saved.dataPenyesuaian) {
                    dataPenyesuaian = saved.dataPenyesuaian;
                    renderTabelPenyesuaian();
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
                kodegd: document.getElementById('kodegd').value,
                detail: dataPenyesuaian, // array yang sudah kamu simpan saat tambah item

                operator: document.body.dataset.kodeuser || ""  
            };

            fetch('prosesupdatepeny.php', {
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
                    localStorage.removeItem('formPenyesuaianEdit');
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
            const url = `notaprintpeny.php?nonota=${encodeURIComponent(noNota)}&from=editpenyesuaian.php`;
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
            const form = document.getElementById('formPenyesuaian');
            const formData = {};

            form.querySelectorAll('input, select, option, textarea, button').forEach(el => {
                formData[el.id] = {
                    value: el.value,
                    disabled: el.disabled
                };
            });

            // Simpan currentstat
            formData['currentstat'] = currentstat;

            // Simpan dataPenyesuaian juga jika penting
            formData['dataPenyesuaian'] = dataPenyesuaian;

            // Simpan ke localStorage
            localStorage.setItem('formPenyesuaianEdit', JSON.stringify(formData));
        });
    </script>
</body>
</html>
