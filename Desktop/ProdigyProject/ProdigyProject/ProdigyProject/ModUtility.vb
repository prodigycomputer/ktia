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
            CType(copy, TextBox).CharacterCasing = CType(original, TextBox).CharacterCasing
        ElseIf TypeOf copy Is Label Then
            CType(copy, Label).TextAlign = CType(original, Label).TextAlign
        ElseIf TypeOf copy Is Button Then
            CType(copy, Button).TextAlign = CType(original, Button).TextAlign
        ElseIf TypeOf copy Is DateTimePicker Then
            Dim dtp As DateTimePicker = CType(copy, DateTimePicker)
            dtp.Format = DateTimePickerFormat.Custom
            dtp.CustomFormat = "dd/MM/yyyy"
            dtp.Value = DateTime.Now

        ElseIf TypeOf copy Is ComboBox Then
            Dim cboOrig As ComboBox = CType(original, ComboBox)
            Dim cboCopy As ComboBox = CType(copy, ComboBox)

            ' Salin item dan properti penting ComboBox
            cboCopy.DropDownStyle = cboOrig.DropDownStyle
            cboCopy.Sorted = cboOrig.Sorted
            cboCopy.MaxDropDownItems = cboOrig.MaxDropDownItems
            cboCopy.DropDownWidth = cboOrig.DropDownWidth
            cboCopy.IntegralHeight = cboOrig.IntegralHeight

            ' Salin semua item
            For Each item In cboOrig.Items
                cboCopy.Items.Add(item)
            Next

            ' Pilihan awal (jika ada)
            cboCopy.SelectedIndex = -1
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

    ' Fungsi universal untuk membersihkan semua kontrol dalam dictionary
    Public Sub ClearForm(ByVal dict As Dictionary(Of String, Control))
        Try
            For Each kvp In dict
                Dim ctrl As Control = kvp.Value

                Select Case True
                    Case TypeOf ctrl Is TextBox
                        Dim tb As TextBox = CType(ctrl, TextBox)
                        tb.Clear()

                    Case TypeOf ctrl Is ComboBox
                        Dim cb As ComboBox = CType(ctrl, ComboBox)
                        cb.SelectedIndex = -1
                        cb.Text = ""

                    Case TypeOf ctrl Is DataGridView
                        Dim dgv As DataGridView = CType(ctrl, DataGridView)
                        dgv.Rows.Clear()

                    Case TypeOf ctrl Is DateTimePicker
                        Dim dtp As DateTimePicker = CType(ctrl, DateTimePicker)
                        dtp.Value = Date.Today

                    Case TypeOf ctrl Is CheckBox
                        Dim chk As CheckBox = CType(ctrl, CheckBox)
                        chk.Checked = False

                    Case TypeOf ctrl Is NumericUpDown
                        Dim num As NumericUpDown = CType(ctrl, NumericUpDown)
                        num.Value = 0
                End Select
            Next

        Catch ex As Exception
            MsgBox("Terjadi kesalahan saat membersihkan form: " & ex.Message, MsgBoxStyle.Exclamation)
        End Try
    End Sub

End Module

