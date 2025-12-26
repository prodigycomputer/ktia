Imports System.Data.Odbc

Public Class FLogin

    Private isExitConfirmed As Boolean = False

    Private Sub FLogin_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.FormBorderStyle = FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
    End Sub

    Private Sub btnLOGIN_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLOGIN.Click
        Try
            BukaKoneksi()

            Dim sql As String = "SELECT * FROM zusers WHERE username=? AND kunci=?"
            Cmd = New OdbcCommand(sql, Conn)

            ' Tambahkan parameter sesuai urutan tanda ?
            Cmd.Parameters.AddWithValue("@username", tbUSERNAME.Text.Trim())
            Cmd.Parameters.AddWithValue("@kunci", tbPASSWORD.Text.Trim())

            Rd = Cmd.ExecuteReader()

            If Rd.Read() Then
                ' Simpan kode user yang login ke variabel global
                KodeUserLogin = Rd("kodeuser").ToString()
                NamaUserLogin = Rd("username").ToString()
                Me.DialogResult = DialogResult.OK
                Me.Close()
            Else
                ' Login gagal → beri pesan, jangan tutup form
                MessageBox.Show("Username atau Password salah!", "Login Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error)
                tbPASSWORD.Clear()
                tbPASSWORD.Focus()
            End If

            Rd.Close()
            Conn.Close()

        Catch ex As Exception
            MessageBox.Show("Terjadi kesalahan: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnCANCEL_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCANCEL.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub
End Class
