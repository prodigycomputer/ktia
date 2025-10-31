Imports System.Data.Odbc

Public Class ItSales

    Public Sub LoadDataSls(ByVal keyword As String)

        Dim sql As String
        If keyword = "*" Then
            sql = "SELECT kodesls, namasls, alamat, kota, ktp, npwp FROM zsales ORDER BY kodesls"
        Else
            sql = "SELECT kodesls, namasls, alamat, kota, ktp, npwp " &
                  "FROM zsales WHERE kodesls LIKE '%" & keyword & "%' OR namasls LIKE '%" & keyword & "%' ORDER BY kodesls"
        End If

        dgitmSLS.Rows.Clear()

        Try
            BukaKoneksi()
            Cmd = New OdbcCommand(sql, Conn)
            Rd = Cmd.ExecuteReader()

            While Rd.Read()
                dgitmsls.Rows.Add(Rd("kodesls"), Rd("namasls"), Rd("alamat"), Rd("kota"), Rd("ktp"), Rd("npwp"))
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

        SetupGridSales(dgitmSLS)
    End Sub

    Private Sub dgitmSLS_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgitmSLS.CellContentClick
        If e.RowIndex >= 0 Then
            Dim kodes As String = dgitmSLS.Rows(e.RowIndex).Cells("kodesls").Value.ToString()
            Dim namas As String = dgitmSLS.Rows(e.RowIndex).Cells("namasls").Value.ToString()
            Dim alamat As String = dgitmSLS.Rows(e.RowIndex).Cells("alamat").Value.ToString()
            Dim kota As String = dgitmSLS.Rows(e.RowIndex).Cells("kota").Value.ToString()
            Dim ktp As String = dgitmSLS.Rows(e.RowIndex).Cells("ktp").Value.ToString()
            Dim npwp As Double = Val(dgitmSLS.Rows(e.RowIndex).Cells("npwp").Value)


            ' kirim ke ItFPopupPem
            Dim parentForm As FPenjualan = TryCast(Me.Owner, FPenjualan)
            If parentForm IsNot Nothing Then
                parentForm.SetSales(kodes, namas, alamat, kota, ktp, npwp)
            End If

            Me.Close()
        End If
    End Sub
End Class