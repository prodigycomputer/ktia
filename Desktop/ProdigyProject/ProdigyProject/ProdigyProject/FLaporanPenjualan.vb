Imports System.Data.Odbc

Public Class FLaporanPenjualan

    Private Sub FLaporanPenjualan_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.MaximizeBox = False

        SetupGridLJual(dgitmLAPJUAL)
        TampilkanDataLaporan()
    End Sub

    Private Sub TampilkanDataLaporan()
        Try
            BukaKoneksi()

            Dim sql As String =
                "SELECT zjual.nonota, zjual.tgl, zjual.tgltempo, zkustomer.namakust, zsales.namasls, zjual.nilai " &
                "FROM zjual " &
                "LEFT JOIN zkustomer ON zjual.kodekust = zkustomer.kodekust " &
                "LEFT JOIN zsales ON zjual.kodesls = zsales.kodesls " &
                "ORDER BY zjual.nonota DESC"

            Dim cmd As New OdbcCommand(sql, Conn)
            Dim rd As OdbcDataReader = cmd.ExecuteReader()

            dgitmLAPJUAL.Rows.Clear()

            While rd.Read()
                dgitmLAPJUAL.Rows.Add(
                    rd("nonota").ToString(),
                    Format(CDate(rd("tgl")), "dd-MM-yyyy"),
                    Format(CDate(rd("tgltempo")), "dd-MM-yyyy"),
                    rd("namakust").ToString(),
                    rd("namasls").ToString(),
                    FormatNumber(Val(rd("nilai")), 0)
                )
            End While

            rd.Close()

        Catch ex As Exception
            MessageBox.Show("Gagal menampilkan data laporan penjualan: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnPRINT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPRINT.Click
        Dim f As New FCetakLaporan()
        f.Param("jenis") = "laporanjual"
        f.ShowDialog()
    End Sub
End Class