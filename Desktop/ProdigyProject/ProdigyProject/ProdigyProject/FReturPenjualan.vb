Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Windows.Forms
Imports System.Data.Odbc

Public Class FReturPenjualan
    Private TabPagesList As New List(Of TabPage)
    Private TabControls As New Dictionary(Of Integer, Dictionary(Of String, Control))
    Private TabButtonState As New Dictionary(Of Integer, String)
    Private TabLoadState As New Dictionary(Of Integer, Boolean)
    Private TabStatus As New Dictionary(Of Integer, String)

    Public Sub SetKustomer(ByVal kodek As String, ByVal namak As String,
                       ByVal alamat As String, ByVal kota As String,
                       ByVal ktp As String, ByVal npwp As String)

        If TabControl1.SelectedTab Is Nothing Then Exit Sub

        ' Ambil nomor tab aktif
        Dim nomor As Integer = Integer.Parse(TabControl1.SelectedTab.Text.Replace("Nota ", ""))

        If TabControls.ContainsKey(nomor) Then
            Dim dict = TabControls(nomor)

            CType(dict("trpjKDKUST"), TextBox).Text = kodek
            CType(dict("trpjNMKUST"), TextBox).Text = namak
            CType(dict("trpjALAMAT"), TextBox).Text = alamat

        End If
    End Sub

    Public Sub SetSales(ByVal kodes As String, ByVal namas As String,
                       ByVal alamat As String, ByVal kota As String,
                       ByVal ktp As String, ByVal npwp As String)

        If TabControl1.SelectedTab Is Nothing Then Exit Sub

        ' Ambil nomor tab aktif
        Dim nomor As Integer = Integer.Parse(TabControl1.SelectedTab.Text.Replace("Nota ", ""))

        If TabControls.ContainsKey(nomor) Then
            Dim dict = TabControls(nomor)

            CType(dict("trpjKDSALES"), TextBox).Text = kodes
            CType(dict("trpjNMSALES"), TextBox).Text = namas

        End If
    End Sub

    Private Sub FReturPenjualan_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' isi dropdown lunas
        trpjSLUNAS.Items.Clear()
        trpjSLUNAS.Items.Add(New With {.Value = 0, .Text = "Belum Lunas"})
        trpjSLUNAS.Items.Add(New With {.Value = 1, .Text = "Lunas"})
        trpjSLUNAS.DisplayMember = "Text"
        trpjSLUNAS.ValueMember = "Value"
        trpjSLUNAS.SelectedValue = 0   ' default Belum Lunas

        SetButtonState(Me, True)
        SetupGridPenjualan(grRPENJUALAN)

        ' Tambah context menu ke grid utama
        Dim cms As New ContextMenuStrip()
        cms.Items.Add("Ubah", Nothing, AddressOf GridUbah_Click)
        cms.Items.Add("Hapus", Nothing, AddressOf GridHapus_Click)
        grRPENJUALAN.ContextMenuStrip = cms

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

        CType(dict("trpjNONOTA"), TextBox).Enabled = False
        CType(dict("trpjNOFAKTUR"), TextBox).Enabled = False
        CType(dict("trpjTANGGAL"), DateTimePicker).Enabled = False
        CType(dict("trpjTEMPO"), DateTimePicker).Enabled = False
        CType(dict("trpjKDKUST"), TextBox).Enabled = False
        CType(dict("trpjNMKUST"), TextBox).Enabled = False
        CType(dict("trpjKDSALES"), TextBox).Enabled = False
        CType(dict("trpjNMSALES"), TextBox).Enabled = False
        CType(dict("trpjKDHARGA"), TextBox).Enabled = False
        CType(dict("trpjKET"), TextBox).Enabled = False
        CType(dict("trpjALAMAT"), TextBox).Enabled = False
        CType(dict("GRID"), DataGridView).Enabled = False

        CType(dict("trpjTOTAL"), TextBox).Enabled = False
        CType(dict("trpjSUBTOTAL"), TextBox).Enabled = False
        CType(dict("trpjNPPN"), TextBox).Enabled = False
        CType(dict("trpjAPPN"), TextBox).Enabled = False
        CType(dict("trpjLAIN"), TextBox).Enabled = False
        CType(dict("trpjNDISK3"), TextBox).Enabled = False
        CType(dict("trpjADISK3"), TextBox).Enabled = False
        CType(dict("trpjNDISK2"), TextBox).Enabled = False
        CType(dict("trpjADISK2"), TextBox).Enabled = False
        CType(dict("trpjNDISK1"), TextBox).Enabled = False
        CType(dict("trpjADISK1"), TextBox).Enabled = False

        trpjSNONOTA.Enabled = True
        dtrpjTGL1.Enabled = True
        dtrpjTGL2.Enabled = True
        trpjSNMKUST.Enabled = True
        trpjSNMSLS.Enabled = True
        trpjSLUNAS.Enabled = True

        CType(dict("btnADDITEM"), Button).Enabled = False
    End Sub

    Private Sub EnabledLoad()
        If TabControl1.SelectedTab Is Nothing Then Exit Sub
        Dim nomor As Integer = Integer.Parse(TabControl1.SelectedTab.Text.Replace("Nota ", ""))
        If Not TabControls.ContainsKey(nomor) Then Exit Sub
        Dim dict = TabControls(nomor)

        CType(dict("trpjNONOTA"), TextBox).Enabled = True
        CType(dict("trpjNOFAKTUR"), TextBox).Enabled = True
        CType(dict("trpjTANGGAL"), DateTimePicker).Enabled = True
        CType(dict("trpjTEMPO"), DateTimePicker).Enabled = True
        CType(dict("trpjKDKUST"), TextBox).Enabled = True
        CType(dict("trpjNMKUST"), TextBox).Enabled = True
        CType(dict("trpjKDSALES"), TextBox).Enabled = True
        CType(dict("trpjNMSALES"), TextBox).Enabled = True
        CType(dict("trpjKDHARGA"), TextBox).Enabled = True
        CType(dict("trpjKET"), TextBox).Enabled = True
        CType(dict("trpjALAMAT"), TextBox).Enabled = False
        CType(dict("GRID"), DataGridView).Enabled = True

        CType(dict("trpjTOTAL"), TextBox).Enabled = False
        CType(dict("trpjSUBTOTAL"), TextBox).Enabled = False
        CType(dict("trpjNPPN"), TextBox).Enabled = False
        CType(dict("trpjAPPN"), TextBox).Enabled = True
        CType(dict("trpjLAIN"), TextBox).Enabled = True
        CType(dict("trpjNDISK3"), TextBox).Enabled = False
        CType(dict("trpjADISK3"), TextBox).Enabled = True
        CType(dict("trpjNDISK2"), TextBox).Enabled = False
        CType(dict("trpjADISK2"), TextBox).Enabled = True
        CType(dict("trpjNDISK1"), TextBox).Enabled = False
        CType(dict("trpjADISK1"), TextBox).Enabled = True

        trpjSNONOTA.Enabled = False
        dtrpjTGL1.Enabled = False
        dtrpjTGL2.Enabled = False
        trpjSNMKUST.Enabled = False
        trpjSNMSLS.Enabled = False
        trpjSLUNAS.Enabled = False

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
            Dim sqlHead As String = "SELECT z.tgl, z.tgltempo, z.nofaktur, z.ket, z.kodekust, k.namakust, z.kodesls, s.namasls, k.alamat, z.nilai, z.lunas, z.disc1, z.hdisc1, z.disc2, z.hdisc2, z.disc3, z.hdisc3, z.ppn, z.hppn, z.lainnya FROM zrjual z LEFT JOIN zkustomer k ON z.kodekust = k.kodekust LEFT JOIN zsales s ON z.kodesls = s.kodesls WHERE z.nonota = ?"
            Using cmd As New OdbcCommand(sqlHead, Conn)
                cmd.Parameters.AddWithValue("@nonota", nonota)
                Rd = cmd.ExecuteReader()
                If Rd.Read() Then
                    Dim aktifTab As TabPage = TabControl1.SelectedTab
                    Dim nomor As Integer = Integer.Parse(aktifTab.Text.Replace("Nota ", ""))
                    Dim dict = TabControls(nomor)

                    CType(dict("trpjNONOTA"), TextBox).Text = nonota
                    CType(dict("trpjNOFAKTUR"), TextBox).Text = Rd("nofaktur").ToString()
                    CType(dict("trpjTANGGAL"), DateTimePicker).Value = CDate(Rd("tgl"))
                    CType(dict("trpjTEMPO"), DateTimePicker).Value = CDate(Rd("tgltempo"))
                    CType(dict("trpjKET"), TextBox).Text = Rd("ket").ToString()
                    CType(dict("trpjKDKUST"), TextBox).Text = Rd("kodekust").ToString()
                    CType(dict("trpjNMKUST"), TextBox).Text = Rd("namakust").ToString()
                    CType(dict("trpjKDSALES"), TextBox).Text = Rd("kodesls").ToString()
                    CType(dict("trpjNMSALES"), TextBox).Text = Rd("namasls").ToString()
                    CType(dict("trpjALAMAT"), TextBox).Text = Rd("alamat").ToString()
                    CType(dict("trpjTOTAL"), TextBox).Text = Rd("nilai").ToString()
                    CType(dict("trpjADISK1"), TextBox).Text = Rd("disc1").ToString()
                    CType(dict("trpjNDISK1"), TextBox).Text = Rd("hdisc1").ToString()
                    CType(dict("trpjADISK2"), TextBox).Text = Rd("disc2").ToString()
                    CType(dict("trpjNDISK2"), TextBox).Text = Rd("hdisc2").ToString()
                    CType(dict("trpjADISK3"), TextBox).Text = Rd("disc3").ToString()
                    CType(dict("trpjNDISK3"), TextBox).Text = Rd("hdisc3").ToString()
                    CType(dict("trpjAPPN"), TextBox).Text = Rd("ppn").ToString()
                    CType(dict("trpjNPPN"), TextBox).Text = Rd("hppn").ToString()
                    CType(dict("trpjLAIN"), TextBox).Text = Rd("lainnya").ToString()
                End If
                Rd.Close()
            End Using

            ' ================== DETAIL ==================
            Dim sqlDet As String = "SELECT d.kodebrg, s.namabrg, d.kodegd, d.jlh1, s.satuan1, d.jlh2, s.satuan2, d.jlh3, s.satuan3, d.harga, d.disca, d.discb, d.discc, d.discrp, d.jumlah FROM zrjualm d LEFT JOIN zstok s ON d.kodebrg = s.kodebrg WHERE d.nonota = ?"
            Using cmdDet As New OdbcCommand(sqlDet, Conn)
                cmdDet.Parameters.AddWithValue("@nonota", nonota)
                Rd = cmdDet.ExecuteReader()

                Dim aktifTab As TabPage = TabControl1.SelectedTab
                Dim nomor As Integer = Integer.Parse(aktifTab.Text.Replace("Nota ", ""))
                Dim dict = TabControls(nomor)
                Dim grid As DataGridView = CType(dict("GRID"), DataGridView)

                If grid.Columns.Count = 0 Then
                    SetupGridPenjualan(grid)
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
        dtrpjTGL1.Enabled = Not nonotaActive
        dtrpjTGL2.Enabled = Not nonotaActive
        trpjSNMKUST.Enabled = Not nonotaActive
        trpjSNMSLS.Enabled = Not nonotaActive
        trpjSLUNAS.Enabled = Not nonotaActive
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

        dict("trpjTANGGAL") = tab.Controls.Find("trpjTANGGAL", True)(0)
        dict("trpjNONOTA") = tab.Controls.Find("trpjNONOTA", True)(0)
        dict("trpjNOFAKTUR") = tab.Controls.Find("trpjNOFAKTUR", True)(0)
        dict("trpjTEMPO") = tab.Controls.Find("trpjTEMPO", True)(0)
        dict("trpjKET") = tab.Controls.Find("trpjKET", True)(0)
        dict("trpjKDKUST") = tab.Controls.Find("trpjKDKUST", True)(0)
        dict("trpjNMKUST") = tab.Controls.Find("trpjNMKUST", True)(0)
        dict("trpjKDSALES") = tab.Controls.Find("trpjKDSALES", True)(0)
        dict("trpjNMSALES") = tab.Controls.Find("trpjNMSALES", True)(0)
        dict("trpjKDHARGA") = tab.Controls.Find("trpjKDHARGA", True)(0)
        dict("trpjALAMAT") = tab.Controls.Find("trpjALAMAT", True)(0)
        dict("trpjTOTAL") = tab.Controls.Find("trpjTOTAL", True)(0)
        dict("trpjSUBTOTAL") = tab.Controls.Find("trpjSUBTOTAL", True)(0)
        dict("trpjNPPN") = tab.Controls.Find("trpjNPPN", True)(0)
        dict("trpjAPPN") = tab.Controls.Find("trpjAPPN", True)(0)
        dict("trpjLAIN") = tab.Controls.Find("trpjLAIN", True)(0)

        CType(dict("trpjAPPN"), TextBox).Text = "11"

        dict("trpjNDISK3") = tab.Controls.Find("trpjNDISK3", True)(0)
        dict("trpjADISK3") = tab.Controls.Find("trpjADISK3", True)(0)
        dict("trpjNDISK2") = tab.Controls.Find("trpjNDISK2", True)(0)
        dict("trpjADISK2") = tab.Controls.Find("trpjADISK2", True)(0)
        dict("trpjNDISK1") = tab.Controls.Find("trpjNDISK1", True)(0)
        dict("trpjADISK1") = tab.Controls.Find("trpjADISK1", True)(0)

        ' Grid
        Dim grid As DataGridView = CType(tab.Controls.Find("grRPENJUALAN", True)(0), DataGridView)
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
        AddHandler CType(dict("trpjKDKUST"), TextBox).KeyDown, AddressOf trpjKDKUST_KeyDown
        AddHandler CType(dict("trpjNMKUST"), TextBox).KeyDown, AddressOf trpjNMKUST_KeyDown
        AddHandler CType(dict("trpjKDSALES"), TextBox).KeyDown, AddressOf trpjKDSALES_KeyDown
        AddHandler CType(dict("trpjNMSALES"), TextBox).KeyDown, AddressOf trpjNMSALES_KeyDown
        AddHandler CType(dict("trpjNOFAKTUR"), TextBox).KeyDown, AddressOf trpjNOFAKTUR_KeyDown

        Dim txtADISK1 As TextBox = CType(dict("trpjADISK1"), TextBox)
        Dim txtADISK2 As TextBox = CType(dict("trpjADISK2"), TextBox)
        Dim txtADISK3 As TextBox = CType(dict("trpjADISK3"), TextBox)
        Dim txtAPPN As TextBox = CType(dict("trpjAPPN"), TextBox)
        Dim txtLAIN As TextBox = CType(dict("trpjLAIN"), TextBox)

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

    Private Sub brpjTabAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles brpjTabAdd.Click
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

    Private Sub brpjTabDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles brpjTabDel.Click
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

            MRPenSimpan.SimpanPenjualan(nomor, dict, status)

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

        ' set status
        SetTabStatus(nomor, "tambah")

        Dim dict = TabControls(nomor)
        ClearForm(dict)

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

        ' ambil kontrol tpjNONOTA dari tab aktif
        Dim trpjNONOTA As TextBox = TryCast(TabControls(nomor)("trpjNONOTA"), TextBox)

        ' cek apakah kosong
        If trpjNONOTA Is Nothing OrElse String.IsNullOrWhiteSpace(trpjNONOTA.Text) Then
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

            Dim nonota As String = CType(dict("trpjNONOTA"), TextBox).Text.Trim()

            If String.IsNullOrEmpty(nonota) Then
                MessageBox.Show("Tidak ada nota yang bisa dihapus.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            If MessageBox.Show("Yakin hapus nota " & nonota & " ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then
                Exit Sub
            End If

            ' === Hapus data dari database (module) ===
            MRPenSimpan.HapusPembelian(nonota, dict)

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

        CType(dict("trpjSUBTOTAL"), TextBox).Text = subtotal.ToString()
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

    ' ================== KEYDOWN KUSTOMER ==================
    Private Sub trpjKDKUST_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs)
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            Dim txtKode As TextBox = CType(sender, TextBox)
            If txtKode.Text.Trim() <> "" Then
                Dim f As New ItDtKustomer
                f.Owner = Me
                f.Show()
                f.LoadDataKust(txtKode.Text.Trim())
            End If
        End If
    End Sub

    Private Sub trpjNMKUST_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs)
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            Dim txtNama As TextBox = CType(sender, TextBox)
            If txtNama.Text.Trim() <> "" Then
                Dim f As New ItDtKustomer
                f.Owner = Me
                f.Show()
                f.LoadDataKust(txtNama.Text.Trim())
            End If
        End If
    End Sub

    Private Sub trpjKDSALES_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs)
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            Dim txtKode As TextBox = CType(sender, TextBox)
            If txtKode.Text.Trim() <> "" Then
                Dim f As New ItDtSales
                f.Owner = Me
                f.Show()
                f.LoadDataSls(txtKode.Text.Trim())
            End If
        End If
    End Sub

    Private Sub trpjNMSALES_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs)
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            Dim txtNama As TextBox = CType(sender, TextBox)
            If txtNama.Text.Trim() <> "" Then
                Dim f As New ItDtSales
                f.Owner = Me
                f.Show()
                f.LoadDataSls(txtNama.Text.Trim())
            End If
        End If
    End Sub

    Private Sub btnCARI_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCARI.Click
        Dim f As New ItDtRPenjualan()

        Dim nonota As String = trpjSNONOTA.Text.Trim()
        Dim tgl1 As String = dtrpjTGL1.Value.ToString("yyyy-MM-dd")
        Dim tgl2 As String = dtrpjTGL2.Value.ToString("yyyy-MM-dd")
        Dim namasup As String = trpjSNMKUST.Text.Trim()
        Dim status As String = ""

        ' Tentukan status lunas hanya jika nonota kosong
        If nonota = "" Then
            If trpjSLUNAS.SelectedIndex = 0 Then
                status = "belum"
            ElseIf trpjSLUNAS.SelectedIndex = 1 Then
                status = "lunas"
            End If
        End If

        ' Load data sesuai filter
        f.Owner = Me
        f.Show()
        f.LoadDataRJual(nonota, tgl1, tgl2, namasup, status)

        ' === Clear filter setelah pencarian ===
        trpjSNONOTA.Clear()
        trpjSNMKUST.Clear()
        trpjSLUNAS.SelectedIndex = -1 ' reset pilihan
        ' reset tanggal ke hari ini
        dtrpjTGL1.Value = DateTime.Today
        dtrpjTGL2.Value = DateTime.Today
    End Sub

    Private Sub trpjSNONOTA_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles trpjSNONOTA.TextChanged
        If trpjSNONOTA.Text.Trim() <> "" Then
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

        Dim subtotal As Decimal = Val(CType(dict("trpjSUBTOTAL"), TextBox).Text)
        Dim adisk1 As Decimal = Val(CType(dict("trpjADISK1"), TextBox).Text)
        Dim adisk2 As Decimal = Val(CType(dict("trpjADISK2"), TextBox).Text)
        Dim adisk3 As Decimal = Val(CType(dict("trpjADISK3"), TextBox).Text)
        Dim appn As Decimal = Val(CType(dict("trpjAPPN"), TextBox).Text)
        Dim lain As Decimal = Val(CType(dict("trpjLAIN"), TextBox).Text)

        Dim hasil = ModHitung.HitungSubtotalTotal(subtotal, adisk1, adisk2, adisk3, appn, lain)

        CType(dict("trpjNDISK1"), TextBox).Text = hasil("hdisca").ToString()
        CType(dict("trpjNDISK2"), TextBox).Text = hasil("hdiscb").ToString()
        CType(dict("trpjNDISK3"), TextBox).Text = hasil("hdiscc").ToString()
        CType(dict("trpjNPPN"), TextBox).Text = hasil("ppn").ToString()
        CType(dict("trpjTOTAL"), TextBox).Text = hasil("total").ToString()
    End Sub

    Private Sub OnlyNumber_KeyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs) _
        Handles trpjSUBTOTAL.KeyPress, trpjTOTAL.KeyPress, trpjADISK1.KeyPress,
                trpjADISK2.KeyPress, trpjADISK3.KeyPress, trpjNDISK1.KeyPress,
                trpjNDISK2.KeyPress, trpjNDISK3.KeyPress, trpjLAIN.KeyPress,
                trpjAPPN.KeyPress, trpjNPPN.KeyPress

        AngkaHelper.HanyaAngka(e)
    End Sub

    Private Sub btnPRINT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPRINT.Click
        Try
            Dim aktifTab As TabPage = TabControl1.SelectedTab
            Dim nomor As Integer = Integer.Parse(aktifTab.Text.Replace("Nota ", ""))
            Dim dict = TabControls(nomor)

            Dim nonota As String = CType(dict("trpjNONOTA"), TextBox).Text

            If String.IsNullOrEmpty(nonota) Then
                MessageBox.Show("Pilih nota yang akan dicetak!", "Info")
                Exit Sub
            End If

            ' --- Buka form cetak ---
            Dim f As New FCetak()
            f.Param("nonota") = nonota
            f.Param("jenis") = "rpenjualan"
            f.ShowDialog()

        Catch ex As Exception
            MessageBox.Show("Gagal cetak nota: " & ex.Message, "Error")
        End Try
    End Sub

    Private Sub trpjNOFAKTUR_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles trpjNOFAKTUR.KeyDown
        If e.KeyCode = Keys.Enter Then
            Try
                Dim aktifTab As TabPage = TabControl1.SelectedTab
                Dim nomor As Integer = Integer.Parse(aktifTab.Text.Replace("Nota ", ""))

                ' pastikan dictionary tab terdaftar
                If Not TabControls.ContainsKey(nomor) Then
                    RegisterTabControls(aktifTab, nomor)
                End If

                Dim dict = TabControls(nomor)

                BukaKoneksi()

                Dim nonota As String = Trim(CType(dict("trpjNOFAKTUR"), TextBox).Text)
                If nonota = "" Then Exit Sub

                ' ====== JOIN zbeli + zsupplier ======
                Dim sql As String = "SELECT zjual.kodekust, zjual.kodesls, zkustomer.namakust, zkustomer.alamat, zsales.namasls " &
                                    "FROM zjual " &
                                    "JOIN zkustomer ON zjual.kodekust = zkustomer.kodekust " &
                                    "JOIN zsales ON zjual.kodesls = zsales.kodesls " &
                                    "WHERE zjual.nonota = ?"

                Dim cmd As New Odbc.OdbcCommand(sql, Conn)
                cmd.Parameters.AddWithValue("@nonota", nonota)

                Dim rd As Odbc.OdbcDataReader = cmd.ExecuteReader()

                If rd.Read() Then
                    CType(dict("trpjKDKUST"), TextBox).Text = rd("kodekust").ToString()
                    CType(dict("trpjNMKUST"), TextBox).Text = rd("namakust").ToString()
                    CType(dict("trpjALAMAT"), TextBox).Text = rd("alamat").ToString()
                    CType(dict("trpjKDSALES"), TextBox).Text = rd("kodesls").ToString()
                    CType(dict("trpjNMSALES"), TextBox).Text = rd("namasls").ToString()

                    ' Nonaktifkan field setelah data terisi
                    CType(dict("trpjKDKUST"), TextBox).Enabled = False
                    CType(dict("trpjNMKUST"), TextBox).Enabled = False
                    CType(dict("trpjKDSALES"), TextBox).Enabled = False
                    CType(dict("trpjNMSALES"), TextBox).Enabled = False
                Else
                    MessageBox.Show("Nomor faktur tidak ditemukan!", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    ' Kosongkan dan aktifkan kembali field supaya bisa diisi manual
                    CType(dict("trpjKDKUST"), TextBox).Clear()
                    CType(dict("trpjNMKUST"), TextBox).Clear()
                    CType(dict("trpjALAMAT"), TextBox).Clear()
                    CType(dict("trpjKDSALES"), TextBox).Clear()
                    CType(dict("trpjNMSALES"), TextBox).Clear()

                    CType(dict("trpjKDKUST"), TextBox).Enabled = True
                    CType(dict("trpjNMKUST"), TextBox).Enabled = True
                    CType(dict("trpjKDSALES"), TextBox).Enabled = True
                    CType(dict("trpjNMSALES"), TextBox).Enabled = True
                End If

                rd.Close()
                Conn.Close()

            Catch ex As Exception
                MessageBox.Show("Terjadi kesalahan: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub
End Class