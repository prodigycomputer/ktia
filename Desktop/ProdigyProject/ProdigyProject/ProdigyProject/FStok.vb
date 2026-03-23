Imports System.Data.Odbc
Imports System.Globalization

Public Class FStok
    Private statusMode As String = ""   ' status: "TAMBAH" / "UBAH"
    Public KodeLama As String = ""
    Private isUserTypingSearch As Boolean = False
    Private koma As CultureInfo = New CultureInfo("en-US")
    Private isLoadingData As Boolean = False

    Dim isi1 As Integer
    Dim isi2 As Integer

    Public Sub SetMerek(ByVal kode As String, ByVal nama As String)
        txtKDMERK.Text = kode
        txtNMMERK.Text = nama
    End Sub

    Public Sub SetGolongan(ByVal kode As String, ByVal nama As String)
        txtKDGOL.Text = kode
        txtNMGOL.Text = nama
    End Sub

    Public Sub SetGrup(ByVal kode As String, ByVal nama As String)
        txtKDGRUP.Text = kode
        txtNMGRUP.Text = nama
    End Sub
    Private Sub FStok_Shown(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Shown
        Me.ActiveControl = Nothing
    End Sub

    Private Sub FStok_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles Me.KeyDown

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

    Private Sub FStok_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetButtonState(Me, True)
        AngkaHelper.AktifkanEnterPindah(Me)
        BukaKoneksi()
        DisabledLoad()

        ModPlaceholder.SetPlaceholder(tSKDBRG, "KODE BARANG")
        ModPlaceholder.SetPlaceholder(tSNMBRG, "NAMA BARANG")

        isUserTypingSearch = False

        ' pastikan dua-duanya aktif di awal
        tSKDBRG.Enabled = True
        tSNMBRG.Enabled = True
    End Sub

    Private Sub Search_KeyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs) _
    Handles tSKDBRG.KeyPress, tSNMBRG.KeyPress

        If Char.IsLetterOrDigit(e.KeyChar) Then
            isUserTypingSearch = True
        End If
    End Sub


    Private Sub TextBox_Enter(ByVal sender As Object, ByVal e As EventArgs) _
    Handles tSKDBRG.Enter, tSNMBRG.Enter

        ModPlaceholder.RemovePlaceholder(CType(sender, TextBox))
    End Sub

    Private Sub TextBox_Leave(ByVal sender As Object, ByVal e As EventArgs) _
        Handles tSKDBRG.Leave, tSNMBRG.Leave

        Dim txt = CType(sender, TextBox)
        If txt.Text.Trim() = "" Then
            ModPlaceholder.SetPlaceholder(txt, txt.Tag.ToString())
        End If
    End Sub


    Public Sub LoadData(ByVal kodebrg As String)
        Try
            isLoadingData = True
            BukaKoneksi()

            Dim sql As String = "SELECT z.kodebrg, z.namabrg, z.kodemerk, m.namamerk, z.kodegol, g.namagol, z.kodegrup, gr.namagrup, z.satuan1, z.satuan2, z.satuan3, z.isi1, z.isi2, z.hrgbeli " &
                "FROM zstok z " &
                "LEFT JOIN zmerek m ON z.kodemerk = m.kodemerk " &
                "LEFT JOIN zgolongan g ON z.kodegol = g.kodegol " &
                "LEFT JOIN zgrup gr ON z.kodegrup = gr.kodegrup " &
                "WHERE z.kodebrg = ?"

            'Dim sql As String = "SELECT kodebrg, namabrg, kodemerk, kodegol, kodegrup, satuan1, satuan2, satuan3, isi1, isi2, hrgbeli FROM zstok WHERE kodebrg = ?"
            Cmd = New OdbcCommand(sql, Conn)
            Cmd.Parameters.AddWithValue("", kodebrg)

            Rd = Cmd.ExecuteReader()

            If Rd.Read() Then
                ' === Isi field ke textbox di form ===
                txtKDMERK.Text = Rd("kodemerk").ToString()
                txtKDGOL.Text = Rd("kodegol").ToString()
                txtKDGRUP.Text = Rd("kodegrup").ToString()
                txtNMMERK.Text = Rd("namamerk").ToString()
                txtNMGOL.Text = Rd("namagol").ToString()
                txtNMGRUP.Text = Rd("namagrup").ToString()
                txtKDBRG.Text = Rd("kodebrg").ToString()
                txtNMBRG.Text = Rd("namabrg").ToString()
                txtSTN1.Text = Rd("satuan1").ToString()
                'txtISI1.Text = Rd("isi1").ToString()
                txtISI1.Text = CDbl(Rd("isi1")).ToString("N2", koma)
                txtSTN2.Text = Rd("satuan2").ToString()
                'txtISI2.Text = Rd("isi2").ToString()
                txtISI2.Text = CDbl(Rd("isi2")).ToString("N2", koma)
                txtSTN3.Text = Rd("satuan3").ToString()
                'txtHRGBELI.Text = Rd("hrgbeli").ToString()
                txtHRGBELI.Text = CDbl(Rd("hrgbeli")).ToString("N2", koma)
                ' Kalau ada field lain (misalnya level, aktif, dsb) bisa ditambah di sini
                ' txtLEVEL.Text = Rd("level").ToString()
            Else
                MessageBox.Show("Data Barang tidak ditemukan.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

        Catch ex As Exception
            MessageBox.Show("Gagal memuat data user: " & ex.Message)
        Finally
            isLoadingData = False
        End Try
    End Sub

    Private Sub KosongkanInput()
        txtKDMERK.Text = ""
        txtKDGOL.Text = ""
        txtKDGRUP.Text = ""
        txtNMMERK.Text = ""
        txtNMGOL.Text = ""
        txtNMGRUP.Text = ""
        txtKDBRG.Text = ""
        txtNMBRG.Text = ""
        txtSTN1.Text = ""
        txtISI1.Text = "0.00"
        txtSTN2.Text = ""
        txtISI2.Text = "0.00"
        txtSTN3.Text = ""
        txtHRGBELI.Text = "0.00"
        txtGAMBAR.Text = ""
        tSKDBRG.Text = ""
        tSNMBRG.Text = ""
    End Sub

    Private Sub DisabledLoad()
        txtKDMERK.Enabled = False
        txtKDGOL.Enabled = False
        txtKDGRUP.Enabled = False
        txtNMMERK.Enabled = False
        txtNMGOL.Enabled = False
        txtNMGRUP.Enabled = False
        txtKDBRG.Enabled = False
        txtNMBRG.Enabled = False
        txtSTN1.Enabled = False
        txtISI1.Enabled = False
        txtSTN2.Enabled = False
        txtISI2.Enabled = False
        txtSTN3.Enabled = False
        txtHRGBELI.Enabled = False
        btnUPLOAD.Enabled = False
        txtGAMBAR.Enabled = False
        btnHARGA.Enabled = False
        tSKDBRG.Enabled = True
        tSNMBRG.Enabled = True
    End Sub

    Private Sub EnabledLoad()
        txtKDMERK.Enabled = True
        txtKDGOL.Enabled = True
        txtKDGRUP.Enabled = True
        txtNMMERK.Enabled = True
        txtNMGOL.Enabled = True
        txtNMGRUP.Enabled = True
        txtKDBRG.Enabled = True
        txtNMBRG.Enabled = True
        txtSTN1.Enabled = True
        txtISI1.Enabled = True
        txtHRGBELI.Enabled = True
        btnUPLOAD.Enabled = True
        txtGAMBAR.Enabled = True
        btnHARGA.Enabled = True
        tSKDBRG.Enabled = False
        tSNMBRG.Enabled = False
        Dim valIsi1 As Double = 0
        Dim valIsi2 As Double = 0

        Double.TryParse(txtISI1.Text, NumberStyles.Any, koma, valIsi1)
        Double.TryParse(txtISI2.Text, NumberStyles.Any, koma, valIsi2)

        ' ---- ISI1 menentukan STN2 & ISI2 ----
        If valIsi1 > 2 Then
            txtSTN2.Enabled = True
            txtISI2.Enabled = True
        Else
            txtSTN2.Enabled = False
            txtISI2.Enabled = False
            txtSTN3.Enabled = False
            Exit Sub
        End If

        ' ---- ISI2 menentukan STN3 ----
        If valIsi2 > 2 Then
            txtSTN3.Enabled = True
        Else
            txtSTN3.Enabled = False
        End If
    End Sub

    Private Sub btnTAMBAH_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTAMBAH.Click
        ' === Ambil ID menu dari properti Tag form ===
        Dim idMenu As String = Me.Tag.ToString()

        ' === Ambil hak akses user aktif ===
        Dim akses = GetAkses(KodeUserLogin, idMenu)

        ' === Cek apakah user boleh tambah ===
        If Not akses("tambah") Then
            MessageBox.Show("Tidak bisa akses tambah", "Akses Ditolak", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        Else
            SetButtonState(Me, False)
            statusMode = "tambah"
            KosongkanInput()
            EnabledLoad()
        End If
    End Sub

    Private Sub btnUBAH_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUBAH.Click
        ' === Ambil ID menu dari properti Tag form ===
        Dim idMenu As String = Me.Tag.ToString()

        ' === Ambil hak akses user aktif ===
        Dim akses = GetAkses(KodeUserLogin, idMenu)

        If Not akses("ubah") Then
            MessageBox.Show("Tidak bisa akses ubah", "Akses Ditolak", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        Else
            Dim kodebrg As String = txtKDBRG.Text.Trim()

            ' cek apakah kosong
            If kodebrg Is Nothing OrElse String.IsNullOrWhiteSpace(txtKDBRG.Text) Then
                MessageBox.Show("Tidak ada data yang bisa di edit", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            EnabledLoad()
            SetButtonState(Me, False)
            statusMode = "ubah"
        End If
    End Sub

    Private Sub btnHAPUS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHAPUS.Click
        ' === Ambil ID menu dari properti Tag form ===
        Dim idMenu As String = Me.Tag.ToString()

        ' === Ambil hak akses user aktif ===
        Dim akses = GetAkses(KodeUserLogin, idMenu)

        If Not akses("hapus") Then
            MessageBox.Show("Tidak bisa akses hapus", "Akses Ditolak", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        Else
            Try
                Dim kodebrg As String = txtKDBRG.Text.Trim()

                If String.IsNullOrEmpty(kodebrg) Then
                    MessageBox.Show("Tidak ada data yang bisa dihapus.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If

                If MessageBox.Show("Yakin hapus " & kodebrg & " ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then
                    Exit Sub
                End If

                HapusStok(txtKDBRG.Text)
                KosongkanInput()
                SetButtonState(Me, True)
                statusMode = ""
                DisabledLoad()

                MessageBox.Show(kodebrg & " berhasil dihapus.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information)

            Catch ex As Exception
                MessageBox.Show(ex.Message, "Error Hapus", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    Public Sub HapusStok(ByVal kdBrg As String)

        Try
            BukaKoneksi()
            Using Trans = Conn.BeginTransaction()
                Using cmd As New OdbcCommand("DELETE FROM zstok WHERE kodebrg = ?", Conn, Trans)
                    cmd.Parameters.AddWithValue("@kodebrg", kdBrg)
                    cmd.ExecuteNonQuery()
                End Using

                Trans.Commit()
            End Using

        Catch ex As Exception
            MsgBox("Gagal menghapus data barang: " & ex.Message, vbCritical)
        End Try
    End Sub

    Private Sub btnSIMPAN_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSIMPAN.Click
        Try
            ' === 1. Validasi wajib diisi ===
            If txtKDBRG.Text.Trim() = "" Or txtNMBRG.Text.Trim() = "" Or txtKDMERK.Text.Trim() = "" Or txtKDGOL.Text.Trim() = "" Or txtKDGRUP.Text.Trim() = "" Then
                MessageBox.Show("Semua field wajib diisi!", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning)

                ' === 3. Cek apakah KDUSER sudah terdaftar di zusers ===
            Else
                BukaKoneksi()
                Dim CekBrg As String = "SELECT COUNT(*) FROM zstok WHERE kodebrg = ?"
                Dim brgExist As Boolean = False
                Using cmdCekBrg As New OdbcCommand(CekBrg, Conn)
                    cmdCekBrg.Parameters.AddWithValue("@kodebrg", txtKDBRG.Text.Trim())
                    brgExist = Convert.ToInt32(cmdCekBrg.ExecuteScalar())
                End Using

                If statusMode.ToLower() = "tambah" Then
                    If brgExist = False Then
                        ' === 5. Semua validasi lolos → simpan data ===
                        SimpanStok(txtKDBRG.Text, txtNMBRG.Text, txtKDMERK.Text, txtKDGOL.Text, txtKDGRUP.Text, txtSTN1.Text, txtSTN2.Text, txtSTN3.Text, txtISI1.Text, txtISI2.Text, txtHRGBELI.Text, statusMode)
                        MessageBox.Show("Data Barang berhasil disimpan", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information)

                        statusMode = ""
                        SetButtonState(Me, True)
                        DisabledLoad()

                    ElseIf brgExist = True Then
                        MessageBox.Show("Kode Barang sudah terdaftar!" & vbCrLf &
                                        "Tidak dapat menyimpan data baru.", "Validasi",
                                        MessageBoxButtons.OK, MessageBoxIcon.Warning)

                    End If
                ElseIf statusMode.ToLower() = "ubah" Then
                    ' === 5. Semua validasi lolos → simpan data ===
                    SimpanStok(txtKDBRG.Text, txtNMBRG.Text, txtKDMERK.Text, txtKDGOL.Text, txtKDGRUP.Text, txtSTN1.Text, txtSTN2.Text, txtSTN3.Text, txtISI1.Text, txtISI2.Text, txtHRGBELI.Text, statusMode)
                    MessageBox.Show("Data Barang berhasil disimpan", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information)

                    statusMode = ""
                    SetButtonState(Me, True)
                    DisabledLoad()
                Else
                    MessageBox.Show("Mode penyimpanan tidak dikenali!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                End If
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error Simpan", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function ToDouble(ByVal txt As String) As Double
        If txt.Trim = "" Then Return 0

        Dim result As Double

        Double.TryParse(txt, NumberStyles.Any, koma, result)

        Return result
    End Function

    Public Sub SimpanStok(ByVal kdBrg As String,
                          ByVal nmBrg As String,
                          ByVal kdMerk As String,
                          ByVal kdGol As String,
                          ByVal kdGrp As String,
                          ByVal Stn1 As String,
                          ByVal Stn2 As String,
                          ByVal Stn3 As String,
                          ByVal Isi1 As String,
                          ByVal Isi2 As String,
                          ByVal kdHrg As String,
                          ByVal status As String)

        Dim Trans As OdbcTransaction = Nothing

        Try
            If kdBrg = "" Then Throw New Exception("Kode Barang tidak boleh kosong!")

            BukaKoneksi()
            Trans = Conn.BeginTransaction()

            ' Jika mode UBAH, hapus dulu data lama
            If status.ToLower() = "ubah" Then
                KodeLama = kdBrg
                Using cmdDel As New OdbcCommand("DELETE FROM zstok WHERE kodebrg = ?", Conn, Trans)
                    cmdDel.Parameters.AddWithValue("@kodebrg", kdBrg)
                    cmdDel.ExecuteNonQuery()
                End Using

                Using cmd As New OdbcCommand("INSERT INTO zstok (kodebrg, namabrg, kodemerk, kodegol, kodegrup, satuan1, satuan2, satuan3, isi1, isi2, hrgbeli) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)", Conn, Trans)
                    cmd.Parameters.AddWithValue("@kodebrg", kdBrg)
                    cmd.Parameters.AddWithValue("@namabrg", nmBrg)
                    cmd.Parameters.AddWithValue("@kodemerk", kdMerk)
                    cmd.Parameters.AddWithValue("@kodegol", kdGol)
                    cmd.Parameters.AddWithValue("@kodegrup", kdGrp)
                    cmd.Parameters.AddWithValue("@satuan1", Stn1)
                    cmd.Parameters.AddWithValue("@satuan2", Stn2)
                    cmd.Parameters.AddWithValue("@satuan3", Stn3)
                    cmd.Parameters.AddWithValue("@isi1", ToDouble(Isi1))
                    cmd.Parameters.AddWithValue("@isi2", ToDouble(Isi2))
                    cmd.Parameters.AddWithValue("@hrgbeli", ToDouble(kdHrg))
                    cmd.ExecuteNonQuery()
                End Using

            ElseIf status.ToLower() = "tambah" Then
                Using cmd As New OdbcCommand("INSERT INTO zstok (kodebrg, namabrg, kodemerk, kodegol, kodegrup, satuan1, satuan2, satuan3, isi1, isi2, hrgbeli) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)", Conn, Trans)
                    cmd.Parameters.AddWithValue("@kodebrg", kdBrg)
                    cmd.Parameters.AddWithValue("@namabrg", nmBrg)
                    cmd.Parameters.AddWithValue("@kodemerk", kdMerk)
                    cmd.Parameters.AddWithValue("@kodegol", kdGol)
                    cmd.Parameters.AddWithValue("@kodegrup", kdGrp)
                    cmd.Parameters.AddWithValue("@satuan1", Stn1)
                    cmd.Parameters.AddWithValue("@satuan2", Stn2)
                    cmd.Parameters.AddWithValue("@satuan3", Stn3)
                    cmd.Parameters.AddWithValue("@isi1", ToDouble(Isi1))
                    cmd.Parameters.AddWithValue("@isi2", ToDouble(Isi2))
                    cmd.Parameters.AddWithValue("@hrgbeli", ToDouble(kdHrg))
                    cmd.ExecuteNonQuery()
                End Using
            End If

            ' === Commit transaksi jika semua berhasil ===
            Trans.Commit()

        Catch ex As Exception
            ' Rollback jika terjadi error
            If Trans IsNot Nothing Then
                Try
                    Trans.Rollback()
                Catch
                End Try
            End If

            KodeLama = kdBrg
            MsgBox("Gagal menyimpan data barang: " & ex.Message, vbCritical)
        End Try
    End Sub

    Private Sub btnBATAL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBATAL.Click
        SetButtonState(Me, True)
        statusMode = ""
        DisabledLoad()
    End Sub

    Private Sub tUser_TextChanged(ByVal sender As Object, ByVal e As EventArgs) _
    Handles tSKDBRG.TextChanged, tSNMBRG.TextChanged

        ' ⛔ Jangan jalankan logika kalau user belum mengetik
        If Not isUserTypingSearch Then Exit Sub

        Dim isiKODE As Boolean =
            ModPlaceholder.GetRealText(tSKDBRG) <> ""

        Dim isiNAMA As Boolean =
            ModPlaceholder.GetRealText(tSNMBRG) <> ""

        tSNMBRG.Enabled = Not isiKODE
        tSKDBRG.Enabled = Not isiNAMA
    End Sub

    Private Sub btnCARI_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCARI.Click
        Dim f As New ItDtStok()

        Dim kodebrg As String = ModPlaceholder.GetRealText(tSKDBRG)
        Dim namabrg As String = ModPlaceholder.GetRealText(tSNMBRG)

        ' Load data sesuai filter
        f.Owner = Me
        f.Show()
        f.LoadDataStok(kodebrg, namabrg)

        ' === Clear filter setelah pencarian ===
        tSKDBRG.Clear()
        tSNMBRG.Clear()

        ModPlaceholder.SetPlaceholder(tSKDBRG, "KODE BARANG")
        ModPlaceholder.SetPlaceholder(tSNMBRG, "NAMA BARANG")

        isUserTypingSearch = False
        tSKDBRG.Enabled = True
        tSNMBRG.Enabled = True
    End Sub

    Private skipLeaveMerek As Boolean = False
    Private skipLeaveGolongan As Boolean = False
    Private skipLeaveGrup As Boolean = False

    Private Sub OpenMerek(ByVal keyword As String, ByVal mode As String)
        If keyword.Trim() = "" Then Exit Sub

        Dim f As New ItMerek
        f.Owner = Me
        f.Caller = mode
        f.Show()
        f.LoadDataMerek(keyword.Trim(), mode)
    End Sub

    Private Sub txtKDMERK_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtKDMERK.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            skipLeaveMerek = True
            OpenMerek(txtKDMERK.Text, "KODE")
        End If
    End Sub

    Private Sub txtNMMERK_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtNMMERK.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            skipLeaveMerek = True
            OpenMerek(txtNMMERK.Text, "NAMA")
        End If
    End Sub

    Private Sub txtKDMERK_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtKDMERK.Leave
        If skipLeaveMerek Then
            skipLeaveMerek = False
            Exit Sub
        End If

        OpenMerek(txtKDMERK.Text, "KODE")
    End Sub

    Private Sub txtNMMERK_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtNMMERK.Leave
        If skipLeaveMerek Then
            skipLeaveMerek = False
            Exit Sub
        End If
        OpenMerek(txtNMMERK.Text, "NAMA")
    End Sub

    Private Sub OpenGolongan(ByVal keyword As String, ByVal mode As String)
        If keyword.Trim() = "" Then Exit Sub

        Dim f As New ItGolongan
        f.Owner = Me
        f.Caller = mode
        f.Show()
        f.LoadDataGolongan(keyword.Trim(), mode)
    End Sub

    Private Sub txtKDGOL_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtKDGOL.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            skipLeaveGolongan = True
            OpenGolongan(txtKDGOL.Text, "KODE")
        End If
    End Sub

    Private Sub txtNMGOL_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtNMGOL.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            skipLeaveGolongan = True
            OpenGolongan(txtNMGOL.Text, "NAMA")
        End If
    End Sub

    Private Sub txtKDGOL_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtKDGOL.Leave
        If skipLeaveGolongan Then
            skipLeaveGolongan = False
            Exit Sub
        End If
        OpenGolongan(txtKDGOL.Text, "KODE")
    End Sub

    Private Sub txtNMGOL_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtNMGOL.Leave
        If skipLeaveGolongan Then
            skipLeaveGolongan = False
            Exit Sub
        End If
        OpenGolongan(txtNMGOL.Text, "NAMA")
    End Sub

    Private Sub OpenGrup(ByVal keyword As String, ByVal mode As String)
        If keyword.Trim() = "" Then Exit Sub

        Dim f As New ItGrup
        f.Owner = Me
        f.Caller = mode
        f.Show()
        f.LoadDataGrup(keyword.Trim(), mode)
    End Sub

    Private Sub txtKDGRUP_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtKDGRUP.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            skipLeaveGrup = True
            OpenGrup(txtKDGRUP.Text, "KODE")
        End If
    End Sub

    Private Sub txtNMGRUP_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtNMGRUP.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            skipLeaveGrup = True
            OpenGrup(txtNMGRUP.Text, "NAMA")
        End If
    End Sub

    Private Sub txtKDGRUP_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtKDGRUP.Leave
        If skipLeaveGrup Then
            skipLeaveGrup = False
            Exit Sub
        End If
        OpenGrup(txtKDGRUP.Text, "KODE")
    End Sub

    Private Sub txtNMGRUP_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtNMGRUP.Leave
        If skipLeaveGrup Then
            skipLeaveGrup = False
            Exit Sub
        End If
        OpenGrup(txtNMGRUP.Text, "NAMA")
    End Sub

    Private Sub FormatOnLeave(ByVal txt As TextBox)

        If txt.Text.Trim = "" Then Exit Sub

        Dim angka As Double

        If Double.TryParse(txt.Text, NumberStyles.Any, koma, angka) Then
            txt.Text = angka.ToString("N2", koma)
        End If

    End Sub

    Private Sub txtISI1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtISI1.TextChanged
        If isLoadingData Then Exit Sub
        Dim val As Double
        If Double.TryParse(txtISI1.Text, NumberStyles.Any, koma, val) AndAlso val > 2 Then
            txtSTN2.Enabled = True
            txtISI2.Enabled = True
        ElseIf val < 2 Then
            txtSTN2.Enabled = False
            txtISI2.Enabled = False
        End If

    End Sub

    Private Sub txtISI2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtISI2.TextChanged
        If isLoadingData Then Exit Sub
        Dim val As Double
        If Double.TryParse(txtISI1.Text, NumberStyles.Any, koma, val) AndAlso val > 2 Then
            txtSTN3.Enabled = True
        ElseIf val < 2 Then
            txtSTN3.Enabled = False
        End If

    End Sub

    Private Sub txtHRGBELI_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtHRGBELI.TextChanged

    End Sub

    Private Sub txtISI1_Leave(ByVal sender As Object, ByVal e As EventArgs) Handles txtISI1.Leave
        FormatOnLeave(txtISI1)
    End Sub

    Private Sub txtISI2_Leave(ByVal sender As Object, ByVal e As EventArgs) Handles txtISI2.Leave
        FormatOnLeave(txtISI2)
    End Sub

    Private Sub txtHRGBELI_Leave(ByVal sender As Object, ByVal e As EventArgs) Handles txtHRGBELI.Leave
        FormatOnLeave(txtHRGBELI)
    End Sub

    Private Sub btnHARGA_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHARGA.Click
        Dim kdBrg As String = txtKDBRG.Text.Trim()
        Dim valIsi1 As Integer = 0
        Dim valIsi2 As Integer = 0

        Double.TryParse(txtISI1.Text, NumberStyles.Any, koma, valIsi1)
        Double.TryParse(txtISI2.Text, NumberStyles.Any, koma, valIsi2)

        ' Kirim data ke form harga
        Dim f As New FDtHarga()
        f.KodeBarang = kdBrg
        f.Isi1 = CInt(Math.Floor(valIsi1))
        f.Isi2 = CInt(Math.Floor(valIsi2))

        f.ShowDialog()
    End Sub

    Private Sub OnlyNumber_KeyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs) _
        Handles txtISI1.KeyPress, txtISI2.KeyPress, txtHRGBELI.KeyPress

        AngkaHelper.HanyaAngka(e)
    End Sub
End Class