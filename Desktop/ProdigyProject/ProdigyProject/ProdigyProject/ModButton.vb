Module ModButton

    Public Sub SetButtonState(ByVal frm As Form, ByVal tambahAktif As Boolean)
        Dim buttons As New Dictionary(Of String, Button) From {
            {"TAMBAH", TryCast(frm.Controls("btnTAMBAH"), Button)},
            {"UBAH", TryCast(frm.Controls("btnUBAH"), Button)},
            {"HAPUS", TryCast(frm.Controls("btnHAPUS"), Button)},
            {"SIMPAN", TryCast(frm.Controls("btnSIMPAN"), Button)},
            {"BATAL", TryCast(frm.Controls("btnBATAL"), Button)},
            {"CARI", TryCast(frm.Controls("btnCARI"), Button)},
            {"PRINT", TryCast(frm.Controls("btnPRINT"), Button)}
        }

        ' cek semua tombol ada
        If buttons.Values.Any(Function(b) b Is Nothing) Then
            MessageBox.Show("Ada tombol yang tidak ditemukan di form " & frm.Name,
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        ' set kondisi btnTAMBAH
        buttons("TAMBAH").Enabled = tambahAktif
        buttons("UBAH").Enabled = tambahAktif
        buttons("HAPUS").Enabled = tambahAktif
        buttons("CARI").Enabled = tambahAktif
        buttons("PRINT").Enabled = tambahAktif

        ' tombol lain kebalikannya
        For Each kvp In buttons
            If kvp.Key <> "TAMBAH" AndAlso kvp.Key <> "UBAH" AndAlso kvp.Key <> "HAPUS" AndAlso kvp.Key <> "CARI" AndAlso kvp.Key <> "PRINT" Then
                kvp.Value.Enabled = Not tambahAktif
            End If
        Next
    End Sub
End Module
