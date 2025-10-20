<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FDtAkses
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
        Me.dgStAKUN = New System.Windows.Forms.DataGridView()
        Me.btnPTUTUP = New System.Windows.Forms.Button()
        Me.btnPSIMPAN = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblKDUSER = New System.Windows.Forms.Label()
        CType(Me.dgStAKUN, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dgStAKUN
        '
        Me.dgStAKUN.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgStAKUN.Location = New System.Drawing.Point(4, 42)
        Me.dgStAKUN.Name = "dgStAKUN"
        Me.dgStAKUN.Size = New System.Drawing.Size(479, 424)
        Me.dgStAKUN.TabIndex = 0
        '
        'btnPTUTUP
        '
        Me.btnPTUTUP.Location = New System.Drawing.Point(428, 6)
        Me.btnPTUTUP.Name = "btnPTUTUP"
        Me.btnPTUTUP.Size = New System.Drawing.Size(55, 30)
        Me.btnPTUTUP.TabIndex = 152
        Me.btnPTUTUP.Text = "Tutup"
        Me.btnPTUTUP.UseVisualStyleBackColor = True
        '
        'btnPSIMPAN
        '
        Me.btnPSIMPAN.Location = New System.Drawing.Point(367, 6)
        Me.btnPSIMPAN.Name = "btnPSIMPAN"
        Me.btnPSIMPAN.Size = New System.Drawing.Size(55, 30)
        Me.btnPSIMPAN.TabIndex = 151
        Me.btnPSIMPAN.Text = "Simpan"
        Me.btnPSIMPAN.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(5, 14)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(74, 15)
        Me.Label2.TabIndex = 154
        Me.Label2.Text = "Kode User : "
        '
        'lblKDUSER
        '
        Me.lblKDUSER.AutoSize = True
        Me.lblKDUSER.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblKDUSER.Location = New System.Drawing.Point(85, 14)
        Me.lblKDUSER.Name = "lblKDUSER"
        Me.lblKDUSER.Size = New System.Drawing.Size(65, 15)
        Me.lblKDUSER.TabIndex = 155
        Me.lblKDUSER.Text = "Kode User"
        '
        'FDtAkses
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(487, 470)
        Me.Controls.Add(Me.lblKDUSER)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.btnPTUTUP)
        Me.Controls.Add(Me.btnPSIMPAN)
        Me.Controls.Add(Me.dgStAKUN)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "FDtAkses"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "FDtAkses"
        CType(Me.dgStAKUN, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents dgStAKUN As System.Windows.Forms.DataGridView
    Friend WithEvents btnPTUTUP As System.Windows.Forms.Button
    Friend WithEvents btnPSIMPAN As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lblKDUSER As System.Windows.Forms.Label
End Class
