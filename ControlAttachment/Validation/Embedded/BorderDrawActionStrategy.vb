Option Explicit On
Option Strict On

Imports System.Drawing
Imports System.Windows.Forms
Imports ControlAttachment.State

Namespace Validation.Embedded

    ''' <summary>
    ''' コントロールの周りを赤線で囲うアクション
    ''' </summary>
    Public Class BorderDrawActionStrategy
        Implements IErrorActionStrategy

        Private tmrBlink As Timer
        Private _IsBorderVisible As Boolean = False
        Private _TargetControl As Control
        Private _LastDrawTick As Long

        Private ReadOnly Property IsBlinkEnable As Boolean
            Get
                Return tmrBlink IsNot Nothing
            End Get
        End Property

        Public Property BorderLineWidth As Integer

        Sub New()
            BorderLineWidth = 3
        End Sub

        Public Sub New(Optional isBlinkEnable As Boolean = False)
            MyClass.New()

            If isBlinkEnable Then
                tmrBlink = New Timer()
                tmrBlink.Enabled = False
                tmrBlink.Interval = 800
                AddHandler tmrBlink.Tick, AddressOf BlinkTimerTick
            End If
        End Sub

        Private Sub BlinkTimerTick(sender As Object, e As EventArgs)
            _TargetControl?.Refresh()
        End Sub

        Public Sub ErrorAction(control As Control) Implements IErrorActionStrategy.ErrorAction
            If IsBlinkEnable Then
                _TargetControl = control
                tmrBlink.Enabled = True
            End If
        End Sub

        Public Sub SuccesAction(control As Control) Implements IErrorActionStrategy.SuccessAction

            If IsBlinkEnable Then
                tmrBlink.Enabled = False
                _TargetControl = Nothing
            End If

            InvalidateControlRect(control, BorderLineWidth)
        End Sub

        Public Sub ErrorPainting(control As Control) Implements IErrorActionStrategy.ErrorPainting

            If IsBlinkEnable Then

                Dim tick = DateTime.Now.Ticks
                Dim diff = tick - _LastDrawTick
                _LastDrawTick = tick

                If diff < 2000000 Then
                    ' チカチカするのを防ぐため200ms以内なら無視する
                    Return
                End If

                If _IsBorderVisible Then
                    ' 点滅させるために消す
                    InvalidateControlRect(control, BorderLineWidth)
                    _IsBorderVisible = False
                    Return
                End If
            End If

            ' テキストボックス内は狭いので親コントロールに描画する
            Using g As Graphics = control.Parent.CreateGraphics
                Dim pen As Pen = New Pen(Color.Red, BorderLineWidth)
                g.DrawRectangle(pen, GetDrawingRectangle(control, BorderLineWidth))
            End Using

            _IsBorderVisible = True

        End Sub

        Friend Shared Function GetDrawingRectangle(control As Control, lineWidth As Integer) As Rectangle
            Dim rect = New Rectangle(control.Left - lineWidth + 1, control.Top - lineWidth + 1, control.Width + lineWidth, control.Height + lineWidth)
            Return rect
        End Function

        Friend Shared Sub InvalidateControlRect(control As Control, lineWidth As Integer)
            Dim rect = GetDrawingRectangle(control, lineWidth)
            rect.X -= lineWidth
            rect.Y -= lineWidth
            rect.Width += lineWidth * 2
            rect.Height += lineWidth * 2
            control.Parent.Invalidate(rect)
            control.Parent.Update()
        End Sub

        Public Sub BeginHighlight(control As Control) Implements IHighlightingActionStrategy.BeginHighlight
            ErrorAction(control)
        End Sub

        Public Sub EndHighlight(control As Control) Implements IHighlightingActionStrategy.EndHighlight
            SuccesAction(control)
        End Sub

        Public Sub Highlight(control As Control) Implements IHighlightingActionStrategy.Highlight
            ErrorPainting(control)
        End Sub
    End Class

End Namespace