Imports System.Data.Odbc

Public Class ItMerek
    Public Property Caller As String

    Public Sub LoadDataMerek(ByVal keyword As String, ByVal mode As String)
        Dim adaData As Boolean = False

        Dim sql As String = ""
        If mode = "KODE" AndAlso keyword = "*" Then
            sql = "SELECT kodemerk, namamerk FROM zmerek ORDER BY kodemerk"
        ElseIf mode = "NAMA" AndAlso keyword = "*" Then
            sql = "SELECT kodemerk, namamerk FROM zmerek ORDER BY kodemerk"
        ElseIf mode = "KODE" Then
            sql = "SELECT kodemerk, namamerk FROM zmerek " &
                  "WHERE kodemerk LIKE ? ORDER BY kodemerk LIMIT 50"
        ElseIf mode = "NAMA" Then
            sql = "SELECT kodemerk, namamerk FROM zmerek " &
                  "WHERE namamerk LIKE ? ORDER BY namamerk LIMIT 50"
        End If

        dgitmMEREK.Rows.Clear()

        Try
            BukaKoneksi()
            Cmd = New OdbcCommand(sql, Conn)

            If keyword <> "*" AndAlso keyword <> "" Then
                Cmd.Parameters.AddWithValue("@p1", keyword & "%")
            End If

            Rd = Cmd.ExecuteReader()

            While Rd.Read()
                adaData = True
                dgitmMEREK.Rows.Add(Rd("kodemerk"), Rd("namamerk"))
            End While

            If Not adaData Then
                MessageBox.Show("Data tidak ditemukan.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Dim parentForm As FStok = TryCast(Me.Owner, FStok)
                If parentForm IsNot Nothing Then

                    If Caller = "KODE" Then
                        parentForm.txtKDMERK.Clear()
                        Me.Close()
                    ElseIf Caller = "NAMA" Then
                        parentForm.txtNMMERK.Clear()
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

    Private Sub ItMerek_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.FormBorderStyle = FormBorderStyle.FixedSingle
        Me.MaximizeBox = False

        SetupGridDMerek(dgitmMEREK)
    End Sub

    Private Sub PilihData()
        If dgitmMEREK.CurrentRow Is Nothing Then Exit Sub

        Dim kode As String = dgitmMEREK.CurrentRow.Cells("kodemerk").Value.ToString()
        Dim nama As String = dgitmMEREK.CurrentRow.Cells("namamerk").Value.ToString()

        Dim parentForm As FStok = TryCast(Me.Owner, FStok)
        If parentForm IsNot Nothing Then
            parentForm.SetMerek(kode, nama)
        End If

        Me.Close()
    End Sub

    Private Sub dgitmMEREK_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgitmMEREK.CellClick
        PilihData()
    End Sub

    Private Sub dgitmMEREK_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dgitmMEREK.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            PilihData()
        End If
    End Sub
End Class