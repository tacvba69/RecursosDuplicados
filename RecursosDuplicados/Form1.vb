Imports System.IO
Imports System.Security.Cryptography
Imports System.Linq
Imports System.Threading.Tasks
Imports System.Drawing
Imports System.Runtime.InteropServices
Imports System.Windows.Forms
Imports Microsoft.VisualBasic.FileIO
Imports System.Collections

Public Module Shell32Helper
    <DllImport("shell32.dll", CharSet:=CharSet.Auto, BestFitMapping:=False, ThrowOnUnmappableChar:=True)>
    Friend Function SHGetFileInfo(
        <MarshalAs(UnmanagedType.LPTStr)> pszPath As String,
        dwFileAttributes As UInteger,
        ByRef psfi As SHFILEINFO,
        cbSizeFileInfo As UInteger,
        uFlags As UInteger
    ) As IntPtr
    End Function

    <DllImport("shell32.dll", CharSet:=CharSet.Unicode, PreserveSig:=False)>
    Friend Sub SHCreateItemFromParsingName(
        <MarshalAs(UnmanagedType.LPWStr)> pszPath As String,
        pbc As IntPtr,
        <MarshalAs(UnmanagedType.LPStruct)> riid As Guid,
        <MarshalAs(UnmanagedType.IUnknown)> ByRef ppv As Object
    )
    End Sub

    <DllImport("user32.dll")>
    Friend Function DestroyIcon(hIcon As IntPtr) As Boolean
    End Function

    Public Const SHGFI_ICON As UInteger = &H100
    Public Const SHGFI_LARGEICON As UInteger = &H0

    <StructLayout(LayoutKind.Sequential)>
    Public Structure SHFILEINFO
        Public hIcon As IntPtr
        Public iIcon As Integer
        Public dwAttributes As UInteger
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=260)>
        Public szDisplayName As String
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=80)>
        Public szTypeName As String
    End Structure

    Public Const IEI_PRIORITY_NORMAL As UInteger = 1
    Public Const S_OK As Integer = 0
    Public Const E_PENDING As Integer = &H8000000A
    Public Const SIIGBF_RESIZETOFIT As UInteger = &H0
    Public Const SIIGBF_THUMBNAILONLY As UInteger = &H8
End Module

' Interfaces COM para extraer miniaturas de videos
<ComImport(), Guid("BB2E617C-0920-11d1-9A0B-00C04FC2D6C1"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)>
Public Interface IExtractImage
    Function GetLocation(<MarshalAs(UnmanagedType.LPWStr)> ByRef pszPathBuffer As String,
                        ByVal cchMax As UInteger,
                        ByRef pdwPriority As UInteger,
                        ByRef prgSize As SIZE,
                        ByVal dwRecClrDepth As UInteger,
                        ByRef pdwFlags As UInteger) As Integer

    Function Extract(<Out()> ByRef phBmpThumbnail As IntPtr) As Integer
End Interface

' IShellItemImageFactory - más moderno y confiable (Windows Vista+)
<ComImport(), Guid("bcc18b79-ba16-442f-80c4-8a59c30c463b"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)>
Public Interface IShellItemImageFactory
    Function GetImage(<MarshalAs(UnmanagedType.Struct)> ByVal size As SIZE,
                     ByVal flags As UInteger,
                     <Out()> ByRef phbm As IntPtr) As Integer
End Interface

<StructLayout(LayoutKind.Sequential)>
Public Structure SIZE
    Public cx As Integer
    Public cy As Integer
End Structure

Public Class Form1
    Private imageListLarge As New ImageList()
    Private imageListSmall As New ImageList()
    Private imageIndexCounter As Integer = 0
    Private imageCache As New Dictionary(Of String, Integer)()
    Private tamanoMiniatura As Integer = 128 ' Tamaño inicial de las miniaturas
    Private Const TAMANO_MINIMO As Integer = 64
    Private Const TAMANO_MAXIMO As Integer = 256
    Private Const INCREMENTO As Integer = 16
    Private estaRegenerando As Boolean = False ' Control para evitar regeneraciones simultáneas
    Private estadosChecksPersistentes As New Dictionary(Of String, Boolean)() ' Estados persistentes

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Configurar ImageLists
        ActualizarTamanioMiniaturas()

        ' Asignar ImageLists al ListView
        lvDuplicados.LargeImageList = imageListLarge
        lvDuplicados.SmallImageList = imageListSmall

        lvDuplicados.View = View.LargeIcon
        lvDuplicados.CheckBoxes = True
        lvDuplicados.ShowGroups = True
        lvDuplicados.Columns.Add("Archivo", 500)
        lvDuplicados.Columns.Add("Ruta", 700)
        lvDuplicados.Columns.Add("Tamaño", 100)
        lvDuplicados.Columns.Add("Tipo", 100)
        lvDuplicados.FullRowSelect = True
        lvDuplicados.GridLines = True
        lvDuplicados.Sorting = SortOrder.None
        lvDuplicados.ListViewItemSorter = Nothing

        ToolStripProgressBar1.Visible = False
        lblProgreso.Text = ""

        ' Configurar estado inicial de los botones de vista
        ActualizarEstadoBotonesVista()
        
        ' Configurar iconos de los botones de selección
        btnSeleccionarTodos.Image = CrearIconoSeleccionarTodos()
        btnDeseleccionarTodos.Image = CrearIconoDeseleccionarTodos()
        btnInvertirSeleccion.Image = CrearIconoInvertirSeleccion()
        
        ' Aplicar idioma al formulario
        Dim selectedLanguage As String = LanguageManager.GetSavedLanguage()
        LanguageManager.ApplyLanguage(Me, selectedLanguage)
    End Sub

    Private Sub ActualizarEstadoBotonesVista()
        ' Actualizar el estado visual de los botones según la vista actual
        If lvDuplicados.View = View.LargeIcon Then
            btnVistaIconos.Checked = True
            btnVistaDetalles.Checked = False
        Else
            btnVistaIconos.Checked = False
            btnVistaDetalles.Checked = True
        End If
    End Sub

    Private Async Sub BuscarDuplicados()
        Using folderDlg As New FolderBrowserDialog()
            If folderDlg.ShowDialog() = DialogResult.OK Then
                Dim rutaCarpeta As String = folderDlg.SelectedPath
                lvDuplicados.Items.Clear()
                imageListLarge.Images.Clear()
                imageListSmall.Images.Clear()
                imageCache.Clear()
                imageIndexCounter = 0

                ToolStripProgressBar1.Visible = True
                ToolStripProgressBar1.Value = 0
                Dim selectedLanguage As String = LanguageManager.GetSavedLanguage()
                lblProgreso.Text = LanguageResources.GetResource(selectedLanguage, "MsgIniciandoAnalisis")

                Dim duplicados As List(Of List(Of String)) = Await Task.Run(Function() ObtenerDuplicadosConProgreso(rutaCarpeta))

                If duplicados.Count = 0 Then
                    lblProgreso.Text = LanguageResources.GetResource(selectedLanguage, "MsgNoDuplicados")
                    MessageBox.Show(LanguageResources.GetResource(selectedLanguage, "MsgNoDuplicados"))
                Else
                    Dim totalArchivos As Integer = duplicados.Sum(Function(g) g.Count)
                    Dim archivoActual As Integer = 0
                    Dim numeroGrupo As Integer = 1

                    ' Limpiar grupos existentes
                    lvDuplicados.Groups.Clear()

                    Dim selectedLanguageGrupos As String = LanguageManager.GetSavedLanguage()
                    For Each grupo As List(Of String) In duplicados
                        ' Crear un grupo para este conjunto de duplicados
                        Dim nombreGrupo As String = String.Format(LanguageResources.GetResource(selectedLanguageGrupos, "MsgGrupo"), numeroGrupo, grupo.Count)
                        Dim listViewGroup As New ListViewGroup(nombreGrupo) With {
                            .Header = nombreGrupo
                        }
                        lvDuplicados.Groups.Add(listViewGroup)

                        Dim copiasPermitidas As Integer = grupo.Count - 1
                        Dim contadorCheck As Integer = 0
                        For Each archivo As String In grupo
                            Try
                                ' Validar que el archivo existe antes de procesarlo
                                If Not File.Exists(archivo) Then
                                    Continue For
                                End If

                                Dim fi As New FileInfo(archivo)
                                Dim nombre = Path.GetFileName(archivo)
                                ' Validar que el nombre no esté vacío
                                If String.IsNullOrEmpty(nombre) Then
                                    Continue For
                                End If

                                Dim item As New ListViewItem(nombre)
                                item.SubItems.Add(archivo)
                                item.SubItems.Add(ConvertirTamanio(fi.Length))
                                item.SubItems.Add(fi.Extension.ToLower())

                                ' Obtener miniatura o icono
                                Dim imageIndex As Integer = Await ObtenerMiniaturaAsync(archivo)
                                item.ImageIndex = imageIndex

                                ' Asignar el item al grupo
                                item.Group = listViewGroup

                                If contadorCheck < copiasPermitidas Then
                                    item.Checked = True
                                    contadorCheck += 1
                                End If

                                lvDuplicados.Items.Add(item)
                                archivoActual += 1
                                lblProgreso.Text = String.Format(LanguageResources.GetResource(selectedLanguageGrupos, "MsgCargandoMiniaturas"), archivoActual, totalArchivos)
                                Application.DoEvents()
                            Catch ex As Exception
                                ' Ignorar error
                            End Try
                        Next
                        numeroGrupo += 1
                    Next
                    Dim selectedLanguage2 As String = LanguageManager.GetSavedLanguage()
                    lblProgreso.Text = String.Format(LanguageResources.GetResource(selectedLanguage2, "MsgSeEncontraronGrupos"), duplicados.Count)
                End If

                lvDuplicados.View = View.LargeIcon
                ToolStripProgressBar1.Visible = False
            End If
        End Using
    End Sub

    Private Function ObtenerDuplicadosConProgreso(ruta As String) As List(Of List(Of String))
        ' Validar que la ruta existe y es un directorio
        If String.IsNullOrWhiteSpace(ruta) OrElse Not Directory.Exists(ruta) Then
            Return New List(Of List(Of String))()
        End If

        ' Normalizar la ruta para evitar problemas de seguridad
        Dim rutaNormalizada As String
        Try
            rutaNormalizada = Path.GetFullPath(ruta)
            ' Validar que la ruta normalizada sigue siendo válida
            If Not Directory.Exists(rutaNormalizada) Then
                Return New List(Of List(Of String))()
            End If
        Catch ex As Exception
            ' Ruta inválida o sin permisos
            Return New List(Of List(Of String))()
        End Try

        ' Validar longitud de ruta (Windows tiene límite de 260 caracteres, pero puede extenderse)
        If rutaNormalizada.Length > 32767 Then
            Return New List(Of List(Of String))()
        End If

        Dim archivos As String() = Nothing
        Try
            archivos = Directory.GetFiles(rutaNormalizada, "*.*", System.IO.SearchOption.AllDirectories)
        Catch ex As UnauthorizedAccessException
            ' Sin permisos para acceder al directorio
            Invoke(Sub()
                       Dim selectedLanguage5 As String = LanguageManager.GetSavedLanguage()
                       MessageBox.Show(String.Format(LanguageResources.GetResource(selectedLanguage5, "MsgErrorPermisos"), rutaNormalizada), LanguageResources.GetResource(selectedLanguage5, "MsgTituloErrorPermisos"), MessageBoxButtons.OK, MessageBoxIcon.Warning)
                   End Sub)
            Return New List(Of List(Of String))()
        Catch ex As Exception
            ' Otro error al obtener archivos
            Return New List(Of List(Of String))()
        End Try

        ' Advertir si hay muchos archivos, pero permitir procesarlos todos
        If archivos IsNot Nothing AndAlso archivos.Length > 50000 Then
            Invoke(Sub()
                       Dim selectedLanguage6 As String = LanguageManager.GetSavedLanguage()
                       Dim resultado As DialogResult = MessageBox.Show(
                           String.Format(LanguageResources.GetResource(selectedLanguage6, "MsgMuchosArchivos"), archivos.Length, vbCrLf),
                           LanguageResources.GetResource(selectedLanguage6, "MsgTituloMuchosArchivos"), MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                       If resultado = DialogResult.No Then
                           Return
                       End If
                   End Sub)
        End If
        Dim hashDic As New Dictionary(Of String, List(Of String))()

        Invoke(Sub()
                   ToolStripProgressBar1.Maximum = archivos.Length
               End Sub)

        ' Reutilizar instancia de MD5 para mejor rendimiento
        Using md5 As MD5 = MD5.Create()
            For i As Integer = 0 To archivos.Length - 1
                Try
                    ' Validar longitud de ruta del archivo
                    If String.IsNullOrWhiteSpace(archivos(i)) OrElse archivos(i).Length > 32767 Then
                        Continue For
                    End If

                    ' Validar que el archivo existe y obtener información
                    If Not File.Exists(archivos(i)) Then
                        Continue For
                    End If

                    Dim fi As New FileInfo(archivos(i))

                    ' Validar que no sea un directorio (por si acaso)
                    If (fi.Attributes And FileAttributes.Directory) = FileAttributes.Directory Then
                        Continue For
                    End If

                    ' Nota: MD5.ComputeHash con FileStream es eficiente en memoria (lee por bloques)
                    ' El límite se establece muy alto para permitir archivos grandes (50GB)
                    ' Si necesitas procesar archivos aún más grandes, puedes aumentar este valor
                    Const TAMANO_MAXIMO_ARCHIVO As Long = 50L * 1024 * 1024 * 1024 ' 50 GB
                    If fi.Length > TAMANO_MAXIMO_ARCHIVO Then
                        ' Saltar archivos extremadamente grandes para evitar tiempos de procesamiento muy largos
                        Continue For
                    End If

                    ' Validar que el archivo no esté vacío (archivos vacíos pueden tener el mismo hash)
                    If fi.Length = 0 Then
                        Continue For
                    End If

                    ' Validar permisos de lectura antes de intentar abrir
                    Try
                        Using fs As FileStream = File.OpenRead(archivos(i))
                            Dim hash As String = BitConverter.ToString(md5.ComputeHash(fs)).Replace("-", "").ToLower()
                            Dim existingList As List(Of String) = Nothing
                            If Not hashDic.TryGetValue(hash, existingList) Then
                                existingList = New List(Of String)()
                                hashDic.Add(hash, existingList)
                            End If
                            existingList.Add(archivos(i))
                        End Using
                    Catch ex As UnauthorizedAccessException
                        ' Sin permisos de lectura - saltar archivo
                        Continue For
                    Catch ex As IOException
                        ' Archivo bloqueado o en uso - saltar archivo
                        Continue For
                    End Try
                Catch ex As Exception
                    ' Ignorar otros errores de lectura (archivos corruptos, etc.)
                End Try

                Dim iCopy = i
                Dim gruposEncontrados = hashDic.Values.Where(Function(l) l.Count > 1).Count()
                Invoke(Sub()
                           ToolStripProgressBar1.Value = iCopy + 1
                           Dim selectedLanguage4 As String = LanguageManager.GetSavedLanguage()
                           lblProgreso.Text = String.Format(LanguageResources.GetResource(selectedLanguage4, "MsgRevisandoArchivo"), iCopy + 1, archivos.Length, gruposEncontrados)
                       End Sub)
            Next
        End Using

        Return hashDic.Values.Where(Function(list) list.Count > 1).ToList()
    End Function

    Private Function ConvertirTamanio(bytes As Long) As String
        Dim kb As Double = bytes / 1024.0
        If kb < 1024 Then
            Return $"{Math.Round(kb, 2)} KB"
        Else
            Dim gb As Double = kb / 1024.0
            Return $"{Math.Round(gb, 2)} MB"
        End If
    End Function

    Private Async Function ObtenerMiniaturaAsync(rutaArchivo As String) As Task(Of Integer)
        Return Await Task.Run(Function() ObtenerMiniatura(rutaArchivo))
    End Function

    Private Function ObtenerMiniatura(rutaArchivo As String) As Integer
        ' Validar ruta antes de procesar
        If String.IsNullOrWhiteSpace(rutaArchivo) OrElse rutaArchivo.Length > 32767 OrElse Not File.Exists(rutaArchivo) Then
            ' Retornar índice 0 (icono genérico) si la ruta no es válida
            Return 0
        End If

        ' Limpieza inteligente del caché: solo limpiar si es realmente necesario
        ' Aumentar límite significativamente y limpiar solo cuando sea crítico
        Const MAX_CACHE_SIZE As Integer = 50000 ' Aumentado para permitir más archivos
        If imageCache.Count > MAX_CACHE_SIZE Then
            ' Limpiar caché de forma más conservadora (solo 10% a la vez)
            Dim cantidadALimpiar As Integer = Math.Max(1000, imageCache.Count \ 10)
            Dim keysToRemove = imageCache.Keys.Take(cantidadALimpiar).ToList()
            For Each key In keysToRemove
                imageCache.Remove(key)
            Next
        End If

        ' Verificar si ya tenemos esta imagen en caché
        Dim cachedIndex As Integer
        If imageCache.TryGetValue(rutaArchivo, cachedIndex) Then
            Return cachedIndex
        End If

        Try
            Dim fi As New FileInfo(rutaArchivo)
            Dim ext As String = fi.Extension.ToLower()
            Dim thumbnail As Image = Nothing

            ' Para archivos de imagen, cargar y redimensionar
            Dim extensionesImagen As String() = {".jpg", ".jpeg", ".png", ".bmp", ".gif", ".webp", ".tiff", ".tif"}
            If extensionesImagen.Contains(ext) Then
                Try
                    Using originalImg As Image = Image.FromFile(rutaArchivo)
                        thumbnail = RedimensionarImagen(originalImg, tamanoMiniatura, tamanoMiniatura)
                    End Using
                Catch
                    ' Si falla, intentar con shell
                    thumbnail = If(ObtenerMiniaturaShell(rutaArchivo, tamanoMiniatura), ObtenerIconoSistema(rutaArchivo, tamanoMiniatura))
                End Try
            Else
                ' Para videos, intentar obtener miniatura real primero
                Dim extensionesVideo As String() = {".mp4", ".avi", ".mkv", ".mov", ".wmv", ".flv", ".webm", ".m4v", ".3gp", ".mpg", ".mpeg"}
                If extensionesVideo.Contains(ext) Then
                    ' Intentar obtener miniatura de video del shell
                    thumbnail = If(ObtenerMiniaturaVideo(rutaArchivo, tamanoMiniatura), ObtenerMiniaturaShell(rutaArchivo, tamanoMiniatura))
                Else
                    ' Para otros archivos, usar el shell de Windows
                    thumbnail = ObtenerMiniaturaShell(rutaArchivo, tamanoMiniatura)
                End If

                If thumbnail Is Nothing Then
                    ' Si falla, usar icono del sistema
                    thumbnail = ObtenerIconoSistema(rutaArchivo, tamanoMiniatura)
                End If
            End If

            ' Si aún no tenemos thumbnail, usar icono genérico
            If thumbnail Is Nothing Then
                thumbnail = CrearIconoGenerico(tamanoMiniatura)
            End If

            ' Agregar al ImageList con limpieza inteligente
            Dim indice As Integer = 0
            Invoke(Sub()
                       ' Limpieza inteligente: solo limpiar si realmente es necesario
                       ' Aumentar límite significativamente para permitir más archivos
                       Const MAX_IMAGES As Integer = 50000 ' Aumentado para permitir más archivos
                       If imageListLarge.Images.Count >= MAX_IMAGES Then
                           ' Limpiar solo imágenes que no están siendo usadas actualmente
                           ' Obtener índices en uso del ListView
                           Dim indicesEnUso As New HashSet(Of Integer)
                           For Each item As ListViewItem In lvDuplicados.Items
                               If item.ImageIndex >= 0 AndAlso item.ImageIndex < imageListLarge.Images.Count Then
                                   indicesEnUso.Add(item.ImageIndex)
                               End If
                           Next

                           ' Limpiar solo imágenes no usadas (máximo 10% a la vez)
                           Dim cantidadALimpiar As Integer = Math.Min(5000, imageListLarge.Images.Count \ 10)
                           Dim indicesParaLimpiar As New List(Of Integer)

                           ' Buscar índices no usados para limpiar
                           For i As Integer = 0 To Math.Min(cantidadALimpiar * 2, imageListLarge.Images.Count - 1)
                               If Not indicesEnUso.Contains(i) Then
                                   indicesParaLimpiar.Add(i)
                                   If indicesParaLimpiar.Count >= cantidadALimpiar Then
                                       Exit For
                                   End If
                               End If
                           Next

                           ' Limpiar en orden inverso para mantener índices válidos
                           indicesParaLimpiar.Sort()
                           indicesParaLimpiar.Reverse()

                           For Each idx In indicesParaLimpiar
                               Try
                                   If idx < imageListLarge.Images.Count Then
                                       imageListLarge.Images.RemoveAt(idx)
                                   End If
                                   If idx < imageListSmall.Images.Count Then
                                       imageListSmall.Images.RemoveAt(idx)
                                   End If
                               Catch
                               End Try
                           Next

                       End If

                       imageListLarge.Images.Add(thumbnail)
                       imageListSmall.Images.Add(RedimensionarImagen(thumbnail, 32, 32))
                       indice = imageIndexCounter
                       imageCache(rutaArchivo) = imageIndexCounter
                       imageIndexCounter += 1
                   End Sub)

            Return indice
        Catch ex As Exception
            ' En caso de error, usar icono genérico
            Try
                Dim iconoGenerico As Image = CrearIconoGenerico(tamanoMiniatura)
                Dim indice As Integer = 0
                Invoke(Sub()
                           imageListLarge.Images.Add(iconoGenerico)
                           imageListSmall.Images.Add(RedimensionarImagen(iconoGenerico, 32, 32))
                           indice = imageIndexCounter
                           imageCache(rutaArchivo) = imageIndexCounter
                           imageIndexCounter += 1
                       End Sub)
                Return indice
            Catch
                Return 0
            End Try
        End Try
    End Function

    Private Function RedimensionarImagen(original As Image, ancho As Integer, alto As Integer) As Image
        Dim nuevaImagen As New Bitmap(ancho, alto)
        Using g As Graphics = Graphics.FromImage(nuevaImagen)
            g.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
            g.SmoothingMode = Drawing2D.SmoothingMode.HighQuality
            g.PixelOffsetMode = Drawing2D.PixelOffsetMode.HighQuality
            g.CompositingQuality = Drawing2D.CompositingQuality.HighQuality
            g.DrawImage(original, 0, 0, ancho, alto)
        End Using
        Return nuevaImagen
    End Function

    Private Function ObtenerMiniaturaVideo(rutaArchivo As String, tamano As Integer) As Image
        ' Validar ruta antes de procesar
        If String.IsNullOrEmpty(rutaArchivo) OrElse Not File.Exists(rutaArchivo) Then
            Return Nothing
        End If

        Try
            ' Normalizar la ruta para evitar problemas de seguridad
            Dim rutaNormalizada As String = Path.GetFullPath(rutaArchivo)

            ' Método 1: Usar SHCreateItemFromParsingName (método moderno y más confiable)
            Dim shellItem As Object = Nothing
            Try
                Dim iidShellItem As New Guid("43826d1e-e718-42ee-bc55-a1e261c37bfe")
                Shell32Helper.SHCreateItemFromParsingName(rutaNormalizada, IntPtr.Zero, iidShellItem, shellItem)
            Catch
            End Try

            If shellItem IsNot Nothing Then
                ' Intentar obtener IShellItemImageFactory
                Dim imageFactory As IShellItemImageFactory = Nothing
                Dim unk As IntPtr = IntPtr.Zero
                Try
                    unk = Marshal.GetIUnknownForObject(shellItem)
                    Dim iid As New Guid("bcc18b79-ba16-442f-80c4-8a59c30c463b")
                    Dim ppv As IntPtr = IntPtr.Zero
                    Dim hr As Integer = Marshal.QueryInterface(unk, iid, ppv)

                    If hr = 0 AndAlso ppv <> IntPtr.Zero Then
                        imageFactory = CType(Marshal.GetObjectForIUnknown(ppv), IShellItemImageFactory)
                        Marshal.Release(ppv)
                    End If
                Catch
                Finally
                    If unk <> IntPtr.Zero Then
                        Marshal.Release(unk)
                    End If
                End Try

                If imageFactory IsNot Nothing Then
                    Try
                        Dim size As New SIZE() With {.cx = tamano, .cy = tamano}
                        ' Intentar primero con THUMBNAILONLY para obtener miniatura real
                        Dim flags As UInteger = Shell32Helper.SIIGBF_RESIZETOFIT Or Shell32Helper.SIIGBF_THUMBNAILONLY
                        Dim phbm As IntPtr = IntPtr.Zero
                        Dim hr As Integer = imageFactory.GetImage(size, flags, phbm)

                        If hr = Shell32Helper.S_OK AndAlso phbm <> IntPtr.Zero Then
                            Try
                                Using bitmap As Bitmap = Image.FromHbitmap(phbm)
                                    DeleteObject(phbm)
                                    Return RedimensionarImagen(bitmap, tamano, tamano)
                                End Using
                            Catch
                                If phbm <> IntPtr.Zero Then
                                    DeleteObject(phbm)
                                End If
                            End Try
                        Else
                            ' Si THUMBNAILONLY falla, intentar sin ese flag
                            flags = Shell32Helper.SIIGBF_RESIZETOFIT
                            phbm = IntPtr.Zero
                            hr = imageFactory.GetImage(size, flags, phbm)
                            If hr = Shell32Helper.S_OK AndAlso phbm <> IntPtr.Zero Then
                                Try
                                    Using bitmap As Bitmap = Image.FromHbitmap(phbm)
                                        DeleteObject(phbm)
                                        Return RedimensionarImagen(bitmap, tamano, tamano)
                                    End Using
                                Catch
                                    If phbm <> IntPtr.Zero Then
                                        DeleteObject(phbm)
                                    End If
                                End Try
                            End If
                        End If
                    Catch
                    End Try
                End If

                ' Liberar shellItem si es necesario
                Try
                    Marshal.ReleaseComObject(shellItem)
                Catch
                End Try
            End If

            ' Método 2: Usar Shell.Application como fallback
            Dim shell As Object = Nothing
            Dim folder As Object = Nothing
            Dim item As Object = Nothing
            Try
                ' Validar y normalizar rutas antes de usar Path.GetDirectoryName
                Dim directorio As String = Path.GetDirectoryName(rutaNormalizada)
                Dim nombreArchivo As String = Path.GetFileName(rutaNormalizada)

                If String.IsNullOrEmpty(directorio) OrElse String.IsNullOrEmpty(nombreArchivo) Then
                    Return Nothing
                End If

                shell = Activator.CreateInstance(Type.GetTypeFromProgID("Shell.Application"))
                folder = shell.NameSpace(directorio)
                item = folder.ParseName(nombreArchivo)

                ' Intentar obtener IShellItemImageFactory
                Dim imageFactory2 As IShellItemImageFactory = Nothing
                Dim unk2 As IntPtr = IntPtr.Zero
                Try
                    unk2 = Marshal.GetIUnknownForObject(item)
                    Dim iid As New Guid("bcc18b79-ba16-442f-80c4-8a59c30c463b")
                    Dim ppv As IntPtr = IntPtr.Zero
                    Dim hr As Integer = Marshal.QueryInterface(unk2, iid, ppv)

                    If hr = 0 AndAlso ppv <> IntPtr.Zero Then
                        imageFactory2 = CType(Marshal.GetObjectForIUnknown(ppv), IShellItemImageFactory)
                        Marshal.Release(ppv)
                    End If
                Catch
                Finally
                    If unk2 <> IntPtr.Zero Then
                        Marshal.Release(unk2)
                    End If
                End Try

                If imageFactory2 IsNot Nothing Then
                    Try
                        Dim size As New SIZE() With {.cx = tamano, .cy = tamano}
                        Dim flags As UInteger = Shell32Helper.SIIGBF_RESIZETOFIT Or Shell32Helper.SIIGBF_THUMBNAILONLY
                        Dim phbm As IntPtr = IntPtr.Zero
                        Dim hr As Integer = imageFactory2.GetImage(size, flags, phbm)

                        If hr = Shell32Helper.S_OK AndAlso phbm <> IntPtr.Zero Then
                            Try
                                Using bitmap As Bitmap = Image.FromHbitmap(phbm)
                                    DeleteObject(phbm)
                                    Return RedimensionarImagen(bitmap, tamano, tamano)
                                End Using
                            Catch
                                If phbm <> IntPtr.Zero Then
                                    DeleteObject(phbm)
                                End If
                            End Try
                        End If
                    Catch
                    End Try
                End If

                ' Intentar IExtractImage como último recurso
                Dim extractImage As IExtractImage = Nothing
                Dim unk3 As IntPtr = IntPtr.Zero
                Try
                    unk3 = Marshal.GetIUnknownForObject(item)
                    Dim iidExtract As New Guid("BB2E617C-0920-11d1-9A0B-00C04FC2D6C1")
                    Dim ppvExtract As IntPtr = IntPtr.Zero
                    Dim hr As Integer = Marshal.QueryInterface(unk3, iidExtract, ppvExtract)

                    If hr = 0 AndAlso ppvExtract <> IntPtr.Zero Then
                        extractImage = CType(Marshal.GetObjectForIUnknown(ppvExtract), IExtractImage)
                        Marshal.Release(ppvExtract)
                    End If
                Catch
                Finally
                    If unk3 <> IntPtr.Zero Then
                        Marshal.Release(unk3)
                    End If
                End Try

                If extractImage IsNot Nothing Then
                    Try
                        Dim pszPathBuffer As String = Nothing
                        Dim pdwPriority As UInteger = Shell32Helper.IEI_PRIORITY_NORMAL
                        Dim prgSize As New SIZE() With {.cx = tamano, .cy = tamano}
                        Dim dwRecClrDepth As UInteger = 32
                        Dim pdwFlags As UInteger = 0

                        Dim hr As Integer = extractImage.GetLocation(pszPathBuffer, 260, pdwPriority, prgSize, dwRecClrDepth, pdwFlags)

                        If hr = Shell32Helper.S_OK OrElse hr = Shell32Helper.E_PENDING Then
                            Dim phBmpThumbnail As IntPtr = IntPtr.Zero
                            hr = extractImage.Extract(phBmpThumbnail)

                            If hr = Shell32Helper.S_OK AndAlso phBmpThumbnail <> IntPtr.Zero Then
                                Try
                                    Using bitmap As Bitmap = Image.FromHbitmap(phBmpThumbnail)
                                        DeleteObject(phBmpThumbnail)
                                        Return RedimensionarImagen(bitmap, tamano, tamano)
                                    End Using
                                Catch
                                    If phBmpThumbnail <> IntPtr.Zero Then
                                        DeleteObject(phBmpThumbnail)
                                    End If
                                End Try
                            End If
                        End If
                    Catch
                    End Try
                End If
            Catch
            Finally
                ' Liberar recursos COM en orden inverso
                Try
                    If item IsNot Nothing Then
                        Marshal.ReleaseComObject(item)
                    End If
                Catch
                End Try
                Try
                    If folder IsNot Nothing Then
                        Marshal.ReleaseComObject(folder)
                    End If
                Catch
                End Try
                Try
                    If shell IsNot Nothing Then
                        Marshal.ReleaseComObject(shell)
                    End If
                Catch
                End Try
            End Try

            ' Si todo falla, usar el método del shell (solo icono)
            Return ObtenerMiniaturaVideoShell(rutaArchivo, tamano)
        Catch
            Return Nothing
        End Try
    End Function

    Private Function ObtenerMiniaturaVideoShell(rutaArchivo As String, tamano As Integer) As Image
        ' Validar ruta antes de procesar
        If String.IsNullOrEmpty(rutaArchivo) OrElse Not File.Exists(rutaArchivo) Then
            Return Nothing
        End If

        Try
            ' Normalizar la ruta para evitar problemas de seguridad
            Dim rutaNormalizada As String = Path.GetFullPath(rutaArchivo)
            ' Método alternativo: usar SHGetFileInfo con flags para obtener thumbnail
            ' Esto funciona mejor en versiones modernas de Windows
            Dim shInfo As New Shell32Helper.SHFILEINFO
            Dim flags As UInteger = Shell32Helper.SHGFI_ICON Or Shell32Helper.SHGFI_LARGEICON
            Dim hIcon As IntPtr = Shell32Helper.SHGetFileInfo(rutaNormalizada, 0, shInfo, CUInt(Marshal.SizeOf(shInfo)), flags)

            If hIcon <> IntPtr.Zero AndAlso shInfo.hIcon <> IntPtr.Zero Then
                Try
                    Using icono As Icon = Icon.FromHandle(shInfo.hIcon)
                        Dim iconoBitmap As New Bitmap(icono.ToBitmap())
                        Shell32Helper.DestroyIcon(shInfo.hIcon)
                        Return RedimensionarImagen(iconoBitmap, tamano, tamano)
                    End Using
                Catch
                    Shell32Helper.DestroyIcon(shInfo.hIcon)
                End Try
            End If
        Catch
        End Try
        Return Nothing
    End Function

    <DllImport("gdi32.dll")>
    Private Shared Function DeleteObject(hObject As IntPtr) As Boolean
    End Function

    Private Function ObtenerMiniaturaShell(rutaArchivo As String, tamano As Integer) As Image
        ' Validar ruta antes de procesar
        If String.IsNullOrEmpty(rutaArchivo) OrElse Not File.Exists(rutaArchivo) Then
            Return Nothing
        End If

        Try
            ' Normalizar la ruta para evitar problemas de seguridad
            Dim rutaNormalizada As String = Path.GetFullPath(rutaArchivo)
            Dim shInfo As New Shell32Helper.SHFILEINFO
            Dim flags As UInteger = Shell32Helper.SHGFI_ICON Or Shell32Helper.SHGFI_LARGEICON

            Dim hIcon As IntPtr = Shell32Helper.SHGetFileInfo(rutaNormalizada, 0, shInfo, CUInt(Marshal.SizeOf(shInfo)), flags)

            If hIcon <> IntPtr.Zero AndAlso shInfo.hIcon <> IntPtr.Zero Then
                Try
                    Using icono As Icon = Icon.FromHandle(shInfo.hIcon)
                        Dim iconoBitmap As New Bitmap(icono.ToBitmap())
                        Shell32Helper.DestroyIcon(shInfo.hIcon)
                        Return RedimensionarImagen(iconoBitmap, tamano, tamano)
                    End Using
                Catch
                    Shell32Helper.DestroyIcon(shInfo.hIcon)
                End Try
            End If
        Catch
        End Try
        Return Nothing
    End Function

    Private Function ObtenerIconoSistema(rutaArchivo As String, tamano As Integer) As Image
        ' Validar ruta antes de procesar
        If String.IsNullOrEmpty(rutaArchivo) OrElse Not File.Exists(rutaArchivo) Then
            Return CrearIconoGenerico(tamano)
        End If

        Try
            ' Normalizar la ruta para evitar problemas de seguridad
            Dim rutaNormalizada As String = Path.GetFullPath(rutaArchivo)
            Dim icono As Icon = Icon.ExtractAssociatedIcon(rutaNormalizada)
            If icono IsNot Nothing Then
                Try
                    Using iconoBitmap As New Bitmap(icono.ToBitmap())
                        Return RedimensionarImagen(iconoBitmap, tamano, tamano)
                    End Using
                Finally
                    ' Liberar el icono si es necesario
                    icono.Dispose()
                End Try
            End If
        Catch
        End Try
        Return CrearIconoGenerico(tamano)
    End Function

    Private Function CrearIconoGenerico(tamano As Integer) As Image
        Dim img As New Bitmap(tamano, tamano)
        Using g As Graphics = Graphics.FromImage(img)
            g.Clear(Color.LightGray)
            Using font As New Font("Arial", CSng(tamano / 4), FontStyle.Bold)
                Using brush As New SolidBrush(Color.DarkGray)
                    g.DrawString("?", font, brush, CSng(tamano / 3), CSng(tamano / 4))
                End Using
            End Using
        End Using
        Return img
    End Function
    
    Private Function CrearIconoSeleccionarTodos() As Image
        Dim img As New Bitmap(16, 16)
        Using g As Graphics = Graphics.FromImage(img)
            g.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
            g.Clear(Color.Transparent)
            ' Dibujar múltiples checkboxes marcados
            Using pen As New Pen(Color.Black, 1.5F)
                Using brush As New SolidBrush(Color.Green)
                    ' Checkbox 1 (arriba izquierda)
                    g.DrawRectangle(pen, 1, 1, 5, 5)
                    g.FillRectangle(brush, 2, 2, 3, 3)
                    ' Checkbox 2 (arriba derecha)
                    g.DrawRectangle(pen, 10, 1, 5, 5)
                    g.FillRectangle(brush, 11, 2, 3, 3)
                    ' Checkbox 3 (abajo izquierda)
                    g.DrawRectangle(pen, 1, 10, 5, 5)
                    g.FillRectangle(brush, 2, 11, 3, 3)
                    ' Checkbox 4 (abajo derecha)
                    g.DrawRectangle(pen, 10, 10, 5, 5)
                    g.FillRectangle(brush, 11, 11, 3, 3)
                End Using
            End Using
        End Using
        Return img
    End Function
    
    Private Function CrearIconoDeseleccionarTodos() As Image
        Dim img As New Bitmap(16, 16)
        Using g As Graphics = Graphics.FromImage(img)
            g.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
            g.Clear(Color.Transparent)
            ' Dibujar múltiples checkboxes desmarcados
            Using pen As New Pen(Color.Gray, 1.5F)
                ' Checkbox 1 (arriba izquierda)
                g.DrawRectangle(pen, 1, 1, 5, 5)
                ' Checkbox 2 (arriba derecha)
                g.DrawRectangle(pen, 10, 1, 5, 5)
                ' Checkbox 3 (abajo izquierda)
                g.DrawRectangle(pen, 1, 10, 5, 5)
                ' Checkbox 4 (abajo derecha)
                g.DrawRectangle(pen, 10, 10, 5, 5)
            End Using
        End Using
        Return img
    End Function
    
    Private Function CrearIconoInvertirSeleccion() As Image
        Dim img As New Bitmap(16, 16)
        Using g As Graphics = Graphics.FromImage(img)
            g.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
            g.Clear(Color.Transparent)
            ' Dibujar flechas bidireccionales (↔)
            Using pen As New Pen(Color.Blue, 2F)
                pen.EndCap = Drawing2D.LineCap.ArrowAnchor
                ' Flecha izquierda
                g.DrawLine(pen, 12, 8, 4, 8)
                ' Flecha derecha
                g.DrawLine(pen, 4, 8, 12, 8)
                ' Línea vertical central
                g.DrawLine(pen, 8, 3, 8, 13)
            End Using
        End Using
        Return img
    End Function

    ' Botón buscar
    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click
        BuscarDuplicados()
    End Sub


    Private Sub LvDuplicados_DoubleClick(sender As Object, e As EventArgs) Handles lvDuplicados.DoubleClick
        If lvDuplicados.SelectedItems.Count = 0 Then
            Return
        End If

        Dim item As ListViewItem = lvDuplicados.SelectedItems(0)
        If item.SubItems.Count < 2 Then
            Return
        End If

        Dim ruta As String = item.SubItems(1).Text
        ' Validar y normalizar la ruta antes de usarla
        If Not String.IsNullOrWhiteSpace(ruta) AndAlso ruta.Length <= 32767 AndAlso File.Exists(ruta) Then
            Try
                ' Normalizar la ruta para evitar problemas de seguridad
                Dim rutaNormalizada As String = Path.GetFullPath(ruta)

                ' Validar que la ruta normalizada existe y es un archivo
                If Not File.Exists(rutaNormalizada) Then
                    MessageBox.Show("El archivo ya no existe.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Return
                End If

                ' Validar que no sea un directorio
                Dim fi As New FileInfo(rutaNormalizada)
                If (fi.Attributes And FileAttributes.Directory) = FileAttributes.Directory Then
                    MessageBox.Show("No se puede abrir un directorio de esta manera.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Return
                End If

                ' Usar ProcessStartInfo para mayor seguridad
                Dim psi As New ProcessStartInfo("explorer.exe") With {
                    .Arguments = "/select,""" & rutaNormalizada & """",
                    .UseShellExecute = True,
                    .ErrorDialog = True
                }
                Process.Start(psi)
            Catch ex As UnauthorizedAccessException
                MessageBox.Show($"No se tienen permisos para abrir el archivo: {ex.Message}", "Error de permisos", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Catch ex As Exception
                MessageBox.Show($"Error al abrir el archivo: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        Else
            MessageBox.Show("La ruta del archivo no es válida.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    Private Sub BtnVistaIconos_Click(sender As Object, e As EventArgs) Handles btnVistaIconos.Click
        ' Cambiar a vista de iconos grandes
        lvDuplicados.View = View.LargeIcon
        ActualizarEstadoBotonesVista()
    End Sub

    Private Sub BtnVistaDetalles_Click(sender As Object, e As EventArgs) Handles btnVistaDetalles.Click
        ' Cambiar a vista de detalles
        lvDuplicados.View = View.Details
        ActualizarEstadoBotonesVista()
    End Sub

    Private Sub BtnEliminarSeleccionados_Click(sender As Object, e As EventArgs) Handles btnEliminarSeleccionados.Click
        ' Validar que el ListView no esté vacío
        Dim selectedLanguage As String = LanguageManager.GetSavedLanguage()
        If lvDuplicados.Items.Count = 0 Then
            MessageBox.Show(LanguageResources.GetResource(selectedLanguage, "MsgNoArchivosLista"), LanguageResources.GetResource(selectedLanguage, "MsgTituloInformacion"), MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        ' Contar archivos marcados
        Dim archivosMarcados As New List(Of ListViewItem)
        For Each item As ListViewItem In lvDuplicados.Items
            If item.Checked AndAlso item.SubItems.Count > 1 AndAlso Not String.IsNullOrWhiteSpace(item.SubItems(1).Text) Then
                archivosMarcados.Add(item)
            End If
        Next

        If archivosMarcados.Count = 0 Then
            MessageBox.Show(LanguageResources.GetResource(selectedLanguage, "MsgNoArchivosSeleccionados"), LanguageResources.GetResource(selectedLanguage, "MsgTituloInformacion"), MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        ' Advertir si hay muchos archivos a eliminar, pero permitir eliminarlos todos
        If archivosMarcados.Count > 500 Then
            Dim resultadoAdvertencia As DialogResult = MessageBox.Show(
                String.Format(LanguageResources.GetResource(selectedLanguage, "MsgConfirmacionRequerida"), archivosMarcados.Count, vbCrLf),
                LanguageResources.GetResource(selectedLanguage, "MsgTituloConfirmacionRequerida"), MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
            If resultadoAdvertencia <> DialogResult.Yes Then
                Return
            End If
        End If

        ' Confirmar eliminación
        Dim mensaje As String = String.Format(LanguageResources.GetResource(selectedLanguage, "MsgConfirmarEliminacion"), archivosMarcados.Count)
        Dim resultado As DialogResult = MessageBox.Show(mensaje, LanguageResources.GetResource(selectedLanguage, "MsgTituloConfirmarEliminacion"), MessageBoxButtons.YesNo, MessageBoxIcon.Warning)

        If resultado <> DialogResult.Yes Then
            Return
        End If

        ' Eliminar archivos
        Dim eliminadosExitosos As Integer = 0
        Dim eliminadosFallidos As Integer = 0
        Dim errores As New List(Of String)

        lblProgreso.Text = LanguageResources.GetResource(selectedLanguage, "MsgEliminandoArchivos")
        Application.DoEvents()

        For Each item As ListViewItem In archivosMarcados
            Try
                ' Validar que el item tiene suficientes subitems
                If item.SubItems.Count < 2 Then
                    eliminadosFallidos += 1
                    errores.Add($"Item inválido: {item.Text}")
                    Continue For
                End If

                Dim rutaArchivo As String = item.SubItems(1).Text
                If Not String.IsNullOrWhiteSpace(rutaArchivo) AndAlso rutaArchivo.Length <= 32767 Then
                    ' Validar y normalizar la ruta antes de eliminar
                    Try
                        Dim rutaNormalizada As String = Path.GetFullPath(rutaArchivo)

                        ' Validar que la ruta normalizada existe y es un archivo (no directorio)
                        If Not File.Exists(rutaNormalizada) Then
                            eliminadosFallidos += 1
                            errores.Add($"Archivo no encontrado: {item.Text}")
                            Continue For
                        End If

                        ' Validar que no sea un directorio
                        Dim fi As New FileInfo(rutaNormalizada)
                        If (fi.Attributes And FileAttributes.Directory) = FileAttributes.Directory Then
                            eliminadosFallidos += 1
                            errores.Add($"No se puede eliminar un directorio: {item.Text}")
                            Continue For
                        End If

                        ' Validar permisos antes de intentar eliminar
                        Try
                            ' Intentar abrir el archivo para verificar permisos
                            Using fs As FileStream = File.Open(rutaNormalizada, FileMode.Open, FileAccess.Read, FileShare.Delete)
                                ' Archivo accesible, continuar
                            End Using
                        Catch permEx As UnauthorizedAccessException
                            eliminadosFallidos += 1
                            errores.Add($"Sin permisos para eliminar: {item.Text}")
                            Continue For
                        Catch ioEx As IOException
                            ' Archivo en uso, intentar eliminar de todas formas
                        End Try

                        ' Enviar a la papelera de reciclaje en lugar de eliminar permanentemente
                        FileSystem.DeleteFile(rutaNormalizada, UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin)
                        eliminadosExitosos += 1
                        ' Eliminar el item del ListView
                        lvDuplicados.Items.Remove(item)
                    Catch pathEx As Exception
                        ' Error al normalizar la ruta (ruta inválida)
                        eliminadosFallidos += 1
                        errores.Add($"Ruta inválida: {item.Text} ({pathEx.Message})")
                    End Try
                Else
                    eliminadosFallidos += 1
                    errores.Add($"Ruta vacía o inválida: {item.Text}")
                End If
            Catch ex As Exception
                eliminadosFallidos += 1
                errores.Add($"Error al eliminar {item.Text}: {ex.Message}")
            End Try
        Next

        ' Mostrar resultado
        Dim mensajeResultado As String = String.Format(LanguageResources.GetResource(selectedLanguage, "MsgEliminacionCompletada"), vbCrLf, eliminadosExitosos, eliminadosFallidos)
        If errores.Count > 0 Then
            mensajeResultado += vbCrLf & vbCrLf & "Errores:" & vbCrLf & String.Join(vbCrLf, errores.Take(10))
            If errores.Count > 10 Then
                mensajeResultado += vbCrLf & $"... y {errores.Count - 10} error(es) más."
            End If
        End If

        MessageBox.Show(mensajeResultado, LanguageResources.GetResource(selectedLanguage, "MsgTituloResultadoEliminacion"), MessageBoxButtons.OK, MessageBoxIcon.Information)
        lblProgreso.Text = $"Eliminados: {eliminadosExitosos} | Fallidos: {eliminadosFallidos}"
    End Sub

    Private Sub ActualizarTamanioMiniaturas()
        ' Si ya está regenerando, actualizar solo el tamaño y salir
        If estaRegenerando Then
            imageListLarge.ImageSize = New System.Drawing.Size(tamanoMiniatura, tamanoMiniatura)
            Return
        End If

        ' Guardar estados actuales en el diccionario persistente
        estadosChecksPersistentes.Clear()
        If lvDuplicados.Items.Count > 0 Then
            For Each item As ListViewItem In lvDuplicados.Items
                If item.SubItems.Count > 1 Then
                    Dim rutaArchivo As String = item.SubItems(1).Text
                    If Not String.IsNullOrEmpty(rutaArchivo) Then
                        estadosChecksPersistentes(rutaArchivo) = item.Checked
                    End If
                End If
            Next
        End If

        ' Deshabilitar redibujado para evitar parpadeos
        lvDuplicados.BeginUpdate()

        ' Actualizar tamaño del ImageList
        imageListLarge.ImageSize = New System.Drawing.Size(tamanoMiniatura, tamanoMiniatura)
        imageListLarge.ColorDepth = ColorDepth.Depth32Bit
        imageListSmall.ImageSize = New System.Drawing.Size(32, 32)
        imageListSmall.ColorDepth = ColorDepth.Depth32Bit

        ' Asignar ImageLists al ListView
        lvDuplicados.LargeImageList = imageListLarge
        lvDuplicados.SmallImageList = imageListSmall

        ' Si hay items en el ListView, regenerar las miniaturas con el nuevo tamaño
        If lvDuplicados.Items.Count > 0 AndAlso lvDuplicados.View = View.LargeIcon Then
            RegenerarMiniaturas()
        End If

        ' Habilitar redibujado
        lvDuplicados.EndUpdate()
    End Sub

    Private Async Sub RegenerarMiniaturas()
        If lvDuplicados.Items.Count = 0 Then Return
        If estaRegenerando Then Return ' Evitar múltiples regeneraciones simultáneas

        estaRegenerando = True
        Try
            ' Limpiar ImageLists y caché
            imageListLarge.Images.Clear()
            imageListSmall.Images.Clear()
            imageCache.Clear()
            imageIndexCounter = 0

            Dim selectedLanguage As String = LanguageManager.GetSavedLanguage()
            lblProgreso.Text = LanguageResources.GetResource(selectedLanguage, "MsgRegenerandoMiniaturas").Replace("{0}", "0").Replace("{1}", "0")
            Application.DoEvents()

            ' Regenerar miniaturas para cada item
            Dim totalItems As Integer = lvDuplicados.Items.Count
            For i As Integer = 0 To totalItems - 1
                Dim item As ListViewItem = lvDuplicados.Items(i)
                If item.SubItems.Count > 1 Then
                    Dim rutaArchivo As String = item.SubItems(1).Text
                    If Not String.IsNullOrEmpty(rutaArchivo) AndAlso File.Exists(rutaArchivo) Then
                        Try
                            Dim imageIndex As Integer = Await ObtenerMiniaturaAsync(rutaArchivo)
                            item.ImageIndex = imageIndex
                        Catch
                        End Try
                    End If
                End If
                If i Mod 10 = 0 Then
                    Dim selectedLanguage2 As String = LanguageManager.GetSavedLanguage()
                    lblProgreso.Text = String.Format(LanguageResources.GetResource(selectedLanguage2, "MsgRegenerandoMiniaturas"), i + 1, totalItems)
                    Application.DoEvents()
                End If
            Next

            ' Restaurar todos los checks DESPUÉS de actualizar todas las miniaturas
            ' Usar el diccionario persistente que se actualiza antes de cada regeneración
            lvDuplicados.BeginUpdate()
            For Each item As ListViewItem In lvDuplicados.Items
                If item.SubItems.Count > 1 Then
                    Dim rutaArchivo As String = item.SubItems(1).Text
                    If Not String.IsNullOrEmpty(rutaArchivo) Then
                        Dim estadoCheck As Boolean
                        If estadosChecksPersistentes.TryGetValue(rutaArchivo, estadoCheck) Then
                            item.Checked = estadoCheck
                        End If
                    End If
                End If
            Next
            lvDuplicados.EndUpdate()

            Dim selectedLanguage3 As String = LanguageManager.GetSavedLanguage()
            lblProgreso.Text = LanguageResources.GetResource(selectedLanguage3, "MsgMiniaturasRegeneradas")
        Finally
            estaRegenerando = False
        End Try
    End Sub

    Private Sub LvDuplicados_ZoomChanged(sender As Object, delta As Integer) Handles lvDuplicados.ZoomChanged
        ' Delta positivo = rueda hacia arriba (aumentar)
        ' Delta negativo = rueda hacia abajo (reducir)
        If delta > 0 Then
            ' Aumentar tamaño
            If tamanoMiniatura < TAMANO_MAXIMO Then
                tamanoMiniatura = Math.Min(tamanoMiniatura + INCREMENTO, TAMANO_MAXIMO)
                ActualizarTamanioMiniaturas()
            End If
        Else
            ' Reducir tamaño
            If tamanoMiniatura > TAMANO_MINIMO Then
                tamanoMiniatura = Math.Max(tamanoMiniatura - INCREMENTO, TAMANO_MINIMO)
                ActualizarTamanioMiniaturas()
            End If
        End If
    End Sub
    
    Private Sub BtnIdioma_Click(sender As Object, e As EventArgs) Handles btnIdioma.Click
        ' Mostrar formulario de selección de idioma
        Using langForm As New Form2()
            If langForm.ShowDialog() = DialogResult.OK Then
                ' Recargar el idioma y aplicar al formulario inmediatamente
                Dim selectedLanguage As String = LanguageManager.GetSavedLanguage()
                LanguageManager.ApplyLanguage(Me, selectedLanguage)
                MessageBox.Show(LanguageResources.GetResource(selectedLanguage, "MsgIdiomaCambiado"), LanguageResources.GetResource(selectedLanguage, "MsgTituloIdiomaCambiado"), MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End Using
    End Sub
    
    Private Sub BtnSeleccionarTodos_Click(sender As Object, e As EventArgs) Handles btnSeleccionarTodos.Click
        lvDuplicados.BeginUpdate()
        For Each item As ListViewItem In lvDuplicados.Items
            item.Checked = True
        Next
        lvDuplicados.EndUpdate()
        ActualizarEstadisticas()
    End Sub
    
    Private Sub BtnDeseleccionarTodos_Click(sender As Object, e As EventArgs) Handles btnDeseleccionarTodos.Click
        lvDuplicados.BeginUpdate()
        For Each item As ListViewItem In lvDuplicados.Items
            item.Checked = False
        Next
        lvDuplicados.EndUpdate()
        ActualizarEstadisticas()
    End Sub
    
    Private Sub BtnInvertirSeleccion_Click(sender As Object, e As EventArgs) Handles btnInvertirSeleccion.Click
        lvDuplicados.BeginUpdate()
        For Each item As ListViewItem In lvDuplicados.Items
            item.Checked = Not item.Checked
        Next
        lvDuplicados.EndUpdate()
        ActualizarEstadisticas()
    End Sub
    
    Private Sub LvDuplicados_ItemChecked(sender As Object, e As ItemCheckedEventArgs) Handles lvDuplicados.ItemChecked
        ActualizarEstadisticas()
    End Sub
    
    Private Sub ActualizarEstadisticas()
        Dim totalArchivos As Integer = 0
        Dim espacioTotal As Long = 0
        
        For Each item As ListViewItem In lvDuplicados.Items
            If item.Checked AndAlso item.SubItems.Count > 1 Then
                Dim rutaArchivo As String = item.SubItems(1).Text
                If Not String.IsNullOrWhiteSpace(rutaArchivo) AndAlso File.Exists(rutaArchivo) Then
                    Try
                        Dim fi As New FileInfo(rutaArchivo)
                        espacioTotal += fi.Length
                        totalArchivos += 1
                    Catch
                    End Try
                End If
            End If
        Next
        
        Dim selectedLanguage As String = LanguageManager.GetSavedLanguage()
        If totalArchivos > 0 Then
            lblProgreso.Text = $"{totalArchivos} archivo(s) seleccionado(s) - {ConvertirTamanio(espacioTotal)} a liberar"
        Else
            lblProgreso.Text = ""
        End If
    End Sub
    
    Private Sub LvDuplicados_ColumnClick(sender As Object, e As ColumnClickEventArgs) Handles lvDuplicados.ColumnClick
        ' Ordenar por columna
        Dim sorter As ListViewItemComparer = TryCast(lvDuplicados.ListViewItemSorter, ListViewItemComparer)
        
        If sorter IsNot Nothing AndAlso sorter.Column = e.Column Then
            ' Invertir el orden si se hace clic en la misma columna
            sorter.Order = If(sorter.Order = SortOrder.Ascending, SortOrder.Descending, SortOrder.Ascending)
        Else
            ' Nueva columna, orden ascendente
            sorter = New ListViewItemComparer(e.Column, SortOrder.Ascending)
            lvDuplicados.ListViewItemSorter = sorter
        End If
        
        lvDuplicados.Sort()
    End Sub
End Class

' Clase auxiliar para ordenar el ListView
Public Class ListViewItemComparer
    Implements IComparer
    
    Public Property Column As Integer
    Public Property Order As SortOrder
    
    Public Sub New(column As Integer, order As SortOrder)
        Me.Column = column
        Me.Order = order
    End Sub
    
    Public Function Compare(x As Object, y As Object) As Integer Implements IComparer.Compare
        Dim item1 As ListViewItem = DirectCast(x, ListViewItem)
        Dim item2 As ListViewItem = DirectCast(y, ListViewItem)
        
        If item1.SubItems.Count <= Column OrElse item2.SubItems.Count <= Column Then
            Return 0
        End If
        
        Dim text1 As String = item1.SubItems(Column).Text
        Dim text2 As String = item2.SubItems(Column).Text
        
        ' Si es la columna de tamaño, comparar numéricamente
        If Column = 2 Then
            Try
                Dim size1 As Long = ObtenerTamanioEnBytes(text1)
                Dim size2 As Long = ObtenerTamanioEnBytes(text2)
                Dim resultado As Integer = size1.CompareTo(size2)
                Return If(Order = SortOrder.Ascending, resultado, -resultado)
            Catch
            End Try
        End If
        
        ' Comparación de texto
        Dim resultadoTexto As Integer = String.Compare(text1, text2, StringComparison.OrdinalIgnoreCase)
        Return If(Order = SortOrder.Ascending, resultadoTexto, -resultadoTexto)
    End Function
    
    Private Function ObtenerTamanioEnBytes(texto As String) As Long
        If String.IsNullOrEmpty(texto) Then Return 0
        texto = texto.Trim().ToUpper()
        Dim valor As Double = 0
        If texto.EndsWith(" KB") Then
            Double.TryParse(texto.Replace(" KB", ""), valor)
            Return CLng(valor * 1024)
        ElseIf texto.EndsWith(" MB") Then
            Double.TryParse(texto.Replace(" MB", ""), valor)
            Return CLng(valor * 1024 * 1024)
        ElseIf texto.EndsWith(" GB") Then
            Double.TryParse(texto.Replace(" GB", ""), valor)
            Return CLng(valor * 1024 * 1024 * 1024)
        End If
        Return 0
    End Function
End Class
