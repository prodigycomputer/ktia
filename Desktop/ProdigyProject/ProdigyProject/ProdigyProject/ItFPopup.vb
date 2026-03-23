Imports System.Data.Odbc

Public Class ItFPopup

    Public TargetGrid As DataGridView
    Public Property IsEditMode As Boolean = False

    Private FaktorIsi1 As Double = 1
    Private FaktorIsi2 As Double = 1

    Public Sub SetBarang(ByVal kode As String, ByVal nama As String,
                     ByVal sat1 As String, ByVal sat2 As String, ByVal sat3 As String,
                     ByVal isi1 As Double, ByVal isi2 As Double, ByVal harga As Double)

        tPopKDBARANG.Text = kode
        tPopNMBARANG.Text = nama
        tPopSTN1.Text = sat1
        tPopSTN2.Text = sat2
        tPopSTN3.Text = sat3
        tPopHARGA.Value = harga

        FaktorIsi1 = isi1
        FaktorIsi2 = isi2

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

    ' === CEK STOK ===
    Private Sub CekStok()
        Dim kodeBrg As String = tPopKDBARANG.Text.Trim()
        Dim kodeGd As String = ""

        If cbPopGudang.SelectedItem IsNot Nothing Then
            kodeGd = cbPopGudang.SelectedItem.ToString().Split(" "c)(0)
        End If

        tPopSTOK.Text = ModCekStok.GetStok(kodeBrg, kodeGd)
    End Sub

    Public Sub LoadBarangInfo(ByVal kodeBrg As String, Optional ByVal kodegd As String = "")

        If Conn Is Nothing OrElse Conn.State = ConnectionState.Closed Then
            BukaKoneksi()
        End If

        ' --- Barang ---
        Using cmd As New OdbcCommand("SELECT NamaBrg, Satuan1, Satuan2, Satuan3, Isi1, Isi2, HrgBeli FROM zstok WHERE KodeBrg = ?", Conn)
            cmd.Parameters.AddWithValue("?", kodeBrg)
            Using rd As OdbcDataReader = cmd.ExecuteReader()
                If rd.Read() Then
                    tPopNMBARANG.Text = rd("NamaBrg").ToString()
                    tPopSTN1.Text = rd("Satuan1").ToString()
                    tPopSTN2.Text = rd("Satuan2").ToString()
                    tPopSTN3.Text = rd("Satuan3").ToString()
                    tPopHARGA.Text = Val(rd("HrgBeli")).ToString()

                    FaktorIsi1 = If(IsDBNull(rd("Isi1")), 1, CDbl(rd("Isi1")))
                    FaktorIsi2 = If(IsDBNull(rd("Isi2")), 1, CDbl(rd("Isi2")))

                    tPopJLH2.Enabled = (FaktorIsi1 > 1)
                    tPopJLH3.Enabled = (FaktorIsi2 > 1)
                End If
            End Using
        End Using

        ' --- Gudang ---
        LoadGudang()

        If kodegd <> "" Then
            For i As Integer = 0 To cbPopGudang.Items.Count - 1
                Dim item As String = cbPopGudang.Items(i).ToString()
                Dim gdKode As String = item.Split(" "c)(0).Trim()

                If gdKode.ToUpper() = kodegd.Trim().ToUpper() Then
                    cbPopGudang.SelectedIndex = i
                    Exit For
                End If
            Next
        End If
        CekStok()
    End Sub

    Private Sub ClearInput()
        tPopKDBARANG.Clear()
        tPopNMBARANG.Clear()
        tPopJLH1.Value = 0
        tPopJLH2.Value = 0
        tPopJLH3.Value = 0
        tPopSTN1.Clear()
        tPopSTN2.Clear()
        tPopSTN3.Clear()
        tPopDISCA.Value = 0
        tPopDISCB.Value = 0
        tPopDISCC.Value = 0
        tPopDISCRP.Value = 0
        tPopJUMLAH.Value = 0
        tPopHARGA.Value = 0
        tPopSTOK.Clear()
        cbPopGudang.SelectedIndex = -1
        FaktorIsi1 = 1
        FaktorIsi2 = 1
        tPopJLH2.Enabled = (FaktorIsi1 > 1)
        tPopJLH3.Enabled = (FaktorIsi2 > 1)
    End Sub

    Private Sub HitungOtomatis()

        Dim hasil = ModHitung.HitungJumlah(
            tPopJLH1.Value,
            tPopJLH2.Value,
            tPopJLH3.Value,
            tPopHARGA.Value,
            tPopDISCA.Value,
            tPopDISCB.Value,
            tPopDISCC.Value,
            tPopDISCRP.Value,
        FaktorIsi1,
        FaktorIsi2
        )

        Dim finalValue As Decimal = hasil("final")

        If finalValue < tPopJUMLAH.Minimum Then
            finalValue = tPopJUMLAH.Minimum
        End If

        tPopJUMLAH.Value = finalValue
    End Sub

    Private Sub ItFPopupPem_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.FormBorderStyle = FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        AngkaHelper.AktifkanEnterPindah(Me)

        BukaKoneksi()
        If Not IsEditMode Then
            LoadGudang()
        End If

    End Sub


    Private Function DataBelumLengkap() As Boolean
        If tPopKDBARANG.Text.Trim() = "" Then Return True
        If tPopNMBARANG.Text.Trim() = "" Then Return True
        If tPopJLH1.Value = 0 OrElse tPopJLH1.Value <= 0 Then Return True
        If tPopHARGA.Value = 0 OrElse tPopHARGA.Value <= 0 Then Return True
        If cbPopGudang.SelectedIndex = -1 Then Return True
        If tPopJUMLAH.Value = 0 OrElse tPopJUMLAH.Value <= 0 Then Return True

        Return False
    End Function


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
        Try
            If DataBelumLengkap() Then
                MessageBox.Show("Data belum diisi", "Peringatan",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            If TargetGrid Is Nothing Then
                MessageBox.Show("Grid belum dihubungkan!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If

            ' --- Ambil data ---
            Dim kode As String = tPopKDBARANG.Text.Trim()
            Dim nama As String = tPopNMBARANG.Text.Trim()
            Dim gudang As String = ""
            If cbPopGudang.SelectedItem IsNot Nothing Then
                gudang = cbPopGudang.SelectedItem.ToString().Split(" "c)(0)
            End If

            Dim jlh1 As Decimal = tPopJLH1.Value
            Dim sat1 As String = tPopSTN1.Text.Trim()
            Dim jlh2 As Decimal = tPopJLH2.Value
            Dim sat2 As String = tPopSTN2.Text.Trim()
            Dim jlh3 As Decimal = tPopJLH3.Value
            Dim sat3 As String = tPopSTN3.Text.Trim()
            Dim harga As Decimal = tPopHARGA.Value
            Dim disca As Decimal = tPopDISCA.Value
            Dim discb As Decimal = tPopDISCB.Value
            Dim discc As Decimal = tPopDISCC.Value
            Dim discrp As Decimal = tPopDISCRP.Value
            Dim jumlah As Decimal = tPopJUMLAH.Value

            ' --- Masukkan ke grid ---
            If IsEditMode AndAlso TargetGrid.CurrentRow IsNot Nothing Then
                With TargetGrid.CurrentRow
                    .Cells("KodeBrg").Value = kode
                    .Cells("NamaBrg").Value = nama
                    .Cells("KodeGudang").Value = gudang
                    .Cells("Jlh1").Value = jlh1
                    .Cells("Sat1").Value = sat1
                    .Cells("Jlh2").Value = jlh2
                    .Cells("Sat2").Value = sat2
                    .Cells("Jlh3").Value = jlh3
                    .Cells("Sat3").Value = sat3
                    .Cells("Harga").Value = harga
                    .Cells("DiscA").Value = disca
                    .Cells("DiscB").Value = discb
                    .Cells("DiscC").Value = discc
                    .Cells("DiscRp").Value = discrp
                    .Cells("Jumlah").Value = jumlah
                End With
            Else
                TargetGrid.Rows.Add(kode, nama, gudang, jlh1, sat1, jlh2, sat2, jlh3, sat3,
                                    harga, disca, discb, discc, discrp, jumlah)
            End If

            ' --- Reset input untuk tambah berikutnya ---
            IsEditMode = False
            ClearInput()
            tPopKDBARANG.Focus()

        Catch ex As Exception
            MessageBox.Show("Gagal menambah item: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnPopTUTUP_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPopTUTUP.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub

    ' === EVENT: SAAT KODE BARANG SELESAI DIISI ===
    Private Sub tPopKDBARANG_Leave(ByVal sender As Object, ByVal e As EventArgs) Handles tPopKDBARANG.Leave
        CekStok()
    End Sub

    Private Sub cbPopGudang_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbPopGudang.SelectedIndexChanged
        If cbPopGudang.SelectedItem IsNot Nothing Then
            CekStok()
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
                Dim f As New ItStok
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
                Dim f As New ItStok
                f.Owner = Me
                f.Show()
                f.LoadDataStok(tPopNMBARANG.Text.Trim())
            End If
        End If
    End Sub

    ' === ValueChanged hitung otomatis ===
    Private Sub AutoHitung_ValueChanged(ByVal sender As Object, ByVal e As EventArgs) _
        Handles tPopHARGA.ValueChanged, tPopDISCA.ValueChanged, tPopDISCB.ValueChanged,
        tPopDISCC.ValueChanged, tPopDISCRP.ValueChanged
        HitungOtomatis()
    End Sub

    Private Sub ItFPopup_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If TypeOf Me.ActiveControl Is TextBox Then

            Select Case e.KeyCode
                Case Keys.Right
                    Me.SelectNextControl(Me.ActiveControl, True, True, True, True)

                Case Keys.Left
                    Me.SelectNextControl(Me.ActiveControl, False, True, True, True)

                Case Keys.Down
                    Me.SelectNextControl(Me.ActiveControl, True, True, True, True)

                Case Keys.Up
                    Me.SelectNextControl(Me.ActiveControl, False, True, True, True)
            End Select

        End If
    End Sub
End Class