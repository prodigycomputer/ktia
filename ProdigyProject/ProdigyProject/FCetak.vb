Imports System.Data.Odbc
Imports CrystalDecisions.CrystalReports.Engine

Public Class FCetak
    Public ReportPath As String
    Public Param As New Dictionary(Of String, Object)

    Private Function PadTable(ByVal dt As DataTable, ByVal rowsPerPage As Integer) As DataTable
        Dim total As Integer = dt.Rows.Count
        Dim sisa As Integer = total Mod rowsPerPage
        Dim tambah As Integer = 0

        If total < rowsPerPage Then
            ' kalau data kurang dari 1 halaman, isi sampai full 1 halaman
            tambah = rowsPerPage - total
        ElseIf sisa > 0 Then
            ' kalau lebih dari 1 halaman tapi tidak pas kelipatan, isi sisanya
            tambah = rowsPerPage - sisa
        End If

        For i As Integer = 1 To tambah
            Dim newRow As DataRow = dt.NewRow()
            ' biarkan kosong atau isi default
            dt.Rows.Add(newRow)
        Next

        Return dt
    End Function


    Private Sub FCetak_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        Try
            Call BukaKoneksi()

            ' query sama persis dengan yang dipakai di report
            Dim sql As String = "SELECT zstok1.namabrg, zbeli1.nonota, zbeli1.tgl, zbeli1.kodesup, " &
                                "zbeli1.tgltempo, zbelim1.kodebrg, zbelim1.jlh1, zbelim1.jlh2, zbelim1.jlh3, " &
                                "zbelim1.disca, zbelim1.discb, zbelim1.discc, zbelim1.discrp, zbelim1.jumlah, " &
                                "zstok1.satuan1, zstok1.satuan2, zstok1.satuan3, zstok1.harga1, " &
                                "zsupplier1.namasup, zsupplier1.alamat " &
                                "FROM ((dbkita.zbelim zbelim1 " &
                                "INNER JOIN dbkita.zbeli zbeli1 ON zbelim1.nonota=zbeli1.nonota) " &
                                "INNER JOIN dbkita.zstok zstok1 ON zbelim1.kodebrg=zstok1.kodebrg) " &
                                "INNER JOIN dbkita.zsupplier zsupplier1 ON zbeli1.kodesup=zsupplier1.kodesup " &
                                "WHERE zbeli1.nonota='NOTA1';"

            Dim da As New Odbc.OdbcDataAdapter(sql, Conn)
            Dim dt As New DataTable
            da.Fill(dt)

            If dt.Rows.Count = 0 Then
                MessageBox.Show("Data tidak ditemukan")
                Exit Sub
            End If

            ' panggil PadTable biar jumlah baris kelipatan 10
            dt = PadTable(dt, 10)

            ' load report
            Dim projectPath As String = IO.Path.GetFullPath("..\..\")
            Dim rptPath As String = IO.Path.Combine(projectPath, "NotaPembelian.rpt")

            If Not IO.File.Exists(rptPath) Then
                MessageBox.Show("Report file tidak ditemukan: " & rptPath)
                Exit Sub
            End If

            Dim rpt As New ReportDocument()
            rpt.Load(rptPath)
            rpt.SetDataSource(dt)

            CrystalReportViewer1.ReportSource = rpt
            CrystalReportViewer1.Refresh()

        Catch ex As Exception
            MessageBox.Show("Gagal load report: " & ex.Message & vbCrLf & ex.StackTrace)
        End Try

    End Sub
End Class
