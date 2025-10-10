Imports System.Data.Odbc

Module MMutSimpan
    Public KodeLama As String = ""   ' menyimpan nonota lama jika terjadi error

    Public Sub SimpanMutasi(ByVal nomor As Integer,
                               ByVal dict As Dictionary(Of String, Control),
                               ByVal status As String)

        Try
            Dim tgl As String = CType(dict("tmuTANGGAL"), DateTimePicker).Value.ToString("yyyy-MM-dd")
            Dim nonota As String = CType(dict("tmuNONOTA"), TextBox).Text.Trim()
            Dim cb1 As ComboBox = CType(dict("cbmuGD1"), ComboBox)
            Dim cb2 As ComboBox = CType(dict("cbmuGD2"), ComboBox)

            ' Ambil hanya kode gudang sebelum spasi
            Dim gd1 As String = ""
            Dim gd2 As String = ""

            Dim cb1Text As String = If(cb1.SelectedItem IsNot Nothing, cb1.SelectedItem.ToString(), cb1.Text)
            Dim cb2Text As String = If(cb2.SelectedItem IsNot Nothing, cb2.SelectedItem.ToString(), cb2.Text)

            If Not String.IsNullOrWhiteSpace(cb1Text) Then
                gd1 = cb1Text.Split(" "c)(0)
            End If
            If Not String.IsNullOrWhiteSpace(cb2Text) Then
                gd2 = cb2Text.Split(" "c)(0)
            End If

            Dim grid As DataGridView = CType(dict("GRID"), DataGridView)


            ' --- Validasi ---
            If nonota = "" Then Throw New Exception("No Nota tidak boleh kosong!")

            ' --- Buka koneksi ---
            BukaKoneksi()
            Trans = Conn.BeginTransaction()

            ' --- Jika UBAH: hapus data lama dulu ---
            If status = "ubah" Then
                KodeLama = nonota ' simpan kode lama
                Dim sqldel1 As String = "DELETE FROM zmutasim WHERE nonota = ?"
                Using CmdDel1 As New OdbcCommand(sqldel1, Conn, Trans)
                    CmdDel1.Parameters.AddWithValue("@nonota", KodeLama)
                    CmdDel1.ExecuteNonQuery()
                End Using

                Dim sqldel2 As String = "DELETE FROM zmutasi WHERE nonota = ?"
                Using CmdDel2 As New OdbcCommand(sqldel2, Conn, Trans)
                    CmdDel2.Parameters.AddWithValue("@nonota", KodeLama)
                    CmdDel2.ExecuteNonQuery()
                End Using
            End If

            ' --- Simpan Header ---
            Dim sql As String = "INSERT INTO zmutasi (tgl, nonota, kodegd1, kodegd2) " &
                                "VALUES (?, ?, ?, ?)"
            Using Cmd As New OdbcCommand(sql, Conn, Trans)
                Cmd.Parameters.AddWithValue("@tgl", tgl)
                Cmd.Parameters.AddWithValue("@nonota", nonota)
                Cmd.Parameters.AddWithValue("@kodegd1", gd1)
                Cmd.Parameters.AddWithValue("@kodegd2", gd2)
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


                Dim sqldet As String = "INSERT INTO zmutasim " &
                    "(nonota, kodebrg, kodegd1, kodegd2, jlh1, jlh2, jlh3, operator) " &
                    "VALUES (?, ?, ?, ?, ?, ?, ?, ?)"

                Using CmdDet As New OdbcCommand(sqldet, Conn, Trans)
                    CmdDet.Parameters.AddWithValue("@nonota", nonota)
                    CmdDet.Parameters.AddWithValue("@kodebrg", kodebrg)
                    CmdDet.Parameters.AddWithValue("@kodegd1", gd1)
                    CmdDet.Parameters.AddWithValue("@kodegd2", gd2)
                    CmdDet.Parameters.AddWithValue("@jlh1", jlh1)
                    CmdDet.Parameters.AddWithValue("@jlh2", jlh2)
                    CmdDet.Parameters.AddWithValue("@jlh3", jlh3)
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

    Public Sub HapusMutasi(ByVal nonota As String,
                          ByVal dict As Dictionary(Of String, Control))

        If String.IsNullOrEmpty(nonota) Then
            Throw New Exception("No Nota kosong, tidak bisa dihapus.")
        End If

        ' --- Buka koneksi & transaksi ---
        BukaKoneksi()
        Trans = Conn.BeginTransaction()

        Try
            ' hapus detail
            Using cmdDet As New OdbcCommand("DELETE FROM zmutasim WHERE nonota = ?", Conn, Trans)
                cmdDet.Parameters.AddWithValue("@nonota", nonota)
                cmdDet.ExecuteNonQuery()
            End Using

            ' hapus header
            Using cmdHead As New OdbcCommand("DELETE FROM zmutasi WHERE nonota = ?", Conn, Trans)
                cmdHead.Parameters.AddWithValue("@nonota", nonota)
                cmdHead.ExecuteNonQuery()
            End Using

            Trans.Commit()
            Conn.Close()

            ' bersihkan form/tab setelah hapus
            CType(dict("tmuNONOTA"), TextBox).Clear()
            CType(dict("tmuNMSUP"), TextBox).Clear()
            CType(dict("cbmuGD1"), ComboBox).SelectedIndex = -1
            CType(dict("cbmuGD2"), ComboBox).SelectedIndex = -1

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
