Imports System.Data.Odbc

Public Class FGolongan
    Private statusMode As String = ""   ' status: "TAMBAH" / "UBAH"
    Public KodeLama As String = ""

    Private Sub FGolongan_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetButtonState(Me, True)
        BukaKoneksi()
        DisabledLoad()
    End Sub

    Public Sub LoadData(ByVal kodegol As String)
        Try
            BukaKoneksi()

            Dim sql As String = "SELECT kodegol, namagol FROM zgolongan WHERE kodegol = ?"
            Cmd = New OdbcCommand(sql, Conn)
            Cmd.Parameters.AddWithValue("", kodegol)

            Rd = Cmd.ExecuteReader()

            If Rd.Read() Then
                ' === Isi field ke textbox di form ===
                txtKDGOL.Text = Rd("kodegol").ToString()
                txtNMGOL.Text = Rd("namagol").ToString()

                ' Kalau ada field lain (misalnya level, aktif, dsb) bisa ditambah di sini
                ' txtLEVEL.Text = Rd("level").ToString()
            Else
                MessageBox.Show("Data Golongan tidak ditemukan.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

        Catch ex As Exception
            MessageBox.Show("Gagal memuat data user: " & ex.Message)
        End Try
    End Sub

    Private Sub KosongkanInput()
        txtKDGOL.Text = ""
        txtNMGOL.Text = ""
        tSKDGOL.Text = ""
        tSNMGOL.Text = ""
    End Sub

    Private Sub DisabledLoad()
        txtKDGOL.Enabled = False
        txtNMGOL.Enabled = False
        tSKDGOL.Enabled = True
        tSNMGOL.Enabled = True
    End Sub

    Private Sub EnabledLoad()
        txtKDGOL.Enabled = True
        txtNMGOL.Enabled = True
        tSKDGOL.Enabled = False
        tSNMGOL.Enabled = False
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
            Dim kodeGO As String = txtKDGOL.Text.Trim()

            ' cek apakah kosong
            If kodeGO Is Nothing OrElse String.IsNullOrWhiteSpace(txtKDGOL.Text) Then
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
                Dim kodeGO As String = txtKDGOL.Text.Trim()

                If String.IsNullOrEmpty(kodeGO) Then
                    MessageBox.Show("Tidak ada data yang bisa dihapus.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If

                If MessageBox.Show("Yakin hapus " & kodeGO & " ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then
                    Exit Sub
                End If

                HapusGolongan(txtKDGOL.Text)
                KosongkanInput()
                SetButtonState(Me, True)
                statusMode = ""
                DisabledLoad()

                MessageBox.Show(kodeGO & " berhasil dihapus.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information)

            Catch ex As Exception
                MessageBox.Show(ex.Message, "Error Hapus", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    Public Sub HapusGolongan(ByVal kdGo As String)

        Try
            BukaKoneksi()
            Using Trans = Conn.BeginTransaction()
                Using cmd As New OdbcCommand("DELETE FROM zgolongan WHERE kodegol = ?", Conn, Trans)
                    cmd.Parameters.AddWithValue("@kodegol", kdGo)
                    cmd.ExecuteNonQuery()
                End Using

                Trans.Commit()
            End Using

        Catch ex As Exception
            MsgBox("Gagal menghapus data golongan: " & ex.Message, vbCritical)
        End Try
    End Sub

    Private Sub btnSIMPAN_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSIMPAN.Click
        Try
            ' === 1. Validasi wajib diisi ===
            If txtKDGOL.Text.Trim() = "" Or txtNMGOL.Text.Trim() = "" Then
                MessageBox.Show("Semua field wajib diisi!", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning)

                ' === 3. Cek apakah KDUSER sudah terdaftar di zusers ===
            Else
                BukaKoneksi()
                Dim CekGo As String = "SELECT COUNT(*) FROM zgolongan WHERE kodegol = ?"
                Dim goExist As Boolean = False
                Using cmdCekGo As New OdbcCommand(CekGo, Conn)
                    cmdCekGo.Parameters.AddWithValue("@kodegol", txtKDGOL.Text.Trim())
                    goExist = Convert.ToInt32(cmdCekGo.ExecuteScalar())
                End Using

                If statusMode.ToLower() = "tambah" Then
                    If goExist = False Then
                        ' === 5. Semua validasi lolos → simpan data ===
                        SimpanGolongan(txtKDGOL.Text, txtNMGOL.Text, statusMode)
                        MessageBox.Show("Data Golongan berhasil disimpan", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information)

                        statusMode = ""
                        SetButtonState(Me, True)
                        DisabledLoad()

                    ElseIf goExist = True Then
                        MessageBox.Show("Kode Golongan sudah terdaftar!" & vbCrLf &
                                        "Tidak dapat menyimpan data baru.", "Validasi",
                                        MessageBoxButtons.OK, MessageBoxIcon.Warning)

                    End If
                ElseIf statusMode.ToLower() = "ubah" Then
                    ' === 5. Semua validasi lolos → simpan data ===
                    SimpanGolongan(txtKDGOL.Text, txtNMGOL.Text, statusMode)
                    MessageBox.Show("Data Golongan berhasil disimpan", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information)

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

    Public Sub SimpanGolongan(ByVal kdGo As String,
                          ByVal nmGo As String,
                          ByVal status As String)

        Dim Trans As OdbcTransaction = Nothing

        Try
            If kdGo = "" Then Throw New Exception("Kode Golongan tidak boleh kosong!")

            BukaKoneksi()
            Trans = Conn.BeginTransaction()

            ' Jika mode UBAH, hapus dulu data lama
            If status.ToLower() = "ubah" Then
                KodeLama = kdGo
                Using cmdDel As New OdbcCommand("DELETE FROM zgolongan WHERE kodegol = ?", Conn, Trans)
                    cmdDel.Parameters.AddWithValue("@kodegol", kdGo)
                    cmdDel.ExecuteNonQuery()
                End Using

                Using cmd As New OdbcCommand("INSERT INTO zgolongan (kodegol, namagol) VALUES (?, ?)", Conn, Trans)
                    cmd.Parameters.AddWithValue("@kodegol", kdGo)
                    cmd.Parameters.AddWithValue("@namagol", nmGo)
                    cmd.ExecuteNonQuery()
                End Using

            ElseIf status.ToLower() = "tambah" Then
                Using cmd As New OdbcCommand("INSERT INTO zgolongan (kodegol, namagol) VALUES (?, ?)", Conn, Trans)
                    cmd.Parameters.AddWithValue("@kodegol", kdGo)
                    cmd.Parameters.AddWithValue("@namagol", nmGo)
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

            KodeLama = kdGo
            MsgBox("Gagal menyimpan data golongan: " & ex.Message, vbCritical)
        End Try
    End Sub

    Private Sub btnBATAL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBATAL.Click
        SetButtonState(Me, True)
        statusMode = ""
        DisabledLoad()
    End Sub

    Private Sub tUser_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles tSKDGOL.TextChanged, tSNMGOL.TextChanged

        Dim txt As TextBox = CType(sender, TextBox)
        If txt Is tSKDGOL Then
            tSNMGOL.Enabled = (tSKDGOL.Text.Trim() = "")
        ElseIf txt Is tSNMGOL Then
            tSKDGOL.Enabled = (tSNMGOL.Text.Trim() = "")
        End If
    End Sub

    Private Sub btnCARI_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCARI.Click
        Dim f As New ItDtGolongan()

        Dim kodeGO As String = tSKDGOL.Text.Trim()
        Dim namaGO As String = tSNMGOL.Text.Trim()

        ' Load data sesuai filter
        f.Owner = Me
        f.Show()
        f.LoadDataGolongan(kodeGO, namaGO)

        ' === Clear filter setelah pencarian ===
        tSKDGOL.Clear()
        tSNMGOL.Clear()
    End Sub
End Class