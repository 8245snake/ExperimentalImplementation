
Imports ControlAttachment.Validation

Namespace Strategies

    Public Class LabelWritingErrorActionStrategy
        Implements IErrorActionStrategy

        Private _Label As Label

        Public Sub New(label As Label)
            _Label = label
        End Sub

        Public Property Composit As IErrorActionStrategy Implements IErrorActionStrategy.Composit

        Public Sub ErrorAction(control As Control, result As ValidationResult) Implements IErrorActionStrategy.ErrorAction
            _Label.Text = result.Message
            _Label.Visible = True
        End Sub

        Public Sub SuccessAction(control As Control, result As ValidationResult) Implements IErrorActionStrategy.SuccessAction
            _Label.Visible = False
        End Sub

        Public Sub ErrorPainting(control As Control) Implements IErrorActionStrategy.ErrorPainting
        End Sub
    End Class
End Namespace