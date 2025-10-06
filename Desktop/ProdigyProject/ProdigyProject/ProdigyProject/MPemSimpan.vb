Imports System.Data.Odbc

Module MPemSimpan
    Public KodeLama As String = ""   ' menyimpan nonota lama jika terjadi error

    Public Sub SimpanPembelian(ByVal nomor As Integer,
                               ByVal dict As Dictionary(Of String, Control),
                               ByVal status As String)

        Try
            Dim tgl As String = CType(dict("tpmTANGGAL"), DateTimePicker).Value.ToString("yyyy-MM-dd")
            Dim nonota As String = CType(dict("tpmNONOTA"), TextBox).Text.Trim()
            Dim tempo As String = CType(dict("tpmTEMPO"), DateTimePicker).Value.ToString("yyyy-MM-dd")
            Dim ket As String = CType(dict("tpmKET"), TextBox).Text.Trim()
            Dim kodesup As String = CType(dict("tpmKDSUP"), TextBox).Text.Trim()
            Dim namasup As String = CType(dict("tpmNMSUP"), TextBox).Text.Trim()
            Dim alamat As String = CType(dict("tpmALAMAT"), TextBox).Text.Trim()
            Dim grid As DataGridView = CType(dict("GRID"), DataGridView)

            Dim adisk1 As Double = CType(dict("tpmADISK1"), TextBox).Text.Trim()
            Dim ndisk1 As Double = CType(dict("tpmNDISK1"), TextBox).Text.Trim()
            Dim adisk2 As Double = CType(dict("tpmADISK2"), TextBox).Text.Trim()
            Dim ndisk2 As Double = CType(dict("tpmNDISK2"), TextBox).Text.Trim()
            Dim adisk3 As Double = CType(dict("tpmADISK3"), TextBox).Text.Trim()
            Dim ndisk3 As Double = CType(dict("tpmNDISK3"), TextBox).Text.Trim()
            Dim lain As Double = CType(dict("tpmLAIN"), TextBox).Text.Trim()
            Dim appn As Double = CType(dict("tpmAPPN"), TextBox).Text.Trim()
            Dim nppn As Double = CType(dict("tpmNPPN"), TextBox).Text.Trim()
            Dim total As Double = CType(dict("tpmTOTAL"), TextBox).Text.Trim()



            ' --- Validasi ---
            If nonota = "" Then Throw New Exception("No Nota tidak boleh kosong!")
            If kodesup = "" Then Throw New Exception("Supplier belum dipilih!")

            ' --- Buka koneksi ---
            BukaKoneksi()
            Trans = Conn.BeginTransaction()

            ' --- Jika UBAH: hapus data lama dulu ---
            If status = "ubah" Then
                KodeLama = nonota ' simpan kode lama
                Dim sqldel1 As String = "DELETE FROM zbelim WHERE nonota = ?"
                Using CmdDel1 As New OdbcCommand(sqldel1, Conn, Trans)
                    CmdDel1.Parameters.AddWithValue("@nonota", KodeLama)
                    CmdDel1.ExecuteNonQuery()
                End Using

                Dim sqldel2 As String = "DELETE FROM zbeli WHERE nonota = ?"
                Using CmdDel2 As New OdbcCommand(sqldel2, Conn, Trans)
                    CmdDel2.Parameters.AddWithValue("@nonota", KodeLama)
                    CmdDel2.ExecuteNonQuery()
                End Using
            End If

            ' --- Simpan Header ---
            Dim sql As String = "INSERT INTO zbeli (tgl, nonota, tgltempo, ket, kodesup, nilai, lunas, disc1, hdisc1, disc2, hdisc2, disc3, hdisc3, ppn, hppn, lainnya) " &
                                "VALUES (?, ?, ?, ?, ?, ?, 0, ?, ?, ?, ?, ?, ?, ?, ?, ?)"
            Using Cmd As New OdbcCommand(sql, Conn, Trans)
                Cmd.Parameters.AddWithValue("@tgl", tgl)
                Cmd.Parameters.AddWithValue("@nonota", nonota)
                Cmd.Parameters.AddWithValue("@tgltempo", tempo)
                Cmd.Parameters.AddWithValue("@ket", ket)
                Cmd.Parameters.AddWithValue("@kodesup", kodesup)
                Cmd.Parameters.AddWithValue("@nilai", total)
                Cmd.Parameters.AddWithValue("@disc1", adisk1)
                Cmd.Parameters.AddWithValue("@hdisc1", ndisk1)
                Cmd.Parameters.AddWithValue("@disc2", adisk2)
                Cmd.Parameters.AddWithValue("@hdisc2", ndisk2)
                Cmd.Parameters.AddWithValue("@disc3", adisk3)
                Cmd.Parameters.AddWithValue("@hdisc3", ndisk3)
                Cmd.Parameters.AddWithValue("@ppn", appn)
                Cmd.Parameters.AddWithValue("@hppn", nppn)
                Cmd.Parameters.AddWithValue("@lainnya", lain)
                Cmd.ExecuteNonQuery()
            End Using

            ' --- Simpan Detail ---
            For Each row As DataGridViewRow In grid.Rows
                If row.IsNewRow Then Continue For

                Dim kodebrg As String = If(row.Cells("KodeBrg").Value, "").ToString.Trim()
                If kodebrg = "" Then Continue For

                Dim kodegd As String = If(row.Cells("KodeGudang").Value, "").ToString.Trim()
                Dim jlh1 As Double = Val(If(row.Cells("Jlh1").Value, 0))
                Dim jlh2 As Double = Val(If(row.Cells("Jlh2").Value, 0))
                Dim jlh3 As Double = Val(If(row.Cells("Jlh3").Value, 0))
                Dim harga As Double = Val(If(row.Cells("Harga").Value, 0))
                Dim disca As Double = Val(If(row.Cells("Disca").Value, 0))
                Dim discb As Double = Val(If(row.Cells("Discb").Value, 0))
                Dim discc As Double = Val(If(row.Cells("Discc").Value, 0))
                Dim discrp As Double = Val(If(row.Cells("DiscRp").Value, 0))
                Dim jumlah As Double = Val(If(row.Cells("Jumlah").Value, 0))

                Dim sqldet As String = "INSERT INTO zbelim " &
                    "(nonota, kodebrg, kodegd, jlh1, jlh2, jlh3, harga, disca, hdisca, discb, hdiscb, discc, hdiscc, discrp, jumlah, operator) " &
                    "VALUES (?, ?, ?, ?, ?, ?, ?, ?, 0, ?, 0, ?, 0, ?, ?, ?)"

                Using CmdDet As New OdbcCommand(sqldet, Conn, Trans)
                    CmdDet.Parameters.AddWithValue("@nonota", nonota)
                    CmdDet.Parameters.AddWithValue("@kodebrg", kodebrg)
                    CmdDet.Parameters.AddWithValue("@kodegd", kodegd)
                    CmdDet.Parameters.AddWithValue("@jlh1", jlh1)
                    CmdDet.Parameters.AddWithValue("@jlh2", jlh2)
                    CmdDet.Parameters.AddWithValue("@jlh3", jlh3)
                    CmdDet.Parameters.AddWithValue("@harga", harga)
                    CmdDet.Parameters.AddWithValue("@disca", disca)
                    CmdDet.Parameters.AddWithValue("@discb", discb)
                    CmdDet.Parameters.AddWithValue("@discc", discc)
                    CmdDet.Parameters.AddWithValue("@discrp", discrp)
                    CmdDet.Parameters.AddWithValue("@jumlah", jumlah)
                    CmdDet.Parameters.AddWithValue("@operator", Environment.UserName)
                    CmdDet.ExecuteNonQuery()
                End Using
            Next

            ' --- Commit ---
            Trans.Commit()
            Conn.Close()

        Catch ex As Exception
            Try
                If Trans IsNot Nothing Then Trans.Rollback()
                If Conn IsNot Nothing AndAlso Conn.State = ConnectionState.Open Then Conn.Close()
            Catch
            End Try
            Throw New Exception("Error Simpan: " & ex.Message)
        End Try
    End Sub

    Public Sub HapusPembelian(ByVal nonota As String,
                          ByVal dict As Dictionary(Of String, Control))

        If String.IsNullOrEmpty(nonota) Then
            Throw New Exception("No Nota kosong, tidak bisa dihapus.")
        End If

        ' --- Buka koneksi & transaksi ---
        BukaKoneksi()
        Trans = Conn.BeginTransaction()

        Try
            ' hapus detail
            Using cmdDet As New OdbcCommand("DELETE FROM zbelim WHERE nonota = ?", Conn, Trans)
                cmdDet.Parameters.AddWithValue("@nonota", nonota)
                cmdDet.ExecuteNonQuery()
            End Using

            ' hapus header
            Using cmdHead As New OdbcCommand("DELETE FROM zbeli WHERE nonota = ?", Conn, Trans)
                cmdHead.Parameters.AddWithValue("@nonota", nonota)
                cmdHead.ExecuteNonQuery()
            End Using

            Trans.Commit()
            Conn.Close()

            ' bersihkan form/tab setelah hapus
            CType(dict("tpmNONOTA"), TextBox).Clear()
            CType(dict("tpmNMSUP"), TextBox).Clear()
            CType(dict("tpmKDSUP"), TextBox).Clear()
            CType(dict("tpmALAMAT"), TextBox).Clear()
            CType(dict("tpmKET"), TextBox).Clear()
            CType(dict("GRID"), DataGridView).Rows.Clear()
            CType(dict("tpmSUBTOTAL"), TextBox).Clear()
            CType(dict("tpmTOTAL"), TextBox).Clear()


        Catch ex As Exception
            Try
                If Trans IsNot Nothing Then Trans.Rollback()
                If Conn IsNot Nothing AndAlso Conn.State = ConnectionState.Open Then Conn.Close()
            Catch
            End Try
            Throw New Exception("Error Hapus: " & ex.Message)
        End Try
    End Sub

End Module
