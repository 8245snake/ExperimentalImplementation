Option Explicit On
Option Strict On

Imports SpecialControls.Painting


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
            Endless = Short.MaxValue
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
        Public Shared Function CreateTextImage(ByVal text As String,
                                               ByVal font As Font,
                                               Optional ByVal fontColor As Color = Nothing,
                                               Optional ByVal backgroundColor As Color = Nothing,
                                               Optional ByVal paddingX As Single = 5,
                                               Optional ByVal paddingY As Single = 5,
                                               Optional ByVal width As Integer = 100,
                                               Optional ByVal height As Integer = 100,
                                               Optional ByVal sizeToFit As Boolean = True) As Bitmap

            Dim character = New Character()
            character.Text = text
            character.Font = font
            ' 文字色
            character.BrushColor = If(fontColor = Nothing, Color.Black, fontColor)
            ' 中央寄せ
            character.TextHorizontalAlignment = Character.HorizontalAlignmentType.Center
            character.TextVerticalAlignment = Character.VerticalAlignmentType.Center

            ' テキストのサイズを見積もる
            Dim measuredSize As Size
            If sizeToFit Then
                Using dummy = New Bitmap(1000, 1000)
                    Using g As Graphics = Graphics.FromImage(dummy)
                        measuredSize = character.GetMeasuredSize(g)
                    End Using
                End Using
            End If

            Dim canvas As Bitmap
            If sizeToFit Then
                ' 上下左右にパディングを足す
                canvas = New Bitmap(CInt(measuredSize.Width + 2 * paddingX), CInt(measuredSize.Height + 2 * paddingY))
            Else
                canvas = New Bitmap(width, height)
            End If

            character.Width = canvas.Width
            character.Height = canvas.Height

            ' 背景色
            If backgroundColor = Nothing Then
                backgroundColor = Color.White
            End If

            ' ビットマップに描画
            Using g As Graphics = Graphics.FromImage(canvas)
                g.Clear(backgroundColor)
                character.Draw(g)
            End Using

            Return canvas
        End Function

        Public Shared Function CreateFormImage(Of T As {ToolTipForm, New})() As Bitmap
            Using frm = New T()
                Return frm.GetBitmap()
            End Using
        End Function
    End Class

End Namespace