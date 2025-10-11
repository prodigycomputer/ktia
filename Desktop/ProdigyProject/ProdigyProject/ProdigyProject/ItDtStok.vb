Imports System.Data.Odbc

Public Class ItDtStok

    Public Sub LoadDataStok(ByVal keyword As String)

        Dim sql As String
        If keyword = "*" Then
            sql = "SELECT kodebrg, namabrg, kodemerk, kodegol, kodegrup, satuan1, satuan2, satuan3, isi1, isi2, hrgBeli, harga1, harga2, harga3, harga4, harga5, harga6, harga11, harga22, harga33, harga44, harga55, harga66, harga111, harga222, harga333, harga444, harga555, harga666 FROM zstok ORDER BY kodebrg"
        Else
            sql = "SELECT kodebrg, namabrg, kodemerk, kodegol, kodegrup, satuan1, satuan2, satuan3, isi1, isi2, hrgBeli, harga1, harga2, harga3, harga4, harga5, harga6, harga11, harga22, harga33, harga44, harga55, harga66, harga111, harga222, harga333, harga444, harga555, harga666 " &
                  "FROM zstok WHERE kodebrg LIKE '%" & keyword & "%' OR namabrg LIKE '%" & keyword & "%' ORDER BY kodebrg"
        End If

        dgitmSTOK.Rows.Clear()

        Try
            BukaKoneksi()
            Cmd = New OdbcCommand(sql, Conn)
            Rd = Cmd.ExecuteReader()

            While Rd.Read()
                dgitmSTOK.Rows.Add(Rd("kodebrg"), Rd("namabrg"), Rd("kodemerk"), Rd("kodegol"), Rd("kodegrup"),
                                   Rd("satuan1"), Rd("satuan2"), Rd("satuan3"), Rd("isi1"), Rd("isi2"),
                                   Rd("hrgbeli"), Rd("harga1"), Rd("harga2"), Rd("harga3"), Rd("harga4"), Rd("harga5"), Rd("harga6"),
                                   Rd("harga11"), Rd("harga22"), Rd("harga33"), Rd("harga44"), Rd("harga55"), Rd("harga66"),
                                   Rd("harga111"), Rd("harga222"), Rd("harga333"), Rd("harga444"), Rd("harga555"), Rd("harga666"))
            End While

            Rd.Close()
            Conn.Close()
        Catch ex As Exception
            MessageBox.Show("Gagal load data: " & ex.Message)
        End Try
    End Sub

    Private Sub ItDtStok_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.FormBorderStyle = FormBorderStyle.FixedSingle
        Me.MaximizeBox = False

        SetupGridStok(dgitmSTOK)
    End Sub

    Private Sub dgitmSTOK_CellDoubleClick(ByVal sender As Object, ByVal e As DataGridViewCellEventArgs) Handles dgitmSTOK.CellDoubleClick
        If e.RowIndex >= 0 Then
            Dim kode As String = dgitmSTOK.Rows(e.RowIndex).Cells("kodebrg").Value.ToString()
            Dim nama As String = dgitmSTOK.Rows(e.RowIndex).Cells("namabrg").Value.ToString()
            Dim sat1 As String = dgitmSTOK.Rows(e.RowIndex).Cells("satuan1").Value.ToString()
            Dim sat2 As String = dgitmSTOK.Rows(e.RowIndex).Cells("satuan2").Value.ToString()
            Dim sat3 As String = dgitmSTOK.Rows(e.RowIndex).Cells("satuan3").Value.ToString()
            Dim isi1 As Double = Val(dgitmSTOK.Rows(e.RowIndex).Cells("isi1").Value)
            Dim isi2 As Double = Val(dgitmSTOK.Rows(e.RowIndex).Cells("isi2").Value)
            Dim harga As Double = Val(dgitmSTOK.Rows(e.RowIndex).Cells("hrgbeli").Value)

            ' ==== deteksi parent form secara dinamis ====
            If TypeOf Me.Owner Is ItFPopup Then
                Dim parentForm As ItFPopup = CType(Me.Owner, ItFPopup)
                parentForm.SetBarang(kode, nama, sat1, sat2, sat3, isi1, isi2, harga)

            ElseIf TypeOf Me.Owner Is ItFPopupMut Then
                Dim parentForm As ItFPopupMut = CType(Me.Owner, ItFPopupMut)
                parentForm.SetBarang(kode, nama, sat1, sat2, sat3, isi1, isi2)

            ElseIf TypeOf Me.Owner Is ItFPopupPeny Then
                Dim parentForm As ItFPopupPeny = CType(Me.Owner, ItFPopupPeny)
                parentForm.SetBarang(kode, nama, sat1, sat2, sat3, isi1, isi2, harga)
            End If

            Me.Close()
        End If
    End Sub

End Class
