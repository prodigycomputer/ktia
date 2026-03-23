Imports System.Data.Odbc

Public Class ItDtSupplier

    Public Sub LoadDataSupplier(Optional ByVal kodesup As String = "",
                        Optional ByVal namasup As String = "")

        Dim sql As String = "SELECT kodesup, namasup, alamat, kota, ktp, npwp FROM zsupplier"
        Dim whereList As New List(Of String)

        ' --- Filter berdasarkan kodeuser
        If kodesup <> "" Then
            whereList.Add("kodesup LIKE ?")
        End If

        ' --- Filter berdasarkan username
        If namasup <> "" Then
            whereList.Add("namasup LIKE ?")
        End If

        If whereList.Count > 0 Then
            sql &= " WHERE " & String.Join(" AND ", whereList)
        End If

        sql &= " ORDER BY kodesup"

        dgitmSUPP.Rows.Clear()

        Try
            BukaKoneksi()
            Cmd = New OdbcCommand(sql, Conn)

            ' --- Bind parameter sesuai urutan filter
            If kodesup <> "" Then
                Cmd.Parameters.AddWithValue("", "%" & kodesup & "%")
            End If
            If namasup <> "" Then
                Cmd.Parameters.AddWithValue("", "%" & namasup & "%")
            End If

            Rd = Cmd.ExecuteReader()
            While Rd.Read()
                dgitmSUPP.Rows.Add(
                    Rd("kodesup").ToString(),
                    Rd("namasup").ToString(),
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

    Private Sub ItDtSuplier_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.FormBorderStyle = FormBorderStyle.FixedSingle
        Me.MaximizeBox = False

        SetupGridSupplier(dgitmSUPP)
    End Sub

    Private Sub PilihData()
        If dgitmSUPP.CurrentRow Is Nothing Then Exit Sub

        Dim data As String = dgitmSUPP.CurrentRow.Cells("kodesup").Value.ToString()

        Dim parentForm As FSupplier = TryCast(Me.Owner, FSupplier)
        If parentForm IsNot Nothing Then
            parentForm.LoadData(Data)
        End If

        Me.Close()
    End Sub

    Private Sub dgitmSUPP_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgitmSUPP.CellContentClick
        PilihData()
    End Sub

    Private Sub dgitmSUPP_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dgitmSUPP.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            PilihData()
        End If
    End Sub
End Class