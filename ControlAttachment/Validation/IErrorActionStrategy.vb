Imports System.Windows.Forms

Namespace Validation

    Public Interface IErrorActionStrategy
        Property Composit As IErrorActionStrategy
        Sub ErrorAction(control As Control, result As ValidationResult)
        Sub SuccessAction(control As Control, result As ValidationResult)
        Sub ErrorPainting(control As Control)
    End Interface
End Namespace