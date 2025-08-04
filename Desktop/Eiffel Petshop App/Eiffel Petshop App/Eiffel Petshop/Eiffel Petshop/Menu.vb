Imports MySql.Data.MySqlClient
Imports System.Collections.Generic
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports ZXing
Imports AForge.Video
Imports AForge.Video.DirectShow
Imports System.IO
Imports System.Drawing
Imports Microsoft.VisualBasic

Public Class Menu

    Private connString As String = "server=localhost;user id=root;password=;database=dbeiffelpetshop;"
    Private table As DataTable
    Private table2 As DataTable
    Private table3 As DataTable
    Private table4 As DataTable
    Private table5 As DataTable
    Private stokBarang As Integer = 0
    Private usedTransactionNumbers As New HashSet(Of Integer)()
    Private isGeneratingCode As Boolean = False
    Private videoSource As VideoCaptureDevice
    Private barcodeReader As New BarcodeReader()
    Private _chr As String
    Private _inputBox As String

    Private Property Chr(ByVal p1 As Integer) As String
        Get
            Return _chr
        End Get
        Set(ByVal value As String)
            _chr = value
        End Set
    End Property

    Private Property InputBox(ByVal p1 As String, ByVal p2 As String) As String
        Get
            Return _inputBox
        End Get
        Set(ByVal value As String)
            _inputBox = value
        End Set
    End Property

    Private Sub Menu_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ComboBoxJenisBarang2.Items.Add("Obat")
        ComboBoxJenisBarang2.Items.Add("Mainan")
        ComboBoxJenisBarang2.Items.Add("Makanan")
        ComboBoxJenisBarang2.Items.Add("Properti")
        ComboBoxJenisBarang2.Items.Add("Random")

        ComboBoxJenisBarang5.Items.Add("Obat")
        ComboBoxJenisBarang5.Items.Add("Mainan")
        ComboBoxJenisBarang5.Items.Add("Makanan")
        ComboBoxJenisBarang5.Items.Add("Properti")
        ComboBoxJenisBarang5.Items.Add("Random")

        ComboBoxPosisi.Items.Add("Pimpinan")
        ComboBoxPosisi.Items.Add("Kasir")

        ' Menambahkan item ke ComboBoxSatuan
        ComboBoxSatuan2.Items.Add("Pcs")
        ComboBoxSatuan2.Items.Add("Set")
        ComboBoxSatuan2.Items.Add("Lusin")
        ComboBoxSatuan2.Items.Add("Gram")
        ComboBoxSatuan2.Items.Add("Kg")
        ComboBoxSatuan2.Items.Add("L")
        ComboBoxSatuan2.Items.Add("mL")

        TextBoxStok2.TextAlign = HorizontalAlignment.Right
        TextBoxHargaBeli2.TextAlign = HorizontalAlignment.Right
        TextBoxHargaBarang2.TextAlign = HorizontalAlignment.Right

        DataGridViewBarang1.Columns.Add("KodeBarang", "Kode Barang")
        DataGridViewBarang1.Columns.Add("NamaBarang", "Nama Barang")
        DataGridViewBarang1.Columns.Add("HargaBarang", "Harga Barang")
        DataGridViewBarang1.Columns.Add("Jumlah", "Jumlah")
        DataGridViewBarang1.Columns.Add("Subtotal", "Subtotal")

        ' Mengatur lebar kolom
        DataGridViewBarang1.Columns("KodeBarang").Width = 100
        DataGridViewBarang1.Columns("NamaBarang").Width = 200
        DataGridViewBarang1.Columns("HargaBarang").Width = 100
        DataGridViewBarang1.Columns("Jumlah").Width = 80
        DataGridViewBarang1.Columns("Subtotal").Width = 100

        TextBoxHargaBarang1.TextAlign = HorizontalAlignment.Right
        TextBoxJumlahBarang1.TextAlign = HorizontalAlignment.Center
        TextBoxSubtotalBarang1.TextAlign = HorizontalAlignment.Right
        TextBoxTotalBarang1.TextAlign = HorizontalAlignment.Right
        TextBoxTotalHarga1.TextAlign = HorizontalAlignment.Right
        TextBoxTotalBayar1.TextAlign = HorizontalAlignment.Right
        TextBoxDibayar1.TextAlign = HorizontalAlignment.Right
        TextBoxKembalian1.TextAlign = HorizontalAlignment.Right
        TextBoxDiskon1.TextAlign = HorizontalAlignment.Right

        LoadData()
        LoadDataPelanggan()
        LoadDataUser()
        LoadDataLaporanPenjualan()

        ButtonUbah1.Enabled = False
        ButtonHapus1.Enabled = False
        ButtonUbah2.Enabled = False
        ButtonUbah3.Enabled = False
        ButtonUbah5.Enabled = False

        RadioButtonNonPelanggan.Checked = True
        RadioButtonLaporanPenjualan.Checked = True
        'TextBoxKodePelanggan1.ReadOnly = True

        TextBoxTanggal.Text = DateTime.Now.ToString("dd/MM/yyyy")

        TextBoxKasir.Text = CurrentUser.Username
    End Sub

    Private Sub ButtonKeluar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonKeluar.Click
        Dim result As DialogResult = MessageBox.Show("Apakah Anda yakin ingin keluar?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If result = DialogResult.Yes Then
            Application.Exit() ' Menutup aplikasi sepenuhnya
        End If
    End Sub




    ' Form Barang

    Private Sub LoadData()
        Using conn As New MySqlConnection(connString)
            Try
                conn.Open()
                Dim cmd As New MySqlCommand("SELECT * FROM barang", conn)
                Dim adapter As New MySqlDataAdapter(cmd)
                table = New DataTable()
                adapter.Fill(table)

                DataGridBarang2.DataSource = table

                DataGridBarang2.Sort(DataGridBarang2.Columns("jenisbarang"), System.ComponentModel.ListSortDirection.Ascending)

                DataGridBarang2.Columns("stok").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                DataGridBarang2.Columns("hargabeli").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                DataGridBarang2.Columns("hargajual").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

                DataGridBarang2.Columns(0).Width = 80
                DataGridBarang2.Columns(1).Width = 321
                DataGridBarang2.Columns(2).Width = 50
                DataGridBarang2.Columns(3).Width = 50
                DataGridBarang2.Columns(4).Width = 120
                DataGridBarang2.Columns(5).Width = 100
                DataGridBarang2.Columns(6).Width = 100

            Catch ex As Exception
                MessageBox.Show("An error occurred: " & ex.Message)
            End Try
        End Using
    End Sub

    Private Function GenerateUniqueKodeBarang(ByVal jenisbarang As String, ByVal satuan As String) As String
        Dim kodeBarang As String = ""
        Dim isUnique As Boolean = False
        Dim random As New Random()

        ' Mendapatkan dua huruf pertama berdasarkan jenis barang
        Dim jenisCode As String = ""
        Select Case jenisbarang
            Case "Obat"
                jenisCode = "86"
            Case "Mainan"
                jenisCode = "87"
            Case "Makanan"
                jenisCode = "88"
            Case "Properti"
                jenisCode = "89"
            Case "Random"
                jenisCode = "85"
            Case Else
                jenisCode = "00" ' Default jika jenis barang tidak dikenali
        End Select

        ' Mendapatkan kode untuk satuan
        Dim satuanCode As String = ""
        Select Case satuan
            Case "Pcs"
                satuanCode = "71"
            Case "Set"
                satuanCode = "72"
            Case "Lusin"
                satuanCode = "73"
            Case "Gram"
                satuanCode = "74"
            Case "Kg"
                satuanCode = "75"
            Case "L"
                satuanCode = "76"
            Case "mL"
                satuanCode = "77"
            Case Else
                satuanCode = "00" ' Default jika satuan tidak dikenali
        End Select

        ' Menghitung jumlah angka random yang dibutuhkan agar total panjang kode menjadi 13
        Dim randomLength As Integer = 12 - jenisCode.Length - satuanCode.Length

        ' Loop until a unique code is generated
        While Not isUnique
            ' Membuat bagian angka random di tengah
            Dim randomPart As String = ""
            For i As Integer = 1 To randomLength
                randomPart &= random.Next(0, 10).ToString()
            Next

            ' Menggabungkan kode: jenis + random + satuan
            kodeBarang = jenisCode & randomPart & satuanCode

            ' Cek apakah kode barang sudah ada di database
            Using conn As New MySqlConnection(connString)
                conn.Open()
                Dim checkCmd As New MySqlCommand("SELECT COUNT(*) FROM barang WHERE kodebarang = @kodebarang", conn)
                checkCmd.Parameters.AddWithValue("@kodebarang", kodeBarang)

                Dim count As Integer = Convert.ToInt32(checkCmd.ExecuteScalar())
                If count = 0 Then
                    isUnique = True ' Kode barang unik
                End If
            End Using
        End While

        Return kodeBarang
    End Function

    Private Function IsDataExists(ByVal namabarang As String, ByVal satuan As String, ByVal jenisbarang As String) As Boolean
        Using conn As New MySqlConnection(connString)
            conn.Open()
            Dim query As String = "SELECT COUNT(*) FROM barang WHERE namabarang = @namabarang AND satuan = @satuan AND jenisbarang = @jenisbarang"
            Dim cmd As New MySqlCommand(query, conn)

            cmd.Parameters.AddWithValue("@namabarang", namabarang)
            cmd.Parameters.AddWithValue("@satuan", satuan)
            cmd.Parameters.AddWithValue("@jenisbarang", jenisbarang)

            Dim count As Integer = Convert.ToInt32(cmd.ExecuteScalar())
            Return count > 0 ' Mengembalikan true jika data sudah ada
        End Using
    End Function

    Private Sub TextBoxStok2_KeyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs) Handles TextBoxStok2.KeyPress
        ' Hanya izinkan angka, titik, dan koma
        If Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsDigit(e.KeyChar) AndAlso e.KeyChar <> "." AndAlso e.KeyChar <> "," Then
            e.Handled = True
        End If
    End Sub

    Private Sub TextBoxHargaBeli2_KeyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs) Handles TextBoxHargaBeli2.KeyPress
        ' Hanya izinkan angka, titik, dan koma
        If Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsDigit(e.KeyChar) AndAlso e.KeyChar <> "." AndAlso e.KeyChar <> "," Then
            e.Handled = True
        End If
    End Sub

    Private Sub TextBoxHargaBarang2_KeyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs) Handles TextBoxHargaBarang2.KeyPress
        ' Hanya izinkan angka, titik, dan koma
        If Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsDigit(e.KeyChar) AndAlso e.KeyChar <> "." AndAlso e.KeyChar <> "," Then
            e.Handled = True
        End If
    End Sub

    Private Sub ButtonSimpan2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonSimpan2.Click

        If String.IsNullOrEmpty(TextBoxNamaBarang2.Text) OrElse
            String.IsNullOrEmpty(TextBoxStok2.Text) OrElse
            ComboBoxSatuan2.SelectedIndex = -1 OrElse
            ComboBoxJenisBarang2.SelectedIndex = -1 OrElse
            String.IsNullOrEmpty(TextBoxHargaBeli2.Text) OrElse
            String.IsNullOrEmpty(TextBoxHargaBarang2.Text) Then
            MessageBox.Show("Silakan lengkapi semua kolom yang diperlukan.")
            Return
        End If

        If IsDataExists(TextBoxNamaBarang2.Text, ComboBoxSatuan2.SelectedItem.ToString(), ComboBoxJenisBarang2.SelectedItem.ToString()) Then
            MessageBox.Show("Data sudah ada. Silakan masukkan data yang berbeda.")
            Return
        End If

        TextBoxKodeBarang2.Text = GenerateUniqueKodeBarang(ComboBoxJenisBarang2.SelectedItem.ToString(), ComboBoxSatuan2.SelectedItem.ToString())

        Dim hargaBeli As Decimal
        Dim hargaJual As Decimal

        If Not Decimal.TryParse(TextBoxHargaBeli2.Text.Replace("Rp ", "").Replace(".", "").Replace(",", ""), hargaBeli) Then
            MessageBox.Show("Harga beli tidak valid.")
            Return
        End If

        If Not Decimal.TryParse(TextBoxHargaBarang2.Text.Replace("Rp ", "").Replace(".", "").Replace(",", ""), hargaJual) Then
            MessageBox.Show("Harga jual tidak valid.")
            Return
        End If

        Using conn As New MySqlConnection(connString)
            Try
                conn.Open()

                ' SQL command to insert new record
                Dim cmd As New MySqlCommand("INSERT INTO barang (kodebarang, namabarang, stok, satuan, jenisbarang, hargabeli, hargajual) VALUES (@kodebarang, @namabarang, @stok, @satuan, @jenisbarang, @hargabeli, @hargajual)", conn)

                ' Adding parameters
                cmd.Parameters.AddWithValue("@kodebarang", TextBoxKodeBarang2.Text)
                cmd.Parameters.AddWithValue("@namabarang", TextBoxNamaBarang2.Text)
                cmd.Parameters.AddWithValue("@stok", Convert.ToInt32(TextBoxStok2.Text))
                cmd.Parameters.AddWithValue("@satuan", ComboBoxSatuan2.SelectedItem.ToString())
                cmd.Parameters.AddWithValue("@jenisbarang", ComboBoxJenisBarang2.SelectedItem.ToString())
                cmd.Parameters.AddWithValue("@hargabeli", Convert.ToDecimal(TextBoxHargaBeli2.Text))
                cmd.Parameters.AddWithValue("@hargajual", Convert.ToDecimal(TextBoxHargaBarang2.Text))

                ' Execute the command
                cmd.ExecuteNonQuery()
                MessageBox.Show("Data berhasil disimpan!")

                LoadData()

                TextBoxKodeBarang2.Text = ""
                TextBoxNamaBarang2.Text = ""
                TextBoxStok2.Text = ""
                ComboBoxSatuan2.SelectedIndex = -1
                ComboBoxJenisBarang2.SelectedIndex = -1
                TextBoxHargaBeli2.Text = ""
                TextBoxHargaBarang2.Text = ""

            Catch ex As Exception
                MessageBox.Show("An error occurred: " & ex.Message)
            Finally
                conn.Close()
            End Try

            ButtonSimpan2.Enabled = True
            ButtonUbah2.Enabled = False
        End Using
    End Sub

    Private Sub ButtonUbah2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonUbah2.Click
        If String.IsNullOrEmpty(TextBoxKodeBarang2.Text) Then
            MessageBox.Show("Silakan pilih barang yang ingin diubah.")
            Return
        End If

        Dim hargaBeli As Decimal
        Dim hargaJual As Decimal

        If Not Decimal.TryParse(TextBoxHargaBeli2.Text.Replace("Rp ", "").Replace(".", "").Replace(",", "."), hargaBeli) Then
            MessageBox.Show("Harga beli tidak valid.")
            Return
        End If

        If Not Decimal.TryParse(TextBoxHargaBarang2.Text.Replace("Rp ", "").Replace(".", "").Replace(",", "."), hargaJual) Then
            MessageBox.Show("Harga jual tidak valid.")
            Return
        End If

        Using conn As New MySqlConnection(connString)
            Try
                conn.Open()

                ' SQL command to update the record
                Dim cmd As New MySqlCommand("UPDATE barang SET namabarang = @namabarang, stok = @stok, satuan = @satuan, jenisbarang = @jenisbarang, hargabeli = @hargabeli, hargajual = @hargajual WHERE kodebarang = @kodebarang", conn)

                ' Adding parameters
                cmd.Parameters.AddWithValue("@kodebarang", TextBoxKodeBarang2.Text)
                cmd.Parameters.AddWithValue("@namabarang", TextBoxNamaBarang2.Text)
                cmd.Parameters.AddWithValue("@stok", Convert.ToInt32(TextBoxStok2.Text))
                cmd.Parameters.AddWithValue("@satuan", ComboBoxSatuan2.SelectedItem.ToString())
                cmd.Parameters.AddWithValue("@jenisbarang", ComboBoxJenisBarang2.SelectedItem.ToString())
                cmd.Parameters.AddWithValue("@hargabeli", hargaBeli)
                cmd.Parameters.AddWithValue("@hargajual", hargaJual)

                ' Execute the command
                cmd.ExecuteNonQuery()
                MessageBox.Show("Data berhasil diupdate!")

                ' Panggil LoadData untuk memperbarui DataGridView
                LoadData()

                ' Mengosongkan TextBox setelah mengupdate data
                TextBoxKodeBarang2.Text = ""
                TextBoxNamaBarang2.Text = ""
                TextBoxStok2.Text = ""
                ComboBoxSatuan2.SelectedIndex = -1 ' Mengatur ComboBox ke tidak ada pilihan
                ComboBoxJenisBarang2.SelectedIndex = -1 ' Mengatur ComboBox ke tidak ada pilihan
                TextBoxHargaBeli2.Text = ""
                TextBoxHargaBarang2.Text = ""

            Catch ex As Exception
                MessageBox.Show("An error occurred: " & ex.Message)
            Finally
                conn.Close()
            End Try

            ButtonSimpan2.Enabled = True
            ButtonUbah2.Enabled = False
        End Using
    End Sub

    Private Sub ButtonHapus2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonHapus2.Click
        If String.IsNullOrEmpty(TextBoxKodeBarang2.Text) Then
            MessageBox.Show("Silakan pilih barang yang ingin dihapus.")
            Return
        End If

        ' Tampilkan dialog untuk meminta password
        Dim password As String = InputBox("Masukkan password untuk menghapus data:", "Verifikasi Password")

        If String.IsNullOrEmpty(password) Then
            Return
        End If

        ' Verifikasi password (ganti "your_password" dengan password yang benar)
        If password <> "pqkljs08" Then
            MessageBox.Show("Password salah. Data tidak dapat dihapus.")
            Return
        End If

        ' Jika password benar, lanjutkan untuk menghapus data
        Using conn As New MySqlConnection(connString)
            Try
                conn.Open()

                ' SQL command to delete the record
                Dim cmd As New MySqlCommand("DELETE FROM barang WHERE kodebarang = @kodebarang", conn)

                ' Adding parameters
                cmd.Parameters.AddWithValue("@kodebarang", TextBoxKodeBarang2.Text)

                ' Execute the command
                cmd.ExecuteNonQuery()
                MessageBox.Show("Data berhasil dihapus!")

                ' Panggil LoadData untuk memperbarui DataGridView
                LoadData()

                ' Mengosongkan TextBox setelah menghapus data
                TextBoxKodeBarang2.Text = ""
                TextBoxNamaBarang2.Text = ""
                TextBoxStok2.Text = ""
                ComboBoxSatuan2.SelectedIndex = -1
                ComboBoxJenisBarang2.SelectedIndex = -1
                TextBoxHargaBeli2.Text = ""
                TextBoxHargaBarang2.Text = ""

                ' Mengatur tombol kembali
                ButtonSimpan2.Enabled = True
                ButtonUbah2.Enabled = False

            Catch ex As Exception
                MessageBox.Show("An error occurred: " & ex.Message)
            Finally
                conn.Close()
            End Try
        End Using
    End Sub

    Private Sub DataGridBarang2_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridBarang2.CellContentClick
        If e.RowIndex >= 0 Then
            Dim row As DataGridViewRow = DataGridBarang2.Rows(e.RowIndex)

            ' Mengisi TextBox dan ComboBox dengan data dari baris yang dipilih
            TextBoxKodeBarang2.Text = row.Cells("kodebarang").Value.ToString()
            TextBoxNamaBarang2.Text = row.Cells("namabarang").Value.ToString()
            TextBoxStok2.Text = row.Cells("stok").Value.ToString()
            ComboBoxSatuan2.SelectedItem = row.Cells("satuan").Value.ToString()
            ComboBoxJenisBarang2.SelectedItem = row.Cells("jenisbarang").Value.ToString()
            TextBoxHargaBeli2.Text = row.Cells("hargabeli").Value.ToString()
            TextBoxHargaBarang2.Text = row.Cells("hargajual").Value.ToString()

            Dim hargabeli As Decimal = Convert.ToDecimal(row.Cells("hargabeli").Value)
            Dim hargajual As Decimal = Convert.ToDecimal(row.Cells("hargajual").Value)

            TextBoxHargaBeli2.Text = String.Format("Rp {0:N0}", hargabeli) ' Format menjadi Rp.100.000
            TextBoxHargaBarang2.Text = String.Format("Rp {0:N0}", hargajual) ' Format menjadi Rp.200.000

            ButtonSimpan2.Enabled = False
            ButtonUbah2.Enabled = True
        End If
    End Sub

    Private Sub DataGridBarang2_CellFormatting(ByVal sender As Object, ByVal e As DataGridViewCellFormattingEventArgs) Handles DataGridBarang2.CellFormatting
        ' Memformat kolom harga beli dan harga jual
        If DataGridBarang2.Columns(e.ColumnIndex).Name = "hargabeli" Or DataGridBarang2.Columns(e.ColumnIndex).Name = "hargajual" Then
            If e.Value IsNot Nothing Then
                Dim value As Decimal
                If Decimal.TryParse(e.Value.ToString(), value) Then
                    e.Value = String.Format("Rp {0:N0}", value) ' Format menjadi Rp 100.000
                    e.FormattingApplied = True
                End If
            End If
        End If
    End Sub

    Private Sub ButtonSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonSearch.Click
        ' Pastikan ada input dari TextBox
        If String.IsNullOrEmpty(TextBoxSearch.Text) Then
            LoadData()
            MessageBox.Show("Silakan masukkan kata kunci untuk pencarian.")
            Return
        End If

        ' Menggunakan DataView untuk memfilter DataGridView
        Dim dv As New DataView(table) ' dataTable adalah DataTable yang digunakan sebagai sumber data
        dv.RowFilter = String.Format("kodebarang LIKE '%{0}%' OR namabarang LIKE '%{0}%' OR jenisbarang LIKE '%{0}%'", TextBoxSearch.Text)

        ' Mengatur DataGridView untuk menampilkan hasil pencarian
        DataGridBarang2.DataSource = dv

        ' Jika tidak ada hasil pencarian
        If dv.Count = 0 Then
            MessageBox.Show("Tidak ada hasil yang ditemukan.")
        End If
    End Sub

    Private Sub ButtonGenerateBarcode_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonGenerateBarcode.Click
        ' Check if the input TextBox is empty
        If String.IsNullOrEmpty(TextBoxKodeBarang2.Text) Then
            MessageBox.Show("Silakan masukkan Kode Barang terlebih dahulu.")
            Return
        End If

        Dim data As String = TextBoxKodeBarang2.Text.Trim()

        ' Validate input for EAN-13: must be 12 or 13 digits numeric
        If Not System.Text.RegularExpressions.Regex.IsMatch(data, "^\d{12,13}$") Then
            MessageBox.Show("Kode Barang harus berupa 12 atau 13 digit angka untuk EAN-13.", "Input Tidak Valid", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' Create a BarcodeWriter instance for generating the barcode
        Dim barcodeWriter As New ZXing.BarcodeWriter()
        barcodeWriter.Format = ZXing.BarcodeFormat.EAN_13
        barcodeWriter.Options.Width = 300
        barcodeWriter.Options.Height = 150

        Try
            ' Generate the barcode bitmap
            Using barcodeBitmap As Bitmap = barcodeWriter.Write(data)
                ' Display the barcode in the PictureBox
                PictureBoxBarcode.Image = New Bitmap(barcodeBitmap)
            End Using

            MessageBox.Show("Barcode EAN-13 berhasil dibuat!")
        Catch ex As Exception
            MessageBox.Show("Kesalahan saat membuat barcode: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ButtonCetakBarcode_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonCetakBarcode.Click
        ' Check if there is a barcode image to save
        If PictureBoxBarcode.Image Is Nothing Then
            MessageBox.Show("Tidak ada barcode untuk dicetak.")
            Return
        End If

        ' Open a SaveFileDialog to allow user to choose the file name and location
        Using sfd As New SaveFileDialog()
            sfd.Filter = "PDF Files|*.pdf"
            sfd.Title = "Simpan Barcode sebagai PDF"
            If sfd.ShowDialog() = DialogResult.OK Then
                Try
                    ' Create a new document
                    Using document As New Document()
                        PdfWriter.GetInstance(document, New FileStream(sfd.FileName, FileMode.Create))
                        document.Open()

                        ' Add the barcode image to the PDF
                        Dim barcodeImage As iTextSharp.text.Image = iTextSharp.text.Image.GetInstance(PictureBoxBarcode.Image, System.Drawing.Imaging.ImageFormat.Png)
                        barcodeImage.ScaleToFit(document.PageSize.Width - 50, document.PageSize.Height - 50) ' Scale to fit
                        document.Add(barcodeImage)

                        MessageBox.Show("Barcode berhasil disimpan sebagai PDF!")
                    End Using
                Catch ex As Exception
                    MessageBox.Show("Terjadi kesalahan saat menyimpan file: " & ex.Message)
                End Try
            End If
        End Using
    End Sub


    ' Form Pelanggan

    Private Sub LoadDataPelanggan()
        Using conn As New MySqlConnection(connString)
            Try
                conn.Open()
                Dim cmd As New MySqlCommand("SELECT * FROM pelanggan", conn)
                Dim adapter As New MySqlDataAdapter(cmd)
                table2 = New DataTable()
                adapter.Fill(table2)

                DataGridPelanggan.DataSource = table2

                DataGridPelanggan.Sort(DataGridPelanggan.Columns("namapelanggan"), System.ComponentModel.ListSortDirection.Ascending)

                DataGridPelanggan.Columns(0).Width = 90
                DataGridPelanggan.Columns(1).Width = 260
                DataGridPelanggan.Columns(2).Width = 390
                DataGridPelanggan.Columns(3).Width = 81

            Catch ex As Exception
                MessageBox.Show("An error occurred: " & ex.Message)
            End Try
        End Using
    End Sub

    Private Function GenerateUniqueKodePelanggan(ByVal nohandphone As String) As String
        Dim random As New Random()
        Dim kodePelanggan As String = ""
        Dim isUnique As Boolean = False

        ' Pastikan nomor HP memiliki setidaknya 3 digit
        If nohandphone.Length < 3 Then
            Throw New ArgumentException("Nomor HP harus memiliki setidaknya 3 digit.")
        End If

        ' Ambil 3 angka terakhir dari nomor HP
        Dim lastThreeDigits As String = nohandphone.Substring(nohandphone.Length - 3)

        While Not isUnique
            ' Menghasilkan kode pelanggan dengan format "MP-XXXYY"
            kodePelanggan = "MP-" & lastThreeDigits

            ' Menambahkan 2 karakter random
            For i As Integer = 1 To 2
                Dim isLetter As Boolean = random.Next(0, 2) = 0
                If isLetter Then
                    ' Menambahkan huruf A-Z
                    kodePelanggan &= Chr(random.Next(65, 91)) ' ASCII A-Z
                Else
                    ' Menambahkan angka 0-9
                    kodePelanggan &= random.Next(0, 10).ToString()
                End If
            Next

            ' Cek apakah kode pelanggan sudah ada di database
            Using conn As New MySqlConnection(connString)
                conn.Open()
                Dim checkCmd As New MySqlCommand("SELECT COUNT(*) FROM pelanggan WHERE kodepelanggan = @kodepelanggan", conn)
                checkCmd.Parameters.AddWithValue("@kodepelanggan", kodePelanggan)

                Dim count As Integer = Convert.ToInt32(checkCmd.ExecuteScalar())
                If count = 0 Then
                    isUnique = True ' Kode pelanggan unik
                End If
            End Using
        End While

        Return kodePelanggan
    End Function

    Private Function IsDataExists2(ByVal noHandphone As String) As Boolean
        Using conn As New MySqlConnection(connString)
            conn.Open()
            Dim query As String = "SELECT COUNT(*) FROM pelanggan WHERE nohandphone = @nohandphone"
            Dim cmd As New MySqlCommand(query, conn)

            cmd.Parameters.AddWithValue("@nohandphone", noHandphone)

            Dim count As Integer = Convert.ToInt32(cmd.ExecuteScalar())
            Return count > 0 ' Mengembalikan true jika data sudah ada
        End Using
    End Function

    Private Sub ButtonSimpan3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonSimpan3.Click

        If String.IsNullOrEmpty(TextBoxNamaPelanggan3.Text) OrElse
            String.IsNullOrEmpty(TextBoxAlamatPelanggan3.Text) OrElse
            String.IsNullOrEmpty(TextBoxNoHp3.Text) Then
            MessageBox.Show("Silakan lengkapi semua kolom yang diperlukan.")
            Return
        End If

        Dim noHp As String = TextBoxNoHp3.Text.Trim()

        ' Memastikan nomor HP dimulai dengan "08"
        If Not noHp.StartsWith("08") Then
            MessageBox.Show("Nomor HP harus diawali dengan '08'.")
            Return
        End If

        ' Memastikan panjang nomor HP antara 11 hingga 13 karakter
        If noHp.Length < 11 OrElse noHp.Length > 13 Then
            MessageBox.Show("Nomor HP harus terdiri dari 11 hingga 13 digit.")
            Return
        End If

        If IsDataExists2(TextBoxNoHp3.Text) Then
            MessageBox.Show("Data tidak valid. Nomor HP sudah ada.")
            Return
        End If

        TextBoxKodePelanggan3.Text = GenerateUniqueKodePelanggan(noHp)

        Using conn As New MySqlConnection(connString)
            Try
                conn.Open()

                ' SQL command to insert new record
                Dim cmd As New MySqlCommand("INSERT INTO pelanggan (kodepelanggan, namapelanggan, alamat, nohandphone) VALUES (@kodepelanggan, @namapelanggan, @alamat, @nohandphone)", conn)

                ' Adding parameters
                cmd.Parameters.AddWithValue("@kodepelanggan", TextBoxKodePelanggan3.Text)
                cmd.Parameters.AddWithValue("@namapelanggan", TextBoxNamaPelanggan3.Text)
                cmd.Parameters.AddWithValue("@alamat", TextBoxAlamatPelanggan3.Text)
                cmd.Parameters.AddWithValue("@nohandphone", TextBoxNoHp3.Text)

                ' Execute the command
                cmd.ExecuteNonQuery()
                MessageBox.Show("Data berhasil disimpan!")

                LoadDataPelanggan()

                TextBoxKodePelanggan3.Text = ""
                TextBoxNamaPelanggan3.Text = ""
                TextBoxAlamatPelanggan3.Text = ""
                TextBoxNoHp3.Text = ""

            Catch ex As Exception
                MessageBox.Show("An error occurred: " & ex.Message)
            Finally
                conn.Close()
            End Try

            ButtonSimpan2.Enabled = True
            ButtonUbah2.Enabled = False
        End Using
    End Sub

    Private Sub DataGridPelanggan_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridPelanggan.CellContentClick
        If e.RowIndex >= 0 Then
            Dim row As DataGridViewRow = DataGridPelanggan.Rows(e.RowIndex)

            ' Mengisi TextBox dan ComboBox dengan data dari baris yang dipilih
            TextBoxKodePelanggan3.Text = row.Cells("kodepelanggan").Value.ToString()
            TextBoxNamaPelanggan3.Text = row.Cells("namapelanggan").Value.ToString()
            TextBoxAlamatPelanggan3.Text = row.Cells("alamat").Value.ToString()
            TextBoxNoHp3.Text = row.Cells("nohandphone").Value.ToString()

            ButtonSimpan3.Enabled = False
            ButtonUbah3.Enabled = True
        End If
    End Sub

    Private Sub ButtonUbah3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonUbah3.Click

        If String.IsNullOrEmpty(TextBoxKodePelanggan3.Text) Then
            MessageBox.Show("Silakan pilih data pelanggan yang ingin diubah.")
            Return
        End If

        Using conn As New MySqlConnection(connString)
            Try
                conn.Open()

                ' SQL command to update the record
                Dim cmd As New MySqlCommand("UPDATE pelanggan SET namapelanggan = @namapelanggan, alamat = @alamat, nohandphone = @nohandphone WHERE kodepelanggan = @kodepelanggan", conn)

                ' Adding parameters
                cmd.Parameters.AddWithValue("@kodepelanggan", TextBoxKodePelanggan3.Text)
                cmd.Parameters.AddWithValue("@namapelanggan", TextBoxNamaPelanggan3.Text)
                cmd.Parameters.AddWithValue("@alamat", TextBoxAlamatPelanggan3.Text)
                cmd.Parameters.AddWithValue("@nohandphone", TextBoxNoHp3.Text)

                ' Execute the command
                cmd.ExecuteNonQuery()
                MessageBox.Show("Data berhasil diupdate!")

                ' Panggil LoadData untuk memperbarui DataGridView
                LoadDataPelanggan()

                ' Mengosongkan TextBox setelah mengupdate data
                TextBoxKodePelanggan3.Text = ""
                TextBoxNamaPelanggan3.Text = ""
                TextBoxAlamatPelanggan3.Text = ""
                TextBoxNoHp3.Text = ""

            Catch ex As Exception
                MessageBox.Show("An error occurred: " & ex.Message)
            Finally
                conn.Close()
            End Try

            ButtonSimpan3.Enabled = True
            ButtonUbah3.Enabled = False
        End Using
    End Sub

    Private Sub ButtonHapus3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonHapus3.Click
        If String.IsNullOrEmpty(TextBoxKodePelanggan3.Text) Then
            MessageBox.Show("Silakan pilih data pelanggan yang ingin dihapus.")
            Return
        End If

        ' Tampilkan dialog untuk meminta password
        Dim password As String = InputBox("Masukkan password untuk menghapus data:", "Verifikasi Password")

        If String.IsNullOrEmpty(password) Then
            Return
        End If

        ' Verifikasi password (ganti "your_password" dengan password yang benar)
        If password <> "pqkljs08" Then
            MessageBox.Show("Password salah. Data tidak dapat dihapus.")
            Return
        End If

        ' Jika password benar, lanjutkan untuk menghapus data
        Using conn As New MySqlConnection(connString)
            Try
                conn.Open()

                ' SQL command to delete the record
                Dim cmd As New MySqlCommand("DELETE FROM pelanggan WHERE kodepelanggan = @kodepelanggan", conn)

                ' Adding parameters
                cmd.Parameters.AddWithValue("@kodepelanggan", TextBoxKodePelanggan3.Text)

                ' Execute the command
                cmd.ExecuteNonQuery()
                MessageBox.Show("Data berhasil dihapus!")

                ' Panggil LoadData untuk memperbarui DataGridView
                LoadDataPelanggan()

                ' Mengosongkan TextBox setelah menghapus data
                TextBoxKodePelanggan3.Text = ""
                TextBoxNamaPelanggan3.Text = ""
                TextBoxAlamatPelanggan3.Text = ""
                TextBoxNoHp3.Text = ""

                ' Mengatur tombol kembali
                ButtonSimpan3.Enabled = True
                ButtonUbah3.Enabled = False

            Catch ex As Exception
                MessageBox.Show("An error occurred: " & ex.Message)
            Finally
                conn.Close()
            End Try
        End Using
    End Sub

    Private Sub ButtonSearch3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonSearch3.Click
        If String.IsNullOrEmpty(TextBoxSearch3.Text) Then
            LoadDataPelanggan()
            MessageBox.Show("Silakan masukkan kata kunci untuk pencarian.")
            Return
        End If

        ' Menggunakan DataView untuk memfilter DataGridView
        Dim dv As New DataView(table2) ' dataTable adalah DataTable yang digunakan sebagai sumber data
        dv.RowFilter = String.Format("kodepelanggan LIKE '%{0}%' OR namapelanggan LIKE '%{0}%' OR nohandphone LIKE '%{0}%'", TextBoxSearch3.Text)

        ' Mengatur DataGridView untuk menampilkan hasil pencarian
        DataGridPelanggan.DataSource = dv

        ' Jika tidak ada hasil pencarian
        If dv.Count = 0 Then
            MessageBox.Show("Tidak ada hasil yang ditemukan.")
        End If
    End Sub

    Private Sub TextBoxNoHp3_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBoxNoHp3.KeyPress
        If Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsDigit(e.KeyChar) AndAlso e.KeyChar <> "." AndAlso e.KeyChar <> "," Then
            e.Handled = True
        End If
    End Sub




    ' Form Transaksi

    Private Function GenerateTransactionCode() As String
        Dim companyPrefix As String = "EIF" ' Singkatan untuk EIF
        Dim transactionDate As String = DateTime.Now.ToString("ddMMyy") ' Format tanggal
        Dim customerType As String = If(RadioButtonPelanggan.Checked, "MP", "NP") ' MP untuk pelanggan, NP untuk non-pelanggan

        Dim random As New Random()
        Dim randomNumber As Integer

        ' Menghasilkan angka acak yang unik
        Do
            randomNumber = random.Next(1, 1000) ' Angka acak antara 1 dan 999
        Loop While usedTransactionNumbers.Contains(randomNumber) ' Cek apakah angka sudah digunakan

        ' Menambahkan angka yang baru dihasilkan ke dalam HashSet
        usedTransactionNumbers.Add(randomNumber)

        Dim formattedTransactionNumber As String = randomNumber.ToString("D3") ' Format nomor transaksi dengan leading zero

        ' Gabungkan semua bagian untuk membentuk kode transaksi
        Dim transactionCode As String = String.Format("{0}-{1}-{2}-{3}", companyPrefix, transactionDate, customerType, formattedTransactionNumber)
        Return transactionCode
    End Function

    Private Sub GetDataByKodeBarang(ByVal kode As String)
        Using conn As New MySqlConnection(connString)
            Dim command As New MySqlCommand("SELECT * FROM barang WHERE kodebarang = @kodebarang", conn)
            command.Parameters.AddWithValue("@kodebarang", kode)

            Try
                conn.Open()
                Dim reader As MySqlDataReader = command.ExecuteReader()
                If reader.Read() Then
                    ' Mengisi TextBox dan kontrol lain dengan data dari database
                    TextBoxNamaBarang1.Text = reader("namabarang").ToString()
                    TextBoxHargaBarang1.Text = Convert.ToDecimal(reader("hargajual")).ToString("N0")
                    stokBarang = Convert.ToInt32(reader("stok"))
                    ' Anda bisa menambahkan lebih banyak logika untuk mengambil data lain jika diperlukan
                Else
                    ' Jika tidak ditemukan, kosongkan kontrol
                    TextBoxNamaBarang1.Text = ""
                    TextBoxHargaBarang1.Text = ""
                    stokBarang = 0
                End If
            Catch ex As Exception
                MessageBox.Show("Error: " & ex.Message)
            End Try
        End Using
    End Sub

    Private Sub GetDataByKodePelanggan(ByVal kodepl As String)
        Using conn As New MySqlConnection(connString)
            Dim command As New MySqlCommand("SELECT * FROM pelanggan WHERE kodepelanggan = @kodepelanggan", conn)
            command.Parameters.AddWithValue("@kodepelanggan", kodepl)

            Try
                conn.Open()
                Dim reader As MySqlDataReader = command.ExecuteReader()
                If reader.Read() Then
                    ' Mengisi TextBox dan kontrol lain dengan data dari database
                    TextBoxNamaPelanggan1.Text = reader("namapelanggan").ToString()

                    ' Anda bisa menambahkan lebih banyak logika untuk mengambil data lain jika diperlukan
                Else
                    ' Jika tidak ditemukan, kosongkan kontrol
                    TextBoxKodePelanggan1.Text = ""
                End If
            Catch ex As Exception
                MessageBox.Show("Error: " & ex.Message)
            End Try
        End Using
    End Sub

    Private Sub GetDataByNamaBarang(ByVal nama As String)
        Using conn As New MySqlConnection(connString)
            Dim command As New MySqlCommand("SELECT * FROM barang WHERE namabarang LIKE @namabarang", conn)
            command.Parameters.AddWithValue("@namabarang", "%" & nama & "%")

            Try
                conn.Open()
                Dim reader As MySqlDataReader = command.ExecuteReader()
                If reader.Read() Then
                    ' Mengisi TextBox dan kontrol lain dengan data dari database
                    TextBoxKodeBarang1.Text = reader("kodebarang").ToString()
                    TextBoxHargaBarang1.Text = reader("hargajual").ToString()
                    stokBarang = Convert.ToInt32(reader("stok"))
                    ' Anda bisa menambahkan lebih banyak logika untuk mengambil data lain jika diperlukan
                Else
                    ' Jika tidak ditemukan, kosongkan kontrol
                    TextBoxKodeBarang1.Text = ""
                    TextBoxHargaBarang1.Text = ""
                    stokBarang = 0
                End If
            Catch ex As Exception
                MessageBox.Show("Error: " & ex.Message)
            End Try
        End Using
    End Sub

    Private Sub TextBoxKodeBarang1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBoxKodeBarang1.TextChanged
        If Not String.IsNullOrEmpty(TextBoxKodeBarang1.Text) Then
            GetDataByKodeBarang(TextBoxKodeBarang1.Text)
        Else
            ' Kosongkan kontrol jika tidak ada teks
            TextBoxNamaBarang1.Text = ""
            TextBoxHargaBarang1.Text = ""
            stokBarang = 0
        End If
    End Sub

    Private Sub TextBoxKodePelanggan1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBoxKodePelanggan1.TextChanged
        ' Hanya panggil GetDataByKodePelanggan jika kode tidak dihasilkan secara otomatis
        If Not isGeneratingCode Then
            If Not String.IsNullOrEmpty(TextBoxKodePelanggan1.Text) Then
                GetDataByKodePelanggan(TextBoxKodePelanggan1.Text)
            Else
                ' Kosongkan kontrol jika tidak ada teks
                TextBoxNamaPelanggan1.Text = ""
            End If
        End If
    End Sub

    Private Sub TextBoxNamaBarang1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBoxNamaBarang1.TextChanged
        If Not String.IsNullOrEmpty(TextBoxNamaBarang1.Text) Then
            GetDataByNamaBarang(TextBoxNamaBarang1.Text)
        Else
            ' Kosongkan kontrol jika tidak ada teks
            TextBoxNamaBarang1.Text = ""
            TextBoxHargaBarang1.Text = ""
            stokBarang = 0
        End If
    End Sub

    Private Sub UpdateTotal()
        Dim totalJumlah As Integer = 0
        Dim totalHarga As Decimal = 0

        ' Iterasi melalui setiap baris di DataGridView
        For Each row As DataGridViewRow In DataGridViewBarang1.Rows
            If Not row.IsNewRow Then ' Pastikan bukan baris baru
                Dim jumlah As Integer
                Dim subtotal As Decimal

                ' Ambil nilai jumlah dan subtotal dari baris
                If Integer.TryParse(row.Cells("Jumlah").Value.ToString(), jumlah) Then
                    totalJumlah += jumlah
                End If

                If Decimal.TryParse(row.Cells("Subtotal").Value.ToString(), subtotal) Then
                    totalHarga += subtotal
                End If
            End If
        Next

        ' Update TextBox dengan total jumlah dan total harga
        TextBoxTotalBarang1.Text = totalJumlah.ToString()
        TextBoxTotalHarga1.Text = totalHarga.ToString("N0") ' Format sebagai angka dengan pemisah ribuan

        Dim diskon As Decimal
        If Decimal.TryParse(TextBoxDiskon1.Text, diskon) Then
            ' Pastikan diskon tidak lebih dari 100%
            If diskon < 0 Then
                diskon = 0
            ElseIf diskon > 100 Then
                diskon = 100
            End If
        Else
            diskon = 0 ' Jika diskon tidak valid, set ke 0
        End If

        ' Hitung total bayar setelah diskon
        Dim totalBayar As Decimal = totalHarga - (totalHarga * (diskon / 100))
        TextBoxTotalBayar1.Text = totalBayar.ToString("N0") ' Format sebagai angka dengan pemisah ribuan

        ' Hitung Kembalian
        Dim dibayar As Decimal
        If Decimal.TryParse(TextBoxDibayar1.Text, dibayar) Then
            If dibayar < totalBayar Then
                MessageBox.Show("Jumlah yang dibayar kurang dari total bayar. Silakan masukkan ulang jumlah yang dibayar.", "Input Ulang", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                TextBoxDibayar1.Focus() ' Kembali ke TextBoxDibayar1 untuk input ulang
                TextBoxDibayar1.Clear() ' Kosongkan TextBox untuk input ulang
                TextBoxKembalian1.Text = "0" ' Reset kembalian
                Return ' Keluar dari metode
            Else
                Dim kembalian As Decimal = dibayar - totalBayar
                TextBoxKembalian1.Text = kembalian.ToString("N0") ' Format sebagai angka dengan pemisah ribuan
            End If
        Else
            TextBoxKembalian1.Text = "0" ' Jika input tidak valid, reset kembalian
        End If
    End Sub

    Private Sub DataGridViewBarang1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridViewBarang1.CellContentClick
        If e.RowIndex >= 0 Then
            Dim row As DataGridViewRow = DataGridViewBarang1.Rows(e.RowIndex)

            ' Mengisi TextBox dengan data dari baris yang dipilih
            TextBoxKodeBarang1.Text = row.Cells("KodeBarang").Value.ToString()
            TextBoxNamaBarang1.Text = row.Cells("NamaBarang").Value.ToString()
            TextBoxHargaBarang1.Text = row.Cells("HargaBarang").Value.ToString()
            TextBoxJumlahBarang1.Text = row.Cells("Jumlah").Value.ToString()
            TextBoxSubtotalBarang1.Text = row.Cells("Subtotal").Value.ToString()


            ' Mengaktifkan tombol Ubah
            ButtonPilih.Enabled = False
            ButtonUbah1.Enabled = True
            ButtonHapus1.Enabled = True
        End If
    End Sub

    Private Sub ButtonPilih_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonPilih.Click

        If String.IsNullOrEmpty(TextBoxKodeBarang1.Text) OrElse String.IsNullOrEmpty(TextBoxNamaBarang1.Text) OrElse String.IsNullOrEmpty(TextBoxHargaBarang1.Text) OrElse String.IsNullOrEmpty(TextBoxJumlahBarang1.Text) Then
            MessageBox.Show("Semua field harus diisi sebelum menambahkan data.")
            Return
        End If

        ' Ambil kode barang yang baru
        Dim newKodeBarang As String = TextBoxKodeBarang1.Text
        Dim isDuplicate As Boolean = False

        ' Cek apakah kode barang sudah ada di DataGridView
        For Each row As DataGridViewRow In DataGridViewBarang1.Rows
            If row.Cells(0).Value IsNot Nothing AndAlso row.Cells(0).Value.ToString() = newKodeBarang Then
                isDuplicate = True
                Exit For
            End If
        Next

        ' Jika tidak ada duplikasi, tambahkan data baru
        If Not isDuplicate Then
            ' Membuat baris baru di DataGridView
            Dim rowData As String() = New String() {newKodeBarang, TextBoxNamaBarang1.Text, TextBoxHargaBarang1.Text, TextBoxJumlahBarang1.Text, TextBoxSubtotalBarang1.Text}
            DataGridViewBarang1.Rows.Add(rowData)

            ' Mengosongkan TextBox setelah data ditambahkan
            TextBoxKodeBarang1.Clear()
            TextBoxNamaBarang1.Clear()
            TextBoxHargaBarang1.Clear()
            TextBoxJumlahBarang1.Clear()
            TextBoxSubtotalBarang1.Clear()
        Else
            ' Jika ada duplikasi, tampilkan pesan
            MessageBox.Show("Data dengan kode barang ini sudah ada. Silakan masukkan kode barang yang berbeda.")
            TextBoxKodeBarang1.Clear()
            TextBoxNamaBarang1.Clear()
            TextBoxHargaBarang1.Clear()
            TextBoxJumlahBarang1.Clear()
            TextBoxSubtotalBarang1.Clear()
        End If
        UpdateTotal()
    End Sub

    Private Sub ButtonUbah1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonUbah1.Click
        If String.IsNullOrEmpty(TextBoxKodeBarang1.Text) OrElse String.IsNullOrEmpty(TextBoxNamaBarang1.Text) OrElse String.IsNullOrEmpty(TextBoxHargaBarang1.Text) OrElse String.IsNullOrEmpty(TextBoxJumlahBarang1.Text) Then
            MessageBox.Show("Semua field harus diisi sebelum mengubah data.")
            Return
        End If

        ' Mengupdate baris yang dipilih di DataGridView
        Dim selectedRowIndex As Integer = DataGridViewBarang1.CurrentCell.RowIndex
        DataGridViewBarang1.Rows(selectedRowIndex).Cells("KodeBarang").Value = TextBoxKodeBarang1.Text
        DataGridViewBarang1.Rows(selectedRowIndex).Cells("NamaBarang").Value = TextBoxNamaBarang1.Text
        DataGridViewBarang1.Rows(selectedRowIndex).Cells("HargaBarang").Value = TextBoxHargaBarang1.Text
        DataGridViewBarang1.Rows(selectedRowIndex).Cells("Jumlah").Value = TextBoxJumlahBarang1.Text
        DataGridViewBarang1.Rows(selectedRowIndex).Cells("Subtotal").Value = TextBoxSubtotalBarang1.Text

        ' Mengosongkan TextBox setelah data diubah
        TextBoxKodeBarang1.Clear()
        TextBoxNamaBarang1.Clear()
        TextBoxHargaBarang1.Clear()
        TextBoxJumlahBarang1.Clear()
        TextBoxSubtotalBarang1.Clear()

        ' Menonaktifkan tombol Ubah
        ButtonPilih.Enabled = True
        ButtonUbah1.Enabled = False
        ButtonHapus1.Enabled = False

        UpdateTotal()
    End Sub

    Private Sub ButtonHapus1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonHapus1.Click
        If DataGridViewBarang1.CurrentRow IsNot Nothing Then
            Dim result As DialogResult = MessageBox.Show("Apakah Anda yakin ingin menghapus data ini?", "Konfirmasi Hapus", MessageBoxButtons.YesNo)
            If result = DialogResult.Yes Then
                ' Menghapus baris yang dipilih di DataGridView
                DataGridViewBarang1.Rows.RemoveAt(DataGridViewBarang1.CurrentRow.Index)

                ' Mengosongkan TextBox setelah data dihapus
                TextBoxKodeBarang1.Clear()
                TextBoxNamaBarang1.Clear()
                TextBoxHargaBarang1.Clear()
                TextBoxJumlahBarang1.Clear()
                TextBoxSubtotalBarang1.Clear()

                ' Menonaktifkan tombol Ubah
                ButtonUbah1.Enabled = False
                ButtonHapus1.Enabled = False
            End If
        End If

        UpdateTotal()
    End Sub

    Private Sub TextBoxJumlahBarang1_KeyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs) Handles TextBoxJumlahBarang1.KeyPress
        ' Hanya izinkan angka, titik, dan koma
        If Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsDigit(e.KeyChar) AndAlso e.KeyChar <> "." AndAlso e.KeyChar <> "," Then
            e.Handled = True
        End If
    End Sub

    Private Sub TextBoxJumlahBarang1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBoxJumlahBarang1.TextChanged
        Dim jumlah As Integer
        Dim harga As Decimal

        ' Cek apakah jumlah barang valid
        If Integer.TryParse(TextBoxJumlahBarang1.Text, jumlah) AndAlso jumlah > 0 Then
            ' Cek apakah harga barang valid
            If Decimal.TryParse(TextBoxHargaBarang1.Text, harga) Then
                ' Validasi jumlah tidak melebihi stok
                If jumlah > stokBarang Then
                    MessageBox.Show("Jumlah tidak boleh melebihi stok yang tersedia: " & stokBarang)
                    TextBoxJumlahBarang1.Text = stokBarang.ToString() ' Set jumlah ke stok maksimum
                    TextBoxJumlahBarang1.SelectionStart = TextBoxJumlahBarang1.Text.Length ' Pindahkan kursor ke akhir
                    Return
                End If

                ' Hitung subtotal
                Dim subtotal As Decimal = jumlah * harga
                TextBoxSubtotalBarang1.Text = subtotal.ToString("N0") ' Format sebagai angka dengan pemisah ribuan
            Else
                TextBoxSubtotalBarang1.Text = "0" ' Jika harga tidak valid, set subtotal ke 0
            End If
        Else
            TextBoxSubtotalBarang1.Text = "0" ' Jika jumlah tidak valid, set subtotal ke 0
        End If
    End Sub

    Private Sub RadioButtonPelanggan_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButtonPelanggan.CheckedChanged
        If RadioButtonPelanggan.Checked Then
            TextBoxKodePelanggan1.ReadOnly = False ' Aktifkan TextBoxKodePelanggan1
            RadioButtonNonPelanggan.Checked = False ' Nonaktifkan RadioButton Non Pelanggan
            TextBoxKodePelanggan1.Text = ""
            TextBoxNamaPelanggan1.Text = ""
        End If
    End Sub

    Private Sub RadioButtonNonPelanggan_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButtonNonPelanggan.CheckedChanged
        If RadioButtonNonPelanggan.Checked Then
            TextBoxKodePelanggan1.ReadOnly = True ' Nonaktifkan TextBoxKodePelanggan1
            RadioButtonPelanggan.Checked = False ' Nonaktifkan RadioButton Pelanggan

            ' Set flag untuk menunjukkan bahwa kode sedang dihasilkan
            TextBoxKodePelanggan1.Text = "NP-CASH"
            TextBoxNamaPelanggan1.Text = "NonPelanggan"
        End If
    End Sub

    Private Sub TextBoxDiskon1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBoxDiskon1.TextChanged
        UpdateTotal()
    End Sub

    Private Sub ButtonKonfirmasi1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonKonfirmasi1.Click
        UpdateTotal()
        TextBoxNoTransaksi.Text = GenerateTransactionCode()
    End Sub

    Private Sub PrintReceipt(ByVal kodeTransaksi As String, ByVal tanggal As DateTime, ByVal tipepelanggan As String, ByVal kodepelanggan As String, ByVal namaPelanggan As String, ByVal jumlahBarang As Integer, ByVal totalHarga As Decimal, ByVal totalBayar As Decimal, ByVal dibayar As Decimal, ByVal kembalian As Decimal)
        ' Membuat PDF untuk nota
        Dim saveFileDialog As New SaveFileDialog()
        saveFileDialog.Filter = "PDF Files (*.pdf)|*.pdf"
        saveFileDialog.Title = "Simpan Nota"
        saveFileDialog.FileName = "Nota_" & kodeTransaksi & ".pdf"

        If saveFileDialog.ShowDialog() = DialogResult.OK Then
            Using fs As New FileStream(saveFileDialog.FileName, FileMode.Create, FileAccess.Write, FileShare.None)
                Dim document As New Document(PageSize.A5.Rotate(), 5, 5, 5, 5)
                PdfWriter.GetInstance(document, fs)
                document.Open()

                ' Header
                document.Add(New Paragraph("             EIFFEL PETSHOP             ") With {.Alignment = Element.ALIGN_CENTER})
                document.Add(New Paragraph("==================================================================================="))

                ' Transaction Information
                Dim transactionTable As New PdfPTable(3)
                transactionTable.SetWidths(New Single() {30.0F, 50.0F, 20.0F})
                transactionTable.WidthPercentage = 100
                transactionTable.DefaultCell.Border = PdfPCell.NO_BORDER

                transactionTable.AddCell("Kode Transaksi")
                transactionTable.AddCell(": " & kodeTransaksi)
                transactionTable.AddCell(tanggal.ToString("dd/MM/yyyy HH:mm:ss"))

                document.Add(transactionTable)

                document.Add(New Paragraph("==================================================================================="))

                ' Customer Information
                Dim customerTable As New PdfPTable(2)
                customerTable.SetWidths(New Single() {30.0F, 70.0F})
                customerTable.WidthPercentage = 100
                customerTable.DefaultCell.Border = PdfPCell.NO_BORDER

                customerTable.AddCell("Pelanggan")
                customerTable.AddCell(": " & tipepelanggan)
                customerTable.AddCell("Kode Pelanggan")
                customerTable.AddCell(": " & kodepelanggan)
                customerTable.AddCell("Nama Pelanggan")
                customerTable.AddCell(": " & namaPelanggan)

                document.Add(customerTable)

                ' Rincian Barang in a structured table
                document.Add(New Paragraph(" Rincian Barang:"))
                document.Add(New Paragraph(" "))

                Dim rincianTable As New PdfPTable(4)
                rincianTable.SetWidths(New Single() {50.0F, 10.0F, 20.0F, 20.0F})
                rincianTable.WidthPercentage = 100
                rincianTable.AddCell(CreateCell("Nama Barang"))
                rincianTable.AddCell(CreateCell("Jumlah"))
                rincianTable.AddCell(CreateCell("Harga Satuan"))
                rincianTable.AddCell(CreateCell("Subtotal"))

                ' Adding product details from DataGridView
                For Each row As DataGridViewRow In DataGridViewBarang1.Rows
                    If Not row.IsNewRow Then
                        Dim namaBarang As String = row.Cells("NamaBarang").Value.ToString()
                        Dim jumlah As Integer = Convert.ToInt32(row.Cells("Jumlah").Value)
                        Dim harga As Decimal = Convert.ToDecimal(row.Cells("HargaBarang").Value)
                        Dim subtotal As Decimal = Convert.ToDecimal(row.Cells("Subtotal").Value)

                        rincianTable.AddCell(CreateCell(namaBarang))
                        rincianTable.AddCell(CreateCell(jumlah.ToString()))
                        rincianTable.AddCell(CreateCell(harga.ToString("C")))
                        rincianTable.AddCell(CreateCell(subtotal.ToString("C")))
                    End If
                Next

                document.Add(rincianTable)

                ' Summary
                document.Add(New Paragraph("==================================================================================="))

                Dim summaryTable As New PdfPTable(2)
                summaryTable.SetWidths(New Single() {30.0F, 70.0F})
                summaryTable.WidthPercentage = 100
                summaryTable.DefaultCell.Border = PdfPCell.NO_BORDER

                summaryTable.AddCell("Jumlah Barang")
                summaryTable.AddCell(": " & jumlahBarang.ToString())
                summaryTable.AddCell("Total Harga")
                summaryTable.AddCell(": " & totalHarga.ToString("C"))
                summaryTable.AddCell("Total Dibayar")
                summaryTable.AddCell(": " & totalBayar.ToString("C"))
                summaryTable.AddCell("Dibayar")
                summaryTable.AddCell(": " & dibayar.ToString("C"))
                summaryTable.AddCell("Kembalian")
                summaryTable.AddCell(": " & kembalian.ToString("C"))

                document.Add(summaryTable)

                document.Add(New Paragraph("==================================================================================="))
                Dim tandatanganTable As New PdfPTable(4)
                tandatanganTable.SetWidths(New Single() {5.0F, 25.0F, 45.0F, 25.0F})
                tandatanganTable.WidthPercentage = 100
                tandatanganTable.DefaultCell.Border = PdfPCell.NO_BORDER

                tandatanganTable.AddCell("   ")
                tandatanganTable.AddCell("Penerima,")
                tandatanganTable.AddCell("   ")
                tandatanganTable.AddCell("Hormat Kami,")
                document.Add(tandatanganTable)


                document.Add(New Paragraph("                                                                                   "))
                document.Add(New Paragraph("                                                                                   "))

                Dim tandatangan2Table As New PdfPTable(4)
                tandatangan2Table.SetWidths(New Single() {5.0F, 25.0F, 45.0F, 25.0F})
                tandatangan2Table.WidthPercentage = 100
                tandatangan2Table.DefaultCell.Border = PdfPCell.NO_BORDER

                tandatangan2Table.AddCell("   ")
                tandatangan2Table.AddCell("______________")
                tandatangan2Table.AddCell("   ")
                tandatangan2Table.AddCell("________________")
                document.Add(tandatangan2Table)

                document.Close()
            End Using

            MessageBox.Show("Nota berhasil dicetak ke PDF.")
        End If
    End Sub

    Private Function CreateCell(ByVal text As String) As PdfPCell
        Dim cell As New PdfPCell(New Phrase(text))
        cell.MinimumHeight = 20 ' Adjust this value as necessary
        Return cell
    End Function

    Private Sub ButtonSimpan1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonSimpan1.Click
        If String.IsNullOrEmpty(TextBoxNoTransaksi.Text) OrElse
            String.IsNullOrEmpty(TextBoxTotalBarang1.Text) OrElse
            String.IsNullOrEmpty(TextBoxTotalBayar1.Text) OrElse
            String.IsNullOrEmpty(TextBoxDibayar1.Text) OrElse
            String.IsNullOrEmpty(TextBoxKembalian1.Text) Then
            MessageBox.Show("Semua field harus diisi sebelum menyimpan data.")
            Return
        End If

        Dim kodeTransaksi As String = TextBoxNoTransaksi.Text
        Dim tanggal As DateTime = DateTime.Now

        ' Ambil UserID dan Username dari pengguna yang login
        ' Ganti dengan cara nyata sesuai implementasi autentikasi aplikasi Anda
        Dim userId As String = CurrentUser.UserId  ' Contoh: Variabel global atau Singleton
        Dim kasir As String = CurrentUser.Username

        Dim tipepelanggan As String
        Dim kodepelanggan As String = TextBoxKodePelanggan1.Text
        Dim namaPelanggan As String = TextBoxNamaPelanggan1.Text
        If RadioButtonPelanggan.Checked Then
            tipepelanggan = "Pelanggan"
        Else
            tipepelanggan = "Non-Pelanggan"
        End If

        Dim jumlahBarang As Integer = Convert.ToInt32(TextBoxTotalBarang1.Text)
        Dim totalHarga As Decimal = Convert.ToDecimal(TextBoxTotalHarga1.Text)
        Dim totalBayar As Decimal = Convert.ToDecimal(TextBoxTotalBayar1.Text)
        Dim dibayar As Decimal = Convert.ToDecimal(TextBoxDibayar1.Text)
        Dim kembalian As Decimal = Convert.ToDecimal(TextBoxKembalian1.Text)

        Dim result As DialogResult = MessageBox.Show("Apakah Anda ingin mencetak nota?", "Cetak Nota", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        Using conn As New MySqlConnection(connString)
            conn.Open()
            Dim transaction As MySqlTransaction = conn.BeginTransaction()

            Try
                Dim command As New MySqlCommand("INSERT INTO transaksi (kodetransaksi, tanggal, UserID, Username, tipepelanggan, kodepelanggan, namapelanggan, jumlahbarang, totalharga, totalbayar, dibayar, kembalian) " &
                                                "VALUES (@kodetransaksi, @tanggal, @UserID, @Username, @tipepelanggan, @kodepelanggan, @namapelanggan, @jumlahbarang, @totalharga, @totalbayar, @dibayar, @kembalian)", conn, transaction)

                command.Parameters.AddWithValue("@kodetransaksi", kodeTransaksi)
                command.Parameters.AddWithValue("@tanggal", tanggal)
                command.Parameters.AddWithValue("@UserID", userId)
                command.Parameters.AddWithValue("@Username", kasir)
                command.Parameters.AddWithValue("@tipepelanggan", tipepelanggan)
                command.Parameters.AddWithValue("@kodepelanggan", kodepelanggan)
                command.Parameters.AddWithValue("@namapelanggan", namaPelanggan)
                command.Parameters.AddWithValue("@jumlahbarang", jumlahBarang)
                command.Parameters.AddWithValue("@totalharga", totalHarga)
                command.Parameters.AddWithValue("@totalbayar", totalBayar)
                command.Parameters.AddWithValue("@dibayar", dibayar)
                command.Parameters.AddWithValue("@kembalian", kembalian)

                command.ExecuteNonQuery()

                ' Insert data detail barang transaksi
                For Each row As DataGridViewRow In DataGridViewBarang1.Rows
                    If Not row.IsNewRow Then
                        Dim kodeBarang As String = row.Cells("KodeBarang").Value.ToString()
                        Dim jumlah As Integer = Convert.ToInt32(row.Cells("Jumlah").Value)
                        Dim namaBarang As String = row.Cells("NamaBarang").Value.ToString()
                        Dim hargaBarang As Decimal = Convert.ToDecimal(row.Cells("HargaBarang").Value)
                        Dim subtotal As Decimal = Convert.ToDecimal(row.Cells("Subtotal").Value)

                        Dim commandDetail As New MySqlCommand("INSERT INTO barangtransaksi (kodetransaksi, kodebarang, namabarang, hargabarang, jumlahbarang, subtotal) " &
                                                              "VALUES (@kodetransaksi, @kodebarang, @namabarang, @hargabarang, @jumlahbarang, @subtotal)", conn, transaction)
                        commandDetail.Parameters.AddWithValue("@kodetransaksi", kodeTransaksi)
                        commandDetail.Parameters.AddWithValue("@kodebarang", kodeBarang)
                        commandDetail.Parameters.AddWithValue("@namabarang", namaBarang)
                        commandDetail.Parameters.AddWithValue("@hargabarang", hargaBarang)
                        commandDetail.Parameters.AddWithValue("@jumlahbarang", jumlah)
                        commandDetail.Parameters.AddWithValue("@subtotal", subtotal)

                        commandDetail.ExecuteNonQuery()

                        ' Update stok barang
                        Dim commandUpdateStock As New MySqlCommand("UPDATE barang SET stok = stok - @jumlahbarang WHERE kodebarang = @kodebarang", conn, transaction)
                        commandUpdateStock.Parameters.AddWithValue("@jumlahbarang", jumlah)
                        commandUpdateStock.Parameters.AddWithValue("@kodebarang", kodeBarang)
                        commandUpdateStock.ExecuteNonQuery()
                    End If
                Next

                If result = DialogResult.Yes Then
                    PrintReceipt(kodeTransaksi, tanggal, tipepelanggan, kodepelanggan, namaPelanggan, jumlahBarang, totalHarga, totalBayar, dibayar, kembalian)
                End If

                LoadData()
                transaction.Commit()
                MessageBox.Show("Data transaksi berhasil disimpan.")

                ' Bersihkan isi form
                TextBoxNoTransaksi.Clear()
                TextBoxKodePelanggan1.Clear()
                TextBoxKodeBarang1.Clear()
                TextBoxNamaPelanggan1.Clear()
                TextBoxNamaBarang1.Clear()
                TextBoxHargaBarang1.Clear()
                TextBoxSubtotalBarang1.Clear()
                TextBoxJumlahBarang1.Clear()
                TextBoxTotalBarang1.Clear()
                TextBoxTotalBayar1.Clear()
                TextBoxDibayar1.Clear()
                TextBoxKembalian1.Clear()
                DataGridViewBarang1.Rows.Clear()

            Catch ex As Exception
                transaction.Rollback()
                MessageBox.Show("Error: " & ex.Message)
            End Try
        End Using
    End Sub

    Private Sub ButtonBatal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonBatal.Click
        Dim result As DialogResult = MessageBox.Show("Apakah Anda yakin ingin membatalkan?", "Konfirmasi Batal", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        ' Check if the user clicked "Yes"
        If result = DialogResult.Yes Then
            ' Clear the input fields
            TextBoxNoTransaksi.Text = ""
            TextBoxKasir.Text = ""
            TextBoxKodePelanggan1.Text = ""
            TextBoxKodeBarang1.Text = ""
            TextBoxNamaPelanggan1.Text = ""
            TextBoxNamaBarang1.Text = ""
            TextBoxHargaBarang1.Text = ""
            TextBoxSubtotalBarang1.Text = ""
            TextBoxJumlahBarang1.Text = ""
            TextBoxTotalBarang1.Text = ""
            TextBoxTotalBayar1.Text = ""
            TextBoxDibayar1.Text = ""
            TextBoxKembalian1.Text = ""

            ' Clear DataGridView
            DataGridViewBarang1.Rows.Clear()
            ' Any other fields that need to be cleared
        End If
    End Sub

    Private Sub ButtonScanBarcode_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonScanBarcode.Click
        ' Menampilkan daftar kamera yang terhubung
        Dim videoDevices As New FilterInfoCollection(FilterCategory.VideoInputDevice)
        If videoDevices.Count = 0 Then
            MessageBox.Show("Tidak ada kamera yang terdeteksi.")
            Return
        End If

        ' Menggunakan kamera pertama yang terdeteksi
        videoSource = New VideoCaptureDevice(videoDevices(0).MonikerString)
        AddHandler videoSource.NewFrame, AddressOf videoSource_NewFrame
        videoSource.Start()
    End Sub

    Private Sub videoSource_NewFrame(ByVal sender As Object, ByVal eventArgs As NewFrameEventArgs)
        ' Mengambil frame dari kamera
        Dim bitmap As Bitmap = eventArgs.Frame.Clone()
        Dim result As Result = barcodeReader.Decode(bitmap)

        If result IsNot Nothing Then
            ' Jika barcode terdeteksi, hentikan kamera dan panggil fungsi GetDataByKodeBarang
            videoSource.SignalToStop()
            videoSource.WaitForStop()

            ' Panggil fungsi untuk mendapatkan data berdasarkan kode barang
            GetDataByKodeBarang(result.Text)
        End If
    End Sub

    Private Sub TextBoxDiskon1_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBoxDiskon1.KeyPress
        If Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsDigit(e.KeyChar) AndAlso e.KeyChar <> "." AndAlso e.KeyChar <> "," Then
            e.Handled = True
        End If
    End Sub

    Private Sub TextBoxDibayar1_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBoxDibayar1.KeyPress
        If Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsDigit(e.KeyChar) AndAlso e.KeyChar <> "." AndAlso e.KeyChar <> "," Then
            e.Handled = True
        End If
    End Sub





    ' Form Pengaturan Akun

    Private Sub LoadDataUser()
        Using conn As New MySqlConnection(connString)
            Try
                conn.Open()
                Dim cmd As New MySqlCommand("SELECT * FROM users", conn)
                Dim adapter As New MySqlDataAdapter(cmd)
                table3 = New DataTable()
                adapter.Fill(table3)

                DataGridViewDataKaryawan.DataSource = table3

                DataGridViewDataKaryawan.Sort(DataGridViewDataKaryawan.Columns("Username"), System.ComponentModel.ListSortDirection.Ascending)

                DataGridViewDataKaryawan.Columns(0).Width = 80
                DataGridViewDataKaryawan.Columns(1).Width = 100
                DataGridViewDataKaryawan.Columns(2).Width = 80
                DataGridViewDataKaryawan.Columns(3).Width = 150
                DataGridViewDataKaryawan.Columns(4).Width = 90
                DataGridViewDataKaryawan.Columns(5).Width = 260
                DataGridViewDataKaryawan.Columns(6).Width = 80

            Catch ex As Exception
                MessageBox.Show("An error occurred: " & ex.Message)
            End Try
        End Using
    End Sub

    Private Function GenerateUserCode() As String
        Dim userPrefix As String = "UR" ' Prefix untuk kode pengguna
        Dim random As New Random()
        Dim randomCode As String = ""

        ' Menghasilkan nomor urut pengguna
        Dim userCount As Integer = GetUserCount() ' Mendapatkan jumlah pengguna yang ada
        Dim userNumber As String = (userCount + 1).ToString("D3") ' Format menjadi 3 digit

        ' Menghasilkan 4 karakter acak (2 huruf dan 2 angka)
        For i As Integer = 1 To 2
            ' Tambahkan huruf acak
            Dim letterIndex As Integer = random.Next(0, 26) ' 26 huruf dalam alfabet
            randomCode &= Chr(65 + letterIndex) ' Chr(65) adalah 'A'
        Next

        For i As Integer = 1 To 2
            ' Tambahkan angka acak
            Dim number As Integer = random.Next(0, 10) ' Angka dari 0 hingga 9
            randomCode &= number.ToString()
        Next

        ' Gabungkan prefix dengan nomor urut dan kode acak
        Return userPrefix & userNumber & "-" & randomCode
    End Function

    Private Function GetUserCount() As Integer
        Using conn As New MySqlConnection(connString)
            conn.Open()
            Dim query As String = "SELECT COUNT(*) FROM users"
            Using cmd As New MySqlCommand(query, conn)
                Return Convert.ToInt32(cmd.ExecuteScalar())
            End Using
        End Using
    End Function

    Private Function IsDataExists3(ByVal noHp As String) As Boolean
        Using conn As New MySqlConnection(connString)
            conn.Open()
            Dim query As String = "SELECT COUNT(*) FROM users WHERE NoHp = @NoHp"
            Dim cmd As New MySqlCommand(query, conn)

            cmd.Parameters.AddWithValue("@NoHp", noHp)

            Dim count As Integer = Convert.ToInt32(cmd.ExecuteScalar())
            Return count > 0 ' Mengembalikan true jika data sudah ada
        End Using
    End Function

    Private Sub ButtonRegis1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonRegis1.Click

        If String.IsNullOrEmpty(TextBoxUsername1.Text) OrElse
            String.IsNullOrEmpty(TextBoxPassword1.Text) OrElse
            String.IsNullOrEmpty(TextBoxKonfirmasiPassword1.Text) OrElse
            String.IsNullOrEmpty(TextBoxNoHp5.Text) OrElse
            String.IsNullOrEmpty(TextBoxEmail1.Text) OrElse
            String.IsNullOrEmpty(TextBoxAlamat5.Text) OrElse
            ComboBoxPosisi.SelectedIndex = -1 Then
            MessageBox.Show("Silakan lengkapi semua kolom yang diperlukan.")
            Return
        End If

        Dim noHp1 As String = TextBoxNoHp5.Text.Trim()

        ' Memastikan nomor HP dimulai dengan "08"
        If Not noHp1.StartsWith("08") Then
            MessageBox.Show("Nomor HP harus diawali dengan '08'.")
            Return
        End If

        ' Memastikan panjang nomor HP antara 11 hingga 13 karakter
        If noHp1.Length < 11 OrElse noHp1.Length > 13 Then
            MessageBox.Show("Nomor HP harus terdiri dari 11 hingga 13 digit.")
            Return
        End If

        If IsDataExists3(TextBoxNoHp5.Text) Then
            MessageBox.Show("Data tidak valid. Nomor HP sudah ada.")
            Return
        End If

        TextBoxKodeUser1.Text = GenerateUserCode()

        ' Create a new SQL connection
        Using connection As New MySqlConnection(connString)
            ' SQL command to insert data into Users table
            Dim commandText As String = "INSERT INTO users (UserID, Username, Password, Email, NoHp, Alamat, Posisi) VALUES (@UserID, @Username, @Password, @Email, @NoHp, @Alamat, @Posisi)"

            Using command As New MySqlCommand(commandText, connection)
                ' Add parameters to the command
                command.Parameters.AddWithValue("@UserID", TextBoxKodeUser1.Text)
                command.Parameters.AddWithValue("@Username", TextBoxUsername1.Text)
                command.Parameters.AddWithValue("@Password", TextBoxPassword1.Text)
                command.Parameters.AddWithValue("@Email", TextBoxEmail1.Text)
                command.Parameters.AddWithValue("@NoHp", TextBoxNoHp5.Text)
                command.Parameters.AddWithValue("@Alamat", TextBoxAlamat5.Text)
                command.Parameters.AddWithValue("@Posisi", ComboBoxPosisi.SelectedItem.ToString())

                Try
                    ' Open the connection
                    connection.Open()
                    ' Execute the command
                    command.ExecuteNonQuery()
                    MessageBox.Show("User  registered successfully!")
                Catch ex As MySqlException
                    MessageBox.Show("Database error: " & ex.Message)
                Catch ex As Exception
                    MessageBox.Show("Error: " & ex.Message)
                Finally
                    ' Close the connection
                    connection.Close()
                End Try
            End Using

            LoadDataUser()

            TextBoxKodeUser1.Text = ""
            TextBoxUsername1.Text = ""
            TextBoxPassword1.Text = ""
            TextBoxKonfirmasiPassword1.Text = ""
            TextBoxNoHp5.Text = ""
            TextBoxEmail1.Text = ""
            TextBoxAlamat5.Text = ""
            ComboBoxPosisi.SelectedIndex = -1
        End Using
    End Sub

    Private Sub DataGridViewDataKaryawan_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridViewDataKaryawan.CellContentClick
        If e.RowIndex >= 0 Then
            Dim row As DataGridViewRow = DataGridViewDataKaryawan.Rows(e.RowIndex)

            ' Mengisi TextBox dengan data dari baris yang dipilih
            TextBoxKodeUser1.Text = row.Cells("UserID").Value.ToString()
            TextBoxUsername1.Text = row.Cells("Username").Value.ToString()
            TextBoxPassword1.Text = row.Cells("Password").Value.ToString()
            TextBoxKonfirmasiPassword1.Text = row.Cells("Password").Value.ToString()
            TextBoxNoHp5.Text = row.Cells("NoHp").Value.ToString()
            TextBoxEmail1.Text = row.Cells("Email").Value.ToString()
            TextBoxAlamat5.Text = row.Cells("Alamat").Value.ToString()
            ComboBoxPosisi.SelectedItem = row.Cells("Posisi").Value.ToString()

            ButtonRegis1.Enabled = False
            ButtonUbah5.Enabled = True
            ButtonHapus1.Enabled = True

        End If
    End Sub

    Private Sub ButtonUbah5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonUbah5.Click
        ' Validasi input
        If String.IsNullOrEmpty(TextBoxUsername1.Text) OrElse
           String.IsNullOrEmpty(TextBoxPassword1.Text) OrElse
           String.IsNullOrEmpty(TextBoxKonfirmasiPassword1.Text) OrElse
           String.IsNullOrEmpty(TextBoxNoHp5.Text) OrElse
           String.IsNullOrEmpty(TextBoxEmail1.Text) OrElse
           String.IsNullOrEmpty(TextBoxAlamat5.Text) OrElse
           ComboBoxPosisi.SelectedIndex = -1 Then
            MessageBox.Show("Silakan lengkapi semua kolom yang diperlukan.")
            Return
        End If

        Dim noHp2 As String = TextBoxNoHp5.Text.Trim()

        ' Memastikan nomor HP dimulai dengan "08"
        If Not noHp2.StartsWith("08") Then
            MessageBox.Show("Nomor HP harus diawali dengan '08'.")
            Return
        End If

        ' Memastikan panjang nomor HP antara 11 hingga 13 karakter
        If noHp2.Length < 11 OrElse noHp2.Length > 13 Then
            MessageBox.Show("Nomor HP harus terdiri dari 11 hingga 13 digit.")
            Return
        End If

        ' Mengupdate data pengguna di database
        Using connection As New MySqlConnection(connString)
            Dim commandText As String = "UPDATE users SET Username = @Username, Password = @Password, Email = @Email, NoHp = @NoHp, Alamat = @Alamat, Posisi = @Posisi WHERE UserID = @UserID"

            Using command As New MySqlCommand(commandText, connection)
                ' Add parameters to the command
                command.Parameters.AddWithValue("@UserID", TextBoxKodeUser1.Text)
                command.Parameters.AddWithValue("@Username", TextBoxUsername1.Text)
                command.Parameters.AddWithValue("@Password", TextBoxPassword1.Text)
                command.Parameters.AddWithValue("@Email", TextBoxEmail1.Text)
                command.Parameters.AddWithValue("@NoHp", TextBoxNoHp5.Text)
                command.Parameters.AddWithValue("@Alamat", TextBoxAlamat5.Text)
                command.Parameters.AddWithValue("@Posisi", ComboBoxPosisi.SelectedItem.ToString())

                Try
                    ' Open the connection
                    connection.Open()
                    ' Execute the command
                    command.ExecuteNonQuery()
                    MessageBox.Show("User  updated successfully!")
                Catch ex As MySqlException
                    MessageBox.Show("Database error: " & ex.Message)
                Catch ex As Exception
                    MessageBox.Show("Error: " & ex.Message)
                Finally
                    ' Close the connection
                    connection.Close()
                End Try
            End Using
        End Using

        ' Memuat ulang data ke DataGridView
        LoadDataUser()

        ' Mengosongkan input setelah update
        TextBoxKodeUser1.Text = ""
        TextBoxUsername1.Text = ""
        TextBoxPassword1.Text = ""
        TextBoxKonfirmasiPassword1.Text = ""
        TextBoxNoHp5.Text = ""
        TextBoxEmail1.Text = ""
        TextBoxAlamat5.Text = ""
        ComboBoxPosisi.SelectedIndex = -1

        ButtonRegis1.Enabled = True
        ButtonUbah5.Enabled = False
    End Sub

    Private Sub ButtonHapus5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonHapus5.Click
        If String.IsNullOrEmpty(TextBoxKodeUser1.Text) Then
            MessageBox.Show("Silakan pilih data user yang ingin dihapus.")
            Return
        End If

        ' Tampilkan dialog untuk meminta password
        Dim password As String = InputBox("Masukkan password untuk menghapus data:", "Verifikasi Password")

        If String.IsNullOrEmpty(password) Then
            Return
        End If

        ' Verifikasi password (ganti "your_password" dengan password yang benar)
        If password <> "pqkljs08" Then
            MessageBox.Show("Password salah. Data tidak dapat dihapus.")
            Return
        End If

        ' Jika password benar, lanjutkan untuk menghapus data
        Using conn As New MySqlConnection(connString)
            Try
                conn.Open()

                ' SQL command to delete the record
                Dim cmd As New MySqlCommand("DELETE FROM users WHERE UserID = @UserID", conn)

                ' Adding parameters
                cmd.Parameters.AddWithValue("@UserID", TextBoxKodeUser1.Text)

                ' Execute the command
                cmd.ExecuteNonQuery()
                MessageBox.Show("Data berhasil dihapus!")

                ' Panggil LoadData untuk memperbarui DataGridView
                LoadDataUser()

                ' Mengosongkan TextBox setelah menghapus data
                TextBoxKodeUser1.Text = ""
                TextBoxUsername1.Text = ""
                TextBoxPassword1.Text = ""
                TextBoxKonfirmasiPassword1.Text = ""
                TextBoxNoHp5.Text = ""
                TextBoxEmail1.Text = ""
                TextBoxAlamat5.Text = ""
                ComboBoxPosisi.SelectedIndex = -1
            Catch ex As Exception
                MessageBox.Show("An error occurred: " & ex.Message)
            Finally
                conn.Close()
            End Try
        End Using
    End Sub

    Private Sub TextBoxNoHp5_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBoxNoHp5.KeyPress
        If Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsDigit(e.KeyChar) AndAlso e.KeyChar <> "." AndAlso e.KeyChar <> "," Then
            e.Handled = True
        End If
    End Sub





    ' Form Laporan

    Private Sub LoadDataLaporanPenjualan()
        Using conn As New MySqlConnection(connString)
            Try
                conn.Open()
                ' Mengambil data berdasarkan tanggal yang dipilih
                Dim cmd As New MySqlCommand("SELECT * FROM transaksi", conn)
                Dim adapter As New MySqlDataAdapter(cmd)
                table4 = New DataTable()
                adapter.Fill(table4)

                DataGridViewTampilLaporan.DataSource = table4

                ' Mengatur urutan dan lebar kolom
                DataGridViewTampilLaporan.Sort(DataGridViewTampilLaporan.Columns("tanggal"), System.ComponentModel.ListSortDirection.Ascending)

                ' Mengatur lebar kolom
                DataGridViewTampilLaporan.Columns(0).Width = 110
                DataGridViewTampilLaporan.Columns(1).Width = 80
                DataGridViewTampilLaporan.Columns(2).Width = 80
                DataGridViewTampilLaporan.Columns(3).Width = 100
                DataGridViewTampilLaporan.Columns(4).Width = 90
                DataGridViewTampilLaporan.Columns(5).Width = 260
                DataGridViewTampilLaporan.Columns(6).Width = 80
                DataGridViewTampilLaporan.Columns(7).Width = 80
                DataGridViewTampilLaporan.Columns(8).Width = 80
                DataGridViewTampilLaporan.Columns(9).Width = 80
                DataGridViewTampilLaporan.Columns(10).Width = 80

            Catch ex As Exception
                MessageBox.Show("An error occurred: " & ex.Message)
            End Try
        End Using
    End Sub

    Private Sub LoadDataLaporanPenjualanTanggal(ByVal filterDate As DateTime)
        Using conn As New MySqlConnection(connString)
            Try
                conn.Open()
                ' Mengambil data berdasarkan tanggal yang dipilih
                Dim cmd As New MySqlCommand("SELECT * FROM transaksi WHERE DATE(tanggal) = @filterDate", conn)
                cmd.Parameters.AddWithValue("@filterDate", filterDate.Date)

                Dim adapter As New MySqlDataAdapter(cmd)
                table4 = New DataTable()
                adapter.Fill(table4)

                DataGridViewTampilLaporan.DataSource = table4

                ' Mengatur urutan dan lebar kolom
                DataGridViewTampilLaporan.Sort(DataGridViewTampilLaporan.Columns("tanggal"), System.ComponentModel.ListSortDirection.Ascending)

                ' Mengatur lebar kolom
                DataGridViewTampilLaporan.Columns(0).Width = 110
                DataGridViewTampilLaporan.Columns(1).Width = 80
                DataGridViewTampilLaporan.Columns(2).Width = 80
                DataGridViewTampilLaporan.Columns(3).Width = 100
                DataGridViewTampilLaporan.Columns(4).Width = 90
                DataGridViewTampilLaporan.Columns(5).Width = 260
                DataGridViewTampilLaporan.Columns(6).Width = 80
                DataGridViewTampilLaporan.Columns(7).Width = 80
                DataGridViewTampilLaporan.Columns(8).Width = 80
                DataGridViewTampilLaporan.Columns(9).Width = 80
                DataGridViewTampilLaporan.Columns(10).Width = 80

            Catch ex As Exception
                MessageBox.Show("An error occurred: " & ex.Message)
            End Try
        End Using
    End Sub

    Private Sub LoadDataLaporanStok()
        Using conn As New MySqlConnection(connString)
            Try
                conn.Open()
                ' Mengambil kolom tertentu dari tabel barang
                Dim cmd As New MySqlCommand("SELECT kodebarang, namabarang, stok, satuan, jenisbarang FROM barang", conn)
                Dim adapter As New MySqlDataAdapter(cmd)
                Dim table5 As New DataTable()
                adapter.Fill(table5)

                DataGridViewTampilLaporan.DataSource = table5

                DataGridViewTampilLaporan.Sort(DataGridViewTampilLaporan.Columns("jenisbarang"), System.ComponentModel.ListSortDirection.Ascending)

                DataGridViewTampilLaporan.Columns(0).Width = 90
                DataGridViewTampilLaporan.Columns(1).Width = 480
                DataGridViewTampilLaporan.Columns(2).Width = 80
                DataGridViewTampilLaporan.Columns(3).Width = 80
                DataGridViewTampilLaporan.Columns(4).Width = 100

            Catch ex As Exception
                MessageBox.Show("An error occurred: " & ex.Message)
            End Try
        End Using
    End Sub

    Private Sub LoadDataLaporanStokByJenisBarang(ByVal jenisBarang As String)
        Using conn As New MySqlConnection(connString)
            Try
                conn.Open()
                ' Mengambil kolom tertentu dari tabel barang berdasarkan jenis barang
                Dim cmd As New MySqlCommand("SELECT kodebarang, namabarang, stok, satuan, jenisbarang FROM barang WHERE jenisbarang = @jenisBarang", conn)
                cmd.Parameters.AddWithValue("@jenisBarang", jenisBarang)
                Dim adapter As New MySqlDataAdapter(cmd)
                Dim table5 As New DataTable()
                adapter.Fill(table5)

                DataGridViewTampilLaporan.DataSource = table5

                DataGridViewTampilLaporan.Sort(DataGridViewTampilLaporan.Columns("jenisbarang"), System.ComponentModel.ListSortDirection.Ascending)

                DataGridViewTampilLaporan.Columns(0).Width = 90
                DataGridViewTampilLaporan.Columns(1).Width = 480
                DataGridViewTampilLaporan.Columns(2).Width = 80
                DataGridViewTampilLaporan.Columns(3).Width = 80
                DataGridViewTampilLaporan.Columns(4).Width = 100

            Catch ex As Exception
                MessageBox.Show("An error occurred: " & ex.Message)
            End Try
        End Using
    End Sub

    Private Sub ComboBoxJenisBarang5_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ComboBoxJenisBarang5.SelectedIndexChanged
        Dim selectedJenisBarang As String = ComboBoxJenisBarang5.SelectedItem.ToString()
        LoadDataLaporanStokByJenisBarang(selectedJenisBarang)
    End Sub

    Private Function GetDataTableFromDataGridView() As DataTable
        Dim dt As New DataTable()

        ' Menambahkan kolom ke DataTable sesuai dengan kolom di DataGridView
        For Each column As DataGridViewColumn In DataGridViewTampilLaporan.Columns
            dt.Columns.Add(column.Name, GetType(String)) ' Menggunakan String untuk kolom yang akan diformat
        Next

        ' Menambahkan baris ke DataTable dari DataGridView
        For Each row As DataGridViewRow In DataGridViewTampilLaporan.Rows
            If Not row.IsNewRow Then
                Dim newRow As DataRow = dt.NewRow()
                For Each column As DataGridViewColumn In DataGridViewTampilLaporan.Columns
                    If column.Name = "Tanggal" Then ' Gantilah "Tanggal" dengan nama kolom yang sesuai
                        Dim originalDate As DateTime = Convert.ToDateTime(row.Cells(column.Index).Value)
                        newRow(column.Name) = originalDate.ToString("dd-MMMM-yyyy") ' Format tanggal di sini
                    Else
                        newRow(column.Name) = row.Cells(column.Index).Value
                    End If
                Next
                dt.Rows.Add(newRow)
            End If
        Next

        Return dt
    End Function

    Private Sub RadioButtonLaporanPenjualan_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButtonLaporanPenjualan.CheckedChanged
        If RadioButtonLaporanPenjualan.Checked Then
            RadioButtonLaporanStok.Checked = False ' Nonaktifkan RadioButton
            LoadDataLaporanPenjualan()
        End If
    End Sub

    Private Sub RadioButtonLaporanStok_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButtonLaporanStok.CheckedChanged
        If RadioButtonLaporanStok.Checked Then
            RadioButtonLaporanPenjualan.Checked = False ' Nonaktifkan RadioButton
            LoadDataLaporanStok()
        End If
    End Sub

    Private Sub DateTimePickerTanggal_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DateTimePickerTanggal.ValueChanged
        ' Memanggil fungsi untuk memuat data laporan penjualan berdasarkan tanggal yang dipilih
        LoadDataLaporanPenjualanTanggal(DateTimePickerTanggal.Value)
    End Sub

    Private Sub ButtonCetak4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonCetak4.Click
        Dim laporanViewer As New LaporanPenjualanViewer()
        Dim StokViewer As New LaporanStokViewer()

        Dim dataTable As DataTable = GetDataTableFromDataGridView()

        If RadioButtonLaporanPenjualan.Checked = True Then

            ' Memanggil metode untuk memuat laporan dengan DataTable
            laporanViewer.LoadReport(dataTable)

            ' Menampilkan form laporan
            laporanViewer.Show()
        ElseIf RadioButtonLaporanStok.Checked = True Then
            ' Memanggil metode untuk memuat laporan
            StokViewer.LoadReport(dataTable) ' Pastikan Anda memiliki metode ini di LaporanPenjualanViewer

            ' Menampilkan form laporan
            StokViewer.Show()
        End If
    End Sub

End Class