Imports System.Data.Odbc

Public Class FLaporanPenjualan

    Private Sub FLaporanPenjualan_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.MaximizeBox = False
    End Sub

    Private Sub btnPRINT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPRINT.Click
        Try
            ' --- Pastikan user pilih jenis laporan ---
            If Not cbREKAP.Checked AndAlso Not cbRINCI.Checked Then
                MessageBox.Show("Silakan pilih jenis laporan (Rekap atau Rinci).",
                                "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            ' --- Buka form cetak ---
            Dim f As New FCetak()

            Dim jenisLaporan As String = ""
            Dim sql As String = ""
            Dim nonota As String = ""

            If cbREKAP.Checked Then
                jenisLaporan = "laporanrekapjual"
                sql = "SELECT nonota FROM zjual" ' Ambil salah satu (atau bisa sesuaikan filter)
            ElseIf cbRINCI.Checked Then
                jenisLaporan = "laporanrincijual"
                sql = "SELECT nonota FROM zjualm"
            End If

            ' --- Ambil nonota dari database ---
            Using cmd As New OdbcCommand(sql, Conn)
                Dim rd As OdbcDataReader = cmd.ExecuteReader()
                If rd.Read() Then
                    nonota = rd("nonota").ToString()
                End If
                rd.Close()
            End Using

            ' --- Isi parameter laporan ---
            f.Param("tipe") = "laporan"
            f.Param("jenis") = jenisLaporan
            f.Param("nonota") = nonota
            f.Param("kodekust1") = txtLKDKUST1.Text.Trim()
            f.Param("kodekust2") = txtLKDKUST2.Text.Trim()
            f.Param("tgl1") = dtTGL1.Value.ToString("dd-MM-yyyy")
            f.Param("tgl2") = dtTGL2.Value.ToString("dd-MM-yyyy")

            ' --- Tampilkan form cetak ---
            f.ShowDialog()

        Catch ex As Exception
            MessageBox.Show("Terjadi kesalahan saat mencetak laporan: " & ex.Message,
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub cbRINCI_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbRINCI.CheckedChanged
        If cbRINCI.Checked Then
            cbREKAP.Checked = False
        End If
    End Sub

    Private Sub cbREKAP_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbREKAP.CheckedChanged
        If cbREKAP.Checked Then
            cbRINCI.Checked = False
        End If
    End Sub

    Private Sub btnTUTUP_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTUTUP.Click
        Me.Close()
    End Sub
End Class