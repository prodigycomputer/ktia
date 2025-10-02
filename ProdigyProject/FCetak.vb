Imports System.Data.Odbc
Imports CrystalDecisions.CrystalReports.Engine

Public Class FCetak
    Public Param As New Dictionary(Of String, Object)

    Private Sub FCetak_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        Try
            Call BukaKoneksi()

            ' Ambil nomor nota dari parameter
            Dim nonota As String = Param("nonota").ToString()

            ' SQL ambil data sesuai nota
            Dim sql As String =
                "SELECT zstok.namabrg, zbeli.nonota, zbeli.tgl, zbeli.kodesup, " &
                "zbeli.tgltempo, zbeli.disc1, zbeli.disc2, zbeli.disc3, zbeli.ppn, " &
                "zbeli.lainnya, zbeli.nilai, zbelim.kodebrg, zbelim.jlh1, zbelim.jlh2, zbelim.jlh3, " &
                "zbelim.disca, zbelim.discb, zbelim.discc, zbelim.discrp, zbelim.jumlah, " &
                "zstok.satuan1, zstok.satuan2, zstok.satuan3, zbelim.harga, " &
                "zsupplier.namasup, zsupplier.alamat " &
                "FROM ((dbkita.zbelim zbelim " &
                "INNER JOIN dbkita.zbeli zbeli ON zbelim.nonota=zbeli.nonota) " &
                "INNER JOIN dbkita.zstok zstok ON zbelim.kodebrg=zstok.kodebrg) " &
                "INNER JOIN dbkita.zsupplier zsupplier ON zbeli.kodesup=zsupplier.kodesup " &
                "WHERE zbeli.nonota='" & nonota & "'"

            ' Isi DataTable
            Dim da As New OdbcDataAdapter(sql, Conn)
            Dim dt As New DataTable
            da.Fill(dt)

            If dt.Rows.Count = 0 Then
                MessageBox.Show("Data nota tidak ditemukan!")
                Exit Sub
            End If

            ' --- Tambahkan baris kosong agar total baris kelipatan 10 ---
            Dim totalRows As Integer = dt.Rows.Count
            Dim sisa As Integer = totalRows Mod 10

            If sisa <> 0 Then
                Dim tambahan As Integer = 10 - sisa
                For i As Integer = 1 To tambahan
                    Dim row As DataRow = dt.NewRow()
                    dt.Rows.Add(row) ' baris kosong
                Next
            End If

            ' Buat dataset dari XSD (NotaPem.xsd)
            Dim ds As New NotaDt()

            ' Merge ke tabel "NotaPembelian"
            ds.Tables("NotaPembelian").Merge(dt)

            ' Load report
            Dim rpt As New ReportDocument()
            rpt.Load(Application.StartupPath & "\NotaPembelian.rpt")

            ' Set datasource
            rpt.SetDataSource(ds)

            ' Tampilkan ke viewer
            CrystalReportViewer1.ReportSource = rpt
            CrystalReportViewer1.Refresh()

        Catch ex As Exception
            MessageBox.Show("Gagal load report: " & ex.Message)
        End Try
    End Sub
End Class
