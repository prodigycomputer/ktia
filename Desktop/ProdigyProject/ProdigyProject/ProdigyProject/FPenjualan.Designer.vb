<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FPenjualan
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
        Me.tpjSLUNAS = New System.Windows.Forms.ComboBox()
        Me.tpjSNMKUST = New System.Windows.Forms.TextBox()
        Me.dtpjTGL2 = New System.Windows.Forms.DateTimePicker()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.dtpjTGL1 = New System.Windows.Forms.DateTimePicker()
        Me.bpjTabDel = New System.Windows.Forms.Button()
        Me.bpjTabAdd = New System.Windows.Forms.Button()
        Me.btnPRINT = New System.Windows.Forms.Button()
        Me.btnCARI = New System.Windows.Forms.Button()
        Me.tpjSNONOTA = New System.Windows.Forms.TextBox()
        Me.btnBATAL = New System.Windows.Forms.Button()
        Me.btnHAPUS = New System.Windows.Forms.Button()
        Me.btnUBAH = New System.Windows.Forms.Button()
        Me.btnSIMPAN = New System.Windows.Forms.Button()
        Me.btnTAMBAH = New System.Windows.Forms.Button()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.Nota1 = New System.Windows.Forms.TabPage()
        Me.tpjKDHARGA = New System.Windows.Forms.TextBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.tpjKDSALES = New System.Windows.Forms.TextBox()
        Me.tpjNMSALES = New System.Windows.Forms.TextBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.tpjTOTAL = New System.Windows.Forms.TextBox()
        Me.tpjSUBTOTAL = New System.Windows.Forms.TextBox()
        Me.tpjLAIN = New System.Windows.Forms.TextBox()
        Me.tpjNPPN = New System.Windows.Forms.TextBox()
        Me.tpjAPPN = New System.Windows.Forms.TextBox()
        Me.tpjNDISK3 = New System.Windows.Forms.TextBox()
        Me.tpjADISK3 = New System.Windows.Forms.TextBox()
        Me.tpjNDISK2 = New System.Windows.Forms.TextBox()
        Me.tpjADISK2 = New System.Windows.Forms.TextBox()
        Me.tpjNDISK1 = New System.Windows.Forms.TextBox()
        Me.tpjADISK1 = New System.Windows.Forms.TextBox()
        Me.tpjALAMAT = New System.Windows.Forms.TextBox()
        Me.tpjKDKUST = New System.Windows.Forms.TextBox()
        Me.tpjKET = New System.Windows.Forms.TextBox()
        Me.tpjNMKUST = New System.Windows.Forms.TextBox()
        Me.tpjNONOTA = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.grPENJUALAN = New System.Windows.Forms.DataGridView()
        Me.btnADDITEM = New System.Windows.Forms.Button()
        Me.tpjTEMPO = New System.Windows.Forms.DateTimePicker()
        Me.tpjTANGGAL = New System.Windows.Forms.DateTimePicker()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.tpjSNMSLS = New System.Windows.Forms.TextBox()
        Me.TabControl1.SuspendLayout()
        Me.Nota1.SuspendLayout()
        CType(Me.grPENJUALAN, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'tpjSLUNAS
        '
        Me.tpjSLUNAS.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.tpjSLUNAS.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tpjSLUNAS.FormattingEnabled = True
        Me.tpjSLUNAS.Location = New System.Drawing.Point(912, 7)
        Me.tpjSLUNAS.Name = "tpjSLUNAS"
        Me.tpjSLUNAS.Size = New System.Drawing.Size(107, 24)
        Me.tpjSLUNAS.TabIndex = 30
        '
        'tpjSNMKUST
        '
        Me.tpjSNMKUST.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.tpjSNMKUST.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tpjSNMKUST.Location = New System.Drawing.Point(679, 8)
        Me.tpjSNMKUST.Name = "tpjSNMKUST"
        Me.tpjSNMKUST.Size = New System.Drawing.Size(113, 23)
        Me.tpjSNMKUST.TabIndex = 29
        '
        'dtpjTGL2
        '
        Me.dtpjTGL2.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpjTGL2.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpjTGL2.Location = New System.Drawing.Point(576, 8)
        Me.dtpjTGL2.Name = "dtpjTGL2"
        Me.dtpjTGL2.Size = New System.Drawing.Size(100, 23)
        Me.dtpjTGL2.TabIndex = 28
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(559, 13)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(15, 13)
        Me.Label15.TabIndex = 27
        Me.Label15.Text = "%"
        '
        'dtpjTGL1
        '
        Me.dtpjTGL1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpjTGL1.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpjTGL1.Location = New System.Drawing.Point(458, 8)
        Me.dtpjTGL1.Name = "dtpjTGL1"
        Me.dtpjTGL1.Size = New System.Drawing.Size(100, 23)
        Me.dtpjTGL1.TabIndex = 26
        '
        'bpjTabDel
        '
        Me.bpjTabDel.Location = New System.Drawing.Point(984, 36)
        Me.bpjTabDel.Name = "bpjTabDel"
        Me.bpjTabDel.Size = New System.Drawing.Size(70, 25)
        Me.bpjTabDel.TabIndex = 25
        Me.bpjTabDel.Text = "Hapus Tab"
        Me.bpjTabDel.UseVisualStyleBackColor = True
        '
        'bpjTabAdd
        '
        Me.bpjTabAdd.Location = New System.Drawing.Point(897, 36)
        Me.bpjTabAdd.Name = "bpjTabAdd"
        Me.bpjTabAdd.Size = New System.Drawing.Size(80, 25)
        Me.bpjTabAdd.TabIndex = 24
        Me.bpjTabAdd.Text = "Tambah Tab"
        Me.bpjTabAdd.UseVisualStyleBackColor = True
        '
        'btnPRINT
        '
        Me.btnPRINT.Location = New System.Drawing.Point(289, 4)
        Me.btnPRINT.Name = "btnPRINT"
        Me.btnPRINT.Size = New System.Drawing.Size(50, 30)
        Me.btnPRINT.TabIndex = 23
        Me.btnPRINT.Text = "Print"
        Me.btnPRINT.UseVisualStyleBackColor = True
        '
        'btnCARI
        '
        Me.btnCARI.Location = New System.Drawing.Point(1022, 4)
        Me.btnCARI.Name = "btnCARI"
        Me.btnCARI.Size = New System.Drawing.Size(47, 30)
        Me.btnCARI.TabIndex = 22
        Me.btnCARI.Text = "Cari"
        Me.btnCARI.UseVisualStyleBackColor = True
        '
        'tpjSNONOTA
        '
        Me.tpjSNONOTA.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.tpjSNONOTA.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tpjSNONOTA.Location = New System.Drawing.Point(345, 8)
        Me.tpjSNONOTA.Name = "tpjSNONOTA"
        Me.tpjSNONOTA.Size = New System.Drawing.Size(108, 23)
        Me.tpjSNONOTA.TabIndex = 21
        '
        'btnBATAL
        '
        Me.btnBATAL.Location = New System.Drawing.Point(228, 4)
        Me.btnBATAL.Name = "btnBATAL"
        Me.btnBATAL.Size = New System.Drawing.Size(55, 30)
        Me.btnBATAL.TabIndex = 20
        Me.btnBATAL.Text = "Batal"
        Me.btnBATAL.UseVisualStyleBackColor = True
        '
        'btnHAPUS
        '
        Me.btnHAPUS.Location = New System.Drawing.Point(172, 4)
        Me.btnHAPUS.Name = "btnHAPUS"
        Me.btnHAPUS.Size = New System.Drawing.Size(55, 30)
        Me.btnHAPUS.TabIndex = 19
        Me.btnHAPUS.Text = "Hapus"
        Me.btnHAPUS.UseVisualStyleBackColor = True
        '
        'btnUBAH
        '
        Me.btnUBAH.Location = New System.Drawing.Point(116, 4)
        Me.btnUBAH.Name = "btnUBAH"
        Me.btnUBAH.Size = New System.Drawing.Size(55, 30)
        Me.btnUBAH.TabIndex = 18
        Me.btnUBAH.Text = "Ubah"
        Me.btnUBAH.UseVisualStyleBackColor = True
        '
        'btnSIMPAN
        '
        Me.btnSIMPAN.Location = New System.Drawing.Point(60, 4)
        Me.btnSIMPAN.Name = "btnSIMPAN"
        Me.btnSIMPAN.Size = New System.Drawing.Size(55, 30)
        Me.btnSIMPAN.TabIndex = 17
        Me.btnSIMPAN.Text = "Simpan"
        Me.btnSIMPAN.UseVisualStyleBackColor = True
        '
        'btnTAMBAH
        '
        Me.btnTAMBAH.Location = New System.Drawing.Point(4, 4)
        Me.btnTAMBAH.Name = "btnTAMBAH"
        Me.btnTAMBAH.Size = New System.Drawing.Size(55, 30)
        Me.btnTAMBAH.TabIndex = 16
        Me.btnTAMBAH.Text = "Tambah"
        Me.btnTAMBAH.UseVisualStyleBackColor = True
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.Nota1)
        Me.TabControl1.Location = New System.Drawing.Point(4, 67)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(1065, 552)
        Me.TabControl1.TabIndex = 31
        '
        'Nota1
        '
        Me.Nota1.BackColor = System.Drawing.Color.Transparent
        Me.Nota1.Controls.Add(Me.tpjKDHARGA)
        Me.Nota1.Controls.Add(Me.Label18)
        Me.Nota1.Controls.Add(Me.tpjKDSALES)
        Me.Nota1.Controls.Add(Me.tpjNMSALES)
        Me.Nota1.Controls.Add(Me.Label17)
        Me.Nota1.Controls.Add(Me.tpjTOTAL)
        Me.Nota1.Controls.Add(Me.tpjSUBTOTAL)
        Me.Nota1.Controls.Add(Me.tpjLAIN)
        Me.Nota1.Controls.Add(Me.tpjNPPN)
        Me.Nota1.Controls.Add(Me.tpjAPPN)
        Me.Nota1.Controls.Add(Me.tpjNDISK3)
        Me.Nota1.Controls.Add(Me.tpjADISK3)
        Me.Nota1.Controls.Add(Me.tpjNDISK2)
        Me.Nota1.Controls.Add(Me.tpjADISK2)
        Me.Nota1.Controls.Add(Me.tpjNDISK1)
        Me.Nota1.Controls.Add(Me.tpjADISK1)
        Me.Nota1.Controls.Add(Me.tpjALAMAT)
        Me.Nota1.Controls.Add(Me.tpjKDKUST)
        Me.Nota1.Controls.Add(Me.tpjKET)
        Me.Nota1.Controls.Add(Me.tpjNMKUST)
        Me.Nota1.Controls.Add(Me.tpjNONOTA)
        Me.Nota1.Controls.Add(Me.Label14)
        Me.Nota1.Controls.Add(Me.Label13)
        Me.Nota1.Controls.Add(Me.Label9)
        Me.Nota1.Controls.Add(Me.Label8)
        Me.Nota1.Controls.Add(Me.Label12)
        Me.Nota1.Controls.Add(Me.Label10)
        Me.Nota1.Controls.Add(Me.Label11)
        Me.Nota1.Controls.Add(Me.grPENJUALAN)
        Me.Nota1.Controls.Add(Me.btnADDITEM)
        Me.Nota1.Controls.Add(Me.tpjTEMPO)
        Me.Nota1.Controls.Add(Me.tpjTANGGAL)
        Me.Nota1.Controls.Add(Me.Label5)
        Me.Nota1.Controls.Add(Me.Label4)
        Me.Nota1.Controls.Add(Me.Label3)
        Me.Nota1.Controls.Add(Me.Label2)
        Me.Nota1.Controls.Add(Me.Label7)
        Me.Nota1.Controls.Add(Me.Label1)
        Me.Nota1.Location = New System.Drawing.Point(4, 22)
        Me.Nota1.Name = "Nota1"
        Me.Nota1.Padding = New System.Windows.Forms.Padding(3)
        Me.Nota1.Size = New System.Drawing.Size(1057, 526)
        Me.Nota1.TabIndex = 0
        Me.Nota1.Text = "Nota 1"
        '
        'tpjKDHARGA
        '
        Me.tpjKDHARGA.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.tpjKDHARGA.Location = New System.Drawing.Point(111, 77)
        Me.tpjKDHARGA.Name = "tpjKDHARGA"
        Me.tpjKDHARGA.Size = New System.Drawing.Size(138, 20)
        Me.tpjKDHARGA.TabIndex = 50
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(9, 80)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(73, 15)
        Me.Label18.TabIndex = 51
        Me.Label18.Text = "Kode Harga"
        '
        'tpjKDSALES
        '
        Me.tpjKDSALES.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.tpjKDSALES.Location = New System.Drawing.Point(380, 5)
        Me.tpjKDSALES.Name = "tpjKDSALES"
        Me.tpjKDSALES.Size = New System.Drawing.Size(100, 20)
        Me.tpjKDSALES.TabIndex = 49
        '
        'tpjNMSALES
        '
        Me.tpjNMSALES.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.tpjNMSALES.Location = New System.Drawing.Point(486, 5)
        Me.tpjNMSALES.Name = "tpjNMSALES"
        Me.tpjNMSALES.Size = New System.Drawing.Size(270, 20)
        Me.tpjNMSALES.TabIndex = 47
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.Location = New System.Drawing.Point(279, 8)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(38, 15)
        Me.Label17.TabIndex = 46
        Me.Label17.Text = "Sales"
        '
        'tpjTOTAL
        '
        Me.tpjTOTAL.Location = New System.Drawing.Point(860, 498)
        Me.tpjTOTAL.Name = "tpjTOTAL"
        Me.tpjTOTAL.Size = New System.Drawing.Size(190, 20)
        Me.tpjTOTAL.TabIndex = 45
        Me.tpjTOTAL.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tpjSUBTOTAL
        '
        Me.tpjSUBTOTAL.Location = New System.Drawing.Point(860, 360)
        Me.tpjSUBTOTAL.Name = "tpjSUBTOTAL"
        Me.tpjSUBTOTAL.Size = New System.Drawing.Size(190, 20)
        Me.tpjSUBTOTAL.TabIndex = 43
        Me.tpjSUBTOTAL.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tpjLAIN
        '
        Me.tpjLAIN.Location = New System.Drawing.Point(860, 452)
        Me.tpjLAIN.Name = "tpjLAIN"
        Me.tpjLAIN.Size = New System.Drawing.Size(190, 20)
        Me.tpjLAIN.TabIndex = 40
        Me.tpjLAIN.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tpjNPPN
        '
        Me.tpjNPPN.Location = New System.Drawing.Point(912, 475)
        Me.tpjNPPN.Name = "tpjNPPN"
        Me.tpjNPPN.Size = New System.Drawing.Size(138, 20)
        Me.tpjNPPN.TabIndex = 38
        Me.tpjNPPN.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tpjAPPN
        '
        Me.tpjAPPN.Location = New System.Drawing.Point(860, 475)
        Me.tpjAPPN.Name = "tpjAPPN"
        Me.tpjAPPN.Size = New System.Drawing.Size(50, 20)
        Me.tpjAPPN.TabIndex = 37
        Me.tpjAPPN.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tpjNDISK3
        '
        Me.tpjNDISK3.Location = New System.Drawing.Point(912, 429)
        Me.tpjNDISK3.Name = "tpjNDISK3"
        Me.tpjNDISK3.Size = New System.Drawing.Size(138, 20)
        Me.tpjNDISK3.TabIndex = 35
        Me.tpjNDISK3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tpjADISK3
        '
        Me.tpjADISK3.Location = New System.Drawing.Point(860, 429)
        Me.tpjADISK3.Name = "tpjADISK3"
        Me.tpjADISK3.Size = New System.Drawing.Size(50, 20)
        Me.tpjADISK3.TabIndex = 34
        Me.tpjADISK3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tpjNDISK2
        '
        Me.tpjNDISK2.Location = New System.Drawing.Point(912, 406)
        Me.tpjNDISK2.Name = "tpjNDISK2"
        Me.tpjNDISK2.Size = New System.Drawing.Size(138, 20)
        Me.tpjNDISK2.TabIndex = 32
        Me.tpjNDISK2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tpjADISK2
        '
        Me.tpjADISK2.Location = New System.Drawing.Point(860, 406)
        Me.tpjADISK2.Name = "tpjADISK2"
        Me.tpjADISK2.Size = New System.Drawing.Size(50, 20)
        Me.tpjADISK2.TabIndex = 31
        Me.tpjADISK2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tpjNDISK1
        '
        Me.tpjNDISK1.Location = New System.Drawing.Point(912, 383)
        Me.tpjNDISK1.Name = "tpjNDISK1"
        Me.tpjNDISK1.Size = New System.Drawing.Size(138, 20)
        Me.tpjNDISK1.TabIndex = 29
        Me.tpjNDISK1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tpjADISK1
        '
        Me.tpjADISK1.Location = New System.Drawing.Point(860, 383)
        Me.tpjADISK1.Name = "tpjADISK1"
        Me.tpjADISK1.Size = New System.Drawing.Size(50, 20)
        Me.tpjADISK1.TabIndex = 28
        Me.tpjADISK1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tpjALAMAT
        '
        Me.tpjALAMAT.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.tpjALAMAT.Location = New System.Drawing.Point(380, 53)
        Me.tpjALAMAT.Name = "tpjALAMAT"
        Me.tpjALAMAT.Size = New System.Drawing.Size(475, 20)
        Me.tpjALAMAT.TabIndex = 18
        '
        'tpjKDKUST
        '
        Me.tpjKDKUST.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.tpjKDKUST.Location = New System.Drawing.Point(380, 29)
        Me.tpjKDKUST.Name = "tpjKDKUST"
        Me.tpjKDKUST.Size = New System.Drawing.Size(100, 20)
        Me.tpjKDKUST.TabIndex = 17
        '
        'tpjKET
        '
        Me.tpjKET.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.tpjKET.Location = New System.Drawing.Point(380, 77)
        Me.tpjKET.Name = "tpjKET"
        Me.tpjKET.Size = New System.Drawing.Size(669, 20)
        Me.tpjKET.TabIndex = 5
        '
        'tpjNMKUST
        '
        Me.tpjNMKUST.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.tpjNMKUST.Location = New System.Drawing.Point(486, 29)
        Me.tpjNMKUST.Name = "tpjNMKUST"
        Me.tpjNMKUST.Size = New System.Drawing.Size(270, 20)
        Me.tpjNMKUST.TabIndex = 12
        '
        'tpjNONOTA
        '
        Me.tpjNONOTA.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.tpjNONOTA.Location = New System.Drawing.Point(111, 29)
        Me.tpjNONOTA.Name = "tpjNONOTA"
        Me.tpjNONOTA.Size = New System.Drawing.Size(138, 20)
        Me.tpjNONOTA.TabIndex = 1
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(796, 501)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(34, 15)
        Me.Label14.TabIndex = 44
        Me.Label14.Text = "Total"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(796, 361)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(52, 15)
        Me.Label13.TabIndex = 42
        Me.Label13.Text = "Subtotal"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(796, 455)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(59, 15)
        Me.Label9.TabIndex = 39
        Me.Label9.Text = "Lain-Lain"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(796, 476)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(32, 15)
        Me.Label8.TabIndex = 36
        Me.Label8.Text = "PPN"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(796, 432)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(55, 15)
        Me.Label12.TabIndex = 33
        Me.Label12.Text = "Diskon 3"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(796, 409)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(55, 15)
        Me.Label10.TabIndex = 30
        Me.Label10.Text = "Diskon 2"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(796, 386)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(55, 15)
        Me.Label11.TabIndex = 25
        Me.Label11.Text = "Diskon 1"
        '
        'grPENJUALAN
        '
        Me.grPENJUALAN.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grPENJUALAN.Location = New System.Drawing.Point(3, 129)
        Me.grPENJUALAN.Name = "grPENJUALAN"
        Me.grPENJUALAN.Size = New System.Drawing.Size(1048, 225)
        Me.grPENJUALAN.TabIndex = 20
        '
        'btnADDITEM
        '
        Me.btnADDITEM.Location = New System.Drawing.Point(985, 103)
        Me.btnADDITEM.Name = "btnADDITEM"
        Me.btnADDITEM.Size = New System.Drawing.Size(65, 20)
        Me.btnADDITEM.TabIndex = 19
        Me.btnADDITEM.Text = "Add Item"
        Me.btnADDITEM.UseVisualStyleBackColor = True
        '
        'tpjTEMPO
        '
        Me.tpjTEMPO.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.tpjTEMPO.Location = New System.Drawing.Point(111, 53)
        Me.tpjTEMPO.Name = "tpjTEMPO"
        Me.tpjTEMPO.Size = New System.Drawing.Size(138, 20)
        Me.tpjTEMPO.TabIndex = 7
        '
        'tpjTANGGAL
        '
        Me.tpjTANGGAL.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.tpjTANGGAL.Location = New System.Drawing.Point(111, 5)
        Me.tpjTANGGAL.Name = "tpjTANGGAL"
        Me.tpjTANGGAL.Size = New System.Drawing.Size(138, 20)
        Me.tpjTANGGAL.TabIndex = 6
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(278, 55)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(45, 15)
        Me.Label5.TabIndex = 14
        Me.Label5.Text = "Alamat"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(278, 78)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(71, 15)
        Me.Label4.TabIndex = 4
        Me.Label4.Text = "Keterangan"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(9, 55)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(79, 15)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "Jatuh Tempo"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(9, 32)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(52, 15)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "No Nota"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(278, 32)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(60, 15)
        Me.Label7.TabIndex = 11
        Me.Label7.Text = "Kustomer"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(9, 8)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(52, 15)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Tanggal"
        '
        'tpjSNMSLS
        '
        Me.tpjSNMSLS.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.tpjSNMSLS.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tpjSNMSLS.Location = New System.Drawing.Point(795, 8)
        Me.tpjSNMSLS.Name = "tpjSNMSLS"
        Me.tpjSNMSLS.Size = New System.Drawing.Size(113, 23)
        Me.tpjSNMSLS.TabIndex = 32
        '
        'FPenjualan
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1075, 622)
        Me.Controls.Add(Me.tpjSNMSLS)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.tpjSLUNAS)
        Me.Controls.Add(Me.tpjSNMKUST)
        Me.Controls.Add(Me.dtpjTGL2)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.dtpjTGL1)
        Me.Controls.Add(Me.bpjTabDel)
        Me.Controls.Add(Me.bpjTabAdd)
        Me.Controls.Add(Me.btnPRINT)
        Me.Controls.Add(Me.btnCARI)
        Me.Controls.Add(Me.tpjSNONOTA)
        Me.Controls.Add(Me.btnBATAL)
        Me.Controls.Add(Me.btnHAPUS)
        Me.Controls.Add(Me.btnUBAH)
        Me.Controls.Add(Me.btnSIMPAN)
        Me.Controls.Add(Me.btnTAMBAH)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FPenjualan"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "FPenjualan"
        Me.TabControl1.ResumeLayout(False)
        Me.Nota1.ResumeLayout(False)
        Me.Nota1.PerformLayout()
        CType(Me.grPENJUALAN, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents tpjSLUNAS As System.Windows.Forms.ComboBox
    Friend WithEvents tpjSNMKUST As System.Windows.Forms.TextBox
    Friend WithEvents dtpjTGL2 As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents dtpjTGL1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents bpjTabDel As System.Windows.Forms.Button
    Friend WithEvents bpjTabAdd As System.Windows.Forms.Button
    Friend WithEvents btnPRINT As System.Windows.Forms.Button
    Friend WithEvents btnCARI As System.Windows.Forms.Button
    Friend WithEvents tpjSNONOTA As System.Windows.Forms.TextBox
    Friend WithEvents btnBATAL As System.Windows.Forms.Button
    Friend WithEvents btnHAPUS As System.Windows.Forms.Button
    Friend WithEvents btnUBAH As System.Windows.Forms.Button
    Friend WithEvents btnSIMPAN As System.Windows.Forms.Button
    Friend WithEvents btnTAMBAH As System.Windows.Forms.Button
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents Nota1 As System.Windows.Forms.TabPage
    Friend WithEvents tpjTOTAL As System.Windows.Forms.TextBox
    Friend WithEvents tpjSUBTOTAL As System.Windows.Forms.TextBox
    Friend WithEvents tpjLAIN As System.Windows.Forms.TextBox
    Friend WithEvents tpjNPPN As System.Windows.Forms.TextBox
    Friend WithEvents tpjAPPN As System.Windows.Forms.TextBox
    Friend WithEvents tpjNDISK3 As System.Windows.Forms.TextBox
    Friend WithEvents tpjADISK3 As System.Windows.Forms.TextBox
    Friend WithEvents tpjNDISK2 As System.Windows.Forms.TextBox
    Friend WithEvents tpjADISK2 As System.Windows.Forms.TextBox
    Friend WithEvents tpjNDISK1 As System.Windows.Forms.TextBox
    Friend WithEvents tpjADISK1 As System.Windows.Forms.TextBox
    Friend WithEvents tpjALAMAT As System.Windows.Forms.TextBox
    Friend WithEvents tpjKDKUST As System.Windows.Forms.TextBox
    Friend WithEvents tpjKET As System.Windows.Forms.TextBox
    Friend WithEvents tpjNMKUST As System.Windows.Forms.TextBox
    Friend WithEvents tpjNONOTA As System.Windows.Forms.TextBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents grPENJUALAN As System.Windows.Forms.DataGridView
    Friend WithEvents btnADDITEM As System.Windows.Forms.Button
    Friend WithEvents tpjTEMPO As System.Windows.Forms.DateTimePicker
    Friend WithEvents tpjTANGGAL As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents tpjKDSALES As System.Windows.Forms.TextBox
    Friend WithEvents tpjNMSALES As System.Windows.Forms.TextBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents tpjKDHARGA As System.Windows.Forms.TextBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents tpjSNMSLS As System.Windows.Forms.TextBox
End Class
