Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Windows.Forms
Imports System.Data.Odbc

Public Class FPenjualan
    Private TabPagesList As New List(Of TabPage)
    Private TabControls As New Dictionary(Of Integer, Dictionary(Of String, Control))
    Private TabButtonState As New Dictionary(Of Integer, String)
    Private TabLoadState As New Dictionary(Of Integer, Boolean)
    Private TabStatus As New Dictionary(Of Integer, String)
    Public KodeLama As String = ""

    Public Sub SetKustomer(ByVal kodek As String, ByVal namak As String,
                       ByVal alamat As String, ByVal kota As String,
                       ByVal ktp As String, ByVal npwp As String)

        If TabControl1.SelectedTab Is Nothing Then Exit Sub

        ' Ambil nomor tab aktif
        Dim nomor As Integer = Integer.Parse(TabControl1.SelectedTab.Text.Replace("Nota ", ""))

        If TabControls.ContainsKey(nomor) Then
            Dim dict = TabControls(nomor)

            CType(dict("tpjKDKUST"), TextBox).Text = kodek
            CType(dict("tpjNMKUST"), TextBox).Text = namak
            CType(dict("tpjALAMAT"), TextBox).Text = alamat

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

            CType(dict("tpjKDSALES"), TextBox).Text = kodes
            CType(dict("tpjNMSALES"), TextBox).Text = namas

        End If
    End Sub

    Private Sub FPenjualan_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' isi dropdown lunas
        tpjSLUNAS.Items.Clear()
        tpjSLUNAS.Items.Add(New With {.Value = 0, .Text = "Belum Lunas"})
        tpjSLUNAS.Items.Add(New With {.Value = 1, .Text = "Lunas"})
        tpjSLUNAS.DisplayMember = "Text"
        tpjSLUNAS.ValueMember = "Value"
        tpjSLUNAS.SelectedValue = 0   ' default Belum Lunas

        SetButtonState(Me, True)
        SetupGridPENJUALAN(grPENJUALAN)

        ' Tambah context menu ke grid utama
        Dim cms As New ContextMenuStrip()
        cms.Items.Add("Ubah", Nothing, AddressOf GridUbah_Click)
        cms.Items.Add("Hapus", Nothing, AddressOf GridHapus_Click)
        grPENJUALAN.ContextMenuStrip = cms

        ' Registrasi tab manual pertama (Nota 1)
        If TabControl1.TabPages.Count > 0 Then
            Dim firstTab As TabPage = TabControl1.TabPages(0)
            TabPagesList.Add(firstTab)
            RegisterTabControls(firstTab, 1)
        End If
        ' Ambil idmenu dari form.Tag yang dikirim dari dashboard
        
        DisabledLoad()
    End Sub

    Private Sub DisabledLoad()
        If TabControl1.SelectedTab Is Nothing Then Exit Sub
        Dim nomor As Integer = Integer.Parse(TabControl1.SelectedTab.Text.Replace("Nota ", ""))
        If Not TabControls.ContainsKey(nomor) Then Exit Sub
        Dim dict = TabControls(nomor)

        CType(dict("tpjNONOTA"), TextBox).Enabled = False
        CType(dict("tpjTANGGAL"), DateTimePicker).Enabled = False
        CType(dict("tpjTEMPO"), DateTimePicker).Enabled = False
        CType(dict("tpjKDKUST"), TextBox).Enabled = False
        CType(dict("tpjNMKUST"), TextBox).Enabled = False
        CType(dict("tpjKDSALES"), TextBox).Enabled = False
        CType(dict("tpjNMSALES"), TextBox).Enabled = False
        CType(dict("tpjKDHARGA"), TextBox).Enabled = False
        CType(dict("tpjKET"), TextBox).Enabled = False
        CType(dict("tpjALAMAT"), TextBox).Enabled = False
        CType(dict("GRID"), DataGridView).Enabled = False

        CType(dict("tpjTOTAL"), TextBox).Enabled = False
        CType(dict("tpjSUBTOTAL"), TextBox).Enabled = False
        CType(dict("tpjNPPN"), TextBox).Enabled = False
        CType(dict("tpjAPPN"), TextBox).Enabled = False
        CType(dict("tpjLAIN"), TextBox).Enabled = False
        CType(dict("tpjNDISK3"), TextBox).Enabled = False
        CType(dict("tpjADISK3"), TextBox).Enabled = False
        CType(dict("tpjNDISK2"), TextBox).Enabled = False
        CType(dict("tpjADISK2"), TextBox).Enabled = False
        CType(dict("tpjNDISK1"), TextBox).Enabled = False
        CType(dict("tpjADISK1"), TextBox).Enabled = False

        tpjSNONOTA.Enabled = True
        dtpjTGL1.Enabled = True
        dtpjTGL2.Enabled = True
        tpjSNMKUST.Enabled = True
        tpjSNMSLS.Enabled = True
        tpjSLUNAS.Enabled = True

        CType(dict("btnADDITEM"), Button).Enabled = False
    End Sub

    Private Sub EnabledLoad()
        If TabControl1.SelectedTab Is Nothing Then Exit Sub
        Dim nomor As Integer = Integer.Parse(TabControl1.SelectedTab.Text.Replace("Nota ", ""))
        If Not TabControls.ContainsKey(nomor) Then Exit Sub
        Dim dict = TabControls(nomor)

        CType(dict("tpjNONOTA"), TextBox).Enabled = True
        CType(dict("tpjTANGGAL"), DateTimePicker).Enabled = True
        CType(dict("tpjTEMPO"), DateTimePicker).Enabled = True
        CType(dict("tpjKDKUST"), TextBox).Enabled = True
        CType(dict("tpjNMKUST"), TextBox).Enabled = True
        CType(dict("tpjKDSALES"), TextBox).Enabled = True
        CType(dict("tpjNMSALES"), TextBox).Enabled = True
        CType(dict("tpjKDHARGA"), TextBox).Enabled = True
        CType(dict("tpjKET"), TextBox).Enabled = True
        CType(dict("tpjALAMAT"), TextBox).Enabled = False
        CType(dict("GRID"), DataGridView).Enabled = True

        CType(dict("tpjTOTAL"), TextBox).Enabled = False
        CType(dict("tpjSUBTOTAL"), TextBox).Enabled = False
        CType(dict("tpjNPPN"), TextBox).Enabled = False
        CType(dict("tpjAPPN"), TextBox).Enabled = True
        CType(dict("tpjLAIN"), TextBox).Enabled = True
        CType(dict("tpjNDISK3"), TextBox).Enabled = False
        CType(dict("tpjADISK3"), TextBox).Enabled = True
        CType(dict("tpjNDISK2"), TextBox).Enabled = False
        CType(dict("tpjADISK2"), TextBox).Enabled = True
        CType(dict("tpjNDISK1"), TextBox).Enabled = False
        CType(dict("tpjADISK1"), TextBox).Enabled = True

        tpjSNONOTA.Enabled = False
        dtpjTGL1.Enabled = False
        dtpjTGL2.Enabled = False
        tpjSNMKUST.Enabled = False
        tpjSNMSLS.Enabled = False
        tpjSLUNAS.Enabled = False

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
            Dim sqlHead As String = "SELECT z.tgl, z.tgltempo, z.ket, z.kodekust, k.namakust, z.kodesls, s.namasls, k.alamat, z.nilai, z.lunas, z.disc1, z.hdisc1, z.disc2, z.hdisc2, z.disc3, z.hdisc3, z.ppn, z.hppn, z.lainnya FROM zjual z LEFT JOIN zkustomer k ON z.kodekust = k.kodekust LEFT JOIN zsales s ON z.kodesls = s.kodesls WHERE z.nonota = ?"
            Using cmd As New OdbcCommand(sqlHead, Conn)
                cmd.Parameters.AddWithValue("@nonota", nonota)
                Rd = cmd.ExecuteReader()
                If Rd.Read() Then
                    Dim aktifTab As TabPage = TabControl1.SelectedTab
                    Dim nomor As Integer = Integer.Parse(aktifTab.Text.Replace("Nota ", ""))
                    Dim dict = TabControls(nomor)

                    CType(dict("tpjNONOTA"), TextBox).Text = nonota
                    CType(dict("tpjTANGGAL"), DateTimePicker).Value = CDate(Rd("tgl"))
                    CType(dict("tpjTEMPO"), DateTimePicker).Value = CDate(Rd("tgltempo"))
                    CType(dict("tpjKET"), TextBox).Text = Rd("ket").ToString()
                    CType(dict("tpjKDKUST"), TextBox).Text = Rd("kodekust").ToString()
                    CType(dict("tpjNMKUST"), TextBox).Text = Rd("namakust").ToString()
                    CType(dict("tpjKDSALES"), TextBox).Text = Rd("kodesls").ToString()
                    CType(dict("tpjNMSALES"), TextBox).Text = Rd("namasls").ToString()
                    CType(dict("tpjALAMAT"), TextBox).Text = Rd("alamat").ToString()
                    CType(dict("tpjTOTAL"), TextBox).Text = Rd("nilai").ToString()
                    CType(dict("tpjADISK1"), TextBox).Text = Rd("disc1").ToString()
                    CType(dict("tpjNDISK1"), TextBox).Text = Rd("hdisc1").ToString()
                    CType(dict("tpjADISK2"), TextBox).Text = Rd("disc2").ToString()
                    CType(dict("tpjNDISK2"), TextBox).Text = Rd("hdisc2").ToString()
                    CType(dict("tpjADISK3"), TextBox).Text = Rd("disc3").ToString()
                    CType(dict("tpjNDISK3"), TextBox).Text = Rd("hdisc3").ToString()
                    CType(dict("tpjAPPN"), TextBox).Text = Rd("ppn").ToString()
                    CType(dict("tpjNPPN"), TextBox).Text = Rd("hppn").ToString()
                    CType(dict("tpjLAIN"), TextBox).Text = Rd("lainnya").ToString()
                End If
                Rd.Close()
            End Using

            ' ================== DETAIL ==================
            Dim sqlDet As String = "SELECT d.kodebrg, s.namabrg, d.kodegd, d.jlh1, s.satuan1, d.jlh2, s.satuan2, d.jlh3, s.satuan3, d.harga, d.disca, d.discb, d.discc, d.discrp, d.jumlah FROM zjualm d LEFT JOIN zstok s ON d.kodebrg = s.kodebrg WHERE d.nonota = ?"
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
        dtpjTGL1.Enabled = Not nonotaActive
        dtpjTGL2.Enabled = Not nonotaActive
        tpjSNMKUST.Enabled = Not nonotaActive
        tpjSNMSLS.Enabled = Not nonotaActive
        tpjSLUNAS.Enabled = Not nonotaActive
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

        dict("tpjTANGGAL") = tab.Controls.Find("tpjTANGGAL", True)(0)
        dict("tpjNONOTA") = tab.Controls.Find("tpjNONOTA", True)(0)
        dict("tpjTEMPO") = tab.Controls.Find("tpjTEMPO", True)(0)
        dict("tpjKET") = tab.Controls.Find("tpjKET", True)(0)
        dict("tpjKDKUST") = tab.Controls.Find("tpjKDKUST", True)(0)
        dict("tpjNMKUST") = tab.Controls.Find("tpjNMKUST", True)(0)
        dict("tpjKDSALES") = tab.Controls.Find("tpjKDSALES", True)(0)
        dict("tpjNMSALES") = tab.Controls.Find("tpjNMSALES", True)(0)
        dict("tpjKDHARGA") = tab.Controls.Find("tpjKDHARGA", True)(0)
        dict("tpjALAMAT") = tab.Controls.Find("tpjALAMAT", True)(0)
        dict("tpjTOTAL") = tab.Controls.Find("tpjTOTAL", True)(0)
        dict("tpjSUBTOTAL") = tab.Controls.Find("tpjSUBTOTAL", True)(0)
        dict("tpjNPPN") = tab.Controls.Find("tpjNPPN", True)(0)
        dict("tpjAPPN") = tab.Controls.Find("tpjAPPN", True)(0)
        dict("tpjLAIN") = tab.Controls.Find("tpjLAIN", True)(0)

        CType(dict("tpjAPPN"), TextBox).Text = "11"

        dict("tpjNDISK3") = tab.Controls.Find("tpjNDISK3", True)(0)
        dict("tpjADISK3") = tab.Controls.Find("tpjADISK3", True)(0)
        dict("tpjNDISK2") = tab.Controls.Find("tpjNDISK2", True)(0)
        dict("tpjADISK2") = tab.Controls.Find("tpjADISK2", True)(0)
        dict("tpjNDISK1") = tab.Controls.Find("tpjNDISK1", True)(0)
        dict("tpjADISK1") = tab.Controls.Find("tpjADISK1", True)(0)

        ' Grid
        Dim grid As DataGridView = CType(tab.Controls.Find("grPENJUALAN", True)(0), DataGridView)
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
        AddHandler CType(dict("tpjKDKUST"), TextBox).KeyDown, AddressOf tpjKDKUST_KeyDown
        AddHandler CType(dict("tpjNMKUST"), TextBox).KeyDown, AddressOf tpjNMKUST_KeyDown
        AddHandler CType(dict("tpjKDSALES"), TextBox).KeyDown, AddressOf tpjKDSALES_KeyDown
        AddHandler CType(dict("tpjNMSALES"), TextBox).KeyDown, AddressOf tpjNMSALES_KeyDown

        Dim txtADISK1 As TextBox = CType(dict("tpjADISK1"), TextBox)
        Dim txtADISK2 As TextBox = CType(dict("tpjADISK2"), TextBox)
        Dim txtADISK3 As TextBox = CType(dict("tpjADISK3"), TextBox)
        Dim txtAPPN As TextBox = CType(dict("tpjAPPN"), TextBox)
        Dim txtLAIN As TextBox = CType(dict("tpjLAIN"), TextBox)

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
        Dim btn As Button = CType(dict("btnADDITEM"), Button)

        RemoveHandler btn.Click, AddressOf btnADDITEM_Click
        AddHandler btn.Click, AddressOf btnADDITEM_Click

        If TabStatus.ContainsKey(nomor) Then
            TabStatus(nomor) = String.Empty
        Else
            TabStatus.Add(nomor, String.Empty)
        End If
        tab.Tag = String.Empty
    End Sub

    ' ================== TAMBAH TAB ==================
    Private Sub bpjTabAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bpjTabAdd.Click
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
            SetupGridPenjualan(grid)
        End If

        SetTabStatus(nomor, String.Empty)
        TabControl1.SelectedTab = newTab
        TabLoadState(nomor) = True
        TabButtonState(nomor) = True
        SetButtonState(Me, True)

    End Sub

    ' ================== HAPUS TAB ==================
    Private Sub bpjTabDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bpjTabDel.Click
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

            ' Ambil nomor nota dari tab aktif
            Dim nonota As String = CType(dict("tpjNONOTA"), TextBox).Text.Trim()
            If String.IsNullOrEmpty(nonota) Then
                MessageBox.Show("Nomor nota belum diisi!", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            Dim status As String = GetTabStatus(nomor)

            BukaKoneksi()
            Dim sqlCek As String = "SELECT COUNT(*) FROM zjual WHERE nonota = ?"
            Dim exists As Boolean = False
            Using cmd As New OdbcCommand(sqlCek, Conn)
                cmd.Parameters.AddWithValue("@nonota", nonota)
                exists = Convert.ToInt32(cmd.ExecuteScalar()) > 0
            End Using
            Conn.Close()

            ' === If / Else ===
            If exists AndAlso status = "tambah" Then
                MessageBox.Show("Nomor nota '" & nonota & "' sudah terdaftar!" & vbCrLf &
                                "", "Validasi",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub

            ElseIf (Not exists AndAlso status = "tambah") OrElse (exists AndAlso status = "ubah") Then
                ' Jalankan simpan

                SimpanPenjualan(nomor, dict, status)

                MessageBox.Show("Data penjualan berhasil disimpan untuk " & aktifTab.Text, "Sukses")

                TabLoadState(nomor) = True
                TabButtonState(nomor) = True
                SetButtonState(Me, True)
                DisabledLoad()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error Simpan")
        End Try
    End Sub

    Public Sub SimpanPenjualan(ByVal nomor As Integer,
                              ByVal dict As Dictionary(Of String, Control),
                              ByVal status As String)

        Try
            Dim tgl As String = CType(dict("tpjTANGGAL"), DateTimePicker).Value.ToString("yyyy-MM-dd")
            Dim nonota As String = CType(dict("tpjNONOTA"), TextBox).Text.Trim()
            Dim tempo As String = CType(dict("tpjTEMPO"), DateTimePicker).Value.ToString("yyyy-MM-dd")
            Dim ket As String = CType(dict("tpjKET"), TextBox).Text.Trim()
            Dim kodekust As String = CType(dict("tpjKDKUST"), TextBox).Text.Trim()
            Dim namakust As String = CType(dict("tpjNMKUST"), TextBox).Text.Trim()
            Dim kodesls As String = CType(dict("tpjKDSALES"), TextBox).Text.Trim()
            Dim namasls As String = CType(dict("tpjNMSALES"), TextBox).Text.Trim()
            Dim alamat As String = CType(dict("tpjALAMAT"), TextBox).Text.Trim()
            Dim grid As DataGridView = CType(dict("GRID"), DataGridView)

            Dim adisk1 As Double = Val(CType(dict("tpjADISK1"), TextBox).Text.Trim())
            Dim ndisk1 As Double = Val(CType(dict("tpjNDISK1"), TextBox).Text.Trim())
            Dim adisk2 As Double = Val(CType(dict("tpjADISK2"), TextBox).Text.Trim())
            Dim ndisk2 As Double = Val(CType(dict("tpjNDISK2"), TextBox).Text.Trim())
            Dim adisk3 As Double = Val(CType(dict("tpjADISK3"), TextBox).Text.Trim())
            Dim ndisk3 As Double = Val(CType(dict("tpjNDISK3"), TextBox).Text.Trim())
            Dim lain As Double = Val(CType(dict("tpjLAIN"), TextBox).Text.Trim())
            Dim appn As Double = Val(CType(dict("tpjAPPN"), TextBox).Text.Trim())
            Dim nppn As Double = Val(CType(dict("tpjNPPN"), TextBox).Text.Trim())
            Dim total As Double = Val(CType(dict("tpjTOTAL"), TextBox).Text.Trim())

            ' --- Validasi ---
            If nonota = "" Then Throw New Exception("No Nota tidak boleh kosong!")
            If kodekust = "" Then Throw New Exception("Kustomer belum dipilih!")
            If kodesls = "" Then Throw New Exception("Sales belum dipilih!")

            ' --- Buka koneksi ---
            BukaKoneksi()
            Trans = Conn.BeginTransaction()

            ' --- Jika UBAH: hapus data lama dulu ---
            If status = "ubah" Then
                KodeLama = nonota ' simpan kode lama

                Dim sqldel1 As String = "DELETE FROM zjualm WHERE nonota = ?"
                Using CmdDel1 As New OdbcCommand(sqldel1, Conn, Trans)
                    CmdDel1.Parameters.AddWithValue("@nonota", KodeLama)
                    CmdDel1.ExecuteNonQuery()
                End Using

                Dim sqldel2 As String = "DELETE FROM zjual WHERE nonota = ?"
                Using CmdDel2 As New OdbcCommand(sqldel2, Conn, Trans)
                    CmdDel2.Parameters.AddWithValue("@nonota", KodeLama)
                    CmdDel2.ExecuteNonQuery()
                End Using
            End If

            ' --- Simpan Header ---
            Dim sql As String = "INSERT INTO zjual (tgl, nonota, tgltempo, ket, kodekust, kodesls, nilai, lunas, disc1, hdisc1, disc2, hdisc2, disc3, hdisc3, ppn, hppn, lainnya) " &
                                "VALUES (?, ?, ?, ?, ?, ?, ?, 0, ?, ?, ?, ?, ?, ?, ?, ?, ?)"
            Using Cmd As New OdbcCommand(sql, Conn, Trans)
                Cmd.Parameters.AddWithValue("@tgl", tgl)
                Cmd.Parameters.AddWithValue("@nonota", nonota)
                Cmd.Parameters.AddWithValue("@tgltempo", tempo)
                Cmd.Parameters.AddWithValue("@ket", ket)
                Cmd.Parameters.AddWithValue("@kodekust", kodekust)
                Cmd.Parameters.AddWithValue("@kodesls", kodesls)
                Cmd.Parameters.AddWithValue("@nilai", total)
                Cmd.Parameters.AddWithValue("@disc1", adisk1)
                Cmd.Parameters.AddWithValue("@hdisc1", ndisk1)
                Cmd.Parameters.AddWithValue("@disc2", adisk2)
                Cmd.Parameters.AddWithValue("@hdisc2", ndisk2)
                Cmd.Parameters.AddWithValue("@disc3", adisk3)
                Cmd.Parameters.AddWithValue("@hdisc3", ndisk3)
                Cmd.Parameters.AddWithValue("@ppn", appn)
                Cmd.Parameters.AddWithValue("@hppn", nppn)
                Cmd.Parameters.AddWithValue("@lainnya", lain)
                Cmd.ExecuteNonQuery()
            End Using

            ' --- Simpan Detail ---
            For Each row As DataGridViewRow In grid.Rows
                If row.IsNewRow Then Continue For

                Dim kodebrg As String = If(row.Cells("KodeBrg").Value, "").ToString.Trim()
                If kodebrg = "" Then Continue For

                Dim kodegd As String = If(row.Cells("KodeGudang").Value, "").ToString.Trim()
                Dim jlh1 As Double = Val(If(row.Cells("Jlh1").Value, 0))
                Dim jlh2 As Double = Val(If(row.Cells("Jlh2").Value, 0))
                Dim jlh3 As Double = Val(If(row.Cells("Jlh3").Value, 0))
                Dim harga As Double = Val(If(row.Cells("Harga").Value, 0))
                Dim disca As Double = Val(If(row.Cells("Disca").Value, 0))
                Dim discb As Double = Val(If(row.Cells("Discb").Value, 0))
                Dim discc As Double = Val(If(row.Cells("Discc").Value, 0))
                Dim discrp As Double = Val(If(row.Cells("DiscRp").Value, 0))
                Dim jumlah As Double = Val(If(row.Cells("Jumlah").Value, 0))

                Dim sqldet As String = "INSERT INTO zjualm " &
                    "(nonota, kodebrg, kodegd, jlh1, jlh2, jlh3, harga, disca, hdisca, discb, hdiscb, discc, hdiscc, discrp, jumlah, operator) " &
                    "VALUES (?, ?, ?, ?, ?, ?, ?, ?, 0, ?, 0, ?, 0, ?, ?, ?)"

                Using CmdDet As New OdbcCommand(sqldet, Conn, Trans)
                    CmdDet.Parameters.AddWithValue("@nonota", nonota)
                    CmdDet.Parameters.AddWithValue("@kodebrg", kodebrg)
                    CmdDet.Parameters.AddWithValue("@kodegd", kodegd)
                    CmdDet.Parameters.AddWithValue("@jlh1", jlh1)
                    CmdDet.Parameters.AddWithValue("@jlh2", jlh2)
                    CmdDet.Parameters.AddWithValue("@jlh3", jlh3)
                    CmdDet.Parameters.AddWithValue("@harga", harga)
                    CmdDet.Parameters.AddWithValue("@disca", disca)
                    CmdDet.Parameters.AddWithValue("@discb", discb)
                    CmdDet.Parameters.AddWithValue("@discc", discc)
                    CmdDet.Parameters.AddWithValue("@discrp", discrp)
                    CmdDet.Parameters.AddWithValue("@jumlah", jumlah)
                    CmdDet.Parameters.AddWithValue("@operator", Environment.UserName)
                    CmdDet.ExecuteNonQuery()
                End Using
            Next

            ' --- Commit ---
            Trans.Commit()
            Conn.Close()

        Catch ex As Exception
            Try
                If Trans IsNot Nothing Then Trans.Rollback()
                If Conn IsNot Nothing AndAlso Conn.State = ConnectionState.Open Then Conn.Close()
            Catch
            End Try
            Throw New Exception("Error Simpan: " & ex.Message)
        End Try
    End Sub

    Private Sub btnTAMBAH_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTAMBAH.Click
        ' === Ambil ID menu dari properti Tag form ===
        Dim idMenu As String = Me.Tag.ToString()

        ' === Ambil hak akses user aktif ===
        Dim akses = GetAkses(KodeUserLogin, idMenu)

        ' === Cek apakah user boleh tambah ===
        If Not akses("tambah") Then
            MessageBox.Show("Tidak bisa akses tambah", "Akses Ditolak", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        Else
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
        End If
    End Sub

    Private Sub btnUBAH_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUBAH.Click
        ' === Ambil ID menu dari properti Tag form ===
        Dim idMenu As String = Me.Tag.ToString()

        ' === Ambil hak akses user aktif ===
        Dim akses = GetAkses(KodeUserLogin, idMenu)

        If Not akses("ubah") Then
            MessageBox.Show("Tidak bisa akses ubah", "Akses Ditolak", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        Else
            Dim nomor As Integer = Integer.Parse(TabControl1.SelectedTab.Text.Replace("Nota ", ""))

            If Not TabControls.ContainsKey(nomor) Then
                RegisterTabControls(TabControl1.SelectedTab, nomor)
            End If

            ' ambil kontrol tpjNONOTA dari tab aktif
            Dim tpjNONOTA As TextBox = TryCast(TabControls(nomor)("tpjNONOTA"), TextBox)

            ' cek apakah kosong
            If tpjNONOTA Is Nothing OrElse String.IsNullOrWhiteSpace(tpjNONOTA.Text) Then
                MessageBox.Show("Tidak ada data yang bisa di edit", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            ' set status
            SetTabStatus(nomor, "ubah")

            TabLoadState(nomor) = False
            TabButtonState(nomor) = False
            SetButtonState(Me, False)
            EnabledLoad()
        End If
    End Sub

    Private Sub btnHAPUS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHAPUS.Click
        ' === Ambil ID menu dari properti Tag form ===
        Dim idMenu As String = Me.Tag.ToString()

        ' === Ambil hak akses user aktif ===
        Dim akses = GetAkses(KodeUserLogin, idMenu)

        If Not akses("hapus") Then
            MessageBox.Show("Tidak bisa akses hapus", "Akses Ditolak", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        Else
            Try
                Dim aktifTab As TabPage = TabControl1.SelectedTab
                Dim nomor As Integer = Integer.Parse(aktifTab.Text.Replace("Nota ", ""))

                If Not TabControls.ContainsKey(nomor) Then Exit Sub
                Dim dict = TabControls(nomor)

                Dim nonota As String = CType(dict("tpjNONOTA"), TextBox).Text.Trim()

                If String.IsNullOrEmpty(nonota) Then
                    MessageBox.Show("Tidak ada nota yang bisa dihapus.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If

                If MessageBox.Show("Yakin hapus nota " & nonota & " ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then
                    Exit Sub
                End If

                ' === Hapus data dari database (module) ===
                HapusPembelian(nonota, dict)

                ' === Update status tab setelah hapus ===
                TabLoadState(nomor) = True
                TabButtonState(nomor) = True
                SetButtonState(Me, True)
                DisabledLoad()

                MessageBox.Show("Nota " & nonota & " berhasil dihapus.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information)

            Catch ex As Exception
                MessageBox.Show(ex.Message, "Error Hapus", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    Public Sub HapusPembelian(ByVal nonota As String,
                          ByVal dict As Dictionary(Of String, Control))

        If String.IsNullOrEmpty(nonota) Then
            Throw New Exception("No Nota kosong, tidak bisa dihapus.")
        End If

        ' --- Buka koneksi & transaksi ---
        BukaKoneksi()
        Trans = Conn.BeginTransaction()

        Try
            ' hapus detail
            Using cmdDet As New OdbcCommand("DELETE FROM zjualm WHERE nonota = ?", Conn, Trans)
                cmdDet.Parameters.AddWithValue("@nonota", nonota)
                cmdDet.ExecuteNonQuery()
            End Using

            ' hapus header
            Using cmdHead As New OdbcCommand("DELETE FROM zjual WHERE nonota = ?", Conn, Trans)
                cmdHead.Parameters.AddWithValue("@nonota", nonota)
                cmdHead.ExecuteNonQuery()
            End Using

            Trans.Commit()
            Conn.Close()

            ' bersihkan form/tab setelah hapus
            CType(dict("tpjNONOTA"), TextBox).Clear()
            CType(dict("tpjNMKUST"), TextBox).Clear()
            CType(dict("tpjKDKUST"), TextBox).Clear()
            CType(dict("tpjNMSALES"), TextBox).Clear()
            CType(dict("tpjKDSALES"), TextBox).Clear()
            CType(dict("tpjKDHARGA"), TextBox).Clear()
            CType(dict("tpjALAMAT"), TextBox).Clear()
            CType(dict("tpjKET"), TextBox).Clear()
            CType(dict("GRID"), DataGridView).Rows.Clear()
            CType(dict("tpjSUBTOTAL"), TextBox).Clear()
            CType(dict("tpjNDISK1"), TextBox).Clear()
            CType(dict("tpjADISK1"), TextBox).Clear()
            CType(dict("tpjNDISK2"), TextBox).Clear()
            CType(dict("tpjADISK2"), TextBox).Clear()
            CType(dict("tpjNDISK3"), TextBox).Clear()
            CType(dict("tpjADISK3"), TextBox).Clear()
            CType(dict("tpjLAIN"), TextBox).Clear()
            CType(dict("tpjNPPN"), TextBox).Clear()
            CType(dict("tpjAPPN"), TextBox).Clear()
            CType(dict("tpjTOTAL"), TextBox).Clear()


        Catch ex As Exception
            Try
                If Trans IsNot Nothing Then Trans.Rollback()
                If Conn IsNot Nothing AndAlso Conn.State = ConnectionState.Open Then Conn.Close()
            Catch
            End Try
            Throw New Exception("Error Hapus: " & ex.Message)
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

        CType(dict("tpjSUBTOTAL"), TextBox).Text = subtotal.ToString()
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

    ' ================== KEYDOWN KUSTOMER ==================
    Private Sub tpjKDKUST_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs)
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            Dim txtKode As TextBox = CType(sender, TextBox)
            If txtKode.Text.Trim() <> "" Then
                Dim f As New ItKustomer
                f.Owner = Me
                f.Show()
                f.LoadDataKust(txtKode.Text.Trim())
            End If
        End If
    End Sub

    Private Sub tpjNMKUST_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs)
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            Dim txtNama As TextBox = CType(sender, TextBox)
            If txtNama.Text.Trim() <> "" Then
                Dim f As New ItKustomer
                f.Owner = Me
                f.Show()
                f.LoadDataKust(txtNama.Text.Trim())
            End If
        End If
    End Sub

    Private Sub tpjKDSALES_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs)
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            Dim txtKode As TextBox = CType(sender, TextBox)
            If txtKode.Text.Trim() <> "" Then
                Dim f As New ItSales
                f.Owner = Me
                f.Show()
                f.LoadDataSls(txtKode.Text.Trim())
            End If
        End If
    End Sub

    Private Sub tpjNMSALES_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs)
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            Dim txtNama As TextBox = CType(sender, TextBox)
            If txtNama.Text.Trim() <> "" Then
                Dim f As New ItSales
                f.Owner = Me
                f.Show()
                f.LoadDataSls(txtNama.Text.Trim())
            End If
        End If
    End Sub

    Private Sub btnCARI_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCARI.Click
        Dim f As New ItDtPenjualan()

        Dim nonota As String = tpjSNONOTA.Text.Trim()
        Dim tgl1 As String = dtpjTGL1.Value.ToString("yyyy-MM-dd")
        Dim tgl2 As String = dtpjTGL2.Value.ToString("yyyy-MM-dd")
        Dim namasup As String = tpjSNMKUST.Text.Trim()
        Dim status As String = ""

        ' Tentukan status lunas hanya jika nonota kosong
        If nonota = "" Then
            If tpjSLUNAS.SelectedIndex = 0 Then
                status = "belum"
            ElseIf tpjSLUNAS.SelectedIndex = 1 Then
                status = "lunas"
            End If
        End If

        ' Load data sesuai filter
        f.Owner = Me
        f.Show()
        f.LoadDataJual(nonota, tgl1, tgl2, namasup, status)

        ' === Clear filter setelah pencarian ===
        tpjSNONOTA.Clear()
        tpjSNMKUST.Clear()
        tpjSLUNAS.SelectedIndex = -1 ' reset pilihan
        ' reset tanggal ke hari ini
        dtpjTGL1.Value = DateTime.Today
        dtpjTGL2.Value = DateTime.Today
    End Sub

    Private Sub tpjSNONOTA_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tpjSNONOTA.TextChanged
        If tpjSNONOTA.Text.Trim() <> "" Then
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

        Dim subtotal As Decimal = Val(CType(dict("tpjSUBTOTAL"), TextBox).Text)
        Dim adisk1 As Decimal = Val(CType(dict("tpjADISK1"), TextBox).Text)
        Dim adisk2 As Decimal = Val(CType(dict("tpjADISK2"), TextBox).Text)
        Dim adisk3 As Decimal = Val(CType(dict("tpjADISK3"), TextBox).Text)
        Dim appn As Decimal = Val(CType(dict("tpjAPPN"), TextBox).Text)
        Dim lain As Decimal = Val(CType(dict("tpjLAIN"), TextBox).Text)

        Dim hasil = ModHitung.HitungSubtotalTotal(subtotal, adisk1, adisk2, adisk3, appn, lain)

        CType(dict("tpjNDISK1"), TextBox).Text = hasil("hdisca").ToString()
        CType(dict("tpjNDISK2"), TextBox).Text = hasil("hdiscb").ToString()
        CType(dict("tpjNDISK3"), TextBox).Text = hasil("hdiscc").ToString()
        CType(dict("tpjNPPN"), TextBox).Text = hasil("ppn").ToString()
        CType(dict("tpjTOTAL"), TextBox).Text = hasil("total").ToString()
    End Sub

    Private Sub OnlyNumber_KeyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs) _
        Handles tpjSUBTOTAL.KeyPress, tpjTOTAL.KeyPress, tpjADISK1.KeyPress,
                tpjADISK2.KeyPress, tpjADISK3.KeyPress, tpjNDISK1.KeyPress,
                tpjNDISK2.KeyPress, tpjNDISK3.KeyPress, tpjLAIN.KeyPress,
                tpjAPPN.KeyPress, tpjNPPN.KeyPress

        AngkaHelper.HanyaAngka(e)
    End Sub

    Private Sub btnPRINT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPRINT.Click
        Try
            Dim aktifTab As TabPage = TabControl1.SelectedTab
            Dim nomor As Integer = Integer.Parse(aktifTab.Text.Replace("Nota ", ""))
            Dim dict = TabControls(nomor)

            Dim nonota As String = CType(dict("tpjNONOTA"), TextBox).Text

            If String.IsNullOrEmpty(nonota) Then
                MessageBox.Show("Pilih nota yang akan dicetak!", "Info")
                Exit Sub
            End If

            ' --- Buka form cetak ---
            Dim f As New FCetak()
            f.Param("nonota") = nonota
            f.Param("jenis") = "penjualan"
            f.Param("tipe") = "nota"
            f.ShowDialog()

        Catch ex As Exception
            MessageBox.Show("Gagal cetak nota: " & ex.Message, "Error")
        End Try
    End Sub
End Class