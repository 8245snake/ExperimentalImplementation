Option Explicit On
Option Strict On

Namespace Painting

    ''' <summary>
    ''' 四角形の描画用オブジェクト
    ''' </summary>
    Public Class Tetragon
        Inherits Shape

        Public Overrides Sub Draw(ByRef g As Graphics)
            MyBase.Draw(g)

            Select Case Coloring
                Case ColoringType.Fill
                    g.FillRectangle(New SolidBrush(BrushColor), Rect)
                    If BorderWidth > 0 Then
                        g.DrawRectangle(New Pen(New SolidBrush(BorderColor), BorderWidth), Rect)
                    End If
                Case ColoringType.Outline
                    g.DrawRectangle(New Pen(New SolidBrush(BorderColor), BorderWidth), Rect)
            End Select
        End Sub

    End Class

End Namespace
