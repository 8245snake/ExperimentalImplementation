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
                    If BorderWidth > 0 Then
                        DrawBorder(g)
                    End If
                Case ColoringType.Outline
                    DrawBorder(g)
            End Select

        End Sub

        ''' <summary>
        ''' 枠線を描画する
        ''' </summary>
        ''' <param name="g">デバイスコンテキスト</param>
        Private Sub DrawBorder(ByRef g As Graphics)
            Dim rectWidth As Single = Width - Height
            Dim left = X + CSng(Height) / 2 - 1
            Dim bottom = Y + Me.Height - 1

            g.DrawArc(New Pen(BorderColor, BorderWidth), X, Y, Height, Height, 90, 180)
            g.DrawLine(New Pen(BorderColor, BorderWidth), left, Y, left + rectWidth, Y)
            g.DrawLine(New Pen(BorderColor, BorderWidth), left - 1, bottom, left + rectWidth + 1, bottom)
            g.DrawArc(New Pen(BorderColor, BorderWidth), X + rectWidth - 2, Y, Height, Height, 270, 180)
        End Sub

    End Class

End Namespace