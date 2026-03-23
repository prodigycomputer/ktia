Imports System.Data.Odbc

Public Class ItGolongan
    Public Property Caller As String

    Public Sub LoadDataGolongan(ByVal keyword As String, ByVal mode As String)
        Dim adaData As Boolean = False

        Dim sql As String = ""
        If mode = "KODE" AndAlso keyword = "*" Then
            sql = "SELECT kodegol, namagol FROM zgolongan ORDER BY kodegol"
        ElseIf mode = "NAMA" AndAlso keyword = "*" Then
            sql = "SELECT kodegol, namagol FROM zgolongan ORDER BY kodegol"
        ElseIf mode = "KODE" Then
            sql = "SELECT kodegol, namagol FROM zgolongan " &
                  "WHERE kodegol LIKE ? ORDER BY kodegol LIMIT 50"
        ElseIf mode = "NAMA" Then
            sql = "SELECT kodegol, namagol FROM zgolongan " &
                  "WHERE namagol LIKE ? ORDER BY namagol LIMIT 50"
        End If

        dgitmGOL.Rows.Clear()

        Try
            BukaKoneksi()
            Cmd = New OdbcCommand(sql, Conn)

            If keyword <> "*" AndAlso keyword <> "" Then
                Cmd.Parameters.AddWithValue("@p1", keyword & "%")
            End If

            Rd = Cmd.ExecuteReader()

            While Rd.Read()
                adaData = True
                dgitmGOL.Rows.Add(Rd("kodegol"), Rd("namagol"))
            End While

            If Not adaData Then
                MessageBox.Show("Data tidak ditemukan.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Dim parentForm As FStok = TryCast(Me.Owner, FStok)
                If parentForm IsNot Nothing Then

                    If Caller = "KODE" Then
                        parentForm.txtKDGOL.Clear()
                        Me.Close()
                    ElseIf Caller = "NAMA" Then
                        parentForm.txtNMGOL.Clear()
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

    Private Sub ItGolongan_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.FormBorderStyle = FormBorderStyle.FixedSingle
        Me.MaximizeBox = False

        SetupGridDGolongan(dgitmGOL)
    End Sub

    Private Sub PilihData()
        If dgitmGOL.CurrentRow Is Nothing Then Exit Sub

        Dim kode As String = dgitmGOL.CurrentRow.Cells("kodegol").Value.ToString()
        Dim nama As String = dgitmGOL.CurrentRow.Cells("namagol").Value.ToString()

        Dim parentForm As FStok = TryCast(Me.Owner, FStok)
        If parentForm IsNot Nothing Then
            parentForm.SetGolongan(kode, nama)
        End If

        Me.Close()
    End Sub

    Private Sub dgitmGOL_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgitmGOL.CellClick
        PilihData()
    End Sub

    Private Sub dgitmGOL_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dgitmGOL.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            PilihData()
        End If
    End Sub
End Class