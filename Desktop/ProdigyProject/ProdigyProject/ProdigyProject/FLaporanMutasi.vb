Imports System.Data.Odbc

Public Class FLaporanMutasi

    Private Sub FLaporanMutasi_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.MaximizeBox = False

        SetupGridDMutasi(dgitmLAPMUT)
        TampilkanDataLaporan()
    End Sub

    Private Sub TampilkanDataLaporan()
        Try
            BukaKoneksi()

            Dim sql As String =
                "SELECT zmutasi.tgl, zmutasi.nonota, g1.namagd AS namagd1, g2.namagd AS namagd2 " &
                "FROM zmutasi " &
                "JOIN zgudang g1 ON zmutasi.kodegd1 = g1.kodegd " &
                "JOIN zgudang g2 ON zmutasi.kodegd2 = g2.kodegd " &
                "ORDER BY zmutasi.nonota DESC"

            Dim cmd As New OdbcCommand(sql, Conn)
            Dim rd As OdbcDataReader = cmd.ExecuteReader()

            dgitmLAPMUT.Rows.Clear()

            While rd.Read()
                dgitmLAPMUT.Rows.Add(
                    Format(CDate(rd("tgl")), "dd-MM-yyyy"),
                    rd("nonota").ToString(),
                    rd("namagd1").ToString(),
                    rd("namagd2").ToString()
                )
            End While

            rd.Close()

        Catch ex As Exception
            MessageBox.Show("Gagal menampilkan data laporan mutasi: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnPRINT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPRINT.Click
        Dim f As New FCetakLaporan()
        f.Param("jenis") = "laporanmutasi"
        f.ShowDialog()
    End Sub
End Class