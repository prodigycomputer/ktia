<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ItDtPenjualan
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
        Me.dgitmPENJUALAN = New System.Windows.Forms.DataGridView()
        CType(Me.dgitmPENJUALAN, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dgitmPENJUALAN
        '
        Me.dgitmPENJUALAN.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgitmPENJUALAN.Location = New System.Drawing.Point(8, 8)
        Me.dgitmPENJUALAN.Name = "dgitmPENJUALAN"
        Me.dgitmPENJUALAN.ReadOnly = True
        Me.dgitmPENJUALAN.Size = New System.Drawing.Size(846, 427)
        Me.dgitmPENJUALAN.TabIndex = 1
        '
        'ItDtPenjualan
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(862, 445)
        Me.Controls.Add(Me.dgitmPENJUALAN)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "ItDtPenjualan"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "DtPenjualan"
        CType(Me.dgitmPENJUALAN, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents dgitmPENJUALAN As System.Windows.Forms.DataGridView
End Class
