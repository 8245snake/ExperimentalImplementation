﻿Option Explicit On
Option Strict On

Imports System.Drawing.Drawing2D
Imports System.Threading

''' <summary>
''' スプラッシュメッセージを表示するクラス
''' </summary>
Public Class SplashMessage

#Region "Win32Api"

    ''' <summary>
    '''     Specifies a raster-operation code. These codes define how the color data for the
    '''     source rectangle is to be combined with the color data for the destination
    '''     rectangle to achieve the final color.
    ''' </summary>
    Enum TernaryRasterOperations As UInteger
        ''' <summary>dest = source</summary>
        SRCCOPY = &HCC0020
        ''' <summary>dest = source OR dest</summary>
        SRCPAINT = &HEE0086
        ''' <summary>dest = source AND dest</summary>
        SRCAND = &H8800C6
        ''' <summary>dest = source XOR dest</summary>
        SRCINVERT = &H660046
        ''' <summary>dest = source AND (NOT dest)</summary>
        SRCERASE = &H440328
        ''' <summary>dest = (NOT source)</summary>
        NOTSRCCOPY = &H330008
        ''' <summary>dest = (NOT src) AND (NOT dest)</summary>
        NOTSRCERASE = &H1100A6
        ''' <summary>dest = (source AND pattern)</summary>
        MERGECOPY = &HC000CA
        ''' <summary>dest = (NOT source) OR dest</summary>
        MERGEPAINT = &HBB0226
        ''' <summary>dest = pattern</summary>
        PATCOPY = &HF00021
        ''' <summary>dest = DPSnoo</summary>
        PATPAINT = &HFB0A09
        ''' <summary>dest = pattern XOR dest</summary>
        PATINVERT = &H5A0049
        ''' <summary>dest = (NOT dest)</summary>
        DSTINVERT = &H550009
        ''' <summary>dest = BLACK</summary>
        BLACKNESS = &H42
        ''' <summary>dest = WHITE</summary>
        WHITENESS = &HFF0062
        ''' <summary>
        ''' Capture window as seen on screen.  This includes layered windows
        ''' such as WPF windows with AllowsTransparency="true"
        ''' </summary>
        CAPTUREBLT = &H40000000
    End Enum


    Declare Function GetDC Lib "user32.dll" (ByVal hWnd As IntPtr) As IntPtr
    Declare Function CreateCompatibleDC Lib "gdi32.dll" (hdc As IntPtr) As IntPtr
    Declare Function DeleteDC Lib "gdi32" (ByVal hDC As IntPtr) As IntPtr
    Declare Function ReleaseDC Lib "user32" (ByVal hwnd As IntPtr, ByVal hDC As IntPtr) As IntPtr

    Declare Function BitBlt Lib "gdi32.dll" (ByVal hdc As IntPtr, ByVal nXDest As Integer,
                                             ByVal nYDest As Integer,
                                             ByVal nWidth As Integer,
                                             ByVal nHeight As Integer,
                                             ByVal hdcSrc As IntPtr,
                                             ByVal nXSrc As Integer,
                                             ByVal nYSrc As Integer,
                                             ByVal dwRop As TernaryRasterOperations) As Boolean



    Declare Function AlphaBlend Lib "msimg32.dll" _
        (ByVal hdcDest As IntPtr _
        , ByVal nXDest As Long _
        , ByVal nYDest As Long _
        , ByVal nWidthDest As Long _
        , ByVal nHeightDest As Long _
        , ByVal hdcSrc As IntPtr _
        , ByVal nXSrc As Long _
        , ByVal nYSrc As Long _
        , ByVal nWidthSrc As Long _
        , ByVal nHeightSrc As Long _
        , ByVal nBlendFunc As Long) As Long

    Declare Function SelectObject Lib "gdi32.dll" (ByVal prmlngHDc As IntPtr, ByVal hObject As IntPtr) As IntPtr

    Declare Function InvalidateRect Lib "User32" (ByVal hWnd As IntPtr, ByRef lpRect As RECT, ByVal bErase As Boolean) As Boolean


    <System.Runtime.InteropServices.DllImport("User32.dll")>
    Private Shared Function PrintWindow(ByVal hwnd As IntPtr, ByVal hDC As IntPtr, ByVal nFlags As Integer) As Boolean
    End Function

    Public Structure RECT
        Public Left As Long
        Public Top As Long
        Public Right As Long
        Public Bottom As Long
    End Structure

#End Region

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
