Option Explicit On
Option Strict On

Namespace Switches

    ''' <summary>
    ''' 大きなチェックボックス
    ''' </summary>
    Public Class LargeCheckBox
        Inherits CheckBox

        Sub New()
            Me.AutoSize = False
            Me.TextAlign = ContentAlignment.MiddleLeft
        End Sub

        Protected Overrides Sub OnPaint(pevent As PaintEventArgs)
            MyBase.OnPaint(pevent)

            Dim g As Graphics = pevent.Graphics
            ' オリジナルの描画を消して自前で描く
            g.Clear(Me.BackColor)
            g.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias

            Dim sideLength As Integer = Me.Height
            Dim margin As Integer = CInt(sideLength / 4)
            Select Case Me.CheckState
                Case CheckState.Unchecked
                    ControlPaint.DrawCheckBox(g, 0, 0, sideLength, sideLength, ButtonState.Normal Or ButtonState.Flat)
                Case CheckState.Checked
                    ControlPaint.DrawCheckBox(g, 0, 0, sideLength, sideLength, ButtonState.Checked Or ButtonState.Flat)
                Case CheckState.Indeterminate
                    ControlPaint.DrawCheckBox(g, 0, 0, sideLength, sideLength, ButtonState.Normal Or ButtonState.Flat)
                    g.FillRectangle(Brushes.Black, margin, margin, sideLength - 2 * margin, sideLength - 2 * margin)
                Case Else
            End Select

            Dim textSize As Size = TextRenderer.MeasureText(Me.Text, Me.Font)
            Dim top As Integer = 0
            Dim left As Integer = 0
            Select Case Me.TextAlign
                Case ContentAlignment.MiddleLeft
                    left = sideLength
                    top = CInt((Me.Height - textSize.Height) / 2)
                Case Else
                    Throw New Exception("LargeCheckBox.TextAlignにはMiddleLeftのみ設定可能です")
            End Select

            TextRenderer.DrawText(g, Me.Text, Me.Font, New Point(left, top), Me.ForeColor)
        End Sub

    End Class
End Namespace



