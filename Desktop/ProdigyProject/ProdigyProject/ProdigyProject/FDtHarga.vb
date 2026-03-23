Imports System.Data.Odbc
Imports System.Globalization

Public Class FDtHarga
    Public Property KodeBarang As String
    Public Property Isi1 As Integer
    Public Property Isi2 As Integer
    Private koma As CultureInfo = New CultureInfo("en-US")

    Private Sub FDtHarga_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        lblKDBARANG.Text = KodeBarang

        Try
            BukaKoneksi()

            ' === Ambil struktur kolom harga ===
            Cmd = New OdbcCommand("SHOW COLUMNS FROM zstok", Conn)
            Rd = Cmd.ExecuteReader()

            Dim hargaList As New List(Of String)
            While Rd.Read()
                Dim colName As String = Rd("Field").ToString()
                If colName.ToLower().StartsWith("harga") Then
                    hargaList.Add(colName)
                End If
            End While
            Rd.Close()

            ' === Bagi berdasarkan pola ===
            Dim grup1 = hargaList.Where(Function(h) h Like "harga[1-6]" AndAlso h.Length = 6).ToList()     ' harga1-6
            Dim grup2 = hargaList.Where(Function(h) h Like "harga[1-6][1-6]" AndAlso h.Length = 7).ToList()  ' harga11-66
            Dim grup3 = hargaList.Where(Function(h) h Like "harga[1-6][1-6][1-6]" AndAlso h.Length = 8).ToList() ' harga111-666

            ' === Urut biar rapi ===
            grup1.Sort()
            grup2.Sort()
            grup3.Sort()

            ' === Posisi dasar ===
            Dim startX As Integer = 10
            Dim startY As Integer = 60
            Dim spacingX As Integer = 160
            Dim spacingY As Integer = 23

            ' === Tampilkan tiap kolom secara vertikal ===
            TampilkanKolomVertikal(grup1, startX, startY, spacingY)
            TampilkanKolomVertikal(grup2, startX + spacingX, startY, spacingY)
            TampilkanKolomVertikal(grup3, startX + spacingX * 2, startY, spacingY)

            ' === Atur enable/disable sesuai isi1 & isi2 ===
            AturEnableHarga(grup1, grup2, grup3)

            ' === Jika kode barang ada, tampilkan nilainya ===
            If Not String.IsNullOrEmpty(KodeBarang) Then
                MuatHargaBarang(KodeBarang, hargaList)
            End If

        Catch ex As Exception
            MessageBox.Show("Gagal memuat kolom harga: " & ex.Message)
        Finally
            If Not Rd Is Nothing AndAlso Not Rd.IsClosed Then Rd.Close()
            If Conn.State = ConnectionState.Open Then Conn.Close()
        End Try
    End Sub

    Private Sub TextBoxAngka_KeyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs)
        ' Izinkan angka 0–9 dan Backspace
        If Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsDigit(e.KeyChar) AndAlso e.KeyChar <> ","c AndAlso e.KeyChar <> "."c Then
            e.Handled = True
        End If
    End Sub
    ' === Atur textbox mana yang aktif ===
    Private Sub AturEnableHarga(ByVal grup1 As List(Of String), ByVal grup2 As List(Of String), ByVal grup3 As List(Of String))
        Dim enable1 As Boolean = (Isi1 > 2)
        Dim enable2 As Boolean = (Isi2 > 2)

        ' Default semua disable
        For Each ctl As Control In Me.Controls
            If TypeOf ctl Is TextBox AndAlso ctl.Name.StartsWith("txtharga", StringComparison.OrdinalIgnoreCase) Then
                ctl.Enabled = False
            End If
        Next

        ' Jika isi1 > 2 dan isi2 > 2
        If enable1 AndAlso enable2 Then
            AktifkanGroup(grup1)
            AktifkanGroup(grup2)
            AktifkanGroup(grup3)

            ' Jika isi1 > 2 dan isi2 < 2
        ElseIf enable1 AndAlso Not enable2 Then
            AktifkanGroup(grup1)
            AktifkanGroup(grup2)

            ' Jika isi1 < 2 dan isi2 < 2
        Else
            AktifkanGroup(grup1)
        End If
    End Sub

    Private Sub AktifkanGroup(ByVal daftar As List(Of String))
        For Each nama As String In daftar
            Dim ctl As Control = Me.Controls.Find("txt" & nama.ToUpper(), True).FirstOrDefault()
            If ctl IsNot Nothing Then ctl.Enabled = True
        Next
    End Sub

    ' === Ambil harga barang dari database ===
    Private Sub MuatHargaBarang(ByVal kode As String, ByVal daftarHarga As List(Of String))
        Try
            BukaKoneksi()
            Dim kolomList As String = String.Join(",", daftarHarga)
            Dim sql As String = "SELECT " & kolomList & " FROM zstok WHERE kodebrg = ?"
            Cmd = New OdbcCommand(sql, Conn)
            Cmd.Parameters.AddWithValue("@kodebrg", kode)
            Rd = Cmd.ExecuteReader()

            If Rd.Read() Then
                For Each col As String In daftarHarga
                    Dim ctl = Me.Controls.Find("txt" & col.ToUpper(), True).FirstOrDefault()
                    If ctl IsNot Nothing AndAlso TypeOf ctl Is TextBox Then
                        Dim nilai = Rd(col)
                        If Not IsDBNull(nilai) Then
                            Dim angka As Double
                            If Double.TryParse(nilai.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, angka) Then
                                CType(ctl, TextBox).Text = angka.ToString("N2", koma)
                            Else
                                CType(ctl, TextBox).Text = 0D.ToString("N2", koma)
                            End If
                        Else
                            CType(ctl, TextBox).Text = "0,00"
                        End If
                    End If
                Next
            End If
            Rd.Close()

        Catch ex As Exception
            MessageBox.Show("Gagal memuat data harga: " & ex.Message)
        End Try
    End Sub
    Private Sub FormatRibuanDesimal(ByVal txt As TextBox)

        If txt.Text.Trim = "" Then Exit Sub

        Dim angka As Double

        If Double.TryParse(txt.Text, NumberStyles.Any, koma, angka) Then
            txt.Text = angka.ToString("N2", koma)
        End If

    End Sub

    ' === Sub bantu untuk tampilan vertikal ===
    Private Sub TampilkanKolomVertikal(ByVal daftar As List(Of String), ByVal startX As Integer, ByVal startY As Integer, ByVal spacingY As Integer)
        Dim i As Integer = 0
        For Each colName As String In daftar
            ' Label
            Dim lbl As New Label()
            lbl.Text = colName
            lbl.Left = startX
            lbl.Top = startY + (i * spacingY)
            lbl.AutoSize = True

            ' TextBox
            Dim txt As New TextBox()
            txt.Name = "txt" & colName.ToUpper()
            txt.Left = startX + 60
            txt.Top = startY + (i * spacingY) - 3
            txt.Width = 90
            txt.TextAlign = HorizontalAlignment.Right

            ' === PASANG KEY PRESS ANGKA ===
            AddHandler txt.KeyPress, AddressOf TextBoxAngka_KeyPress
            AddHandler txt.Leave, AddressOf TextBoxAngka_Leave

            Me.Controls.Add(lbl)
            Me.Controls.Add(txt)

            i += 1
        Next
    End Sub

    Private Sub TextBoxAngka_Leave(ByVal sender As Object, ByVal e As EventArgs)
        Dim txt As TextBox = CType(sender, TextBox)
        FormatRibuanDesimal(txt)
    End Sub

    Private Sub btnPTUTUP_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPTUTUP.Click
        Me.Close()
    End Sub

    Private Sub btnPSIMPAN_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPSIMPAN.Click
        Try
            If String.IsNullOrEmpty(KodeBarang) Then
                MessageBox.Show("Kode barang belum diatur!")
                Exit Sub
            End If

            BukaKoneksi()

            ' === Ambil semua textbox harga yang ADA di form saja ===
            Dim hargaControls = Me.Controls.
                OfType(Of TextBox)().
                Where(Function(t) t.Name.StartsWith("txtHARGA", StringComparison.OrdinalIgnoreCase)).
                OrderBy(Function(t) t.Name).
                ToList()

            If hargaControls.Count = 0 Then
                MessageBox.Show("Tidak ada kolom harga ditemukan!")
                Exit Sub
            End If

            ' === Cek apakah data sudah ada ===
            Dim cekCmd As New OdbcCommand("SELECT COUNT(*) FROM zstok WHERE kodebrg = ?", Conn)
            cekCmd.Parameters.AddWithValue("", KodeBarang)
            Dim ada As Integer = Convert.ToInt32(cekCmd.ExecuteScalar())

            If ada > 0 Then
                ' =========================
                ' UPDATE
                ' =========================
                Dim sql As String = "UPDATE zstok SET "

                Dim setParts As New List(Of String)
                For Each txt In hargaControls
                    Dim namaKolom As String = txt.Name.Substring(3).ToLower()
                    setParts.Add(namaKolom & " = ?")
                Next

                sql &= String.Join(", ", setParts)
                sql &= " WHERE kodebrg = ?"

                Dim cmd As New OdbcCommand(sql, Conn)

                ' === isi parameter sesuai URUTAN ? ===
                For Each txt In hargaControls
                    Dim angka As Double = 0
                    Double.TryParse(txt.Text, NumberStyles.Any, koma, angka)
                    cmd.Parameters.AddWithValue("", angka)
                Next

                ' parameter terakhir untuk WHERE
                cmd.Parameters.AddWithValue("", KodeBarang)

                Dim rows = cmd.ExecuteNonQuery()

                If rows > 0 Then
                    MessageBox.Show("Data harga berhasil diupdate!")
                Else
                    MessageBox.Show("Data tidak berubah atau kode tidak ditemukan!")
                End If

            Else
                ' =========================
                ' INSERT
                ' =========================
                Dim kolomList As New List(Of String)
                kolomList.Add("kodebrg")

                For Each txt In hargaControls
                    kolomList.Add(txt.Name.Substring(3).ToLower())
                Next

                Dim sql As String =
                    "INSERT INTO zstok (" &
                    String.Join(",", kolomList) &
                    ") VALUES (" &
                    String.Join(",", Enumerable.Repeat("?", kolomList.Count)) &
                    ")"

                Dim cmd As New OdbcCommand(sql, Conn)

                ' urutan parameter HARUS sama
                cmd.Parameters.AddWithValue("", KodeBarang)

                For Each txt In hargaControls
                    Dim angka As Double = 0
                    Double.TryParse(txt.Text, NumberStyles.Any, koma, angka)
                    cmd.Parameters.AddWithValue("", angka)
                Next

                cmd.ExecuteNonQuery()
                MessageBox.Show("Data harga berhasil ditambahkan!")
            End If

        Catch ex As Exception
            MessageBox.Show("Gagal simpan: " & ex.Message)
        Finally
            If Conn.State = ConnectionState.Open Then Conn.Close()
        End Try
    End Sub

    Private Sub FDtHarga_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If TypeOf Me.ActiveControl Is TextBox Then

            Select Case e.KeyCode
                Case Keys.Right
                    Me.SelectNextControl(Me.ActiveControl, True, True, True, True)

                Case Keys.Left
                    Me.SelectNextControl(Me.ActiveControl, False, True, True, True)

                Case Keys.Down
                    Me.SelectNextControl(Me.ActiveControl, True, True, True, True)

                Case Keys.Up
                    Me.SelectNextControl(Me.ActiveControl, False, True, True, True)
            End Select

        End If
    End Sub
End Class
