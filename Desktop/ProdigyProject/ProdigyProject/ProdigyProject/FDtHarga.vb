Imports System.Data.Odbc

Public Class FDtHarga
    Public Property KodeBarang As String
    Public Property Isi1 As Integer
    Public Property Isi2 As Integer

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
                        CType(ctl, TextBox).Text = If(IsDBNull(nilai), "", nilai.ToString())
                    End If
                Next
            End If
            Rd.Close()

        Catch ex As Exception
            MessageBox.Show("Gagal memuat data harga: " & ex.Message)
        End Try
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

            Me.Controls.Add(lbl)
            Me.Controls.Add(txt)

            i += 1
        Next
    End Sub

    Private Sub btnPTUTUP_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPTUTUP.Click
        Me.Close()
    End Sub

    Private Sub btnPSIMPAN_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPSIMPAN.Click
        Try
            If String.IsNullOrEmpty(KodeBarang) Then
                MessageBox.Show("Kode barang belum diatur!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            BukaKoneksi()

            ' === Ambil semua kolom harga di tabel ===
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

            ' === Cek apakah kode barang sudah ada ===
            Cmd = New OdbcCommand("SELECT COUNT(*) FROM zstok WHERE kodebrg = ?", Conn)
            Cmd.Parameters.AddWithValue("@kodebrg", KodeBarang)
            Dim ada As Integer = Convert.ToInt32(Cmd.ExecuteScalar())

            ' === Siapkan nilai harga dari form ===
            Dim nilaiHarga As New Dictionary(Of String, String)
            For Each col As String In hargaList
                Dim ctl = Me.Controls.Find("txt" & col.ToUpper(), True).FirstOrDefault()
                If ctl IsNot Nothing AndAlso TypeOf ctl Is TextBox Then
                    Dim isi = CType(ctl, TextBox).Text.Trim()
                    nilaiHarga(col) = If(isi = "", "0", isi)
                End If
            Next

            ' === Jika sudah ada, update ===
            If ada > 0 Then
                Dim updateSql As String = "UPDATE zstok SET "
                Dim updateParts As New List(Of String)

                For Each kvp In nilaiHarga
                    updateParts.Add(kvp.Key & " = ?")
                Next

                updateSql &= String.Join(", ", updateParts) & " WHERE kodebrg = ?"

                Cmd = New OdbcCommand(updateSql, Conn)

                ' Tambahkan semua nilai harga
                For Each kvp In nilaiHarga
                    Cmd.Parameters.AddWithValue("@" & kvp.Key, kvp.Value)
                Next

                ' Parameter terakhir untuk kode barang
                Cmd.Parameters.AddWithValue("@kodebrg", KodeBarang)

                Cmd.ExecuteNonQuery()
                MessageBox.Show("Data harga berhasil diperbarui!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information)

            Else
                ' === Jika belum ada, insert ===
                Dim semuaKolom As New List(Of String)
                semuaKolom.Add("kodebrg")
                semuaKolom.AddRange(nilaiHarga.Keys)

                Dim kolomSql As String = String.Join(",", semuaKolom)
                Dim paramSql As String = String.Join(",", Enumerable.Repeat("?", semuaKolom.Count))

                Dim insertSql As String = "INSERT INTO zstok (" & kolomSql & ") VALUES (" & paramSql & ")"
                Cmd = New OdbcCommand(insertSql, Conn)

                ' Tambahkan kodebrg dulu
                Cmd.Parameters.AddWithValue("@kodebrg", KodeBarang)

                ' Tambahkan semua nilai harga
                For Each kvp In nilaiHarga
                    Cmd.Parameters.AddWithValue("@" & kvp.Key, kvp.Value)
                Next

                Cmd.ExecuteNonQuery()
                MessageBox.Show("Data harga baru berhasil ditambahkan!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

        Catch ex As Exception
            MessageBox.Show("Gagal menyimpan data harga: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not Rd Is Nothing AndAlso Not Rd.IsClosed Then Rd.Close()
            If Conn.State = ConnectionState.Open Then Conn.Close()
        End Try
    End Sub

End Class
