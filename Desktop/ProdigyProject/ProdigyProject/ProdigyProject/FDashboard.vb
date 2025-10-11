Public Class FDashboard

    Private Sub FDashboard_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        Me.FormBorderStyle = FormBorderStyle.FixedSingle
        Dim berhasil As Boolean = False

        ' Loop sampai user berhasil login atau menutup form login
        While Not berhasil
            Dim login As New FLogin()
            Dim result As DialogResult = login.ShowDialog()

            If result = DialogResult.OK Then
                berhasil = True
                ' Login sukses → dashboard lanjut berjalan
            ElseIf result = DialogResult.Cancel Then
                ' Kalau user batal, keluar aplikasi
                Application.Exit()
                Exit While
            End If
        End While

        ' Buat TabControl
    End Sub

    Private Function BukaChild(Of T As {Form, New})() As T
        ' Cek apakah form sudah terbuka
        For Each f As Form In Me.MdiChildren
            If TypeOf f Is T Then
                f.Activate()
                Return CType(f, T)
            End If
        Next

        ' Kalau belum ada, buat baru
        Dim frm As New T()
        frm.MdiParent = Me
        frm.Show()
        Return frm
    End Function

    ' Tambahkan event ini
    Private Sub FDashboard_FormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs) Handles MyBase.FormClosing
        Dim tanya As DialogResult = MessageBox.Show("Apakah anda mau keluar?", "Konfirmasi",
                                                    MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If tanya = DialogResult.No Then
            e.Cancel = True ' batal menutup form
        End If
    End Sub

    Private Sub smExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles smExit.Click

    End Sub

    Private Sub smPembelian_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles smPembelian.Click
        BukaChild(Of FPembelian)()
    End Sub

    Private Sub smPenjualan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles smPenjualan.Click
        BukaChild(Of FPenjualan)()
    End Sub

    Private Sub smMutasi_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles smMutasi.Click
        BukaChild(Of FMutasi)()
    End Sub

    Private Sub smPenyesuaian_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles smPenyesuaian.Click
        BukaChild(Of FPenyesuaian)()
    End Sub

    Private Sub smLaporan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles smLaporan.Click

    End Sub

End Class
