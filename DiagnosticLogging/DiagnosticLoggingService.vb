Option Explicit On
Option Strict On

''' <summary>
''' 計測開始からアイドル状態になるまでの時間を測定するサービス
''' </summary>
Public Class DiagnosticLoggingService

    Public Shared Instance As DiagnosticLoggingService = New DiagnosticLoggingService(Nothing)

    Private _timer As Timer
    Private _eventStartTick As Integer
    Private _timerInterval As Integer = 100
    Private _eventName As String = ""
    Private _isReadyToStart As Boolean = True
    Private _logger As IDiagnosticLogger

    Public Sub New(logger As IDiagnosticLogger, Optional timerInterval As Integer = 100)
        _logger = logger
        _timer = New Timer()
        _timer.Enabled = False
        _timerInterval = timerInterval
        _timer.Interval = timerInterval
        AddHandler _timer.Tick, AddressOf Tick
    End Sub

    ''' <summary>
    ''' 測定を開始する
    ''' </summary>
    ''' <param name="eventName">ログに出力される任意のイベント名</param>
    Public Sub Start(eventName As String)
        If Not _isReadyToStart Then
            Return
        End If
        _eventName = eventName
        _eventStartTick = Environment.TickCount
        RemoveHandler Application.Idle, AddressOf OnIdle
        AddHandler Application.Idle, AddressOf OnIdle
        _isReadyToStart = False
    End Sub

    Private Sub OnIdle(sender As Object, e As EventArgs)
        ' アイドル状態になったからといって一瞬のことかもしれないので{_timerInterval}ミリ秒だけログ出力を保留する
        _timer.Enabled = True
    End Sub

    ''' <summary>
    ''' アイドル状態になったあとしばらくして呼ばれる
    ''' </summary>
    Private Sub Tick(sender As Object, e As EventArgs)
        _timer.Enabled = False
        Dim millisecond As Integer = Environment.TickCount - _eventStartTick - _timerInterval
        If _logger Is Nothing Then
            System.Console.WriteLine($"[{_eventName}] {millisecond}ミリ秒")
        Else
            _logger.WriteLog(_eventName, millisecond)
        End If
        ' 後始末
        RemoveHandler Application.Idle, AddressOf OnIdle
        _isReadyToStart = True
        _eventName = ""
    End Sub
End Class

Public Interface IDiagnosticLogger
    ''' <summary>
    ''' 測定ログを出力する
    ''' </summary>
    ''' <param name="eventName">イベント名</param>
    ''' <param name="millisecond">経過時間(ミリ秒)</param>
    Sub WriteLog(eventName As String, millisecond As Integer)
End Interface
