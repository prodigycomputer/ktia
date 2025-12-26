Imports System.Data.Odbc

Public Class FSales
    Private statusMode As String = ""   ' status: "TAMBAH" / "UBAH"
    Public KodeLama As String = ""

    Private isUserTypingSearch As Boolean = False

    Private Sub FStok_Shown(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Shown
        Me.ActiveControl = Nothing
    End Sub

    Private Sub FSales_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetButtonState(Me, True)
        BukaKoneksi()
        DisabledLoad()

        ModPlaceholder.SetPlaceholder(tSKDSLS, "KODE SALES")
        ModPlaceholder.SetPlaceholder(tSNMSLS, "NAMA SALES")

        isUserTypingSearch = False

        ' pastikan dua-duanya aktif di awal
        tSKDSLS.Enabled = True
        tSNMSLS.Enabled = True
    End Sub

    Private Sub Search_KeyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs) _
    Handles tSKDSLS.KeyPress, tSNMSLS.KeyPress

        If Char.IsLetterOrDigit(e.KeyChar) Then
            isUserTypingSearch = True
        End If
    End Sub


    Private Sub TextBox_Enter(ByVal sender As Object, ByVal e As EventArgs) _
    Handles tSKDSLS.Enter, tSNMSLS.Enter

        ModPlaceholder.RemovePlaceholder(CType(sender, TextBox))
    End Sub

    Private Sub TextBox_Leave(ByVal sender As Object, ByVal e As EventArgs) _
        Handles tSKDSLS.Leave, tSNMSLS.Leave

        Dim txt = CType(sender, TextBox)
        If txt.Text.Trim() = "" Then
            ModPlaceholder.SetPlaceholder(txt, txt.Tag.ToString())
        End If
    End Sub

    Public Sub LoadData(ByVal kodesls As String)
        Try
            BukaKoneksi()

            Dim sql As String = "SELECT kodesls, namasls, alamat, kota, ktp, npwp FROM zsales WHERE kodesls = ?"
            Cmd = New OdbcCommand(sql, Conn)
            Cmd.Parameters.AddWithValue("", kodesls)

            Rd = Cmd.ExecuteReader()

            If Rd.Read() Then
                ' === Isi field ke textbox di form ===
                txtKDSLS.Text = Rd("kodesls").ToString()
                txtNMSLS.Text = Rd("namasls").ToString()
                txtALAMAT.Text = Rd("alamat").ToString()
                txtKOTA.Text = Rd("kota").ToString()
                txtKTP.Text = Rd("ktp").ToString()
                txtNPWP.Text = Rd("npwp").ToString()
                ' Kalau ada field lain (misalnya level, aktif, dsb) bisa ditambah di sini
                ' txtLEVEL.Text = Rd("level").ToString()
            Else
                MessageBox.Show("Data Sales tidak ditemukan.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

        Catch ex As Exception
            MessageBox.Show("Gagal memuat data user: " & ex.Message)
        End Try
    End Sub

    Private Sub KosongkanInput()
        txtKDSLS.Text = ""
        txtNMSLS.Text = ""
        txtALAMAT.Text = ""
        txtKOTA.Text = ""
        txtKTP.Text = ""
        txtNPWP.Text = ""
        txtGAMBAR.Text = ""
        tSKDSLS.Text = ""
        tSNMSLS.Text = ""
    End Sub

    Private Sub DisabledLoad()
        txtKDSLS.Enabled = False
        txtNMSLS.Enabled = False
        txtALAMAT.Enabled = False
        txtKOTA.Enabled = False
        txtKTP.Enabled = False
        txtNPWP.Enabled = False
        txtGAMBAR.Enabled = False
        btnUPLOAD.Enabled = False
        tSKDSLS.Enabled = True
        tSNMSLS.Enabled = True
    End Sub

    Private Sub EnabledLoad()
        txtKDSLS.Enabled = True
        txtNMSLS.Enabled = True
        txtALAMAT.Enabled = True
        txtKOTA.Enabled = True
        txtKTP.Enabled = True
        txtNPWP.Enabled = True
        txtGAMBAR.Enabled = True
        btnUPLOAD.Enabled = True
        tSKDSLS.Enabled = False
        tSNMSLS.Enabled = False
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
            Dim kodesls As String = txtKDSLS.Text.Trim()

            ' cek apakah kosong
            If kodesls Is Nothing OrElse String.IsNullOrWhiteSpace(txtKDSLS.Text) Then
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
                Dim kodesls As String = txtKDSLS.Text.Trim()

                If String.IsNullOrEmpty(kodesls) Then
                    MessageBox.Show("Tidak ada data yang bisa dihapus.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If

                If MessageBox.Show("Yakin hapus " & kodesls & " ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then
                    Exit Sub
                End If

                HapusSales(txtKDSLS.Text)
                KosongkanInput()
                SetButtonState(Me, True)
                statusMode = ""
                DisabledLoad()

                MessageBox.Show(kodesls & " berhasil dihapus.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information)

            Catch ex As Exception
                MessageBox.Show(ex.Message, "Error Hapus", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    Public Sub HapusSales(ByVal kdSls As String)

        Try
            BukaKoneksi()
            Using Trans = Conn.BeginTransaction()
                Using cmd As New OdbcCommand("DELETE FROM zsales WHERE kodesls = ?", Conn, Trans)
                    cmd.Parameters.AddWithValue("@kodesls", kdSls)
                    cmd.ExecuteNonQuery()
                End Using

                Trans.Commit()
            End Using

        Catch ex As Exception
            MsgBox("Gagal menghapus data sales: " & ex.Message, vbCritical)
        End Try
    End Sub

    Private Sub btnSIMPAN_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSIMPAN.Click
        Try
            ' === 1. Validasi wajib diisi ===
            If txtKDSLS.Text.Trim() = "" Or txtNMSLS.Text.Trim() = "" Or txtALAMAT.Text.Trim() = "" Or txtKOTA.Text.Trim() = "" Or txtKTP.Text.Trim() = "" Or txtNPWP.Text.Trim() = "" Then
                MessageBox.Show("Semua field wajib diisi!", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning)

                ' === 3. Cek apakah KDUSER sudah terdaftar di zusers ===
            Else
                BukaKoneksi()
                Dim CekSls As String = "SELECT COUNT(*) FROM zsales WHERE kodesls = ?"
                Dim slsExist As Boolean = False
                Using cmdCekSls As New OdbcCommand(CekSls, Conn)
                    cmdCekSls.Parameters.AddWithValue("@kodesls", txtKDSLS.Text.Trim())
                    slsExist = Convert.ToInt32(cmdCekSls.ExecuteScalar())
                End Using

                If statusMode.ToLower() = "tambah" Then
                    If slsExist = False Then
                        ' === 5. Semua validasi lolos → simpan data ===
                        SimpanSales(txtKDSLS.Text, txtNMSLS.Text, txtALAMAT.Text, txtKOTA.Text, txtKTP.Text, txtNPWP.Text, statusMode)
                        MessageBox.Show("Data Sales berhasil disimpan", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information)

                        statusMode = ""
                        SetButtonState(Me, True)
                        DisabledLoad()

                    ElseIf slsExist = True Then
                        MessageBox.Show("Kode Sales sudah terdaftar!" & vbCrLf &
                                        "Tidak dapat menyimpan data baru.", "Validasi",
                                        MessageBoxButtons.OK, MessageBoxIcon.Warning)

                    End If
                ElseIf statusMode.ToLower() = "ubah" Then
                    ' === 5. Semua validasi lolos → simpan data ===
                    SimpanSales(txtKDSLS.Text, txtNMSLS.Text, txtALAMAT.Text, txtKOTA.Text, txtKTP.Text, txtNPWP.Text, statusMode)
                    MessageBox.Show("Data Sales berhasil disimpan", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information)

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

    Public Sub SimpanSales(ByVal kdSls As String,
                          ByVal nmSls As String,
                          ByVal alamat As String,
                          ByVal kota As String,
                          ByVal ktp As String,
                          ByVal npwp As String,
                          ByVal status As String)

        Dim Trans As OdbcTransaction = Nothing

        Try
            If kdSls = "" Then Throw New Exception("Kode Sales tidak boleh kosong!")

            BukaKoneksi()
            Trans = Conn.BeginTransaction()

            ' Jika mode UBAH, hapus dulu data lama
            If status.ToLower() = "ubah" Then
                KodeLama = kdSls
                Using cmdDel As New OdbcCommand("DELETE FROM zsales WHERE kodesls = ?", Conn, Trans)
                    cmdDel.Parameters.AddWithValue("@kodesls", kdSls)
                    cmdDel.ExecuteNonQuery()
                End Using

                Using cmd As New OdbcCommand("INSERT INTO zsales (kodesls, namasls, alamat, kota, ktp, npwp) VALUES (?, ?, ?, ?, ?, ?)", Conn, Trans)
                    cmd.Parameters.AddWithValue("@kodesls", kdSls)
                    cmd.Parameters.AddWithValue("@namasls", nmSls)
                    cmd.Parameters.AddWithValue("@alamat", alamat)
                    cmd.Parameters.AddWithValue("@kota", kota)
                    cmd.Parameters.AddWithValue("@ktp", ktp)
                    cmd.Parameters.AddWithValue("@npwp", npwp)
                    cmd.ExecuteNonQuery()
                End Using

            ElseIf status.ToLower() = "tambah" Then
                Using cmd As New OdbcCommand("INSERT INTO zsales (kodesls, namasls, alamat, kota, ktp, npwp) VALUES (?, ?, ?, ?, ?, ?)", Conn, Trans)
                    cmd.Parameters.AddWithValue("@kodesls", kdSls)
                    cmd.Parameters.AddWithValue("@namasls", nmSls)
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

            KodeLama = kdSls
            MsgBox("Gagal menyimpan data sales: " & ex.Message, vbCritical)
        End Try
    End Sub

    Private Sub btnBATAL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBATAL.Click
        SetButtonState(Me, True)
        statusMode = ""
        DisabledLoad()
    End Sub

    Private Sub tUser_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles tSKDSLS.TextChanged, tSNMSLS.TextChanged

        ' ⛔ Jangan jalankan logika kalau user belum mengetik
        If Not isUserTypingSearch Then Exit Sub

        Dim isiKODE As Boolean =
            ModPlaceholder.GetRealText(tSKDSLS) <> ""

        Dim isiNAMA As Boolean =
            ModPlaceholder.GetRealText(tSNMSLS) <> ""

        tSNMSLS.Enabled = Not isiKODE
        tSKDSLS.Enabled = Not isiNAMA

    End Sub


    Private Sub btnCARI_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCARI.Click
        Dim f As New ItDtSales()

        Dim kodesls As String = tSKDSLS.Text.Trim()
        Dim namasls As String = tSNMSLS.Text.Trim()

        ' Load data sesuai filter
        f.Owner = Me
        f.Show()
        f.LoadDataSales(kodesls, namasls)

        ' === Clear filter setelah pencarian ===
        tSKDSLS.Clear()
        tSNMSLS.Clear()

        ModPlaceholder.SetPlaceholder(tSKDSLS, "KODE SALES")
        ModPlaceholder.SetPlaceholder(tSNMSLS, "NAMA SALES")

        isUserTypingSearch = False
        tSKDSLS.Enabled = True
        tSNMSLS.Enabled = True
    End Sub
End Class