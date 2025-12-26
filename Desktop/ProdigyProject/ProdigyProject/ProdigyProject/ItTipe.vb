Imports System.Data.Odbc

Public Class ItTipe

    Public Sub LoadDataTipe(ByVal keyword As String, ByVal mode As String)

        Dim sql As String = ""
        If mode = "KODE" AndAlso keyword = "*" Then
            sql = "SELECT kodetipe, namatipe FROM ztipe ORDER BY kodetipe"
        ElseIf mode = "NAMA" AndAlso keyword = "*" Then
            sql = "SELECT kodetipe, namatipe FROM ztipe ORDER BY kodetipe"
        ElseIf mode = "KODE" Then
            sql = "SELECT kodetipe, namatipe FROM ztipe " &
                  "WHERE kodetipe LIKE ? ORDER BY kodetipe LIMIT 50"
        ElseIf mode = "NAMA" Then
            sql = "SELECT kodetipe, namatipe FROM ztipe " &
                  "WHERE namatipe LIKE ? ORDER BY namatipe LIMIT 50"
        End If

        dgitmTIPE.Rows.Clear()

        Try
            BukaKoneksi()
            Cmd = New OdbcCommand(sql, Conn)

            If keyword <> "*" AndAlso keyword <> "" Then
                Cmd.Parameters.AddWithValue("@p1", keyword & "%")
            End If

            Rd = Cmd.ExecuteReader()

            While Rd.Read()
                dgitmTIPE.Rows.Add(Rd("kodetipe"), Rd("namatipe"))
            End While

            Rd.Close()
            Conn.Close()
        Catch ex As Exception
            MessageBox.Show("Gagal load data: " & ex.Message)
        End Try
    End Sub

    Private Sub ItTipe_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.FormBorderStyle = FormBorderStyle.FixedSingle
        Me.MaximizeBox = False

        SetupGridTipe(dgitmTIPE)
    End Sub

    Private Sub dgitmTIPE_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgitmTIPE.CellContentClick
        If e.RowIndex >= 0 Then
            Dim kodet As String = dgitmTIPE.Rows(e.RowIndex).Cells("kodetipe").Value.ToString()
            Dim namat As String = dgitmTIPE.Rows(e.RowIndex).Cells("namatipe").Value.ToString()

            ' kirim ke ItFPopupPem
            Dim parentForm As FKustomer = TryCast(Me.Owner, FKustomer)
            If parentForm IsNot Nothing Then
                parentForm.SetTipe(kodet, namat)
            End If

            Me.Close()
        End If
    End Sub
End Class