<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form2
    Inherits System.Windows.Forms.Form

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

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        lblTitulo = New System.Windows.Forms.Label()
        lblSeleccionar = New System.Windows.Forms.Label()
        cmbIdioma = New System.Windows.Forms.ComboBox()
        btnAceptar = New System.Windows.Forms.Button()
        btnCancelar = New System.Windows.Forms.Button()
        SuspendLayout()
        ' 
        ' lblTitulo
        ' 
        lblTitulo.AutoSize = True
        lblTitulo.Font = New System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold)
        lblTitulo.Location = New System.Drawing.Point(20, 20)
        lblTitulo.Name = "lblTitulo"
        lblTitulo.Size = New System.Drawing.Size(200, 21)
        lblTitulo.TabIndex = 0
        lblTitulo.Text = "Seleccionar Idioma"
        ' 
        ' lblSeleccionar
        ' 
        lblSeleccionar.AutoSize = True
        lblSeleccionar.Location = New System.Drawing.Point(20, 60)
        lblSeleccionar.Name = "lblSeleccionar"
        lblSeleccionar.Size = New System.Drawing.Size(120, 15)
        lblSeleccionar.TabIndex = 1
        lblSeleccionar.Text = "Seleccione un idioma:"
        ' 
        ' cmbIdioma
        ' 
        cmbIdioma.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        cmbIdioma.FormattingEnabled = True
        cmbIdioma.Location = New System.Drawing.Point(20, 85)
        cmbIdioma.Name = "cmbIdioma"
        cmbIdioma.Size = New System.Drawing.Size(300, 30)
        cmbIdioma.TabIndex = 2
        ' 
        ' btnAceptar
        ' 
        btnAceptar.Location = New System.Drawing.Point(164, 130)
        btnAceptar.Name = "btnAceptar"
        btnAceptar.Size = New System.Drawing.Size(75, 23)
        btnAceptar.TabIndex = 3
        btnAceptar.Text = "Aceptar"
        btnAceptar.UseVisualStyleBackColor = True
        ' 
        ' btnCancelar
        ' 
        btnCancelar.Location = New System.Drawing.Point(245, 130)
        btnCancelar.Name = "btnCancelar"
        btnCancelar.Size = New System.Drawing.Size(75, 23)
        btnCancelar.TabIndex = 4
        btnCancelar.Text = "Cancelar"
        btnCancelar.UseVisualStyleBackColor = True
        ' 
        ' Form2
        ' 
        AutoScaleDimensions = New System.Drawing.SizeF(7F, 15F)
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        ClientSize = New System.Drawing.Size(340, 170)
        Controls.Add(btnCancelar)
        Controls.Add(btnAceptar)
        Controls.Add(cmbIdioma)
        Controls.Add(lblSeleccionar)
        Controls.Add(lblTitulo)
        FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        MaximizeBox = False
        MinimizeBox = False
        Name = "Form2"
        StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Text = "Seleccionar Idioma"
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents lblTitulo As System.Windows.Forms.Label
    Friend WithEvents lblSeleccionar As System.Windows.Forms.Label
    Friend WithEvents cmbIdioma As System.Windows.Forms.ComboBox
    Friend WithEvents btnAceptar As System.Windows.Forms.Button
    Friend WithEvents btnCancelar As System.Windows.Forms.Button
End Class

