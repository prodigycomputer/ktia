Imports System.Data.Odbc
Imports System.IO
Imports System.Windows.Forms

Module Koneksi

    Public Conn As OdbcConnection
    Public Cmd As OdbcCommand
    Public Da As OdbcDataAdapter
    Public Ds As DataSet
    Public Rd As OdbcDataReader
    Public Trans As OdbcTransaction

    Private connStr As String = ""

    ' === Baca config.ini ===
    Private Sub LoadConfig()
        Try
            Dim path As String = Application.StartupPath & "\config.ini"

            If Not File.Exists(path) Then
                MessageBox.Show("config.ini tidak ditemukan!" & vbCrLf &
                                "Pastikan file config.ini ada di folder aplikasi.",
                                "Error Konfigurasi", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End
            End If

            Dim lines() As String = File.ReadAllLines(path)

            Dim driver, server, database, user, password, optionDb As String
            driver = ""
            server = ""
            database = ""
            user = ""
            password = ""
            optionDb = "3"

            For Each line As String In lines
                If line.StartsWith("Driver=") Then driver = line.Split("="c)(1)
                If line.StartsWith("Server=") Then server = line.Split("="c)(1)
                If line.StartsWith("Database=") Then database = line.Split("="c)(1)
                If line.StartsWith("User=") Then user = line.Split("="c)(1)
                If line.StartsWith("Password=") Then password = line.Split("="c)(1)
                If line.StartsWith("Option=") Then optionDb = line.Split("="c)(1)
            Next

            connStr =
                "Driver={" & driver & "};" &
                "Server=" & server & ";" &
                "Database=" & database & ";" &
                "User=" & user & ";" &
                "Password=" & password & ";" &
                "Option=" & optionDb & ";" &
                "Pooling=True;" &
                "MinPoolSize=1;" &
                "MaxPoolSize=50;" &
                "ConnectionTimeout=3;"

        Catch ex As Exception
            MessageBox.Show("Gagal membaca config.ini:" & vbCrLf & ex.Message,
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub

    ' === Buka koneksi ===
    Public Sub BukaKoneksi()
        Try
            If connStr = "" Then LoadConfig()

            If Conn Is Nothing Then
                Conn = New OdbcConnection(connStr)
            End If

            If Conn.State <> ConnectionState.Open Then
                Conn.Open()
            End If

        Catch ex As Exception
            MessageBox.Show("Gagal koneksi ke database:" & vbCrLf & ex.Message,
                            "Koneksi Database", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' === Cek & reconnect ===
    Public Sub CekKoneksi()
        Try
            If Conn Is Nothing OrElse Conn.State <> ConnectionState.Open Then
                Conn = New OdbcConnection(connStr)
                Conn.Open()
            End If
        Catch ex As Exception
            MessageBox.Show("Koneksi database terputus:" & vbCrLf & ex.Message,
                            "Reconnect", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try
    End Sub

End Module
