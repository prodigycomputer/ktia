﻿Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Windows.Forms
Imports System.Data.Odbc

Public Class FMutasi
    Private TabPagesList As New List(Of TabPage)
    Private TabControls As New Dictionary(Of Integer, Dictionary(Of String, Control))
    Private TabButtonState As New Dictionary(Of Integer, String)
    Private TabLoadState As New Dictionary(Of Integer, Boolean)
    Private TabStatus As New Dictionary(Of Integer, String)

    Private Sub LoadGudang()
        Dim sql As String = "SELECT kodegd, namagd FROM zgudang ORDER BY kodegd"

        Try
            cbmuGD1.Items.Clear()
            cbmuGD2.Items.Clear()

            Cmd = New OdbcCommand(sql, Conn)
            Rd = Cmd.ExecuteReader()

            While Rd.Read()
                Dim kode As String = Rd("kodegd").ToString()
                Dim nama As String = Rd("namagd").ToString()
                Dim itemText As String = kode & " " & nama

                cbmuGD1.Items.Add(itemText)
                cbmuGD2.Items.Add(itemText)
            End While

            Rd.Close()
        Catch ex As Exception
            MessageBox.Show("Gagal load gudang: " & ex.Message)
        End Try
    End Sub

    Private Sub FMutasi_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetButtonState(Me, True)
        SetupGridMUTASI(grMUTASI)

        ' Tambah context menu ke grid utama
        Dim cms As New ContextMenuStrip()
        cms.Items.Add("Ubah", Nothing, AddressOf GridUbah_Click)
        cms.Items.Add("Hapus", Nothing, AddressOf GridHapus_Click)
        grMUTASI.ContextMenuStrip = cms

        ' Registrasi tab manual pertama (Nota 1)
        If TabControl1.TabPages.Count > 0 Then
            Dim firstTab As TabPage = TabControl1.TabPages(0)
            TabPagesList.Add(firstTab)
            RegisterTabControls(firstTab, 1)
        End If
        BukaKoneksi()
        LoadGudang()
        DisabledLoad()
    End Sub

    Private Sub DisabledLoad()
        If TabControl1.SelectedTab Is Nothing Then Exit Sub
        Dim nomor As Integer = Integer.Parse(TabControl1.SelectedTab.Text.Replace("Nota ", ""))
        If Not TabControls.ContainsKey(nomor) Then Exit Sub
        Dim dict = TabControls(nomor)

        CType(dict("tmuNONOTA"), TextBox).Enabled = False
        CType(dict("tmuTANGGAL"), DateTimePicker).Enabled = False
        CType(dict("cbmuGD1"), ComboBox).Enabled = False
        CType(dict("cbmuGD2"), ComboBox).Enabled = False
        CType(dict("GRID"), DataGridView).Enabled = False

        tmuSNONOTA.Enabled = True
        dtmuTGL.Enabled = True
        tmuSGD1.Enabled = True
        tmuSGD2.Enabled = True

        CType(dict("btnADDITEM"), Button).Enabled = False
    End Sub

    Private Sub EnabledLoad()
        If TabControl1.SelectedTab Is Nothing Then Exit Sub
        Dim nomor As Integer = Integer.Parse(TabControl1.SelectedTab.Text.Replace("Nota ", ""))
        If Not TabControls.ContainsKey(nomor) Then Exit Sub
        Dim dict = TabControls(nomor)

        CType(dict("tmuNONOTA"), TextBox).Enabled = True
        CType(dict("tmuTANGGAL"), DateTimePicker).Enabled = True
        CType(dict("cbmuGD1"), ComboBox).Enabled = True
        CType(dict("cbmuGD2"), ComboBox).Enabled = True
        CType(dict("GRID"), DataGridView).Enabled = True

        tmuSNONOTA.Enabled = False
        dtmuTGL.Enabled = False
        tmuSGD1.Enabled = False
        tmuSGD2.Enabled = False

        CType(dict("btnADDITEM"), Button).Enabled = True
    End Sub

    ' ============= EVENT KLIK UBAH (UNIVERSAL PER TAB) =============
    Private Sub GridUbah_Click(ByVal sender As Object, ByVal e As EventArgs)
        Try
            ' --- Ambil tab aktif ---
            Dim aktifTab As TabPage = TabControl1.SelectedTab
            Dim nomor As Integer = Integer.Parse(aktifTab.Text.Replace("Nota ", ""))

            ' --- Ambil dictionary kontrol tab tersebut ---
            Dim dict = TabControls(nomor)
            Dim grid As DataGridView = CType(dict("GRID"), DataGridView)

            ' --- Pastikan ada baris yang dipilih ---
            If grid.CurrentRow Is Nothing Then Return
            Dim row As DataGridViewRow = grid.CurrentRow

            ' --- Siapkan popup ---
            Dim popup As New ItFPopupMut()
            Dim kodeBrg As String = row.Cells("KodeBrg").Value.ToString()

            ' --- Isi data popup dari grid ---
            popup.tPopKDBARANG.Text = kodeBrg
            popup.tPopJLH1.Text = Val(row.Cells("Jlh1").Value).ToString()
            popup.tPopJLH2.Text = Val(row.Cells("Jlh2").Value).ToString()
            popup.tPopJLH3.Text = Val(row.Cells("Jlh3").Value).ToString()

            ' --- Load info barang berdasarkan Kode & Gudang ---
            popup.TargetGrid = grid
            popup.IsEditMode = True
            popup.LoadBarangInfo(kodeBrg)

            ' --- Tampilkan popup dan update data jika OK ---
            If popup.ShowDialog() = DialogResult.OK Then
                row.Cells("KodeBrg").Value = popup.tPopKDBARANG.Text
                row.Cells("Jlh1").Value = Val(popup.tPopJLH1.Text)
                row.Cells("Jlh2").Value = Val(popup.tPopJLH2.Text)
                row.Cells("Jlh3").Value = Val(popup.tPopJLH3.Text)
            End If

        Catch ex As Exception
            MessageBox.Show("Gagal ubah item: " & ex.Message, "Error")
        End Try
    End Sub


    ' ============= EVENT KLIK HAPUS (UNIVERSAL PER TAB) =============
    Private Sub GridHapus_Click(ByVal sender As Object, ByVal e As EventArgs)
        Try
            ' --- Ambil tab aktif ---
            Dim aktifTab As TabPage = TabControl1.SelectedTab
            Dim nomor As Integer = Integer.Parse(aktifTab.Text.Replace("Nota ", ""))

            ' --- Ambil dictionary kontrol tab ---
            Dim dict = TabControls(nomor)
            Dim grid As DataGridView = CType(dict("GRID"), DataGridView)

            ' --- Pastikan ada baris yang valid ---
            If grid.CurrentRow Is Nothing OrElse grid.CurrentRow.IsNewRow Then
                MessageBox.Show("Pilih item yang valid!", "Info")
                Exit Sub
            End If

            ' --- Konfirmasi hapus ---
            If MessageBox.Show("Hapus item ini?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                grid.Rows.Remove(grid.CurrentRow)
            End If

        Catch ex As Exception
            MessageBox.Show("Gagal hapus item: " & ex.Message, "Error")
        End Try
    End Sub

    Public Sub LoadNota(ByVal nonota As String)
        Try
            BukaKoneksi()

            ' --- HEADER ---
            Dim sqlHead As String = "SELECT z.tgl, z.kodegd1, z.kodegd2 FROM zmutasi z WHERE z.nonota = ?"
            Using cmd As New OdbcCommand(sqlHead, Conn)
                cmd.Parameters.AddWithValue("@nonota", nonota)
                Rd = cmd.ExecuteReader()
                If Rd.Read() Then
                    Dim aktifTab As TabPage = TabControl1.SelectedTab
                    Dim nomor As Integer = Integer.Parse(aktifTab.Text.Replace("Nota ", ""))
                    Dim dict = TabControls(nomor)

                    CType(dict("tmuNONOTA"), TextBox).Text = nonota
                    CType(dict("tmuTANGGAL"), DateTimePicker).Value = CDate(Rd("tgl"))

                    Dim cb1 As ComboBox = CType(dict("cbmuGD1"), ComboBox)
                    Dim cb2 As ComboBox = CType(dict("cbmuGD2"), ComboBox)
                    Dim kodegd1 As String = Rd("kodegd1").ToString().Trim()
                    Dim kodegd2 As String = Rd("kodegd2").ToString().Trim()

                    cb1.SelectedIndex = cb1.FindString(kodegd1)
                    cb2.SelectedIndex = cb2.FindString(kodegd2)
                End If
                Rd.Close()
            End Using

            ' --- DETAIL ---
            Dim sqlDet As String = "SELECT d.kodebrg, s.namabrg, d.jlh1, s.satuan1, d.jlh2, s.satuan2, d.jlh3, s.satuan3 FROM zmutasim d LEFT JOIN zstok s ON d.kodebrg = s.kodebrg WHERE d.nonota = ?"
            Using cmdDet As New OdbcCommand(sqlDet, Conn)
                cmdDet.Parameters.AddWithValue("@nonota", nonota)
                Rd = cmdDet.ExecuteReader()

                Dim aktifTab As TabPage = TabControl1.SelectedTab
                Dim nomor As Integer = Integer.Parse(aktifTab.Text.Replace("Nota ", ""))
                Dim dict = TabControls(nomor)
                Dim grid As DataGridView = CType(dict("GRID"), DataGridView)

                If grid.Columns.Count = 0 Then
                    SetupGridMutasi(grid)
                End If

                grid.Rows.Clear()
                While Rd.Read()
                    grid.Rows.Add(
                        Rd("kodebrg").ToString(),
                        Rd("namabrg").ToString(),
                        Rd("jlh1"),
                        Rd("satuan1").ToString(),
                        Rd("jlh2"),
                        Rd("satuan2").ToString(),
                        Rd("jlh3"),
                        Rd("satuan3").ToString()
                    )
                End While
                Rd.Close()
            End Using
            Conn.Close()

        Catch ex As Exception
            MessageBox.Show("Gagal load nota: " & ex.Message, "Error")
        End Try
    End Sub

    Private Sub SetFilterState(ByVal nonotaActive As Boolean)
        dtmuTGL.Enabled = Not nonotaActive
        tmuSGD1.Enabled = Not nonotaActive
        tmuSGD2.Enabled = Not nonotaActive
    End Sub

    ' ----------------- STATUS HELPERS -----------------
    Private Sub SetTabStatus(ByVal nomor As Integer, ByVal status As String)
        If TabStatus.ContainsKey(nomor) Then
            TabStatus(nomor) = status
        Else
            TabStatus.Add(nomor, status)
        End If

        ' simpan juga di Tag TabPage (tidak mengubah Text)
        For Each tp As TabPage In TabControl1.TabPages
            If tp.Text = "Nota " & nomor Then
                tp.Tag = status
                Exit For
            End If
        Next
    End Sub

    Private Function GetTabStatus(ByVal nomor As Integer) As String
        If TabStatus.ContainsKey(nomor) Then
            Return TabStatus(nomor)
        End If
        Return String.Empty
    End Function

    ' ================== REGISTER TAB ==================
    Private Sub RegisterTabControls(ByVal tab As TabPage, ByVal nomor As Integer)
        Dim dict As New Dictionary(Of String, Control)

        dict("tmuTANGGAL") = tab.Controls.Find("tmuTANGGAL", True)(0)
        dict("tmuNONOTA") = tab.Controls.Find("tmuNONOTA", True)(0)
        dict("cbmuGD1") = tab.Controls.Find("cbmuGD1", True)(0)
        dict("cbmuGD2") = tab.Controls.Find("cbmuGD2", True)(0)

        ' Grid
        Dim grid As DataGridView = CType(tab.Controls.Find("grMUTASI", True)(0), DataGridView)
        dict("GRID") = grid

        ' Context menu khusus untuk grid ini
        Dim cms As New ContextMenuStrip()
        cms.Items.Add("Ubah", Nothing, AddressOf GridUbah_Click)
        cms.Items.Add("Hapus", Nothing, AddressOf GridHapus_Click)
        grid.ContextMenuStrip = cms
        dict("btnADDITEM") = tab.Controls.Find("btnADDITEM", True)(0)

        TabControls(nomor) = dict

        ' Event tombol Add Item
        AddHandler CType(dict("btnADDITEM"), Button).Click, AddressOf btnADDITEM_Click

        If TabStatus.ContainsKey(nomor) Then
            TabStatus(nomor) = String.Empty
        Else
            TabStatus.Add(nomor, String.Empty)
        End If
        tab.Tag = String.Empty
    End Sub

    ' ================== TAMBAH TAB ==================
    Private Sub bmuTabAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bmuTabAdd.Click
        If TabPagesList.Count = 0 Then Return

        Dim nomor As Integer = GetNextNotaNumber(TabControl1)
        Dim nama As String = "Nota " & nomor
        Dim template As TabPage = TabControl1.SelectedTab
        Dim newTab As New TabPage(nama)

        For Each ctrl As Control In template.Controls
            newTab.Controls.Add(CloneControl(ctrl))
            DisabledLoad()
        Next

        TabControl1.TabPages.Add(newTab)
        TabPagesList.Add(newTab)
        RegisterTabControls(newTab, nomor)
        SetTabStatus(nomor, String.Empty)
        TabControl1.SelectedTab = newTab
        TabLoadState(nomor) = True
        TabButtonState(nomor) = True
        SetButtonState(Me, True)

    End Sub

    ' ================== HAPUS TAB ==================
    Private Sub bmuTabDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bmuTabDel.Click
        If TabPagesList.Count > 1 Then
            Dim aktifIndex As Integer = TabControl1.SelectedIndex
            Dim aktifTab As TabPage = TabControl1.SelectedTab
            Dim nomor As Integer = Integer.Parse(aktifTab.Text.Replace("Nota ", ""))

            TabControl1.TabPages.RemoveAt(aktifIndex)
            TabPagesList.RemoveAt(aktifIndex)
            TabControls.Remove(nomor)

            If TabButtonState.ContainsKey(nomor) Then
                TabButtonState.Remove(nomor)
            End If
        Else
            MessageBox.Show("Minimal 1 tab harus ada!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Sub btnSIMPAN_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSIMPAN.Click
        Try
            Dim aktifTab As TabPage = TabControl1.SelectedTab
            Dim nomor As Integer = Integer.Parse(aktifTab.Text.Replace("Nota ", ""))
            Dim dict = TabControls(nomor)
            Dim status As String = GetTabStatus(nomor)

            MMutSimpan.SimpanMutasi(nomor, dict, status)

            MessageBox.Show("Data pembelian berhasil disimpan untuk " & aktifTab.Text, "Sukses")

            TabLoadState(nomor) = True
            TabButtonState(nomor) = True
            SetButtonState(Me, True)
            DisabledLoad()

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error Simpan")
        End Try
    End Sub

    Private Sub btnTAMBAH_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTAMBAH.Click
        Dim nomor As Integer = Integer.Parse(TabControl1.SelectedTab.Text.Replace("Nota ", ""))

        If Not TabControls.ContainsKey(nomor) Then
            RegisterTabControls(TabControl1.SelectedTab, nomor)
        End If

        Dim dict = TabControls(nomor)
        ClearForm(dict)

        ' set status
        SetTabStatus(nomor, "tambah")

        TabLoadState(nomor) = False
        TabButtonState(nomor) = False
        SetButtonState(Me, False)
        EnabledLoad()
    End Sub

    Private Sub btnUBAH_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUBAH.Click
        Dim nomor As Integer = Integer.Parse(TabControl1.SelectedTab.Text.Replace("Nota ", ""))

        If Not TabControls.ContainsKey(nomor) Then
            RegisterTabControls(TabControl1.SelectedTab, nomor)
        End If

        ' ambil kontrol tmuNONOTA dari tab aktif
        Dim tmuNONOTA As TextBox = TryCast(TabControls(nomor)("tmuNONOTA"), TextBox)

        ' cek apakah kosong
        If tmuNONOTA Is Nothing OrElse String.IsNullOrWhiteSpace(tmuNONOTA.Text) Then
            MessageBox.Show("Tidak ada data yang bisa di edit", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        ' set status
        SetTabStatus(nomor, "ubah")

        TabLoadState(nomor) = False
        TabButtonState(nomor) = False
        SetButtonState(Me, False)
        EnabledLoad()
    End Sub

    Private Sub btnHAPUS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHAPUS.Click
        Try
            Dim aktifTab As TabPage = TabControl1.SelectedTab
            Dim nomor As Integer = Integer.Parse(aktifTab.Text.Replace("Nota ", ""))

            If Not TabControls.ContainsKey(nomor) Then Exit Sub
            Dim dict = TabControls(nomor)

            Dim nonota As String = CType(dict("tmuNONOTA"), TextBox).Text.Trim()

            If String.IsNullOrEmpty(nonota) Then
                MessageBox.Show("Tidak ada nota yang bisa dihapus.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            If MessageBox.Show("Yakin hapus nota " & nonota & " ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then
                Exit Sub
            End If

            ' === Hapus data dari database (module) ===
            MPemSimpan.HapusPembelian(nonota, dict)

            ' === Update status tab setelah hapus ===
            TabLoadState(nomor) = True
            TabButtonState(nomor) = True
            SetButtonState(Me, True)
            DisabledLoad()

            MessageBox.Show("Nota " & nonota & " berhasil dihapus.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error Hapus", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnBATAL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBATAL.Click
        Dim nomor As Integer = Integer.Parse(TabControl1.SelectedTab.Text.Replace("Nota ", ""))
        TabLoadState(nomor) = True
        TabButtonState(nomor) = True
        SetButtonState(Me, True)
        DisabledLoad()
    End Sub

    Private Sub btnADDITEM_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnADDITEM.Click
        Try
            ' --- Ambil tab aktif ---
            Dim aktifTab As TabPage = TabControl1.SelectedTab
            Dim nomor As Integer = Integer.Parse(aktifTab.Text.Replace("Nota ", ""))
            Dim dict = TabControls(nomor)

            ' --- Ambil grid di tab aktif ---
            Dim grid As DataGridView = CType(dict("GRID"), DataGridView)

            Dim popup As New ItFPopupMut()
            popup.TargetGrid = grid
            If popup.ShowDialog(Me) = DialogResult.OK Then
                ' Fokus ke row terakhir
                If grid.Rows.Count > 0 Then
                    grid.CurrentCell = grid.Rows(grid.Rows.Count - 1).Cells(0)
                    grid.BeginEdit(True)
                End If
            End If

        Catch ex As Exception
            MessageBox.Show("Gagal menambahkan item: " & ex.Message, "Error")
        End Try
    End Sub

    Private Sub btnCARI_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCARI.Click
        Dim f As New ItDtMutasi()

        Dim nonota As String = tmuSNONOTA.Text.Trim()
        Dim tgl As String = dtmuTGL.Value.ToString("yyyy-MM-dd")
        Dim namagd1 As String = tmuSGD1.Text.Trim()
        Dim namagd2 As String = tmuSGD2.Text.Trim()

        ' Load data sesuai filter
        f.Owner = Me
        f.Show()
        f.LoadDataMutasi(nonota, tgl, namagd1, namagd2)

        ' === Clear filter setelah pencarian ===
        tmuSNONOTA.Clear()
        tmuSGD1.Clear()
        tmuSGD2.Clear()

        ' reset tanggal ke hari ini
        dtmuTGL.Value = DateTime.Today
    End Sub

    Private Sub tmuSNONOTA_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmuSNONOTA.TextChanged
        If tmuSNONOTA.Text.Trim() <> "" Then
            ' kalau nonota diisi, disable yang lain
            SetFilterState(True)
        Else
            ' kalau nonota kosong, enable semua lagi
            SetFilterState(False)
        End If
    End Sub

    Private Sub TabControl1_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TabControl1.SelectedIndexChanged
        If TabControl1.SelectedTab Is Nothing Then Exit Sub

        Dim nomor As Integer
        If Not Integer.TryParse(TabControl1.SelectedTab.Text.Replace("Nota ", ""), nomor) Then Exit Sub

        ' Pastikan ada entri pada dictionaries (jika belum, inisialisasi default)
        If Not TabButtonState.ContainsKey(nomor) Then
            TabButtonState(nomor) = True
        End If
        If Not TabLoadState.ContainsKey(nomor) Then
            ' Asumsikan load state default = True (data sudah load / readonly)
            TabLoadState(nomor) = True
        End If

        ' === handle tombol ===
        If TabLoadState(nomor) Then
            DisabledLoad()
        Else
            EnabledLoad()
        End If

        SetButtonState(Me, TabButtonState(nomor))
    End Sub

    Private Sub btnPRINT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPRINT.Click
        Try
            Dim aktifTab As TabPage = TabControl1.SelectedTab
            Dim nomor As Integer = Integer.Parse(aktifTab.Text.Replace("Nota ", ""))
            Dim dict = TabControls(nomor)

            Dim nonota As String = CType(dict("tmuNONOTA"), TextBox).Text

            If String.IsNullOrEmpty(nonota) Then
                MessageBox.Show("Pilih nota yang akan dicetak!", "Info")
                Exit Sub
            End If

            ' --- Buka form cetak ---
            Dim f As New FCetak()
            f.Param("nonota") = nonota
            f.Param("jenis") = "mutasi"
            f.ShowDialog()

        Catch ex As Exception
            MessageBox.Show("Gagal cetak nota: " & ex.Message, "Error")
        End Try
    End Sub
End Class