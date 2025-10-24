Imports System.Data.Odbc

Public Class FMerek
    Private statusMode As String = ""   ' status: "TAMBAH" / "UBAH"
    Public KodeLama As String = ""

    Private Sub FMerek_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetButtonState(Me, True)
        BukaKoneksi()
        DisabledLoad()
    End Sub

    Public Sub LoadData(ByVal kodemerk As String)
        Try
            BukaKoneksi()

            Dim sql As String = "SELECT kodemerk, namamerk FROM zmerek WHERE kodemerk = ?"
            Cmd = New OdbcCommand(sql, Conn)
            Cmd.Parameters.AddWithValue("", kodemerk)

            Rd = Cmd.ExecuteReader()

            If Rd.Read() Then
                ' === Isi field ke textbox di form ===
                txtKDMEREK.Text = Rd("kodemerk").ToString()
                txtNMMEREK.Text = Rd("namamerk").ToString()

                ' Kalau ada field lain (misalnya level, aktif, dsb) bisa ditambah di sini
                ' txtLEVEL.Text = Rd("level").ToString()
            Else
                MessageBox.Show("Data Merek tidak ditemukan.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

        Catch ex As Exception
            MessageBox.Show("Gagal memuat data user: " & ex.Message)
        End Try
    End Sub

    Private Sub KosongkanInput()
        txtKDMEREK.Text = ""
        txtNMMEREK.Text = ""
        tSKDMEREK.Text = ""
        tSNMMEREK.Text = ""
    End Sub

    Private Sub DisabledLoad()
        txtKDMEREK.Enabled = False
        txtNMMEREK.Enabled = False
        tSKDMEREK.Enabled = True
        tSNMMEREK.Enabled = True
    End Sub

    Private Sub EnabledLoad()
        txtKDMEREK.Enabled = True
        txtNMMEREK.Enabled = True
        tSKDMEREK.Enabled = False
        tSNMMEREK.Enabled = False
    End Sub

    Private Sub btnTAMBAH_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTAMBAH.Click
        SetButtonState(Me, False)
        statusMode = "tambah"
        KosongkanInput()
        EnabledLoad()
    End Sub

    Private Sub btnUBAH_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUBAH.Click
        Dim kodeMR As String = txtKDMEREK.Text.Trim()

        ' cek apakah kosong
        If kodeMR Is Nothing OrElse String.IsNullOrWhiteSpace(txtKDMEREK.Text) Then
            MessageBox.Show("Tidak ada data yang bisa di edit", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        EnabledLoad()
        SetButtonState(Me, False)
        statusMode = "ubah"
    End Sub

    Private Sub btnHAPUS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHAPUS.Click
        Try
            Dim kodeMR As String = txtKDMEREK.Text.Trim()

            If String.IsNullOrEmpty(kodeMR) Then
                MessageBox.Show("Tidak ada data yang bisa dihapus.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            If MessageBox.Show("Yakin hapus " & kodeMR & " ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then
                Exit Sub
            End If

            HapusMerek(txtKDMEREK.Text)
            KosongkanInput()
            SetButtonState(Me, True)
            statusMode = ""
            DisabledLoad()

            MessageBox.Show(kodeMR & " berhasil dihapus.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error Hapus", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub HapusMerek(ByVal kdMr As String)

        Try
            BukaKoneksi()
            Using Trans = Conn.BeginTransaction()
                Using cmd As New OdbcCommand("DELETE FROM zmerek WHERE kodemerk = ?", Conn, Trans)
                    cmd.Parameters.AddWithValue("@kodemerk", kdMr)
                    cmd.ExecuteNonQuery()
                End Using

                Trans.Commit()
            End Using

        Catch ex As Exception
            MsgBox("Gagal menghapus data merek: " & ex.Message, vbCritical)
        End Try
    End Sub

    Private Sub btnSIMPAN_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSIMPAN.Click
        Try
            ' === 1. Validasi wajib diisi ===
            If txtKDMEREK.Text.Trim() = "" Or txtNMMEREK.Text.Trim() = "" Then
                MessageBox.Show("Semua field wajib diisi!", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning)

                ' === 3. Cek apakah KDUSER sudah terdaftar di zusers ===
            Else
                BukaKoneksi()
                Dim CekMr As String = "SELECT COUNT(*) FROM zmerek WHERE kodemerk = ?"
                Dim mrExist As Boolean = False
                Using cmdCekMr As New OdbcCommand(CekMr, Conn)
                    cmdCekMr.Parameters.AddWithValue("@kodemerk", txtKDMEREK.Text.Trim())
                    mrExist = Convert.ToInt32(cmdCekMr.ExecuteScalar())
                End Using

                If statusMode.ToLower() = "tambah" Then
                    If mrExist = False Then
                        ' === 5. Semua validasi lolos → simpan data ===
                        SimpanMerek(txtKDMEREK.Text, txtNMMEREK.Text, statusMode)
                        MessageBox.Show("Data Merek berhasil disimpan", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information)

                        statusMode = ""
                        SetButtonState(Me, True)
                        DisabledLoad()

                    ElseIf mrExist = True Then
                        MessageBox.Show("Kode Merek sudah terdaftar!" & vbCrLf &
                                        "Tidak dapat menyimpan data baru.", "Validasi",
                                        MessageBoxButtons.OK, MessageBoxIcon.Warning)

                    End If
                ElseIf statusMode.ToLower() = "ubah" Then
                    ' === 5. Semua validasi lolos → simpan data ===
                    SimpanMerek(txtKDMEREK.Text, txtNMMEREK.Text, statusMode)
                    MessageBox.Show("Data Merek berhasil disimpan", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information)

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

    Public Sub SimpanMerek(ByVal kdMr As String,
                          ByVal nmMr As String,
                          ByVal status As String)

        Dim Trans As OdbcTransaction = Nothing

        Try
            If kdMr = "" Then Throw New Exception("Kode Merek tidak boleh kosong!")

            BukaKoneksi()
            Trans = Conn.BeginTransaction()

            ' Jika mode UBAH, hapus dulu data lama
            If status.ToLower() = "ubah" Then
                KodeLama = kdMr
                Using cmdDel As New OdbcCommand("DELETE FROM zmerek WHERE kodemerk = ?", Conn, Trans)
                    cmdDel.Parameters.AddWithValue("@kodemerk", kdMr)
                    cmdDel.ExecuteNonQuery()
                End Using

                Using cmd As New OdbcCommand("INSERT INTO zmerek (kodemerk, namamerk) VALUES (?, ?)", Conn, Trans)
                    cmd.Parameters.AddWithValue("@kodemerk", kdMr)
                    cmd.Parameters.AddWithValue("@namamerk", nmMr)
                    cmd.ExecuteNonQuery()
                End Using

            ElseIf status.ToLower() = "tambah" Then
                Using cmd As New OdbcCommand("INSERT INTO zmerek (kodemerk, namamerk) VALUES (?, ?)", Conn, Trans)
                    cmd.Parameters.AddWithValue("@kodemerk", kdMr)
                    cmd.Parameters.AddWithValue("@namamerk", nmMr)
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

            KodeLama = kdMr
            MsgBox("Gagal menyimpan data merek: " & ex.Message, vbCritical)
        End Try
    End Sub

    Private Sub btnBATAL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBATAL.Click
        SetButtonState(Me, True)
        statusMode = ""
        DisabledLoad()
    End Sub

    Private Sub tUser_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles tSKDMEREK.TextChanged, tSNMMEREK.TextChanged

        Dim txt As TextBox = CType(sender, TextBox)
        If txt Is tSKDMEREK Then
            tSNMMEREK.Enabled = (tSKDMEREK.Text.Trim() = "")
        ElseIf txt Is tSNMMEREK Then
            tSKDMEREK.Enabled = (tSNMMEREK.Text.Trim() = "")
        End If
    End Sub

    Private Sub btnCARI_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCARI.Click
        Dim f As New ItDtMerek()

        Dim kodeMR As String = tSKDMEREK.Text.Trim()
        Dim namaMR As String = tSNMMEREK.Text.Trim()

        ' Load data sesuai filter
        f.Owner = Me
        f.Show()
        f.LoadDataMerek(kodeMR, namaMR)

        ' === Clear filter setelah pencarian ===
        tSKDMEREK.Clear()
        tSNMMEREK.Clear()
    End Sub
End Class