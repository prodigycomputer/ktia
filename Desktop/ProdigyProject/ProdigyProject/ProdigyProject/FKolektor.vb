Imports System.Data.Odbc

Public Class FKolektor
    Private statusMode As String = ""   ' status: "TAMBAH" / "UBAH"
    Public KodeLama As String = ""

    Private Sub FKolektor_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetButtonState(Me, True)
        BukaKoneksi()
        DisabledLoad()
    End Sub

    Public Sub LoadData(ByVal kodekol As String)
        Try
            BukaKoneksi()

            Dim sql As String = "SELECT kodekol, namakol, alamat, kota, ktp, npwp FROM zkolektor WHERE kodekol = ?"
            Cmd = New OdbcCommand(sql, Conn)
            Cmd.Parameters.AddWithValue("", kodekol)

            Rd = Cmd.ExecuteReader()

            If Rd.Read() Then
                ' === Isi field ke textbox di form ===
                txtKDKOL.Text = Rd("kodekol").ToString()
                txtNMKOL.Text = Rd("namakol").ToString()
                txtALAMAT.Text = Rd("alamat").ToString()
                txtKOTA.Text = Rd("kota").ToString()
                txtKTP.Text = Rd("ktp").ToString()
                txtNPWP.Text = Rd("npwp").ToString()
                ' Kalau ada field lain (misalnya level, aktif, dsb) bisa ditambah di sini
                ' txtLEVEL.Text = Rd("level").ToString()
            Else
                MessageBox.Show("Data Kolektor tidak ditemukan.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

        Catch ex As Exception
            MessageBox.Show("Gagal memuat data user: " & ex.Message)
        End Try
    End Sub

    Private Sub KosongkanInput()
        txtKDKOL.Text = ""
        txtNMKOL.Text = ""
        txtALAMAT.Text = ""
        txtKOTA.Text = ""
        txtKTP.Text = ""
        txtNPWP.Text = ""
        txtGAMBAR.Text = ""
        tSKDKOL.Text = ""
        tSNMKOL.Text = ""
    End Sub

    Private Sub DisabledLoad()
        txtKDKOL.Enabled = False
        txtNMKOL.Enabled = False
        txtALAMAT.Enabled = False
        txtKOTA.Enabled = False
        txtKTP.Enabled = False
        txtNPWP.Enabled = False
        txtGAMBAR.Enabled = False
        btnUPLOAD.Enabled = False
        tSKDKOL.Enabled = True
        tSNMKOL.Enabled = True
    End Sub

    Private Sub EnabledLoad()
        txtKDKOL.Enabled = True
        txtNMKOL.Enabled = True
        txtALAMAT.Enabled = True
        txtKOTA.Enabled = True
        txtKTP.Enabled = True
        txtNPWP.Enabled = True
        txtGAMBAR.Enabled = True
        btnUPLOAD.Enabled = True
        tSKDKOL.Enabled = False
        tSNMKOL.Enabled = False
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
            Dim kodesls As String = txtKDKOL.Text.Trim()

            ' cek apakah kosong
            If kodesls Is Nothing OrElse String.IsNullOrWhiteSpace(txtKDKOL.Text) Then
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
                Dim kodekol As String = txtKDKOL.Text.Trim()

                If String.IsNullOrEmpty(kodekol) Then
                    MessageBox.Show("Tidak ada data yang bisa dihapus.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If

                If MessageBox.Show("Yakin hapus " & kodekol & " ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then
                    Exit Sub
                End If

                HapusKolektor(txtKDKOL.Text)
                KosongkanInput()
                SetButtonState(Me, True)
                statusMode = ""
                DisabledLoad()

                MessageBox.Show(kodekol & " berhasil dihapus.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information)

            Catch ex As Exception
                MessageBox.Show(ex.Message, "Error Hapus", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    Public Sub HapusKolektor(ByVal kdKol As String)

        Try
            BukaKoneksi()
            Using Trans = Conn.BeginTransaction()
                Using cmd As New OdbcCommand("DELETE FROM zkolektor WHERE kodekol = ?", Conn, Trans)
                    cmd.Parameters.AddWithValue("@kodekol", kdKol)
                    cmd.ExecuteNonQuery()
                End Using

                Trans.Commit()
            End Using

        Catch ex As Exception
            MsgBox("Gagal menghapus data kolektor: " & ex.Message, vbCritical)
        End Try
    End Sub

    Private Sub btnSIMPAN_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSIMPAN.Click
        Try
            ' === 1. Validasi wajib diisi ===
            If txtKDKOL.Text.Trim() = "" Or txtNMKOL.Text.Trim() = "" Or txtALAMAT.Text.Trim() = "" Or txtKOTA.Text.Trim() = "" Or txtKTP.Text.Trim() = "" Or txtNPWP.Text.Trim() = "" Then
                MessageBox.Show("Semua field wajib diisi!", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning)

                ' === 3. Cek apakah KDUSER sudah terdaftar di zusers ===
            Else
                BukaKoneksi()
                Dim CekKol As String = "SELECT COUNT(*) FROM zkolektor WHERE kodekol = ?"
                Dim kolExist As Boolean = False
                Using cmdCekKol As New OdbcCommand(CekKol, Conn)
                    cmdCekKol.Parameters.AddWithValue("@kodekol", txtKDKOL.Text.Trim())
                    kolExist = Convert.ToInt32(cmdCekKol.ExecuteScalar())
                End Using

                If statusMode.ToLower() = "tambah" Then
                    If kolExist = False Then
                        ' === 5. Semua validasi lolos → simpan data ===
                        SimpanKolektor(txtKDKOL.Text, txtNMKOL.Text, txtALAMAT.Text, txtKOTA.Text, txtKTP.Text, txtNPWP.Text, statusMode)
                        MessageBox.Show("Data Kolektor berhasil disimpan", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information)

                        statusMode = ""
                        SetButtonState(Me, True)
                        DisabledLoad()

                    ElseIf kolExist = True Then
                        MessageBox.Show("Kode Kolektor sudah terdaftar!" & vbCrLf &
                                        "Tidak dapat menyimpan data baru.", "Validasi",
                                        MessageBoxButtons.OK, MessageBoxIcon.Warning)

                    End If
                ElseIf statusMode.ToLower() = "ubah" Then
                    ' === 5. Semua validasi lolos → simpan data ===
                    SimpanKolektor(txtKDKOL.Text, txtNMKOL.Text, txtALAMAT.Text, txtKOTA.Text, txtKTP.Text, txtNPWP.Text, statusMode)
                    MessageBox.Show("Data Kolektor berhasil disimpan", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information)

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

    Public Sub SimpanKolektor(ByVal kdKol As String,
                          ByVal nmKol As String,
                          ByVal alamat As String,
                          ByVal kota As String,
                          ByVal ktp As String,
                          ByVal npwp As String,
                          ByVal status As String)

        Dim Trans As OdbcTransaction = Nothing

        Try
            If kdKol = "" Then Throw New Exception("Kode Kolektor tidak boleh kosong!")

            BukaKoneksi()
            Trans = Conn.BeginTransaction()

            ' Jika mode UBAH, hapus dulu data lama
            If status.ToLower() = "ubah" Then
                KodeLama = kdKol
                Using cmdDel As New OdbcCommand("DELETE FROM zkolektor WHERE kodekol = ?", Conn, Trans)
                    cmdDel.Parameters.AddWithValue("@kodekol", kdKol)
                    cmdDel.ExecuteNonQuery()
                End Using

                Using cmd As New OdbcCommand("INSERT INTO zkolektor (kodekol, namakol, alamat, kota, ktp, npwp) VALUES (?, ?, ?, ?, ?, ?)", Conn, Trans)
                    cmd.Parameters.AddWithValue("@kodekol", kdKol)
                    cmd.Parameters.AddWithValue("@namakol", nmKol)
                    cmd.Parameters.AddWithValue("@alamat", alamat)
                    cmd.Parameters.AddWithValue("@kota", kota)
                    cmd.Parameters.AddWithValue("@ktp", ktp)
                    cmd.Parameters.AddWithValue("@npwp", npwp)
                    cmd.ExecuteNonQuery()
                End Using

            ElseIf status.ToLower() = "tambah" Then
                Using cmd As New OdbcCommand("INSERT INTO zkolektor (kodekol, namakol, alamat, kota, ktp, npwp) VALUES (?, ?, ?, ?, ?, ?)", Conn, Trans)
                    cmd.Parameters.AddWithValue("@kodekol", kdKol)
                    cmd.Parameters.AddWithValue("@namakol", nmKol)
                    cmd.Parameters.AddWithValue("@alamat", alamat)
                    cmd.Parameters.AddWithValue("@kota", kota)
                    cmd.Parameters.AddWithValue("@ktp", ktp)
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

            KodeLama = kdKol
            MsgBox("Gagal menyimpan data kolektor: " & ex.Message, vbCritical)
        End Try
    End Sub

    Private Sub btnBATAL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBATAL.Click
        SetButtonState(Me, True)
        statusMode = ""
        DisabledLoad()
    End Sub

    Private Sub tUser_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles tSKDKOL.TextChanged, tSNMKOL.TextChanged

        Dim txt As TextBox = CType(sender, TextBox)
        If txt Is tSKDKOL Then
            tSNMKOL.Enabled = (tSKDKOL.Text.Trim() = "")
        ElseIf txt Is tSNMKOL Then
            tSKDKOL.Enabled = (tSNMKOL.Text.Trim() = "")
        End If
    End Sub

    Private Sub btnCARI_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCARI.Click
        Dim f As New ItDtKolektor()

        Dim kodekol As String = tSKDKOL.Text.Trim()
        Dim namakol As String = tSNMKOL.Text.Trim()

        ' Load data sesuai filter
        f.Owner = Me
        f.Show()
        f.LoadDataKolektor(kodekol, namakol)

        ' === Clear filter setelah pencarian ===
        tSKDKOL.Clear()
        tSNMKOL.Clear()
    End Sub
End Class