Imports System.Windows.Forms
Imports System.Drawing
Imports System.Drawing.Drawing2D

Public Class Form2
    Private flagImages As New Dictionary(Of String, Image)()
    
    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Aplicar idioma al formulario
        Dim currentLanguage As String = LanguageManager.GetSavedLanguage()
        LanguageManager.ApplyLanguage(Me, currentLanguage)
        
        ' Crear banderas para cada idioma
        CrearBanderas()
        
        ' Configurar el ComboBox para dibujo personalizado
        cmbIdioma.DrawMode = DrawMode.OwnerDrawFixed
        cmbIdioma.DropDownStyle = ComboBoxStyle.DropDownList
        cmbIdioma.ItemHeight = 30
        
        ' Cargar idiomas disponibles
        cmbIdioma.Items.Clear()
        Dim languages As Dictionary(Of String, String) = LanguageManager.GetAvailableLanguages()
        
        For Each lang As KeyValuePair(Of String, String) In languages
            cmbIdioma.Items.Add(New LanguageItem(lang.Value, lang.Key))
        Next
        
        ' Seleccionar el idioma guardado o el del sistema
        Dim savedLang As String = LanguageManager.GetSavedLanguage()
        For i As Integer = 0 To cmbIdioma.Items.Count - 1
            Dim item As LanguageItem = DirectCast(cmbIdioma.Items(i), LanguageItem)
            If item.LanguageCode = savedLang Then
                cmbIdioma.SelectedIndex = i
                Exit For
            End If
        Next
        
        ' Si no se encontró, seleccionar el primero
        If cmbIdioma.SelectedIndex = -1 AndAlso cmbIdioma.Items.Count > 0 Then
            cmbIdioma.SelectedIndex = 0
        End If
    End Sub
    
    Private Sub CrearBanderas()
        ' Crear banderas simples con colores representativos
        flagImages("es-ES") = CrearBanderaEspaña()
        flagImages("en-US") = CrearBanderaUSA()
        flagImages("fr-FR") = CrearBanderaFrancia()
        flagImages("de-DE") = CrearBanderaAlemania()
        flagImages("it-IT") = CrearBanderaItalia()
        flagImages("pt-PT") = CrearBanderaPortugal()
    End Sub
    
    Private Function CrearBanderaEspaña() As Image
        Dim img As New Bitmap(40, 26)
        Using g As Graphics = Graphics.FromImage(img)
            g.SmoothingMode = SmoothingMode.AntiAlias
            ' Bandera de España: rojo, amarillo, rojo (horizontal)
            g.FillRectangle(New SolidBrush(Color.FromArgb(200, 20, 20)), 0, 0, 40, 8)
            g.FillRectangle(New SolidBrush(Color.FromArgb(255, 200, 0)), 0, 8, 40, 10)
            g.FillRectangle(New SolidBrush(Color.FromArgb(200, 20, 20)), 0, 18, 40, 8)
        End Using
        Return img
    End Function
    
    Private Function CrearBanderaUSA() As Image
        Dim img As New Bitmap(40, 26)
        Using g As Graphics = Graphics.FromImage(img)
            g.SmoothingMode = SmoothingMode.AntiAlias
            ' Bandera de USA: azul con estrellas y rayas rojas/blancas
            g.FillRectangle(New SolidBrush(Color.FromArgb(0, 40, 104)), 0, 0, 16, 10)
            ' Estrellas blancas (simplificadas - más visibles)
            Using brush As New SolidBrush(Color.White)
                For i As Integer = 0 To 3
                    For j As Integer = 0 To 2
                        g.FillEllipse(brush, 2 + i * 3, 1 + j * 3, 2.5F, 2.5F)
                    Next
                Next
            End Using
            ' Rayas rojas y blancas (más anchas)
            For i As Integer = 0 To 6
                Dim color As Color = If(i Mod 2 = 0, Color.FromArgb(191, 10, 48), Color.White)
                g.FillRectangle(New SolidBrush(color), 16, i * 2.2F, 24, 2.2F)
            Next
        End Using
        Return img
    End Function
    
    Private Function CrearBanderaFrancia() As Image
        Dim img As New Bitmap(40, 26)
        Using g As Graphics = Graphics.FromImage(img)
            g.SmoothingMode = SmoothingMode.AntiAlias
            ' Bandera de Francia: azul, blanco, rojo (vertical)
            g.FillRectangle(New SolidBrush(Color.FromArgb(0, 35, 149)), 0, 0, 13, 26)
            g.FillRectangle(New SolidBrush(Color.White), 13, 0, 14, 26)
            g.FillRectangle(New SolidBrush(Color.FromArgb(237, 41, 57)), 27, 0, 13, 26)
        End Using
        Return img
    End Function
    
    Private Function CrearBanderaAlemania() As Image
        Dim img As New Bitmap(40, 26)
        Using g As Graphics = Graphics.FromImage(img)
            g.SmoothingMode = SmoothingMode.AntiAlias
            ' Bandera de Alemania: negro, rojo, amarillo (horizontal)
            g.FillRectangle(New SolidBrush(Color.Black), 0, 0, 40, 8)
            g.FillRectangle(New SolidBrush(Color.FromArgb(221, 0, 0)), 0, 8, 40, 10)
            g.FillRectangle(New SolidBrush(Color.FromArgb(255, 206, 0)), 0, 18, 40, 8)
        End Using
        Return img
    End Function
    
    Private Function CrearBanderaItalia() As Image
        Dim img As New Bitmap(40, 26)
        Using g As Graphics = Graphics.FromImage(img)
            g.SmoothingMode = SmoothingMode.AntiAlias
            ' Bandera de Italia: verde, blanco, rojo (vertical)
            g.FillRectangle(New SolidBrush(Color.FromArgb(0, 146, 70)), 0, 0, 13, 26)
            g.FillRectangle(New SolidBrush(Color.White), 13, 0, 14, 26)
            g.FillRectangle(New SolidBrush(Color.FromArgb(206, 43, 55)), 27, 0, 13, 26)
        End Using
        Return img
    End Function
    
    Private Function CrearBanderaPortugal() As Image
        Dim img As New Bitmap(40, 26)
        Using g As Graphics = Graphics.FromImage(img)
            g.SmoothingMode = SmoothingMode.AntiAlias
            ' Bandera de Portugal: verde y rojo (vertical)
            g.FillRectangle(New SolidBrush(Color.FromArgb(0, 102, 0)), 0, 0, 18, 26)
            g.FillRectangle(New SolidBrush(Color.FromArgb(255, 0, 0)), 18, 0, 22, 26)
            ' Escudo simplificado (círculo)
            Using brush As New SolidBrush(Color.FromArgb(255, 200, 0))
                g.FillEllipse(brush, 12, 8, 10, 10)
            End Using
        End Using
        Return img
    End Function
    
    Private Sub CmbIdioma_DrawItem(sender As Object, e As DrawItemEventArgs) Handles cmbIdioma.DrawItem
        If e.Index < 0 Then Return
        
        e.DrawBackground()
        
        If e.Index < cmbIdioma.Items.Count Then
            Dim item As LanguageItem = DirectCast(cmbIdioma.Items(e.Index), LanguageItem)
            Dim flagImage As Image = Nothing
            
            ' Obtener la bandera correspondiente
            If flagImages.TryGetValue(item.LanguageCode, flagImage) Then
                ' Dibujar la bandera (más grande y centrada verticalmente)
                Dim flagWidth As Integer = 40
                Dim flagHeight As Integer = 26
                Dim flagY As Integer = e.Bounds.Y + (e.Bounds.Height - flagHeight) \ 2
                Dim flagRect As New Rectangle(e.Bounds.X + 3, flagY, flagWidth, flagHeight)
                e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias
                e.Graphics.DrawImage(flagImage, flagRect)
                
                ' Dibujar el texto
                Dim textRect As New Rectangle(e.Bounds.X + 48, e.Bounds.Y, e.Bounds.Width - 48, e.Bounds.Height)
                Dim textColor As Color = If((e.State And DrawItemState.Selected) = DrawItemState.Selected, Color.White, Color.Black)
                Using brush As New SolidBrush(textColor)
                    Using sf As New StringFormat()
                        sf.LineAlignment = StringAlignment.Center
                        e.Graphics.DrawString(item.DisplayName, e.Font, brush, textRect, sf)
                    End Using
                End Using
            Else
                ' Si no hay bandera, solo dibujar el texto
                Dim textColor As Color = If((e.State And DrawItemState.Selected) = DrawItemState.Selected, Color.White, Color.Black)
                Using brush As New SolidBrush(textColor)
                    e.Graphics.DrawString(item.DisplayName, e.Font, brush, e.Bounds)
                End Using
            End If
        End If
        
        e.DrawFocusRectangle()
    End Sub
    
    Private Sub BtnAceptar_Click(sender As Object, e As EventArgs) Handles btnAceptar.Click
        If cmbIdioma.SelectedItem IsNot Nothing Then
            Dim selectedItem As LanguageItem = DirectCast(cmbIdioma.SelectedItem, LanguageItem)
            LanguageManager.SaveLanguage(selectedItem.LanguageCode)
            Me.DialogResult = DialogResult.OK
            Me.Close()
        End If
    End Sub
    
    Private Sub BtnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub
    
    Private Sub Form2_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        ' Liberar recursos de las banderas
        For Each img As Image In flagImages.Values
            If img IsNot Nothing Then
                img.Dispose()
            End If
        Next
        flagImages.Clear()
    End Sub
End Class

' Clase auxiliar para almacenar información del idioma
Public Class LanguageItem
    Public Property DisplayName As String
    Public Property LanguageCode As String
    
    Public Sub New(displayName As String, languageCode As String)
        Me.DisplayName = displayName
        Me.LanguageCode = languageCode
    End Sub
    
    Public Overrides Function ToString() As String
        Return DisplayName
    End Function
End Class

