Imports System.Data.Odbc

Public Class FPelunasanPem

    Public Sub SetSupplier(ByVal kode As String, ByVal nama As String)
        txtKDSUP.Text = kode
        txtNMPEM.Text = nama

        LoadGridPembelian(kode)

    End Sub

    Private Sub LoadGridPembelian(ByVal kodesup As String)
        dgBAYAR.Rows.Clear()

        Dim sql As String =
            "SELECT tgl, nonota, nilai, bayar FROM zbeli " &
            "WHERE kodesup = ? ORDER BY tgl, nonota"

        Try
            BukaKoneksi()   ' PENTING!

            Using cmd As New OdbcCommand(sql, Conn)
                cmd.Parameters.AddWithValue("@kodesup", kodesup)

                Using rd As OdbcDataReader = cmd.ExecuteReader()
                    While rd.Read()

                        Dim nilai As Decimal = If(IsDBNull(rd("nilai")), 0D, CDec(rd("nilai")))
                        Dim bayar As Decimal = If(IsDBNull(rd("bayar")), 0D, CDec(rd("bayar")))
                        Dim sisa As Decimal = nilai - bayar

                        dgBAYAR.Rows.Add(
                            Format(CDate(rd("tgl")), "dd-MM-yyyy"),
                            rd("nonota").ToString(),
                            nilai,
                            sisa,
                            "",
                            bayar
                        )
                    End While
                End Using
            End Using

        Catch ex As Exception
            MessageBox.Show("Gagal load grid pembayaran: " & ex.Message)

        Finally
            Conn.Close()
        End Try
    End Sub

    Private Sub FPelunasanPem_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetupGridPembayaran(dgBAYAR)
    End Sub

    Private Sub txtKDSUP_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles txtKDSUP.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            Dim txtKode As TextBox = CType(sender, TextBox)
            If txtKode.Text.Trim() <> "" Then
                Dim f As New ItSupplier
                f.Owner = Me
                f.Show()
                f.LoadDataSup(txtKode.Text.Trim())
            End If
        End If
    End Sub

    Private Sub txtNMPEM_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles txtNMPEM.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            Dim txtKode As TextBox = CType(sender, TextBox)
            If txtKode.Text.Trim() <> "" Then
                Dim f As New ItSupplier
                f.Owner = Me
                f.Show()
                f.LoadDataSup(txtKode.Text.Trim())
            End If
        End If
    End Sub

    Private Sub dgBAYAR_CellDoubleClick(ByVal sender As Object, ByVal e As DataGridViewCellEventArgs) Handles dgBAYAR.CellDoubleClick
        If e.RowIndex < 0 Then Exit Sub ' klik header, abaikan

        Dim row As DataGridViewRow = dgBAYAR.Rows(e.RowIndex)
        Dim nonota As String = row.Cells("nonota").Value.ToString()
        Dim nilai As Decimal = row.Cells("nilai").Value.ToString()
        Dim sisa As Decimal = CDec(row.Cells("sisa").Value)

        If IsNumeric(row.Cells("nilai").Value) Then
            nilai = CDec(row.Cells("nilai").Value)
        End If
        ' buka form
        Dim f As New FBayarPelunasan()
        f.NoNota = nonota
        f.Sisa = sisa
        f.Nilai = nilai
        f.ShowDialog()

        If f.DialogResultValue Then
            Dim bayar As Decimal = f.Bayar
            Dim diskon As Decimal = f.Diskon
            Dim sisanew As Decimal = nilai - bayar

            row.Cells("bayar").Value = bayar
            row.Cells("pembulatan").Value = diskon
            row.Cells("sisa").Value = sisanew
        End If
    End Sub
End Class