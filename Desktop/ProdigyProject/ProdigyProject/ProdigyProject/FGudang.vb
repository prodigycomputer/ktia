Imports System.Data.Odbc

Public Class FGudang
    Private statusMode As String = ""   ' status: "TAMBAH" / "UBAH"
    Public KodeLama As String = ""

    Private Sub FGudang_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetButtonState(Me, True)
        BukaKoneksi()
        DisabledLoad()
    End Sub

    Public Sub LoadData(ByVal kodegd As String)
        Try
            BukaKoneksi()

            Dim sql As String = "SELECT kodegd, namagd FROM zgudang WHERE kodegd = ?"
            Cmd = New OdbcCommand(sql, Conn)
            Cmd.Parameters.AddWithValue("", kodegd)

            Rd = Cmd.ExecuteReader()

            If Rd.Read() Then
                ' === Isi field ke textbox di form ===
                txtKDGUDANG.Text = Rd("kodegd").ToString()
                txtNMGUDANG.Text = Rd("namagd").ToString()

                ' Kalau ada field lain (misalnya level, aktif, dsb) bisa ditambah di sini
                ' txtLEVEL.Text = Rd("level").ToString()
            Else
                MessageBox.Show("Data Gudang tidak ditemukan.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

        Catch ex As Exception
            MessageBox.Show("Gagal memuat data user: " & ex.Message)
        End Try
    End Sub

    Private Sub KosongkanInput()
        txtKDGUDANG.Text = ""
        txtNMGUDANG.Text = ""
        tSKDGUDANG.Text = ""
        tSNMGUDANG.Text = ""
    End Sub

    Private Sub DisabledLoad()
        txtKDGUDANG.Enabled = False
        txtNMGUDANG.Enabled = False
        tSKDGUDANG.Enabled = True
        tSNMGUDANG.Enabled = True
    End Sub

    Private Sub EnabledLoad()
        txtKDGUDANG.Enabled = True
        txtNMGUDANG.Enabled = True
        tSKDGUDANG.Enabled = False
        tSNMGUDANG.Enabled = False
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
            Dim kodeGD As String = txtKDGUDANG.Text.Trim()

            ' cek apakah kosong
            If kodeGD Is Nothing OrElse String.IsNullOrWhiteSpace(txtKDGUDANG.Text) Then
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
                Dim kodeGD As String = txtKDGUDANG.Text.Trim()

                If String.IsNullOrEmpty(kodeGD) Then
                    MessageBox.Show("Tidak ada data yang bisa dihapus.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If

                If MessageBox.Show("Yakin hapus " & kodeGD & " ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then
                    Exit Sub
                End If

                HapusGudang(txtKDGUDANG.Text)
                KosongkanInput()
                SetButtonState(Me, True)
                statusMode = ""
                DisabledLoad()

                MessageBox.Show(kodeGD & " berhasil dihapus.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information)

            Catch ex As Exception
                MessageBox.Show(ex.Message, "Error Hapus", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    Public Sub HapusGudang(ByVal kdGd As String)

        Try
            BukaKoneksi()
            Using Trans = Conn.BeginTransaction()
                Using cmd As New OdbcCommand("DELETE FROM zgudang WHERE kodegd = ?", Conn, Trans)
                    cmd.Parameters.AddWithValue("@kodegd", kdGd)
                    cmd.ExecuteNonQuery()
                End Using

                Trans.Commit()
            End Using

        Catch ex As Exception
            MsgBox("Gagal menghapus data gudang: " & ex.Message, vbCritical)
        End Try
    End Sub

    Private Sub btnSIMPAN_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSIMPAN.Click
        Try
            ' === 1. Validasi wajib diisi ===
            If txtKDGUDANG.Text.Trim() = "" Or txtNMGUDANG.Text.Trim() = "" Then
                MessageBox.Show("Semua field wajib diisi!", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning)

                ' === 3. Cek apakah KDUSER sudah terdaftar di zusers ===
            Else
                BukaKoneksi()
                Dim CekGd As String = "SELECT COUNT(*) FROM zgudang WHERE kodegd = ?"
                Dim gdExist As Boolean = False
                Using cmdCekGd As New OdbcCommand(CekGd, Conn)
                    cmdCekGd.Parameters.AddWithValue("@kodegd", txtKDGUDANG.Text.Trim())
                    gdExist = Convert.ToInt32(cmdCekGd.ExecuteScalar())
                End Using

                If statusMode.ToLower() = "tambah" Then
                    If gdExist = False Then
                        ' === 5. Semua validasi lolos → simpan data ===
                        SimpanGudang(txtKDGUDANG.Text, txtNMGUDANG.Text, statusMode)
                        MessageBox.Show("Data Gudang berhasil disimpan", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information)

                        statusMode = ""
                        SetButtonState(Me, True)
                        DisabledLoad()

                    ElseIf gdExist = True Then
                        MessageBox.Show("Kode Gudang sudah terdaftar!" & vbCrLf &
                                        "Tidak dapat menyimpan data baru.", "Validasi",
                                        MessageBoxButtons.OK, MessageBoxIcon.Warning)

                    End If
                ElseIf statusMode.ToLower() = "ubah" Then
                    ' === 5. Semua validasi lolos → simpan data ===
                    SimpanGudang(txtKDGUDANG.Text, txtNMGUDANG.Text, statusMode)
                    MessageBox.Show("Data Gudang berhasil disimpan", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information)

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

    Public Sub SimpanGudang(ByVal kdGd As String,
                          ByVal nmGd As String,
                          ByVal status As String)

        Dim Trans As OdbcTransaction = Nothing

        Try
            If kdGd = "" Then Throw New Exception("Kode Gudang tidak boleh kosong!")

            BukaKoneksi()
            Trans = Conn.BeginTransaction()

            ' Jika mode UBAH, hapus dulu data lama
            If status.ToLower() = "ubah" Then
                KodeLama = kdGd
                Using cmdDel As New OdbcCommand("DELETE FROM zgudang WHERE kodegd = ?", Conn, Trans)
                    cmdDel.Parameters.AddWithValue("@kodegd", kdGd)
                    cmdDel.ExecuteNonQuery()
                End Using

                Using cmd As New OdbcCommand("INSERT INTO zgudang (kodegd, namagd) VALUES (?, ?)", Conn, Trans)
                    cmd.Parameters.AddWithValue("@kodegd", kdGd)
                    cmd.Parameters.AddWithValue("@namagd", nmGd)
                    cmd.ExecuteNonQuery()
                End Using

            ElseIf status.ToLower() = "tambah" Then
                Using cmd As New OdbcCommand("INSERT INTO zgudang (kodegd, namagd) VALUES (?, ?)", Conn, Trans)
                    cmd.Parameters.AddWithValue("@kodegd", kdGd)
                    cmd.Parameters.AddWithValue("@namagd", nmGd)
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

            KodeLama = kdGd
            MsgBox("Gagal menyimpan data gudang: " & ex.Message, vbCritical)
        End Try
    End Sub

    Private Sub btnBATAL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBATAL.Click
        SetButtonState(Me, True)
        statusMode = ""
        DisabledLoad()
    End Sub

    Private Sub tUser_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles tSKDGUDANG.TextChanged, tSNMGUDANG.TextChanged

        Dim txt As TextBox = CType(sender, TextBox)
        If txt Is tSKDGUDANG Then
            tSNMGUDANG.Enabled = (tSKDGUDANG.Text.Trim() = "")
        ElseIf txt Is tSNMGUDANG Then
            tSKDGUDANG.Enabled = (tSNMGUDANG.Text.Trim() = "")
        End If
    End Sub

    Private Sub btnCARI_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCARI.Click
        Dim f As New ItDtGudang()

        Dim kodeGD As String = tSKDGUDANG.Text.Trim()
        Dim namaGD As String = tSNMGUDANG.Text.Trim()

        ' Load data sesuai filter
        f.Owner = Me
        f.Show()
        f.LoadDataGudang(kodeGD, namaGD)

        ' === Clear filter setelah pencarian ===
        tSKDGUDANG.Clear()
        tSNMGUDANG.Clear()
    End Sub
End Class