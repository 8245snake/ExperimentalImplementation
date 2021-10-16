Imports ControlAttachment.Strategies

Public Class LabelWritingErrorActionStrategy
    Implements IErrorActionStrategy

    Private _Label As Label
    Private _Message As String

    Public Sub New(label As Label, message As String)
        _Label = label
        _Message = message
    End Sub

    Public Sub BeginHighlight(control As Control) Implements IHighlightingActionStrategy.BeginHighlight
    End Sub

    Public Sub EndHighlight(control As Control) Implements IHighlightingActionStrategy.EndHighlight
    End Sub

    Public Sub Highlight(control As Control) Implements IHighlightingActionStrategy.Highlight
    End Sub

    Public Sub ErrorAction(control As Control) Implements IErrorActionStrategy.ErrorAction
        _Label.Text = _Message
        _Label.Visible = True
    End Sub

    Public Sub SuccessAction(control As Control) Implements IErrorActionStrategy.SuccessAction
        _Label.Visible = False
    End Sub

    Public Sub ErrorPainting(control As Control) Implements IErrorActionStrategy.ErrorPainting
    End Sub
End Class