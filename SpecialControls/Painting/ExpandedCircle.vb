Option Explicit On
Option Strict On

Namespace Painting

    ''' <summary>
    ''' 伸びた円の描画用オブジェクト
    ''' </summary>
    Public Class ExpandedCircle
        Inherits Shape

        Public Overrides Sub Draw(ByRef g As Graphics)
            MyBase.Draw(g)

            Dim rectWidth As Single = Width - Height

            Select Case Coloring
                Case ColoringType.Fill
                    g.FillPie(New SolidBrush(BrushColor), X, Y, Height, Height, 90, 180)
                    g.FillRectangle(New SolidBrush(BrushColor), X + CSng(Height) / 2 - 1, Y, rectWidth, CSng(Height))
                    g.FillPie(New SolidBrush(BrushColor), X + rectWidth - 2, Y, Height, Height, 270, 180)
                Case ColoringType.Outline
                    Throw New NotImplementedException()
            End Select

        End Sub
    End Class

End Namespace