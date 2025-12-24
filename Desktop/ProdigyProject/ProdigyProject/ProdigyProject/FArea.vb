Imports System.Data.Odbc

Public Class FArea
    Private statusMode As String = ""   ' status: "TAMBAH" / "UBAH"
    Public KodeLama As String = ""

    Private isUserTypingSearch As Boolean = False

    Private Sub FStok_Shown(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Shown
        Me.ActiveControl = Nothing
    End Sub

    Private Sub FArea_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetButtonState(Me, True)
        BukaKoneksi()
        DisabledLoad()

        ModPlaceholder.SetPlaceholder(tSKDAREA, "KODE AREA")
        ModPlaceholder.SetPlaceholder(tSNMAREA, "NAMA AREA")

        isUserTypingSearch = False

        ' pastikan dua-duanya aktif di awal
        tSKDAREA.Enabled = True
        tSNMAREA.Enabled = True
    End Sub

    Private Sub Search_KeyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs) _
    Handles tSKDAREA.KeyPress, tSNMAREA.KeyPress

        If Char.IsLetterOrDigit(e.KeyChar) Then
            isUserTypingSearch = True
        End If
    End Sub


    Private Sub TextBox_Enter(ByVal sender As Object, ByVal e As EventArgs) _
    Handles tSKDAREA.Enter, tSNMAREA.Enter

        ModPlaceholder.RemovePlaceholder(CType(sender, TextBox))
    End Sub

    Private Sub TextBox_Leave(ByVal sender As Object, ByVal e As EventArgs) _
        Handles tSKDAREA.Leave, tSNMAREA.Leave

        Dim txt = CType(sender, TextBox)
        If txt.Text.Trim() = "" Then
            ModPlaceholder.SetPlaceholder(txt, txt.Tag.ToString())
        End If
    End Sub

    Public Sub LoadData(ByVal kodearea As String)
        Try
            BukaKoneksi()

            Dim sql As String = "SELECT kodear, namaar FROM zarea WHERE kodear = ?"
            Cmd = New OdbcCommand(sql, Conn)
            Cmd.Parameters.AddWithValue("", kodearea)

            Rd = Cmd.ExecuteReader()

            If Rd.Read() Then
                ' === Isi field ke textbox di form ===
                txtKDAREA.Text = Rd("kodear").ToString()
                txtNMAREA.Text = Rd("namaar").ToString()

                ' Kalau ada field lain (misalnya level, aktif, dsb) bisa ditambah di sini
                ' txtLEVEL.Text = Rd("level").ToString()
            Else
                MessageBox.Show("Data Area tidak ditemukan.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

        Catch ex As Exception
            MessageBox.Show("Gagal memuat data user: " & ex.Message)
        End Try
    End Sub

    Private Sub KosongkanInput()
        txtKDAREA.Text = ""
        txtNMAREA.Text = ""
        tSKDAREA.Text = ""
        tSNMAREA.Text = ""
    End Sub

    Private Sub DisabledLoad()
        txtKDAREA.Enabled = False
        txtNMAREA.Enabled = False
        tSKDAREA.Enabled = True
        tSNMAREA.Enabled = True
    End Sub

    Private Sub EnabledLoad()
        txtKDAREA.Enabled = True
        txtNMAREA.Enabled = True
        tSKDAREA.Enabled = False
        tSNMAREA.Enabled = False
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
            Dim kodearea As String = txtKDAREA.Text.Trim()

            ' cek apakah kosong
            If kodearea Is Nothing OrElse String.IsNullOrWhiteSpace(txtKDAREA.Text) Then
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
                Dim kodearea As String = txtKDAREA.Text.Trim()

                If String.IsNullOrEmpty(kodearea) Then
                    MessageBox.Show("Tidak ada data yang bisa dihapus.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If

                If MessageBox.Show("Yakin hapus " & kodearea & " ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then
                    Exit Sub
                End If

                HapusArea(txtKDAREA.Text)
                KosongkanInput()
                SetButtonState(Me, True)
                statusMode = ""
                DisabledLoad()

                MessageBox.Show(kodearea & " berhasil dihapus.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information)

            Catch ex As Exception
                MessageBox.Show(ex.Message, "Error Hapus", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    Public Sub HapusArea(ByVal kdArea As String)

        Try
            BukaKoneksi()
            Using Trans = Conn.BeginTransaction()
                Using cmd As New OdbcCommand("DELETE FROM zarea WHERE kodear = ?", Conn, Trans)
                    cmd.Parameters.AddWithValue("@kodear", kdArea)
                    cmd.ExecuteNonQuery()
                End Using

                Trans.Commit()
            End Using
            
        Catch ex As Exception
            MsgBox("Gagal menghapus data area: " & ex.Message, vbCritical)
        End Try
    End Sub

    Private Sub btnSIMPAN_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSIMPAN.Click
        Try
            ' === 1. Validasi wajib diisi ===
            If txtKDAREA.Text.Trim() = "" Or txtNMAREA.Text.Trim() = "" Then
                MessageBox.Show("Semua field wajib diisi!", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning)

                ' === 3. Cek apakah KDUSER sudah terdaftar di zusers ===
            Else
                BukaKoneksi()
                Dim CekArea As String = "SELECT COUNT(*) FROM zarea WHERE kodear = ?"
                Dim areaExist As Boolean = False
                Using cmdCekArea As New OdbcCommand(CekArea, Conn)
                    cmdCekArea.Parameters.AddWithValue("@kodear", txtKDAREA.Text.Trim())
                    areaExist = Convert.ToInt32(cmdCekArea.ExecuteScalar())
                End Using

                If statusMode.ToLower() = "tambah" Then
                    If areaExist = False Then
                        ' === 5. Semua validasi lolos → simpan data ===
                        SimpanArea(txtKDAREA.Text, txtNMAREA.Text, statusMode)
                        MessageBox.Show("Data Area berhasil disimpan", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information)

                        statusMode = ""
                        SetButtonState(Me, True)
                        DisabledLoad()

                    ElseIf areaExist = True Then
                        MessageBox.Show("Kode Area sudah terdaftar!" & vbCrLf &
                                        "Tidak dapat menyimpan data baru.", "Validasi",
                                        MessageBoxButtons.OK, MessageBoxIcon.Warning)

                    End If
                ElseIf statusMode.ToLower() = "ubah" Then
                    ' === 5. Semua validasi lolos → simpan data ===
                    SimpanArea(txtKDAREA.Text, txtNMAREA.Text, statusMode)
                    MessageBox.Show("Data Area berhasil disimpan", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information)

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

    Public Sub SimpanArea(ByVal kdArea As String,
                          ByVal nmArea As String,
                          ByVal status As String)

        Dim Trans As OdbcTransaction = Nothing

        Try
            If kdArea = "" Then Throw New Exception("Kode Area tidak boleh kosong!")

            BukaKoneksi()
            Trans = Conn.BeginTransaction()

            ' Jika mode UBAH, hapus dulu data lama
            If status.ToLower() = "ubah" Then
                KodeLama = kdArea
                Using cmdDel As New OdbcCommand("DELETE FROM zarea WHERE kodear = ?", Conn, Trans)
                    cmdDel.Parameters.AddWithValue("@kodear", kdArea)
                    cmdDel.ExecuteNonQuery()
                End Using

                Using cmd As New OdbcCommand("INSERT INTO zarea (kodear, namaar) VALUES (?, ?)", Conn, Trans)
                    cmd.Parameters.AddWithValue("@kodear", kdArea)
                    cmd.Parameters.AddWithValue("@namaar", nmArea)
                    cmd.ExecuteNonQuery()
                End Using

            ElseIf status.ToLower() = "tambah" Then
                Using cmd As New OdbcCommand("INSERT INTO zarea (kodear, namaar) VALUES (?, ?)", Conn, Trans)
                    cmd.Parameters.AddWithValue("@kodear", kdArea)
                    cmd.Parameters.AddWithValue("@namaar", nmArea)
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

            KodeLama = kdArea
            MsgBox("Gagal menyimpan data area: " & ex.Message, vbCritical)
        End Try
    End Sub

    Private Sub btnBATAL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBATAL.Click
        SetButtonState(Me, True)
        statusMode = ""
        DisabledLoad()
    End Sub

    Private Sub tUser_TextChanged(ByVal sender As Object, ByVal e As EventArgs) _
    Handles tSKDAREA.TextChanged, tSNMAREA.TextChanged

        ' ⛔ Jangan jalankan logika kalau user belum mengetik
        If Not isUserTypingSearch Then Exit Sub

        Dim isiKODE As Boolean =
            ModPlaceholder.GetRealText(tSKDAREA) <> ""

        Dim isiNAMA As Boolean =
            ModPlaceholder.GetRealText(tSNMAREA) <> ""

        tSNMAREA.Enabled = Not isiKODE
        tSKDAREA.Enabled = Not isiNAMA
    End Sub

    Private Sub btnCARI_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCARI.Click
        Dim f As New ItDtArea()

        Dim kodearea As String = tSKDAREA.Text.Trim()
        Dim namaarea As String = tSNMAREA.Text.Trim()

        ' Load data sesuai filter
        f.Owner = Me
        f.Show()
        f.LoadDataArea(kodearea, namaarea)

        ' === Clear filter setelah pencarian ===
        tSKDAREA.Clear()
        tSNMAREA.Clear()

        ModPlaceholder.SetPlaceholder(tSKDAREA, "KODE AREA")
        ModPlaceholder.SetPlaceholder(tSNMAREA, "NAMA AREA")

        isUserTypingSearch = False
        tSKDAREA.Enabled = True
        tSNMAREA.Enabled = True
    End Sub
End Class