Imports System.Data.Odbc
Imports CrystalDecisions.CrystalReports.Engine

Public Class FCetak
    Public Param As New Dictionary(Of String, Object)

    Private Sub FCetak_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Dim jenis As String = Param("jenis").ToString().ToLower()
            Dim nonota As String = Param("nonota").ToString().ToLower()
            Dim tipe As String = Param("tipe").ToString().ToLower()

            ' --- Cek apakah parameter "nonota" ada ---
            If tipe = "nota" Then
                ' === Mode cetak nota ===
                LoadNota(jenis, nonota, CrystalReportViewer1)
            ElseIf tipe = "laporan" Then
                ' === Mode cetak laporan ===
                LoadLaporan(jenis, nonota, Param, CrystalReportViewer1)

            Else
                MessageBox.Show("Tipe cetak tidak dikenal: " & tipe,
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If

        Catch ex As Exception
            MessageBox.Show("Terjadi kesalahan saat memuat laporan: " & ex.Message,
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

End Class
