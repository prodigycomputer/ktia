Imports System.Data.Odbc

Public Class ItDtGudang

    Public Sub LoadDataGudang(Optional ByVal kodeGD As String = "",
                        Optional ByVal namaGD As String = "")

        Dim sql As String = "SELECT kodegd, namagd FROM zgudang"
        Dim whereList As New List(Of String)

        ' --- Filter berdasarkan kodeuser
        If kodeGD <> "" Then
            whereList.Add("kodegd LIKE ?")
        End If

        ' --- Filter berdasarkan username
        If namaGD <> "" Then
            whereList.Add("namagd LIKE ?")
        End If

        If whereList.Count > 0 Then
            sql &= " WHERE " & String.Join(" AND ", whereList)
        End If

        sql &= " ORDER BY kodegd"

        dgitmGD.Rows.Clear()

        Try
            BukaKoneksi()
            Cmd = New OdbcCommand(sql, Conn)

            ' --- Bind parameter sesuai urutan filter
            If kodeGD <> "" Then
                Cmd.Parameters.AddWithValue("", "%" & kodeGD & "%")
            End If
            If namaGD <> "" Then
                Cmd.Parameters.AddWithValue("", "%" & namaGD & "%")
            End If

            Rd = Cmd.ExecuteReader()
            While Rd.Read()
                dgitmGD.Rows.Add(
                    Rd("kodegd").ToString(),
                    Rd("namagd").ToString()
                )
            End While
            Rd.Close()
            Conn.Close()
        Catch ex As Exception
            MessageBox.Show("Gagal load data: " & ex.Message)
        End Try
    End Sub

    Private Sub ItDtGudang_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.FormBorderStyle = FormBorderStyle.FixedSingle
        Me.MaximizeBox = False

        SetupGridDGudang(dgitmGD)
    End Sub

    Private Sub dgitmGD_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgitmGD.CellContentClick
        If e.RowIndex >= 0 Then
            Dim data As String = dgitmGD.Rows(e.RowIndex).Cells("kodegd").Value.ToString()

            Dim parentForm As FGudang = TryCast(Me.Owner, FGudang)
            If parentForm IsNot Nothing Then
                parentForm.LoadData(data)
            End If

            Me.Close()
        End If
    End Sub
End Class