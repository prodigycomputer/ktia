Imports MySql.Data.MySqlClient
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared

Public Class LaporanPenjualanViewer

    Private laporan As ReportDocument

    ' Metode untuk memuat laporan
    Public Sub LoadReport(ByVal dataTable As DataTable)
        ' Membuat instance dari laporan
        laporan = New ReportDocument()
        laporan.Load("C:\Users\Asus\OneDrive\Documents\Data Kuliah\Data Skripsi\Skripsiv2\Eiffel Petshop App\Eiffel Petshop\Eiffel Petshop\LaporanPenjualan.rpt") ' Update path sesuai lokasi laporan Anda

        ' Mengatur DataTable sebagai sumber data laporan
        laporan.SetDataSource(dataTable)

        ' Menampilkan laporan di CrystalReportViewer
        CrystalReportViewer1.ReportSource = laporan
        CrystalReportViewer1.Refresh()
    End Sub

    Private Sub LaporanPenjualanViewer_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub CrystalReportViewer1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CrystalReportViewer1.Load

    End Sub
End Class