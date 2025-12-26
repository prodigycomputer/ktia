<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FGudang
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
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtNMGUDANG = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtKDGUDANG = New System.Windows.Forms.TextBox()
        Me.btnCARI = New System.Windows.Forms.Button()
        Me.tSNMGUDANG = New System.Windows.Forms.TextBox()
        Me.tSKDGUDANG = New System.Windows.Forms.TextBox()
        Me.btnBATAL = New System.Windows.Forms.Button()
        Me.btnHAPUS = New System.Windows.Forms.Button()
        Me.btnUBAH = New System.Windows.Forms.Button()
        Me.btnSIMPAN = New System.Windows.Forms.Button()
        Me.btnTAMBAH = New System.Windows.Forms.Button()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.txtNMGUDANG)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.txtKDGUDANG)
        Me.Panel1.Location = New System.Drawing.Point(9, 83)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(488, 61)
        Me.Panel1.TabIndex = 77
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(8, 31)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(88, 15)
        Me.Label3.TabIndex = 53
        Me.Label3.Text = "Nama Gudang"
        '
        'txtNMGUDANG
        '
        Me.txtNMGUDANG.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtNMGUDANG.Location = New System.Drawing.Point(136, 29)
        Me.txtNMGUDANG.Name = "txtNMGUDANG"
        Me.txtNMGUDANG.Size = New System.Drawing.Size(340, 20)
        Me.txtNMGUDANG.TabIndex = 50
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(8, 8)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(83, 15)
        Me.Label2.TabIndex = 49
        Me.Label2.Text = "Kode Gudang"
        '
        'txtKDGUDANG
        '
        Me.txtKDGUDANG.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtKDGUDANG.Location = New System.Drawing.Point(136, 6)
        Me.txtKDGUDANG.Name = "txtKDGUDANG"
        Me.txtKDGUDANG.Size = New System.Drawing.Size(185, 20)
        Me.txtKDGUDANG.TabIndex = 48
        '
        'btnCARI
        '
        Me.btnCARI.Location = New System.Drawing.Point(450, 6)
        Me.btnCARI.Name = "btnCARI"
        Me.btnCARI.Size = New System.Drawing.Size(47, 30)
        Me.btnCARI.TabIndex = 2
        Me.btnCARI.Text = "Cari"
        Me.btnCARI.UseVisualStyleBackColor = True
        '
        'tSNMGUDANG
        '
        Me.tSNMGUDANG.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.tSNMGUDANG.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tSNMGUDANG.Location = New System.Drawing.Point(146, 10)
        Me.tSNMGUDANG.Name = "tSNMGUDANG"
        Me.tSNMGUDANG.Size = New System.Drawing.Size(298, 23)
        Me.tSNMGUDANG.TabIndex = 1
        '
        'tSKDGUDANG
        '
        Me.tSKDGUDANG.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.tSKDGUDANG.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tSKDGUDANG.Location = New System.Drawing.Point(9, 10)
        Me.tSKDGUDANG.Name = "tSKDGUDANG"
        Me.tSKDGUDANG.Size = New System.Drawing.Size(131, 23)
        Me.tSKDGUDANG.TabIndex = 0
        '
        'btnBATAL
        '
        Me.btnBATAL.Location = New System.Drawing.Point(231, 42)
        Me.btnBATAL.Name = "btnBATAL"
        Me.btnBATAL.Size = New System.Drawing.Size(55, 30)
        Me.btnBATAL.TabIndex = 7
        Me.btnBATAL.Text = "Batal"
        Me.btnBATAL.UseVisualStyleBackColor = True
        '
        'btnHAPUS
        '
        Me.btnHAPUS.Location = New System.Drawing.Point(175, 42)
        Me.btnHAPUS.Name = "btnHAPUS"
        Me.btnHAPUS.Size = New System.Drawing.Size(55, 30)
        Me.btnHAPUS.TabIndex = 6
        Me.btnHAPUS.Text = "Hapus"
        Me.btnHAPUS.UseVisualStyleBackColor = True
        '
        'btnUBAH
        '
        Me.btnUBAH.Location = New System.Drawing.Point(119, 42)
        Me.btnUBAH.Name = "btnUBAH"
        Me.btnUBAH.Size = New System.Drawing.Size(55, 30)
        Me.btnUBAH.TabIndex = 5
        Me.btnUBAH.Text = "Ubah"
        Me.btnUBAH.UseVisualStyleBackColor = True
        '
        'btnSIMPAN
        '
        Me.btnSIMPAN.Location = New System.Drawing.Point(63, 42)
        Me.btnSIMPAN.Name = "btnSIMPAN"
        Me.btnSIMPAN.Size = New System.Drawing.Size(55, 30)
        Me.btnSIMPAN.TabIndex = 4
        Me.btnSIMPAN.Text = "Simpan"
        Me.btnSIMPAN.UseVisualStyleBackColor = True
        '
        'btnTAMBAH
        '
        Me.btnTAMBAH.Location = New System.Drawing.Point(7, 42)
        Me.btnTAMBAH.Name = "btnTAMBAH"
        Me.btnTAMBAH.Size = New System.Drawing.Size(55, 30)
        Me.btnTAMBAH.TabIndex = 3
        Me.btnTAMBAH.Text = "Tambah"
        Me.btnTAMBAH.UseVisualStyleBackColor = True
        '
        'FGudang
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(509, 155)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.btnCARI)
        Me.Controls.Add(Me.tSNMGUDANG)
        Me.Controls.Add(Me.tSKDGUDANG)
        Me.Controls.Add(Me.btnBATAL)
        Me.Controls.Add(Me.btnHAPUS)
        Me.Controls.Add(Me.btnUBAH)
        Me.Controls.Add(Me.btnSIMPAN)
        Me.Controls.Add(Me.btnTAMBAH)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "FGudang"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "FGudang"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtNMGUDANG As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtKDGUDANG As System.Windows.Forms.TextBox
    Friend WithEvents btnCARI As System.Windows.Forms.Button
    Friend WithEvents tSNMGUDANG As System.Windows.Forms.TextBox
    Friend WithEvents tSKDGUDANG As System.Windows.Forms.TextBox
    Friend WithEvents btnBATAL As System.Windows.Forms.Button
    Friend WithEvents btnHAPUS As System.Windows.Forms.Button
    Friend WithEvents btnUBAH As System.Windows.Forms.Button
    Friend WithEvents btnSIMPAN As System.Windows.Forms.Button
    Friend WithEvents btnTAMBAH As System.Windows.Forms.Button
End Class
