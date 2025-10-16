Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Windows.Forms
Imports System.Data.Odbc

Public Class FReturPembelian
    Private TabPagesList As New List(Of TabPage)
    Private TabControls As New Dictionary(Of Integer, Dictionary(Of String, Control))
    Private TabButtonState As New Dictionary(Of Integer, String)
    Private TabLoadState As New Dictionary(Of Integer, Boolean)
    Private TabStatus As New Dictionary(Of Integer, String)

    Public Sub SetSupplier(ByVal kode As String, ByVal nama As String,
                       ByVal alamat As String, ByVal kota As String,
                       ByVal ktp As String, ByVal npwp As String)

        If TabControl1.SelectedTab Is Nothing Then Exit Sub

        ' Ambil nomor tab aktif
        Dim nomor As Integer = Integer.Parse(TabControl1.SelectedTab.Text.Replace("Nota ", ""))

        If TabControls.ContainsKey(nomor) Then
            Dim dict = TabControls(nomor)

            CType(dict("trpmKDSUP"), TextBox).Text = kode
            CType(dict("trpmNMSUP"), TextBox).Text = nama
            CType(dict("trpmALAMAT"), TextBox).Text = alamat
            ' kalau nanti mau dipakai, tinggal tambahkan dict("tpmKOTA"), dict("tpmKTP"), dict("tpmNPWP")
        End If
    End Sub

    Private Sub FReturPembelian_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' isi dropdown lunas
        trmuSLUNAS.Items.Clear()
        trmuSLUNAS.Items.Add(New With {.Value = 0, .Text = "Belum Lunas"})
        trmuSLUNAS.Items.Add(New With {.Value = 1, .Text = "Lunas"})
        trmuSLUNAS.DisplayMember = "Text"
        trmuSLUNAS.ValueMember = "Value"
        trmuSLUNAS.SelectedValue = 0   ' default Belum Lunas

        SetButtonState(Me, True)
        SetupGridPembelian(grRPEMBELIAN)

        ' Tambah context menu ke grid utama
        Dim cms As New ContextMenuStrip()
        cms.Items.Add("Ubah", Nothing, AddressOf GridUbah_Click)
        cms.Items.Add("Hapus", Nothing, AddressOf GridHapus_Click)
        grRPEMBELIAN.ContextMenuStrip = cms

        ' Registrasi tab manual pertama (Nota 1)
        If TabControl1.TabPages.Count > 0 Then
            Dim firstTab As TabPage = TabControl1.TabPages(0)
            TabPagesList.Add(firstTab)
            RegisterTabControls(firstTab, 1)
        End If
        DisabledLoad()
    End Sub

    Private Sub DisabledLoad()
        If TabControl1.SelectedTab Is Nothing Then Exit Sub
        Dim nomor As Integer = Integer.Parse(TabControl1.SelectedTab.Text.Replace("Nota ", ""))
        If Not TabControls.ContainsKey(nomor) Then Exit Sub
        Dim dict = TabControls(nomor)

        CType(dict("trpmNONOTA"), TextBox).Enabled = False
        CType(dict("trpmNOFAKTUR"), TextBox).Enabled = False
        CType(dict("trpmTANGGAL"), DateTimePicker).Enabled = False
        CType(dict("trpmTEMPO"), DateTimePicker).Enabled = False
        CType(dict("trpmKDSUP"), TextBox).Enabled = False
        CType(dict("trpmNMSUP"), TextBox).Enabled = False
        CType(dict("trpmKET"), TextBox).Enabled = False
        CType(dict("trpmALAMAT"), TextBox).Enabled = False
        CType(dict("GRID"), DataGridView).Enabled = False

        CType(dict("trpmTOTAL"), TextBox).Enabled = False
        CType(dict("trpmSUBTOTAL"), TextBox).Enabled = False
        CType(dict("trpmNPPN"), TextBox).Enabled = False
        CType(dict("trpmAPPN"), TextBox).Enabled = False
        CType(dict("trpmLAIN"), TextBox).Enabled = False
        CType(dict("trpmNDISK3"), TextBox).Enabled = False
        CType(dict("trpmADISK3"), TextBox).Enabled = False
        CType(dict("trpmNDISK2"), TextBox).Enabled = False
        CType(dict("trpmADISK2"), TextBox).Enabled = False
        CType(dict("trpmNDISK1"), TextBox).Enabled = False
        CType(dict("trpmADISK1"), TextBox).Enabled = False

        trpmSNONOTA.Enabled = True
        dtrpmTGL1.Enabled = True
        dtrpmTGL2.Enabled = True
        trpmSNMSUP.Enabled = True
        trmuSLUNAS.Enabled = True

        CType(dict("btnADDITEM"), Button).Enabled = False
    End Sub

    Private Sub EnabledLoad()
        If TabControl1.SelectedTab Is Nothing Then Exit Sub
        Dim nomor As Integer = Integer.Parse(TabControl1.SelectedTab.Text.Replace("Nota ", ""))
        If Not TabControls.ContainsKey(nomor) Then Exit Sub
        Dim dict = TabControls(nomor)

        CType(dict("trpmNONOTA"), TextBox).Enabled = True
        CType(dict("trpmNOFAKTUR"), TextBox).Enabled = True
        CType(dict("trpmTANGGAL"), DateTimePicker).Enabled = True
        CType(dict("trpmTEMPO"), DateTimePicker).Enabled = True
        CType(dict("trpmKDSUP"), TextBox).Enabled = True
        CType(dict("trpmNMSUP"), TextBox).Enabled = True
        CType(dict("trpmKET"), TextBox).Enabled = True
        CType(dict("trpmALAMAT"), TextBox).Enabled = False
        CType(dict("GRID"), DataGridView).Enabled = True

        CType(dict("trpmTOTAL"), TextBox).Enabled = False
        CType(dict("trpmSUBTOTAL"), TextBox).Enabled = False
        CType(dict("trpmNPPN"), TextBox).Enabled = False
        CType(dict("trpmAPPN"), TextBox).Enabled = True
        CType(dict("trpmLAIN"), TextBox).Enabled = True
        CType(dict("trpmNDISK3"), TextBox).Enabled = False
        CType(dict("trpmADISK3"), TextBox).Enabled = True
        CType(dict("trpmNDISK2"), TextBox).Enabled = False
        CType(dict("trpmADISK2"), TextBox).Enabled = True
        CType(dict("trpmNDISK1"), TextBox).Enabled = False
        CType(dict("trpmADISK1"), TextBox).Enabled = True

        trpmSNONOTA.Enabled = False
        dtrpmTGL1.Enabled = False
        dtrpmTGL2.Enabled = False
        trpmSNMSUP.Enabled = False
        trmuSLUNAS.Enabled = False

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
            Dim popup As New ItFPopup()
            Dim kodeBrg As String = row.Cells("KodeBrg").Value.ToString()
            Dim kodegd As String = row.Cells("KodeGudang").Value.ToString()

            ' --- Isi data popup dari grid ---
            popup.tPopKDBARANG.Text = kodeBrg
            popup.tPopJLH1.Text = Val(row.Cells("Jlh1").Value).ToString()
            popup.tPopJLH2.Text = Val(row.Cells("Jlh2").Value).ToString()
            popup.tPopJLH3.Text = Val(row.Cells("Jlh3").Value).ToString()
            popup.tPopDISCA.Text = Val(row.Cells("Disca").Value).ToString()
            popup.tPopDISCB.Text = Val(row.Cells("Discb").Value).ToString()
            popup.tPopDISCC.Text = Val(row.Cells("Discc").Value).ToString()
            popup.tPopDISRP.Text = Val(row.Cells("DiscRp").Value).ToString()

            ' --- Load info barang berdasarkan Kode & Gudang ---
            popup.TargetGrid = grid
            popup.IsEditMode = True
            popup.LoadBarangInfo(kodeBrg, kodegd)

            ' --- Tampilkan popup dan update data jika OK ---
            If popup.ShowDialog() = DialogResult.OK Then
                row.Cells("KodeBrg").Value = popup.tPopKDBARANG.Text
                row.Cells("KodeGudang").Value = If(popup.cbPopGudang.SelectedItem IsNot Nothing, popup.cbPopGudang.SelectedItem.ToString(), "")
                row.Cells("Jlh1").Value = Val(popup.tPopJLH1.Text)
                row.Cells("Jlh2").Value = Val(popup.tPopJLH2.Text)
                row.Cells("Jlh3").Value = Val(popup.tPopJLH3.Text)
                row.Cells("Disca").Value = Val(popup.tPopDISCA.Text)
                row.Cells("Discb").Value = Val(popup.tPopDISCB.Text)
                row.Cells("Discc").Value = Val(popup.tPopDISCC.Text)
                row.Cells("DiscRp").Value = Val(popup.tPopDISRP.Text)
                row.Cells("Jumlah").Value = Val(popup.tPopJUMLAH.Text)
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
                UpdateSubtotal(nomor)
            End If

        Catch ex As Exception
            MessageBox.Show("Gagal hapus item: " & ex.Message, "Error")
        End Try
    End Sub

    Public Sub LoadNota(ByVal nonota As String)
        Try
            BukaKoneksi()

            ' ================== HEADER ==================
            Dim sqlHead As String = "SELECT z.tgl, z.tgltempo, z.ket, z.nofaktur, z.kodesup, s.namasup, s.alamat, z.nilai, z.lunas, z.disc1, z.hdisc1, z.disc2, z.hdisc2, z.disc3, z.hdisc3, z.ppn, z.hppn, z.lainnya FROM zrbeli z LEFT JOIN zsupplier s ON z.kodesup = s.kodesup WHERE z.nonota = ?"
            Using cmd As New OdbcCommand(sqlHead, Conn)
                cmd.Parameters.AddWithValue("@nonota", nonota)
                Rd = cmd.ExecuteReader()
                If Rd.Read() Then
                    Dim aktifTab As TabPage = TabControl1.SelectedTab
                    Dim nomor As Integer = Integer.Parse(aktifTab.Text.Replace("Nota ", ""))
                    Dim dict = TabControls(nomor)

                    CType(dict("trpmNONOTA"), TextBox).Text = nonota
                    CType(dict("trpmNOFAKTUR"), TextBox).Text = Rd("nofaktur").ToString()
                    CType(dict("trpmTANGGAL"), DateTimePicker).Value = CDate(Rd("tgl"))
                    CType(dict("trpmTEMPO"), DateTimePicker).Value = CDate(Rd("tgltempo"))
                    CType(dict("trpmKET"), TextBox).Text = Rd("ket").ToString()
                    CType(dict("trpmKDSUP"), TextBox).Text = Rd("kodesup").ToString()
                    CType(dict("trpmNMSUP"), TextBox).Text = Rd("namasup").ToString()
                    CType(dict("trpmALAMAT"), TextBox).Text = Rd("alamat").ToString()
                    CType(dict("trpmTOTAL"), TextBox).Text = Rd("nilai").ToString()
                    CType(dict("trpmADISK1"), TextBox).Text = Rd("disc1").ToString()
                    CType(dict("trpmNDISK1"), TextBox).Text = Rd("hdisc1").ToString()
                    CType(dict("trpmADISK2"), TextBox).Text = Rd("disc2").ToString()
                    CType(dict("trpmNDISK2"), TextBox).Text = Rd("hdisc2").ToString()
                    CType(dict("trpmADISK3"), TextBox).Text = Rd("disc3").ToString()
                    CType(dict("trpmNDISK3"), TextBox).Text = Rd("hdisc3").ToString()
                    CType(dict("trpmAPPN"), TextBox).Text = Rd("ppn").ToString()
                    CType(dict("trpmNPPN"), TextBox).Text = Rd("hppn").ToString()
                    CType(dict("trpmLAIN"), TextBox).Text = Rd("lainnya").ToString()
                End If
                Rd.Close()
            End Using

            ' ================== DETAIL ==================
            Dim sqlDet As String = "SELECT d.kodebrg, s.namabrg, d.kodegd, d.jlh1, s.satuan1, d.jlh2, s.satuan2, d.jlh3, s.satuan3, d.harga, d.disca, d.discb, d.discc, d.discrp, d.jumlah FROM zbelim d LEFT JOIN zstok s ON d.kodebrg = s.kodebrg WHERE d.nonota = ?"
            Using cmdDet As New OdbcCommand(sqlDet, Conn)
                cmdDet.Parameters.AddWithValue("@nonota", nonota)
                Rd = cmdDet.ExecuteReader()

                Dim aktifTab As TabPage = TabControl1.SelectedTab
                Dim nomor As Integer = Integer.Parse(aktifTab.Text.Replace("Nota ", ""))
                Dim dict = TabControls(nomor)
                Dim grid As DataGridView = CType(dict("GRID"), DataGridView)

                If grid.Columns.Count = 0 Then
                    SetupGridPembelian(grid)
                End If

                grid.Rows.Clear()
                While Rd.Read()
                    grid.Rows.Add(
                        Rd("kodebrg").ToString(),
                        Rd("namabrg").ToString(),
                        Rd("kodegd").ToString(),
                        Rd("jlh1"),
                        Rd("satuan1").ToString(),
                        Rd("jlh2"),
                        Rd("satuan2").ToString(),
                        Rd("jlh3"),
                        Rd("satuan3").ToString(),
                        Rd("harga"),
                        Rd("disca"),
                        Rd("discb"),
                        Rd("discc"),
                        Rd("discrp"),
                        Rd("jumlah")
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
        dtrpmTGL1.Enabled = Not nonotaActive
        dtrpmTGL2.Enabled = Not nonotaActive
        trpmSNMSUP.Enabled = Not nonotaActive
        trmuSLUNAS.Enabled = Not nonotaActive
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

        dict("trpmTANGGAL") = tab.Controls.Find("trpmTANGGAL", True)(0)
        dict("trpmNONOTA") = tab.Controls.Find("trpmNONOTA", True)(0)
        dict("trpmNOFAKTUR") = tab.Controls.Find("trpmNOFAKTUR", True)(0)
        dict("trpmTEMPO") = tab.Controls.Find("trpmTEMPO", True)(0)
        dict("trpmKET") = tab.Controls.Find("trpmKET", True)(0)
        dict("trpmKDSUP") = tab.Controls.Find("trpmKDSUP", True)(0)
        dict("trpmNMSUP") = tab.Controls.Find("trpmNMSUP", True)(0)
        dict("trpmALAMAT") = tab.Controls.Find("trpmALAMAT", True)(0)
        dict("trpmTOTAL") = tab.Controls.Find("trpmTOTAL", True)(0)
        dict("trpmSUBTOTAL") = tab.Controls.Find("trpmSUBTOTAL", True)(0)
        dict("trpmNPPN") = tab.Controls.Find("trpmNPPN", True)(0)
        dict("trpmAPPN") = tab.Controls.Find("trpmAPPN", True)(0)
        dict("trpmLAIN") = tab.Controls.Find("trpmLAIN", True)(0)

        CType(dict("trpmAPPN"), TextBox).Text = "11"

        dict("trpmNDISK3") = tab.Controls.Find("trpmNDISK3", True)(0)
        dict("trpmADISK3") = tab.Controls.Find("trpmADISK3", True)(0)
        dict("trpmNDISK2") = tab.Controls.Find("trpmNDISK2", True)(0)
        dict("trpmADISK2") = tab.Controls.Find("trpmADISK2", True)(0)
        dict("trpmNDISK1") = tab.Controls.Find("trpmNDISK1", True)(0)
        dict("trpmADISK1") = tab.Controls.Find("trpmADISK1", True)(0)

        ' Grid
        Dim grid As DataGridView = CType(tab.Controls.Find("grRPEMBELIAN", True)(0), DataGridView)
        dict("GRID") = grid

        ' Context menu khusus untuk grid ini
        Dim cms As New ContextMenuStrip()
        cms.Items.Add("Ubah", Nothing, AddressOf GridUbah_Click)
        cms.Items.Add("Hapus", Nothing, AddressOf GridHapus_Click)
        grid.ContextMenuStrip = cms
        dict("btnADDITEM") = tab.Controls.Find("btnADDITEM", True)(0)

        TabControls(nomor) = dict

        ' === Tambahkan event subtotal otomatis ===
        AddHandler grid.CellValueChanged, Sub(s, e)
                                              UpdateSubtotal(nomor)
                                          End Sub
        AddHandler grid.RowsRemoved, Sub(s, e)
                                         UpdateSubtotal(nomor)
                                     End Sub
        AddHandler grid.RowsAdded, Sub(s, e)
                                       UpdateSubtotal(nomor)
                                   End Sub

        ' Tambahkan event handler dinamis
        AddHandler CType(dict("trpmKDSUP"), TextBox).KeyDown, AddressOf trpmKDSUP_KeyDown
        AddHandler CType(dict("trpmNMSUP"), TextBox).KeyDown, AddressOf trpmNMSUP_KeyDown

        Dim txtADISK1 As TextBox = CType(dict("trpmADISK1"), TextBox)
        Dim txtADISK2 As TextBox = CType(dict("trpmADISK2"), TextBox)
        Dim txtADISK3 As TextBox = CType(dict("trpmADISK3"), TextBox)
        Dim txtAPPN As TextBox = CType(dict("trpmAPPN"), TextBox)
        Dim txtLAIN As TextBox = CType(dict("trpmLAIN"), TextBox)

        Dim allTextBoxes() As TextBox = {txtADISK1, txtADISK2, txtADISK3, txtAPPN, txtLAIN}

        For Each tb As TextBox In allTextBoxes
            ' Saat pindah field
            AddHandler tb.Leave, Sub() HitungOtomatisTotal(nomor)

            ' Saat Enter ditekan
            AddHandler tb.KeyDown, Sub(sender As Object, e As KeyEventArgs)
                                       If e.KeyCode = Keys.Enter Then
                                           HitungOtomatisTotal(nomor)
                                       End If
                                   End Sub
        Next

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
    Private Sub brpmTabAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles brpmTabAdd.Click
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
    Private Sub brpmTabDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles brpmTabDel.Click
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

            MRPemSimpan.SimpanPembelian(nomor, dict, status)

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

        ' ambil kontrol tpmNONOTA dari tab aktif
        Dim trpmNONOTA As TextBox = TryCast(TabControls(nomor)("trpmNONOTA"), TextBox)
        Dim trpmNOFAKTUR As TextBox = TryCast(TabControls(nomor)("trpmNOFAKTUR"), TextBox)

        ' cek apakah kosong
        If trpmNONOTA Is Nothing OrElse String.IsNullOrWhiteSpace(trpmNONOTA.Text) Then
            MessageBox.Show("Tidak ada data yang bisa di edit", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        ' cek apakah nomor faktur kosong
        If trpmNOFAKTUR Is Nothing OrElse String.IsNullOrWhiteSpace(trpmNOFAKTUR.Text) Then
            MessageBox.Show("Nomor faktur belum diisi, silakan tambahkan data terlebih dahulu.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
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

            Dim nonota As String = CType(dict("trpmNONOTA"), TextBox).Text.Trim()

            If String.IsNullOrEmpty(nonota) Then
                MessageBox.Show("Tidak ada nota yang bisa dihapus.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            If MessageBox.Show("Yakin hapus nota " & nonota & " ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then
                Exit Sub
            End If

            ' === Hapus data dari database (module) ===
            MRPemSimpan.HapusPembelian(nonota, dict)

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

    Private Sub UpdateSubtotal(ByVal nomor As Integer)
        If Not TabControls.ContainsKey(nomor) Then Exit Sub

        Dim dict = TabControls(nomor)
        Dim grid As DataGridView = CType(dict("GRID"), DataGridView)
        Dim subtotal As Decimal = 0

        For Each row As DataGridViewRow In grid.Rows
            If row.IsNewRow Then Continue For
            If row.Cells("Jumlah").Value IsNot Nothing Then
                subtotal += Val(row.Cells("Jumlah").Value)
            End If
        Next

        CType(dict("trpmSUBTOTAL"), TextBox).Text = subtotal.ToString()
    End Sub

    Private Sub btnADDITEM_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnADDITEM.Click
        Try
            ' --- Ambil tab aktif ---
            Dim aktifTab As TabPage = TabControl1.SelectedTab
            Dim nomor As Integer = Integer.Parse(aktifTab.Text.Replace("Nota ", ""))
            Dim dict = TabControls(nomor)

            ' --- Ambil grid di tab aktif ---
            Dim grid As DataGridView = CType(dict("GRID"), DataGridView)

            Dim popup As New ItFPopup()
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

    ' ================== KEYDOWN SUPPLIER ==================
    Private Sub trpmKDSUP_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs)
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            Dim txtKode As TextBox = CType(sender, TextBox)
            If txtKode.Text.Trim() <> "" Then
                Dim f As New ItDtSupplier
                f.Owner = Me
                f.Show()
                f.LoadDataSup(txtKode.Text.Trim())
            End If
        End If
    End Sub

    Private Sub trpmNMSUP_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs)
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            Dim txtNama As TextBox = CType(sender, TextBox)
            If txtNama.Text.Trim() <> "" Then
                Dim f As New ItDtSupplier
                f.Owner = Me
                f.Show()
                f.LoadDataSup(txtNama.Text.Trim())
            End If
        End If
    End Sub

    Private Sub btnCARI_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCARI.Click
        Dim f As New ItDtRPembelian()

        Dim nonota As String = trpmSNONOTA.Text.Trim()
        Dim tgl1 As String = dtrpmTGL1.Value.ToString("yyyy-MM-dd")
        Dim tgl2 As String = dtrpmTGL2.Value.ToString("yyyy-MM-dd")
        Dim namasup As String = trpmSNMSUP.Text.Trim()
        Dim status As String = ""

        ' Tentukan status lunas hanya jika nonota kosong
        If nonota = "" Then
            If trmuSLUNAS.SelectedIndex = 0 Then
                status = "belum"
            ElseIf trmuSLUNAS.SelectedIndex = 1 Then
                status = "lunas"
            End If
        End If

        ' Load data sesuai filter
        f.Owner = Me
        f.Show()
        f.LoadDataRBeli(nonota, tgl1, tgl2, namasup, status)

        ' === Clear filter setelah pencarian ===
        trpmSNONOTA.Clear()
        trpmSNMSUP.Clear()
        trmuSLUNAS.SelectedIndex = -1 ' reset pilihan
        ' reset tanggal ke hari ini
        dtrpmTGL1.Value = DateTime.Today
        dtrpmTGL2.Value = DateTime.Today
    End Sub

    Private Sub trmuSLUNAS_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles trmuSLUNAS.SelectedIndexChanged
        If trpmSNONOTA.Text.Trim() = "" Then
            Dim cb As ComboBox = CType(sender, ComboBox)
            Dim val As Integer = Convert.ToInt32(cb.SelectedValue)
            btnCARI.PerformClick()
        End If
    End Sub

    Private Sub trpmSNONOTA_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles trpmSNONOTA.TextChanged
        If trpmSNONOTA.Text.Trim() <> "" Then
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

    Private Sub HitungOtomatisTotal(ByVal nomor As Integer)
        If Not TabControls.ContainsKey(nomor) Then Exit Sub
        Dim dict = TabControls(nomor)

        Dim subtotal As Decimal = Val(CType(dict("trpmSUBTOTAL"), TextBox).Text)
        Dim adisk1 As Decimal = Val(CType(dict("trpmADISK1"), TextBox).Text)
        Dim adisk2 As Decimal = Val(CType(dict("trpmADISK2"), TextBox).Text)
        Dim adisk3 As Decimal = Val(CType(dict("trpmADISK3"), TextBox).Text)
        Dim appn As Decimal = Val(CType(dict("trpmAPPN"), TextBox).Text)
        Dim lain As Decimal = Val(CType(dict("trpmLAIN"), TextBox).Text)

        Dim hasil = ModHitung.HitungSubtotalTotal(subtotal, adisk1, adisk2, adisk3, appn, lain)

        CType(dict("trpmNDISK1"), TextBox).Text = hasil("hdisca").ToString()
        CType(dict("trpmNDISK2"), TextBox).Text = hasil("hdiscb").ToString()
        CType(dict("trpmNDISK3"), TextBox).Text = hasil("hdiscc").ToString()
        CType(dict("trpmNPPN"), TextBox).Text = hasil("ppn").ToString()
        CType(dict("trpmTOTAL"), TextBox).Text = hasil("total").ToString()
    End Sub

    Private Sub OnlyNumber_KeyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs) _
        Handles trpmSUBTOTAL.KeyPress, trpmTOTAL.KeyPress, trpmADISK1.KeyPress,
                trpmADISK2.KeyPress, trpmADISK3.KeyPress, trpmNDISK1.KeyPress,
                trpmNDISK2.KeyPress, trpmNDISK3.KeyPress, trpmLAIN.KeyPress,
                trpmAPPN.KeyPress, trpmNPPN.KeyPress

        AngkaHelper.HanyaAngka(e)
    End Sub

    Private Sub btnPRINT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPRINT.Click
        Try
            Dim aktifTab As TabPage = TabControl1.SelectedTab
            Dim nomor As Integer = Integer.Parse(aktifTab.Text.Replace("Nota ", ""))
            Dim dict = TabControls(nomor)

            Dim nonota As String = CType(dict("trpmNONOTA"), TextBox).Text

            If String.IsNullOrEmpty(nonota) Then
                MessageBox.Show("Pilih nota yang akan dicetak!", "Info")
                Exit Sub
            End If

            ' --- Buka form cetak ---
            Dim f As New FCetak()
            f.Param("nonota") = nonota
            f.Param("jenis") = "rpembelian"
            f.ShowDialog()

        Catch ex As Exception
            MessageBox.Show("Gagal cetak nota: " & ex.Message, "Error")
        End Try
    End Sub
End Class