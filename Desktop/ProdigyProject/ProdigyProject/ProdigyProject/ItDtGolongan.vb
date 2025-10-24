Imports System.Data.Odbc

Public Class ItDtGolongan

    Public Sub LoadDataGolongan(Optional ByVal kodeGO As String = "",
                        Optional ByVal namaGO As String = "")

        Dim sql As String = "SELECT kodegol, namagol FROM zgolongan"
        Dim whereList As New List(Of String)

        ' --- Filter berdasarkan kodeuser
        If kodeGO <> "" Then
            whereList.Add("kodegol LIKE ?")
        End If

        ' --- Filter berdasarkan username
        If namaGO <> "" Then
            whereList.Add("namagol LIKE ?")
        End If

        If whereList.Count > 0 Then
            sql &= " WHERE " & String.Join(" AND ", whereList)
        End If

        sql &= " ORDER BY kodegol"

        dgitmGO.Rows.Clear()

        Try
            BukaKoneksi()
            Cmd = New OdbcCommand(sql, Conn)

            ' --- Bind parameter sesuai urutan filter
            If kodeGO <> "" Then
                Cmd.Parameters.AddWithValue("", "%" & kodeGO & "%")
            End If
            If namaGO <> "" Then
                Cmd.Parameters.AddWithValue("", "%" & namaGO & "%")
            End If

            Rd = Cmd.ExecuteReader()
            While Rd.Read()
                dgitmGO.Rows.Add(
                    Rd("kodegol").ToString(),
                    Rd("namagol").ToString()
                )
            End While
            Rd.Close()
            Conn.Close()
        Catch ex As Exception
            MessageBox.Show("Gagal load data: " & ex.Message)
        End Try
    End Sub

    Private Sub ItDtGolongan_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.FormBorderStyle = FormBorderStyle.FixedSingle
        Me.MaximizeBox = False

        SetupGridDGolongan(dgitmGO)
    End Sub

    Private Sub dgitmGO_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgitmGO.CellContentClick
        If e.RowIndex >= 0 Then
            Dim data As String = dgitmGO.Rows(e.RowIndex).Cells("kodegol").Value.ToString()

            Dim parentForm As FGolongan = TryCast(Me.Owner, FGolongan)
            If parentForm IsNot Nothing Then
                parentForm.LoadData(data)
            End If

            Me.Close()
        End If
    End Sub
End Class