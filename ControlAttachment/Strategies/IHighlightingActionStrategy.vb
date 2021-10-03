Imports System.Windows.Forms

Namespace Strategies
    Public Interface IHighlightingActionStrategy
        Sub BeginHighlight(control As Control)
        Sub EndHighlight(control As Control)
        Sub Highlight(control As Control)

    End Interface
End Namespace