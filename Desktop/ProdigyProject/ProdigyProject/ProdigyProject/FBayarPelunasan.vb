Imports System.Data.Odbc

Public Class FBayarPelunasan

    Public Property NoNota As String
    Public Property Diskon As Decimal
    Public Property Bayar As Decimal
    Public Property Sisa As Decimal
    Public Property Nilai As Decimal
    Public Property DialogResultValue As Boolean = False

    Private Sub OnlyNumber_KeyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs) _
        Handles txtDisk.KeyPress, txtBayar.KeyPress

        AngkaHelper.HanyaAngka(e)
    End Sub

    Private Sub HitungBayarDenganDiskonPersen()
        Dim persen As Decimal = 0

        If IsNumeric(txtDisk.Text) Then
            persen = CDec(txtDisk.Text)
        End If

        ' Hitung diskon rupiah
        Dim diskonRupiah As Decimal = Nilai * (persen / 100)

        ' Hitung bayar setelah diskon
        Dim bayarSetelahDiskon As Decimal = Nilai - diskonRupiah
        If bayarSetelahDiskon < 0 Then bayarSetelahDiskon = 0

        ' Tampilkan (boleh format tanpa desimal atau 2 desimal sesuai kebutuhan)
        txtBayar.Text = bayarSetelahDiskon.ToString("N0")
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

    Private Sub txtDisk_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDisk.TextChanged
        HitungBayarDenganDiskonPersen()
    End Sub
End Class