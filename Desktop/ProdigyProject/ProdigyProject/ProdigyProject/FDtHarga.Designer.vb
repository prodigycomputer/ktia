<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FDtHarga
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
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btnPTUTUP = New System.Windows.Forms.Button()
        Me.btnPSIMPAN = New System.Windows.Forms.Button()
        Me.lblKDBARANG = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(5, 12)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(114, 15)
        Me.Label2.TabIndex = 158
        Me.Label2.Text = "Atur Harga Barang :"
        '
        'btnPTUTUP
        '
        Me.btnPTUTUP.Location = New System.Drawing.Point(434, 4)
        Me.btnPTUTUP.Name = "btnPTUTUP"
        Me.btnPTUTUP.Size = New System.Drawing.Size(55, 30)
        Me.btnPTUTUP.TabIndex = 157
        Me.btnPTUTUP.Text = "Tutup"
        Me.btnPTUTUP.UseVisualStyleBackColor = True
        '
        'btnPSIMPAN
        '
        Me.btnPSIMPAN.Location = New System.Drawing.Point(373, 4)
        Me.btnPSIMPAN.Name = "btnPSIMPAN"
        Me.btnPSIMPAN.Size = New System.Drawing.Size(55, 30)
        Me.btnPSIMPAN.TabIndex = 156
        Me.btnPSIMPAN.Text = "Simpan"
        Me.btnPSIMPAN.UseVisualStyleBackColor = True
        '
        'lblKDBARANG
        '
        Me.lblKDBARANG.AutoSize = True
        Me.lblKDBARANG.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblKDBARANG.Location = New System.Drawing.Point(125, 12)
        Me.lblKDBARANG.Name = "lblKDBARANG"
        Me.lblKDBARANG.Size = New System.Drawing.Size(79, 15)
        Me.lblKDBARANG.TabIndex = 159
        Me.lblKDBARANG.Text = "Kode Barang"
        '
        'FDtHarga
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(496, 239)
        Me.Controls.Add(Me.lblKDBARANG)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.btnPTUTUP)
        Me.Controls.Add(Me.btnPSIMPAN)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "FDtHarga"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "FDtHarga"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btnPTUTUP As System.Windows.Forms.Button
    Friend WithEvents btnPSIMPAN As System.Windows.Forms.Button
    Friend WithEvents lblKDBARANG As System.Windows.Forms.Label
End Class
