Imports System.Data.Odbc

Module MAkunSimpan
    Public KodeLama As String = ""   ' menyimpan KDUSER lama jika terjadi error

    Public Sub SimpanAkun(ByVal kdUser As String,
                          ByVal nmUser As String,
                          ByVal passUser As String,
                          ByVal status As String)

        BukaKoneksi() ' pastikan Conn aktif dari modul koneksi global

        Try

            If kdUser = "" Then Throw New Exception("Kode User tidak boleh kosong!")
            ' Jika mode UBAH, hapus dulu data lama
            If status = "UBAH" Then
                Using cmdDel As New OdbcCommand("DELETE FROM zusers WHERE kodeuser = ?", Conn)
                    cmdDel.Parameters.AddWithValue("@kodeuser", kdUser)
                    cmdDel.ExecuteNonQuery()
                End Using
            End If

            ' Insert data baru
            Using cmd As New OdbcCommand("INSERT INTO zusers (kodeuser, username, kunci) VALUES (?, ?, ?)", Conn)
                cmd.Parameters.AddWithValue("@kodeuser", kdUser)
                cmd.Parameters.AddWithValue("@username", nmUser)
                cmd.Parameters.AddWithValue("@kunci", passUser)
                cmd.ExecuteNonQuery()
            End Using

        Catch ex As Exception
            KodeLama = kdUser
            MsgBox("Gagal menyimpan data akun: " & ex.Message, vbCritical)
        End Try
    End Sub


    Public Sub HapusAkun(ByVal kdUser As String)
        BukaKoneksi() ' pastikan Conn aktif

        Try
            Using cmd As New OdbcCommand("DELETE FROM zusers WHERE kodeuser", Conn)
                cmd.Parameters.AddWithValue("@kodeuser", kdUser)
                cmd.ExecuteNonQuery()
            End Using
        Catch ex As Exception
            MsgBox("Gagal menghapus data akun: " & ex.Message, vbCritical)
        End Try
    End Sub
End Module
