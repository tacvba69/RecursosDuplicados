Imports System.Globalization
Imports System.Threading
Imports System.Resources
Imports System.Reflection
Imports System.IO
Imports System.ComponentModel
Imports System.Windows.Forms

Public Module LanguageManager
    Private Const CONFIG_FILE As String = "language.config"
    
    ''' <summary>
    ''' Obtiene el idioma guardado o retorna el idioma del sistema
    ''' </summary>
    Public Function GetSavedLanguage() As String
        Try
            Dim configPath As String = Path.Combine(Application.StartupPath, CONFIG_FILE)
            If File.Exists(configPath) Then
                Dim content As String = File.ReadAllText(configPath).Trim()
                If Not String.IsNullOrEmpty(content) Then
                    Return content
                End If
            End If
        Catch
        End Try
        Return GetSystemLanguage()
    End Function
    
    ''' <summary>
    ''' Obtiene el idioma del sistema operativo
    ''' </summary>
    Public Function GetSystemLanguage() As String
        Dim culture As CultureInfo = CultureInfo.CurrentUICulture
        Dim langCode As String = culture.TwoLetterISOLanguageName.ToLower()
        
        ' Mapear códigos de idioma comunes
        Select Case langCode
            Case "es"
                Return "es-ES"
            Case "en"
                Return "en-US"
            Case "fr"
                Return "fr-FR"
            Case "de"
                Return "de-DE"
            Case "it"
                Return "it-IT"
            Case "pt"
                Return "pt-PT"
            Case Else
                Return "en-US" ' Idioma por defecto
        End Select
    End Function
    
    ''' <summary>
    ''' Guarda el idioma seleccionado
    ''' </summary>
    Public Sub SaveLanguage(languageCode As String)
        Try
            Dim configPath As String = Path.Combine(Application.StartupPath, CONFIG_FILE)
            File.WriteAllText(configPath, languageCode)
        Catch
        End Try
    End Sub
    
    ''' <summary>
    ''' Verifica si ya se ha seleccionado un idioma antes
    ''' </summary>
    Public Function IsLanguageSelected() As Boolean
        Try
            Dim configPath As String = Path.Combine(Application.StartupPath, CONFIG_FILE)
            Return File.Exists(configPath)
        Catch
        End Try
        Return False
    End Function
    
    ''' <summary>
    ''' Aplica el idioma al formulario y a todos sus controles
    ''' </summary>
    Public Sub ApplyLanguage(form As Form, languageCode As String)
        Try
            Dim culture As New CultureInfo(languageCode)
            Thread.CurrentThread.CurrentUICulture = culture
            Thread.CurrentThread.CurrentCulture = culture
            
            ' Aplicar recursos usando LanguageResources
            If TypeOf form Is Form1 Then
                ApplyLanguageToForm1(DirectCast(form, Form1), languageCode)
            ElseIf TypeOf form Is Form2 Then
                ApplyLanguageToForm2(DirectCast(form, Form2), languageCode)
            End If
        Catch ex As Exception
            ' Si falla, continuar con el idioma por defecto
        End Try
    End Sub
    
    ''' <summary>
    ''' Aplica el idioma específicamente al Form1
    ''' </summary>
    Private Sub ApplyLanguageToForm1(form As Form1, languageCode As String)
        Try
            ' Aplicar textos del formulario
            form.Text = LanguageResources.GetResource(languageCode, "Form1.Text")
            
            ' Aplicar textos de los controles del ToolStrip
            form.ToolStripButton1.Text = LanguageResources.GetResource(languageCode, "ToolStripButton1.Text")
            form.btnVistaIconos.Text = LanguageResources.GetResource(languageCode, "btnVistaIconos.Text")
            form.btnVistaDetalles.Text = LanguageResources.GetResource(languageCode, "btnVistaDetalles.Text")
            form.btnEliminarSeleccionados.Text = LanguageResources.GetResource(languageCode, "btnEliminarSeleccionados.Text")
            form.btnSeleccionarTodos.Text = LanguageResources.GetResource(languageCode, "btnSeleccionarTodos.Text")
            form.btnDeseleccionarTodos.Text = LanguageResources.GetResource(languageCode, "btnDeseleccionarTodos.Text")
            form.btnInvertirSeleccion.Text = LanguageResources.GetResource(languageCode, "btnInvertirSeleccion.Text")
            form.btnIdioma.Text = LanguageResources.GetResource(languageCode, "btnIdioma.Text")
            
            ' Aplicar textos de las columnas del ListView
            If form.lvDuplicados.Columns.Count >= 4 Then
                form.lvDuplicados.Columns(0).Text = LanguageResources.GetResource(languageCode, "lvDuplicados.Column0")
                form.lvDuplicados.Columns(1).Text = LanguageResources.GetResource(languageCode, "lvDuplicados.Column1")
                form.lvDuplicados.Columns(2).Text = LanguageResources.GetResource(languageCode, "lvDuplicados.Column2")
                form.lvDuplicados.Columns(3).Text = LanguageResources.GetResource(languageCode, "lvDuplicados.Column3")
            End If
            
            ' Actualizar nombres de grupos existentes
            If form.lvDuplicados.Groups.Count > 0 Then
                form.lvDuplicados.BeginUpdate()
                Dim grupoNumero As Integer = 1
                For Each grupo As ListViewGroup In form.lvDuplicados.Groups
                    Dim cantidadArchivos As Integer = grupo.Items.Count
                    Dim nuevoNombre As String = String.Format(LanguageResources.GetResource(languageCode, "MsgGrupo"), grupoNumero, cantidadArchivos)
                    grupo.Header = nuevoNombre
                    grupoNumero += 1
                Next
                form.lvDuplicados.EndUpdate()
            End If
        Catch
        End Try
    End Sub
    
    ''' <summary>
    ''' Aplica el idioma específicamente al Form2
    ''' </summary>
    Public Sub ApplyLanguageToForm2(form As Form2, languageCode As String)
        Try
            form.Text = LanguageResources.GetResource(languageCode, "Form2.Text")
            form.lblTitulo.Text = LanguageResources.GetResource(languageCode, "Form2.lblTitulo.Text")
            form.lblSeleccionar.Text = LanguageResources.GetResource(languageCode, "Form2.lblSeleccionar.Text")
            form.btnAceptar.Text = LanguageResources.GetResource(languageCode, "Form2.btnAceptar.Text")
            form.btnCancelar.Text = LanguageResources.GetResource(languageCode, "Form2.btnCancelar.Text")
        Catch
        End Try
    End Sub
    
    ''' <summary>
    ''' Aplica recursos a un control específico
    ''' </summary>
    Private Sub ApplyResourcesToControl(ctrl As Control, resources As ComponentResourceManager, languageCode As String)
        Try
            resources.ApplyResources(ctrl, ctrl.Name)
        Catch
        End Try
    End Sub
    
    ''' <summary>
    ''' Obtiene todos los controles recursivamente
    ''' </summary>
    Private Function GetAllControls(parent As Control) As List(Of Control)
        Dim controls As New List(Of Control)
        For Each ctrl As Control In parent.Controls
            controls.Add(ctrl)
            controls.AddRange(GetAllControls(ctrl))
        Next
        Return controls
    End Function
    
    ''' <summary>
    ''' Obtiene la lista de idiomas disponibles
    ''' </summary>
    Public Function GetAvailableLanguages() As Dictionary(Of String, String)
        Dim languages As New Dictionary(Of String, String)
        languages.Add("es-ES", "Español")
        languages.Add("en-US", "English")
        languages.Add("fr-FR", "Français")
        languages.Add("de-DE", "Deutsch")
        languages.Add("it-IT", "Italiano")
        languages.Add("pt-PT", "Português")
        Return languages
    End Function
End Module

