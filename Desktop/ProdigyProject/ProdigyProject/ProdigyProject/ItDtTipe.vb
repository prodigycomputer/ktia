Imports System.Data.Odbc

Public Class ItDtTipe

    Public Sub LoadDataTipe(Optional ByVal kodetipe As String = "",
                        Optional ByVal namatipe As String = "")

        Dim sql As String = "SELECT kodetipe, namatipe FROM ztipe"
        Dim whereList As New List(Of String)

        ' --- Filter berdasarkan kodeuser
        If kodetipe <> "" Then
            whereList.Add("kodetipe LIKE ?")
        End If

        ' --- Filter berdasarkan username
        If namatipe <> "" Then
            whereList.Add("namatipe LIKE ?")
        End If

        If whereList.Count > 0 Then
            sql &= " WHERE " & String.Join(" AND ", whereList)
        End If

        sql &= " ORDER BY kodetipe"

        dgitmTIPE.Rows.Clear()

        Try
            BukaKoneksi()
            Cmd = New OdbcCommand(sql, Conn)

            ' --- Bind parameter sesuai urutan filter
            If kodetipe <> "" Then
                Cmd.Parameters.AddWithValue("", "%" & kodetipe & "%")
            End If
            If namatipe <> "" Then
                Cmd.Parameters.AddWithValue("", "%" & namatipe & "%")
            End If

            Rd = Cmd.ExecuteReader()
            While Rd.Read()
                dgitmTIPE.Rows.Add(
                    Rd("kodetipe").ToString(),
                    Rd("namatipe").ToString()
                )
            End While
            Rd.Close()
            Conn.Close()
        Catch ex As Exception
            MessageBox.Show("Gagal load data: " & ex.Message)
        End Try
    End Sub

    Private Sub ItDtTipe_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.FormBorderStyle = FormBorderStyle.FixedSingle
        Me.MaximizeBox = False

        SetupGridDTipe(dgitmTIPE)
    End Sub

    Private Sub dgitmTIPE_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgitmTIPE.CellContentClick
        If e.RowIndex >= 0 Then
            Dim data As String = dgitmTIPE.Rows(e.RowIndex).Cells("kodetipe").Value.ToString()

            Dim parentForm As FTipe = TryCast(Me.Owner, FTipe)
            If parentForm IsNot Nothing Then
                parentForm.LoadData(data)
            End If

            Me.Close()
        End If
    End Sub
End Class