Imports MySql.Data.MySqlClient

Public Class Login

    ' Ganti dengan string koneksi yang sesuai dengan pengaturan database Anda
    Private connString As String = "server=localhost;user id=root;password=;database=dbeiffelpetshop;"

    Private Sub Login_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ' Kode yang ingin dijalankan saat form dimuat
    End Sub

    Private Sub ButtonLogin_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonLogin.Click
        ' Ambil username dan password dari TextBox
        Dim username As String = TextBoxUsername.Text
        Dim password As String = TextBoxPassword.Text

        ' Buat koneksi ke database
        Using conn As New MySqlConnection(connString)
            Try
                conn.Open()
                Dim query As String = "SELECT * FROM users WHERE Username=@Username AND Password=@Password"
                Dim cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@Username", username)
                cmd.Parameters.AddWithValue("@Password", password)

                ' Eksekusi query
                Dim reader As MySqlDataReader = cmd.ExecuteReader()

                ' Cek apakah ada hasil
                If reader.HasRows Then
                    reader.Read() ' Membaca data pengguna
                    CurrentUser.Username = reader("Username").ToString()
                    CurrentUser.UserId = reader("UserId").ToString() ' Ganti "UserId" sesuai nama kolom id di tabel Anda

                    MessageBox.Show("Login Berhasil!")
                    ' Arahkan ke form lain atau lakukan tindakan lain
                    Dim menuForm As New Menu()
                    menuForm.Show()
                    Me.Hide()
                Else
                    MessageBox.Show("Username atau Password salah!")
                End If
                reader.Close()
            Catch ex As Exception
                MessageBox.Show("Terjadi kesalahan: " & ex.Message)
            End Try
        End Using
    End Sub

    Private Sub ButtonKeluar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonKeluar.Click
        Dim result As DialogResult = MessageBox.Show("Apakah Anda yakin ingin keluar?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If result = DialogResult.Yes Then
            Application.Exit() ' Menutup aplikasi sepenuhnya
        End If
    End Sub
End Class
