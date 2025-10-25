Imports System.Data.Odbc

Public Class FStok
    Private statusMode As String = ""   ' status: "TAMBAH" / "UBAH"
    Public KodeLama As String = ""

    Dim isi1 As Integer
    Dim isi2 As Integer

    Public Sub SetMerek(ByVal kode As String, ByVal nama As String)
        txtKDMERK.Text = kode
    End Sub

    Public Sub SetGolongan(ByVal kode As String, ByVal nama As String)
        txtKDGOL.Text = kode
    End Sub

    Public Sub SetGrup(ByVal kode As String, ByVal nama As String)
        txtKDGRUP.Text = kode
    End Sub

    Private Sub FStok_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetButtonState(Me, True)
        BukaKoneksi()
        DisabledLoad()
    End Sub

    Public Sub LoadData(ByVal kodebrg As String)
        Try
            BukaKoneksi()

            Dim sql As String = "SELECT kodebrg, namabrg, kodemerk, kodegol, kodegrup, satuan1, satuan2, satuan3, isi1, isi2, hrgbeli FROM zstok WHERE kodebrg = ?"
            Cmd = New OdbcCommand(sql, Conn)
            Cmd.Parameters.AddWithValue("", kodebrg)

            Rd = Cmd.ExecuteReader()

            If Rd.Read() Then
                ' === Isi field ke textbox di form ===
                txtKDMERK.Text = Rd("kodemerk").ToString()
                txtKDGOL.Text = Rd("kodegol").ToString()
                txtKDGRUP.Text = Rd("kodegrup").ToString()
                txtKDBRG.Text = Rd("kodebrg").ToString()
                txtNMBRG.Text = Rd("namabrg").ToString()
                txtSTN1.Text = Rd("satuan1").ToString()
                txtISI1.Text = Rd("isi1").ToString()
                txtSTN2.Text = Rd("satuan2").ToString()
                txtISI2.Text = Rd("isi2").ToString()
                txtSTN3.Text = Rd("satuan3").ToString()
                txtHRGBELI.Text = Rd("hrgbeli").ToString()
                ' Kalau ada field lain (misalnya level, aktif, dsb) bisa ditambah di sini
                ' txtLEVEL.Text = Rd("level").ToString()
            Else
                MessageBox.Show("Data Barang tidak ditemukan.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

        Catch ex As Exception
            MessageBox.Show("Gagal memuat data user: " & ex.Message)
        End Try
    End Sub

    Private Sub KosongkanInput()
        txtKDMERK.Text = ""
        txtKDGOL.Text = ""
        txtKDGRUP.Text = ""
        txtKDBRG.Text = ""
        txtNMBRG.Text = ""
        txtSTN1.Text = ""
        txtISI1.Text = ""
        txtSTN2.Text = ""
        txtISI2.Text = ""
        txtSTN3.Text = ""
        txtHRGBELI.Text = ""
        txtGAMBAR.Text = ""
        tSKDBRG.Text = ""
        tSNMBRG.Text = ""
    End Sub

    Private Sub DisabledLoad()
        txtKDMERK.Enabled = False
        txtKDGOL.Enabled = False
        txtKDGRUP.Enabled = False
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
        tSKDBRG.Enabled = True
        tSNMBRG.Enabled = True
    End Sub

    Private Sub EnabledLoad()
        txtKDMERK.Enabled = True
        txtKDGOL.Enabled = True
        txtKDGRUP.Enabled = True
        txtKDBRG.Enabled = True
        txtNMBRG.Enabled = True
        txtSTN1.Enabled = True
        txtISI1.Enabled = True
        txtHRGBELI.Enabled = True
        btnUPLOAD.Enabled = True
        txtGAMBAR.Enabled = True
        tSKDBRG.Enabled = False
        tSNMBRG.Enabled = False
    End Sub

    Private Sub btnTAMBAH_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTAMBAH.Click
        SetButtonState(Me, False)
        statusMode = "tambah"
        KosongkanInput()
        EnabledLoad()
    End Sub

    Private Sub btnUBAH_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUBAH.Click
        Dim kodebrg As String = txtKDBRG.Text.Trim()

        ' cek apakah kosong
        If kodebrg Is Nothing OrElse String.IsNullOrWhiteSpace(txtKDBRG.Text) Then
            MessageBox.Show("Tidak ada data yang bisa di edit", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        EnabledLoad()
        SetButtonState(Me, False)
        statusMode = "ubah"
    End Sub

    Private Sub btnHAPUS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHAPUS.Click
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
                    cmd.Parameters.AddWithValue("@isi1", Isi1)
                    cmd.Parameters.AddWithValue("@isi2", Isi2)
                    cmd.Parameters.AddWithValue("@hrgbeli", kdHrg)
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
                    cmd.Parameters.AddWithValue("@isi1", Isi1)
                    cmd.Parameters.AddWithValue("@isi2", Isi2)
                    cmd.Parameters.AddWithValue("@hrgbeli", kdHrg)
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

    Private Sub tUser_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles tSKDBRG.TextChanged, tSNMBRG.TextChanged

        Dim txt As TextBox = CType(sender, TextBox)
        If txt Is tSKDBRG Then
            tSNMBRG.Enabled = (tSKDBRG.Text.Trim() = "")
        ElseIf txt Is tSNMBRG Then
            tSKDBRG.Enabled = (tSNMBRG.Text.Trim() = "")
        End If
    End Sub

    Private Sub btnCARI_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCARI.Click
        Dim f As New ItDtStok()

        Dim kodebrg As String = tSKDBRG.Text.Trim()
        Dim namabrg As String = tSNMBRG.Text.Trim()

        ' Load data sesuai filter
        f.Owner = Me
        f.Show()
        f.LoadDataStok(kodebrg, namabrg)

        ' === Clear filter setelah pencarian ===
        tSKDBRG.Clear()
        tSNMBRG.Clear()
    End Sub

    Private Sub txtKDMERK_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtKDMERK.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            If txtKDMERK.Text.Trim() <> "" Then
                Dim f As New ItMerek
                f.Owner = Me
                f.Show()
                f.LoadDataMerek(txtKDMERK.Text.Trim())
            End If
        End If
    End Sub

    Private Sub txtKDGOL_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtKDGOL.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            If txtKDMERK.Text.Trim() <> "" Then
                Dim f As New ItGolongan
                f.Owner = Me
                f.Show()
                f.LoadDataGolongan(txtKDGOL.Text.Trim())
            End If
        End If
    End Sub

    Private Sub txtKDGRUP_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtKDGRUP.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            If txtKDMERK.Text.Trim() <> "" Then
                Dim f As New ItGrup
                f.Owner = Me
                f.Show()
                f.LoadDataGrup(txtKDGRUP.Text.Trim())
            End If
        End If
    End Sub

    Private Sub txtISI1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtISI1.TextChanged
        If Integer.TryParse(txtISI1.Text, isi1) AndAlso isi1 > 2 Then
            txtSTN2.Enabled = True
            txtISI2.Enabled = True
        ElseIf isi1 < 2 Then
            txtSTN2.Enabled = False
            txtISI2.Enabled = False
        End If
    End Sub

    Private Sub txtISI2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtISI2.TextChanged
        If Integer.TryParse(txtISI2.Text, isi2) AndAlso isi2 > 2 Then
            txtSTN3.Enabled = True
        ElseIf isi2 < 2 Then
            txtSTN3.Enabled = False
        End If
    End Sub
End Class