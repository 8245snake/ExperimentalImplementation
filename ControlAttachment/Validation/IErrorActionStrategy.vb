Imports System.Windows.Forms

Namespace Validation

    Public Interface IErrorActionStrategy
        Sub ErrorAction(control As Control)
        Sub SuccessAction(control As Control)
        Sub ErrorPainting(control As Control)
    End Interface
End Namespace