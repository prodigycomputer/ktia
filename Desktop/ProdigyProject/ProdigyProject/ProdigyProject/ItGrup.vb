Imports System.Data.Odbc

Public Class ItGrup
    Public Property Caller As String

    Public Sub LoadDataGrup(ByVal keyword As String, ByVal mode As String)
        Dim adaData As Boolean = False

        Dim sql As String = ""
        If mode = "KODE" AndAlso keyword = "*" Then
            sql = "SELECT kodegrup, namagrup FROM zgrup ORDER BY kodegrup"
        ElseIf mode = "NAMA" AndAlso keyword = "*" Then
            sql = "SELECT kodegrup, namagrup FROM zgrup ORDER BY kodegrup"
        ElseIf mode = "KODE" Then
            sql = "SELECT kodegrup, namagrup FROM zgrup " &
                  "WHERE kodegrup LIKE ? ORDER BY kodegrup LIMIT 50"
        ElseIf mode = "NAMA" Then
            sql = "SELECT kodegrup, namagrup FROM zgrup " &
                  "WHERE namagrup LIKE ? ORDER BY namagrup LIMIT 50"
        End If

        dgitmGRUP.Rows.Clear()

        Try
            BukaKoneksi()
            Cmd = New OdbcCommand(sql, Conn)

            If keyword <> "*" AndAlso keyword <> "" Then
                Cmd.Parameters.AddWithValue("@p1", keyword & "%")
            End If

            Rd = Cmd.ExecuteReader()

            While Rd.Read()
                adaData = True
                dgitmGRUP.Rows.Add(Rd("kodegrup"), Rd("namagrup"))
            End While

            If Not adaData Then
                MessageBox.Show("Data tidak ditemukan.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Dim parentForm As FStok = TryCast(Me.Owner, FStok)
                If parentForm IsNot Nothing Then

                    If Caller = "KODE" Then
                        parentForm.txtKDGRUP.Clear()
                        Me.Close()
                    ElseIf Caller = "NAMA" Then
                        parentForm.txtNMGRUP.Clear()
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

    Private Sub ItGrup_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.FormBorderStyle = FormBorderStyle.FixedSingle
        Me.MaximizeBox = False

        SetupGridDGrup(dgitmGRUP)
    End Sub

    Private Sub PilihData()
        If dgitmGRUP.CurrentRow Is Nothing Then Exit Sub

        Dim kode As String = dgitmGRUP.CurrentRow.Cells("kodegrup").Value.ToString()
        Dim nama As String = dgitmGRUP.CurrentRow.Cells("namagrup").Value.ToString()

        Dim parentForm As FStok = TryCast(Me.Owner, FStok)
        If parentForm IsNot Nothing Then
            parentForm.SetGrup(kode, nama)
        End If

        Me.Close()
    End Sub

    Private Sub dgitmGRUP_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgitmGRUP.CellClick
        PilihData()
    End Sub

    Private Sub dgitmGRUP_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dgitmGRUP.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            PilihData()
        End If
    End Sub
End Class