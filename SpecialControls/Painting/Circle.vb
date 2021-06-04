Option Explicit On
Option Strict On

Namespace Painting

    ''' <summary>
    ''' 円の描画用オブジェクト
    ''' </summary>
    Public Class Circle
        Inherits Shape

        Public Overrides Sub Draw(ByRef g As Graphics)
            MyBase.Draw(g)

            Select Case Coloring
                Case ColoringType.Fill
                    g.FillEllipse(New SolidBrush(BrushColor), Rect)
                Case ColoringType.Outline
                    g.DrawEllipse(New Pen(New SolidBrush(BrushColor), BorderWidth), Rect)
            End Select
        End Sub
    End Class

End Namespace

