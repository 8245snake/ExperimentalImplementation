Option Explicit On
Option Strict On

Imports System.Windows.Forms
Imports ControlAttachment.Strategies

Namespace State

    ''' <summary>
    ''' コントロールをハイライトする機能を提供する
    ''' </summary>
    Public Class HighlightingAttachment
        Inherits NativeWindow
        Implements IHighlightingActionStrategy

        Private _TargetControl As Control

        Private _DrawActionStrategy As IHighlightingActionStrategy

        Private Const WM_PAINT = &HF

        Public Sub New(Optional isBlinkEnable As Boolean = False)
            _DrawActionStrategy = New BorderDrawActionStrategy(isBlinkEnable)
        End Sub

        Public Sub New(drawActionStrategy As IHighlightingActionStrategy)
            _DrawActionStrategy = drawActionStrategy
        End Sub

        Private Sub OnHandleDestroyed(sender As Object, e As EventArgs)
            ReleaseHandle()
        End Sub

        Public Overrides Sub ReleaseHandle()
            RemoveHandler _TargetControl.HandleDestroyed, AddressOf OnHandleDestroyed
            _TargetControl = Nothing

            MyBase.ReleaseHandle()
        End Sub

        Protected Overrides Sub WndProc(ByRef m As Message)
            MyBase.WndProc(m)

            If m.Msg = WM_PAINT Then
                Highlight(_TargetControl)
            End If

        End Sub

        Public Sub BeginHighlight(control As Control) Implements IHighlightingActionStrategy.BeginHighlight
            _TargetControl = control
            AddHandler _TargetControl.HandleDestroyed, AddressOf OnHandleDestroyed

            If _TargetControl.IsHandleCreated Then
                AssignHandle(_TargetControl.Handle)
                _DrawActionStrategy.BeginHighlight(control)
                control.Parent.Refresh()
            End If

        End Sub

        Public Sub EndHighlight(control As Control) Implements IHighlightingActionStrategy.EndHighlight
            _DrawActionStrategy.EndHighlight(control)
            ReleaseHandle()
        End Sub

        Public Sub Highlight(control As Control) Implements IHighlightingActionStrategy.Highlight
            _DrawActionStrategy.Highlight(_TargetControl)
        End Sub

    End Class
End Namespace