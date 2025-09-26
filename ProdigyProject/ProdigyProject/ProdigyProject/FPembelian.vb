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
        Me.FormBorderStyle = FormBorderStyle.FixedSingle
        Me.MaximizeBox = False

        ' isi dropdown lunas
        tpmSLUNAS.Items.Clear()
        tpmSLUNAS.Items.Add(New With {.Value = 0, .Text = "Belum Lunas"})
        tpmSLUNAS.Items.Add(New With {.Value = 1, .Text = "Lunas"})
        tpmSLUNAS.DisplayMember = "Text"
        tpmSLUNAS.ValueMember = "Value"
        tpmSLUNAS.SelectedValue = 0   ' default Belum Lunas

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
        tpmSLUNAS.Enabled = True

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
        tpmSLUNAS.Enabled = False

        CType(dict("btnADDITEM"), Button).Enabled = True
    End Sub

    ' ============= EVENT KLIK UBAH =============
    Private Sub GridUbah_Click(ByVal sender As Object, ByVal e As EventArgs)
        If grPEMBELIAN.CurrentRow Is Nothing Then Return

        Dim aktifTab As TabPage = TabControl1.SelectedTab
        Dim nomor As Integer = Integer.Parse(aktifTab.Text.Replace("Nota ", ""))
        Dim dict = TabControls(nomor)
        Dim grid As DataGridView = CType(dict("GRID"), DataGridView)
        Dim row As DataGridViewRow = grPEMBELIAN.CurrentRow
        Dim popup As New ItFPopup()
        Dim kodeBrg As String = row.Cells("KodeBrg").Value.ToString()
        Dim kodegd As String = row.Cells("KodeGudang").Value.ToString()

        popup.tPopKDBARANG.Text = kodeBrg

        popup.tPopJLH1.Text = Val(row.Cells("Jlh1").Value).ToString()
        popup.tPopJLH2.Text = Val(row.Cells("Jlh2").Value).ToString()
        popup.tPopJLH3.Text = Val(row.Cells("Jlh3").Value).ToString()

        popup.tPopDISCA.Text = Val(row.Cells("Disca").Value).ToString()
        popup.tPopDISCB.Text = Val(row.Cells("Discb").Value).ToString()
        popup.tPopDISCC.Text = Val(row.Cells("Discc").Value).ToString()
        popup.tPopDISRP.Text = Val(row.Cells("DiscRp").Value).ToString()

        ' === Popup akan load Nama, Satuan, Stok, Harga dari DB berdasarkan KodeBrg ===
        popup.TargetGrid = grid
        popup.IsEditMode = True
        popup.LoadBarangInfo(kodeBrg, kodegd)

        ' === Tampilkan popup ===
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
    End Sub

    ' ============= EVENT KLIK HAPUS =============
    Private Sub GridHapus_Click(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim aktifTab As TabPage = TabControl1.SelectedTab
            Dim nomor As Integer = Integer.Parse(aktifTab.Text.Replace("Nota ", ""))
            Dim dict = TabControls(nomor)
            Dim grid As DataGridView = CType(dict("GRID"), DataGridView)

            If grid.CurrentRow Is Nothing OrElse grid.CurrentRow.IsNewRow Then
                MessageBox.Show("Pilih item yang valid!", "Info")
                Exit Sub
            End If

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

            ' ================== HEADER ==================
            Dim sqlHead As String = "SELECT z.tgl, z.tgltempo, z.ket, z.kodesup, s.namasup, s.alamat, z.nilai, z.lunas FROM zbeli z LEFT JOIN zsupplier s ON z.kodesup = s.kodesup WHERE z.nonota = ?"
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
        tpmSLUNAS.Enabled = Not nonotaActive
    End Sub

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

        ' Tambahkan event handler dinamis
        AddHandler CType(dict("tpmKDSUP"), TextBox).KeyDown, AddressOf tpmKDSUP_KeyDown
        AddHandler CType(dict("tpmNMSUP"), TextBox).KeyDown, AddressOf tpmNMSUP_KeyDown

        ' Event tombol Add Item
        AddHandler CType(dict("btnADDITEM"), Button).Click, AddressOf btnADDITEM_Click
    End Sub

    ' ================== CLONE CONTROL ==================
    Private Function CloneControl(ByVal original As Control) As Control
        Dim copy As Control = CType(Activator.CreateInstance(original.GetType()), Control)

        ' --- Properti dasar ---
        copy.Text = original.Text
        copy.Size = original.Size
        copy.Location = original.Location
        copy.Anchor = original.Anchor
        copy.Dock = original.Dock
        copy.Font = original.Font
        copy.BackColor = original.BackColor
        copy.ForeColor = original.ForeColor
        copy.AutoSize = original.AutoSize
        copy.Margin = original.Margin
        copy.Padding = original.Padding
        copy.Enabled = original.Enabled
        copy.Visible = original.Visible
        copy.TabIndex = original.TabIndex
        copy.TabStop = original.TabStop
        copy.Name = original.Name

        ' --- TextBox kosong ---
        If TypeOf copy Is TextBox Then
            CType(copy, TextBox).Clear()
        End If

        ' --- DateTimePicker reset ---
        If TypeOf copy Is DateTimePicker Then
            Dim dtp As DateTimePicker = CType(copy, DateTimePicker)
            dtp.Format = DateTimePickerFormat.Custom
            dtp.CustomFormat = "dd/MM/yyyy"
            dtp.Value = DateTime.Now
        End If

        ' --- DataGridView khusus ---
        If TypeOf copy Is DataGridView Then
            SetupGridPembelian(CType(copy, DataGridView))
        End If

        ' --- Clone anak kontrol ---
        For Each child As Control In original.Controls
            copy.Controls.Add(CloneControl(child))
        Next

        Return copy
    End Function

    ' ================== GET NOMOR TAB ==================
    Private Function GetNextNotaNumber() As Integer
        Dim usedNumbers As New List(Of Integer)

        For Each tp As TabPage In TabControl1.TabPages
            If tp.Text.StartsWith("Nota ") Then
                Dim numPart As String = tp.Text.Substring(5)
                Dim num As Integer
                If Integer.TryParse(numPart, num) Then
                    usedNumbers.Add(num)
                End If
            End If
        Next

        Dim candidate As Integer = 1
        If usedNumbers.Contains(1) Then candidate = 2
        Do While usedNumbers.Contains(candidate)
            candidate += 1
        Loop
        Return candidate
    End Function

    ' ================== TAMBAH TAB ==================
    Private Sub bpmTabAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bpmTabAdd.Click
        If TabPagesList.Count = 0 Then Return

        Dim nomor As Integer = GetNextNotaNumber()
        Dim nama As String = "Nota " & nomor
        Dim template As TabPage = TabControl1.SelectedTab
        Dim newTab As New TabPage(nama)

        For Each ctrl As Control In template.Controls
            newTab.Controls.Add(CloneControl(ctrl))
        Next

        TabControl1.TabPages.Add(newTab)
        TabPagesList.Add(newTab)
        RegisterTabControls(newTab, nomor)
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

            Dim tgl As String = CType(dict("tpmTANGGAL"), DateTimePicker).Value.ToString("yyyy-MM-dd")
            Dim nonota As String = CType(dict("tpmNONOTA"), TextBox).Text.Trim()
            Dim tempo As String = CType(dict("tpmTEMPO"), DateTimePicker).Value.ToString("yyyy-MM-dd")
            Dim ket As String = CType(dict("tpmKET"), TextBox).Text.Trim()
            Dim kodesup As String = CType(dict("tpmKDSUP"), TextBox).Text.Trim()
            Dim namasup As String = CType(dict("tpmNMSUP"), TextBox).Text.Trim()
            Dim alamat As String = CType(dict("tpmALAMAT"), TextBox).Text.Trim()
            Dim grid As DataGridView = CType(dict("GRID"), DataGridView)

            ' --- Validasi input ---
            If nonota = "" Then
                MessageBox.Show("No Nota tidak boleh kosong!", "Warning")
                Exit Sub
            End If
            If kodesup = "" Then
                MessageBox.Show("Supplier belum dipilih!", "Warning")
                Exit Sub
            End If

            ' --- Buka koneksi + mulai transaksi ---
            BukaKoneksi()
            Trans = Conn.BeginTransaction()

            ' --- Simpan Header ---
            Dim sql As String = "INSERT INTO zbeli (tgl, nonota, tgltempo, ket, kodesup, nilai, lunas) VALUES (?, ?, ?, ?, ?, 0, 0)"
            Using Cmd As New OdbcCommand(sql, Conn, Trans)
                Cmd.Parameters.AddWithValue("@tgl", tgl)
                Cmd.Parameters.AddWithValue("@nonota", nonota)
                Cmd.Parameters.AddWithValue("@tgltempo", tempo)
                Cmd.Parameters.AddWithValue("@ket", ket)
                Cmd.Parameters.AddWithValue("@kodesup", kodesup)
                Cmd.ExecuteNonQuery()
            End Using

            ' --- Simpan Detail ---
            For Each row As DataGridViewRow In grid.Rows
                If row.IsNewRow Then Continue For

                Dim kodebrg As String = If(row.Cells("KodeBrg").Value, "").ToString.Trim()
                If kodebrg = "" Then Continue For ' skip baris kosong

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

                Dim sqldet As String = "INSERT INTO zbelim " &
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
                    CmdDet.Parameters.AddWithValue("@operator", Environment.UserName) ' isi operator (username Windows)
                    CmdDet.ExecuteNonQuery()
                End Using
            Next

            ' --- Commit transaksi ---
            Trans.Commit()
            Conn.Close()

            MessageBox.Show("Data pembelian berhasil disimpan untuk " & aktifTab.Text, "Sukses")
            TabLoadState(nomor) = True
            TabButtonState(nomor) = True
            SetButtonState(Me, True)
            DisabledLoad()

        Catch ex As Exception
            Try
                If Trans IsNot Nothing Then
                    Trans.Rollback() ' batalkan transaksi kalau error
                End If
                If Conn IsNot Nothing AndAlso Conn.State = ConnectionState.Open Then
                    Conn.Close()
                End If
            Catch
            End Try

            MessageBox.Show("Gagal simpan data: " & ex.Message, "Error")
        End Try

    End Sub

    Private Sub btnTAMBAH_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTAMBAH.Click
        Dim nomor As Integer = Integer.Parse(TabControl1.SelectedTab.Text.Replace("Nota ", ""))
        TabLoadState(nomor) = False
        TabButtonState(nomor) = False
        SetButtonState(Me, False)
        EnabledLoad()
    End Sub

    Private Sub btnUBAH_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUBAH.Click
        Dim nomor As Integer = Integer.Parse(TabControl1.SelectedTab.Text.Replace("Nota ", ""))
        TabLoadState(nomor) = False
        TabButtonState(nomor) = False
        SetButtonState(Me, False)
        EnabledLoad()
    End Sub

    Private Sub btnHAPUS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHAPUS.Click
        Dim nomor As Integer = Integer.Parse(TabControl1.SelectedTab.Text.Replace("Nota ", ""))
        TabLoadState(nomor) = True
        TabButtonState(nomor) = True
        SetButtonState(Me, True)
        DisabledLoad()
    End Sub

    Private Sub btnBATAL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBATAL.Click
        Dim nomor As Integer = Integer.Parse(TabControl1.SelectedTab.Text.Replace("Nota ", ""))
        TabLoadState(nomor) = True
        TabButtonState(nomor) = True
        SetButtonState(Me, True)
        DisabledLoad()
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

            ' --- Tampilkan popup pilih barang ---
            Dim popup As New ItFPopup()
            popup.TargetGrid = grid
            popup.ShowDialog()

            ' --- Jika popup ada hasil, bisa langsung fokus ke row terakhir ---
            If grid.Rows.Count > 0 Then
                grid.CurrentCell = grid.Rows(grid.Rows.Count - 1).Cells(0)
                grid.BeginEdit(True)
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
            If tpmSLUNAS.SelectedIndex = 0 Then
                status = "belum"
            ElseIf tpmSLUNAS.SelectedIndex = 1 Then
                status = "lunas"
            End If
        End If

        ' Load data sesuai filter
        f.Owner = Me
        f.Show()
        f.LoadDataBeli(nonota, tgl1, tgl2, namasup, status)
    End Sub

    Private Sub tpmSLUNAS_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tpmSLUNAS.SelectedIndexChanged
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

        Dim nomor As Integer = Integer.Parse(TabControl1.SelectedTab.Text.Replace("Nota ", ""))

        ' === handle tombol ===
        If TabButtonState.ContainsKey(nomor) Then
            If TabLoadState(nomor) Then
                DisabledLoad()
            Else
                EnabledLoad()
            End If
            SetButtonState(Me, TabButtonState(nomor))
        Else
            TabButtonState(nomor) = True
            SetButtonState(Me, True)
        End If
    End Sub
End Class
