
Imports ControlAttachment.Validation

Namespace Strategies

    Public Class ErrorMessageActionStrategy
        Implements IErrorActionStrategy

        Public Property Composit As IErrorActionStrategy Implements IErrorActionStrategy.Composit

        Public Sub ErrorAction(control As Control, result As ValidationResult) Implements IErrorActionStrategy.ErrorAction
            MessageBox.Show(result.Message)
        End Sub
        Public Sub SuccessAction(control As Control, result As ValidationResult) Implements IErrorActionStrategy.SuccessAction
        End Sub

        Public Sub ErrorPainting(control As Control) Implements IErrorActionStrategy.ErrorPainting
        End Sub
    End Class
End Namespace