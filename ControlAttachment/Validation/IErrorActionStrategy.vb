Imports System.Windows.Forms
Imports ControlAttachment.State

Namespace Validation

    Public Interface IErrorActionStrategy
        Inherits IHighlightingActionStrategy

        Sub ErrorAction(control As Control)
        Sub SuccessAction(control As Control)
        Sub ErrorPainting(control As Control)
    End Interface
End Namespace