Imports System.Data.Odbc

Public Class FTipe
    Private statusMode As String = ""   ' status: "TAMBAH" / "UBAH"
    Public KodeLama As String = ""

    Private Sub FTipe_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetButtonState(Me, True)
        BukaKoneksi()
        DisabledLoad()
    End Sub

    Public Sub LoadData(ByVal kodetipe As String)
        Try
            BukaKoneksi()

            Dim sql As String = "SELECT kodetipe, namatipe FROM ztipe WHERE kodetipe = ?"
            Cmd = New OdbcCommand(sql, Conn)
            Cmd.Parameters.AddWithValue("", kodetipe)

            Rd = Cmd.ExecuteReader()

            If Rd.Read() Then
                ' === Isi field ke textbox di form ===
                txtKDTIPE.Text = Rd("kodetipe").ToString()
                txtNMTIPE.Text = Rd("namatipe").ToString()

                ' Kalau ada field lain (misalnya level, aktif, dsb) bisa ditambah di sini
                ' txtLEVEL.Text = Rd("level").ToString()
            Else
                MessageBox.Show("Data Tipe tidak ditemukan.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

        Catch ex As Exception
            MessageBox.Show("Gagal memuat data user: " & ex.Message)
        End Try
    End Sub

    Private Sub KosongkanInput()
        txtKDTIPE.Text = ""
        txtNMTIPE.Text = ""
        tSKDTIPE.Text = ""
        tSNMTIPE.Text = ""
    End Sub

    Private Sub DisabledLoad()
        txtKDTIPE.Enabled = False
        txtNMTIPE.Enabled = False
        tSKDTIPE.Enabled = True
        tSNMTIPE.Enabled = True
    End Sub

    Private Sub EnabledLoad()
        txtKDTIPE.Enabled = True
        txtNMTIPE.Enabled = True
        tSKDTIPE.Enabled = False
        tSNMTIPE.Enabled = False
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
            Dim kodetipe As String = txtKDTIPE.Text.Trim()

            ' cek apakah kosong
            If kodetipe Is Nothing OrElse String.IsNullOrWhiteSpace(txtKDTIPE.Text) Then
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
                Dim kodetipe As String = txtKDTIPE.Text.Trim()

                If String.IsNullOrEmpty(kodetipe) Then
                    MessageBox.Show("Tidak ada data yang bisa dihapus.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If

                If MessageBox.Show("Yakin hapus " & kodetipe & " ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then
                    Exit Sub
                End If

                HapusTipe(txtKDTIPE.Text)
                KosongkanInput()
                SetButtonState(Me, True)
                statusMode = ""
                DisabledLoad()

                MessageBox.Show(kodetipe & " berhasil dihapus.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information)

            Catch ex As Exception
                MessageBox.Show(ex.Message, "Error Hapus", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    Public Sub HapusTipe(ByVal kdTipe As String)

        Try
            BukaKoneksi()
            Using Trans = Conn.BeginTransaction()
                Using cmd As New OdbcCommand("DELETE FROM ztipe WHERE kodetipe = ?", Conn, Trans)
                    cmd.Parameters.AddWithValue("@kodetipe", kdTipe)
                    cmd.ExecuteNonQuery()
                End Using

                Trans.Commit()
            End Using
            
        Catch ex As Exception
            MsgBox("Gagal menghapus data tipe: " & ex.Message, vbCritical)
        End Try
    End Sub

    Private Sub btnSIMPAN_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSIMPAN.Click
        Try
            ' === 1. Validasi wajib diisi ===
            If txtKDTIPE.Text.Trim() = "" Or txtNMTIPE.Text.Trim() = "" Then
                MessageBox.Show("Semua field wajib diisi!", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning)

                ' === 3. Cek apakah KDUSER sudah terdaftar di zusers ===
            Else
                BukaKoneksi()
                Dim CekTipe As String = "SELECT COUNT(*) FROM ztipe WHERE kodetipe = ?"
                Dim tipeExist As Boolean = False
                Using cmdCekTipe As New OdbcCommand(CekTipe, Conn)
                    cmdCekTipe.Parameters.AddWithValue("@kodetipe", txtKDTIPE.Text.Trim())
                    tipeExist = Convert.ToInt32(cmdCekTipe.ExecuteScalar())
                End Using

                If statusMode.ToLower() = "tambah" Then
                    If TIPEExist = False Then
                        ' === 5. Semua validasi lolos → simpan data ===
                        SimpanTipe(txtKDTIPE.Text, txtNMTIPE.Text, statusMode)
                        MessageBox.Show("Data Tipe berhasil disimpan", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information)

                        statusMode = ""
                        SetButtonState(Me, True)
                        DisabledLoad()

                    ElseIf TIPEExist = True Then
                        MessageBox.Show("Kode Tipe sudah terdaftar!" & vbCrLf &
                                        "Tidak dapat menyimpan data baru.", "Validasi",
                                        MessageBoxButtons.OK, MessageBoxIcon.Warning)

                    End If
                ElseIf statusMode.ToLower() = "ubah" Then
                    ' === 5. Semua validasi lolos → simpan data ===
                    SimpanTipe(txtKDTIPE.Text, txtNMTIPE.Text, statusMode)
                    MessageBox.Show("Data Tipe berhasil disimpan", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information)

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

    Public Sub SimpanTipe(ByVal kdTipe As String,
                          ByVal nmTipe As String,
                          ByVal status As String)

        Dim Trans As OdbcTransaction = Nothing

        Try
            If kdTipe = "" Then Throw New Exception("Kode Tipe tidak boleh kosong!")

            BukaKoneksi()
            Trans = Conn.BeginTransaction()

            ' Jika mode UBAH, hapus dulu data lama
            If status.ToLower() = "ubah" Then
                KodeLama = kdTipe
                Using cmdDel As New OdbcCommand("DELETE FROM ztipe WHERE kodetipe = ?", Conn, Trans)
                    cmdDel.Parameters.AddWithValue("@kodetipe", kdTipe)
                    cmdDel.ExecuteNonQuery()
                End Using

                Using cmd As New OdbcCommand("INSERT INTO ztipe (kodetipe, namatipe) VALUES (?, ?)", Conn, Trans)
                    cmd.Parameters.AddWithValue("@kodetipe", kdTipe)
                    cmd.Parameters.AddWithValue("@namatipe", nmTipe)
                    cmd.ExecuteNonQuery()
                End Using

            ElseIf status.ToLower() = "tambah" Then
                Using cmd As New OdbcCommand("INSERT INTO ztipe (kodetipe, namatipe) VALUES (?, ?)", Conn, Trans)
                    cmd.Parameters.AddWithValue("@kodetipe", kdTipe)
                    cmd.Parameters.AddWithValue("@namatipe", nmTipe)
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

            KodeLama = kdTipe
            MsgBox("Gagal menyimpan data tipe: " & ex.Message, vbCritical)
        End Try
    End Sub

    Private Sub btnBATAL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBATAL.Click
        SetButtonState(Me, True)
        statusMode = ""
        DisabledLoad()
    End Sub

    Private Sub tUser_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles tSKDTIPE.TextChanged, tSNMTIPE.TextChanged

        Dim txt As TextBox = CType(sender, TextBox)
        If txt Is tSKDTIPE Then
            tSNMTIPE.Enabled = (tSKDTIPE.Text.Trim() = "")
        ElseIf txt Is tSNMTIPE Then
            tSKDTIPE.Enabled = (tSNMTIPE.Text.Trim() = "")
        End If
    End Sub

    Private Sub btnCARI_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCARI.Click
        Dim f As New ItDtTipe()

        Dim kodetipe As String = tSKDTIPE.Text.Trim()
        Dim namatipe As String = tSNMTIPE.Text.Trim()

        ' Load data sesuai filter
        f.Owner = Me
        f.Show()
        f.LoadDataTipe(kodetipe, namatipe)

        ' === Clear filter setelah pencarian ===
        tSKDTIPE.Clear()
        tSNMTIPE.Clear()
    End Sub
End Class