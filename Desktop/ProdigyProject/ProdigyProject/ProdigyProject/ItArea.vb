Imports System.Data.Odbc

Public Class ItArea

    Public Sub LoadDataArea(ByVal keyword As String, ByVal mode As String)

        Dim sql As String = ""
        If mode = "KODE" AndAlso keyword = "*" Then
            sql = "SELECT kodear, namaar FROM zarea ORDER BY kodear"
        ElseIf mode = "NAMA" AndAlso keyword = "*" Then
            sql = "SELECT kodear, namaar FROM zarea ORDER BY kodear"
        ElseIf mode = "KODE" Then
            sql = "SELECT kodear, namaar FROM zarea " &
                  "WHERE kodear LIKE ? ORDER BY kodear LIMIT 50"
        ElseIf mode = "NAMA" Then
            sql = "SELECT kodear, namaar FROM zarea " &
                  "WHERE namaar LIKE ? ORDER BY namaar LIMIT 50"
        End If

        dgitmAREA.Rows.Clear()

        Try
            BukaKoneksi()
            Cmd = New OdbcCommand(sql, Conn)

            If keyword <> "*" AndAlso keyword <> "" Then
                Cmd.Parameters.AddWithValue("@p1", keyword & "%")
            End If

            Rd = Cmd.ExecuteReader()

            While Rd.Read()
                dgitmAREA.Rows.Add(Rd("kodear"), Rd("namaar"))
            End While

            Rd.Close()
            Conn.Close()
        Catch ex As Exception
            MessageBox.Show("Gagal load data: " & ex.Message)
        End Try
    End Sub

    Private Sub ItArea_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.FormBorderStyle = FormBorderStyle.FixedSingle
        Me.MaximizeBox = False

        SetupGridArea(dgitmAREA)
    End Sub

    Private Sub dgitmAREA_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgitmAREA.CellContentClick
        If e.RowIndex >= 0 Then
            Dim kodea As String = dgitmAREA.Rows(e.RowIndex).Cells("kodear").Value.ToString()
            Dim namaa As String = dgitmAREA.Rows(e.RowIndex).Cells("namaar").Value.ToString()

            ' kirim ke ItFPopupPem
            Dim parentForm As FKustomer = TryCast(Me.Owner, FKustomer)
            If parentForm IsNot Nothing Then
                parentForm.SetArea(kodea, namaa)
            End If

            Me.Close()
        End If
    End Sub
End Class