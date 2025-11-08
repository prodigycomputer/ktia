<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FLaporanPembelian
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtLKDSUP1 = New System.Windows.Forms.TextBox()
        Me.txtLKDSUP2 = New System.Windows.Forms.TextBox()
        Me.dtTGL1 = New System.Windows.Forms.DateTimePicker()
        Me.dtTGL2 = New System.Windows.Forms.DateTimePicker()
        Me.cbRINCI = New System.Windows.Forms.CheckBox()
        Me.cbREKAP = New System.Windows.Forms.CheckBox()
        Me.btnTUTUP = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'btnPRINT
        '
        Me.btnPRINT.Location = New System.Drawing.Point(267, 9)
        Me.btnPRINT.Name = "btnPRINT"
        Me.btnPRINT.Size = New System.Drawing.Size(50, 22)
        Me.btnPRINT.TabIndex = 8
        Me.btnPRINT.Text = "Print"
        Me.btnPRINT.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(15, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(73, 13)
        Me.Label1.TabIndex = 9
        Me.Label1.Text = "Kode Supplier"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(65, 32)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(23, 13)
        Me.Label2.TabIndex = 10
        Me.Label2.Text = "s/d"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(42, 55)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(46, 13)
        Me.Label3.TabIndex = 11
        Me.Label3.Text = "Tanggal"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(23, 78)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(65, 13)
        Me.Label4.TabIndex = 12
        Me.Label4.Text = "s/d Tanggal"
        '
        'txtLKDSUP1
        '
        Me.txtLKDSUP1.Location = New System.Drawing.Point(95, 5)
        Me.txtLKDSUP1.Name = "txtLKDSUP1"
        Me.txtLKDSUP1.Size = New System.Drawing.Size(143, 20)
        Me.txtLKDSUP1.TabIndex = 13
        '
        'txtLKDSUP2
        '
        Me.txtLKDSUP2.Location = New System.Drawing.Point(95, 28)
        Me.txtLKDSUP2.Name = "txtLKDSUP2"
        Me.txtLKDSUP2.Size = New System.Drawing.Size(143, 20)
        Me.txtLKDSUP2.TabIndex = 14
        '
        'dtTGL1
        '
        Me.dtTGL1.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtTGL1.Location = New System.Drawing.Point(95, 51)
        Me.dtTGL1.Name = "dtTGL1"
        Me.dtTGL1.Size = New System.Drawing.Size(143, 20)
        Me.dtTGL1.TabIndex = 15
        '
        'dtTGL2
        '
        Me.dtTGL2.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtTGL2.Location = New System.Drawing.Point(95, 74)
        Me.dtTGL2.Name = "dtTGL2"
        Me.dtTGL2.Size = New System.Drawing.Size(143, 20)
        Me.dtTGL2.TabIndex = 16
        '
        'cbRINCI
        '
        Me.cbRINCI.AutoSize = True
        Me.cbRINCI.Location = New System.Drawing.Point(159, 100)
        Me.cbRINCI.Name = "cbRINCI"
        Me.cbRINCI.Size = New System.Drawing.Size(50, 17)
        Me.cbRINCI.TabIndex = 17
        Me.cbRINCI.Text = "Rinci"
        Me.cbRINCI.UseVisualStyleBackColor = True
        '
        'cbREKAP
        '
        Me.cbREKAP.AutoSize = True
        Me.cbREKAP.Location = New System.Drawing.Point(95, 100)
        Me.cbREKAP.Name = "cbREKAP"
        Me.cbREKAP.Size = New System.Drawing.Size(58, 17)
        Me.cbREKAP.TabIndex = 18
        Me.cbREKAP.Text = "Rekap"
        Me.cbREKAP.UseVisualStyleBackColor = True
        '
        'btnTUTUP
        '
        Me.btnTUTUP.Location = New System.Drawing.Point(267, 37)
        Me.btnTUTUP.Name = "btnTUTUP"
        Me.btnTUTUP.Size = New System.Drawing.Size(50, 22)
        Me.btnTUTUP.TabIndex = 19
        Me.btnTUTUP.Text = "Close"
        Me.btnTUTUP.UseVisualStyleBackColor = True
        '
        'FLaporanPembelian
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(342, 128)
        Me.Controls.Add(Me.btnTUTUP)
        Me.Controls.Add(Me.cbREKAP)
        Me.Controls.Add(Me.cbRINCI)
        Me.Controls.Add(Me.dtTGL2)
        Me.Controls.Add(Me.dtTGL1)
        Me.Controls.Add(Me.txtLKDSUP2)
        Me.Controls.Add(Me.txtLKDSUP1)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnPRINT)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "FLaporanPembelian"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "FLaporanPembelian"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnPRINT As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtLKDSUP1 As System.Windows.Forms.TextBox
    Friend WithEvents txtLKDSUP2 As System.Windows.Forms.TextBox
    Friend WithEvents dtTGL1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtTGL2 As System.Windows.Forms.DateTimePicker
    Friend WithEvents cbREKAP As System.Windows.Forms.CheckBox
    Friend WithEvents btnTUTUP As System.Windows.Forms.Button
    Friend WithEvents cbRINCI As System.Windows.Forms.CheckBox
End Class
