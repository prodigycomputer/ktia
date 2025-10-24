﻿Imports System.Data.Odbc

Public Class FGrup
    Private statusMode As String = ""   ' status: "TAMBAH" / "UBAH"
    Public KodeLama As String = ""

    Private Sub FGrup_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetButtonState(Me, True)
        BukaKoneksi()
        DisabledLoad()
    End Sub

    Public Sub LoadData(ByVal kodegrup As String)
        Try
            BukaKoneksi()

            Dim sql As String = "SELECT kodegrup, namagrup FROM zgrup WHERE kodegrup = ?"
            Cmd = New OdbcCommand(sql, Conn)
            Cmd.Parameters.AddWithValue("", kodegrup)

            Rd = Cmd.ExecuteReader()

            If Rd.Read() Then
                ' === Isi field ke textbox di form ===
                txtKDGRUP.Text = Rd("kodegrup").ToString()
                txtNMGRUP.Text = Rd("namagrup").ToString()

                ' Kalau ada field lain (misalnya level, aktif, dsb) bisa ditambah di sini
                ' txtLEVEL.Text = Rd("level").ToString()
            Else
                MessageBox.Show("Data Grup tidak ditemukan.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

        Catch ex As Exception
            MessageBox.Show("Gagal memuat data user: " & ex.Message)
        End Try
    End Sub

    Private Sub KosongkanInput()
        txtKDGRUP.Text = ""
        txtNMGRUP.Text = ""
        tSKDGRUP.Text = ""
        tSNMGRUP.Text = ""
    End Sub

    Private Sub DisabledLoad()
        txtKDGRUP.Enabled = False
        txtNMGRUP.Enabled = False
        tSKDGRUP.Enabled = True
        tSNMGRUP.Enabled = True
    End Sub

    Private Sub EnabledLoad()
        txtKDGRUP.Enabled = True
        txtNMGRUP.Enabled = True
        tSKDGRUP.Enabled = False
        tSNMGRUP.Enabled = False
    End Sub

    Private Sub btnTAMBAH_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTAMBAH.Click
        SetButtonState(Me, False)
        statusMode = "tambah"
        KosongkanInput()
        EnabledLoad()
    End Sub

    Private Sub btnUBAH_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUBAH.Click
        Dim kodeGP As String = txtKDGRUP.Text.Trim()

        ' cek apakah kosong
        If kodeGP Is Nothing OrElse String.IsNullOrWhiteSpace(txtKDGRUP.Text) Then
            MessageBox.Show("Tidak ada data yang bisa di edit", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        EnabledLoad()
        SetButtonState(Me, False)
        statusMode = "ubah"
    End Sub

    Private Sub btnHAPUS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHAPUS.Click
        Try
            Dim kodeGP As String = txtKDGRUP.Text.Trim()

            If String.IsNullOrEmpty(kodeGP) Then
                MessageBox.Show("Tidak ada data yang bisa dihapus.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            If MessageBox.Show("Yakin hapus " & kodeGP & " ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then
                Exit Sub
            End If

            HapusGrup(txtKDGRUP.Text)
            KosongkanInput()
            SetButtonState(Me, True)
            statusMode = ""
            DisabledLoad()

            MessageBox.Show(kodeGP & " berhasil dihapus.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error Hapus", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub HapusGrup(ByVal kdGp As String)

        Try
            BukaKoneksi()
            Using Trans = Conn.BeginTransaction()
                Using cmd As New OdbcCommand("DELETE FROM zgrup WHERE kodegrup = ?", Conn, Trans)
                    cmd.Parameters.AddWithValue("@kodegrup", kdGp)
                    cmd.ExecuteNonQuery()
                End Using

                Trans.Commit()
            End Using

        Catch ex As Exception
            MsgBox("Gagal menghapus data grup: " & ex.Message, vbCritical)
        End Try
    End Sub

    Private Sub btnSIMPAN_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSIMPAN.Click
        Try
            ' === 1. Validasi wajib diisi ===
            If txtKDGRUP.Text.Trim() = "" Or txtNMGRUP.Text.Trim() = "" Then
                MessageBox.Show("Semua field wajib diisi!", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning)

                ' === 3. Cek apakah KDUSER sudah terdaftar di zusers ===
            Else
                BukaKoneksi()
                Dim CekGp As String = "SELECT COUNT(*) FROM zgrup WHERE kodegrup = ?"
                Dim gpExist As Boolean = False
                Using cmdCekGp As New OdbcCommand(CekGp, Conn)
                    cmdCekGp.Parameters.AddWithValue("@kodegrup", txtKDGRUP.Text.Trim())
                    gpExist = Convert.ToInt32(cmdCekGp.ExecuteScalar())
                End Using

                If statusMode.ToLower() = "tambah" Then
                    If gpExist = False Then
                        ' === 5. Semua validasi lolos → simpan data ===
                        SimpanGrup(txtKDGRUP.Text, txtNMGRUP.Text, statusMode)
                        MessageBox.Show("Data Grup berhasil disimpan", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information)

                        statusMode = ""
                        SetButtonState(Me, True)
                        DisabledLoad()

                    ElseIf gpExist = True Then
                        MessageBox.Show("Kode Grup sudah terdaftar!" & vbCrLf &
                                        "Tidak dapat menyimpan data baru.", "Validasi",
                                        MessageBoxButtons.OK, MessageBoxIcon.Warning)

                    End If
                ElseIf statusMode.ToLower() = "ubah" Then
                    ' === 5. Semua validasi lolos → simpan data ===
                    SimpanGrup(txtKDGRUP.Text, txtNMGRUP.Text, statusMode)
                    MessageBox.Show("Data Grup berhasil disimpan", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information)

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

    Public Sub SimpanGrup(ByVal kdGp As String,
                          ByVal nmGp As String,
                          ByVal status As String)

        Dim Trans As OdbcTransaction = Nothing

        Try
            If kdGp = "" Then Throw New Exception("Kode Grup tidak boleh kosong!")

            BukaKoneksi()
            Trans = Conn.BeginTransaction()

            ' Jika mode UBAH, hapus dulu data lama
            If status.ToLower() = "ubah" Then
                KodeLama = kdGp
                Using cmdDel As New OdbcCommand("DELETE FROM zgrup WHERE kodegrup = ?", Conn, Trans)
                    cmdDel.Parameters.AddWithValue("@kodegrup", kdGp)
                    cmdDel.ExecuteNonQuery()
                End Using

                Using cmd As New OdbcCommand("INSERT INTO zgrup (kodegrup, namagrup) VALUES (?, ?)", Conn, Trans)
                    cmd.Parameters.AddWithValue("@kodegrup", kdGp)
                    cmd.Parameters.AddWithValue("@namagrup", nmGp)
                    cmd.ExecuteNonQuery()
                End Using

            ElseIf status.ToLower() = "tambah" Then
                Using cmd As New OdbcCommand("INSERT INTO zgrup (kodegrup, namagrup) VALUES (?, ?)", Conn, Trans)
                    cmd.Parameters.AddWithValue("@kodegrup", kdGp)
                    cmd.Parameters.AddWithValue("@namagrup", nmGp)
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

            KodeLama = kdGp
            MsgBox("Gagal menyimpan data grup: " & ex.Message, vbCritical)
        End Try
    End Sub

    Private Sub btnBATAL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBATAL.Click
        SetButtonState(Me, True)
        statusMode = ""
        DisabledLoad()
    End Sub

    Private Sub tUser_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles tSKDGRUP.TextChanged, tSNMGRUP.TextChanged

        Dim txt As TextBox = CType(sender, TextBox)
        If txt Is tSKDGRUP Then
            tSNMGRUP.Enabled = (tSKDGRUP.Text.Trim() = "")
        ElseIf txt Is tSNMGRUP Then
            tSKDGRUP.Enabled = (tSNMGRUP.Text.Trim() = "")
        End If
    End Sub

    Private Sub btnCARI_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCARI.Click
        Dim f As New ItDtGrup()

        Dim kodeGP As String = tSKDGRUP.Text.Trim()
        Dim namaGP As String = tSNMGRUP.Text.Trim()

        ' Load data sesuai filter
        f.Owner = Me
        f.Show()
        f.LoadDataGrup(kodeGP, namaGP)

        ' === Clear filter setelah pencarian ===
        tSKDGRUP.Clear()
        tSNMGRUP.Clear()
    End Sub
End Class