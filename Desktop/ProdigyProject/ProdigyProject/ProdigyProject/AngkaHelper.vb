Public Class AngkaHelper
    ' === Format angka menjadi 2 desimal ===
    Public Shared Sub FormatAngkaDuaDesimal(ByVal tb As TextBox)
        If tb.Text = "" Then Exit Sub

        Dim nilai As Decimal
        If Decimal.TryParse(tb.Text, nilai) Then
            tb.Text = Format(nilai, "N2")
            tb.SelectionStart = tb.Text.Length
        Else
            tb.Text = "0,00"
            tb.SelectionStart = tb.Text.Length
        End If
    End Sub

    ' === Format angka menjadi persentase (contoh: 10 → "10,00 %") ===
    Public Shared Sub FormatPersen(ByVal tb As TextBox)
        If tb.Text = "" Then Exit Sub

        Dim nilai As Decimal
        If Decimal.TryParse(tb.Text.Replace("%", "").Trim(), nilai) Then
            tb.Text = Format(nilai, "N2") & " %"
            tb.SelectionStart = tb.Text.Length
        Else
            tb.Text = "0,00 %"
            tb.SelectionStart = tb.Text.Length
        End If
    End Sub

    ' === Format angka menjadi Rupiah (contoh: 1000 → "Rp 1.000,00") ===
    Public Shared Sub FormatRupiah(ByVal tb As TextBox)
        If tb.Text = "" Then Exit Sub

        Dim nilai As Decimal
        If Decimal.TryParse(tb.Text.Replace("Rp", "").Trim(), nilai) Then
            tb.Text = "Rp " & Format(nilai, "N2")
            tb.SelectionStart = tb.Text.Length
        Else
            tb.Text = "Rp 0,00"
            tb.SelectionStart = tb.Text.Length
        End If
    End Sub

    ' === Filter agar hanya bisa input angka, backspace, dan koma ===
    Public Shared Sub HanyaAngka(ByVal e As KeyPressEventArgs)
        If Not (Char.IsDigit(e.KeyChar) OrElse e.KeyChar = ChrW(Keys.Back) OrElse e.KeyChar = ","c OrElse e.KeyChar = "."c) Then
            e.Handled = True
        End If
    End Sub
End Class
