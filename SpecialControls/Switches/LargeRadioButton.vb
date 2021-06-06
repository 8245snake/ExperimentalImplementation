Option Explicit On
Option Strict On

Imports SpecialControls.Painting

Namespace Switches
    Public Class LargeRadioButton
        Inherits RadioButton

        Private _IsMouseHovering As Boolean = False

        ''' <summary>
        ''' マウスホバーしたときの背景色
        ''' </summary>
        Public Property HoverColor As Color = ColorTranslator.FromHtml("#110000FF")

        ''' <summary>
        ''' ラジオボタンの円の枠線の太さ
        ''' </summary>
        Public Property CircleBorderWidth As Integer = 1

        ''' <summary>
        ''' ラジオボタン外側の丸
        ''' </summary>
        Private ReadOnly Property OuterCircle As Circle
            Get
                Dim circle As Circle = New Circle()
                circle.Coloring = Shape.ColoringType.Fill
                circle.BrushColor = Color.White
                circle.BorderColor = Color.Black
                circle.BorderWidth = CircleBorderWidth
                circle.X = CircleBorderWidth
                circle.Y = circle.X
                circle.Width = Me.Height - CircleBorderWidth * 2
                circle.Height = circle.Width
                Return circle
            End Get
        End Property

        ''' <summary>
        ''' ラジオボタン内側の丸（チェック時に出る）
        ''' </summary>
        Private ReadOnly Property InnerCircle As Circle
            Get
                Dim circle As Circle = New Circle()
                circle.Coloring = Shape.ColoringType.Fill
                circle.BrushColor = Color.Black

                Dim space As Integer = CInt(Me.Height * 0.2)
                circle.X = CircleBorderWidth + space
                circle.Y = circle.X
                circle.Width = Me.Height - 2 * (CircleBorderWidth + space)
                circle.Height = circle.Width

                Return circle
            End Get
        End Property

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

        Sub New()
            Me.Cursor = Cursors.Hand
        End Sub

        Protected Overrides Sub OnPaint(pevent As PaintEventArgs)
            MyBase.OnPaint(pevent)

            Dim g As Graphics = pevent.Graphics
            ' オリジナルの描画を消して自前で描く（ControlPaint.DrawRadioButtonはいまいちなので円を描いてつくる）
            g.Clear(Me.BackColor)
            g.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias

            If _IsMouseHovering AndAlso Enabled Then
                BorderShape.Draw(g)
            End If

            ' 図形描画
            OuterCircle.Draw(g)
            If Me.Checked Then
                InnerCircle.Draw(g)
            End If

            If Not Enabled Then
                Dim grayCircle As Shape = OuterCircle.DeepCopy()
                grayCircle.BrushColor = ColorTranslator.FromHtml("#AAE0E0E0")
                grayCircle.Draw(g)
            End If

            Dim textSize As Size = TextRenderer.MeasureText(Me.Text, Me.Font)
            Dim circleLength As Integer = Me.Height - CircleBorderWidth * 2
            Dim top As Integer = 0
            Dim left As Integer = 0
            Select Case Me.TextAlign
                Case ContentAlignment.MiddleLeft
                    left = circleLength
                    top = CInt((Me.Height - textSize.Height) / 2)
                Case Else
                    Throw New Exception("LargeRadioButton.TextAlignにはMiddleLeftのみ設定可能です")
            End Select

            Dim fontColor As Color = If(Enabled, Me.ForeColor, Color.Gray)
            TextRenderer.DrawText(g, Me.Text, Me.Font, New Point(left, top), fontColor)

        End Sub


    End Class

End Namespace


