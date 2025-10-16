Imports System.Data.Odbc

Public Class ItDtRPenjualan

    Public Sub LoadDataRJual(Optional ByVal nonota As String = "",
                        Optional ByVal tgl1 As String = "",
                        Optional ByVal tgl2 As String = "",
                        Optional ByVal namakust As String = "",
                        Optional ByVal namasls As String = "",
                        Optional ByVal status As String = "")

        Dim sql As String = "SELECT zrjual.tgl, zrjual.nonota, zkustomer.namakust, zsales.namasls, zrjual.nilai, zrjual.lunas " &
                            "FROM zrjual " &
                            "LEFT JOIN zkustomer ON zrjual.kodekust = zkustomer.kodekust " &
                            "LEFT JOIN zsales ON zrjual.kodesls = zsales.kodesls"

        Dim whereList As New List(Of String)

        ' --- filter berdasarkan nonota
        If nonota <> "" Then
            whereList.Add("zrjual.nonota LIKE ?")
        Else
            ' --- filter tanggal
            If tgl1 <> "" AndAlso tgl2 <> "" Then
                whereList.Add("zrjual.tgl BETWEEN ? AND ?")
            End If

            ' --- filter supplier
            If namakust <> "" Then
                whereList.Add("zkustomer.namakust LIKE ?")
            End If

            If namasls <> "" Then
                whereList.Add("zsales.namasls LIKE ?")
            End If

            ' --- filter status lunas
            If status = "lunas" Then
                whereList.Add("zrjual.lunas = 1")
            ElseIf status = "belum" Then
                whereList.Add("zrjual.lunas = 0")
            End If
        End If

        If whereList.Count > 0 Then
            sql &= " WHERE " & String.Join(" AND ", whereList)
        End If

        sql &= " ORDER BY zrjual.tgl DESC, zrjual.nonota DESC"

        dgitmRPENJUALAN.Rows.Clear()

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
                If namakust <> "" Then
                    Cmd.Parameters.AddWithValue("", "%" & namakust & "%")
                End If
                If namasls <> "" Then
                    Cmd.Parameters.AddWithValue("", "%" & namasls & "%")
                End If
            End If

            Rd = Cmd.ExecuteReader()
            While Rd.Read()
                dgitmRPENJUALAN.Rows.Add(
                    Rd("tgl"),
                    Rd("nonota"),
                    If(IsDBNull(Rd("namakust")), "-", Rd("namakust")),
                    If(IsDBNull(Rd("namasls")), "-", Rd("namasls")),
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

    Private Sub ItDtRPenjualan_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.FormBorderStyle = FormBorderStyle.FixedSingle
        Me.MaximizeBox = False

        SetupGridJual(dgitmRPENJUALAN)
    End Sub

    Private Sub dgitmRPENJUALAN_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgitmRPENJUALAN.CellContentClick
        If e.RowIndex >= 0 Then
            Dim nonota As String = dgitmRPENJUALAN.Rows(e.RowIndex).Cells("nonota").Value.ToString()

            Dim parentForm As FReturPenjualan = TryCast(Me.Owner, FReturPenjualan)
            If parentForm IsNot Nothing Then
                parentForm.LoadNota(nonota)
            End If

            Me.Close()
        End If
    End Sub
End Class