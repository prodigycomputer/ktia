Imports System.Data.Odbc

Public Class ItFPopupPem

    Public TargetGrid As DataGridView

    Public Sub SetBarang(ByVal kode As String, ByVal nama As String,
                     ByVal sat1 As String, ByVal sat2 As String, ByVal sat3 As String,
                     ByVal isi1 As Double, ByVal isi2 As Double, ByVal harga As Double)

        tPopKDBARANG.Text = kode
        tPopNMBARANG.Text = nama
        tPopSTN1.Text = sat1
        tPopSTN2.Text = sat2
        tPopSTN3.Text = sat3
        tPopHARGA.Text = harga

        ' Default disable
        tPopJLH2.Enabled = False
        tPopJLH3.Enabled = False

        ' Atur sesuai isi1 & isi2
        If isi1 > 1 Then
            tPopJLH2.Enabled = True
        End If
        If isi2 > 1 Then
            tPopJLH3.Enabled = True
        End If
    End Sub

    Private Sub ItFPopupPem_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.FormBorderStyle = FormBorderStyle.FixedSingle
        Me.MaximizeBox = False

        BukaKoneksi()
        LoadGudang()
    End Sub

    Private Sub LoadGudang()
        cbPopGudang.Items.Clear()

        Dim sql As String = "SELECT kodegd, namagd FROM zgudang ORDER BY kodegd"

        Try
            Cmd = New OdbcCommand(sql, Conn)
            Rd = Cmd.ExecuteReader()

            While Rd.Read()
                Dim kode As String = Rd("kodegd").ToString()
                Dim nama As String = Rd("namagd").ToString()
                cbPopGudang.Items.Add(kode & " " & nama)
            End While

            Rd.Close()
        Catch ex As Exception
            MessageBox.Show("Gagal load gudang: " & ex.Message)
        End Try
    End Sub

    Private Sub btnPopTAMBAH_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPopTAMBAH.Click
        If TargetGrid Is Nothing Then
            MessageBox.Show("Grid belum dihubungkan!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        ' Ambil data dari textbox/combobox popup
        Dim kode As String = tPopKDBARANG.Text.Trim()
        Dim nama As String = tPopNMBARANG.Text.Trim()
        Dim gudang As String = If(cbPopGudang.SelectedItem IsNot Nothing, cbPopGudang.SelectedItem.ToString(), "")
        Dim jlh1 As String = tPopJLH1.Text.Trim()
        Dim sat1 As String = tPopSTN1.Text.Trim()
        Dim jlh2 As String = tPopJLH2.Text.Trim()
        Dim sat2 As String = tPopSTN2.Text.Trim()
        Dim jlh3 As String = tPopJLH3.Text.Trim()
        Dim sat3 As String = tPopSTN3.Text.Trim()
        Dim harga As Decimal = Val(tPopHARGA.Text)
        Dim disca As Decimal = Val(tPopDISCA.Text)
        Dim discb As Decimal = Val(tPopDISCB.Text)
        Dim discc As Decimal = Val(tPopDISCC.Text)
        Dim discrp As Decimal = Val(tPopDISRP.Text)
        Dim jumlah As Decimal = Val(tPopJUMLAH.Text)

        ' Tambah ke grid
        TargetGrid.Rows.Add(kode, nama, gudang, jlh1, sat1, jlh2, sat2, jlh3, sat3,
                            harga, disca, discb, discc, discrp, jumlah)
    End Sub

    Private Sub btnPopTUTUP_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPopTUTUP.Click
        Me.Close()
    End Sub

    Private Sub cbPopGudang_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbPopGudang.SelectedIndexChanged
        If cbPopGudang.SelectedItem IsNot Nothing Then
            Dim fullText As String = cbPopGudang.SelectedItem.ToString()
            Dim kodegd As String = fullText.Split(" "c)(0)  ' ambil bagian kode
            ' contoh: "G01 Gudang Utama" → kodegd = "G01"
            ' bisa simpan ke variabel / label / textbox hidden
        End If
    End Sub

    Private Sub tPopKDBARANG_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles tPopKDBARANG.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True ' supaya bunyi beep Enter tidak muncul

            If tPopKDBARANG.Text.Trim() <> "" Then
                Dim f As New ItDtStok
                f.Owner = Me
                f.Show()
                f.LoadDataStok(tPopKDBARANG.Text.Trim())
            End If
        End If
    End Sub

    Private Sub tPopNMBARANG_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles tPopNMBARANG.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True

            If tPopNMBARANG.Text.Trim() <> "" Then
                Dim f As New ItDtStok
                f.Owner = Me
                f.Show()
                f.LoadDataStok(tPopNMBARANG.Text.Trim())
            End If
        End If
    End Sub

    ' === DISCA ===
    Private Sub tPopDISCA_KeyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs) Handles tPopDISCA.KeyPress
        AngkaHelper.HanyaAngka(e)
    End Sub


    ' === DISCB ===
    Private Sub tPopDISCB_KeyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs) Handles tPopDISCB.KeyPress
        AngkaHelper.HanyaAngka(e)
    End Sub


    ' === DISCC ===
    Private Sub tPopDISCC_KeyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs) Handles tPopDISCC.KeyPress
        AngkaHelper.HanyaAngka(e)
    End Sub


    ' === DISRP ===
    Private Sub tPopDISRP_KeyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs) Handles tPopDISRP.KeyPress
        AngkaHelper.HanyaAngka(e)
    End Sub


    ' === JLHA1 ===
    Private Sub tPopJLH1_KeyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs) Handles tPopJLH1.KeyPress
        AngkaHelper.HanyaAngka(e)
    End Sub


    ' === JLHA2 ===
    Private Sub tPopJLH2_KeyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs) Handles tPopJLH2.KeyPress
        AngkaHelper.HanyaAngka(e)
    End Sub


    ' === JLHA3 ===
    Private Sub tPopJLH3_KeyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs) Handles tPopJLH3.KeyPress
        AngkaHelper.HanyaAngka(e)
    End Sub


    ' === HARGA ===
    Private Sub tPopHARGA_KeyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs) Handles tPopHARGA.KeyPress
        AngkaHelper.HanyaAngka(e)
    End Sub


    ' === JUMLAH ===
    Private Sub tPopJUMLAH_KeyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs) Handles tPopJUMLAH.KeyPress
        AngkaHelper.HanyaAngka(e)
    End Sub
End Class