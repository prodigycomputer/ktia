Imports System.Data.Odbc

Public Class FLaporanPenyesuaian

    Private Sub FLaporanPenyesuaian_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.MaximizeBox = False

        SetupGridDPenyesuaian(dgitmLAPPENY)
        TampilkanDataLaporan()
    End Sub

    Private Sub TampilkanDataLaporan()
        Try
            BukaKoneksi()

            Dim sql As String =
                "SELECT zpenyesuaian.tgl, zpenyesuaian.nonota, zgudang.namagd " &
                "FROM zpenyesuaian " &
                "LEFT JOIN zgudang ON zpenyesuaian.kodegd = zgudang.kodegd " &
                "ORDER BY zpenyesuaian.nonota DESC"

            Dim cmd As New OdbcCommand(sql, Conn)
            Dim rd As OdbcDataReader = cmd.ExecuteReader()

            dgitmLAPPENY.Rows.Clear()

            While rd.Read()
                dgitmLAPPENY.Rows.Add(
                    Format(CDate(rd("tgl")), "dd-MM-yyyy"),
                    rd("nonota").ToString(),
                    rd("namagd").ToString()
                )
            End While

            rd.Close()

        Catch ex As Exception
            MessageBox.Show("Gagal menampilkan data laporan penyesuaian: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnPRINT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPRINT.Click
        Dim f As New FCetak()
        f.Param("jenis") = "laporanpenyesuaian"
        f.ShowDialog()
    End Sub
End Class