<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FArea
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
        Me.txtNMAREA = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtKDAREA = New System.Windows.Forms.TextBox()
        Me.btnCARI = New System.Windows.Forms.Button()
        Me.tSNMAREA = New System.Windows.Forms.TextBox()
        Me.tSKDAREA = New System.Windows.Forms.TextBox()
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
        Me.Panel1.Controls.Add(Me.txtNMAREA)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.txtKDAREA)
        Me.Panel1.Location = New System.Drawing.Point(9, 83)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(491, 61)
        Me.Panel1.TabIndex = 59
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(8, 31)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(69, 15)
        Me.Label3.TabIndex = 53
        Me.Label3.Text = "Nama Area"
        '
        'txtNMAREA
        '
        Me.txtNMAREA.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtNMAREA.Location = New System.Drawing.Point(136, 29)
        Me.txtNMAREA.Name = "txtNMAREA"
        Me.txtNMAREA.Size = New System.Drawing.Size(347, 20)
        Me.txtNMAREA.TabIndex = 50
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(8, 8)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(64, 15)
        Me.Label2.TabIndex = 49
        Me.Label2.Text = "Kode Area"
        '
        'txtKDAREA
        '
        Me.txtKDAREA.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtKDAREA.Location = New System.Drawing.Point(136, 6)
        Me.txtKDAREA.Name = "txtKDAREA"
        Me.txtKDAREA.Size = New System.Drawing.Size(185, 20)
        Me.txtKDAREA.TabIndex = 48
        '
        'btnCARI
        '
        Me.btnCARI.Location = New System.Drawing.Point(453, 6)
        Me.btnCARI.Name = "btnCARI"
        Me.btnCARI.Size = New System.Drawing.Size(47, 30)
        Me.btnCARI.TabIndex = 2
        Me.btnCARI.Text = "Cari"
        Me.btnCARI.UseVisualStyleBackColor = True
        '
        'tSNMAREA
        '
        Me.tSNMAREA.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.tSNMAREA.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tSNMAREA.Location = New System.Drawing.Point(146, 10)
        Me.tSNMAREA.Name = "tSNMAREA"
        Me.tSNMAREA.Size = New System.Drawing.Size(301, 23)
        Me.tSNMAREA.TabIndex = 1
        '
        'tSKDAREA
        '
        Me.tSKDAREA.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.tSKDAREA.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tSKDAREA.Location = New System.Drawing.Point(9, 10)
        Me.tSKDAREA.Name = "tSKDAREA"
        Me.tSKDAREA.Size = New System.Drawing.Size(131, 23)
        Me.tSKDAREA.TabIndex = 0
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
        'FArea
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(509, 155)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.btnCARI)
        Me.Controls.Add(Me.tSNMAREA)
        Me.Controls.Add(Me.tSKDAREA)
        Me.Controls.Add(Me.btnBATAL)
        Me.Controls.Add(Me.btnHAPUS)
        Me.Controls.Add(Me.btnUBAH)
        Me.Controls.Add(Me.btnSIMPAN)
        Me.Controls.Add(Me.btnTAMBAH)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "FArea"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "FArea"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtNMAREA As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtKDAREA As System.Windows.Forms.TextBox
    Friend WithEvents btnCARI As System.Windows.Forms.Button
    Friend WithEvents tSNMAREA As System.Windows.Forms.TextBox
    Friend WithEvents tSKDAREA As System.Windows.Forms.TextBox
    Friend WithEvents btnBATAL As System.Windows.Forms.Button
    Friend WithEvents btnHAPUS As System.Windows.Forms.Button
    Friend WithEvents btnUBAH As System.Windows.Forms.Button
    Friend WithEvents btnSIMPAN As System.Windows.Forms.Button
    Friend WithEvents btnTAMBAH As System.Windows.Forms.Button
End Class
