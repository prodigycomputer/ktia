﻿Imports System.Data.Odbc

Public Class ItMerek

    Public Sub LoadDataMerek(ByVal keyword As String)

        Dim sql As String
        If keyword = "*" Then
            sql = "SELECT kodemerk, namamerk FROM zmerek ORDER BY kodemerk"
        Else
            sql = "SELECT kodemerk, namamerk " &
                  "FROM zmerek WHERE kodemerk LIKE '%" & keyword & "%' OR namamerk LIKE '%" & keyword & "%' ORDER BY kodemerk"
        End If

        dgitmMEREK.Rows.Clear()

        Try
            BukaKoneksi()
            Cmd = New OdbcCommand(sql, Conn)
            Rd = Cmd.ExecuteReader()

            While Rd.Read()
                dgitmMEREK.Rows.Add(Rd("kodemerk"), Rd("namamerk"))
            End While

            Rd.Close()
            Conn.Close()
        Catch ex As Exception
            MessageBox.Show("Gagal load data: " & ex.Message)
        End Try
    End Sub

    Private Sub ItMerek_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.FormBorderStyle = FormBorderStyle.FixedSingle
        Me.MaximizeBox = False

        SetupGridDMerek(dgitmMEREK)
    End Sub

    Private Sub dgitmMEREK_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgitmMEREK.CellContentClick
        If e.RowIndex >= 0 Then
            Dim kode As String = dgitmMEREK.Rows(e.RowIndex).Cells("kodemerk").Value.ToString()
            Dim nama As String = dgitmMEREK.Rows(e.RowIndex).Cells("namamerk").Value.ToString()

            ' kirim ke ItFPopupPem
            Dim parentForm As FStok = TryCast(Me.Owner, FStok)
            If parentForm IsNot Nothing Then
                parentForm.SetMerek(kode, nama)
            End If

            Me.Close()
        End If
    End Sub
End Class