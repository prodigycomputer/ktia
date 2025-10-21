Imports System.Data.Odbc

Public Class FSetAkun
    Private statusMode As String = ""   ' status: "TAMBAH" / "UBAH"
    Private aksesUser As Dictionary(Of String, Boolean)
    Public KodeLama As String = ""

    Private Sub FSetAkun_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetButtonState(Me, True)
        BukaKoneksi()
        DisabledLoad()
    End Sub

    Public Sub LoadData(ByVal kodeuser As String)
        Try
            BukaKoneksi()

            Dim sql As String = "SELECT kodeuser, username, kunci FROM zusers WHERE kodeuser = ?"
            Cmd = New OdbcCommand(sql, Conn)
            Cmd.Parameters.AddWithValue("", kodeuser)

            Rd = Cmd.ExecuteReader()

            If Rd.Read() Then
                ' === Isi field ke textbox di form ===
                txtKDUSER.Text = Rd("kodeuser").ToString()
                txtNMUSER.Text = Rd("username").ToString()
                txtPASSUSER.Text = Rd("kunci").ToString()
                txtKPASSUSER.Text = Rd("kunci").ToString()

                ' Kalau ada field lain (misalnya level, aktif, dsb) bisa ditambah di sini
                ' txtLEVEL.Text = Rd("level").ToString()
            Else
                MessageBox.Show("Data user tidak ditemukan.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

        Catch ex As Exception
            MessageBox.Show("Gagal memuat data user: " & ex.Message)
        End Try
    End Sub

    Private Sub KosongkanInput()
        txtKDUSER.Text = ""
        txtNMUSER.Text = ""
        txtPASSUSER.Text = ""
        txtKPASSUSER.Text = ""
        tSKDUSER.Text = ""
        tSNMUSER.Text = ""
    End Sub

    Private Sub DisabledLoad()
        txtKDUSER.Enabled = False
        txtNMUSER.Enabled = False
        txtPASSUSER.Enabled = False
        txtKPASSUSER.Enabled = False
        tSKDUSER.Enabled = True
        tSNMUSER.Enabled = True
    End Sub

    Private Sub EnabledLoad()
        txtKDUSER.Enabled = True
        txtNMUSER.Enabled = True
        txtPASSUSER.Enabled = True
        txtKPASSUSER.Enabled = True
        tSKDUSER.Enabled = False
        tSNMUSER.Enabled = False
    End Sub

    Private Sub btnTAMBAH_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTAMBAH.Click
        SetButtonState(Me, False)
        statusMode = "tambah"
        KosongkanInput()
        EnabledLoad()
    End Sub

    Private Sub btnUBAH_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUBAH.Click

        Dim kodeuser As String = txtKDUSER.Text.Trim()

        ' cek apakah kosong
        If kodeuser Is Nothing OrElse String.IsNullOrWhiteSpace(txtKDUSER.Text) Then
            MessageBox.Show("Tidak ada data yang bisa di edit", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        EnabledLoad()
        SetButtonState(Me, False)
        statusMode = "ubah"
    End Sub

    Private Sub btnHAPUS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHAPUS.Click
        Try
            Dim kodeuser As String = txtKDUSER.Text.Trim()

            If String.IsNullOrEmpty(kodeuser) Then
                MessageBox.Show("Tidak ada User yang bisa dihapus.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            If MessageBox.Show("Yakin hapus " & kodeuser & " ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then
                Exit Sub
            End If

            HapusAkun(txtKDUSER.Text)
            KosongkanInput()
            SetButtonState(Me, True)
            statusMode = ""
            DisabledLoad()

            MessageBox.Show(kodeuser & " berhasil dihapus.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error Hapus", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub HapusAkun(ByVal kdUser As String)
        Try
            BukaKoneksi()

            Using Trans = Conn.BeginTransaction()
                ' Hapus dari zakses dulu (supaya tidak orphan)
                Using cmdAkses As New OdbcCommand("DELETE FROM zakses WHERE kodeuser = ?", Conn, Trans)
                    cmdAkses.Parameters.AddWithValue("@kodeuser", kdUser)
                    cmdAkses.ExecuteNonQuery()
                End Using

                ' Lalu hapus dari zusers
                Using cmdUser As New OdbcCommand("DELETE FROM zusers WHERE kodeuser = ?", Conn, Trans)
                    cmdUser.Parameters.AddWithValue("@kodeuser", kdUser)
                    cmdUser.ExecuteNonQuery()
                End Using

                Trans.Commit()
            End Using

        Catch ex As Exception
            MsgBox("Gagal menghapus data akun: " & ex.Message, vbCritical)
        End Try
    End Sub

    Private Sub btnSIMPAN_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSIMPAN.Click
        Try
            ' === 1. Validasi wajib diisi ===
            If txtKDUSER.Text.Trim() = "" Or txtNMUSER.Text.Trim() = "" Or txtPASSUSER.Text.Trim() = "" Then
                MessageBox.Show("Semua field wajib diisi!", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning)

                ' === 3. Cek apakah KDUSER sudah terdaftar di zusers ===
            Else
                BukaKoneksi()
                Dim CekUser As String = "SELECT COUNT(*) FROM zusers WHERE kodeuser = ?"
                Dim userExist As Boolean = False
                Using cmdCekUser As New OdbcCommand(CekUser, Conn)
                    cmdCekUser.Parameters.AddWithValue("@kodeuser", txtKDUSER.Text.Trim())
                    userExist = Convert.ToInt32(cmdCekUser.ExecuteScalar())
                End Using

                Dim CekAkses As String = "SELECT COUNT(*) FROM zakses WHERE kodeuser = ?"
                Dim aksesExist As Boolean = True
                Using cmdCekAkses As New OdbcCommand(CekAkses, Conn)
                    cmdCekAkses.Parameters.AddWithValue("@kodeuser", txtKDUSER.Text.Trim())
                    aksesExist = Convert.ToInt32(cmdCekAkses.ExecuteScalar())
                End Using

                If statusMode.ToLower() = "tambah" Then
                    If userExist = False AndAlso aksesExist = True Then
                        ' ✅ Kondisi valid untuk simpan
                        SimpanAkun(txtKDUSER.Text, txtNMUSER.Text, txtPASSUSER.Text, statusMode)
                        MessageBox.Show("Data user baru berhasil disimpan.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information)

                        statusMode = ""
                        SetButtonState(Me, True)
                        DisabledLoad()

                    ElseIf userExist = True Then
                        MessageBox.Show("Kode User sudah terdaftar!" & vbCrLf &
                                        "Tidak dapat menyimpan data baru.", "Validasi",
                                        MessageBoxButtons.OK, MessageBoxIcon.Warning)

                    ElseIf aksesExist = False Then
                        MessageBox.Show("User belum memiliki hak akses!" & vbCrLf &
                                        "Silakan atur hak akses terlebih dahulu sebelum menyimpan.", "Validasi",
                                        MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    End If

                ElseIf statusMode.ToLower() = "ubah" Then
                    SimpanAkun(txtKDUSER.Text, txtNMUSER.Text, txtPASSUSER.Text, statusMode)
                    MessageBox.Show("Perubahan data user berhasil disimpan.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information)

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

    Public Sub SimpanAkun(ByVal kdUser As String,
                          ByVal nmUser As String,
                          ByVal passUser As String,
                          ByVal status As String)

        Dim Trans As OdbcTransaction = Nothing

        Try
            If kdUser.Trim() = "" Then Throw New Exception("Kode User tidak boleh kosong!")

            BukaKoneksi()
            Trans = Conn.BeginTransaction()

            ' === Mode Ubah: hapus data lama dulu ===
            If status.ToLower() = "ubah" Then
                KodeLama = kdUser
                Using cmdDel As New OdbcCommand("DELETE FROM zusers WHERE kodeuser = ?", Conn, Trans)
                    cmdDel.Parameters.AddWithValue("@kodeuser", kdUser)
                    cmdDel.ExecuteNonQuery()
                End Using

                Using cmd As New OdbcCommand("INSERT INTO zusers (kodeuser, username, kunci) VALUES (?, ?, ?)", Conn, Trans)
                    cmd.Parameters.AddWithValue("@kodeuser", kdUser)
                    cmd.Parameters.AddWithValue("@username", nmUser)
                    cmd.Parameters.AddWithValue("@kunci", passUser)
                    cmd.ExecuteNonQuery()
                End Using
            ElseIf status.ToLower() = "tambah" Then
                ' === Insert data baru ===
                Using cmd As New OdbcCommand("INSERT INTO zusers (kodeuser, username, kunci) VALUES (?, ?, ?)", Conn, Trans)
                    cmd.Parameters.AddWithValue("@kodeuser", kdUser)
                    cmd.Parameters.AddWithValue("@username", nmUser)
                    cmd.Parameters.AddWithValue("@kunci", passUser)
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

            KodeLama = kdUser
            MsgBox("Gagal menyimpan data akun: " & ex.Message, vbCritical)
        End Try
    End Sub

    Private Sub btnBATAL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBATAL.Click
        SetButtonState(Me, True)
        statusMode = ""
        DisabledLoad()
    End Sub

    Private Sub tUser_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles tSKDUSER.TextChanged, tSNMUSER.TextChanged

        Dim txt As TextBox = CType(sender, TextBox)
        If txt Is tSKDUSER Then
            tSNMUSER.Enabled = (tSKDUSER.Text.Trim() = "")
        ElseIf txt Is tSNMUSER Then
            tSKDUSER.Enabled = (tSNMUSER.Text.Trim() = "")
        End If
    End Sub

    Private Sub btnAKSES_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAKSES.Click
        Try
            Dim kodeUser As String = txtKDUSER.Text.Trim()

            If String.IsNullOrEmpty(kodeUser) Then
                MessageBox.Show("Pilih atau buat user terlebih dahulu sebelum mengatur hak akses.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            ' Buka form FDtAkses dan kirim kode user
            Dim aksesForm As New FDtAkses()
            aksesForm.KodeUser = kodeUser
            aksesForm.ShowDialog(Me)

        Catch ex As Exception
            MessageBox.Show("Terjadi kesalahan: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnCARI_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCARI.Click
        Dim f As New ItDtUser()

        Dim kodeuser As String = tSKDUSER.Text.Trim()
        Dim username As String = tSNMUSER.Text.Trim()

        ' Load data sesuai filter
        f.Owner = Me
        f.Show()
        f.LoadDataUser(kodeuser, username)

        ' === Clear filter setelah pencarian ===
        tSKDUSER.Clear()
        tSNMUSER.Clear()
    End Sub
End Class