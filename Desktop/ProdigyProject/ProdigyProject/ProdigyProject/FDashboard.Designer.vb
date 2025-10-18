<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FDashboard
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
        Me.hMenu = New System.Windows.Forms.MenuStrip()
        Me.mFile = New System.Windows.Forms.ToolStripMenuItem()
        Me.smExit = New System.Windows.Forms.ToolStripMenuItem()
        Me.mTransaksi = New System.Windows.Forms.ToolStripMenuItem()
        Me.smPembelian = New System.Windows.Forms.ToolStripMenuItem()
        Me.smReturPembelian = New System.Windows.Forms.ToolStripMenuItem()
        Me.smPenjualan = New System.Windows.Forms.ToolStripMenuItem()
        Me.smReturPenjualan = New System.Windows.Forms.ToolStripMenuItem()
        Me.smMutasi = New System.Windows.Forms.ToolStripMenuItem()
        Me.smPenyesuaian = New System.Windows.Forms.ToolStripMenuItem()
        Me.mLaporan = New System.Windows.Forms.ToolStripMenuItem()
        Me.smLaporan = New System.Windows.Forms.ToolStripMenuItem()
        Me.smAkun = New System.Windows.Forms.ToolStripMenuItem()
        Me.hMenu.SuspendLayout()
        Me.SuspendLayout()
        '
        'hMenu
        '
        Me.hMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mFile, Me.mTransaksi, Me.mLaporan})
        Me.hMenu.Location = New System.Drawing.Point(0, 0)
        Me.hMenu.Name = "hMenu"
        Me.hMenu.Size = New System.Drawing.Size(1066, 24)
        Me.hMenu.TabIndex = 0
        Me.hMenu.Text = "Menu"
        '
        'mFile
        '
        Me.mFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.mFile.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.smAkun, Me.smExit})
        Me.mFile.Name = "mFile"
        Me.mFile.Size = New System.Drawing.Size(37, 20)
        Me.mFile.Text = "File"
        '
        'smExit
        '
        Me.smExit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.smExit.Name = "smExit"
        Me.smExit.Size = New System.Drawing.Size(152, 22)
        Me.smExit.Text = "Exit"
        '
        'mTransaksi
        '
        Me.mTransaksi.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.mTransaksi.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.smPembelian, Me.smReturPembelian, Me.smPenjualan, Me.smReturPenjualan, Me.smMutasi, Me.smPenyesuaian})
        Me.mTransaksi.Name = "mTransaksi"
        Me.mTransaksi.Size = New System.Drawing.Size(67, 20)
        Me.mTransaksi.Text = "Transaksi"
        '
        'smPembelian
        '
        Me.smPembelian.Name = "smPembelian"
        Me.smPembelian.Size = New System.Drawing.Size(161, 22)
        Me.smPembelian.Text = "Pembelian"
        '
        'smReturPembelian
        '
        Me.smReturPembelian.Name = "smReturPembelian"
        Me.smReturPembelian.Size = New System.Drawing.Size(161, 22)
        Me.smReturPembelian.Text = "Retur Pembelian"
        '
        'smPenjualan
        '
        Me.smPenjualan.Name = "smPenjualan"
        Me.smPenjualan.Size = New System.Drawing.Size(161, 22)
        Me.smPenjualan.Text = "Penjualan"
        '
        'smReturPenjualan
        '
        Me.smReturPenjualan.Name = "smReturPenjualan"
        Me.smReturPenjualan.Size = New System.Drawing.Size(161, 22)
        Me.smReturPenjualan.Text = "Retur Penjualan"
        '
        'smMutasi
        '
        Me.smMutasi.Name = "smMutasi"
        Me.smMutasi.Size = New System.Drawing.Size(161, 22)
        Me.smMutasi.Text = "Mutasi"
        '
        'smPenyesuaian
        '
        Me.smPenyesuaian.Name = "smPenyesuaian"
        Me.smPenyesuaian.Size = New System.Drawing.Size(161, 22)
        Me.smPenyesuaian.Text = "Penyesuaian"
        '
        'mLaporan
        '
        Me.mLaporan.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.mLaporan.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.smLaporan})
        Me.mLaporan.Name = "mLaporan"
        Me.mLaporan.Size = New System.Drawing.Size(62, 20)
        Me.mLaporan.Text = "Laporan"
        '
        'smLaporan
        '
        Me.smLaporan.Name = "smLaporan"
        Me.smLaporan.Size = New System.Drawing.Size(117, 22)
        Me.smLaporan.Text = "Laporan"
        '
        'smAkun
        '
        Me.smAkun.Name = "smAkun"
        Me.smAkun.Size = New System.Drawing.Size(152, 22)
        Me.smAkun.Text = "Akun"
        '
        'FDashboard
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1066, 497)
        Me.Controls.Add(Me.hMenu)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.IsMdiContainer = True
        Me.Name = "FDashboard"
        Me.Text = "Prodigy"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.hMenu.ResumeLayout(False)
        Me.hMenu.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents hMenu As System.Windows.Forms.MenuStrip
    Friend WithEvents mFile As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents smExit As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mTransaksi As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents smPembelian As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents smPenjualan As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents smMutasi As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents smPenyesuaian As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mLaporan As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents smLaporan As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents smReturPembelian As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents smReturPenjualan As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents smAkun As System.Windows.Forms.ToolStripMenuItem

End Class
