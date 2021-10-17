Option Explicit On
Option Strict On

Imports System.Drawing
Imports System.Windows.Forms
Imports ControlAttachment.State
Imports ControlAttachment.Validation

Namespace Strategies
    Public Class FillActionStrategy
        Implements IErrorActionStrategy, IHighlightingActionStrategy

        Public Property Composit As IErrorActionStrategy Implements IErrorActionStrategy.Composit

        Public Property ErrorColor As Color

        ' インスタンスを使い回すことを想定してデフォルト背景色とハンドルをセットで持っておく
        Private Handle As IntPtr
        Private NormalColor As Color = Color.Empty


        Public Sub New()
            ' デフォルトは薄い赤
            ErrorColor = ColorTranslator.FromHtml("#FFFF9999")
        End Sub

        Public Sub New(errorColor As Color)
            Me.ErrorColor = errorColor
        End Sub

        Public Sub New(composit As IErrorActionStrategy)
            MyClass.New()
            Me.Composit = composit
        End Sub

        Public Sub New(fillColor As Color, composit As IErrorActionStrategy)
            MyClass.New(fillColor)
            Me.Composit = composit
        End Sub

        Public Sub ErrorAction(control As Control) Implements IErrorActionStrategy.ErrorAction
            If NormalColor = Color.Empty OrElse control.Handle <> Handle Then
                ' 通常時の背景色を保持しておく
                NormalColor = control.BackColor
                Handle = control.Handle
            End If
            control.BackColor = ErrorColor
        End Sub

        Public Sub SuccessAction(control As Control) Implements IErrorActionStrategy.SuccessAction
            control.BackColor = NormalColor
        End Sub

        Public Sub ErrorPainting(control As Control) Implements IErrorActionStrategy.ErrorPainting
        End Sub

        Public Sub BeginHighlight(control As Control) Implements IHighlightingActionStrategy.BeginHighlight
            ErrorAction(control)
        End Sub

        Public Sub EndHighlight(control As Control) Implements IHighlightingActionStrategy.EndHighlight
            SuccessAction(control)
        End Sub

        Public Sub Highlight(control As Control) Implements IHighlightingActionStrategy.Highlight
        End Sub
    End Class
End Namespace