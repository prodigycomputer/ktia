Imports System.Data.Odbc

Public Class ItDtUser

    Public Sub LoadDataUser(Optional ByVal kodeuser As String = "",
                        Optional ByVal username As String = "")

        Dim sql As String = "SELECT kodeuser, username, kunci FROM zusers"
        Dim whereList As New List(Of String)

        ' --- Filter berdasarkan kodeuser
        If kodeuser <> "" Then
            whereList.Add("kodeuser LIKE ?")
        End If

        ' --- Filter berdasarkan username
        If username <> "" Then
            whereList.Add("username LIKE ?")
        End If

        If whereList.Count > 0 Then
            sql &= " WHERE " & String.Join(" AND ", whereList)
        End If

        sql &= " ORDER BY kodeuser"

        dgitmUSER.Rows.Clear()

        Try
            BukaKoneksi()
            Cmd = New OdbcCommand(sql, Conn)

            ' --- Bind parameter sesuai urutan filter
            If kodeuser <> "" Then
                Cmd.Parameters.AddWithValue("", "%" & kodeuser & "%")
            End If
            If username <> "" Then
                Cmd.Parameters.AddWithValue("", "%" & username & "%")
            End If

            Rd = Cmd.ExecuteReader()
             While Rd.Read()
                dgitmUSER.Rows.Add(
                    Rd("kodeuser").ToString(),
                    Rd("username").ToString()
                )
            End While
            Rd.Close()
            Conn.Close()
        Catch ex As Exception
            MessageBox.Show("Gagal load data: " & ex.Message)
        End Try
    End Sub

    Private Sub ItDtUser_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.FormBorderStyle = FormBorderStyle.FixedSingle
        Me.MaximizeBox = False

        SetupGridDUser(dgitmUSER)
    End Sub

    Private Sub dgitmUSER_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgitmUSER.CellContentClick
        If e.RowIndex >= 0 Then
            Dim data As String = dgitmUSER.Rows(e.RowIndex).Cells("kodeuser").Value.ToString()

            Dim parentForm As FSetAkun = TryCast(Me.Owner, FSetAkun)
            If parentForm IsNot Nothing Then
                parentForm.LoadData(data)
            End If

            Me.Close()
        End If
    End Sub
End Class