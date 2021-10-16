﻿Imports ControlAttachment.Strategies

Public Class ErrorMessageActionStrategy
    Implements IErrorActionStrategy

    Private _Message As String

    Public Sub New(message As String)
        _Message = message
    End Sub

    Public Sub BeginHighlight(control As Control) Implements IHighlightingActionStrategy.BeginHighlight
    End Sub

    Public Sub EndHighlight(control As Control) Implements IHighlightingActionStrategy.EndHighlight
    End Sub

    Public Sub Highlight(control As Control) Implements IHighlightingActionStrategy.Highlight
    End Sub

    Public Sub ErrorAction(control As Control) Implements IErrorActionStrategy.ErrorAction
        MessageBox.Show(_Message)
    End Sub
    Public Sub SuccessAction(control As Control) Implements IErrorActionStrategy.SuccessAction
    End Sub

    Public Sub ErrorPainting(control As Control) Implements IErrorActionStrategy.ErrorPainting
    End Sub
End Class