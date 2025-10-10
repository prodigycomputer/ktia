Imports System.Data.Odbc

Public Class ItDtMutasi

    Public Sub LoadDataMutasi(Optional ByVal nonota As String = "",
                          Optional ByVal tgl As String = "",
                          Optional ByVal namagd1 As String = "",
                          Optional ByVal namagd2 As String = "")

        Dim sql As String =
            "SELECT zmutasi.tgl, zmutasi.nonota, g1.namagd AS namagd1, g2.namagd AS namagd2 " &
            "FROM zmutasi " &
            "JOIN zgudang g1 ON zmutasi.kodegd1 = g1.kodegd " &
            "JOIN zgudang g2 ON zmutasi.kodegd2 = g2.kodegd"

        Dim whereList As New List(Of String)

        ' --- filter berdasarkan nonota
        If nonota <> "" Then
            whereList.Add("zmutasi.nonota LIKE ?")
        Else
            ' --- filter tanggal
            If tgl <> "" Then
                whereList.Add("zmutasi.tgl = ?")
            End If

            ' --- filter gudang asal
            If namagd1 <> "" Then
                whereList.Add("g1.namagd LIKE ?")
            End If

            ' --- filter gudang tujuan
            If namagd2 <> "" Then
                whereList.Add("g2.namagd LIKE ?")
            End If
        End If

        ' --- tambahkan WHERE jika ada kondisi
        If whereList.Count > 0 Then
            sql &= " WHERE " & String.Join(" AND ", whereList)
        End If

        sql &= " ORDER BY zmutasi.tgl DESC, zmutasi.nonota DESC"

        ' --- bersihkan isi grid
        dgitmMUTASI.Rows.Clear()

        Try
            BukaKoneksi()
            Cmd = New OdbcCommand(sql, Conn)

            ' --- bind parameter sesuai urutan where
            If nonota <> "" Then
                Cmd.Parameters.AddWithValue("", "%" & nonota & "%")
            Else
                If tgl <> "" Then
                    Cmd.Parameters.AddWithValue("", tgl)
                End If
                If namagd1 <> "" Then
                    Cmd.Parameters.AddWithValue("", "%" & namagd1 & "%")
                End If
                If namagd2 <> "" Then
                    Cmd.Parameters.AddWithValue("", "%" & namagd2 & "%")
                End If
            End If

            ' --- eksekusi dan tampilkan data
            Rd = Cmd.ExecuteReader()
            While Rd.Read()
                dgitmMUTASI.Rows.Add(
                    Rd("tgl"),
                    Rd("nonota"),
                    If(IsDBNull(Rd("namagd1")), "-", Rd("namagd1")),
                    If(IsDBNull(Rd("namagd2")), "-", Rd("namagd2"))
                )
            End While

            Rd.Close()
            Conn.Close()

        Catch ex As Exception
            MessageBox.Show("Gagal load data: " & ex.Message)
        End Try
    End Sub


    Private Sub ItDtMutasi_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.FormBorderStyle = FormBorderStyle.FixedSingle
        Me.MaximizeBox = False

        SetupGridDMutasi(dgitmMUTASI)
    End Sub

    Private Sub dgitmMUTASI_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgitmMUTASI.CellContentClick
        If e.RowIndex >= 0 Then
            Dim nonota As String = dgitmMUTASI.Rows(e.RowIndex).Cells("nonota").Value.ToString()

            Dim parentForm As FMutasi = TryCast(Me.Owner, FMutasi)
            If parentForm IsNot Nothing Then
                parentForm.LoadNota(nonota)
            End If

            Me.Close()
        End If
    End Sub
End Class