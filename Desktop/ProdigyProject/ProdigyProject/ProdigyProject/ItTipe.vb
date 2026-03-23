Imports System.Data.Odbc

Public Class ItTipe
    Public Property Caller As String

    Public Sub LoadDataTipe(ByVal keyword As String, ByVal mode As String)
        Dim adaData As Boolean = False

        Dim sql As String = ""
        If mode = "KODE" AndAlso keyword = "*" Then
            sql = "SELECT kodetipe, namatipe FROM ztipe ORDER BY kodetipe"
        ElseIf mode = "NAMA" AndAlso keyword = "*" Then
            sql = "SELECT kodetipe, namatipe FROM ztipe ORDER BY kodetipe"
        ElseIf mode = "KODE" Then
            sql = "SELECT kodetipe, namatipe FROM ztipe " &
                  "WHERE kodetipe LIKE ? ORDER BY kodetipe LIMIT 50"
        ElseIf mode = "NAMA" Then
            sql = "SELECT kodetipe, namatipe FROM ztipe " &
                  "WHERE namatipe LIKE ? ORDER BY namatipe LIMIT 50"
        End If

        dgitmTIPE.Rows.Clear()

        Try
            BukaKoneksi()
            Cmd = New OdbcCommand(sql, Conn)

            If keyword <> "*" AndAlso keyword <> "" Then
                Cmd.Parameters.AddWithValue("@p1", keyword & "%")
            End If

            Rd = Cmd.ExecuteReader()

            While Rd.Read()
                adaData = True
                dgitmTIPE.Rows.Add(Rd("kodetipe"), Rd("namatipe"))
            End While

            If Not adaData Then
                MessageBox.Show("Data tidak ditemukan.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Dim parentForm As FKustomer = TryCast(Me.Owner, FKustomer)
                If parentForm IsNot Nothing Then

                    If Caller = "KODE" Then
                        parentForm.txtKDTIPE.Clear()
                        Me.Close()
                    ElseIf Caller = "NAMA" Then
                        parentForm.txtNMTIPE.Clear()
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

    Private Sub ItTipe_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.FormBorderStyle = FormBorderStyle.FixedSingle
        Me.MaximizeBox = False

        SetupGridTipe(dgitmTIPE)
    End Sub

    Private Sub PilihData()
        If dgitmTIPE.CurrentRow Is Nothing Then Exit Sub

        Dim kode As String = dgitmTIPE.CurrentRow.Cells("kodetipe").Value.ToString()
        Dim nama As String = dgitmTIPE.CurrentRow.Cells("namatipe").Value.ToString()

        Dim parentForm As FKustomer = TryCast(Me.Owner, FKustomer)
        If parentForm IsNot Nothing Then
            parentForm.SetTipe(kode, nama)
        End If

        Me.Close()
    End Sub

    Private Sub dgitmTIPE_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgitmTIPE.CellContentClick
        PilihData()
    End Sub

    Private Sub dgitmTIPE_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dgitmTIPE.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            PilihData()
        End If
    End Sub
End Class