<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FLaporanPenjualan
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
        Me.btnPRINT = New System.Windows.Forms.Button()
        Me.dgitmLAPJUAL = New System.Windows.Forms.DataGridView()
        CType(Me.dgitmLAPJUAL, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnPRINT
        '
        Me.btnPRINT.Location = New System.Drawing.Point(7, 6)
        Me.btnPRINT.Name = "btnPRINT"
        Me.btnPRINT.Size = New System.Drawing.Size(50, 30)
        Me.btnPRINT.TabIndex = 10
        Me.btnPRINT.Text = "Print"
        Me.btnPRINT.UseVisualStyleBackColor = True
        '
        'dgitmLAPJUAL
        '
        Me.dgitmLAPJUAL.BackgroundColor = System.Drawing.Color.White
        Me.dgitmLAPJUAL.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgitmLAPJUAL.Location = New System.Drawing.Point(5, 42)
        Me.dgitmLAPJUAL.Name = "dgitmLAPJUAL"
        Me.dgitmLAPJUAL.ReadOnly = True
        Me.dgitmLAPJUAL.Size = New System.Drawing.Size(846, 394)
        Me.dgitmLAPJUAL.TabIndex = 9
        '
        'FLaporanPenjualan
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(862, 445)
        Me.Controls.Add(Me.btnPRINT)
        Me.Controls.Add(Me.dgitmLAPJUAL)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "FLaporanPenjualan"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "FLaporanPenjualan"
        CType(Me.dgitmLAPJUAL, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnPRINT As System.Windows.Forms.Button
    Friend WithEvents dgitmLAPJUAL As System.Windows.Forms.DataGridView
End Class
