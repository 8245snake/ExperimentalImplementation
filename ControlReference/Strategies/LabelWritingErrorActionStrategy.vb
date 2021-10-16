Imports ControlAttachment.Strategies

Namespace Strategies

    Public Class LabelWritingErrorActionStrategy
        Implements IErrorActionStrategy

        Private _Label As Label
        Private _Message As String

        Public Sub New(label As Label, message As String)
            _Label = label
            _Message = message
        End Sub

        Public Property Composit As IErrorActionStrategy Implements IErrorActionStrategy.Composit

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
End Namespace