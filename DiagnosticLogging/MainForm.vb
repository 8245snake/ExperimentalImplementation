Option Explicit On
Option Strict On

Imports System.Threading
Imports System.Windows.Forms

Public Class MainForm

    Private _count As Integer = 0

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        DiagnosticLoggingService.Instance.Start("Button1_Click")
        System.Console.WriteLine("1秒待ち")
        Thread.Sleep(1000)
    End Sub

    Private Sub MainForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DiagnosticLoggingService.Instance.Start("MainForm_Load")
        System.Console.WriteLine("1秒待ち")
        Thread.Sleep(1000)
    End Sub

    Private Sub MainForm_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        DiagnosticLoggingService.Instance.Start("MainForm_Shown")
        Dim timer As System.Windows.Forms.Timer = New System.Windows.Forms.Timer()
        timer.Interval = 100
        AddHandler timer.Tick, AddressOf Timer_Tick
        timer.Enabled = True
    End Sub

    ''' <summary>
    ''' 2秒かかる処理を0.1秒間隔で3回繰り返す
    ''' </summary>
    Private Sub Timer_Tick(sender As Object, e As EventArgs)
        DiagnosticLoggingService.Instance.Start("Timer_Tick")
        System.Console.WriteLine("2秒待ち")
        Thread.Sleep(2000)
        _count += 1
        If _count >= 3 Then
            RemoveHandler DirectCast(sender, System.Windows.Forms.Timer).Tick, AddressOf Timer_Tick
            DirectCast(sender, System.Windows.Forms.Timer).Enabled = False
        End If
    End Sub
End Class
