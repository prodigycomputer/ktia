Imports System.Data.Odbc

Public Class ItDtSupplier

    Public Sub LoadDataSup(ByVal keyword As String)

        Dim sql As String
        If keyword = "*" Then
            sql = "SELECT kodesup, namasup, alamat, kota, ktp, npwp FROM zsupplier ORDER BY kodesup"
        Else
            sql = "SELECT kodesup, namasup, alamat, kota, ktp, npwp " &
                  "FROM zsupplier WHERE kodesup LIKE '%" & keyword & "%' OR namasup LIKE '%" & keyword & "%' ORDER BY kodesup"
        End If

        dgitmSUP.Rows.Clear()

        Try
            BukaKoneksi()
            Cmd = New OdbcCommand(sql, Conn)
            Rd = Cmd.ExecuteReader()

            While Rd.Read()
                dgitmSUP.Rows.Add(Rd("kodesup"), Rd("namasup"), Rd("alamat"), Rd("kota"), Rd("ktp"), Rd("npwp"))
            End While

            Rd.Close()
            Conn.Close()
        Catch ex As Exception
            MessageBox.Show("Gagal load data: " & ex.Message)
        End Try
    End Sub

    Private Sub ItDtSupplier_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.FormBorderStyle = FormBorderStyle.FixedSingle
        Me.MaximizeBox = False

        SetupGridSupplier(dgitmSUP)
    End Sub

    Private Sub dgitmSUP_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgitmSUP.CellContentClick
        If e.RowIndex >= 0 Then
            Dim kode As String = dgitmSUP.Rows(e.RowIndex).Cells("kodesup").Value.ToString()
            Dim nama As String = dgitmSUP.Rows(e.RowIndex).Cells("namasup").Value.ToString()
            Dim alamat As String = dgitmSUP.Rows(e.RowIndex).Cells("alamat").Value.ToString()
            Dim kota As String = dgitmSUP.Rows(e.RowIndex).Cells("kota").Value.ToString()
            Dim ktp As String = dgitmSUP.Rows(e.RowIndex).Cells("ktp").Value.ToString()
            Dim npwp As Double = Val(dgitmSUP.Rows(e.RowIndex).Cells("npwp").Value)

            ' kirim ke ItFPopupPem
            Dim parentForm As FPembelian = TryCast(Me.Owner, FPembelian)
            If parentForm IsNot Nothing Then
                parentForm.SetSupplier(kode, nama, alamat, kota, ktp, npwp)
            End If

            Me.Close()
        End If
    End Sub
End Class