Imports System.Data.Odbc

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
                Me.Text = "PRODIGY  |  USER : " & NamaUserLogin
                ' Login sukses → dashboard lanjut berjalan
            ElseIf result = DialogResult.Cancel Then
                ' Kalau user batal, keluar aplikasi
                Application.Exit()
                Exit While
            End If
        End While

        ' Buat TabControl
        ' === Koneksi database ===
        BukaKoneksi()

        ' === Buat menu otomatis dari database ===
        BuatMenuDariDatabase()
    End Sub

    Private Function BersihkanNama(ByVal rawName As String) As String
        If rawName.StartsWith("sm") Then
            rawName = rawName.Substring(2)
        ElseIf rawName.StartsWith("m") Then
            rawName = rawName.Substring(1)
        End If

        ' Ubah huruf pertama jadi kapital, sisanya huruf kecil
        If rawName.Length > 0 Then
            rawName = Char.ToUpper(rawName(0)) & rawName.Substring(1)
        End If

        Return rawName
    End Function

    Private Sub BuatMenuDariDatabase()
        Try
            hMenu.Items.Clear()

            ' Ambil daftar menu dari database
            Dim sql As String = "SELECT idmenu, mainmenu, submenu, urutan FROM zmenu ORDER BY mainmenu, urutan"
            Cmd = New OdbcCommand(sql, Conn)
            Rd = Cmd.ExecuteReader()

            Dim menuDict As New Dictionary(Of String, List(Of Dictionary(Of String, String)))()

            While Rd.Read()
                Dim main As String = Rd("mainmenu").ToString()
                Dim subm As String = Rd("submenu").ToString()
                Dim idMenu As String = Rd("idmenu").ToString()

                ' Cek akses berdasarkan user login
                Dim akses = GetAkses(KodeUserLogin, idMenu)

                ' Kalau user punya hak (minimal satu tidak 0), baru tampilkan submenu
                If akses("tambah") Or akses("ubah") Or akses("hapus") Then
                    If Not menuDict.ContainsKey(main) Then
                        menuDict(main) = New List(Of Dictionary(Of String, String))()
                    End If
                    menuDict(main).Add(New Dictionary(Of String, String) From {
                        {"submenu", subm},
                        {"idmenu", idMenu}
                    })
                End If
            End While
            Rd.Close()

            ' === Buat menu & submenu dinamis ===
            For Each mainMenuName In menuDict.Keys
                If menuDict(mainMenuName).Count = 0 Then Continue For ' skip kalau semua submenu kosong

                Dim mainItem As New ToolStripMenuItem(BersihkanNama(mainMenuName))
                hMenu.Items.Add(mainItem)

                For Each subData In menuDict(mainMenuName)
                    Dim submName = subData("submenu")
                    Dim idMenu = subData("idmenu")

                    Dim subItem As New ToolStripMenuItem(BersihkanNama(submName))
                    subItem.Tag = idMenu ' penting: Tag = idmenu agar bisa cek akses per form
                    AddHandler subItem.Click, AddressOf SubMenu_Click
                    mainItem.DropDownItems.Add(subItem)
                Next
            Next

        Catch ex As Exception
            MessageBox.Show("Gagal membuat menu: " & ex.Message)
        End Try
    End Sub

    Private Sub SubMenu_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim item As ToolStripMenuItem = CType(sender, ToolStripMenuItem)
        Dim idMenu As String = item.Tag.ToString()

        Select Case idMenu
            Case "ME001"
                Dim f As New FStok()
                f.Tag = idMenu
                f.MdiParent = Me
                f.Show()

            Case "ME002"
                Dim f As New FArea()
                f.Tag = idMenu
                f.MdiParent = Me
                f.Show()

            Case "ME003"
                Dim f As New FTipe()
                f.Tag = idMenu
                f.MdiParent = Me
                f.Show()

            Case "ME004"
                Dim f As New FKustomer()
                f.Tag = idMenu
                f.MdiParent = Me
                f.Show()

            Case "ME005"
                Dim f As New FSupplier()
                f.Tag = idMenu
                f.MdiParent = Me
                f.Show()

            Case "ME006"
                Dim f As New FSales()
                f.Tag = idMenu
                f.MdiParent = Me
                f.Show()

            Case "ME007"
                Dim f As New FGudang()
                f.Tag = idMenu
                f.MdiParent = Me
                f.Show()

            Case "ME008"
                Dim f As New FMerek()
                f.Tag = idMenu
                f.MdiParent = Me
                f.Show()

            Case "ME009"
                Dim f As New FGolongan()
                f.Tag = idMenu
                f.MdiParent = Me
                f.Show()

            Case "ME0010"
                Dim f As New FGrup()
                f.Tag = idMenu
                f.MdiParent = Me
                f.Show()

            Case "ME0011"
                Dim f As New FSetAkun()
                f.Tag = idMenu
                f.MdiParent = Me
                f.Show()

            Case "ME0012"
                Me.DialogResult = DialogResult.Cancel
                Me.Close()

            Case "ME0013"
                Dim f As New FPembelian()
                f.Tag = idMenu
                f.MdiParent = Me
                f.Show()

            Case "ME0014"
                Dim f As New FReturPembelian()
                f.Tag = idMenu
                f.MdiParent = Me
                f.Show()

            Case "ME0015"
                Dim f As New FPenjualan()
                f.Tag = idMenu
                f.MdiParent = Me
                f.Show()

            Case "ME0016"
                Dim f As New FReturPenjualan()
                f.Tag = idMenu
                f.MdiParent = Me
                f.Show()

            Case "ME0017"
                Dim f As New FMutasi()
                f.Tag = idMenu
                f.MdiParent = Me
                f.Show()

            Case "ME0018"
                Dim f As New FPenyesuaian()
                f.Tag = idMenu
                f.MdiParent = Me
                f.Show()

                ' Tambahkan case sesuai idmenu kamu di zmenu
                ' Case "ME003" ...
                ' Case "ME004" ...

            Case Else
                MessageBox.Show("Menu belum terhubung dengan form, ID: " & idMenu)
        End Select
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
End Class
