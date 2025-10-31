Imports System.Data.Odbc
Imports CrystalDecisions.CrystalReports.Engine

Public Class FCetakLaporan
    Public Param As New Dictionary(Of String, Object)

    Private Sub FCetakLaporan_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim jenis As String = Param("jenis").ToString().ToLower()

        LoadLaporan(jenis, CrystalReportViewer1)
    End Sub
End Class