Option Explicit On
Option Strict On

Imports System.Drawing
Imports System.Windows.Forms

Namespace Activity
    Public Class DropEventArgs
        Inherits EventArgs

        Public DropedControl As Control
        Public DropedPosition As Point

        Public Sub New()
        End Sub

        Public Sub New(dropedControl As Control, dropedPosition As Point)
            Me.DropedControl = dropedControl
            Me.DropedPosition = dropedPosition
        End Sub

    End Class
End Namespace