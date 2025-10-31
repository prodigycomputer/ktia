Imports System.Data.Odbc

Public Class ItDtKolektor

    Public Sub LoadDataKolektor(Optional ByVal kodekol As String = "",
                        Optional ByVal namakol As String = "")

        Dim sql As String = "SELECT kodekol, namakol, alamat, kota, ktp, npwp FROM zkolektor"
        Dim whereList As New List(Of String)

        ' --- Filter berdasarkan kodeuser
        If kodekol <> "" Then
            whereList.Add("kodekol LIKE ?")
        End If

        ' --- Filter berdasarkan username
        If namakol <> "" Then
            whereList.Add("namakol LIKE ?")
        End If

        If whereList.Count > 0 Then
            sql &= " WHERE " & String.Join(" AND ", whereList)
        End If

        sql &= " ORDER BY kodekol"

        dgitmKOL.Rows.Clear()

        Try
            BukaKoneksi()
            Cmd = New OdbcCommand(sql, Conn)

            ' --- Bind parameter sesuai urutan filter
            If kodekol <> "" Then
                Cmd.Parameters.AddWithValue("", "%" & kodekol & "%")
            End If
            If namakol <> "" Then
                Cmd.Parameters.AddWithValue("", "%" & namakol & "%")
            End If

            Rd = Cmd.ExecuteReader()
            While Rd.Read()
                dgitmKOL.Rows.Add(
                    Rd("kodekol").ToString(),
                    Rd("namakol").ToString(),
                    Rd("alamat").ToString(),
                    Rd("kota").ToString(),
                    Rd("ktp").ToString(),
                    Rd("npwp").ToString()
                )
            End While
            Rd.Close()
            Conn.Close()
        Catch ex As Exception
            MessageBox.Show("Gagal load data: " & ex.Message)
        End Try
    End Sub

    Private Sub ItDtKolektor_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.FormBorderStyle = FormBorderStyle.FixedSingle
        Me.MaximizeBox = False

        SetupGridKolektor(dgitmKOL)
    End Sub

    Private Sub dgitmKOL_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgitmKOL.CellContentClick
        If e.RowIndex >= 0 Then
            Dim data As String = dgitmKOL.Rows(e.RowIndex).Cells("kodekol").Value.ToString()

            Dim parentForm As FKolektor = TryCast(Me.Owner, FKolektor)
            If parentForm IsNot Nothing Then
                parentForm.LoadData(data)
            End If

            Me.Close()
        End If
    End Sub
End Class