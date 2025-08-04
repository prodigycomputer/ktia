Imports MySql.Data.MySqlClient
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared

Public Class LaporanStokViewer

    Private laporan As ReportDocument

    ' Metode untuk memuat laporan
    Public Sub LoadReport(ByVal dataTable As DataTable)
        ' Membuat instance dari laporan
        laporan = New ReportDocument()
        laporan.Load("C:\Users\Asus\OneDrive\Documents\Data Kuliah\Data Skripsi\Skripsiv2\Eiffel Petshop App\Eiffel Petshop\Eiffel Petshop\LaporanStok.rpt") ' Update path sesuai lokasi laporan Anda

        ' Mengatur DataTable sebagai sumber data laporan
        laporan.SetDataSource(dataTable)

        ' Menampilkan laporan di CrystalReportViewer
        CrystalReportViewer1.ReportSource = laporan
        CrystalReportViewer1.Refresh()
    End Sub

    Private Sub LaporanStokViewer_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
End Class