Imports System.Data.Odbc

Public Class FKustomer
    Private statusMode As String = ""   ' status: "TAMBAH" / "UBAH"
    Public KodeLama As String = ""

    Public Sub SetTipe(ByVal kodet As String, ByVal namat As String)
        txtKDTIPE.Text = kodet
    End Sub

    Public Sub SetArea(ByVal kodea As String, ByVal namaa As String)
        txtKDAREA.Text = kodea
    End Sub

    Private Sub FKustomer_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetButtonState(Me, True)
        BukaKoneksi()
        DisabledLoad()
    End Sub

    Public Sub LoadData(ByVal kodekust As String)
        Try
            BukaKoneksi()

            Dim sql As String = "SELECT kodekust, namakust, kodear, kodetipe, alamat, kota, kodehrg, ktp, npwp FROM zkustomer WHERE kodekust = ?"
            Cmd = New OdbcCommand(sql, Conn)
            Cmd.Parameters.AddWithValue("", kodekust)

            Rd = Cmd.ExecuteReader()

            If Rd.Read() Then
                ' === Isi field ke textbox di form ===
                txtKDKUST.Text = Rd("kodekust").ToString()
                txtNMKUST.Text = Rd("namakust").ToString()
                txtALAMAT.Text = Rd("alamat").ToString()
                txtKOTA.Text = Rd("kota").ToString()
                txtKDHARGA.Text = Rd("kodehrg").ToString()
                txtKTP.Text = Rd("ktp").ToString()
                txtNPWP.Text = Rd("npwp").ToString()
                txtKDTIPE.Text = Rd("kodetipe").ToString()
                txtKDAREA.Text = Rd("kodear").ToString()
                ' Kalau ada field lain (misalnya level, aktif, dsb) bisa ditambah di sini
                ' txtLEVEL.Text = Rd("level").ToString()
            Else
                MessageBox.Show("Data Kustomer tidak ditemukan.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

        Catch ex As Exception
            MessageBox.Show("Gagal memuat data user: " & ex.Message)
        End Try
    End Sub

    Private Sub KosongkanInput()
        txtKDKUST.Text = ""
        txtNMKUST.Text = ""
        txtALAMAT.Text = ""
        txtKOTA.Text = ""
        txtKDHARGA.Text = ""
        txtKTP.Text = ""
        txtNPWP.Text = ""
        txtGAMBAR.Text = ""
        txtKDTIPE.Text = ""
        txtKDAREA.Text = ""
        tSKDKUST.Text = ""
        tSNMKUST.Text = ""
    End Sub

    Private Sub DisabledLoad()
        txtKDKUST.Enabled = False
        txtNMKUST.Enabled = False
        txtALAMAT.Enabled = False
        txtKOTA.Enabled = False
        txtKDHARGA.Enabled = False
        txtKTP.Enabled = False
        txtNPWP.Enabled = False
        txtKDTIPE.Enabled = False
        txtKDAREA.Enabled = False
        txtGAMBAR.Enabled = False
        btnUPLOAD.Enabled = False
        tSKDKUST.Enabled = True
        tSNMKUST.Enabled = True
    End Sub

    Private Sub EnabledLoad()
        txtKDKUST.Enabled = True
        txtNMKUST.Enabled = True
        txtALAMAT.Enabled = True
        txtKOTA.Enabled = True
        txtKDHARGA.Enabled = True
        txtKTP.Enabled = True
        txtNPWP.Enabled = True
        txtKDTIPE.Enabled = True
        txtKDAREA.Enabled = True
        txtGAMBAR.Enabled = True
        btnUPLOAD.Enabled = True
        tSKDKUST.Enabled = False
        tSNMKUST.Enabled = False
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
            Dim kodekust As String = txtKDKUST.Text.Trim()

            ' cek apakah kosong
            If kodekust Is Nothing OrElse String.IsNullOrWhiteSpace(txtKDKUST.Text) Then
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
                Dim kodekust As String = txtKDKUST.Text.Trim()

                If String.IsNullOrEmpty(kodekust) Then
                    MessageBox.Show("Tidak ada data yang bisa dihapus.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If

                If MessageBox.Show("Yakin hapus " & kodekust & " ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then
                    Exit Sub
                End If

                HapusKustomer(txtKDKUST.Text)
                KosongkanInput()
                SetButtonState(Me, True)
                statusMode = ""
                DisabledLoad()

                MessageBox.Show(kodekust & " berhasil dihapus.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information)

            Catch ex As Exception
                MessageBox.Show(ex.Message, "Error Hapus", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    Public Sub HapusKustomer(ByVal kdKus As String)

        Try
            BukaKoneksi()
            Using Trans = Conn.BeginTransaction()
                Using cmd As New OdbcCommand("DELETE FROM zkustomer WHERE kodekust = ?", Conn, Trans)
                    cmd.Parameters.AddWithValue("@kodesls", kdKus)
                    cmd.ExecuteNonQuery()
                End Using

                Trans.Commit()
            End Using

        Catch ex As Exception
            MsgBox("Gagal menghapus data kustomer: " & ex.Message, vbCritical)
        End Try
    End Sub

    Private Sub btnSIMPAN_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSIMPAN.Click
        Try
            ' === 1. Validasi wajib diisi ===
            If txtKDKUST.Text.Trim() = "" Or txtNMKUST.Text.Trim() = "" Or txtALAMAT.Text.Trim() = "" Or txtKDHARGA.Text.Trim() = "" Or txtKOTA.Text.Trim() = "" Or txtKTP.Text.Trim() = "" Or txtNPWP.Text.Trim() = "" Or txtKDTIPE.Text.Trim() = "" Or txtKDAREA.Text.Trim() = "" Then
                MessageBox.Show("Semua field wajib diisi!", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning)

                ' === 3. Cek apakah KDUSER sudah terdaftar di zusers ===
            Else
                BukaKoneksi()
                Dim CekKus As String = "SELECT COUNT(*) FROM zkustomer WHERE kodekust = ?"
                Dim kustExist As Boolean = False
                Using cmdCekKus As New OdbcCommand(CekKus, Conn)
                    cmdCekKus.Parameters.AddWithValue("@kodekust", txtKDKUST.Text.Trim())
                    kustExist = Convert.ToInt32(cmdCekKus.ExecuteScalar())
                End Using

                If statusMode.ToLower() = "tambah" Then
                    If kustExist = False Then
                        ' === 5. Semua validasi lolos → simpan data ===
                        SimpanKustomer(txtKDKUST.Text, txtNMKUST.Text, txtALAMAT.Text, txtKOTA.Text, txtKDHARGA.Text, txtKTP.Text, txtNPWP.Text, txtKDTIPE.Text, txtKDAREA.Text, statusMode)
                        MessageBox.Show("Data Kustomer berhasil disimpan", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information)

                        statusMode = ""
                        SetButtonState(Me, True)
                        DisabledLoad()

                    ElseIf kustExist = True Then
                        MessageBox.Show("Kode Kustomer sudah terdaftar!" & vbCrLf &
                                        "Tidak dapat menyimpan data baru.", "Validasi",
                                        MessageBoxButtons.OK, MessageBoxIcon.Warning)

                    End If
                ElseIf statusMode.ToLower() = "ubah" Then
                    ' === 5. Semua validasi lolos → simpan data ===
                    SimpanKustomer(txtKDKUST.Text, txtNMKUST.Text, txtALAMAT.Text, txtKOTA.Text, txtKDHARGA.Text, txtKTP.Text, txtNPWP.Text, txtKDTIPE.Text, txtKDAREA.Text, statusMode)
                    MessageBox.Show("Data Kustomer berhasil disimpan", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information)

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

    Public Sub SimpanKustomer(ByVal kdKus As String,
                          ByVal nmKus As String,
                          ByVal alamat As String,
                          ByVal kota As String,
                          ByVal kdHrg As String,
                          ByVal ktp As String,
                          ByVal npwp As String,
                          ByVal kdTipe As String,
                          ByVal kdAr As String,
                          ByVal status As String)

        Dim Trans As OdbcTransaction = Nothing

        Try
            If kdKus = "" Then Throw New Exception("Kode Kustomer tidak boleh kosong!")

            BukaKoneksi()
            Trans = Conn.BeginTransaction()

            ' Jika mode UBAH, hapus dulu data lama
            If status.ToLower() = "ubah" Then
                KodeLama = kdKus
                Using cmdDel As New OdbcCommand("DELETE FROM zkustomer WHERE kodekust = ?", Conn, Trans)
                    cmdDel.Parameters.AddWithValue("@kodekust", kdKus)
                    cmdDel.ExecuteNonQuery()
                End Using

                Using cmd As New OdbcCommand("INSERT INTO zkustomer (kodekust, namakust, alamat, kota, kodehrg, ktp, kodetipe, kodear, npwp) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?)", Conn, Trans)
                    cmd.Parameters.AddWithValue("@kodekust", kdKus)
                    cmd.Parameters.AddWithValue("@namakust", nmKus)
                    cmd.Parameters.AddWithValue("@alamat", alamat)
                    cmd.Parameters.AddWithValue("@kota", kota)
                    cmd.Parameters.AddWithValue("@kodehrg", kdHrg)
                    cmd.Parameters.AddWithValue("@ktp", ktp)
                    cmd.Parameters.AddWithValue("@kodetipe", kdTipe)
                    cmd.Parameters.AddWithValue("@kodear", kdAr)
                    cmd.Parameters.AddWithValue("@npwp", npwp)
                    cmd.ExecuteNonQuery()
                End Using

            ElseIf status.ToLower() = "tambah" Then
                Using cmd As New OdbcCommand("INSERT INTO zkustomer (kodekust, namakust, alamat, kota, kodehrg, ktp, kodetipe, kodear, npwp) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?)", Conn, Trans)
                    cmd.Parameters.AddWithValue("@kodekust", kdKus)
                    cmd.Parameters.AddWithValue("@namakust", nmKus)
                    cmd.Parameters.AddWithValue("@alamat", alamat)
                    cmd.Parameters.AddWithValue("@kota", kota)
                    cmd.Parameters.AddWithValue("@kodehrg", kdHrg)
                    cmd.Parameters.AddWithValue("@ktp", ktp)
                    cmd.Parameters.AddWithValue("@kodetipe", kdTipe)
                    cmd.Parameters.AddWithValue("@kodear", kdAr)
                    cmd.Parameters.AddWithValue("@npwp", npwp)
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

            KodeLama = kdKus
            MsgBox("Gagal menyimpan data kustomer: " & ex.Message, vbCritical)
        End Try
    End Sub

    Private Sub btnBATAL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBATAL.Click
        SetButtonState(Me, True)
        statusMode = ""
        DisabledLoad()
    End Sub

    Private Sub tUser_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles tSKDKUST.TextChanged, tSNMKUST.TextChanged

        Dim txt As TextBox = CType(sender, TextBox)
        If txt Is tSKDKUST Then
            tSNMKUST.Enabled = (tSKDKUST.Text.Trim() = "")
        ElseIf txt Is tSNMKUST Then
            tSKDKUST.Enabled = (tSNMKUST.Text.Trim() = "")
        End If
    End Sub

    Private Sub btnCARI_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCARI.Click
        Dim f As New ItDtKustomer()

        Dim kodekust As String = tSKDKUST.Text.Trim()
        Dim namakust As String = tSNMKUST.Text.Trim()

        ' Load data sesuai filter
        f.Owner = Me
        f.Show()
        f.LoadDataKustomer(kodekust, namakust)

        ' === Clear filter setelah pencarian ===
        tSKDKUST.Clear()
        tSNMKUST.Clear()
    End Sub

    Private Sub txtKDTIPE_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles txtKDTIPE.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            If txtKDTIPE.Text.Trim() <> "" Then
                Dim f As New ItTipe
                f.Owner = Me
                f.Show()
                f.LoadDataTipe(txtKDTIPE.Text.Trim())
            End If
        End If
    End Sub

    Private Sub txtKDAREA_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles txtKDAREA.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            If txtKDTIPE.Text.Trim() <> "" Then
                Dim f As New ItArea
                f.Owner = Me
                f.Show()
                f.LoadDataArea(txtKDAREA.Text.Trim())
            End If
        End If
    End Sub
End Class