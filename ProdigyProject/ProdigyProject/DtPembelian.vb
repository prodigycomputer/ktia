Imports System.Data.Odbc

Public Class DtPembelian

    Public Sub LoadDataBeli(Optional ByVal nonota As String = "",
                        Optional ByVal tgl1 As String = "",
                        Optional ByVal tgl2 As String = "",
                        Optional ByVal namasup As String = "",
                        Optional ByVal status As String = "")

        Dim sql As String = "SELECT zbeli.tgl, zbeli.nonota, zsupplier.namasup, zbeli.nilai, zbeli.lunas " &
                            "FROM zbeli " &
                            "JOIN zsupplier ON zbeli.kodesup = zsupplier.kodesup"

        Dim whereList As New List(Of String)

        ' --- filter berdasarkan nonota
        If nonota <> "" Then
            whereList.Add("zbeli.nonota LIKE ?")
        Else
            ' --- filter tanggal
            If tgl1 <> "" AndAlso tgl2 <> "" Then
                whereList.Add("zbeli.tgl BETWEEN ? AND ?")
            End If

            ' --- filter supplier
            If namasup <> "" Then
                whereList.Add("zsupplier.namasup LIKE ?")
            End If

            ' --- filter status lunas
            If status = "lunas" Then
                whereList.Add("zbeli.lunas = 1")
            ElseIf status = "belum" Then
                whereList.Add("zbeli.lunas = 0")
            End If
        End If

        If whereList.Count > 0 Then
            sql &= " WHERE " & String.Join(" AND ", whereList)
        End If

        sql &= " ORDER BY zbeli.tgl DESC, zbeli.nonota DESC"

        dgitmPEMBELIAN.Rows.Clear()

        Try
            BukaKoneksi()
            Cmd = New OdbcCommand(sql, Conn)

            ' --- bind parameter sesuai urutan where
            Dim idx As Integer = 0
            If nonota <> "" Then
                Cmd.Parameters.AddWithValue("@" & idx, "%" & nonota & "%") : idx += 1
            Else
                If tgl1 <> "" AndAlso tgl2 <> "" Then
                    Cmd.Parameters.AddWithValue("@" & idx, tgl1) : idx += 1
                    Cmd.Parameters.AddWithValue("@" & idx, tgl2) : idx += 1
                End If

                If namasup <> "" Then
                    Cmd.Parameters.AddWithValue("@" & idx, "%" & namasup & "%") : idx += 1
                End If
            End If

            Rd = Cmd.ExecuteReader()
            While Rd.Read()
                dgitmPEMBELIAN.Rows.Add(
                    Rd("tgl"),
                    Rd("nonota"),
                    Rd("namasup"),
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

    Private Sub DtPembelian_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.FormBorderStyle = FormBorderStyle.FixedSingle
        Me.MaximizeBox = False

        SetupGridBeli(dgitmPEMBELIAN)
    End Sub
End Class