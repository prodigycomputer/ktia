<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FPenyesuaian
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
        Me.tpenySGD = New System.Windows.Forms.TextBox()
        Me.dtpenyTGL = New System.Windows.Forms.DateTimePicker()
        Me.bpenyTabDel = New System.Windows.Forms.Button()
        Me.bpenyTabAdd = New System.Windows.Forms.Button()
        Me.btnPRINT = New System.Windows.Forms.Button()
        Me.btnCARI = New System.Windows.Forms.Button()
        Me.tpenySNONOTA = New System.Windows.Forms.TextBox()
        Me.btnBATAL = New System.Windows.Forms.Button()
        Me.btnHAPUS = New System.Windows.Forms.Button()
        Me.btnUBAH = New System.Windows.Forms.Button()
        Me.btnSIMPAN = New System.Windows.Forms.Button()
        Me.btnTAMBAH = New System.Windows.Forms.Button()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.Nota1 = New System.Windows.Forms.TabPage()
        Me.tpenyTANGGAL = New System.Windows.Forms.DateTimePicker()
        Me.cbpenyGD = New System.Windows.Forms.ComboBox()
        Me.tpenyNONOTA = New System.Windows.Forms.TextBox()
        Me.grPENYESUAIAN = New System.Windows.Forms.DataGridView()
        Me.btnADDITEM = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TabControl1.SuspendLayout()
        Me.Nota1.SuspendLayout()
        CType(Me.grPENYESUAIAN, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'tpenySGD
        '
        Me.tpenySGD.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.tpenySGD.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tpenySGD.Location = New System.Drawing.Point(697, 8)
        Me.tpenySGD.Name = "tpenySGD"
        Me.tpenySGD.Size = New System.Drawing.Size(222, 23)
        Me.tpenySGD.TabIndex = 41
        '
        'dtpenyTGL
        '
        Me.dtpenyTGL.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpenyTGL.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpenyTGL.Location = New System.Drawing.Point(551, 8)
        Me.dtpenyTGL.Name = "dtpenyTGL"
        Me.dtpenyTGL.Size = New System.Drawing.Size(140, 23)
        Me.dtpenyTGL.TabIndex = 40
        '
        'bpenyTabDel
        '
        Me.bpenyTabDel.Location = New System.Drawing.Point(903, 36)
        Me.bpenyTabDel.Name = "bpenyTabDel"
        Me.bpenyTabDel.Size = New System.Drawing.Size(70, 25)
        Me.bpenyTabDel.TabIndex = 39
        Me.bpenyTabDel.Text = "Hapus Tab"
        Me.bpenyTabDel.UseVisualStyleBackColor = True
        '
        'bpenyTabAdd
        '
        Me.bpenyTabAdd.Location = New System.Drawing.Point(816, 36)
        Me.bpenyTabAdd.Name = "bpenyTabAdd"
        Me.bpenyTabAdd.Size = New System.Drawing.Size(80, 25)
        Me.bpenyTabAdd.TabIndex = 38
        Me.bpenyTabAdd.Text = "Tambah Tab"
        Me.bpenyTabAdd.UseVisualStyleBackColor = True
        '
        'btnPRINT
        '
        Me.btnPRINT.Location = New System.Drawing.Point(289, 4)
        Me.btnPRINT.Name = "btnPRINT"
        Me.btnPRINT.Size = New System.Drawing.Size(50, 30)
        Me.btnPRINT.TabIndex = 37
        Me.btnPRINT.Text = "Print"
        Me.btnPRINT.UseVisualStyleBackColor = True
        '
        'btnCARI
        '
        Me.btnCARI.Location = New System.Drawing.Point(930, 4)
        Me.btnCARI.Name = "btnCARI"
        Me.btnCARI.Size = New System.Drawing.Size(47, 30)
        Me.btnCARI.TabIndex = 36
        Me.btnCARI.Text = "Cari"
        Me.btnCARI.UseVisualStyleBackColor = True
        '
        'tpenySNONOTA
        '
        Me.tpenySNONOTA.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.tpenySNONOTA.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tpenySNONOTA.Location = New System.Drawing.Point(345, 8)
        Me.tpenySNONOTA.Name = "tpenySNONOTA"
        Me.tpenySNONOTA.Size = New System.Drawing.Size(200, 23)
        Me.tpenySNONOTA.TabIndex = 35
        '
        'btnBATAL
        '
        Me.btnBATAL.Location = New System.Drawing.Point(228, 4)
        Me.btnBATAL.Name = "btnBATAL"
        Me.btnBATAL.Size = New System.Drawing.Size(55, 30)
        Me.btnBATAL.TabIndex = 34
        Me.btnBATAL.Text = "Batal"
        Me.btnBATAL.UseVisualStyleBackColor = True
        '
        'btnHAPUS
        '
        Me.btnHAPUS.Location = New System.Drawing.Point(172, 4)
        Me.btnHAPUS.Name = "btnHAPUS"
        Me.btnHAPUS.Size = New System.Drawing.Size(55, 30)
        Me.btnHAPUS.TabIndex = 33
        Me.btnHAPUS.Text = "Hapus"
        Me.btnHAPUS.UseVisualStyleBackColor = True
        '
        'btnUBAH
        '
        Me.btnUBAH.Location = New System.Drawing.Point(116, 4)
        Me.btnUBAH.Name = "btnUBAH"
        Me.btnUBAH.Size = New System.Drawing.Size(55, 30)
        Me.btnUBAH.TabIndex = 32
        Me.btnUBAH.Text = "Ubah"
        Me.btnUBAH.UseVisualStyleBackColor = True
        '
        'btnSIMPAN
        '
        Me.btnSIMPAN.Location = New System.Drawing.Point(60, 4)
        Me.btnSIMPAN.Name = "btnSIMPAN"
        Me.btnSIMPAN.Size = New System.Drawing.Size(55, 30)
        Me.btnSIMPAN.TabIndex = 31
        Me.btnSIMPAN.Text = "Simpan"
        Me.btnSIMPAN.UseVisualStyleBackColor = True
        '
        'btnTAMBAH
        '
        Me.btnTAMBAH.Location = New System.Drawing.Point(4, 4)
        Me.btnTAMBAH.Name = "btnTAMBAH"
        Me.btnTAMBAH.Size = New System.Drawing.Size(55, 30)
        Me.btnTAMBAH.TabIndex = 30
        Me.btnTAMBAH.Text = "Tambah"
        Me.btnTAMBAH.UseVisualStyleBackColor = True
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.Nota1)
        Me.TabControl1.Location = New System.Drawing.Point(5, 67)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(978, 388)
        Me.TabControl1.TabIndex = 42
        '
        'Nota1
        '
        Me.Nota1.BackColor = System.Drawing.Color.Transparent
        Me.Nota1.Controls.Add(Me.tpenyTANGGAL)
        Me.Nota1.Controls.Add(Me.cbpenyGD)
        Me.Nota1.Controls.Add(Me.tpenyNONOTA)
        Me.Nota1.Controls.Add(Me.grPENYESUAIAN)
        Me.Nota1.Controls.Add(Me.btnADDITEM)
        Me.Nota1.Controls.Add(Me.Label3)
        Me.Nota1.Controls.Add(Me.Label2)
        Me.Nota1.Controls.Add(Me.Label1)
        Me.Nota1.Location = New System.Drawing.Point(4, 22)
        Me.Nota1.Name = "Nota1"
        Me.Nota1.Padding = New System.Windows.Forms.Padding(3)
        Me.Nota1.Size = New System.Drawing.Size(970, 362)
        Me.Nota1.TabIndex = 0
        Me.Nota1.Text = "Nota 1"
        '
        'tpenyTANGGAL
        '
        Me.tpenyTANGGAL.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.tpenyTANGGAL.Location = New System.Drawing.Point(111, 10)
        Me.tpenyTANGGAL.Name = "tpenyTANGGAL"
        Me.tpenyTANGGAL.Size = New System.Drawing.Size(138, 20)
        Me.tpenyTANGGAL.TabIndex = 34
        '
        'cbpenyGD
        '
        Me.cbpenyGD.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbpenyGD.FormattingEnabled = True
        Me.cbpenyGD.Location = New System.Drawing.Point(111, 56)
        Me.cbpenyGD.Name = "cbpenyGD"
        Me.cbpenyGD.Size = New System.Drawing.Size(220, 21)
        Me.cbpenyGD.TabIndex = 22
        '
        'tpenyNONOTA
        '
        Me.tpenyNONOTA.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.tpenyNONOTA.Location = New System.Drawing.Point(111, 33)
        Me.tpenyNONOTA.Name = "tpenyNONOTA"
        Me.tpenyNONOTA.Size = New System.Drawing.Size(138, 20)
        Me.tpenyNONOTA.TabIndex = 1
        '
        'grPENYESUAIAN
        '
        Me.grPENYESUAIAN.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grPENYESUAIAN.Location = New System.Drawing.Point(3, 104)
        Me.grPENYESUAIAN.Name = "grPENYESUAIAN"
        Me.grPENYESUAIAN.Size = New System.Drawing.Size(961, 250)
        Me.grPENYESUAIAN.TabIndex = 20
        '
        'btnADDITEM
        '
        Me.btnADDITEM.Location = New System.Drawing.Point(899, 78)
        Me.btnADDITEM.Name = "btnADDITEM"
        Me.btnADDITEM.Size = New System.Drawing.Size(65, 20)
        Me.btnADDITEM.TabIndex = 19
        Me.btnADDITEM.Text = "Add Item"
        Me.btnADDITEM.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(9, 57)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(51, 15)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "Gudang"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(9, 34)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(52, 15)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "No Nota"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(9, 10)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(52, 15)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Tanggal"
        '
        'FPenyesuaian
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(989, 459)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.tpenySGD)
        Me.Controls.Add(Me.dtpenyTGL)
        Me.Controls.Add(Me.bpenyTabDel)
        Me.Controls.Add(Me.bpenyTabAdd)
        Me.Controls.Add(Me.btnPRINT)
        Me.Controls.Add(Me.btnCARI)
        Me.Controls.Add(Me.tpenySNONOTA)
        Me.Controls.Add(Me.btnBATAL)
        Me.Controls.Add(Me.btnHAPUS)
        Me.Controls.Add(Me.btnUBAH)
        Me.Controls.Add(Me.btnSIMPAN)
        Me.Controls.Add(Me.btnTAMBAH)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "FPenyesuaian"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "FPenyesuaian"
        Me.TabControl1.ResumeLayout(False)
        Me.Nota1.ResumeLayout(False)
        Me.Nota1.PerformLayout()
        CType(Me.grPENYESUAIAN, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents tpenySGD As System.Windows.Forms.TextBox
    Friend WithEvents dtpenyTGL As System.Windows.Forms.DateTimePicker
    Friend WithEvents bpenyTabDel As System.Windows.Forms.Button
    Friend WithEvents bpenyTabAdd As System.Windows.Forms.Button
    Friend WithEvents btnPRINT As System.Windows.Forms.Button
    Friend WithEvents btnCARI As System.Windows.Forms.Button
    Friend WithEvents tpenySNONOTA As System.Windows.Forms.TextBox
    Friend WithEvents btnBATAL As System.Windows.Forms.Button
    Friend WithEvents btnHAPUS As System.Windows.Forms.Button
    Friend WithEvents btnUBAH As System.Windows.Forms.Button
    Friend WithEvents btnSIMPAN As System.Windows.Forms.Button
    Friend WithEvents btnTAMBAH As System.Windows.Forms.Button
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents Nota1 As System.Windows.Forms.TabPage
    Friend WithEvents tpenyTANGGAL As System.Windows.Forms.DateTimePicker
    Friend WithEvents cbpenyGD As System.Windows.Forms.ComboBox
    Friend WithEvents tpenyNONOTA As System.Windows.Forms.TextBox
    Friend WithEvents grPENYESUAIAN As System.Windows.Forms.DataGridView
    Friend WithEvents btnADDITEM As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
End Class
