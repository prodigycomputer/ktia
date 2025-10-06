<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ItDtPembelian
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
        Me.dgitmPEMBELIAN = New System.Windows.Forms.DataGridView()
        CType(Me.dgitmPEMBELIAN, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dgitmPEMBELIAN
        '
        Me.dgitmPEMBELIAN.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgitmPEMBELIAN.Location = New System.Drawing.Point(8, 8)
        Me.dgitmPEMBELIAN.Name = "dgitmPEMBELIAN"
        Me.dgitmPEMBELIAN.ReadOnly = True
        Me.dgitmPEMBELIAN.Size = New System.Drawing.Size(846, 427)
        Me.dgitmPEMBELIAN.TabIndex = 0
        '
        'ItDtPembelian
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(862, 445)
        Me.Controls.Add(Me.dgitmPEMBELIAN)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "ItDtPembelian"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "DtPembelian"
        CType(Me.dgitmPEMBELIAN, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents dgitmPEMBELIAN As System.Windows.Forms.DataGridView
End Class
