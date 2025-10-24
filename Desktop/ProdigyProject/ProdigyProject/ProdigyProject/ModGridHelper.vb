Module ModGridHelper

    ' === Setup untuk Grid Pembelian (zbelim) ===
    Public Sub SetupGridPembelian(ByVal grid As DataGridView)
        With grid
            .AllowUserToAddRows = False
            .AllowUserToDeleteRows = False
            .AllowUserToResizeRows = False
            .ReadOnly = True
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .MultiSelect = False
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None
            .RowHeadersVisible = False
            .EditMode = DataGridViewEditMode.EditProgrammatically ' <<<<< penting

            ' === Definisi Kolom ===
            .Columns.Add("KodeBrg", "Kode brg")
            .Columns("KodeBrg").Width = 100

            .Columns.Add("NamaBrg", "Nama brg")
            .Columns("NamaBrg").Width = 200

            .Columns.Add("KodeGudang", "Kode Gudang")
            .Columns("KodeGudang").Width = 100

            .Columns.Add("Jlh1", "Jlh 1")
            .Columns("Jlh1").Width = 60

            .Columns.Add("Sat1", "Satuan 1")
            .Columns("Sat1").Width = 80

            .Columns.Add("Jlh2", "Jlh 2")
            .Columns("Jlh2").Width = 60

            .Columns.Add("Sat2", "Satuan 2")
            .Columns("Sat2").Width = 80

            .Columns.Add("Jlh3", "Jlh 3")
            .Columns("Jlh3").Width = 60

            .Columns.Add("Sat3", "Satuan 3")
            .Columns("Sat3").Width = 80

            .Columns.Add("Harga", "Harga")
            .Columns("Harga").Width = 100
            .Columns("Harga").DefaultCellStyle.Format = "N0"

            .Columns.Add("Disca", "Disca")
            .Columns("Disca").Width = 50

            .Columns.Add("Discb", "Discb")
            .Columns("Discb").Width = 50

            .Columns.Add("Discc", "Discc")
            .Columns("Discc").Width = 50

            .Columns.Add("DiscRp", "Disc Rp")
            .Columns("DiscRp").Width = 100
            .Columns("DiscRp").DefaultCellStyle.Format = "N0"

            .Columns.Add("Jumlah", "Jumlah")
            .Columns("Jumlah").Width = 100
            .Columns("Jumlah").DefaultCellStyle.Format = "N0"
        End With
    End Sub

    ' === Setup untuk Grid Pembelian (zjualm) ===
    Public Sub SetupGridPenjualan(ByVal grid As DataGridView)
        With grid
            .AllowUserToAddRows = False
            .AllowUserToDeleteRows = False
            .AllowUserToResizeRows = False
            .ReadOnly = True
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .MultiSelect = False
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None
            .RowHeadersVisible = False
            .EditMode = DataGridViewEditMode.EditProgrammatically ' <<<<< penting

            ' === Definisi Kolom ===
            .Columns.Add("KodeBrg", "Kode brg")
            .Columns("KodeBrg").Width = 100

            .Columns.Add("NamaBrg", "Nama brg")
            .Columns("NamaBrg").Width = 200

            .Columns.Add("KodeGudang", "Kode Gudang")
            .Columns("KodeGudang").Width = 100

            .Columns.Add("Jlh1", "Jlh 1")
            .Columns("Jlh1").Width = 60

            .Columns.Add("Sat1", "Satuan 1")
            .Columns("Sat1").Width = 80

            .Columns.Add("Jlh2", "Jlh 2")
            .Columns("Jlh2").Width = 60

            .Columns.Add("Sat2", "Satuan 2")
            .Columns("Sat2").Width = 80

            .Columns.Add("Jlh3", "Jlh 3")
            .Columns("Jlh3").Width = 60

            .Columns.Add("Sat3", "Satuan 3")
            .Columns("Sat3").Width = 80

            .Columns.Add("Harga", "Harga")
            .Columns("Harga").Width = 100
            .Columns("Harga").DefaultCellStyle.Format = "N0"

            .Columns.Add("Disca", "Disca")
            .Columns("Disca").Width = 50

            .Columns.Add("Discb", "Discb")
            .Columns("Discb").Width = 50

            .Columns.Add("Discc", "Discc")
            .Columns("Discc").Width = 50

            .Columns.Add("DiscRp", "Disc Rp")
            .Columns("DiscRp").Width = 100
            .Columns("DiscRp").DefaultCellStyle.Format = "N0"

            .Columns.Add("Jumlah", "Jumlah")
            .Columns("Jumlah").Width = 100
            .Columns("Jumlah").DefaultCellStyle.Format = "N0"
        End With
    End Sub

    ' === Setup untuk Grid Stok (zstok) ===
    Public Sub SetupGridStok(ByVal grid As DataGridView)
        With grid
            .AllowUserToAddRows = False
            .AllowUserToDeleteRows = False
            .AllowUserToResizeRows = False
            .ReadOnly = True
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .MultiSelect = False
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None
            .RowHeadersVisible = False
            .Columns.Clear()

            ' === Kolom utama dari tabel zstok ===
            .Columns.Add("kodebrg", "Kode Barang")
            .Columns("kodebrg").Width = 120

            .Columns.Add("namabrg", "Nama Barang")
            .Columns("namabrg").Width = 200

            .Columns.Add("kodemerk", "Kode Merk")
            .Columns("kodemerk").Width = 100

            .Columns.Add("kodegol", "Kode Golongan")
            .Columns("kodegol").Width = 120

            .Columns.Add("kodegrup", "Kode Grup")
            .Columns("kodegrup").Width = 100

            .Columns.Add("satuan1", "Satuan 1")
            .Columns("satuan1").Width = 80

            .Columns.Add("satuan2", "Satuan 2")
            .Columns("satuan2").Width = 80

            .Columns.Add("satuan3", "Satuan 3")
            .Columns("satuan3").Width = 80

            .Columns.Add("isi1", "Isi 1")
            .Columns("isi1").Width = 50

            .Columns.Add("isi2", "Isi 2")
            .Columns("isi2").Width = 50

            .Columns.Add("hrgbeli", "Harga Beli")
            .Columns("hrgbeli").Width = 100
            .Columns("hrgbeli").DefaultCellStyle.Format = "N0"

            ' Contoh harga jual
            .Columns.Add("harga1", "Harga 1")
            .Columns("harga1").Width = 80
            .Columns("harga1").DefaultCellStyle.Format = "N0"

            .Columns.Add("harga2", "Harga 2")
            .Columns("harga2").Width = 80
            .Columns("harga2").DefaultCellStyle.Format = "N0"

            .Columns.Add("harga3", "Harga 3")
            .Columns("harga3").Width = 80
            .Columns("harga3").DefaultCellStyle.Format = "N0"

            .Columns.Add("harga4", "Harga 4")
            .Columns("harga4").Width = 80
            .Columns("harga4").DefaultCellStyle.Format = "N0"

            .Columns.Add("harga5", "Harga 5")
            .Columns("harga5").Width = 80
            .Columns("harga5").DefaultCellStyle.Format = "N0"

            .Columns.Add("harga6", "Harga 6")
            .Columns("harga6").Width = 80
            .Columns("harga6").DefaultCellStyle.Format = "N0"

            ' Contoh harga jual
            .Columns.Add("harga11", "Harga 11")
            .Columns("harga11").Width = 80
            .Columns("harga11").DefaultCellStyle.Format = "N0"

            .Columns.Add("harga22", "Harga 22")
            .Columns("harga22").Width = 80
            .Columns("harga22").DefaultCellStyle.Format = "N0"

            .Columns.Add("harga33", "Harga 33")
            .Columns("harga33").Width = 80
            .Columns("harga33").DefaultCellStyle.Format = "N0"

            .Columns.Add("harga44", "Harga 44")
            .Columns("harga44").Width = 80
            .Columns("harga44").DefaultCellStyle.Format = "N0"

            .Columns.Add("harga55", "Harga 55")
            .Columns("harga55").Width = 80
            .Columns("harga55").DefaultCellStyle.Format = "N0"

            .Columns.Add("harga66", "Harga 66")
            .Columns("harga66").Width = 80
            .Columns("harga66").DefaultCellStyle.Format = "N0"

            ' Contoh harga jual
            .Columns.Add("harga111", "Harga 111")
            .Columns("harga111").Width = 80
            .Columns("harga111").DefaultCellStyle.Format = "N0"

            .Columns.Add("harga222", "Harga 222")
            .Columns("harga222").Width = 80
            .Columns("harga222").DefaultCellStyle.Format = "N0"

            .Columns.Add("harga333", "Harga 333")
            .Columns("harga333").Width = 80
            .Columns("harga333").DefaultCellStyle.Format = "N0"

            .Columns.Add("harga444", "Harga 444")
            .Columns("harga444").Width = 80
            .Columns("harga444").DefaultCellStyle.Format = "N0"

            .Columns.Add("harga555", "Harga 555")
            .Columns("harga555").Width = 80
            .Columns("harga555").DefaultCellStyle.Format = "N0"

            .Columns.Add("harga666", "Harga 666")
            .Columns("harga666").Width = 80
            .Columns("harga666").DefaultCellStyle.Format = "N0"
        End With
    End Sub

    ' === Setup untuk Grid Pembelian (zbeli) ===
    Public Sub SetupGridBeli(ByVal grid As DataGridView)
        With grid
            .AllowUserToAddRows = False
            .AllowUserToDeleteRows = False
            .AllowUserToResizeRows = False   ' <<< baris tidak bisa di-resize
            .ReadOnly = True
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .MultiSelect = False
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None
            .RowHeadersVisible = False
            .Columns.Clear()

            ' === Definisi Kolom ===
            .Columns.Add("tgl", "Tanggal")
            .Columns("tgl").Width = 150

            .Columns.Add("nonota", "No Nota")
            .Columns("nonota").Width = 193

            .Columns.Add("namasup", "Supplier")
            .Columns("namasup").Width = 200

            .Columns.Add("nilai", "Nilai")
            .Columns("nilai").Width = 150
            .Columns("nilai").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("nilai").DefaultCellStyle.Format = "N0"

            .Columns.Add("lunas", "Lunas")
            .Columns("lunas").Width = 150
        End With
    End Sub

    ' === Setup untuk Grid Pembelian (zbeli) ===
    Public Sub SetupGridJual(ByVal grid As DataGridView)
        With grid
            .AllowUserToAddRows = False
            .AllowUserToDeleteRows = False
            .AllowUserToResizeRows = False   ' <<< baris tidak bisa di-resize
            .ReadOnly = True
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .MultiSelect = False
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None
            .RowHeadersVisible = False
            .Columns.Clear()

            ' === Definisi Kolom ===
            .Columns.Add("tgl", "Tanggal")
            .Columns("tgl").Width = 150

            .Columns.Add("nonota", "No Nota")
            .Columns("nonota").Width = 193

            .Columns.Add("namakust", "Kustomer")
            .Columns("namakust").Width = 100

            .Columns.Add("namasls", "Sales")
            .Columns("namasls").Width = 100

            .Columns.Add("nilai", "Nilai")
            .Columns("nilai").Width = 150
            .Columns("nilai").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("nilai").DefaultCellStyle.Format = "N0"

            .Columns.Add("lunas", "Lunas")
            .Columns("lunas").Width = 150
        End With
    End Sub

    ' === Setup untuk Grid Suppluer (zsupplier) ===
    Public Sub SetupGridSupplier(ByVal grid As DataGridView)
        With grid
            .AllowUserToAddRows = False
            .AllowUserToDeleteRows = False
            .AllowUserToResizeRows = False
            .ReadOnly = True
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .MultiSelect = False
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None
            .RowHeadersVisible = False
            .Columns.Clear()

            ' === Kolom utama dari tabel zstok ===
            .Columns.Add("kodesup", "Kode Supplier")
            .Columns("kodesup").Width = 120

            .Columns.Add("namasup", "Nama Supplier")
            .Columns("namasup").Width = 200

            .Columns.Add("alamat", "Alamat")
            .Columns("alamat").Width = 100

            .Columns.Add("kota", "Kota")
            .Columns("kota").Width = 120

            .Columns.Add("ktp", "KTP")
            .Columns("ktp").Width = 100

            .Columns.Add("npwp", "NPWP")
            .Columns("npwp").Width = 80
        End With
    End Sub

    ' === Setup untuk Grid Suppluer (zsupplier) ===
    Public Sub SetupGridKustomer(ByVal grid As DataGridView)
        With grid
            .AllowUserToAddRows = False
            .AllowUserToDeleteRows = False
            .AllowUserToResizeRows = False
            .ReadOnly = True
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .MultiSelect = False
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None
            .RowHeadersVisible = False
            .Columns.Clear()

            ' === Kolom utama dari tabel zstok ===
            .Columns.Add("kodekust", "Kode Kustomer")
            .Columns("kodekust").Width = 120

            .Columns.Add("namakust", "Nama Kustomer")
            .Columns("namakust").Width = 200

            .Columns.Add("alamat", "Alamat")
            .Columns("alamat").Width = 100

            .Columns.Add("kota", "Kota")
            .Columns("kota").Width = 120

            .Columns.Add("ktp", "KTP")
            .Columns("ktp").Width = 100

            .Columns.Add("npwp", "NPWP")
            .Columns("npwp").Width = 80
        End With
    End Sub

    ' === Setup untuk Grid Suppluer (zsupplier) ===
    Public Sub SetupGridSales(ByVal grid As DataGridView)
        With grid
            .AllowUserToAddRows = False
            .AllowUserToDeleteRows = False
            .AllowUserToResizeRows = False
            .ReadOnly = True
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .MultiSelect = False
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None
            .RowHeadersVisible = False
            .Columns.Clear()

            ' === Kolom utama dari tabel zstok ===
            .Columns.Add("kodesls", "Kode Sales")
            .Columns("kodesls").Width = 120

            .Columns.Add("namasls", "Nama Sales")
            .Columns("namasls").Width = 200

            .Columns.Add("alamat", "Alamat")
            .Columns("alamat").Width = 100

            .Columns.Add("kota", "Kota")
            .Columns("kota").Width = 120

            .Columns.Add("ktp", "KTP")
            .Columns("ktp").Width = 100

            .Columns.Add("npwp", "NPWP")
            .Columns("npwp").Width = 80
        End With
    End Sub

    ' === Setup untuk Grid Mutasi (zmutasim) ===
    Public Sub SetupGridMutasi(ByVal grid As DataGridView)
        With grid
            .AllowUserToAddRows = False
            .AllowUserToDeleteRows = False
            .AllowUserToResizeRows = False
            .ReadOnly = True
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .MultiSelect = False
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None
            .RowHeadersVisible = False
            .EditMode = DataGridViewEditMode.EditProgrammatically ' <<<<< penting

            ' === Definisi Kolom ===
            .Columns.Add("KodeBrg", "Kode brg")
            .Columns("KodeBrg").Width = 100

            .Columns.Add("NamaBrg", "Nama brg")
            .Columns("NamaBrg").Width = 438

            .Columns.Add("Jlh1", "Jlh 1")
            .Columns("Jlh1").Width = 60

            .Columns.Add("Sat1", "Satuan 1")
            .Columns("Sat1").Width = 80

            .Columns.Add("Jlh2", "Jlh 2")
            .Columns("Jlh2").Width = 60

            .Columns.Add("Sat2", "Satuan 2")
            .Columns("Sat2").Width = 80

            .Columns.Add("Jlh3", "Jlh 3")
            .Columns("Jlh3").Width = 60

            .Columns.Add("Sat3", "Satuan 3")
            .Columns("Sat3").Width = 80
        End With
    End Sub

    ' === Setup untuk Grid Mutasi (zmutasim) ===
    Public Sub SetupGridDMutasi(ByVal grid As DataGridView)
        With grid
            .AllowUserToAddRows = False
            .AllowUserToDeleteRows = False
            .AllowUserToResizeRows = False
            .ReadOnly = True
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .MultiSelect = False
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None
            .RowHeadersVisible = False

            ' === Definisi Kolom ===
            .Columns.Add("tgl", "Tanggal")
            .Columns("tgl").Width = 100

            .Columns.Add("nonota", "No Nota")
            .Columns("nonota").Width = 223

            .Columns.Add("namagd1", "Nama Gudang 1")
            .Columns("namagd1").Width = 260

            .Columns.Add("namagd2", "Nama Gudang 2")
            .Columns("namagd2").Width = 260

        End With
    End Sub

    ' === Setup untuk Grid Mutasi (zmutasim) ===
    Public Sub SetupGridPenyesuaian(ByVal grid As DataGridView)
        With grid
            .AllowUserToAddRows = False
            .AllowUserToDeleteRows = False
            .AllowUserToResizeRows = False
            .ReadOnly = True
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .MultiSelect = False
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None
            .RowHeadersVisible = False
            .EditMode = DataGridViewEditMode.EditProgrammatically ' <<<<< penting

            ' === Definisi Kolom ===
            .Columns.Add("KodeBrg", "Kode brg")
            .Columns("KodeBrg").Width = 100

            .Columns.Add("NamaBrg", "Nama brg")
            .Columns("NamaBrg").Width = 238

            .Columns.Add("Jlh1", "Jlh 1")
            .Columns("Jlh1").Width = 60

            .Columns.Add("Sat1", "Satuan 1")
            .Columns("Sat1").Width = 80

            .Columns.Add("Jlh2", "Jlh 2")
            .Columns("Jlh2").Width = 60

            .Columns.Add("Sat2", "Satuan 2")
            .Columns("Sat2").Width = 80

            .Columns.Add("Jlh3", "Jlh 3")
            .Columns("Jlh3").Width = 60

            .Columns.Add("Sat3", "Satuan 3")
            .Columns("Sat3").Width = 80

            .Columns.Add("Qty", "Qty")
            .Columns("Qty").Width = 100

            .Columns.Add("Harga", "Harga")
            .Columns("Harga").Width = 100
        End With
    End Sub

    ' === Setup untuk Grid Penyesuaian (zmutasim) ===
    Public Sub SetupGridDPenyesuaian(ByVal grid As DataGridView)
        With grid
            .AllowUserToAddRows = False
            .AllowUserToDeleteRows = False
            .AllowUserToResizeRows = False
            .ReadOnly = True
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .MultiSelect = False
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None
            .RowHeadersVisible = False

            ' === Definisi Kolom ===
            .Columns.Add("tgl", "Tanggal")
            .Columns("tgl").Width = 140

            .Columns.Add("nonota", "No Nota")
            .Columns("nonota").Width = 343

            .Columns.Add("namagd", "Nama Gudang")
            .Columns("namagd").Width = 360

        End With
    End Sub

    ' === Setup untuk Grid Penyesuaian (zmutasim) ===
    Public Sub SetupGridDUser(ByVal grid As DataGridView)
        With grid
            .AllowUserToAddRows = False
            .AllowUserToDeleteRows = False
            .AllowUserToResizeRows = False
            .ReadOnly = True
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .MultiSelect = False
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None
            .RowHeadersVisible = False

            ' === Definisi Kolom ===
            .Columns.Add("kodeuser", "Kode User")
            .Columns("kodeuser").Width = 140

            .Columns.Add("username", "Username")
            .Columns("username").Width = 207

        End With
    End Sub

    ' === Setup untuk Grid Penyesuaian (zmutasim) ===
    Public Sub SetupGridDArea(ByVal grid As DataGridView)
        With grid
            .AllowUserToAddRows = False
            .AllowUserToDeleteRows = False
            .AllowUserToResizeRows = False
            .ReadOnly = True
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .MultiSelect = False
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None
            .RowHeadersVisible = False

            ' === Definisi Kolom ===
            .Columns.Add("kodear", "Kode Area")
            .Columns("kodear").Width = 140

            .Columns.Add("namaar", "Nama Area")
            .Columns("namaar").Width = 207

        End With
    End Sub

    Public Sub SetupGridDTipe(ByVal grid As DataGridView)
        With grid
            .AllowUserToAddRows = False
            .AllowUserToDeleteRows = False
            .AllowUserToResizeRows = False
            .ReadOnly = True
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .MultiSelect = False
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None
            .RowHeadersVisible = False

            ' === Definisi Kolom ===
            .Columns.Add("kodetipe", "Kode Tipe")
            .Columns("kodetipe").Width = 140

            .Columns.Add("namatipe", "Nama Tipe")
            .Columns("namatipe").Width = 207

        End With
    End Sub

    Public Sub SetupGridDGudang(ByVal grid As DataGridView)
        With grid
            .AllowUserToAddRows = False
            .AllowUserToDeleteRows = False
            .AllowUserToResizeRows = False
            .ReadOnly = True
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .MultiSelect = False
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None
            .RowHeadersVisible = False

            ' === Definisi Kolom ===
            .Columns.Add("kodegd", "Kode Gudang")
            .Columns("kodegd").Width = 140

            .Columns.Add("namagd", "Nama Gudang")
            .Columns("namagd").Width = 207

        End With
    End Sub

    Public Sub SetupGridDGrup(ByVal grid As DataGridView)
        With grid
            .AllowUserToAddRows = False
            .AllowUserToDeleteRows = False
            .AllowUserToResizeRows = False
            .ReadOnly = True
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .MultiSelect = False
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None
            .RowHeadersVisible = False

            ' === Definisi Kolom ===
            .Columns.Add("kodegrup", "Kode Grup")
            .Columns("kodegrup").Width = 140

            .Columns.Add("namagrup", "Nama Grup")
            .Columns("namagrup").Width = 207

        End With
    End Sub

    Public Sub SetupGridDMerek(ByVal grid As DataGridView)
        With grid
            .AllowUserToAddRows = False
            .AllowUserToDeleteRows = False
            .AllowUserToResizeRows = False
            .ReadOnly = True
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .MultiSelect = False
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None
            .RowHeadersVisible = False

            ' === Definisi Kolom ===
            .Columns.Add("kodemerk", "Kode Merek")
            .Columns("kodemerk").Width = 140

            .Columns.Add("namamerk", "Nama Merek")
            .Columns("namamerk").Width = 207

        End With
    End Sub

    Public Sub SetupGridDGolongan(ByVal grid As DataGridView)
        With grid
            .AllowUserToAddRows = False
            .AllowUserToDeleteRows = False
            .AllowUserToResizeRows = False
            .ReadOnly = True
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .MultiSelect = False
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None
            .RowHeadersVisible = False

            ' === Definisi Kolom ===
            .Columns.Add("kodegol", "Kode Golongan")
            .Columns("kodegol").Width = 140

            .Columns.Add("namagol", "Nama Golongan")
            .Columns("namagol").Width = 207

        End With
    End Sub
End Module
