﻿Partial Class NotaDt

    Partial Class NotaPenjualanDataTable

        Private Sub NotaPenjualanDataTable_ColumnChanging(ByVal sender As System.Object, ByVal e As System.Data.DataColumnChangeEventArgs) Handles Me.ColumnChanging
            If (e.Column.ColumnName = Me.kodekustColumn.ColumnName) Then
                'Add user code here
            End If

        End Sub

    End Class


End Class
