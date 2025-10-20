Imports System.Data.Odbc

Module MAksesSimpan

    ' Fungsi untuk menyimpan atau memperbarui hak akses user
    Public Sub SimpanAksesUser(ByVal kodeUser As String, ByVal dg As DataGridView)
        Try
            If Conn Is Nothing OrElse Conn.State = ConnectionState.Closed Then
                BukaKoneksi()
            End If

            Using trans As OdbcTransaction = Conn.BeginTransaction()
                ' --- Cek apakah kodeuser sudah ada di zakses
                Dim sqlCek As String = "SELECT COUNT(*) FROM zakses WHERE kodeuser = ?"
                Dim cmdCek As New OdbcCommand(sqlCek, Conn, trans)
                cmdCek.Parameters.AddWithValue("@kodeuser", kodeUser)
                Dim sudahAda As Integer = Convert.ToInt32(cmdCek.ExecuteScalar())

                ' --- Jika sudah ada, hapus semua data akses lama user tersebut
                If sudahAda > 0 Then
                    Dim cmdDel As New OdbcCommand("DELETE FROM zakses WHERE kodeuser = ?", Conn, trans)
                    cmdDel.Parameters.AddWithValue("@kodeuser", kodeUser)
                    cmdDel.ExecuteNonQuery()
                End If

                ' --- Siapkan command insert
                Dim cmdIns As New OdbcCommand("INSERT INTO zakses (kodeuser, idmenu, tambah, ubah, hapus) VALUES (?, ?, ?, ?, ?)", Conn, trans)

                ' --- Loop semua baris grid
                For Each row As DataGridViewRow In dg.Rows
                    Dim namaForm As String = row.Cells("NamaForm").Value.ToString()
                    Dim tambah As Integer = If(Convert.ToBoolean(row.Cells("Tambah").Value), 1, 0)
                    Dim ubah As Integer = If(Convert.ToBoolean(row.Cells("Ubah").Value), 1, 0)
                    Dim hapus As Integer = If(Convert.ToBoolean(row.Cells("Hapus").Value), 1, 0)

                    ' Ambil idmenu berdasarkan submenu
                    Dim submenu As String = "sm" & namaForm
                    Dim sqlGetId As String = "SELECT idmenu FROM zmenu WHERE submenu = ?"
                    Using cmdGetId As New OdbcCommand(sqlGetId, Conn, trans)
                        cmdGetId.Parameters.AddWithValue("@submenu", submenu)
                        Dim idmenuObj As Object = cmdGetId.ExecuteScalar()

                        If idmenuObj IsNot Nothing Then
                            Dim idmenu As String = idmenuObj.ToString()

                            cmdIns.Parameters.Clear()
                            cmdIns.Parameters.AddWithValue("@kodeuser", kodeUser)
                            cmdIns.Parameters.AddWithValue("@idmenu", idmenu)
                            cmdIns.Parameters.AddWithValue("@tambah", tambah)
                            cmdIns.Parameters.AddWithValue("@ubah", ubah)
                            cmdIns.Parameters.AddWithValue("@hapus", hapus)
                            cmdIns.ExecuteNonQuery()
                        End If
                    End Using
                Next

                trans.Commit()
            End Using

            MessageBox.Show("Data akses untuk user '" & kodeUser & "' berhasil disimpan.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception
            MessageBox.Show("Gagal menyimpan data akses user: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

End Module
