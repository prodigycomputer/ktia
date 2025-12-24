Imports System.Drawing
Imports System.Windows.Forms

Module ModPlaceholder
    Public Sub SetPlaceholder(ByVal txt As TextBox, ByVal placeholder As String)
        txt.Tag = placeholder
        txt.Text = placeholder
        txt.ForeColor = Color.Gray
    End Sub

    Public Sub RemovePlaceholder(ByVal txt As TextBox)
        If txt.Text = txt.Tag.ToString() Then
            txt.Text = ""
            txt.ForeColor = Color.Black
        End If
    End Sub

    Public Function IsPlaceholder(ByVal txt As TextBox) As Boolean
        Return txt.Text = txt.Tag.ToString()
    End Function

    ' === Ambil text asli (tanpa placeholder) ===
    Public Function GetRealText(ByVal txt As TextBox) As String
        If txt.ForeColor = Color.Gray Then
            Return ""
        Else
            Return txt.Text.Trim()
        End If
    End Function
End Module
