Imports System.Data.Odbc

Public Class ItFPopupPeny

    Public TargetGrid As DataGridView
    Public Property IsEditMode As Boolean = False

    Private FaktorIsi1 As Double = 1
    Private FaktorIsi2 As Double = 1

    Public Sub SetBarang(ByVal kode As String, ByVal nama As String,
                     ByVal sat1 As String, ByVal sat2 As String, ByVal sat3 As String,
                     ByVal isi1 As Double, ByVal isi2 As Double, ByVal harga As Double)

        tPopKDBARANG.Text = kode
        tPopNMBARANG.Text = nama
        tPopSTN1.Text = sat1
        tPopSTN2.Text = sat2
        tPopSTN3.Text = sat3
        tPopHARGA.Text = harga

        FaktorIsi1 = isi1
        FaktorIsi2 = isi2

        ' Default disable
        tPopJLH2.Enabled = False
        tPopJLH3.Enabled = False

        ' Atur sesuai isi1 & isi2
        If isi1 > 1 Then
            tPopJLH2.Enabled = True
        End If
        If isi2 > 1 Then
            tPopJLH3.Enabled = True
        End If
    End Sub

    ' === CEK STOK ===
    Private Sub CekStok()
        Try
            BukaKoneksi()

            Dim kodeBrg As String = tPopKDBARANG.Text.Trim()

            If kodeBrg = "" Then
                tPopSTOK.Text = ""
                Exit Sub
            End If

            Dim sql As String =
            "SELECT s.sisa1, s.sisa2, s.sisa3, z.satuan1, z.satuan2, z.satuan3 " &
            "FROM zsaldo s " &
            "LEFT JOIN zstok z ON s.kodebrg = z.kodebrg " &
            "WHERE s.kodebrg = ?"

            Using cmd As New OdbcCommand(sql, Conn)
                cmd.Parameters.AddWithValue("@1", kodeBrg)

                Using rd As OdbcDataReader = cmd.ExecuteReader()
                    If rd.Read() Then
                        Dim s1 As Decimal = If(IsDBNull(rd("sisa1")), 0, Convert.ToDecimal(rd("sisa1")))
                        Dim s2 As Decimal = If(IsDBNull(rd("sisa2")), 0, Convert.ToDecimal(rd("sisa2")))
                        Dim s3 As Decimal = If(IsDBNull(rd("sisa3")), 0, Convert.ToDecimal(rd("sisa3")))

                        Dim st1 As String = If(IsDBNull(rd("satuan1")), "", rd("satuan1").ToString())
                        Dim st2 As String = If(IsDBNull(rd("satuan2")), "", rd("satuan2").ToString())
                        Dim st3 As String = If(IsDBNull(rd("satuan3")), "", rd("satuan3").ToString())

                        If s1 = 0 AndAlso s2 = 0 AndAlso s3 = 0 Then
                            tPopSTOK.Text = "Stok Habis"
                        Else
                            ' Format tampilan stok per satuan
                            Dim parts As New List(Of String)
                            If s1 > 0 Then parts.Add(s1.ToString("N0") & " " & st1)
                            If s2 > 0 Then parts.Add(s2.ToString("N0") & " " & st2)
                            If s3 > 0 Then parts.Add(s3.ToString("N0") & " " & st3)

                            ' Jika semuanya nol tapi ingin tetap tampilkan nol juga:
                            If parts.Count = 0 Then
                                parts.Add(s1.ToString("N0") & " " & st1)
                                parts.Add(s2.ToString("N0") & " " & st2)
                                parts.Add(s3.ToString("N0") & " " & st3)
                            End If

                            tPopSTOK.Text = String.Join(", ", parts)
                        End If
                    Else
                        tPopSTOK.Text = "Stok tidak tersedia"
                    End If
                End Using
            End Using

        Catch ex As Exception
            MsgBox("Terjadi kesalahan saat cek stok: " & ex.Message)
        End Try
    End Sub

    Public Sub LoadBarangInfo(ByVal kodeBrg As String)

        If Conn Is Nothing OrElse Conn.State = ConnectionState.Closed Then
            BukaKoneksi()
        End If

        ' --- Barang ---
        Using cmd As New OdbcCommand("SELECT NamaBrg, Satuan1, Satuan2, Satuan3, Isi1, Isi2 FROM zstok WHERE KodeBrg = ?", Conn)
            cmd.Parameters.AddWithValue("?", kodeBrg)
            Using rd As OdbcDataReader = cmd.ExecuteReader()
                If rd.Read() Then
                    tPopNMBARANG.Text = rd("NamaBrg").ToString()
                    tPopSTN1.Text = rd("Satuan1").ToString()
                    tPopSTN2.Text = rd("Satuan2").ToString()
                    tPopSTN3.Text = rd("Satuan3").ToString()

                    FaktorIsi1 = If(IsDBNull(rd("Isi1")), 1, CDbl(rd("Isi1")))
                    FaktorIsi2 = If(IsDBNull(rd("Isi2")), 1, CDbl(rd("Isi2")))

                    tPopJLH2.Enabled = (FaktorIsi1 > 1)
                    tPopJLH3.Enabled = (FaktorIsi2 > 1)
                End If
            End Using
        End Using

    End Sub

    Private Sub ClearInput()
        tPopKDBARANG.Clear()
        tPopNMBARANG.Clear()
        tPopJLH1.Clear()
        tPopJLH2.Clear()
        tPopJLH3.Clear()
        tPopSTN1.Clear()
        tPopSTN2.Clear()
        tPopSTN3.Clear()
        tPopQTY.Clear()
        tPopHARGA.Clear()
        tPopSTOK.Clear()
        FaktorIsi1 = 1
        FaktorIsi2 = 1
        tPopJLH2.Enabled = (FaktorIsi1 > 1)
        tPopJLH3.Enabled = (FaktorIsi2 > 1)
    End Sub

    Private Sub ItFPopupPeny_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.FormBorderStyle = FormBorderStyle.FixedSingle
        Me.MaximizeBox = False

        BukaKoneksi()
    End Sub

    Private Sub btnPopTAMBAH_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPopTAMBAH.Click
        Try
            If TargetGrid Is Nothing Then
                MessageBox.Show("Grid belum dihubungkan!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If

            ' --- Ambil data ---
            Dim kode As String = tPopKDBARANG.Text.Trim()
            Dim nama As String = tPopNMBARANG.Text.Trim()

            Dim jlh1 As String = tPopJLH1.Text.Trim()
            Dim sat1 As String = tPopSTN1.Text.Trim()
            Dim jlh2 As String = tPopJLH2.Text.Trim()
            Dim sat2 As String = tPopSTN2.Text.Trim()
            Dim jlh3 As String = tPopJLH3.Text.Trim()
            Dim sat3 As String = tPopSTN3.Text.Trim()
            Dim qty As String = tPopQTY.Text.Trim()
            Dim harga As String = tPopHARGA.Text.Trim()

            ' --- Masukkan ke grid ---
            If IsEditMode AndAlso TargetGrid.CurrentRow IsNot Nothing Then
                With TargetGrid.CurrentRow
                    .Cells("KodeBrg").Value = kode
                    .Cells("NamaBrg").Value = nama
                    .Cells("Jlh1").Value = jlh1
                    .Cells("Sat1").Value = sat1
                    .Cells("Jlh2").Value = jlh2
                    .Cells("Sat2").Value = sat2
                    .Cells("Jlh3").Value = jlh3
                    .Cells("Sat3").Value = sat3
                    .Cells("Qty").Value = qty
                    .Cells("Harga").Value = harga

                End With
            Else
                TargetGrid.Rows.Add(kode, nama, jlh1, sat1, jlh2, sat2, jlh3, sat3, qty, harga)
            End If

            ' --- Reset input untuk tambah berikutnya ---
            IsEditMode = False
            ClearInput()
            tPopKDBARANG.Focus()

        Catch ex As Exception
            MessageBox.Show("Gagal menambah item: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnPopTUTUP_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPopTUTUP.Click
        Me.Close()
    End Sub

    ' === EVENT: SAAT KODE BARANG SELESAI DIISI ===
    Private Sub tPopKDBARANG_Leave(ByVal sender As Object, ByVal e As EventArgs) Handles tPopKDBARANG.Leave
        CekStok()
    End Sub

    Private Sub tPopKDBARANG_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles tPopKDBARANG.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True ' supaya bunyi beep Enter tidak muncul

            If tPopKDBARANG.Text.Trim() <> "" Then
                Dim f As New ItStok
                f.Owner = Me
                f.Show()
                f.LoadDataStok(tPopKDBARANG.Text.Trim())
            End If
        End If
    End Sub

    Private Sub tPopNMBARANG_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles tPopNMBARANG.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True

            If tPopNMBARANG.Text.Trim() <> "" Then
                Dim f As New ItStok
                f.Owner = Me
                f.Show()
                f.LoadDataStok(tPopNMBARANG.Text.Trim())
            End If
        End If
    End Sub

    ' === KeyPress hanya angka ===
    Private Sub OnlyNumber_KeyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs) _
        Handles tPopJLH1.KeyPress, tPopJLH2.KeyPress, tPopJLH3.KeyPress, tPopQTY.KeyPress, tPopHARGA.KeyPress

        AngkaHelper.HanyaAngka(e)
    End Sub
End Class