Option Explicit On
Option Strict On

Namespace Painting

    ''' <summary>
    ''' 扇形の描画用オブジェクト
    ''' </summary>
    Public Class CircularSector
        Inherits Shape

        Public Property StartAngle As Single
        Public Property SweepAngle As Single

        Public Overrides Sub Draw(ByRef g As Graphics)
            MyBase.Draw(g)

            Select Case Coloring
                Case ColoringType.Fill
                    g.FillPie(New SolidBrush(BrushColor), Rect, StartAngle, SweepAngle)
                Case ColoringType.Outline
                    g.DrawPie(New Pen(New SolidBrush(BrushColor), BorderWidth), Rect, StartAngle, SweepAngle)
            End Select
        End Sub


    End Class

End Namespace