
Imports ControlAttachment.Validation

Namespace Strategies

    Public Class ErrorMessageActionStrategy
        Implements IErrorActionStrategy

        Private _Message As String

        Public Sub New(message As String)
            _Message = message
        End Sub


        Public Property Composit As IErrorActionStrategy Implements IErrorActionStrategy.Composit

        Public Sub ErrorAction(control As Control) Implements IErrorActionStrategy.ErrorAction
            MessageBox.Show(_Message)
        End Sub
        Public Sub SuccessAction(control As Control) Implements IErrorActionStrategy.SuccessAction
        End Sub

        Public Sub ErrorPainting(control As Control) Implements IErrorActionStrategy.ErrorPainting
        End Sub
    End Class
End Namespace