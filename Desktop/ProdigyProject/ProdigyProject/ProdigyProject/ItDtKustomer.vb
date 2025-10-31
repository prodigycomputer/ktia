Imports System.Data.Odbc

Public Class ItDtKustomer

    Public Sub LoadDataKustomer(Optional ByVal kodekust As String = "",
                        Optional ByVal namakust As String = "")

        Dim sql As String = "SELECT kodekust, namakust, alamat, kota, kodehrg, ktp, npwp, kodetipe, kodear FROM zkustomer"
        Dim whereList As New List(Of String)

        ' --- Filter berdasarkan kodeuser
        If kodekust <> "" Then
            whereList.Add("kodekust LIKE ?")
        End If

        ' --- Filter berdasarkan username
        If namakust <> "" Then
            whereList.Add("namakust LIKE ?")
        End If

        If whereList.Count > 0 Then
            sql &= " WHERE " & String.Join(" AND ", whereList)
        End If

        sql &= " ORDER BY kodekust"

        dgitmKUSTT.Rows.Clear()

        Try
            BukaKoneksi()
            Cmd = New OdbcCommand(sql, Conn)

            ' --- Bind parameter sesuai urutan filter
            If kodekust <> "" Then
                Cmd.Parameters.AddWithValue("", "%" & kodekust & "%")
            End If
            If namakust <> "" Then
                Cmd.Parameters.AddWithValue("", "%" & namakust & "%")
            End If

            Rd = Cmd.ExecuteReader()
            While Rd.Read()
                dgitmKUSTT.Rows.Add(
                    Rd("kodekust").ToString(),
                    Rd("namakust").ToString(),
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

    Private Sub ItDtKustomer_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.FormBorderStyle = FormBorderStyle.FixedSingle
        Me.MaximizeBox = False

        SetupGridKustomer(dgitmKUSTT)
    End Sub

    Private Sub dgitmKUSTT_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgitmKUSTT.CellContentClick
        If e.RowIndex >= 0 Then
            Dim data As String = dgitmKUSTT.Rows(e.RowIndex).Cells("kodekust").Value.ToString()

            Dim parentForm As FKustomer = TryCast(Me.Owner, FKustomer)
            If parentForm IsNot Nothing Then
                parentForm.LoadData(data)
            End If

            Me.Close()
        End If
    End Sub
End Class