<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FPelunasanPem
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
        Me.dgBAYAR = New System.Windows.Forms.DataGridView()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtKDSUP = New System.Windows.Forms.TextBox()
        Me.txtNMPEM = New System.Windows.Forms.TextBox()
        CType(Me.dgBAYAR, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dgBAYAR
        '
        Me.dgBAYAR.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgBAYAR.Location = New System.Drawing.Point(4, 37)
        Me.dgBAYAR.Name = "dgBAYAR"
        Me.dgBAYAR.Size = New System.Drawing.Size(604, 219)
        Me.dgBAYAR.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(1, 10)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(32, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Kode"
        '
        'txtKDSUP
        '
        Me.txtKDSUP.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtKDSUP.Location = New System.Drawing.Point(40, 6)
        Me.txtKDSUP.Name = "txtKDSUP"
        Me.txtKDSUP.Size = New System.Drawing.Size(119, 20)
        Me.txtKDSUP.TabIndex = 2
        '
        'txtNMPEM
        '
        Me.txtNMPEM.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtNMPEM.Location = New System.Drawing.Point(165, 6)
        Me.txtNMPEM.Name = "txtNMPEM"
        Me.txtNMPEM.Size = New System.Drawing.Size(443, 20)
        Me.txtNMPEM.TabIndex = 3
        '
        'FPelunasanPem
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(616, 261)
        Me.Controls.Add(Me.txtNMPEM)
        Me.Controls.Add(Me.txtKDSUP)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.dgBAYAR)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "FPelunasanPem"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "FPelunasanPem"
        CType(Me.dgBAYAR, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents dgBAYAR As System.Windows.Forms.DataGridView
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtKDSUP As System.Windows.Forms.TextBox
    Friend WithEvents txtNMPEM As System.Windows.Forms.TextBox
End Class
