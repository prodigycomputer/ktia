Imports System.Data.Odbc

Public Class ItGrup

    Public Sub LoadDataGrup(ByVal keyword As String)

        Dim sql As String
        If keyword = "*" Then
            sql = "SELECT kodegrup, namagrup FROM zgrup ORDER BY kodegrup"
        Else
            sql = "SELECT kodegrup, namagrup " &
                  "FROM zgrup WHERE kodegrup LIKE '%" & keyword & "%' OR namagrup LIKE '%" & keyword & "%' ORDER BY kodegrup"
        End If

        dgitmGRUP.Rows.Clear()

        Try
            BukaKoneksi()
            Cmd = New OdbcCommand(sql, Conn)
            Rd = Cmd.ExecuteReader()

            While Rd.Read()
                dgitmGRUP.Rows.Add(Rd("kodegrup"), Rd("namagrup"))
            End While

            Rd.Close()
            Conn.Close()
        Catch ex As Exception
            MessageBox.Show("Gagal load data: " & ex.Message)
        End Try
    End Sub

    Private Sub ItGrup_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.FormBorderStyle = FormBorderStyle.FixedSingle
        Me.MaximizeBox = False

        SetupGridDGrup(dgitmGRUP)
    End Sub

    Private Sub dgitmGRUP_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgitmGRUP.CellContentClick
        If e.RowIndex >= 0 Then
            Dim kode As String = dgitmGRUP.Rows(e.RowIndex).Cells("kodegrup").Value.ToString()
            Dim nama As String = dgitmGRUP.Rows(e.RowIndex).Cells("namagrup").Value.ToString()

            ' kirim ke ItFPopupPem
            Dim parentForm As FStok = TryCast(Me.Owner, FStok)
            If parentForm IsNot Nothing Then
                parentForm.SetGrup(kode, nama)
            End If

            Me.Close()
        End If
    End Sub
End Class