Imports System.Data.Odbc
Module Koneksi
    Public Conn As OdbcConnection
    Public Cmd As OdbcCommand
    Public Da As OdbcDataAdapter
    Public Ds As DataSet
    Public Rd As OdbcDataReader
    Public Trans As OdbcTransaction

    Public Sub BukaKoneksi()
        Try
            ' --- koneksi langsung tanpa DSN ---
            Dim connStr As String = "Driver={MySQL ODBC 5.1 Driver};Server=localhost;Database=dbkita;User=root;Password=;Option=3;"
            Conn = New OdbcConnection(connStr)

            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If

        Catch ex As Exception
            MessageBox.Show("Gagal koneksi: " & ex.Message)
        End Try
    End Sub
End Module
