Imports System.Data.Odbc

Public Class ItDtPenyesuaian

    Public Sub LoadDataPenyesuaian(Optional ByVal nonota As String = "",
                                   Optional ByVal tgl As String = "",
                                   Optional ByVal namagd As String = "")

        Dim sql As String =
            "SELECT zpenyesuaian.tgl, zpenyesuaian.nonota, zgudang.namagd " &
            "FROM zpenyesuaian " &
            "LEFT JOIN zgudang ON zpenyesuaian.kodegd = zgudang.kodegd"

        Dim whereList As New List(Of String)

        ' --- filter berdasarkan nonota
        If nonota <> "" Then
            whereList.Add("zpenyesuaian.nonota LIKE ?")
        Else
            ' --- filter tanggal
            If tgl <> "" Then
                whereList.Add("zpenyesuaian.tgl = ?")
            End If

            ' --- filter gudang
            If namagd <> "" Then
                whereList.Add("zgudang.namagd LIKE ?")
            End If
        End If

        ' --- tambahkan WHERE jika ada kondisi
        If whereList.Count > 0 Then
            sql &= " WHERE " & String.Join(" AND ", whereList)
        End If

        sql &= " ORDER BY zpenyesuaian.tgl DESC, zpenyesuaian.nonota DESC"

        ' --- bersihkan isi grid
        dgitmPENYESUAIAN.Rows.Clear()

        Try
            BukaKoneksi()
            Cmd = New OdbcCommand(sql, Conn)

            ' --- bind parameter sesuai urutan kondisi
            If nonota <> "" Then
                Cmd.Parameters.AddWithValue("", "%" & nonota & "%")
            Else
                If tgl <> "" Then
                    Cmd.Parameters.AddWithValue("", tgl)
                End If
                If namagd <> "" Then
                    Cmd.Parameters.AddWithValue("", "%" & namagd & "%")
                End If
            End If

            ' --- eksekusi dan tampilkan data
            Rd = Cmd.ExecuteReader()
            While Rd.Read()
                dgitmPENYESUAIAN.Rows.Add(
                    Format(CDate(Rd("tgl")), "dd-MM-yyyy"),
                    Rd("nonota").ToString(),
                    If(IsDBNull(Rd("namagd")), "-", Rd("namagd").ToString())
                )
            End While

            Rd.Close()
            Conn.Close()

        Catch ex As Exception
            MessageBox.Show("Gagal load data: " & ex.Message)
        End Try
    End Sub

    Private Sub ItDtPenyesuaian_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.FormBorderStyle = FormBorderStyle.FixedSingle
        Me.MaximizeBox = False

        SetupGridDPenyesuaian(dgitmPENYESUAIAN)
    End Sub

    Private Sub dgitmPENYESUAIAN_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgitmPENYESUAIAN.CellContentClick
        If e.RowIndex >= 0 Then
            Dim nonota As String = dgitmPENYESUAIAN.Rows(e.RowIndex).Cells("nonota").Value.ToString()

            Dim parentForm As FPenyesuaian = TryCast(Me.Owner, FPenyesuaian)
            If parentForm IsNot Nothing Then
                parentForm.LoadNota(nonota)
            End If

            Me.Close()
        End If
    End Sub
End Class
