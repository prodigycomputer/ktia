Imports System.Data.Odbc
Imports CrystalDecisions.CrystalReports.Engine

Module ModLaporan

    Public HeaderNamaToko As String = "Prodigy Computer"
    Public HeaderAlamat As String = "Gajah Mada, Jl. Setia Budi No 21/115, Pontianak"

    Public Sub LoadLaporan(ByVal jenis As String, ByVal nonota As String, ByVal Param As Dictionary(Of String, Object), ByVal viewer As CrystalDecisions.Windows.Forms.CrystalReportViewer)
        Try
            Call BukaKoneksi()

            Dim sql As String = ""
            Dim cmd As OdbcCommand = Nothing

            Select Case jenis.ToLower()
                Case "laporanrekapbeli"
                    Dim kodesup1 As String = Param("kodesup1").ToString().Trim()
                    Dim kodesup2 As String = Param("kodesup2").ToString().Trim()
                    Dim tgl1 As String = Format(CDate(Param("tgl1")), "yyyy-MM-dd")
                    Dim tgl2 As String = Format(CDate(Param("tgl2")), "yyyy-MM-dd")

                    sql =
                        "SELECT zbeli.nonota, zbeli.tgl, zbeli.tgltempo, zsupplier.namasup, zbeli.nilai " &
                        "FROM zbeli " &
                        "LEFT JOIN zsupplier ON zbeli.kodesup = zsupplier.kodesup "

                    ' --- daftar kondisi WHERE ---
                    Dim whereList As New List(Of String)

                    ' === Filter tanggal ===
                    If tgl1 <> "" AndAlso tgl2 <> "" Then
                        If tgl1 = tgl2 Then
                            whereList.Add("zbeli.tgl = ?")
                        Else
                            whereList.Add("zbeli.tgl BETWEEN ? AND ?")
                        End If
                    End If

                    ' === Filter supplier ===
                    If kodesup1 <> "" AndAlso kodesup2 <> "" Then
                        If kodesup1 = kodesup2 Then
                            whereList.Add("zbeli.kodesup = ?")
                        Else
                            whereList.Add("zbeli.kodesup BETWEEN ? AND ?")
                        End If
                    ElseIf kodesup1 <> "" AndAlso kodesup2 = "" Then
                        whereList.Add("zbeli.kodesup = ?")
                    ElseIf kodesup2 <> "" AndAlso kodesup1 = "" Then
                        whereList.Add("zbeli.kodesup = ?")
                    End If

                    ' --- gabung WHERE ---
                    If whereList.Count > 0 Then
                        sql &= " WHERE " & String.Join(" AND ", whereList)
                    End If

                    sql &= " ORDER BY zbeli.nonota ASC"

                    ' --- Eksekusi query ---
                    cmd = New OdbcCommand(sql, Conn)

                    ' === Binding parameter ===
                    If tgl1 <> "" AndAlso tgl2 <> "" Then
                        If tgl1 = tgl2 Then
                            cmd.Parameters.AddWithValue("", tgl1)
                        Else
                            cmd.Parameters.AddWithValue("", tgl1)
                            cmd.Parameters.AddWithValue("", tgl2)
                        End If
                    End If

                    If kodesup1 <> "" AndAlso kodesup2 <> "" Then
                        If kodesup1 = kodesup2 Then
                            cmd.Parameters.AddWithValue("", kodesup1)
                        Else
                            cmd.Parameters.AddWithValue("", kodesup1)
                            cmd.Parameters.AddWithValue("", kodesup2)
                        End If
                    ElseIf kodesup1 <> "" AndAlso kodesup2 = "" Then
                        cmd.Parameters.AddWithValue("", kodesup1)
                    ElseIf kodesup2 <> "" AndAlso kodesup1 = "" Then
                        cmd.Parameters.AddWithValue("", kodesup2)
                    End If


                Case "laporanrincibeli"
                    Dim kodesup1 As String = Param("kodesup1").ToString().Trim()
                    Dim kodesup2 As String = Param("kodesup2").ToString().Trim()
                    Dim tgl1 As String = Format(CDate(Param("tgl1")), "yyyy-MM-dd")
                    Dim tgl2 As String = Format(CDate(Param("tgl2")), "yyyy-MM-dd")

                    sql =
                        "SELECT zbelim.nonota, zbeli.tgl, zbeli.kodesup, zsupplier.namasup, " &
                        "zstok.kodebrg, zstok.namabrg, zbelim.jlh1, zbelim.jlh2, zbelim.jlh3, " &
                        "zbelim.harga, zbelim.jumlah " &
                        "FROM zbelim " &
                        "LEFT JOIN zbeli ON zbelim.nonota = zbeli.nonota " &
                        "LEFT JOIN zsupplier ON zbeli.kodesup = zsupplier.kodesup " &
                        "LEFT JOIN zstok ON zbelim.kodebrg = zstok.kodebrg "

                    ' --- daftar kondisi WHERE ---
                    Dim whereList As New List(Of String)

                    ' === Filter tanggal ===
                    If tgl1 <> "" AndAlso tgl2 <> "" Then
                        If tgl1 = tgl2 Then
                            whereList.Add("zbeli.tgl = ?")
                        Else
                            whereList.Add("zbeli.tgl BETWEEN ? AND ?")
                        End If
                    End If

                    ' === Filter supplier ===
                    If kodesup1 <> "" AndAlso kodesup2 <> "" Then
                        If kodesup1 = kodesup2 Then
                            whereList.Add("zbeli.kodesup = ?")
                        Else
                            whereList.Add("zbeli.kodesup BETWEEN ? AND ?")
                        End If
                    ElseIf kodesup1 <> "" AndAlso kodesup2 = "" Then
                        whereList.Add("zbeli.kodesup = ?")
                    ElseIf kodesup2 <> "" AndAlso kodesup1 = "" Then
                        whereList.Add("zbeli.kodesup = ?")
                    End If

                    ' --- gabung WHERE ---
                    If whereList.Count > 0 Then
                        sql &= " WHERE " & String.Join(" AND ", whereList)
                    End If

                    sql &= " ORDER BY zbeli.nonota ASC"

                    ' --- Eksekusi query ---
                    cmd = New OdbcCommand(sql, Conn)

                    ' === Binding parameter ===
                    If tgl1 <> "" AndAlso tgl2 <> "" Then
                        If tgl1 = tgl2 Then
                            cmd.Parameters.AddWithValue("", tgl1)
                        Else
                            cmd.Parameters.AddWithValue("", tgl1)
                            cmd.Parameters.AddWithValue("", tgl2)
                        End If
                    End If

                    If kodesup1 <> "" AndAlso kodesup2 <> "" Then
                        If kodesup1 = kodesup2 Then
                            cmd.Parameters.AddWithValue("", kodesup1)
                        Else
                            cmd.Parameters.AddWithValue("", kodesup1)
                            cmd.Parameters.AddWithValue("", kodesup2)
                        End If
                    ElseIf kodesup1 <> "" AndAlso kodesup2 = "" Then
                        cmd.Parameters.AddWithValue("", kodesup1)
                    ElseIf kodesup2 <> "" AndAlso kodesup1 = "" Then
                        cmd.Parameters.AddWithValue("", kodesup2)
                    End If

                Case "laporanrekapjual"
                    Dim kodekust1 As String = Param("kodekust1").ToString().Trim()
                    Dim kodekust2 As String = Param("kodekust2").ToString().Trim()
                    Dim tgl1 As String = Format(CDate(Param("tgl1")), "yyyy-MM-dd")
                    Dim tgl2 As String = Format(CDate(Param("tgl2")), "yyyy-MM-dd")
                    sql =
                        "SELECT zjual.nonota, zjual.tgl, zjual.tgltempo, zkustomer.namakust, zsales.namasls, zjual.nilai " &
                        "FROM zjual " &
                        "LEFT JOIN zkustomer ON zjual.kodekust = zkustomer.kodekust " &
                        "LEFT JOIN zsales ON zjual.kodesls = zsales.kodesls "

                    ' --- daftar kondisi WHERE ---
                    Dim whereList As New List(Of String)

                    ' === Filter tanggal ===
                    If tgl1 <> "" AndAlso tgl2 <> "" Then
                        If tgl1 = tgl2 Then
                            whereList.Add("zjual.tgl = ?")
                        Else
                            whereList.Add("zjual.tgl BETWEEN ? AND ?")
                        End If
                    End If

                    ' === Filter supplier ===
                    If kodekust1 <> "" AndAlso kodekust2 <> "" Then
                        If kodekust1 = kodekust2 Then
                            whereList.Add("zjual.kodekust = ?")
                        Else
                            whereList.Add("zjual.kodekust BETWEEN ? AND ?")
                        End If
                    ElseIf kodekust1 <> "" AndAlso kodekust2 = "" Then
                        whereList.Add("zjual.kodekust = ?")
                    ElseIf kodekust2 <> "" AndAlso kodekust1 = "" Then
                        whereList.Add("zjual.kodekust = ?")
                    End If

                    ' --- gabung WHERE ---
                    If whereList.Count > 0 Then
                        sql &= " WHERE " & String.Join(" AND ", whereList)
                    End If

                    sql &= " ORDER BY zjual.nonota ASC"

                    ' --- Eksekusi query ---
                    cmd = New OdbcCommand(sql, Conn)

                    ' === Binding parameter ===
                    If tgl1 <> "" AndAlso tgl2 <> "" Then
                        If tgl1 = tgl2 Then
                            cmd.Parameters.AddWithValue("", tgl1)
                        Else
                            cmd.Parameters.AddWithValue("", tgl1)
                            cmd.Parameters.AddWithValue("", tgl2)
                        End If
                    End If

                    If kodekust1 <> "" AndAlso kodekust2 <> "" Then
                        If kodekust1 = kodekust2 Then
                            cmd.Parameters.AddWithValue("", kodekust1)
                        Else
                            cmd.Parameters.AddWithValue("", kodekust1)
                            cmd.Parameters.AddWithValue("", kodekust2)
                        End If
                    ElseIf kodekust1 <> "" AndAlso kodekust2 = "" Then
                        cmd.Parameters.AddWithValue("", kodekust1)
                    ElseIf kodekust2 <> "" AndAlso kodekust1 = "" Then
                        cmd.Parameters.AddWithValue("", kodekust2)
                    End If

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

            Dim da As New OdbcDataAdapter(Cmd)
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
                Case "laporanrekapbeli"
                    ds.Tables("LaporanRekapBeli").Merge(dt)

                Case "laporanrincibeli"
                    ds.Tables("LaporanRinciBeli").Merge(dt)

                Case "laporanrekapjual"
                    ds.Tables("LaporanJual").Merge(dt)

                Case "laporanmutasi"
                    ds.Tables("LaporanMutasi").Merge(dt)

                Case "laporanpenyesuaian"
                    ds.Tables("LaporanPenyesuaian").Merge(dt)
            End Select


            ' Pilih report sesuai jenis
            Dim rpt As New ReportDocument()
            Select Case jenis.ToLower()
                Case "laporanrekapbeli"
                    rpt.Load(Application.StartupPath & "\Laporan\LaporanRekapPembelian.rpt")

                Case "laporanrincibeli"
                    rpt.Load(Application.StartupPath & "\Laporan\LaporanRinciPembelian.rpt")

                Case "laporanrekapjual"
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
