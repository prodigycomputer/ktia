Imports System.Data.Odbc
Imports System.Drawing

Public Class FDtAkses
    Public Property KodeUser As String  ' untuk menerima data dari FSetAkun

    Private Sub FDtAkses_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        lblKDUSER.Text = "Kode User : " & KodeUser
        TampilkanDataAkses()
    End Sub

    Private Sub TampilkanDataAkses()
        Try
            ' Pastikan koneksi terbuka
            If Conn Is Nothing OrElse Conn.State = ConnectionState.Closed Then
                BukaKoneksi()
            End If

            ' === Ambil semua menu yang tersedia ===
            Dim sqlMenu As String = "SELECT idmenu, submenu FROM zmenu ORDER BY mainmenu, urutan"
            Dim daMenu As New OdbcDataAdapter(sqlMenu, Conn)
            Dim dtMenu As New DataTable
            daMenu.Fill(dtMenu)

            ' === Ambil data akses berdasarkan user ===
            Dim sqlAkses As String = "SELECT idmenu, tambah, ubah, hapus FROM zakses WHERE kodeuser = ?"
            Dim daAkses As New OdbcDataAdapter(sqlAkses, Conn)
            daAkses.SelectCommand.Parameters.AddWithValue("@kodeuser", KodeUser)
            Dim dtAkses As New DataTable
            daAkses.Fill(dtAkses)

            ' Bersihkan DataGridView
            dgStAKUN.Columns.Clear()
            dgStAKUN.Rows.Clear()
            dgStAKUN.AutoGenerateColumns = False
            dgStAKUN.AllowUserToAddRows = False
            dgStAKUN.RowHeadersVisible = False
            dgStAKUN.AllowUserToDeleteRows = False
            dgStAKUN.AllowUserToResizeRows = False
            dgStAKUN.SelectionMode = DataGridViewSelectionMode.CellSelect

            ' Style Header
            dgStAKUN.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            dgStAKUN.ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 9, FontStyle.Bold)

            ' Kolom NamaForm
            Dim colNama As New DataGridViewTextBoxColumn()
            colNama.HeaderText = "Nama Form"
            colNama.Name = "NamaForm"
            colNama.ReadOnly = True
            colNama.Width = 188
            colNama.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            dgStAKUN.Columns.Add(colNama)

            ' Kolom Tambah, Ubah, Hapus
            For Each nama In {"Tambah", "Ubah", "Hapus"}
                Dim col As New DataGridViewCheckBoxColumn()
                col.HeaderText = nama
                col.Name = nama
                col.Width = 90
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                dgStAKUN.Columns.Add(col)
            Next

            ' === Tentukan apakah user sudah punya data di zakses ===
            Dim adaAkses As Boolean = (dtAkses.Rows.Count > 0)

            ' === Isi baris DataGridView ===
            For Each rowMenu As DataRow In dtMenu.Rows
                Dim idmenu As String = rowMenu("idmenu").ToString()
                Dim submenu As String = rowMenu("submenu").ToString()
                If submenu.StartsWith("sm") Then submenu = submenu.Substring(2)

                Dim tTambah As Boolean = True
                Dim tUbah As Boolean = True
                Dim tHapus As Boolean = True

                If adaAkses Then
                    ' Cek apakah idmenu ada di dtAkses
                    Dim found() As DataRow = dtAkses.Select("idmenu = '" & idmenu & "'")
                    If found.Length > 0 Then
                        tTambah = (found(0)("tambah").ToString() = "1")
                        tUbah = (found(0)("ubah").ToString() = "1")
                        tHapus = (found(0)("hapus").ToString() = "1")
                    End If
                End If

                dgStAKUN.Rows.Add(submenu, tTambah, tUbah, tHapus)
            Next

        Catch ex As Exception
            MessageBox.Show("Terjadi kesalahan saat menampilkan data akses: " & ex.Message)
        End Try
    End Sub

    ' Batasi agar hanya kolom checkbox bisa diklik
    Private Sub dgStAKUN_CellClick(ByVal sender As Object, ByVal e As DataGridViewCellEventArgs) Handles dgStAKUN.CellClick
        If e.RowIndex >= 0 AndAlso e.ColumnIndex >= 0 Then
            Dim colName = dgStAKUN.Columns(e.ColumnIndex).Name
            ' Jika klik di kolom NamaForm, jangan lakukan apa pun
            If colName = "NamaForm" Then
                dgStAKUN.ClearSelection()
            End If
        End If
    End Sub

    Private Sub btnPTUTUP_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPTUTUP.Click
        Me.Close()
    End Sub

    Private Sub btnPSIMPAN_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPSIMPAN.Click
        SimpanAksesUser(KodeUser, dgStAKUN)
    End Sub

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
End Class
