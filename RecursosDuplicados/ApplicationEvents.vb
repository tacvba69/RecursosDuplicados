Imports Microsoft.VisualBasic.ApplicationServices

Namespace My
    Partial Friend Class MyApplication
        Private Sub MyApplication_Startup(sender As Object, e As StartupEventArgs) Handles Me.Startup
            ' Verificar si ya se ha seleccionado un idioma
            If Not LanguageManager.IsLanguageSelected() Then
                ' Mostrar formulario de selección de idioma
                Using langForm As New Form2()
                    If langForm.ShowDialog() = DialogResult.OK Then
                        ' El idioma ya fue guardado en Form2
                    Else
                        ' Si se cancela, usar el idioma del sistema
                        Dim systemLang As String = LanguageManager.GetSystemLanguage()
                        LanguageManager.SaveLanguage(systemLang)
                    End If
                End Using
            End If
            
            ' Aplicar el idioma guardado al Form1
            Dim selectedLanguage As String = LanguageManager.GetSavedLanguage()
            LanguageManager.ApplyLanguage(Form1, selectedLanguage)
        End Sub
    End Class
End Namespace
