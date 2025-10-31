Imports System.Data.Odbc

Public Class ItDtMerek

    Public Sub LoadDataMerek(Optional ByVal kodeMR As String = "",
                        Optional ByVal namaMR As String = "")

        Dim sql As String = "SELECT kodemerk, namamerk FROM zmerek"
        Dim whereList As New List(Of String)

        ' --- Filter berdasarkan kodeuser
        If kodeMR <> "" Then
            whereList.Add("kodemerk LIKE ?")
        End If

        ' --- Filter berdasarkan username
        If namaMR <> "" Then
            whereList.Add("namamerk LIKE ?")
        End If

        If whereList.Count > 0 Then
            sql &= " WHERE " & String.Join(" AND ", whereList)
        End If

        sql &= " ORDER BY kodemerk"

        dgitmMR.Rows.Clear()

        Try
            BukaKoneksi()
            Cmd = New OdbcCommand(sql, Conn)

            ' --- Bind parameter sesuai urutan filter
            If kodeMR <> "" Then
                Cmd.Parameters.AddWithValue("", "%" & kodeMR & "%")
            End If
            If namaMR <> "" Then
                Cmd.Parameters.AddWithValue("", "%" & namaMR & "%")
            End If

            Rd = Cmd.ExecuteReader()
            While Rd.Read()
                dgitmMR.Rows.Add(
                    Rd("kodemerk").ToString(),
                    Rd("namamerk").ToString()
                )
            End While
            Rd.Close()
            Conn.Close()
        Catch ex As Exception
            MessageBox.Show("Gagal load data: " & ex.Message)
        End Try
    End Sub

    Private Sub ItDtMerek_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.FormBorderStyle = FormBorderStyle.FixedSingle
        Me.MaximizeBox = False

        SetupGridDMerek(dgitmMR)
    End Sub

    Private Sub dgitmMR_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgitmMR.CellContentClick
        If e.RowIndex >= 0 Then
            Dim data As String = dgitmMR.Rows(e.RowIndex).Cells("kodemerk").Value.ToString()

            Dim parentForm As FMerek = TryCast(Me.Owner, FMerek)
            If parentForm IsNot Nothing Then
                parentForm.LoadData(data)
            End If

            Me.Close()
        End If
    End Sub
End Class