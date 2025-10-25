<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ItMerek
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
        Me.dgitmMEREK = New System.Windows.Forms.DataGridView()
        CType(Me.dgitmMEREK, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dgitmMEREK
        '
        Me.dgitmMEREK.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgitmMEREK.Location = New System.Drawing.Point(5, 4)
        Me.dgitmMEREK.Name = "dgitmMEREK"
        Me.dgitmMEREK.Size = New System.Drawing.Size(350, 214)
        Me.dgitmMEREK.TabIndex = 6
        '
        'ItMerek
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(361, 223)
        Me.Controls.Add(Me.dgitmMEREK)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "ItMerek"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ItMerek"
        CType(Me.dgitmMEREK, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents dgitmMEREK As System.Windows.Forms.DataGridView
End Class
