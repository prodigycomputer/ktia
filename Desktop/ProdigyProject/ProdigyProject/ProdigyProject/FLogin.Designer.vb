﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FLogin
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.tbUSERNAME = New System.Windows.Forms.TextBox()
        Me.tbPASSWORD = New System.Windows.Forms.TextBox()
        Me.btnCANCEL = New System.Windows.Forms.Button()
        Me.btnLOGIN = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(56, 42)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(85, 17)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "USERNAME"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(56, 66)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(88, 17)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "PASSWORD"
        '
        'tbUSERNAME
        '
        Me.tbUSERNAME.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.tbUSERNAME.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbUSERNAME.Location = New System.Drawing.Point(161, 38)
        Me.tbUSERNAME.Name = "tbUSERNAME"
        Me.tbUSERNAME.Size = New System.Drawing.Size(131, 23)
        Me.tbUSERNAME.TabIndex = 2
        '
        'tbPASSWORD
        '
        Me.tbPASSWORD.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.tbPASSWORD.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbPASSWORD.Location = New System.Drawing.Point(161, 64)
        Me.tbPASSWORD.Name = "tbPASSWORD"
        Me.tbPASSWORD.Size = New System.Drawing.Size(131, 23)
        Me.tbPASSWORD.TabIndex = 3
        '
        'btnCANCEL
        '
        Me.btnCANCEL.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCANCEL.Location = New System.Drawing.Point(86, 115)
        Me.btnCANCEL.Name = "btnCANCEL"
        Me.btnCANCEL.Size = New System.Drawing.Size(75, 33)
        Me.btnCANCEL.TabIndex = 4
        Me.btnCANCEL.Text = "CANCEL"
        Me.btnCANCEL.UseVisualStyleBackColor = True
        '
        'btnLOGIN
        '
        Me.btnLOGIN.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnLOGIN.Location = New System.Drawing.Point(187, 115)
        Me.btnLOGIN.Name = "btnLOGIN"
        Me.btnLOGIN.Size = New System.Drawing.Size(75, 33)
        Me.btnLOGIN.TabIndex = 5
        Me.btnLOGIN.Text = "LOGIN"
        Me.btnLOGIN.UseVisualStyleBackColor = True
        '
        'FLogin
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(349, 187)
        Me.Controls.Add(Me.btnLOGIN)
        Me.Controls.Add(Me.btnCANCEL)
        Me.Controls.Add(Me.tbPASSWORD)
        Me.Controls.Add(Me.tbUSERNAME)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "FLogin"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "FLogin"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents tbUSERNAME As System.Windows.Forms.TextBox
    Friend WithEvents tbPASSWORD As System.Windows.Forms.TextBox
    Friend WithEvents btnCANCEL As System.Windows.Forms.Button
    Friend WithEvents btnLOGIN As System.Windows.Forms.Button
End Class
