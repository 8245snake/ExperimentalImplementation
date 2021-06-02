Option Explicit On
Option Strict On

Imports System.Threading
Imports System.Reflection

''' <summary>
''' スプラッシュメッセージとして表示可能なフォーム
''' </summary>
Public Class SplashMessageForm
    Inherits Form

    Private _SplashMessage As SplashMessage = New SplashMessage()
    Private _SplashX As Integer
    Private _SplashY As Integer
    Private _SplashDuration As SplashMessage.DisplayDuration
    Private _CalledByShowSplash As Boolean = False
    Private _IsFirstLoad As Boolean = True
    Private _Delay As Integer = 300

    ''' <summary>
    ''' スプラッシュメッセージを表示するまでの遅延時間（ミリ秒）
    ''' </summary>
    Public Property Delay As Integer
        Get
            Return _Delay
        End Get
        Set(value As Integer)
            _Delay = CInt(IIf(value >= 300, value, 300))
        End Set
    End Property

    Sub New()
        Me.StartPosition = FormStartPosition.Manual
        Me.Location = New Point(-10000, -10000)
        Me.ShowInTaskbar = False
    End Sub

    Private Sub ShowSplash()

        If _CalledByShowSplash Then
            Me.ActiveControl = Nothing
            ' フォームの可視化中に画面に描こうとするととちらつくため待つ
            Thread.Sleep(Delay)
            Dim bmt As Bitmap = SplashMessage.CreateFormImage(Me)
            _SplashMessage.Show(bmt, _SplashX, _SplashY, _SplashDuration)
            ' フォームの不可視化中に画面に描こうとするととちらつくため待つ
            Thread.Sleep(Delay)
            Me.Hide()
        End If
    End Sub

    Protected Overrides Sub OnShown(e As EventArgs)
        MyBase.OnShown(e)
        _IsFirstLoad = False
        ' Shownが走ったあとではないとスクリーンキャプチャが撮れないためここで呼ぶ
        ShowSplash()
    End Sub

    ''' <summary>
    ''' 自身をスプラッシュメッセージとして表示します
    ''' </summary>
    ''' <param name="x">X座標</param>
    ''' <param name="y">Y座標</param>
    ''' <param name="duration">表示時間</param>
    ''' <remarks>using句内で呼び出すとうまく動作しません</remarks>
    Public Sub ShowSplash(x As Integer, y As Integer, duration As SplashMessage.DisplayDuration)

        _SplashX = x
        _SplashY = y
        _SplashDuration = duration
        _CalledByShowSplash = True
        Me.Show()
        If Not _IsFirstLoad Then
            OnShown(New EventArgs())
        End If

    End Sub

    ''' <summary>
    ''' スプラッシュメッセージを閉じます
    ''' </summary>
    Public Sub HideSplash()
        _SplashMessage.Hide()
    End Sub

End Class
