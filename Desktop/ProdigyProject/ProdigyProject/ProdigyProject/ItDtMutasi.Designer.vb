<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ItDtMutasi
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.dgitmMUTASI = New System.Windows.Forms.DataGridView()
        CType(Me.dgitmMUTASI, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dgitmMUTASI
        '
        Me.dgitmMUTASI.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgitmMUTASI.Location = New System.Drawing.Point(8, 8)
        Me.dgitmMUTASI.Name = "dgitmMUTASI"
        Me.dgitmMUTASI.ReadOnly = True
        Me.dgitmMUTASI.Size = New System.Drawing.Size(846, 427)
        Me.dgitmMUTASI.TabIndex = 1
        '
        'ItDtMutasi
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(862, 445)
        Me.Controls.Add(Me.dgitmMUTASI)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "ItDtMutasi"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ItDtMutasi"
        CType(Me.dgitmMUTASI, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents dgitmMUTASI As System.Windows.Forms.DataGridView
End Class
