Imports System.Data.Odbc

Public Class ItDtGrup

    Public Sub LoadDataGrup(Optional ByVal kodeGP As String = "",
                        Optional ByVal namaGP As String = "")

        Dim sql As String = "SELECT kodegrup, namagrup FROM zgrup"
        Dim whereList As New List(Of String)

        ' --- Filter berdasarkan kodeuser
        If kodeGP <> "" Then
            whereList.Add("kodegrup LIKE ?")
        End If

        ' --- Filter berdasarkan username
        If namaGP <> "" Then
            whereList.Add("namagrup LIKE ?")
        End If

        If whereList.Count > 0 Then
            sql &= " WHERE " & String.Join(" AND ", whereList)
        End If

        sql &= " ORDER BY kodegrup"

        dgitmGP.Rows.Clear()

        Try
            BukaKoneksi()
            Cmd = New OdbcCommand(sql, Conn)

            ' --- Bind parameter sesuai urutan filter
            If kodeGP <> "" Then
                Cmd.Parameters.AddWithValue("", "%" & kodeGP & "%")
            End If
            If namaGP <> "" Then
                Cmd.Parameters.AddWithValue("", "%" & namaGP & "%")
            End If

            Rd = Cmd.ExecuteReader()
            While Rd.Read()
                dgitmGP.Rows.Add(
                    Rd("kodegrup").ToString(),
                    Rd("namagrup").ToString()
                )
            End While
            Rd.Close()
            Conn.Close()
        Catch ex As Exception
            MessageBox.Show("Gagal load data: " & ex.Message)
        End Try
    End Sub

    Private Sub ItDtGrup_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.FormBorderStyle = FormBorderStyle.FixedSingle
        Me.MaximizeBox = False

        SetupGridDGrup(dgitmGP)
    End Sub

    Private Sub dgitmGP_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgitmGP.CellContentClick
        If e.RowIndex >= 0 Then
            Dim data As String = dgitmGP.Rows(e.RowIndex).Cells("kodegrup").Value.ToString()

            Dim parentForm As FGrup = TryCast(Me.Owner, FGrup)
            If parentForm IsNot Nothing Then
                parentForm.LoadData(data)
            End If

            Me.Close()
        End If
    End Sub
End Class