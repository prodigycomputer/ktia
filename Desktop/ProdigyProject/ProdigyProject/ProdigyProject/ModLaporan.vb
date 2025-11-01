Imports System.Data.Odbc
Imports CrystalDecisions.CrystalReports.Engine

Module ModLaporan

    Public HeaderNamaToko As String = "Prodigy Computer"
    Public HeaderAlamat As String = "Gajah Mada, Jl. Setia Budi No 21/115, Pontianak"

    Public Sub LoadLaporan(ByVal jenis As String, ByVal viewer As CrystalDecisions.Windows.Forms.CrystalReportViewer)
        Try
            Call BukaKoneksi()

            Dim sql As String = ""

            Select Case jenis.ToLower()
                Case "laporanbeli"
                    sql =
                        "SELECT zbeli.nonota, zbeli.tgl, zbeli.tgltempo, zsupplier.namasup, zbeli.nilai " &
                        "FROM zbeli " &
                        "LEFT JOIN zsupplier ON zbeli.kodesup = zsupplier.kodesup " &
                        "ORDER BY zbeli.nonota ASC"

                Case "laporanjual"
                    sql =
                        "SELECT zjual.nonota, zjual.tgl, zjual.tgltempo, zkustomer.namakust, zsales.namasls, zjual.nilai " &
                        "FROM zjual " &
                        "LEFT JOIN zkustomer ON zjual.kodekust = zkustomer.kodekust " &
                        "LEFT JOIN zsales ON zjual.kodesls = zsales.kodesls " &
                        "ORDER BY zjual.nonota ASC"

                Case "laporanmutasi"
                    sql =
                        "SELECT zmutasi.nonota, zmutasi.tgl, g1.namagd AS namagd1, g2.namagd AS namagd2 " &
                        "FROM zmutasi " &
                        "JOIN zgudang g1 ON zmutasi.kodegd1 = g1.kodegd " &
                        "JOIN zgudang g2 ON zmutasi.kodegd2 = g2.kodegd " &
                        "ORDER BY zmutasi.nonota ASC"

                Case "laporanpenyesuaian"
                    sql =
                        "SELECT zpenyesuaian.tgl, zpenyesuaian.nonota, zgudang.namagd " &
                        "FROM zpenyesuaian " &
                        "LEFT JOIN zgudang ON zpenyesuaian.kodegd = zgudang.kodegd " &
                        "ORDER BY zpenyesuaian.nonota ASC"
            End Select

            Dim da As New OdbcDataAdapter(sql, Conn)
            Dim dt As New DataTable
            da.Fill(dt)

            If dt.Rows.Count = 0 Then
                MessageBox.Show("Data laporan tidak ditemukan!")
                Exit Sub
            End If

            Dim header As DataRow = dt.Rows(0)
            Dim headerCols As String() = {
                "nonota", "tgl", "tgltempo", "nofaktur",
                "kodesup", "kodekust", "kodesls",
                "namasup", "namakust", "namasls", "alamat",
                "kodegd1", "kodegd2", "namagd1", "namagd2",
                "kodegd", "namagd", "nilai"
            }

            ' Buat dataset
            Dim ds As New LaporanDt()

            Select Case jenis.ToLower()
                Case "laporanbeli"
                    ds.Tables("LaporanBeli").Merge(dt)

                Case "laporanjual"
                    ds.Tables("LaporanJual").Merge(dt)

                Case "laporanmutasi"
                    ds.Tables("LaporanMutasi").Merge(dt)

                Case "laporanpenyesuaian"
                    ds.Tables("LaporanPenyesuaian").Merge(dt)
            End Select


            ' Pilih report sesuai jenis
            Dim rpt As New ReportDocument()
            Select Case jenis.ToLower()
                Case "laporanbeli"
                    rpt.Load(Application.StartupPath & "\Laporan\LaporanPembelian.rpt")

                Case "laporanjual"
                    rpt.Load(Application.StartupPath & "\Laporan\LaporanPenjualan.rpt")

                Case "laporanmutasi"
                    rpt.Load(Application.StartupPath & "\Laporan\LaporanMutasi.rpt")

                Case "laporanpenyesuaian"
                    rpt.Load(Application.StartupPath & "\Laporan\LaporanPenyesuaian.rpt")
            End Select

            rpt.SetDataSource(ds)
            rpt.SetParameterValue("pNamaToko", HeaderNamaToko)
            rpt.SetParameterValue("pAlamat", HeaderAlamat)
            viewer.ReportSource = rpt
            viewer.Refresh()

        Catch ex As Exception
            MessageBox.Show("Gagal load report: " & ex.Message)
        End Try
    End Sub
End Module
