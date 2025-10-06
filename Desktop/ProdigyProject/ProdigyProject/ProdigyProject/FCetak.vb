Imports System.Data.Odbc
Imports CrystalDecisions.CrystalReports.Engine

Public Class FCetak
    Public Param As New Dictionary(Of String, Object)

    Private Sub FCetak_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim nonota As String = Param("nonota").ToString()
        Dim jenis As String = Param("jenis").ToString().ToLower()  ' isi: "pembelian" atau "penjualan"

        LoadNota(jenis, nonota, CrystalReportViewer1)
    End Sub

End Class
