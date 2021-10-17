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

        Public Property FillBrush As Brush

        Public Sub New()
            ' デフォルトは薄い赤
            FillBrush = New SolidBrush(ColorTranslator.FromHtml("#22FF0000"))
        End Sub

        Public Sub New(fillColor As Color)
            FillBrush = New SolidBrush(fillColor)
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
        End Sub

        Public Sub SuccessAction(control As Control) Implements IErrorActionStrategy.SuccessAction
        End Sub

        Public Sub ErrorPainting(control As Control) Implements IErrorActionStrategy.ErrorPainting
            'g.FillRectangle(FillBrush, 0, 0, control.Width, control.Height)
        End Sub

        Public Sub BeginHighlight(control As Control) Implements IHighlightingActionStrategy.BeginHighlight
        End Sub

        Public Sub EndHighlight(control As Control) Implements IHighlightingActionStrategy.EndHighlight
        End Sub

        Public Sub Highlight(control As Control) Implements IHighlightingActionStrategy.Highlight
        End Sub
    End Class
End Namespace