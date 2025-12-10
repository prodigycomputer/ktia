Imports System.Data.Odbc

Public Class FBayarPelunasan

    Public Property NoNota As String
    Public Property Diskon As Decimal
    Public Property Bayar As Decimal
    Public Property Sisa As Decimal
    Public Property DialogResultValue As Boolean = False

    Private Sub OnlyNumber_KeyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs) _
        Handles txtDisk.KeyPress, txtBayar.KeyPress

        AngkaHelper.HanyaAngka(e)
    End Sub

    Private Sub FBayarPelunasan_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        lblNoNota.Text = NoNota
        lblSisa.Text = "Rp " & Sisa.ToString("N2")
    End Sub

    Private Sub btnBATAL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBATAL.Click
        Me.Close()
    End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        Dim d As Decimal = 0
        Dim b As Decimal = 0

        If IsNumeric(txtDisk.Text) Then d = CDec(txtDisk.Text)
        If IsNumeric(txtBayar.Text) Then b = CDec(txtBayar.Text)

        Diskon = d
        Bayar = b
        DialogResultValue = True

        ' UPATE BAYAR (pakai IFNULL biar NULL jadi 0)
        Dim sql As String =
            "UPDATE zbeli SET bayar = IFNULL(bayar,0) + ? WHERE nonota = ?"

        Using cmd As New OdbcCommand(sql, Conn)
            cmd.Parameters.AddWithValue("@bayar", b)
            cmd.Parameters.AddWithValue("@nonota", NoNota)
            cmd.ExecuteNonQuery()
        End Using

        Me.Close()
    End Sub
End Class