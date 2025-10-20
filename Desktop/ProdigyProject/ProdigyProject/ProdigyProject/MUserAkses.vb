Imports System.Data.Odbc

Module MUserAkses
    ' === Variabel global untuk menyimpan user login aktif ===
    Public KodeUserLogin As String = ""

    ' === Fungsi untuk mengambil data akses berdasarkan user dan idmenu ===
    Public Function GetAkses(ByVal kodeUser As String, ByVal idMenu As String) As Dictionary(Of String, Boolean)
        Dim akses As New Dictionary(Of String, Boolean) From {
            {"tambah", False},
            {"ubah", False},
            {"hapus", False},
            {"adaAkses", False}
        }

        Try
            Dim sql As String = "SELECT tambah, ubah, hapus FROM zakses WHERE kodeuser=? AND idmenu=?"
            Using cmd As New OdbcCommand(sql, Conn)
                cmd.Parameters.AddWithValue("@1", kodeUser)
                cmd.Parameters.AddWithValue("@2", idMenu)

                Using rd As OdbcDataReader = cmd.ExecuteReader()
                    If rd.Read() Then
                        akses("tambah") = (rd("tambah") = 1)
                        akses("ubah") = (rd("ubah") = 1)
                        akses("hapus") = (rd("hapus") = 1)
                        akses("adaAkses") = True
                    End If
                End Using
            End Using
        Catch ex As Exception
            MsgBox("Error cek akses: " & ex.Message)
        End Try

        Return akses
    End Function

    ' === Fungsi bantu untuk memblok tombol di setiap form sesuai hak akses ===
    Public Sub TerapkanAksesKeButton(ByVal form As Form, ByVal idMenu As String)
        If String.IsNullOrEmpty(KodeUserLogin) Then Exit Sub
        Dim akses = GetAkses(KodeUserLogin, idMenu)

        ' Cari tombol-tombol umum
        Dim btnTambah = TryCast(form.Controls("btnTAMBAH"), Button)
        Dim btnUbah = TryCast(form.Controls("btnUBAH"), Button)
        Dim btnHapus = TryCast(form.Controls("btnHAPUS"), Button)

        If btnTambah IsNot Nothing Then
            If akses("tambah") Then
                btnTambah.Enabled = True
            Else
                btnTambah.Enabled = False
                AddHandler btnTambah.Click, Sub() MsgBox("Anda tidak punya akses untuk menambah!", MsgBoxStyle.Exclamation)
            End If
        End If

        If btnUbah IsNot Nothing Then
            If akses("ubah") Then
                btnUbah.Enabled = True
            Else
                btnUbah.Enabled = False
                AddHandler btnUbah.Click, Sub() MsgBox("Anda tidak punya akses untuk mengubah!", MsgBoxStyle.Exclamation)
            End If
        End If

        If btnHapus IsNot Nothing Then
            If akses("hapus") Then
                btnHapus.Enabled = True
            Else
                btnHapus.Enabled = False
                AddHandler btnHapus.Click, Sub() MsgBox("Anda tidak punya akses untuk menghapus!", MsgBoxStyle.Exclamation)
            End If
        End If
    End Sub
End Module
