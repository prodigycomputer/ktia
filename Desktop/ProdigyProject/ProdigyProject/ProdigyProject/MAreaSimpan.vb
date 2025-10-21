Imports System.Data.Odbc

Module MAreaSimpan
    Public KodeLama As String = ""

    Public Sub SimpanArea(ByVal kdArea As String,
                          ByVal nmArea As String,
                          ByVal status As String)

        BukaKoneksi()
        Dim Trans As OdbcTransaction = Nothing


        Try

            If kdArea = "" Then Throw New Exception("Kode Area tidak boleh kosong!")
            ' Jika mode UBAH, hapus dulu data lama
            If status = "UBAH" Then
                Using cmdDel As New OdbcCommand("DELETE FROM zarea WHERE kodear = ?", Conn)
                    cmdDel.Parameters.AddWithValue("@kodear", kdArea)
                    cmdDel.ExecuteNonQuery()
                End Using
            End If

            ' Insert data baru
            Using cmd As New OdbcCommand("INSERT INTO zarea (kodear, namaar) VALUES (?, ?)", Conn)
                cmd.Parameters.AddWithValue("@kodear", kdArea)
                cmd.Parameters.AddWithValue("@namaar", nmArea)
                cmd.ExecuteNonQuery()
            End Using

        Catch ex As Exception
            KodeLama = kdArea
            MsgBox("Gagal menyimpan data area: " & ex.Message, vbCritical)
        End Try
    End Sub


    Public Sub HapusArea(ByVal kdArea As String)
        BukaKoneksi() ' pastikan Conn aktif

        Try
            Using cmd As New OdbcCommand("DELETE FROM zarea WHERE kodear", Conn)
                cmd.Parameters.AddWithValue("@kodear", kdArea)
                cmd.ExecuteNonQuery()
            End Using
        Catch ex As Exception
            MsgBox("Gagal menghapus data area: " & ex.Message, vbCritical)
        End Try
    End Sub
End Module
