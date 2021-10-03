Option Explicit On
Option Strict On

Imports System.Drawing
Imports System.Windows.Forms

Namespace Decoration

    <Attachment(AllowMultiple:=False)>
    Public Class HoverActionAttachment
        Inherits NativeWindow

        Private _TargetControl As Control
        Public Property HoverColor As Color
        Public Property NormalColor As Color


        Public Sub New(targetControl As Control, hoverColor As Color)
            _TargetControl = targetControl
            Me.HoverColor = hoverColor
            Me.NormalColor = _TargetControl.BackColor

            AddHandler _TargetControl.HandleCreated, AddressOf OnHandleCreated
            AddHandler _TargetControl.HandleDestroyed, AddressOf OnHandleDestroyed

            AddHandler _TargetControl.MouseEnter, AddressOf OnMouseEnter
            AddHandler _TargetControl.MouseLeave, AddressOf OnMouseLeave

            If _TargetControl.IsHandleCreated Then
                AssignHandle(_TargetControl.Handle)
            End If

        End Sub

        Private Sub OnMouseLeave(sender As Object, e As EventArgs)
            _TargetControl.BackColor = NormalColor

        End Sub

        Private Sub OnMouseEnter(sender As Object, e As EventArgs)
            _TargetControl.BackColor = HoverColor

        End Sub

        Public Overrides Sub ReleaseHandle()
            RemoveHandler _TargetControl.HandleCreated, AddressOf OnHandleCreated
            RemoveHandler _TargetControl.HandleDestroyed, AddressOf OnHandleDestroyed
            RemoveHandler _TargetControl.MouseEnter, AddressOf OnMouseEnter
            RemoveHandler _TargetControl.MouseLeave, AddressOf OnMouseLeave
            _TargetControl = Nothing
            MyBase.ReleaseHandle()
        End Sub

        Private Sub OnHandleCreated(sender As Object, e As EventArgs)
            Dim ctrl = TryCast(sender, Control)
            AssignHandle(ctrl.Handle)
        End Sub

        Private Sub OnHandleDestroyed(sender As Object, e As EventArgs)
            ReleaseHandle()
        End Sub

        Protected Overrides Sub WndProc(ByRef m As Message)
            MyBase.WndProc(m)

        End Sub


    End Class
End Namespace