Imports System.Data.Odbc

Public Class ItDtArea

    Public Sub LoadDataArea(Optional ByVal kodearea As String = "",
                        Optional ByVal namaarea As String = "")

        Dim sql As String = "SELECT kodear, namaar FROM zarea"
        Dim whereList As New List(Of String)

        ' --- Filter berdasarkan kodeuser
        If kodearea <> "" Then
            whereList.Add("kodear LIKE ?")
        End If

        ' --- Filter berdasarkan username
        If namaarea <> "" Then
            whereList.Add("namaar LIKE ?")
        End If

        If whereList.Count > 0 Then
            sql &= " WHERE " & String.Join(" AND ", whereList)
        End If

        sql &= " ORDER BY kodear"

        dgitmAREA.Rows.Clear()

        Try
            BukaKoneksi()
            Cmd = New OdbcCommand(sql, Conn)

            ' --- Bind parameter sesuai urutan filter
            If kodearea <> "" Then
                Cmd.Parameters.AddWithValue("", "%" & kodearea & "%")
            End If
            If namaarea <> "" Then
                Cmd.Parameters.AddWithValue("", "%" & namaarea & "%")
            End If

            Rd = Cmd.ExecuteReader()
            While Rd.Read()
                dgitmAREA.Rows.Add(
                    Rd("kodear").ToString(),
                    Rd("namaar").ToString()
                )
            End While
            Rd.Close()
            Conn.Close()
        Catch ex As Exception
            MessageBox.Show("Gagal load data: " & ex.Message)
        End Try
    End Sub

    Private Sub ItDtArea_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.FormBorderStyle = FormBorderStyle.FixedSingle
        Me.MaximizeBox = False

        SetupGridDArea(dgitmAREA)
    End Sub

    Private Sub dgitmAREA_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgitmAREA.CellContentClick
        If e.RowIndex >= 0 Then
            Dim data As String = dgitmAREA.Rows(e.RowIndex).Cells("kodear").Value.ToString()

            Dim parentForm As FArea = TryCast(Me.Owner, FArea)
            If parentForm IsNot Nothing Then
                parentForm.LoadData(data)
            End If

            Me.Close()
        End If
    End Sub
End Class