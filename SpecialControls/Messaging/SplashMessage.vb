Option Explicit On
Option Strict On

Imports System.Drawing.Drawing2D


Namespace Messaging


    ''' <summary>
    ''' スプラッシュメッセージを表示するクラス
    ''' </summary>
    Public Class SplashMessage

        ''' <summary>
        ''' スプラッシュメッセージが消えるまでの時間
        ''' </summary>
        Public Enum DisplayDuration
            ''' <summary>
            ''' 短時間（1秒）
            ''' </summary>
            ShortTime = 1000
            ''' <summary>
            ''' 長時間（3秒）
            ''' </summary>
            LongTime = 3000
            ''' <summary>
            ''' 消えない
            ''' </summary>
            Endless = -1
        End Enum

        ''' <summary>
        ''' テキストが書かれたビットマップを作成する
        ''' </summary>
        ''' <param name="text">表示するテキスト</param>
        ''' <param name="font">フォント</param>
        ''' <param name="fontColor">文字色(省略時は黒)</param>
        ''' <param name="backgroundColor">背景色(省略時は白)</param>
        ''' <param name="paddingX">水平方向のパディング</param>
        ''' <param name="paddingY">垂直方向のパディング</param>
        ''' <param name="width">幅</param>
        ''' <param name="height">高さ</param>
        ''' <param name="sizeToFit">幅と高さを自動で合わせるか（widthとheight引数は無視されます）</param>
        ''' <returns>ビットマップ</returns>
        Public Shared Function CreateTextImage(text As String,
                                               font As Font,
                                               Optional ByVal fontColor As Color = Nothing,
                                               Optional ByVal backgroundColor As Color = Nothing,
                                               Optional ByVal paddingX As Single = 5,
                                               Optional ByVal paddingY As Single = 5,
                                               Optional ByVal width As Integer = 100,
                                               Optional ByVal height As Integer = 100,
                                               Optional ByVal sizeToFit As Boolean = True) As Bitmap
            Dim measuredSize As Size
            ' テキストのサイズを見積もる
            If sizeToFit Then
                Dim dummy As Bitmap = New Bitmap(1000, 1000)
                Using g As Graphics = Graphics.FromImage(dummy)
                    g.SmoothingMode = SmoothingMode.AntiAlias
                    g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias
                    measuredSize = TextRenderer.MeasureText(g, text, font, New Size(1000, 1000), TextFormatFlags.NoPadding)
                End Using
            End If

            Dim canvas As Bitmap
            If sizeToFit Then
                ' パディングを足す。水平方向だけ5%くらい足りないので余分に補正
                canvas = New Bitmap(CInt(measuredSize.Width + 2 * paddingX + measuredSize.Width * 0.05), CInt(measuredSize.Height + 2 * paddingY))
            Else
                canvas = New Bitmap(width, height)
            End If

            ' 文字色と背景色
            If fontColor = Nothing Then
                fontColor = Color.Black
            End If
            If backgroundColor = Nothing Then
                backgroundColor = Color.White
            End If

            ' ビットマップに描画
            Using g As Graphics = Graphics.FromImage(canvas)
                g.Clear(backgroundColor)
                g.SmoothingMode = SmoothingMode.AntiAlias
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias
                g.DrawString(text, font, New SolidBrush(fontColor), paddingX, paddingY)
            End Using

            Return canvas
        End Function


    End Class

End Namespace