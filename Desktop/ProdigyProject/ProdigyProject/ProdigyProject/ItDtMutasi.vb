Imports System.Data.Odbc

Public Class ItDtMutasi

    Public Sub LoadDataMut(ByVal keyword As String)

        Dim sql As String
        If keyword = "*" Then
            sql = "SELECT kodekust, namakust, alamat, kota, ktp, npwp FROM zkustomer ORDER BY kodekust"
        Else
            sql = "SELECT kodekust, namakust, alamat, kota, ktp, npwp " &
                  "FROM zkustomer WHERE kodekust LIKE '%" & keyword & "%' OR namakust LIKE '%" & keyword & "%' ORDER BY kodekust"
        End If

        dgitmMUT.Rows.Clear()

        Try
            BukaKoneksi()
            Cmd = New OdbcCommand(sql, Conn)
            Rd = Cmd.ExecuteReader()

            While Rd.Read()
                dgitmMUT.Rows.Add(Rd("kodekust"), Rd("namakust"), Rd("alamat"), Rd("kota"), Rd("ktp"), Rd("npwp"))
            End While

            Rd.Close()
            Conn.Close()
        Catch ex As Exception
            MessageBox.Show("Gagal load data: " & ex.Message)
        End Try
    End Sub

    Private Sub ItDtMutasi_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.FormBorderStyle = FormBorderStyle.FixedSingle
        Me.MaximizeBox = False

        SetupGridKustomer(dgitmMUT)
    End Sub

    Private Sub dgitmMUT_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgitmMUT.CellContentClick
        If e.RowIndex >= 0 Then
            Dim kodek As String = dgitmMUT.Rows(e.RowIndex).Cells("kodekust").Value.ToString()
            Dim namak As String = dgitmMUT.Rows(e.RowIndex).Cells("namakust").Value.ToString()
            Dim alamat As String = dgitmMUT.Rows(e.RowIndex).Cells("alamat").Value.ToString()
            Dim kota As String = dgitmMUT.Rows(e.RowIndex).Cells("kota").Value.ToString()
            Dim ktp As String = dgitmMUT.Rows(e.RowIndex).Cells("ktp").Value.ToString()
            Dim npwp As Double = Val(dgitmMUT.Rows(e.RowIndex).Cells("npwp").Value)


            ' kirim ke ItFPopupPem
            Dim parentForm As FPenjualan = TryCast(Me.Owner, FPenjualan)
            If parentForm IsNot Nothing Then
                parentForm.SetKustomer(kodek, namak, alamat, kota, ktp, npwp)
            End If

            Me.Close()
        End If
    End Sub
End Class