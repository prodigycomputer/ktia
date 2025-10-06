<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ItDtKustomer
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
        Me.dgitmKUST = New System.Windows.Forms.DataGridView()
        CType(Me.dgitmKUST, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dgitmKUST
        '
        Me.dgitmKUST.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgitmKUST.Location = New System.Drawing.Point(8, 7)
        Me.dgitmKUST.Name = "dgitmKUST"
        Me.dgitmKUST.Size = New System.Drawing.Size(547, 292)
        Me.dgitmKUST.TabIndex = 0
        '
        'ItDtKustomer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(563, 308)
        Me.Controls.Add(Me.dgitmKUST)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "ItDtKustomer"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ItDtKustomer"
        CType(Me.dgitmKUST, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents dgitmKUST As System.Windows.Forms.DataGridView
End Class
