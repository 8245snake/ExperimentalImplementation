Option Explicit On
Option Strict On

Imports System.Threading
Imports System.Windows.Forms

Public Class MainForm
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        DiagnosticLoggingService.Instance.Start("Button1_Click")
        Thread.Sleep(1000)
    End Sub

    Private Sub MainForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DiagnosticLoggingService.Instance.Start("MainForm_Load")
        Thread.Sleep(1000)
    End Sub

    Private Sub MainForm_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        DiagnosticLoggingService.Instance.Start("MainForm_Shown")
        Dim timer As System.Windows.Forms.Timer = New System.Windows.Forms.Timer()
        timer.Interval = 100
        AddHandler timer.Tick, AddressOf Timer_Tick
        timer.Enabled = True
    End Sub

    Private Sub Timer_Tick(sender As Object, e As EventArgs)
        DiagnosticLoggingService.Instance.Start("Timer_Tick")
        Thread.Sleep(5000)
        RemoveHandler DirectCast(sender, System.Windows.Forms.Timer).Tick, AddressOf Timer_Tick
        DirectCast(sender, System.Windows.Forms.Timer).Enabled = False
    End Sub
End Class
