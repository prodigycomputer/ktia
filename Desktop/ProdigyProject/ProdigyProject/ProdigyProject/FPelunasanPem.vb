Public Class FPelunasanPem

    Private Sub CariSupplierDanLoadGrid()
        Dim kode As String = txtKDSUP.Text.Trim
        Dim nama As String = txtNMPEM.Text.Trim

        ' Jika user isi NAMA supplier dulu
        If kode = "" And nama <> "" Then
            kode = CariKodeSupDariNama(nama)
            txtKDSUP.Text = kode
        End If

        ' Jika user isi KODE supplier dulu
        If nama = "" And kode <> "" Then
            nama = CariNamaSupDariKode(kode)
            txtNMPEM.Text = nama
        End If

        If kode <> "" Then
            LoadGridPembelian(kode)
        Else
            dgBAYAR.Rows.Clear()
        End If
    End Sub

    Private Function CariKodeSupDariNama(ByVal nama As String) As String
        Dim sql As String = "SELECT kodesup FROM zsupplier WHERE namasup LIKE ?"

        Using cmd As New Odbc.OdbcCommand(sql, Conn)
            cmd.Parameters.AddWithValue("@nama", "%" & nama & "%")

            Dim rd = cmd.ExecuteReader()
            If rd.Read() Then
                Return rd("kodesup").ToString()
            End If

            Return ""
        End Using
    End Function

    Private Function CariNamaSupDariKode(ByVal kode As String) As String
        Dim sql As String = "SELECT namasup FROM zsupplier WHERE kodesup = ?"

        Using cmd As New Odbc.OdbcCommand(sql, Conn)
            cmd.Parameters.AddWithValue("@kodesup", kode)

            Dim rd = cmd.ExecuteReader()
            If rd.Read() Then
                Return rd("namasup").ToString()
            End If

            Return ""
        End Using
    End Function

    Private Sub LoadGridPembelian(ByVal kodesup As String)
        dgBAYAR.Rows.Clear()

        Dim sql As String =
            "SELECT tgl, nonota, nilai, bayar FROM zbeli " &
            "WHERE kodesup = ? ORDER BY tgl, nonota"

        Using cmd As New Odbc.OdbcCommand(sql, Conn)
            cmd.Parameters.AddWithValue("@kodesup", kodesup)

            Dim rd = cmd.ExecuteReader()
            While rd.Read()
                Dim nilai As Decimal = If(IsDBNull(rd("nilai")), 0, CDec(rd("nilai")))
                Dim bayar As Decimal = If(IsDBNull(rd("bayar")), 0, CDec(rd("bayar")))
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
    End Sub

    Private Sub FPelunasanPem_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetupGridPembayaran(dgBAYAR)
    End Sub

    Private Sub txtKDSUP_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles txtKDSUP.KeyDown
        If e.KeyCode = Keys.Enter Then
            CariSupplierDanLoadGrid()
        End If
    End Sub

    Private Sub txtNMPEM_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles txtNMPEM.KeyDown
        If e.KeyCode = Keys.Enter Then
            CariSupplierDanLoadGrid()
        End If
    End Sub

    Private Sub dgBAYAR_CellDoubleClick(ByVal sender As Object, ByVal e As DataGridViewCellEventArgs) Handles dgBAYAR.CellDoubleClick
        If e.RowIndex < 0 Then Exit Sub ' klik header, abaikan

        Dim row As DataGridViewRow = dgBAYAR.Rows(e.RowIndex)
        Dim nonota As String = row.Cells("nonota").Value.ToString()
        Dim nilai As Decimal = row.Cells("nilai").Value.ToString()

        If IsNumeric(row.Cells("nilai").Value) Then
            nilai = CDec(row.Cells("nilai").Value)
        End If
        ' buka form
        Dim f As New FBayarPelunasan()
        f.NoNota = nonota
        f.ShowDialog()

        If f.DialogResultValue Then
            Dim bayar As Decimal = f.Bayar
            Dim diskon As Decimal = f.Diskon
            Dim sisa As Decimal = nilai - bayar

            row.Cells("bayar").Value = bayar
            row.Cells("pembulatan").Value = diskon
            row.Cells("sisa").Value = sisa
        End If
    End Sub
End Class