Imports System.Data.Odbc
Imports System.Windows.Forms

Module Koneksi
    ' === Variabel global untuk koneksi dan objek DB ===
    Public Conn As OdbcConnection
    Public Cmd As OdbcCommand
    Public Da As OdbcDataAdapter
    Public Ds As DataSet
    Public Rd As OdbcDataReader
    Public Trans As OdbcTransaction

    ' === Connection string utama (ganti sesuai kebutuhan) ===
    Private ReadOnly connStr As String =
        "Driver={MySQL ODBC 5.1 Driver};" &
        "Server=192.168.1.123;" &
        "Database=dbkita;" &
        "User=kita;" &
        "Password=kita;" &
        "Option=3;" &
        "Pooling=True;" &
        "MinPoolSize=1;" &
        "MaxPoolSize=50;" &
        "ConnectionTimeout=3;"

    ' === Fungsi utama untuk membuka koneksi ===
    Public Sub BukaKoneksi()
        Try
            ' === Jika belum ada koneksi, buat baru ===
            If Conn Is Nothing Then
                Conn = New OdbcConnection(connStr)
            End If

            ' === Jika koneksi tertutup atau terputus, buka ulang ===
            If Conn.State <> ConnectionState.Open Then
                Conn.Open()
            End If

        Catch ex As Exception
            MessageBox.Show("Gagal koneksi ke database:" & vbCrLf & ex.Message,
                            "Koneksi Database",
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' === Fungsi untuk memastikan koneksi tetap hidup (auto-reconnect) ===
    Public Sub CekKoneksi()
        Try
            If Conn Is Nothing OrElse Conn.State <> ConnectionState.Open Then
                Conn = New OdbcConnection(connStr)
                Conn.Open()
            End If
        Catch ex As Exception
            MessageBox.Show("Koneksi database terputus: " & ex.Message,
                            "Koneksi Ulang", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try
    End Sub

    ' === Fungsi untuk menutup koneksi (jarang perlu dipanggil) ===
    Public Sub TutupKoneksi()
        Try
            If Conn IsNot Nothing AndAlso Conn.State = ConnectionState.Open Then
                Conn.Close()
            End If
        Catch ex As Exception
            MessageBox.Show("Gagal menutup koneksi: " & ex.Message)
        End Try
    End Sub

End Module

