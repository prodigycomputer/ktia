Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Windows.Forms
Imports System.Data.Odbc

Public Class FPembelian
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

            CType(dict("tpmKDSUP"), TextBox).Text = kode
            CType(dict("tpmNMSUP"), TextBox).Text = nama
            CType(dict("tpmALAMAT"), TextBox).Text = alamat
            ' kalau nanti mau dipakai, tinggal tambahkan dict("tpmKOTA"), dict("tpmKTP"), dict("tpmNPWP")
        End If
    End Sub


    ' ================== FORM LOAD ==================
    Private Sub FPembelian_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        ' isi dropdown lunas
        tmuSLUNAS.Items.Clear()
        tmuSLUNAS.Items.Add(New With {.Value = 0, .Text = "Belum Lunas"})
        tmuSLUNAS.Items.Add(New With {.Value = 1, .Text = "Lunas"})
        tmuSLUNAS.DisplayMember = "Text"
        tmuSLUNAS.ValueMember = "Value"
        tmuSLUNAS.SelectedValue = 0   ' default Belum Lunas

        SetButtonState(Me, True)
        SetupGridPembelian(grPEMBELIAN)

        ' Tambah context menu ke grid utama
        Dim cms As New ContextMenuStrip()
        cms.Items.Add("Ubah", Nothing, AddressOf GridUbah_Click)
        cms.Items.Add("Hapus", Nothing, AddressOf GridHapus_Click)
        grPEMBELIAN.ContextMenuStrip = cms

        ' Registrasi tab manual pertama (Nota 1)
        If TabControl1.TabPages.Count > 0 Then
            Dim firstTab As TabPage = TabControl1.TabPages(0)
            TabPagesList.Add(firstTab)
            RegisterTabControls(firstTab, 1)
        End If
        ' Ambil idmenu dari form.Tag yang dikirim dari dashboard
        Dim idMenu As String = Me.Tag.ToString()

        ' Terapkan hak akses ke tombol
        TerapkanAksesKeButton(Me, idMenu)
        DisabledLoad()
    End Sub

    Private Sub DisabledLoad()
        If TabControl1.SelectedTab Is Nothing Then Exit Sub
        Dim nomor As Integer = Integer.Parse(TabControl1.SelectedTab.Text.Replace("Nota ", ""))
        If Not TabControls.ContainsKey(nomor) Then Exit Sub
        Dim dict = TabControls(nomor)

        CType(dict("tpmNONOTA"), TextBox).Enabled = False
        CType(dict("tpmTANGGAL"), DateTimePicker).Enabled = False
        CType(dict("tpmTEMPO"), DateTimePicker).Enabled = False
        CType(dict("tpmKDSUP"), TextBox).Enabled = False
        CType(dict("tpmNMSUP"), TextBox).Enabled = False
        CType(dict("tpmKET"), TextBox).Enabled = False
        CType(dict("tpmALAMAT"), TextBox).Enabled = False
        CType(dict("GRID"), DataGridView).Enabled = False

        CType(dict("tpmTOTAL"), TextBox).Enabled = False
        CType(dict("tpmSUBTOTAL"), TextBox).Enabled = False
        CType(dict("tpmNPPN"), TextBox).Enabled = False
        CType(dict("tpmAPPN"), TextBox).Enabled = False
        CType(dict("tpmLAIN"), TextBox).Enabled = False
        CType(dict("tpmNDISK3"), TextBox).Enabled = False
        CType(dict("tpmADISK3"), TextBox).Enabled = False
        CType(dict("tpmNDISK2"), TextBox).Enabled = False
        CType(dict("tpmADISK2"), TextBox).Enabled = False
        CType(dict("tpmNDISK1"), TextBox).Enabled = False
        CType(dict("tpmADISK1"), TextBox).Enabled = False

        tpmSNONOTA.Enabled = True
        dtpmTGL1.Enabled = True
        dtpmTGL2.Enabled = True
        tpmSNMSUP.Enabled = True
        tmuSLUNAS.Enabled = True

        CType(dict("btnADDITEM"), Button).Enabled = False
    End Sub

    Private Sub EnabledLoad()
        If TabControl1.SelectedTab Is Nothing Then Exit Sub
        Dim nomor As Integer = Integer.Parse(TabControl1.SelectedTab.Text.Replace("Nota ", ""))
        If Not TabControls.ContainsKey(nomor) Then Exit Sub
        Dim dict = TabControls(nomor)

        CType(dict("tpmNONOTA"), TextBox).Enabled = True
        CType(dict("tpmTANGGAL"), DateTimePicker).Enabled = True
        CType(dict("tpmTEMPO"), DateTimePicker).Enabled = True
        CType(dict("tpmKDSUP"), TextBox).Enabled = True
        CType(dict("tpmNMSUP"), TextBox).Enabled = True
        CType(dict("tpmKET"), TextBox).Enabled = True
        CType(dict("tpmALAMAT"), TextBox).Enabled = False
        CType(dict("GRID"), DataGridView).Enabled = True

        CType(dict("tpmTOTAL"), TextBox).Enabled = False
        CType(dict("tpmSUBTOTAL"), TextBox).Enabled = False
        CType(dict("tpmNPPN"), TextBox).Enabled = False
        CType(dict("tpmAPPN"), TextBox).Enabled = True
        CType(dict("tpmLAIN"), TextBox).Enabled = True
        CType(dict("tpmNDISK3"), TextBox).Enabled = False
        CType(dict("tpmADISK3"), TextBox).Enabled = True
        CType(dict("tpmNDISK2"), TextBox).Enabled = False
        CType(dict("tpmADISK2"), TextBox).Enabled = True
        CType(dict("tpmNDISK1"), TextBox).Enabled = False
        CType(dict("tpmADISK1"), TextBox).Enabled = True

        tpmSNONOTA.Enabled = False
        dtpmTGL1.Enabled = False
        dtpmTGL2.Enabled = False
        tpmSNMSUP.Enabled = False
        tmuSLUNAS.Enabled = False

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
            Dim sqlHead As String = "SELECT z.tgl, z.tgltempo, z.ket, z.kodesup, s.namasup, s.alamat, z.nilai, z.lunas, z.disc1, z.hdisc1, z.disc2, z.hdisc2, z.disc3, z.hdisc3, z.ppn, z.hppn, z.lainnya FROM zbeli z LEFT JOIN zsupplier s ON z.kodesup = s.kodesup WHERE z.nonota = ?"
            Using cmd As New OdbcCommand(sqlHead, Conn)
                cmd.Parameters.AddWithValue("@nonota", nonota)
                Rd = cmd.ExecuteReader()
                If Rd.Read() Then
                    Dim aktifTab As TabPage = TabControl1.SelectedTab
                    Dim nomor As Integer = Integer.Parse(aktifTab.Text.Replace("Nota ", ""))
                    Dim dict = TabControls(nomor)

                    CType(dict("tpmNONOTA"), TextBox).Text = nonota
                    CType(dict("tpmTANGGAL"), DateTimePicker).Value = CDate(Rd("tgl"))
                    CType(dict("tpmTEMPO"), DateTimePicker).Value = CDate(Rd("tgltempo"))
                    CType(dict("tpmKET"), TextBox).Text = Rd("ket").ToString()
                    CType(dict("tpmKDSUP"), TextBox).Text = Rd("kodesup").ToString()
                    CType(dict("tpmNMSUP"), TextBox).Text = Rd("namasup").ToString()
                    CType(dict("tpmALAMAT"), TextBox).Text = Rd("alamat").ToString()
                    CType(dict("tpmTOTAL"), TextBox).Text = Rd("nilai").ToString()
                    CType(dict("tpmADISK1"), TextBox).Text = Rd("disc1").ToString()
                    CType(dict("tpmNDISK1"), TextBox).Text = Rd("hdisc1").ToString()
                    CType(dict("tpmADISK2"), TextBox).Text = Rd("disc2").ToString()
                    CType(dict("tpmNDISK2"), TextBox).Text = Rd("hdisc2").ToString()
                    CType(dict("tpmADISK3"), TextBox).Text = Rd("disc3").ToString()
                    CType(dict("tpmNDISK3"), TextBox).Text = Rd("hdisc3").ToString()
                    CType(dict("tpmAPPN"), TextBox).Text = Rd("ppn").ToString()
                    CType(dict("tpmNPPN"), TextBox).Text = Rd("hppn").ToString()
                    CType(dict("tpmLAIN"), TextBox).Text = Rd("lainnya").ToString()
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
        dtpmTGL1.Enabled = Not nonotaActive
        dtpmTGL2.Enabled = Not nonotaActive
        tpmSNMSUP.Enabled = Not nonotaActive
        tmuSLUNAS.Enabled = Not nonotaActive
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

        dict("tpmTANGGAL") = tab.Controls.Find("tpmTANGGAL", True)(0)
        dict("tpmNONOTA") = tab.Controls.Find("tpmNONOTA", True)(0)
        dict("tpmTEMPO") = tab.Controls.Find("tpmTEMPO", True)(0)
        dict("tpmKET") = tab.Controls.Find("tpmKET", True)(0)
        dict("tpmKDSUP") = tab.Controls.Find("tpmKDSUP", True)(0)
        dict("tpmNMSUP") = tab.Controls.Find("tpmNMSUP", True)(0)
        dict("tpmALAMAT") = tab.Controls.Find("tpmALAMAT", True)(0)
        dict("tpmTOTAL") = tab.Controls.Find("tpmTOTAL", True)(0)
        dict("tpmSUBTOTAL") = tab.Controls.Find("tpmSUBTOTAL", True)(0)
        dict("tpmNPPN") = tab.Controls.Find("tpmNPPN", True)(0)
        dict("tpmAPPN") = tab.Controls.Find("tpmAPPN", True)(0)
        dict("tpmLAIN") = tab.Controls.Find("tpmLAIN", True)(0)

        CType(dict("tpmAPPN"), TextBox).Text = "11"

        dict("tpmNDISK3") = tab.Controls.Find("tpmNDISK3", True)(0)
        dict("tpmADISK3") = tab.Controls.Find("tpmADISK3", True)(0)
        dict("tpmNDISK2") = tab.Controls.Find("tpmNDISK2", True)(0)
        dict("tpmADISK2") = tab.Controls.Find("tpmADISK2", True)(0)
        dict("tpmNDISK1") = tab.Controls.Find("tpmNDISK1", True)(0)
        dict("tpmADISK1") = tab.Controls.Find("tpmADISK1", True)(0)

        ' Grid
        Dim grid As DataGridView = CType(tab.Controls.Find("grPEMBELIAN", True)(0), DataGridView)
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
        AddHandler CType(dict("tpmKDSUP"), TextBox).KeyDown, AddressOf tpmKDSUP_KeyDown
        AddHandler CType(dict("tpmNMSUP"), TextBox).KeyDown, AddressOf tpmNMSUP_KeyDown

        Dim txtADISK1 As TextBox = CType(dict("tpmADISK1"), TextBox)
        Dim txtADISK2 As TextBox = CType(dict("tpmADISK2"), TextBox)
        Dim txtADISK3 As TextBox = CType(dict("tpmADISK3"), TextBox)
        Dim txtAPPN As TextBox = CType(dict("tpmAPPN"), TextBox)
        Dim txtLAIN As TextBox = CType(dict("tpmLAIN"), TextBox)

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
    Private Sub bpmTabAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bpmTabAdd.Click
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

        ' Ambil grid dari tab baru dan panggil SetupGridPembelian
        Dim dict = TabControls(nomor)
        If dict.ContainsKey("GRID") Then
            Dim grid As DataGridView = CType(dict("GRID"), DataGridView)
            SetupGridPembelian(grid)
        End If

        SetTabStatus(nomor, String.Empty)
        TabControl1.SelectedTab = newTab
        TabLoadState(nomor) = True
        TabButtonState(nomor) = True
        SetButtonState(Me, True)

    End Sub

    ' ================== HAPUS TAB ==================
    Private Sub bpmTabDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bpmTabDel.Click
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

    ' ================== SIMPAN ==================
    Private Sub btnSIMPAN_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSIMPAN.Click
        Try
            Dim aktifTab As TabPage = TabControl1.SelectedTab
            Dim nomor As Integer = Integer.Parse(aktifTab.Text.Replace("Nota ", ""))
            Dim dict = TabControls(nomor)
            Dim status As String = GetTabStatus(nomor)

            MPemSimpan.SimpanPembelian(nomor, dict, status)

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
        Dim tpmNONOTA As TextBox = TryCast(TabControls(nomor)("tpmNONOTA"), TextBox)

        ' cek apakah kosong
        If tpmNONOTA Is Nothing OrElse String.IsNullOrWhiteSpace(tpmNONOTA.Text) Then
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

            Dim nonota As String = CType(dict("tpmNONOTA"), TextBox).Text.Trim()

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

        CType(dict("tpmSUBTOTAL"), TextBox).Text = subtotal.ToString()
    End Sub


    ' ================== ADD ITEM ==================
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
    Private Sub tpmKDSUP_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs)
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

    Private Sub tpmNMSUP_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs)
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
        Dim f As New ItDtPembelian()

        Dim nonota As String = tpmSNONOTA.Text.Trim()
        Dim tgl1 As String = dtpmTGL1.Value.ToString("yyyy-MM-dd")
        Dim tgl2 As String = dtpmTGL2.Value.ToString("yyyy-MM-dd")
        Dim namasup As String = tpmSNMSUP.Text.Trim()
        Dim status As String = ""

        ' Tentukan status lunas hanya jika nonota kosong
        If nonota = "" Then
            If tmuSLUNAS.SelectedIndex = 0 Then
                status = "belum"
            ElseIf tmuSLUNAS.SelectedIndex = 1 Then
                status = "lunas"
            End If
        End If

        ' Load data sesuai filter
        f.Owner = Me
        f.Show()
        f.LoadDataBeli(nonota, tgl1, tgl2, namasup, status)

        ' === Clear filter setelah pencarian ===
        tpmSNONOTA.Clear()
        tpmSNMSUP.Clear()
        tmuSLUNAS.SelectedIndex = -1 ' reset pilihan
        ' reset tanggal ke hari ini
        dtpmTGL1.Value = DateTime.Today
        dtpmTGL2.Value = DateTime.Today
    End Sub

    Private Sub tpmSLUNAS_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmuSLUNAS.SelectedIndexChanged
        If tpmSNONOTA.Text.Trim() = "" Then
            Dim cb As ComboBox = CType(sender, ComboBox)
            Dim val As Integer = Convert.ToInt32(cb.SelectedValue)
            btnCARI.PerformClick()
        End If
    End Sub

    Private Sub tpmSNONOTA_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tpmSNONOTA.TextChanged
        If tpmSNONOTA.Text.Trim() <> "" Then
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

        Dim subtotal As Decimal = Val(CType(dict("tpmSUBTOTAL"), TextBox).Text)
        Dim adisk1 As Decimal = Val(CType(dict("tpmADISK1"), TextBox).Text)
        Dim adisk2 As Decimal = Val(CType(dict("tpmADISK2"), TextBox).Text)
        Dim adisk3 As Decimal = Val(CType(dict("tpmADISK3"), TextBox).Text)
        Dim appn As Decimal = Val(CType(dict("tpmAPPN"), TextBox).Text)
        Dim lain As Decimal = Val(CType(dict("tpmLAIN"), TextBox).Text)

        Dim hasil = ModHitung.HitungSubtotalTotal(subtotal, adisk1, adisk2, adisk3, appn, lain)

        CType(dict("tpmNDISK1"), TextBox).Text = hasil("hdisca").ToString()
        CType(dict("tpmNDISK2"), TextBox).Text = hasil("hdiscb").ToString()
        CType(dict("tpmNDISK3"), TextBox).Text = hasil("hdiscc").ToString()
        CType(dict("tpmNPPN"), TextBox).Text = hasil("ppn").ToString()
        CType(dict("tpmTOTAL"), TextBox).Text = hasil("total").ToString()
    End Sub

    Private Sub OnlyNumber_KeyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs) _
        Handles tpmSUBTOTAL.KeyPress, tpmTOTAL.KeyPress, tpmADISK1.KeyPress,
                tpmADISK2.KeyPress, tpmADISK3.KeyPress, tpmNDISK1.KeyPress,
                tpmNDISK2.KeyPress, tpmNDISK3.KeyPress, tpmLAIN.KeyPress,
                tpmAPPN.KeyPress, tpmNPPN.KeyPress

        AngkaHelper.HanyaAngka(e)
    End Sub

    Private Sub btnPRINT_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnPRINT.Click
        Try
            Dim aktifTab As TabPage = TabControl1.SelectedTab
            Dim nomor As Integer = Integer.Parse(aktifTab.Text.Replace("Nota ", ""))
            Dim dict = TabControls(nomor)

            Dim nonota As String = CType(dict("tpmNONOTA"), TextBox).Text

            If String.IsNullOrEmpty(nonota) Then
                MessageBox.Show("Pilih nota yang akan dicetak!", "Info")
                Exit Sub
            End If

            ' --- Buka form cetak ---
            Dim f As New FCetak()
            f.Param("nonota") = nonota
            f.Param("jenis") = "pembelian"
            f.ShowDialog()

        Catch ex As Exception
            MessageBox.Show("Gagal cetak nota: " & ex.Message, "Error")
        End Try
    End Sub
End Class
