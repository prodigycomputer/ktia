Imports System.Data.Odbc
Imports CrystalDecisions.CrystalReports.Engine

Module ModNota

    Public HeaderNamaToko As String = "Prodigy Computer"
    Public HeaderAlamat As String = "Gajah Mada, Jl. Setia Budi No 21/115, Pontianak"

    ' Fungsi umum untuk load report
    Public Sub LoadNota(ByVal jenis As String, ByVal nonota As String, ByVal viewer As CrystalDecisions.Windows.Forms.CrystalReportViewer)
        Try
            Call BukaKoneksi()

            Dim sql As String = ""

            Select Case jenis.ToLower()
                Case "pembelian"
                    sql =
                        "SELECT zstok.namabrg, zbeli.nonota, zbeli.tgl, zbeli.kodesup, " &
                        "zbeli.tgltempo, zbeli.disc1, zbeli.disc2, zbeli.disc3, zbeli.hdisc1, zbeli.hdisc2, zbeli.hdisc3, " &
                        "zbeli.ppn, zbeli.hppn, zbeli.lainnya, zbeli.nilai, " &
                        "zbelim.kodebrg, zbelim.jlh1, zbelim.jlh2, zbelim.jlh3, " &
                        "zbelim.disca, zbelim.discb, zbelim.discc, zbelim.discrp, zbelim.jumlah, " &
                        "zstok.satuan1, zstok.satuan2, zstok.satuan3, zbelim.harga, " &
                        "zsupplier.namasup, zsupplier.alamat " &
                        "FROM ((dbkita.zbeli zbeli " &
                        "INNER JOIN dbkita.zbelim zbelim ON zbeli.nonota=zbelim.nonota) " &
                        "LEFT JOIN dbkita.zstok zstok ON zbelim.kodebrg=zstok.kodebrg) " &
                        "LEFT JOIN dbkita.zsupplier zsupplier ON zbeli.kodesup=zsupplier.kodesup " &
                        "WHERE zbeli.nonota='" & nonota & "'"

                Case "rpembelian"
                    sql =
                        "SELECT zstok.namabrg, zrbeli.nonota, zrbeli.tgl, zrbeli.kodesup, zrbeli.nofaktur," &
                        "zrbeli.tgltempo, zrbeli.disc1, zrbeli.disc2, zrbeli.disc3, zrbeli.hdisc1, zrbeli.hdisc2, zrbeli.hdisc3, " &
                        "zrbeli.ppn, zrbeli.hppn, zrbeli.lainnya, zrbeli.nilai, " &
                        "zrbelim.kodebrg, zrbelim.jlh1, zrbelim.jlh2, zrbelim.jlh3, " &
                        "zrbelim.disca, zrbelim.discb, zrbelim.discc, zrbelim.discrp, zrbelim.jumlah, " &
                        "zstok.satuan1, zstok.satuan2, zstok.satuan3, zrbelim.harga, " &
                        "zsupplier.namasup, zsupplier.alamat " &
                        "FROM ((dbkita.zrbeli zrbeli " &
                        "INNER JOIN dbkita.zrbelim zrbelim ON zrbeli.nonota=zrbelim.nonota) " &
                        "LEFT JOIN dbkita.zstok zstok ON zrbelim.kodebrg=zstok.kodebrg) " &
                        "LEFT JOIN dbkita.zsupplier zsupplier ON zrbeli.kodesup=zsupplier.kodesup " &
                        "WHERE zrbeli.nonota='" & nonota & "'"

                Case "penjualan"
                    ' --- query penjualan (isi sesuai tabel kamu)
                    sql =
                        "SELECT zstok.namabrg, zjual.nonota, zjual.tgl, zjual.kodekust, zjual.kodesls, " &
                        "zjual.tgltempo, zjual.disc1, zjual.disc2, zjual.disc3, zjual.hdisc1, zjual.hdisc2, zjual.hdisc3, " &
                        "zjual.ppn, zjual.hppn, zjual.lainnya, zjual.nilai, " &
                        "zjualm.kodebrg, zjualm.jlh1, zjualm.jlh2, zjualm.jlh3, " &
                        "zjualm.disca, zjualm.discb, zjualm.discc, zjualm.discrp, zjualm.jumlah, " &
                        "zstok.satuan1, zstok.satuan2, zstok.satuan3, zjualm.harga, " &
                        "zkustomer.namakust, zsales.namasls, zkustomer.alamat " &
                        "FROM (((dbkita.zjual zjual " &
                        "INNER JOIN dbkita.zjualm zjualm ON zjual.nonota=zjualm.nonota) " &
                        "LEFT JOIN dbkita.zstok zstok ON zjualm.kodebrg=zstok.kodebrg) " &
                        "LEFT JOIN dbkita.zkustomer zkustomer ON zjual.kodekust=zkustomer.kodekust) " &
                        "LEFT JOIN dbkita.zsales zsales ON zjual.kodesls=zsales.kodesls " &
                        "WHERE zjual.nonota='" & nonota & "'"

                Case "mutasi"
                    ' --- query penjualan (isi sesuai tabel kamu)
                    sql =
                        "SELECT zmutasi.nonota, zmutasi.tgl, zmutasi.kodegd1, zmutasi.kodegd2, " &
                        "g1.namagd AS namagd1, g2.namagd AS namagd2, " &
                        "zmutasim.kodebrg, zmutasim.jlh1, zmutasim.jlh2, zmutasim.jlh3, " &
                        "zstok.namabrg, zstok.satuan1, zstok.satuan2, zstok.satuan3 " &
                        "FROM ((dbkita.zmutasi zmutasi " &
                        "INNER JOIN dbkita.zmutasim zmutasim ON zmutasi.nonota=zmutasim.nonota) " &
                        "LEFT JOIN dbkita.zstok zstok ON zmutasim.kodebrg=zstok.kodebrg) " &
                        "JOIN dbkita.zgudang g1 ON zmutasi.kodegd1 = g1.kodegd " &
                        "JOIN dbkita.zgudang g2 ON zmutasi.kodegd2 = g2.kodegd " &
                        "WHERE zmutasi.nonota='" & nonota & "'"

                Case "penyesuaian"
                    ' --- query penjualan (isi sesuai tabel kamu)
                    sql =
                        "SELECT zpenyesuaian.nonota, zpenyesuaian.tgl, zpenyesuaian.kodegd, zgudang.namagd, " &
                        "zpenyesuaianm.kodebrg, zpenyesuaianm.jlh1, zpenyesuaianm.jlh2, zpenyesuaianm.jlh3, " &
                        "zstok.namabrg, zstok.satuan1, zstok.satuan2, zstok.satuan3 " &
                        "FROM ((dbkita.zpenyesuaian zpenyesuaian " &
                        "INNER JOIN dbkita.zpenyesuaianm zpenyesuaianm ON zpenyesuaian.nonota=zpenyesuaianm.nonota) " &
                        "LEFT JOIN dbkita.zstok zstok ON zpenyesuaianm.kodebrg=zstok.kodebrg) " &
                        "LEFT JOIN dbkita.zgudang zgudang ON zpenyesuaian.kodegd=zgudang.kodegd " &
                        "WHERE zpenyesuaian.nonota='" & nonota & "'"
            End Select

            Dim da As New OdbcDataAdapter(sql, Conn)
            Dim dt As New DataTable
            da.Fill(dt)

            If dt.Rows.Count = 0 Then
                MessageBox.Show("Data nota tidak ditemukan!")
                Exit Sub
            End If

            ' --- Jika ingin tambahkan padding row minimal 10 ---
            If dt.Rows.Count < 9 Then
                Dim header As DataRow = dt.Rows(0)

                ' daftar kolom header yang selalu diisi
                Dim headerCols As String() = {
                    "nonota", "tgl", "tgltempo", "nofaktur",
                    "kodesup", "kodekust", "kodesls",
                    "namasup", "namakust", "namasls", "alamat",
                    "kodegd1", "kodegd2", "namagd1", "namagd2",
                    "kodegd", "namagd",
                    "disc1", "disc2", "disc3",
                    "hdisc1", "hdisc2", "hdisc3",
                    "ppn", "hppn", "lainnya", "nilai"
                }

                For i As Integer = 1 To (9 - dt.Rows.Count)
                    Dim row As DataRow = dt.NewRow()

                    ' loop semua kolom di DataTable
                    For Each col As DataColumn In dt.Columns
                        If headerCols.Contains(col.ColumnName.ToLower()) Then
                            ' copy nilai dari row pertama (header)
                            row(col.ColumnName) = header(col.ColumnName)
                        Else
                            ' sisanya biarkan kosong
                            row(col.ColumnName) = DBNull.Value
                        End If
                    Next

                    dt.Rows.Add(row)
                Next
            End If

            ' Buat dataset
            Dim ds As New NotaDt()

            Select Case jenis.ToLower()
                Case "pembelian"
                    ds.Tables("NotaPembelian").Merge(dt)
                Case "rpembelian"
                    ds.Tables("NotaRPembelian").Merge(dt)
                Case "penjualan"
                    ds.Tables("NotaPenjualan").Merge(dt)
                Case "mutasi"
                    ds.Tables("NotaMutasi").Merge(dt)
                Case "penyesuaian"
                    ds.Tables("NotaPenyesuaian").Merge(dt)
            End Select


            ' Pilih report sesuai jenis
            Dim rpt As New ReportDocument()
            Select Case jenis.ToLower()
                Case "pembelian"
                    rpt.Load(Application.StartupPath & "\Report\NotaPembelian.rpt")
                Case "rpembelian"
                    rpt.Load(Application.StartupPath & "\Report\NotaReturPembelian.rpt")
                Case "penjualan"
                    rpt.Load(Application.StartupPath & "\Report\NotaPenjualan.rpt")
                Case "mutasi"
                    rpt.Load(Application.StartupPath & "\Report\NotaMutasi.rpt")
                Case "penyesuaian"
                    rpt.Load(Application.StartupPath & "\Report\NotaPenyesuaian.rpt")
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
