Imports System.Data.Odbc

Public Class ItGolongan

    Public Sub LoadDataGolongan(ByVal keyword As String, ByVal mode As String)

        Dim sql As String = ""
        If mode = "KODE" AndAlso keyword = "*" Then
            sql = "SELECT kodegol, namagol FROM zgolongan ORDER BY kodegol"
        ElseIf mode = "NAMA" AndAlso keyword = "*" Then
            sql = "SELECT kodegol, namagol FROM zgolongan ORDER BY kodegol"
        ElseIf mode = "KODE" Then
            sql = "SELECT kodegol, namagol FROM zgolongan " &
                  "WHERE kodegol LIKE ? ORDER BY kodegol LIMIT 50"
        ElseIf mode = "NAMA" Then
            sql = "SELECT kodegol, namagol FROM zgolongan " &
                  "WHERE namagol LIKE ? ORDER BY namagol LIMIT 50"
        End If

        dgitmGOL.Rows.Clear()

        Try
            BukaKoneksi()
            Cmd = New OdbcCommand(sql, Conn)

            If keyword <> "*" AndAlso keyword <> "" Then
                Cmd.Parameters.AddWithValue("@p1", keyword & "%")
            End If

            Rd = Cmd.ExecuteReader()

            While Rd.Read()
                dgitmGOL.Rows.Add(Rd("kodegol"), Rd("namagol"))
            End While

            Rd.Close()
            Conn.Close()
        Catch ex As Exception
            MessageBox.Show("Gagal load data: " & ex.Message)
        End Try
    End Sub

    Private Sub ItGolongan_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.FormBorderStyle = FormBorderStyle.FixedSingle
        Me.MaximizeBox = False

        SetupGridDGolongan(dgitmGOL)
    End Sub

    Private Sub dgitmGOL_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgitmGOL.CellContentClick
        If e.RowIndex >= 0 Then
            Dim kode As String = dgitmGOL.Rows(e.RowIndex).Cells("kodegol").Value.ToString()
            Dim nama As String = dgitmGOL.Rows(e.RowIndex).Cells("namagol").Value.ToString()

            ' kirim ke ItFPopupPem
            Dim parentForm As FStok = TryCast(Me.Owner, FStok)
            If parentForm IsNot Nothing Then
                parentForm.SetGolongan(kode, nama)
            End If

            Me.Close()
        End If
    End Sub
End Class