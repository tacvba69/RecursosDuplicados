<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        FolderBrowserDialog1 = New FolderBrowserDialog()
        ToolStrip1 = New ToolStrip()
        ToolStripButton1 = New ToolStripButton()
        ToolStripSeparator1 = New ToolStripSeparator()
        btnVistaIconos = New ToolStripButton()
        btnVistaDetalles = New ToolStripButton()
        ToolStripSeparator2 = New ToolStripSeparator()
        btnEliminarSeleccionados = New ToolStripButton()
        ToolStripSeparator3 = New ToolStripSeparator()
        btnSeleccionarTodos = New ToolStripButton()
        btnDeseleccionarTodos = New ToolStripButton()
        btnInvertirSeleccion = New ToolStripButton()
        ToolStripSeparator4 = New ToolStripSeparator()
        btnIdioma = New ToolStripButton()
        StatusStrip1 = New StatusStrip()
        ToolStripProgressBar1 = New ToolStripProgressBar()
        lblProgreso = New ToolStripStatusLabel()
        lvDuplicados = New ListViewConZoom()
        ToolStrip1.SuspendLayout()
        StatusStrip1.SuspendLayout()
        SuspendLayout()
        ' 
        ' ToolStrip1
        ' 
        ToolStrip1.Items.AddRange(New ToolStripItem() {ToolStripButton1, ToolStripSeparator1, btnVistaIconos, btnVistaDetalles, ToolStripSeparator2, btnEliminarSeleccionados, ToolStripSeparator3, btnSeleccionarTodos, btnDeseleccionarTodos, btnInvertirSeleccion, ToolStripSeparator4, btnIdioma})
        ToolStrip1.Location = New Point(0, 0)
        ToolStrip1.Name = "ToolStrip1"
        ToolStrip1.Size = New System.Drawing.Size(1000, 25)
        ToolStrip1.TabIndex = 2
        ToolStrip1.Text = "ToolStrip1"
        ' 
        ' ToolStripButton1
        ' 
        ToolStripButton1.DisplayStyle = ToolStripItemDisplayStyle.Image
        ToolStripButton1.Image = My.Resources.Resources.folder_explore
        ToolStripButton1.ImageTransparentColor = Color.Magenta
        ToolStripButton1.Name = "ToolStripButton1"
        ToolStripButton1.Size = New System.Drawing.Size(23, 22)
        ToolStripButton1.Text = "Buscar"
        ' 
        ' ToolStripSeparator1
        ' 
        ToolStripSeparator1.Name = "ToolStripSeparator1"
        ToolStripSeparator1.Size = New System.Drawing.Size(6, 25)
        ' 
        ' btnVistaIconos
        ' 
        btnVistaIconos.CheckOnClick = True
        btnVistaIconos.DisplayStyle = ToolStripItemDisplayStyle.Image
        btnVistaIconos.Image = My.Resources.Resources.application_view_icons
        btnVistaIconos.ImageTransparentColor = Color.Magenta
        btnVistaIconos.Name = "btnVistaIconos"
        btnVistaIconos.Size = New System.Drawing.Size(23, 22)
        btnVistaIconos.Text = "Vista Iconos"
        ' 
        ' btnVistaDetalles
        ' 
        btnVistaDetalles.CheckOnClick = True
        btnVistaDetalles.DisplayStyle = ToolStripItemDisplayStyle.Image
        btnVistaDetalles.Image = My.Resources.Resources.application_view_detail
        btnVistaDetalles.ImageTransparentColor = Color.Magenta
        btnVistaDetalles.Name = "btnVistaDetalles"
        btnVistaDetalles.Size = New System.Drawing.Size(23, 22)
        btnVistaDetalles.Text = "Vista Detalles"
        ' 
        ' ToolStripSeparator2
        ' 
        ToolStripSeparator2.Name = "ToolStripSeparator2"
        ToolStripSeparator2.Size = New System.Drawing.Size(6, 25)
        ' 
        ' btnEliminarSeleccionados
        ' 
        btnEliminarSeleccionados.DisplayStyle = ToolStripItemDisplayStyle.Image
        btnEliminarSeleccionados.Image = My.Resources.Resources.delete_x16
        btnEliminarSeleccionados.ImageTransparentColor = Color.Magenta
        btnEliminarSeleccionados.Name = "btnEliminarSeleccionados"
        btnEliminarSeleccionados.Size = New System.Drawing.Size(23, 22)
        btnEliminarSeleccionados.Text = "Eliminar Archivos Seleccionados"
        ' 
        ' ToolStripSeparator3
        ' 
        ToolStripSeparator3.Name = "ToolStripSeparator3"
        ToolStripSeparator3.Size = New System.Drawing.Size(6, 25)
        ' 
        ' btnSeleccionarTodos
        ' 
        btnSeleccionarTodos.DisplayStyle = ToolStripItemDisplayStyle.Image
        btnSeleccionarTodos.ImageTransparentColor = Color.Magenta
        btnSeleccionarTodos.Name = "btnSeleccionarTodos"
        btnSeleccionarTodos.Size = New System.Drawing.Size(23, 22)
        btnSeleccionarTodos.Text = "Seleccionar Todos"
        ' 
        ' btnDeseleccionarTodos
        ' 
        btnDeseleccionarTodos.DisplayStyle = ToolStripItemDisplayStyle.Image
        btnDeseleccionarTodos.ImageTransparentColor = Color.Magenta
        btnDeseleccionarTodos.Name = "btnDeseleccionarTodos"
        btnDeseleccionarTodos.Size = New System.Drawing.Size(23, 22)
        btnDeseleccionarTodos.Text = "Deseleccionar Todos"
        ' 
        ' btnInvertirSeleccion
        ' 
        btnInvertirSeleccion.DisplayStyle = ToolStripItemDisplayStyle.Image
        btnInvertirSeleccion.ImageTransparentColor = Color.Magenta
        btnInvertirSeleccion.Name = "btnInvertirSeleccion"
        btnInvertirSeleccion.Size = New System.Drawing.Size(23, 22)
        btnInvertirSeleccion.Text = "Invertir Selección"
        ' 
        ' ToolStripSeparator4
        ' 
        ToolStripSeparator4.Name = "ToolStripSeparator4"
        ToolStripSeparator4.Size = New System.Drawing.Size(6, 25)
        ' 
        ' btnIdioma
        ' 
        btnIdioma.DisplayStyle = ToolStripItemDisplayStyle.Image
        btnIdioma.Image = My.Resources.Resources.application
        btnIdioma.ImageTransparentColor = Color.Magenta
        btnIdioma.Name = "btnIdioma"
        btnIdioma.Size = New System.Drawing.Size(23, 22)
        btnIdioma.Text = "Idioma"
        ' 
        ' StatusStrip1
        ' 
        StatusStrip1.Items.AddRange(New ToolStripItem() {ToolStripProgressBar1, lblProgreso})
        StatusStrip1.Location = New Point(0, 488)
        StatusStrip1.Name = "StatusStrip1"
        StatusStrip1.Size = New System.Drawing.Size(1000, 22)
        StatusStrip1.TabIndex = 4
        StatusStrip1.Text = "StatusStrip1"
        ' 
        ' ToolStripProgressBar1
        ' 
        ToolStripProgressBar1.Name = "ToolStripProgressBar1"
        ToolStripProgressBar1.Size = New System.Drawing.Size(200, 16)
        ' 
        ' lblProgreso
        ' 
        lblProgreso.AutoSize = False
        lblProgreso.Name = "lblProgreso"
        lblProgreso.Spring = True
        lblProgreso.Text = "ToolStripStatusLabel1"
        lblProgreso.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' lvDuplicados
        ' 
        lvDuplicados.CheckBoxes = True
        lvDuplicados.Dock = DockStyle.Fill
        lvDuplicados.Location = New Point(0, 25)
        lvDuplicados.Name = "lvDuplicados"
        lvDuplicados.Size = New System.Drawing.Size(1000, 463)
        lvDuplicados.TabIndex = 5
        lvDuplicados.UseCompatibleStateImageBehavior = False
        ' 
        ' Form1
        ' 
        AutoScaleDimensions = New SizeF(7.0F, 15.0F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New System.Drawing.Size(1000, 510)
        Controls.Add(lvDuplicados)
        Controls.Add(StatusStrip1)
        Controls.Add(ToolStrip1)
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Name = "Form1"
        Text = "Borrar Archivos Duplicados"
        WindowState = FormWindowState.Maximized
        ToolStrip1.ResumeLayout(False)
        ToolStrip1.PerformLayout()
        StatusStrip1.ResumeLayout(False)
        StatusStrip1.PerformLayout()
        ResumeLayout(False)
        PerformLayout()
    End Sub
    Friend WithEvents FolderBrowserDialog1 As FolderBrowserDialog
    Friend WithEvents ToolStrip1 As ToolStrip
    Friend WithEvents ToolStripButton1 As ToolStripButton
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents btnEliminarSeleccionados As ToolStripButton
    Friend WithEvents StatusStrip1 As StatusStrip
    Friend WithEvents ToolStripProgressBar1 As ToolStripProgressBar
    Friend WithEvents lvDuplicados As ListViewConZoom
    Friend WithEvents lblProgreso As ToolStripStatusLabel
    Friend WithEvents btnVistaIconos As ToolStripButton
    Friend WithEvents btnVistaDetalles As ToolStripButton
    Friend WithEvents ToolStripSeparator2 As ToolStripSeparator
    Friend WithEvents ToolStripSeparator3 As ToolStripSeparator
    Friend WithEvents btnSeleccionarTodos As ToolStripButton
    Friend WithEvents btnDeseleccionarTodos As ToolStripButton
    Friend WithEvents btnInvertirSeleccion As ToolStripButton
    Friend WithEvents ToolStripSeparator4 As ToolStripSeparator
    Friend WithEvents btnIdioma As ToolStripButton

End Class
