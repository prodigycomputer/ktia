Imports System.Data.Odbc

Public Class ItArea
    Public Property Caller As String

    Public Sub LoadDataArea(ByVal keyword As String, ByVal mode As String)
        Dim adaData As Boolean = False

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
                adaData = True
                dgitmAREA.Rows.Add(Rd("kodear"), Rd("namaar"))
            End While

            If Not adaData Then
                MessageBox.Show("Data tidak ditemukan.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Dim parentForm As FKustomer = TryCast(Me.Owner, FKustomer)
                If parentForm IsNot Nothing Then

                    If Caller = "KODE" Then
                        parentForm.txtKDAREA.Clear()
                        Me.Close()
                    ElseIf Caller = "NAMA" Then
                        parentForm.txtNMAREA.Clear()
                        Me.Close()
                    End If

                End If
            End If

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

    Private Sub PilihData()
        If dgitmAREA.CurrentRow Is Nothing Then Exit Sub

        Dim kode As String = dgitmAREA.CurrentRow.Cells("kodear").Value.ToString()
        Dim nama As String = dgitmAREA.CurrentRow.Cells("namaar").Value.ToString()

        Dim parentForm As FKustomer = TryCast(Me.Owner, FKustomer)
        If parentForm IsNot Nothing Then
            parentForm.SetArea(kode, nama)
        End If

        Me.Close()
    End Sub

    Private Sub dgitmAREA_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgitmAREA.CellClick
        PilihData()
    End Sub

    Private Sub dgitmAREA_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dgitmAREA.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            PilihData()
        End If
    End Sub
End Class