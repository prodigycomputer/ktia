Imports System.Data.Odbc

Public Class ItDtPenjualan

    Public Sub LoadDatajual(Optional ByVal nonota As String = "",
                        Optional ByVal tgl1 As String = "",
                        Optional ByVal tgl2 As String = "",
                        Optional ByVal namasup As String = "",
                        Optional ByVal status As String = "")

        Dim sql As String = "SELECT zjual.tgl, zjual.nonota, zsupplier.namasup, zjual.nilai, zjual.lunas " &
                            "FROM zjual " &
                            "LEFT JOIN zsupplier ON zjual.kodesup = zsupplier.kodesup"

        Dim whereList As New List(Of String)

        ' --- filter berdasarkan nonota
        If nonota <> "" Then
            whereList.Add("zjual.nonota LIKE ?")
        Else
            ' --- filter tanggal
            If tgl1 <> "" AndAlso tgl2 <> "" Then
                whereList.Add("zjual.tgl BETWEEN ? AND ?")
            End If

            ' --- filter supplier
            If namasup <> "" Then
                whereList.Add("zsupplier.namasup LIKE ?")
            End If

            ' --- filter status lunas
            If status = "lunas" Then
                whereList.Add("zjual.lunas = 1")
            ElseIf status = "belum" Then
                whereList.Add("zjual.lunas = 0")
            End If
        End If

        If whereList.Count > 0 Then
            sql &= " WHERE " & String.Join(" AND ", whereList)
        End If

        sql &= " ORDER BY zjual.tgl DESC, zjual.nonota DESC"

        dgitmPENJUALAN.Rows.Clear()

        Try
            BukaKoneksi()
            Cmd = New OdbcCommand(sql, Conn)

            ' --- bind parameter sesuai urutan where
            If nonota <> "" Then
                Cmd.Parameters.AddWithValue("", "%" & nonota & "%")
            Else
                If tgl1 <> "" AndAlso tgl2 <> "" Then
                    Cmd.Parameters.AddWithValue("", tgl1)
                    Cmd.Parameters.AddWithValue("", tgl2)
                End If
                If namasup <> "" Then
                    Cmd.Parameters.AddWithValue("", "%" & namasup & "%")
                End If
            End If

            Rd = Cmd.ExecuteReader()
            While Rd.Read()
                dgitmPENJUALAN.Rows.Add(
                    Rd("tgl"),
                    Rd("nonota"),
                    If(IsDBNull(Rd("namasup")), "-", Rd("namasup")),
                    Rd("nilai"),
                    Rd("lunas")
                )
            End While
            Rd.Close()
            Conn.Close()
        Catch ex As Exception
            MessageBox.Show("Gagal load data: " & ex.Message)
        End Try
    End Sub

    Private Sub ItDtPenjualan_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.FormBorderStyle = FormBorderStyle.FixedSingle
        Me.MaximizeBox = False

        SetupGridNOTA(dgitmPENJUALAN)
    End Sub

    Private Sub dgitmPENJUALAN_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgitmPENJUALAN.CellContentClick
        If e.RowIndex >= 0 Then
            Dim nonota As String = dgitmPENJUALAN.Rows(e.RowIndex).Cells("nonota").Value.ToString()

            Dim parentForm As FPenjualan = TryCast(Me.Owner, FPenjualan)
            If parentForm IsNot Nothing Then
                parentForm.LoadNota(nonota)
            End If

            Me.Close()
        End If
    End Sub
End Class