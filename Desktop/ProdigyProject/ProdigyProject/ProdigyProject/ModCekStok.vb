Imports System.Data.Odbc

Module ModCekStok
    Public Function GetStok(ByVal kodeBrg As String, Optional ByVal kodeGd As String = "") As String
        Try
            If Conn Is Nothing OrElse Conn.State = ConnectionState.Closed Then
                BukaKoneksi()
            End If

            If String.IsNullOrEmpty(kodeBrg) Then
                Return ""
            End If

            Dim sql As String
            Dim cmd As OdbcCommand

            If kodeGd.Trim() <> "" Then
                ' === Jika pakai gudang ===
                sql = "SELECT s.sisa1, s.sisa2, s.sisa3, z.satuan1, z.satuan2, z.satuan3 " &
                      "FROM zsaldo s " &
                      "LEFT JOIN zstok z ON s.kodebrg = z.kodebrg " &
                      "WHERE s.kodebrg = ? AND s.kodegd = ?"

                cmd = New OdbcCommand(sql, Conn)
                cmd.Parameters.AddWithValue("@1", kodeBrg)
                cmd.Parameters.AddWithValue("@2", kodeGd)

            Else
                ' === Jika tidak pakai gudang ===
                sql = "SELECT s.sisa1, s.sisa2, s.sisa3, z.satuan1, z.satuan2, z.satuan3 " &
                      "FROM zsaldo s " &
                      "LEFT JOIN zstok z ON s.kodebrg = z.kodebrg " &
                      "WHERE s.kodebrg = ?"

                cmd = New OdbcCommand(sql, Conn)
                cmd.Parameters.AddWithValue("@1", kodeBrg)
            End If

            Using rd As OdbcDataReader = cmd.ExecuteReader()
                If rd.Read() Then
                    Dim s1 As Decimal = If(IsDBNull(rd("sisa1")), 0, Convert.ToDecimal(rd("sisa1")))
                    Dim s2 As Decimal = If(IsDBNull(rd("sisa2")), 0, Convert.ToDecimal(rd("sisa2")))
                    Dim s3 As Decimal = If(IsDBNull(rd("sisa3")), 0, Convert.ToDecimal(rd("sisa3")))

                    Dim st1 As String = If(IsDBNull(rd("satuan1")), "", rd("satuan1").ToString())
                    Dim st2 As String = If(IsDBNull(rd("satuan2")), "", rd("satuan2").ToString())
                    Dim st3 As String = If(IsDBNull(rd("satuan3")), "", rd("satuan3").ToString())

                    ' Jika semua nol
                    If s1 = 0 AndAlso s2 = 0 AndAlso s3 = 0 Then
                        Return "Stok Habis"
                    End If

                    ' Format tampilan stok
                    Dim parts As New List(Of String)
                    If s1 > 0 Then parts.Add(s1.ToString("N0") & " " & st1)
                    If s2 > 0 Then parts.Add(s2.ToString("N0") & " " & st2)
                    If s3 > 0 Then parts.Add(s3.ToString("N0") & " " & st3)

                    If parts.Count = 0 Then
                        parts.Add(s1.ToString("N0") & " " & st1)
                        parts.Add(s2.ToString("N0") & " " & st2)
                        parts.Add(s3.ToString("N0") & " " & st3)
                    End If

                    Return String.Join(", ", parts)
                Else
                    Return "Stok tidak tersedia"
                End If
            End Using

        Catch ex As Exception
            Return "Error: " & ex.Message
        End Try
    End Function
End Module
