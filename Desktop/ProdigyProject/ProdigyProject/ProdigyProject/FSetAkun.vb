Imports System.Data.Odbc

Public Class FSetAkun
    Private statusMode As String = ""   ' status: "TAMBAH" / "UBAH"

    Private Sub FSetAkun_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetButtonState(Me, True)
        BukaKoneksi()
        DisabledLoad()
    End Sub

    Private Sub KosongkanInput()
        txtKDUSER.Text = ""
        txtNMUSER.Text = ""
        txtPASSUSER.Text = ""
        txtKPASSUSER.Text = ""
        tSKDUSER.Text = ""
        tSNMUSER.Text = ""
    End Sub

    Private Sub DisabledLoad()
        txtKDUSER.Enabled = False
        txtNMUSER.Enabled = False
        txtPASSUSER.Enabled = False
        txtKPASSUSER.Enabled = False
        tSKDUSER.Enabled = True
        tSNMUSER.Enabled = True
    End Sub

    Private Sub EnabledLoad()
        txtKDUSER.Enabled = True
        txtNMUSER.Enabled = True
        txtPASSUSER.Enabled = True
        txtKPASSUSER.Enabled = True
        tSKDUSER.Enabled = False
        tSNMUSER.Enabled = False
    End Sub

    Private Sub btnTAMBAH_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTAMBAH.Click
        SetButtonState(Me, False)
        statusMode = "tambah"
        KosongkanInput()
        EnabledLoad()
    End Sub

    Private Sub btnUBAH_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUBAH.Click

        Dim kodeuser As String = txtKDUSER.Text.Trim()

        ' cek apakah kosong
        If kodeuser Is Nothing OrElse String.IsNullOrWhiteSpace(txtKDUSER.Text) Then
            MessageBox.Show("Tidak ada data yang bisa di edit", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        EnabledLoad()
        SetButtonState(Me, False)
        statusMode = "ubah"
    End Sub

    Private Sub btnHAPUS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHAPUS.Click
        Try
            Dim kodeuser As String = txtKDUSER.Text.Trim()

            If String.IsNullOrEmpty(kodeuser) Then
                MessageBox.Show("Tidak ada User yang bisa dihapus.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            If MessageBox.Show("Yakin hapus " & kodeuser & " ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then
                Exit Sub
            End If

            MAkunSimpan.HapusAkun(txtKDUSER.Text)

            SetButtonState(Me, True)
            statusMode = ""
            DisabledLoad()

            MessageBox.Show(kodeuser & " berhasil dihapus.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error Hapus", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnSIMPAN_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSIMPAN.Click
        Try
            MAkunSimpan.SimpanAkun(txtKDUSER.Text, txtNMUSER.Text, txtPASSUSER.Text, statusMode)

            MessageBox.Show("Data User berhasil disimpan")

            statusMode = ""
            SetButtonState(Me, True)
            DisabledLoad()

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error Simpan")
        End Try
    End Sub

    Private Sub btnBATAL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBATAL.Click
        SetButtonState(Me, True)
        statusMode = ""
        DisabledLoad()
    End Sub

    Private Sub tUser_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles tSKDUSER.TextChanged, tSNMUSER.TextChanged

        Dim txt As TextBox = CType(sender, TextBox)
        If txt Is tSKDUSER Then
            tSNMUSER.Enabled = (tSKDUSER.Text.Trim() = "")
        ElseIf txt Is tSNMUSER Then
            tSKDUSER.Enabled = (tSNMUSER.Text.Trim() = "")
        End If
    End Sub

    Private Sub btnAKSES_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAKSES.Click

    End Sub
End Class