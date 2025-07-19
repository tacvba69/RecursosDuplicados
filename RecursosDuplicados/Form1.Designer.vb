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
        btnEliminarSeleccionados = New ToolStripButton()
        StatusStrip1 = New StatusStrip()
        ToolStripProgressBar1 = New ToolStripProgressBar()
        lblProgreso = New ToolStripStatusLabel()
        lvDuplicados = New ListView()
        ToolStripButton2 = New ToolStripButton()
        ToolStripSeparator2 = New ToolStripSeparator()
        ToolStrip1.SuspendLayout()
        StatusStrip1.SuspendLayout()
        SuspendLayout()
        ' 
        ' ToolStrip1
        ' 
        ToolStrip1.Items.AddRange(New ToolStripItem() {ToolStripButton1, ToolStripSeparator1, ToolStripButton2, ToolStripSeparator2, btnEliminarSeleccionados})
        ToolStrip1.Location = New Point(0, 0)
        ToolStrip1.Name = "ToolStrip1"
        ToolStrip1.Size = New Size(1000, 25)
        ToolStrip1.TabIndex = 2
        ToolStrip1.Text = "ToolStrip1"
        ' 
        ' ToolStripButton1
        ' 
        ToolStripButton1.DisplayStyle = ToolStripItemDisplayStyle.Image
        ToolStripButton1.Image = My.Resources.Resources.folder_explore
        ToolStripButton1.ImageTransparentColor = Color.Magenta
        ToolStripButton1.Name = "ToolStripButton1"
        ToolStripButton1.Size = New Size(23, 22)
        ToolStripButton1.Text = "Buscar"
        ' 
        ' ToolStripSeparator1
        ' 
        ToolStripSeparator1.Name = "ToolStripSeparator1"
        ToolStripSeparator1.Size = New Size(6, 25)
        ' 
        ' btnEliminarSeleccionados
        ' 
        btnEliminarSeleccionados.DisplayStyle = ToolStripItemDisplayStyle.Image
        btnEliminarSeleccionados.Image = My.Resources.Resources.delete_x16
        btnEliminarSeleccionados.ImageTransparentColor = Color.Magenta
        btnEliminarSeleccionados.Name = "btnEliminarSeleccionados"
        btnEliminarSeleccionados.Size = New Size(23, 22)
        btnEliminarSeleccionados.Text = "Eliminar Archivos Seleccionados"
        ' 
        ' StatusStrip1
        ' 
        StatusStrip1.Items.AddRange(New ToolStripItem() {ToolStripProgressBar1, lblProgreso})
        StatusStrip1.Location = New Point(0, 488)
        StatusStrip1.Name = "StatusStrip1"
        StatusStrip1.Size = New Size(1000, 22)
        StatusStrip1.TabIndex = 4
        StatusStrip1.Text = "StatusStrip1"
        ' 
        ' ToolStripProgressBar1
        ' 
        ToolStripProgressBar1.Name = "ToolStripProgressBar1"
        ToolStripProgressBar1.Size = New Size(200, 16)
        ' 
        ' lblProgreso
        ' 
        lblProgreso.AutoSize = False
        lblProgreso.Name = "lblProgreso"
        lblProgreso.Size = New Size(300, 17)
        lblProgreso.Text = "ToolStripStatusLabel1"
        lblProgreso.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' lvDuplicados
        ' 
        lvDuplicados.CheckBoxes = True
        lvDuplicados.Dock = DockStyle.Fill
        lvDuplicados.Location = New Point(0, 25)
        lvDuplicados.Name = "lvDuplicados"
        lvDuplicados.Size = New Size(1000, 463)
        lvDuplicados.TabIndex = 5
        lvDuplicados.UseCompatibleStateImageBehavior = False
        ' 
        ' ToolStripButton2
        ' 
        ToolStripButton2.DisplayStyle = ToolStripItemDisplayStyle.Image
        ToolStripButton2.Image = CType(resources.GetObject("ToolStripButton2.Image"), Image)
        ToolStripButton2.ImageTransparentColor = Color.Magenta
        ToolStripButton2.Name = "ToolStripButton2"
        ToolStripButton2.Size = New Size(23, 22)
        ToolStripButton2.Text = "ToolStripButton2"
        ' 
        ' ToolStripSeparator2
        ' 
        ToolStripSeparator2.Name = "ToolStripSeparator2"
        ToolStripSeparator2.Size = New Size(6, 25)
        ' 
        ' Form1
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1000, 510)
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
    Friend WithEvents lvDuplicados As ListView
    Friend WithEvents lblProgreso As ToolStripStatusLabel
    Friend WithEvents ToolStripButton2 As ToolStripButton
    Friend WithEvents ToolStripSeparator2 As ToolStripSeparator

End Class
