Imports System.IO
Imports System.Security.Cryptography
Imports System.Linq
Imports System.Threading.Tasks

Public Class Form1

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lvDuplicados.View = View.Details
        lvDuplicados.CheckBoxes = True
        lvDuplicados.Columns.Add("Archivo", 500)
        lvDuplicados.Columns.Add("Ruta", 700)
        lvDuplicados.Columns.Add("Tamaño", 100)
        lvDuplicados.Columns.Add("Tipo", 100)
        lvDuplicados.FullRowSelect = True
        lvDuplicados.GridLines = True


        ToolStripProgressBar1.Visible = False
        lblProgreso.Text = ""
    End Sub

    Private Async Sub BuscarDuplicados()
        Using folderDlg As New FolderBrowserDialog()
            If folderDlg.ShowDialog() = DialogResult.OK Then
                Dim rutaCarpeta As String = folderDlg.SelectedPath
                lvDuplicados.Items.Clear()

                ToolStripProgressBar1.Visible = True
                ToolStripProgressBar1.Value = 0
                lblProgreso.Text = "Iniciando análisis..."

                Dim duplicados As List(Of List(Of String)) = Await Task.Run(Function() ObtenerDuplicadosConProgreso(rutaCarpeta))

                Dim contadorImg As Integer = 0
                If duplicados.Count = 0 Then
                    lblProgreso.Text = "No se encontraron duplicados."
                    MessageBox.Show("No se encontraron duplicados.")
                Else
                    For Each grupo As List(Of String) In duplicados
                        Dim copiasPermitidas As Integer = grupo.Count - 1
                        Dim contadorCheck As Integer = 0
                        For Each archivo As String In grupo
                            Try
                                Dim fi As New FileInfo(archivo)
                                Dim nombre = Path.GetFileName(archivo)
                                Dim item As New ListViewItem(nombre)
                                item.SubItems.Add(archivo)
                                item.SubItems.Add(ConvertirTamanio(fi.Length))
                                item.SubItems.Add(fi.Extension.ToLower())

                                Try
                                    Dim ext As String = fi.Extension.ToLower()
                                    If ext = ".jpg" OrElse ext = ".jpeg" OrElse ext = ".png" OrElse ext = ".bmp" OrElse ext = ".gif" Then
                                        Dim imgBytes As Byte() = File.ReadAllBytes(archivo)
                                        Using ms As New MemoryStream(imgBytes)
                                            Dim img As Image = Image.FromStream(ms)
                                            item.ImageKey = contadorImg.ToString()
                                            contadorImg += 1
                                        End Using
                                    End If
                                Catch ex As Exception
                                    ' Ignorar imágenes no válidas
                                End Try

                                If contadorCheck < copiasPermitidas Then
                                    item.Checked = True
                                    contadorCheck += 1
                                End If

                                lvDuplicados.Items.Add(item)
                            Catch ex As Exception
                                ' Ignorar error
                            End Try
                        Next
                        Dim separador As New ListViewItem("---")
                        separador.ForeColor = Color.Gray
                        separador.Checked = False
                        separador.BackColor = Color.LightGray
                        separador.SubItems.Add("")
                        separador.SubItems.Add("")
                        separador.SubItems.Add("")
                        lvDuplicados.Items.Add(separador)
                    Next
                    lblProgreso.Text = $"Se encontraron {duplicados.Count} grupos de archivos duplicados."
                End If

                lvDuplicados.View = View.LargeIcon
                ToolStripProgressBar1.Visible = False
            End If
        End Using
    End Sub

    Private Function ObtenerDuplicadosConProgreso(ruta As String) As List(Of List(Of String))
        Dim archivos As String() = Directory.GetFiles(ruta, "*.*", SearchOption.AllDirectories)
        Dim hashDic As New Dictionary(Of String, List(Of String))()
        Dim total As Integer = archivos.Length

        Invoke(Sub()
                   ToolStripProgressBar1.Maximum = total
               End Sub)

        For i As Integer = 0 To archivos.Length - 1
            Try
                Using fs As FileStream = File.OpenRead(archivos(i))
                    Dim md5 As MD5 = MD5.Create()
                    Dim hash As String = BitConverter.ToString(md5.ComputeHash(fs)).Replace("-", "").ToLower()
                    If Not hashDic.ContainsKey(hash) Then
                        hashDic(hash) = New List(Of String)()
                    End If
                    hashDic(hash).Add(archivos(i))
                End Using
            Catch ex As Exception
                ' Ignorar errores de lectura
            End Try

            Dim iCopy = i
            Dim gruposEncontrados = hashDic.Values.Where(Function(l) l.Count > 1).Count()
            Invoke(Sub()
                       ToolStripProgressBar1.Value = iCopy + 1
                       lblProgreso.Text = $"Revisando archivo {iCopy + 1} de {archivos.Length}... Grupos encontrados: {gruposEncontrados}"
                   End Sub)
        Next

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

    ' Botón buscar
    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click
        BuscarDuplicados()
    End Sub


    Private Sub lvDuplicados_DoubleClick(sender As Object, e As EventArgs) Handles lvDuplicados.DoubleClick
        If lvDuplicados.SelectedItems.Count > 0 Then
            Dim ruta As String = lvDuplicados.SelectedItems(0).SubItems(1).Text
            If File.Exists(ruta) Then
                Process.Start("explorer.exe", "/select," & ruta)
            End If
        End If
    End Sub

    Private Sub ToolStripButton2_Click(sender As Object, e As EventArgs) Handles ToolStripButton2.Click
        lvDuplicados.View = View.Details
    End Sub
End Class
