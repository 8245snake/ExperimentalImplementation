﻿Imports System.Windows.Forms

Namespace State
    Public Interface IHighlightingActionStrategy
        Sub BeginHighlight(control As Control)
        Sub EndHighlight(control As Control)
        Sub Highlight(control As Control)

    End Interface
End Namespace