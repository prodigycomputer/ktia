Imports System.Data.Odbc

Module MUserAkses
    ' === Variabel global untuk menyimpan user login aktif ===
    Public KodeUserLogin As String = ""
    Public NamaUserLogin As String = ""

    ' === Fungsi untuk mengambil data akses berdasarkan user dan idmenu ===
    Public Function GetAkses(ByVal kodeUser As String, ByVal idMenu As String) As Dictionary(Of String, Boolean)
        Dim akses As New Dictionary(Of String, Boolean) From {
            {"tambah", False},
            {"ubah", False},
            {"hapus", False},
            {"adaAkses", False}
        }

        Try
            If Conn Is Nothing Then
                BukaKoneksi()
            ElseIf Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If

            Dim sql As String = "SELECT tambah, ubah, hapus FROM zakses WHERE kodeuser=? AND idmenu=?"
            Using cmd As New OdbcCommand(sql, Conn)
                cmd.Parameters.AddWithValue("@1", kodeUser)
                cmd.Parameters.AddWithValue("@2", idMenu)

                Using rd As OdbcDataReader = cmd.ExecuteReader()
                    If rd.Read() Then
                        akses("tambah") = (rd("tambah") = 1)
                        akses("ubah") = (rd("ubah") = 1)
                        akses("hapus") = (rd("hapus") = 1)
                        akses("adaAkses") = True
                    End If
                End Using
            End Using
        Catch ex As Exception
            MsgBox("Error cek akses: " & ex.Message)
        End Try

        Return akses
    End Function
End Module
