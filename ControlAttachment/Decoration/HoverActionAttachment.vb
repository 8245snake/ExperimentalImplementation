Option Explicit On
Option Strict On

Imports System.Drawing
Imports System.Windows.Forms

Namespace Decoration

    <Attachment(AllowMultiple:=False)>
    Friend Class HoverActionAttachment
        Inherits NativeWindow

        Private _TargetControl As Control
        Public Property HoverColor As Color
        Public Property NormalColor As Color

        Public Sub New(targetControl As Control, hoverColor As Color)
            _TargetControl = targetControl
            Me.HoverColor = hoverColor
            Me.NormalColor = _TargetControl.BackColor
            AddHandler _TargetControl.MouseEnter, AddressOf OnMouseEnter
            AddHandler _TargetControl.MouseLeave, AddressOf OnMouseLeave
            AddHandler _TargetControl.HandleDestroyed, AddressOf OnHandleDestroyed
        End Sub

        Private Sub OnMouseLeave(sender As Object, e As EventArgs)
            _TargetControl.BackColor = NormalColor
        End Sub

        Private Sub OnMouseEnter(sender As Object, e As EventArgs)
            _TargetControl.BackColor = HoverColor
        End Sub

        Public Overrides Sub ReleaseHandle()
            RemoveHandler _TargetControl.HandleDestroyed, AddressOf OnHandleDestroyed
            RemoveHandler _TargetControl.MouseEnter, AddressOf OnMouseEnter
            RemoveHandler _TargetControl.MouseLeave, AddressOf OnMouseLeave
            _TargetControl = Nothing
            MyBase.ReleaseHandle()
        End Sub

        Private Sub OnHandleDestroyed(sender As Object, e As EventArgs)
            ReleaseHandle()
        End Sub

    End Class
End Namespace