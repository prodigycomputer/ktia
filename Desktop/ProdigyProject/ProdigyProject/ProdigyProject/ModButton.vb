Module ModButton

    Public Sub SetButtonState(ByVal frm As Form, ByVal tambahAktif As Boolean)
        ' Daftar nama tombol yang mungkin ada di form
        Dim buttonNames As String() = {"btnTAMBAH", "btnUBAH", "btnHAPUS",
                                       "btnSIMPAN", "btnBATAL", "btnCARI", "btnAKSES"}

        ' Simpan tombol yang ditemukan saja
        Dim buttons As New Dictionary(Of String, Button)
        For Each name In buttonNames
            Dim btn As Button = TryCast(frm.Controls.Find(name, True).FirstOrDefault(), Button)
            If btn IsNot Nothing Then
                buttons.Add(name.Replace("btn", "").ToUpper(), btn)
            End If
        Next

        ' Tidak perlu error kalau ada yang tidak ditemukan — fungsi tetap jalan

        ' Atur tombol utama (mode normal)
        For Each key In {"TAMBAH", "UBAH", "HAPUS", "CARI"}
            If buttons.ContainsKey(key) Then
                buttons(key).Enabled = tambahAktif
            End If
        Next

        ' Atur tombol input (mode tambah/ubah)
        For Each key In {"SIMPAN", "BATAL", "AKSES"}
            If buttons.ContainsKey(key) Then
                buttons(key).Enabled = Not tambahAktif
            End If
        Next
    End Sub

End Module
