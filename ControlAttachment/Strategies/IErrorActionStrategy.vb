﻿Imports System.Windows.Forms

Namespace Strategies

    Public Interface IErrorActionStrategy
        Property Composit As IErrorActionStrategy
        Sub ErrorAction(control As Control)
        Sub SuccessAction(control As Control)
        Sub ErrorPainting(control As Control)
    End Interface
End Namespace