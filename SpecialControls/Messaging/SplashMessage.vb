Option Explicit On
Option Strict On

Imports System.Drawing.Drawing2D
Imports System.Threading
Imports SpecialControls.Win32

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
            ''' 短時間（約1秒）
            ''' </summary>
            ShortTime = 300
            ''' <summary>
            ''' 長時間（約3秒）
            ''' </summary>
            LongTime = 1000
            ''' <summary>
            ''' 消えない
            ''' </summary>
            Endless = -1
        End Enum

        Private _Bitmap As Bitmap
        Private _X As Integer
        Private _Y As Integer
        Private _Duration As DisplayDuration
        Private _IsCancel As Boolean = False

        ''' <summary>
        ''' 常に表示し続けるか
        ''' </summary>
        Private ReadOnly Property IsEndless As Boolean
            Get
                Return _Duration <= 0
            End Get
        End Property


        ''' <summary>
        ''' スプラッシュメッセージを表示する
        ''' </summary>
        ''' <param name="bmp">表示するビットマップ</param>
        ''' <param name="x">X座標</param>
        ''' <param name="y">Y座標</param>
        ''' <param name="duration">表示時間</param>
        Public Sub Show(bmp As Bitmap, x As Integer, y As Integer, Optional duration As DisplayDuration = DisplayDuration.ShortTime)
            _Bitmap = bmp
            _X = x
            _Y = y
            _Duration = duration
            _IsCancel = False
            Dim t As New Thread(New ThreadStart(AddressOf DoThread))
            t.IsBackground = True
            t.Start()
        End Sub

        ''' <summary>
        ''' 表示中のスプラッシュメッセージを閉じる
        ''' </summary>
        Public Sub Hide()
            _IsCancel = True
            _Bitmap = Nothing
        End Sub

        Private Sub DoThread()
            showSplashMessage(_Bitmap, _X, _Y)
        End Sub

        ''' <summary>
        '''  ビットマップを指定した座標に表示する
        ''' </summary>
        ''' <param name="bmp"></param>
        ''' <param name="x"></param>
        ''' <param name="y"></param>
        Protected Overridable Sub showSplashMessage(bmp As Bitmap, x As Integer, y As Integer)

            ' Zeroを指定するとデスクトップのデバイスコンテキストを取得できる
            Dim hdc As IntPtr = GetDC(IntPtr.Zero)
            Dim hsrc As IntPtr = CreateCompatibleDC(hdc)
            SelectObject(hsrc, bmp.GetHbitmap())

            Dim width As Integer = bmp.Width
            Dim height As Integer = bmp.Height

            ' 少しずつ透過度を上げて表示
            AlphaBlend(hdc, x, y, width, height, hsrc, 0, 0, width, height, 100 * &H10000)
            Thread.Sleep(50)
            AlphaBlend(hdc, x, y, width, height, hsrc, 0, 0, width, height, 180 * &H10000)
            Thread.Sleep(50)

            Dim total As Long = 0
            Dim sleepMilliseconds As Integer = 10
            If IsEndless Then
                total = -100
            End If
            While Not _IsCancel AndAlso total < _Duration
                ' 透過する必要ないのでBitBlt
                BitBlt(hdc, x, y, width, height, hsrc, 0, 0, TernaryRasterOperations.SRCCOPY)
                Thread.Sleep(sleepMilliseconds)
                If Not IsEndless Then
                    total += sleepMilliseconds
                End If
            End While

            ' 後始末
            DeleteDC(hsrc)
            ReleaseDC(IntPtr.Zero, hdc)

            ' メッセージを消す
            Dim rect As RECT
            rect.Left = x
            rect.Top = y
            rect.Right = x + width
            rect.Bottom = y + height
            InvalidateRect(IntPtr.Zero, rect, True)
        End Sub

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

        ''' <summary>
        ''' 指定したフォームのビットマップを作成する
        ''' </summary>
        ''' <param name="form">フォーム</param>
        ''' <param name="clientAreaOnly">クライアント領域のみに限定するか</param>
        ''' <returns>ビットマップ</returns>
        Public Shared Function CreateFormImage(form As Form, Optional clientAreaOnly As Boolean = True) As Bitmap

            Dim bmt As Bitmap = CaptureControl(form)

            If clientAreaOnly Then
                ' クライアント領域に切り詰める
                Dim rect As Rectangle
                rect.X = CInt((form.Size.Width - form.ClientSize.Width) / 2)
                rect.Y = form.Size.Height - form.ClientSize.Height - rect.X
                rect.Width = form.ClientSize.Width
                rect.Height = form.ClientSize.Height
                bmt = bmt.Clone(rect, bmt.PixelFormat)
            End If

            Return bmt
        End Function

        ''' <summary>
        ''' コントロールをキャプチャする。
        ''' DrawToBitmapでは不可能なリッチテキストコントロールにも対応。
        ''' </summary>
        ''' <param name="ctrl">コントロール</param>
        ''' <returns>ビットマップ</returns>
        Public Shared Function CaptureControl(ByVal ctrl As Control) As Bitmap
            Dim img As New Bitmap(ctrl.Width, ctrl.Height)
            Using memg As Graphics = Graphics.FromImage(img)
                Dim dc As IntPtr = memg.GetHdc()
                PrintWindow(ctrl.Handle, dc, 0)
                memg.ReleaseHdc(dc)
            End Using
            Return img
        End Function

    End Class

End Namespace