Module MainModule
    Sub Main()
        Application.EnableVisualStyles()
        Application.SetCompatibleTextRenderingDefault(False)

        ' Menjalankan form login
        Dim loginForm As New Login()
        Application.Run(loginForm)
    End Sub
End Module
