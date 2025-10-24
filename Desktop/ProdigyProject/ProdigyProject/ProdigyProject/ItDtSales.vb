Imports System.Data.Odbc

Public Class ItDtSales

    Public Sub LoadDataSales(Optional ByVal kodesls As String = "",
                        Optional ByVal namasls As String = "")

        Dim sql As String = "SELECT kodesls, namasls, alamat, kota, ktp, npwp FROM zsales"
        Dim whereList As New List(Of String)

        ' --- Filter berdasarkan kodeuser
        If kodesls <> "" Then
            whereList.Add("kodesls LIKE ?")
        End If

        ' --- Filter berdasarkan username
        If namasls <> "" Then
            whereList.Add("namasls LIKE ?")
        End If

        If whereList.Count > 0 Then
            sql &= " WHERE " & String.Join(" AND ", whereList)
        End If

        sql &= " ORDER BY kodesls"

        dgitmSLSS.Rows.Clear()

        Try
            BukaKoneksi()
            Cmd = New OdbcCommand(sql, Conn)

            ' --- Bind parameter sesuai urutan filter
            If kodesls <> "" Then
                Cmd.Parameters.AddWithValue("", "%" & kodesls & "%")
            End If
            If namasls <> "" Then
                Cmd.Parameters.AddWithValue("", "%" & namasls & "%")
            End If

            Rd = Cmd.ExecuteReader()
            While Rd.Read()
                dgitmSLSS.Rows.Add(
                    Rd("kodesls").ToString(),
                    Rd("namasls").ToString(),
                    Rd("alamat").ToString(),
                    Rd("kota").ToString(),
                    Rd("ktp").ToString(),
                    Rd("npwp").ToString()
                )
            End While
            Rd.Close()
            Conn.Close()
        Catch ex As Exception
            MessageBox.Show("Gagal load data: " & ex.Message)
        End Try
    End Sub

    Private Sub ItDtSales_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.FormBorderStyle = FormBorderStyle.FixedSingle
        Me.MaximizeBox = False

        SetupGridSales(dgitmSLSS)
    End Sub

    Private Sub dgitmSLSS_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgitmSLSS.CellContentClick
        If e.RowIndex >= 0 Then
            Dim data As String = dgitmSLSS.Rows(e.RowIndex).Cells("kodesls").Value.ToString()

            Dim parentForm As FSales = TryCast(Me.Owner, FSales)
            If parentForm IsNot Nothing Then
                parentForm.LoadData(data)
            End If

            Me.Close()
        End If
    End Sub
End Class