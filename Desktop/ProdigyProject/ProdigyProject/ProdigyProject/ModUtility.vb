Module ModUtility

    ' --- Clone Control ---
    Public Function CloneControl(ByVal original As Control) As Control
        Dim copy As Control = CType(Activator.CreateInstance(original.GetType()), Control)

        ' Properti umum
        copy.Text = original.Text
        copy.Size = original.Size
        copy.Location = original.Location
        copy.Anchor = original.Anchor
        copy.Dock = original.Dock
        copy.Font = original.Font
        copy.BackColor = original.BackColor
        copy.ForeColor = original.ForeColor
        copy.AutoSize = original.AutoSize
        copy.Margin = original.Margin
        copy.Padding = original.Padding
        copy.Enabled = original.Enabled
        copy.Visible = original.Visible
        copy.TabIndex = original.TabIndex
        copy.TabStop = original.TabStop
        copy.Name = original.Name

        ' Reset sesuai tipe kontrol
        If TypeOf copy Is TextBox Then
            CType(copy, TextBox).Clear()
            CType(copy, TextBox).TextAlign = CType(original, TextBox).TextAlign
        ElseIf TypeOf copy Is Label Then
            CType(copy, Label).TextAlign = CType(original, Label).TextAlign
        ElseIf TypeOf copy Is Button Then
            CType(copy, Button).TextAlign = CType(original, Button).TextAlign
        ElseIf TypeOf copy Is DateTimePicker Then
            Dim dtp As DateTimePicker = CType(copy, DateTimePicker)
            dtp.Format = DateTimePickerFormat.Custom
            dtp.CustomFormat = "dd/MM/yyyy"
            dtp.Value = DateTime.Now
        ElseIf TypeOf copy Is DataGridView Then
            SetupGridPenjualan(CType(copy, DataGridView))
        End If

        ' Clone anak-anak kontrol
        For Each child As Control In original.Controls
            copy.Controls.Add(CloneControl(child))
        Next

        Return copy
    End Function

    ' --- Get nomor nota ---
    Public Function GetNextNotaNumber(ByVal TabControl1 As TabControl) As Integer
        Dim usedNumbers As New List(Of Integer)
        For Each tp As TabPage In TabControl1.TabPages
            If tp.Text.StartsWith("Nota ") Then
                Dim numPart As String = tp.Text.Substring(5)
                Dim num As Integer
                If Integer.TryParse(numPart, num) Then
                    usedNumbers.Add(num)
                End If
            End If
        Next

        Dim candidate As Integer = 1
        Do While usedNumbers.Contains(candidate)
            candidate += 1
        Loop
        Return candidate
    End Function

End Module

