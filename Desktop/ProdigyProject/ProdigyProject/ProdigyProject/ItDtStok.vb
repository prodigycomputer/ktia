Imports System.Data.Odbc

Public Class ItDtStok

    Public Sub LoadDataStok(Optional ByVal kodebrg As String = "",
                        Optional ByVal namabrg As String = "")

        Dim sql As String = "SELECT kodebrg, namabrg, kodemerk, kodegol, kodegrup, satuan1, satuan2, satuan3, isi1, isi2, hrgbeli, harga1, harga2, harga3, harga4, harga5, harga6, harga11, harga22, harga33, harga44, harga55, harga66, harga111, harga222, harga333, harga444, harga555, harga666 FROM zstok"
        Dim whereList As New List(Of String)

        ' --- Filter berdasarkan kodeuser
        If kodebrg <> "" Then
            whereList.Add("kodebrg LIKE ?")
        End If

        ' --- Filter berdasarkan username
        If namabrg <> "" Then
            whereList.Add("namabrg LIKE ?")
        End If

        If whereList.Count > 0 Then
            sql &= " WHERE " & String.Join(" AND ", whereList)
        End If

        sql &= " ORDER BY kodebrg"

        dgitmSTOK.Rows.Clear()

        Try
            BukaKoneksi()
            Cmd = New OdbcCommand(sql, Conn)

            ' --- Bind parameter sesuai urutan filter
            If kodebrg <> "" Then
                Cmd.Parameters.AddWithValue("", "%" & kodebrg & "%")
            End If
            If namabrg <> "" Then
                Cmd.Parameters.AddWithValue("", "%" & namabrg & "%")
            End If

            Rd = Cmd.ExecuteReader()
            While Rd.Read()
                dgitmSTOK.Rows.Add(
                    Rd("kodebrg").ToString(),
                    Rd("namabrg").ToString(),
                    Rd("kodemerk").ToString(),
                    Rd("kodegol").ToString(),
                    Rd("kodegrup").ToString(),
                    Rd("satuan1").ToString(),
                    Rd("satuan2").ToString(),
                    Rd("satuan3").ToString(),
                    Rd("isi1"),
                    Rd("isi2"),
                    Rd("hrgbeli"),
                    Rd("harga1"),
                    Rd("harga2"),
                    Rd("harga3"),
                    Rd("harga4"),
                    Rd("harga5"),
                    Rd("harga6"),
                    Rd("harga11"),
                    Rd("harga22"),
                    Rd("harga33"),
                    Rd("harga44"),
                    Rd("harga55"),
                    Rd("harga66"),
                    Rd("harga111"),
                    Rd("harga222"),
                    Rd("harga333"),
                    Rd("harga444"),
                    Rd("harga555"),
                    Rd("harga666")
                )
            End While
            Rd.Close()
            Conn.Close()
        Catch ex As Exception
            MessageBox.Show("Gagal load data: " & ex.Message)
        End Try
    End Sub

    Private Sub ItDtStok_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.FormBorderStyle = FormBorderStyle.FixedSingle
        Me.MaximizeBox = False

        SetupGridStok(dgitmSTOK)
    End Sub

    Private Sub dgitmSTOK_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgitmSTOK.CellContentClick
        If e.RowIndex >= 0 Then
            Dim data As String = dgitmSTOK.Rows(e.RowIndex).Cells("kodebrg").Value.ToString()

            Dim parentForm As FStok = TryCast(Me.Owner, FStok)
            If parentForm IsNot Nothing Then
                parentForm.LoadData(data)
            End If

            Me.Close()
        End If
    End Sub
End Class