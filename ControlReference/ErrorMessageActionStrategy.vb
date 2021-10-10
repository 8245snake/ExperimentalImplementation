Imports ControlAttachment.Strategies

Public Class ErrorMessageActionStrategy
    Implements IErrorActionStrategy

    Public Sub BeginHighlight(control As Control) Implements IHighlightingActionStrategy.BeginHighlight
        Throw New NotImplementedException
    End Sub

    Public Sub EndHighlight(control As Control) Implements IHighlightingActionStrategy.EndHighlight
        Throw New NotImplementedException
    End Sub

    Public Sub Highlight(control As Control) Implements IHighlightingActionStrategy.Highlight
        Throw New NotImplementedException
    End Sub

    Public Sub ErrorAction(control As Control) Implements IErrorActionStrategy.ErrorAction
        MessageBox.Show("文字数超過")
    End Sub

    Public Sub SuccessAction(control As Control) Implements IErrorActionStrategy.SuccessAction
        Throw New NotImplementedException
    End Sub

    Public Sub ErrorPainting(control As Control) Implements IErrorActionStrategy.ErrorPainting
        Throw New NotImplementedException
    End Sub
End Class