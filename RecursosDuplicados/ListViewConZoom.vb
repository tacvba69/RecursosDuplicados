Imports System.Windows.Forms
Imports System.Runtime.InteropServices

' ListView personalizado para interceptar Ctrl + rueda del mouse
Public Class ListViewConZoom
    Inherits ListView

    Public Event ZoomChanged As EventHandler(Of Integer)

    <DllImport("user32.dll")>
    Private Shared Function GetKeyState(nVirtKey As Integer) As Short
    End Function

    Private Const VK_CONTROL As Integer = &H11

    Protected Overrides Sub WndProc(ByRef m As Message)
        Const WM_MOUSEWHEEL As Integer = &H20A
        If m.Msg = WM_MOUSEWHEEL Then
            ' Verificar si Ctrl está presionado usando GetKeyState
            Dim ctrlPressed As Boolean = (GetKeyState(VK_CONTROL) And &H8000) <> 0
            If ctrlPressed AndAlso Me.View = View.LargeIcon Then
                ' Obtener el delta de la rueda de forma segura
                ' El delta está en los 16 bits superiores de WParam como signed value
                Dim wParamValue As Long = m.WParam.ToInt64()
                ' Extraer los 16 bits superiores
                Dim deltaUnsigned As UInteger = CUInt((wParamValue >> 16) And &HFFFFL)
                ' Convertir a signed integer de forma segura
                Dim delta As Integer
                If deltaUnsigned > 32767UI Then
                    ' Es negativo en complemento a 2
                    delta = CInt(deltaUnsigned) - 65536
                Else
                    ' Es positivo
                    delta = CInt(deltaUnsigned)
                End If
                RaiseEvent ZoomChanged(Me, delta)
                Return ' Consumir el mensaje para que no se propague
            End If
        End If
        MyBase.WndProc(m)
    End Sub
End Class

