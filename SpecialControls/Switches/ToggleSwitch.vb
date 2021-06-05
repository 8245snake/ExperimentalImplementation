Option Explicit On
Option Strict On

Imports SpecialControls.Painting
Imports SpecialControls.Painting.Character


Namespace Switches

    ''' <summary>
    ''' OnとOffの状態が見やすいトグルスイッチ
    ''' </summary>
    Public Class ToggleSwitch
        Inherits PictureBox

        ''' <summary>
        ''' チェック状態が変化したときに発火する
        ''' </summary>
        Public Event CheckedChanged As EventHandler

        Private _IsChecked As Boolean = False
        Private _FontName As String
        Private _FontSize As Single
        Private _FontStyle As FontStyle
        Private _ValueProgress As Single
        Private _IsMouseHovering As Boolean = False

        ''' <summary>
        ''' チェック状態を取得または設定します
        ''' </summary>
        ''' <returns>True;チェックあり、False:チェックなし</returns>
        Public Property IsChecked As Boolean
            Get
                Return _IsChecked
            End Get
            Set(value As Boolean)
                Dim blChanged As Boolean = _IsChecked <> value
                _IsChecked = value
                ' アニメーション不要なのでいきなり0→1のように遷移する
                _ValueProgress = If(_IsChecked, 1, 0)
                Me.Invalidate()
                If blChanged Then
                    RaiseEvent CheckedChanged(Me, New EventArgs())
                End If
            End Set
        End Property

        ''' <summary>
        ''' <seealso cref="IsChecked"/>がTrueのときのキャプション
        ''' </summary>
        Public Property TrueText As String

        ''' <summary>
        ''' <seealso cref="IsChecked"/>がTrueのときの背景色
        ''' </summary>
        Public Property TrueColor As Color

        ''' <summary>
        ''' <seealso cref="IsChecked"/>がFalseのときのキャプション
        ''' </summary>
        Public Property FalseText As String

        ''' <summary>
        ''' <seealso cref="IsChecked"/>がFalseのときの背景色
        ''' </summary>
        Public Property FalseColor As Color


        ''' <summary>
        ''' フォント名
        ''' </summary>
        Public Property FontName As String
            Get
                Return _FontName
            End Get
            Set
                _FontName = Value
                Me.Invalidate()
            End Set
        End Property

        ''' <summary>
        ''' フォントサイズ
        ''' </summary>
        Public Property FontSize As Single
            Get
                Return _FontSize
            End Get
            Set
                _FontSize = Value
                Me.Invalidate()
            End Set
        End Property

        ''' <summary>
        ''' フォントスタイル
        ''' </summary>
        Public Property FontStyle As FontStyle
            Get
                Return _FontStyle
            End Get
            Set
                _FontStyle = Value
                Me.Invalidate()
            End Set
        End Property

        ''' <summary>
        ''' スイッチの位置（0.0～1.0）
        ''' </summary>
        Protected Property ValueProgress As Single
            Get
                Return _ValueProgress
            End Get
            Set(value As Single)
                _ValueProgress = value
            End Set
        End Property

        ''' <summary>
        ''' スイッチの位置の最小X座標値
        ''' </summary>
        Protected Overridable ReadOnly Property MinValue As Integer
            Get
                Return CInt(Me.Height * 0.1)
            End Get
        End Property

        ''' <summary>
        ''' スイッチの位置の最大X座標値
        ''' </summary>
        Protected Overridable ReadOnly Property MaxValue As Integer
            Get
                Return Width - Height
            End Get
        End Property

        ''' <summary>
        ''' チェックした際にアニメーションを表示ずるか
        ''' </summary>
        Public Property EnableAnimation As Boolean = True

        ''' <summary>
        ''' コントロールの外枠の<seealso cref="Shape"/>
        ''' </summary>
        Protected Overridable ReadOnly Property OutlineShape As Shape
            Get
                Dim body As ExpandedCircle = New ExpandedCircle
                body.Coloring = Shape.ColoringType.Fill
                body.BrushColor = If(IsChecked, TrueColor, FalseColor)
                body.Rect = New Rectangle(0, 0, Me.Width, Me.Height)
                Return body
            End Get
        End Property

        ''' <summary>
        ''' マウスホバーしたときに追加で表示される枠線
        ''' </summary>
        Protected Overridable ReadOnly Property BorderShape As Shape
            Get
                Dim body As ExpandedCircle = New ExpandedCircle
                body.Coloring = Shape.ColoringType.Outline
                body.BorderColor = Color.CadetBlue
                body.BorderWidth = 1
                body.Rect = New Rectangle(0, 0, Me.Width, Me.Height)
                Return body
            End Get
        End Property

        ''' <summary>
        ''' スイッチの部分の<seealso cref="Shape"/>
        ''' </summary>
        Protected Overridable ReadOnly Property SwitchShape As Shape
            Get
                Dim circle As Circle = New Circle()
                circle.Coloring = Shape.ColoringType.Fill
                circle.BrushColor = Color.White
                ' 位置は_ValueProgressによって変化する
                circle.X = CInt(MinValue + (MaxValue - MinValue) * _ValueProgress)
                circle.Y = CInt(Height * 0.1 / 2.0)
                circle.Height = CInt(Me.Height * 0.9)
                circle.Width = CInt(Me.Height * 0.9)
                Return circle
            End Get
        End Property

        ''' <summary>
        ''' OnとOffの文言表示ラベルの<seealso cref="Shape"/>
        ''' </summary>
        Protected Overridable ReadOnly Property TextShape As Shape
            Get
                Dim label As Character = New Character()
                label.Coloring = Shape.ColoringType.Fill
                label.BrushColor = Color.White
                label.TextHorizontalAlignment = HorizontalAlignmentType.Center
                label.TextVerticalAlignment = VerticalAlignmentType.Center
                label.Text = If(IsChecked, TrueText, FalseText)
                label.Font = If(Not String.IsNullOrWhiteSpace(FontName) And FontSize > 0, New Font(FontName, FontSize, FontStyle), Me.Font)
                label.X = If(IsChecked, 0, CInt(Me.Height * 0.9))
                label.Y = 0
                label.Height = Me.Height
                label.Width = Me.Width - CInt(Me.Height * 0.9)
                Return label
            End Get
        End Property

        Sub New()
            ' デザイン時の初期値
            Me.Height = 23
            Me.Width = 50
            TrueColor = Color.Lime
            FalseColor = Color.DarkGray
            Me.Cursor = Cursors.Hand
        End Sub


        Protected Overrides Sub OnPaint(pe As PaintEventArgs)
            Dim g As Graphics = pe.Graphics
            g.Clear(Control.DefaultBackColor)
            OutlineShape.Draw(g)
            SwitchShape.Draw(g)
            TextShape.Draw(g)
            If _IsMouseHovering Then
                BorderShape.Draw(g)
            End If
        End Sub

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

        Protected Overrides Sub OnClick(e As EventArgs)

            If EnableAnimation Then
                CheckedChangeWithAnimation()
            End If

            IsChecked = Not IsChecked
            MyBase.OnClick(e)
        End Sub

        ''' <summary>
        ''' 少しずつチェック状態が遷移するアニメーションの処理。
        ''' ValueProgress値を変えながら再描画する。
        ''' </summary>
        Protected Overridable Sub CheckedChangeWithAnimation()
            Dim stepOfValue As Single = If(IsChecked, -0.2F, 0.2F)
            _ValueProgress = If(IsChecked, 1.0F, 0.0F)
            For index = 1 To 5
                _ValueProgress += stepOfValue
                Me.Invalidate()
                Threading.Thread.Sleep(10)
                Application.DoEvents()
            Next
        End Sub

    End Class

End Namespace
