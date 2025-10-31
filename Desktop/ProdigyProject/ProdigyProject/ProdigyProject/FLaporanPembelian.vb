Imports System.Data.Odbc

Public Class FLaporanPembelian

    Private Sub FLaporanPembelian_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.MaximizeBox = False

        SetupGridLBeli(dgitmLAPBELI)
        TampilkanDataLaporan()
    End Sub

    Private Sub TampilkanDataLaporan()
        Try
            BukaKoneksi()

            Dim sql As String =
                "SELECT zbeli.nonota, zbeli.tgl, zbeli.tgltempo, zsupplier.namasup, zbeli.nilai " &
                "FROM zbeli " &
                "LEFT JOIN zsupplier ON zbeli.kodesup = zsupplier.kodesup " &
                "ORDER BY zbeli.nonota DESC"

            Dim cmd As New OdbcCommand(sql, Conn)
            Dim rd As OdbcDataReader = cmd.ExecuteReader()

            dgitmLAPBELI.Rows.Clear()

            While rd.Read()
                dgitmLAPBELI.Rows.Add(
                    rd("nonota").ToString(),
                    Format(CDate(rd("tgl")), "dd-MM-yyyy"),
                    Format(CDate(rd("tgltempo")), "dd-MM-yyyy"),
                    rd("namasup").ToString(),
                    FormatNumber(Val(rd("nilai")), 0)
                )
            End While

            rd.Close()

        Catch ex As Exception
            MessageBox.Show("Gagal menampilkan data laporan pembelian: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnPRINT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPRINT.Click
        ' --- Buka form cetak ---
        Dim f As New FCetakLaporan()
        f.Param("jenis") = "laporanbeli"
        f.ShowDialog()
    End Sub
End Class
