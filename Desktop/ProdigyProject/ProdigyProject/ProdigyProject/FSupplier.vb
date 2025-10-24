Imports System.Data.Odbc

Public Class FSupplier
    Private statusMode As String = ""   ' status: "TAMBAH" / "UBAH"
    Public KodeLama As String = ""

    Private Sub FSupplier_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetButtonState(Me, True)
        BukaKoneksi()
        DisabledLoad()
    End Sub

    Public Sub LoadData(ByVal kodesup As String)
        Try
            BukaKoneksi()

            Dim sql As String = "SELECT kodesup, namasup, alamat, kota, ktp, npwp FROM zsupplier WHERE kodesup = ?"
            Cmd = New OdbcCommand(sql, Conn)
            Cmd.Parameters.AddWithValue("", kodesup)

            Rd = Cmd.ExecuteReader()

            If Rd.Read() Then
                ' === Isi field ke textbox di form ===
                txtKDSUP.Text = Rd("kodesup").ToString()
                txtNMSUP.Text = Rd("namasup").ToString()
                txtALAMAT.Text = Rd("alamat").ToString()
                txtKOTA.Text = Rd("kota").ToString()
                txtKTP.Text = Rd("ktp").ToString()
                txtNPWP.Text = Rd("npwp").ToString()
                ' Kalau ada field lain (misalnya level, aktif, dsb) bisa ditambah di sini
                ' txtLEVEL.Text = Rd("level").ToString()
            Else
                MessageBox.Show("Data Supplier tidak ditemukan.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

        Catch ex As Exception
            MessageBox.Show("Gagal memuat data user: " & ex.Message)
        End Try
    End Sub

    Private Sub KosongkanInput()
        txtKDSUP.Text = ""
        txtNMSUP.Text = ""
        txtALAMAT.Text = ""
        txtKOTA.Text = ""
        txtKTP.Text = ""
        txtNPWP.Text = ""
        txtGAMBAR.Text = ""
        tSKDSUP.Text = ""
        tSNMSUP.Text = ""
    End Sub

    Private Sub DisabledLoad()
        txtKDSUP.Enabled = False
        txtNMSUP.Enabled = False
        txtALAMAT.Enabled = False
        txtKOTA.Enabled = False
        txtKTP.Enabled = False
        txtNPWP.Enabled = False
        txtGAMBAR.Enabled = False
        btnUPLOAD.Enabled = False
        tSKDSUP.Enabled = True
        tSNMSUP.Enabled = True
    End Sub

    Private Sub EnabledLoad()
        txtKDSUP.Enabled = True
        txtNMSUP.Enabled = True
        txtALAMAT.Enabled = True
        txtKOTA.Enabled = True
        txtKTP.Enabled = True
        txtNPWP.Enabled = True
        txtGAMBAR.Enabled = False
        btnUPLOAD.Enabled = False
        tSKDSUP.Enabled = False
        tSNMSUP.Enabled = False
    End Sub

    Private Sub btnTAMBAH_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTAMBAH.Click
        SetButtonState(Me, False)
        statusMode = "tambah"
        KosongkanInput()
        EnabledLoad()
    End Sub

    Private Sub btnUBAH_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUBAH.Click
        Dim kodesup As String = txtKDSUP.Text.Trim()

        ' cek apakah kosong
        If kodesup Is Nothing OrElse String.IsNullOrWhiteSpace(txtKDSUP.Text) Then
            MessageBox.Show("Tidak ada data yang bisa di edit", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        EnabledLoad()
        SetButtonState(Me, False)
        statusMode = "ubah"
    End Sub

    Private Sub btnHAPUS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHAPUS.Click
        Try
            Dim kodesup As String = txtKDSUP.Text.Trim()

            If String.IsNullOrEmpty(kodesup) Then
                MessageBox.Show("Tidak ada data yang bisa dihapus.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            If MessageBox.Show("Yakin hapus " & kodesup & " ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then
                Exit Sub
            End If

            HapusSupplier(txtKDSUP.Text)
            KosongkanInput()
            SetButtonState(Me, True)
            statusMode = ""
            DisabledLoad()

            MessageBox.Show(kodesup & " berhasil dihapus.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error Hapus", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub HapusSupplier(ByVal kdSup As String)

        Try
            BukaKoneksi()
            Using Trans = Conn.BeginTransaction()
                Using cmd As New OdbcCommand("DELETE FROM zsupplier WHERE kodesup = ?", Conn, Trans)
                    cmd.Parameters.AddWithValue("@kodesup", kdSup)
                    cmd.ExecuteNonQuery()
                End Using

                Trans.Commit()
            End Using

        Catch ex As Exception
            MsgBox("Gagal menghapus data supplier: " & ex.Message, vbCritical)
        End Try
    End Sub

    Private Sub btnSIMPAN_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSIMPAN.Click
        Try
            ' === 1. Validasi wajib diisi ===
            If txtKDSUP.Text.Trim() = "" Or txtNMSUP.Text.Trim() = "" Or txtALAMAT.Text.Trim() = "" Or txtKOTA.Text.Trim() = "" Or txtKTP.Text.Trim() = "" Or txtNPWP.Text.Trim() = "" Then
                MessageBox.Show("Semua field wajib diisi!", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning)

                ' === 3. Cek apakah KDUSER sudah terdaftar di zusers ===
            Else
                BukaKoneksi()
                Dim CekSup As String = "SELECT COUNT(*) FROM zsupplier WHERE kodesup = ?"
                Dim supExist As Boolean = False
                Using cmdCekSup As New OdbcCommand(CekSup, Conn)
                    cmdCekSup.Parameters.AddWithValue("@kodesup", txtKDSUP.Text.Trim())
                    supExist = Convert.ToInt32(cmdCekSup.ExecuteScalar())
                End Using

                If statusMode.ToLower() = "tambah" Then
                    If supExist = False Then
                        ' === 5. Semua validasi lolos → simpan data ===
                        SimpanSupplier(txtKDSUP.Text, txtNMSUP.Text, txtALAMAT.Text, txtKOTA.Text, txtKTP.Text, txtNPWP.Text, statusMode)
                        MessageBox.Show("Data Supplier berhasil disimpan", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information)

                        statusMode = ""
                        SetButtonState(Me, True)
                        DisabledLoad()

                    ElseIf supExist = True Then
                        MessageBox.Show("Kode Supplier sudah terdaftar!" & vbCrLf &
                                        "Tidak dapat menyimpan data baru.", "Validasi",
                                        MessageBoxButtons.OK, MessageBoxIcon.Warning)

                    End If
                ElseIf statusMode.ToLower() = "ubah" Then
                    ' === 5. Semua validasi lolos → simpan data ===
                    SimpanSupplier(txtKDSUP.Text, txtNMSUP.Text, txtALAMAT.Text, txtKOTA.Text, txtKTP.Text, txtNPWP.Text, statusMode)
                    MessageBox.Show("Data Supplier berhasil disimpan", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information)

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

    Public Sub SimpanSupplier(ByVal kdSup As String,
                          ByVal nmSup As String,
                          ByVal alamat As String,
                          ByVal kota As String,
                          ByVal ktp As String,
                          ByVal npwp As String,
                          ByVal status As String)

        Dim Trans As OdbcTransaction = Nothing

        Try
            If kdSup = "" Then Throw New Exception("Kode Supplier tidak boleh kosong!")

            BukaKoneksi()
            Trans = Conn.BeginTransaction()

            ' Jika mode UBAH, hapus dulu data lama
            If status.ToLower() = "ubah" Then
                KodeLama = kdSup
                Using cmdDel As New OdbcCommand("DELETE FROM zsupplier WHERE kodesup = ?", Conn, Trans)
                    cmdDel.Parameters.AddWithValue("@kodesup", kdSup)
                    cmdDel.ExecuteNonQuery()
                End Using

                Using cmd As New OdbcCommand("INSERT INTO zsupplier (kodesup, namasup, alamat, kota, ktp, npwp) VALUES (?, ?, ?, ?, ?, ?)", Conn, Trans)
                    cmd.Parameters.AddWithValue("@kodesup", kdSup)
                    cmd.Parameters.AddWithValue("@namasup", nmSup)
                    cmd.Parameters.AddWithValue("@alamat", alamat)
                    cmd.Parameters.AddWithValue("@kota", kota)
                    cmd.Parameters.AddWithValue("@ktp", ktp)
                    cmd.Parameters.AddWithValue("@npwp", npwp)
                    cmd.ExecuteNonQuery()
                End Using

            ElseIf status.ToLower() = "tambah" Then
                Using cmd As New OdbcCommand("INSERT INTO zsupplier (kodesup, namasup, alamat, kota, ktp, npwp) VALUES (?, ?, ?, ?, ?, ?)", Conn, Trans)
                    cmd.Parameters.AddWithValue("@kodesup", kdSup)
                    cmd.Parameters.AddWithValue("@namasup", nmSup)
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

            KodeLama = kdSup
            MsgBox("Gagal menyimpan data supplier: " & ex.Message, vbCritical)
        End Try
    End Sub

    Private Sub btnBATAL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBATAL.Click
        SetButtonState(Me, True)
        statusMode = ""
        DisabledLoad()
    End Sub

    Private Sub tUser_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles tSKDSUP.TextChanged, tSNMSUP.TextChanged

        Dim txt As TextBox = CType(sender, TextBox)
        If txt Is tSKDSUP Then
            tSNMSUP.Enabled = (tSKDSUP.Text.Trim() = "")
        ElseIf txt Is tSNMSUP Then
            tSKDSUP.Enabled = (tSNMSUP.Text.Trim() = "")
        End If
    End Sub

    Private Sub btnCARI_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCARI.Click
        Dim f As New ItDtSupplier()

        Dim kodesup As String = tSKDSUP.Text.Trim()
        Dim namasup As String = tSNMSUP.Text.Trim()

        ' Load data sesuai filter
        f.Owner = Me
        f.Show()
        f.LoadDataSupplier(kodesup, namasup)

        ' === Clear filter setelah pencarian ===
        tSKDSUP.Clear()
        tSNMSUP.Clear()
    End Sub
End Class