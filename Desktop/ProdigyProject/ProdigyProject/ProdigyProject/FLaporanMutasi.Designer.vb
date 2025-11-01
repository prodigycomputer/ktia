<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FLaporanMutasi
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
        Me.dgitmLAPMUT = New System.Windows.Forms.DataGridView()
        CType(Me.dgitmLAPMUT, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnPRINT
        '
        Me.btnPRINT.Location = New System.Drawing.Point(7, 6)
        Me.btnPRINT.Name = "btnPRINT"
        Me.btnPRINT.Size = New System.Drawing.Size(50, 30)
        Me.btnPRINT.TabIndex = 12
        Me.btnPRINT.Text = "Print"
        Me.btnPRINT.UseVisualStyleBackColor = True
        '
        'dgitmLAPMUT
        '
        Me.dgitmLAPMUT.BackgroundColor = System.Drawing.Color.White
        Me.dgitmLAPMUT.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgitmLAPMUT.Location = New System.Drawing.Point(5, 42)
        Me.dgitmLAPMUT.Name = "dgitmLAPMUT"
        Me.dgitmLAPMUT.ReadOnly = True
        Me.dgitmLAPMUT.Size = New System.Drawing.Size(846, 394)
        Me.dgitmLAPMUT.TabIndex = 11
        '
        'FLaporanMutasi
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(862, 445)
        Me.Controls.Add(Me.btnPRINT)
        Me.Controls.Add(Me.dgitmLAPMUT)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "FLaporanMutasi"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "FLaporanMutasi"
        CType(Me.dgitmLAPMUT, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnPRINT As System.Windows.Forms.Button
    Friend WithEvents dgitmLAPMUT As System.Windows.Forms.DataGridView
End Class
