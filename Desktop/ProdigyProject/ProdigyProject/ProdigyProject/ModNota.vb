Imports System.Data.Odbc
Imports CrystalDecisions.CrystalReports.Engine

Module ModNota

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

                Case "penjualan"
                    ' --- query penjualan (isi sesuai tabel kamu)
                    sql =
                        "SELECT zjual.nonota, zjual.tgl, zjual.kodecust, " &
                        "zjualm.kodebrg, zjualm.jumlah, zjualm.harga, zstok.namabrg, " &
                        "zcust.namacust, zcust.alamat " &
                        "FROM ((dbkita.zjual zjual " &
                        "INNER JOIN dbkita.zjualm zjualm ON zjual.nonota=zjualm.nonota) " &
                        "LEFT JOIN dbkita.zstok zstok ON zjualm.kodebrg=zstok.kodebrg) " &
                        "LEFT JOIN dbkita.zcust zcust ON zjual.kodecust=zcust.kodecust " &
                        "WHERE zjual.nonota='" & nonota & "'"
            End Select

            Dim da As New OdbcDataAdapter(sql, Conn)
            Dim dt As New DataTable
            da.Fill(dt)

            If dt.Rows.Count = 0 Then
                MessageBox.Show("Data nota tidak ditemukan!")
                Exit Sub
            End If

            ' --- Jika ingin tambahkan padding row minimal 10 ---
            If dt.Rows.Count < 10 Then
                Dim header As DataRow = dt.Rows(0)

                ' daftar kolom header yang selalu diisi
                Dim headerCols As String() = {
                    "nonota", "tgl", "tgltempo",
                    "kodesup", "kodekust",
                    "namasup", "namakust", "alamat",
                    "disc1", "disc2", "disc3",
                    "hdisc1", "hdisc2", "hdisc3",
                    "ppn", "hppn", "lainnya", "nilai"
                }

                For i As Integer = 1 To (10 - dt.Rows.Count)
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
            ds.Tables(0).Merge(dt)

            ' Pilih report sesuai jenis
            Dim rpt As New ReportDocument()
            Select Case jenis.ToLower()
                Case "pembelian"
                    rpt.Load(Application.StartupPath & "\NotaPembelian.rpt")
                Case "penjualan"
                    rpt.Load(Application.StartupPath & "\NotaPenjualan.rpt")
            End Select

            rpt.SetDataSource(ds)
            viewer.ReportSource = rpt
            viewer.Refresh()

        Catch ex As Exception
            MessageBox.Show("Gagal load report: " & ex.Message)
        End Try
    End Sub

End Module
