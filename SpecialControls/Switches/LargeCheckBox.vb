Option Explicit On
Option Strict On
Imports SpecialControls.Painting

Namespace Switches

    ''' <summary>
    ''' 大きなチェックボックス
    ''' </summary>
    Public Class LargeCheckBox
        Inherits CheckBox

        Private _IsMouseHovering As Boolean = False

        ''' <summary>
        ''' マウスホバーしたときの背景色
        ''' </summary>
        Public Property HoverColor As Color = ColorTranslator.FromHtml("#110000FF")

        Sub New()
            Me.AutoSize = False
            Me.TextAlign = ContentAlignment.MiddleLeft
            Me.Cursor = Cursors.Hand
        End Sub

        ''' <summary>
        ''' マウスホバーしたときに追加で表示される枠線
        ''' </summary>
        Protected Overridable ReadOnly Property BorderShape As Shape
            Get
                Dim body As Tetragon = New Tetragon
                body.Coloring = Shape.ColoringType.Fill
                body.BrushColor = HoverColor
                body.BorderWidth = 0
                body.Rect = New Rectangle(0, 0, Me.Width, Me.Height)
                Return body
            End Get
        End Property

        Protected Overrides Sub OnMouseEnter(e As EventArgs)
            MyBase.OnMouseEnter(e)
            _IsMouseHovering = True
            Me.Invalidate()
        End Sub

        Protected Overrides Sub OnMouseLeave(e As EventArgs)
            MyBase.OnMouseLeave(e)
            _IsMouseHovering = False
            Me.Invalidate()
        End Sub

        Protected Overrides Sub OnPaint(pevent As PaintEventArgs)
            MyBase.OnPaint(pevent)

            Dim g As Graphics = pevent.Graphics
            ' オリジナルの描画を消して自前で描く
            g.Clear(Me.BackColor)
            g.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias

            If _IsMouseHovering Then
                BorderShape.Draw(g)
            End If

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



