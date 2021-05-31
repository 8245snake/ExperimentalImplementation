Option Explicit On
Option Strict On

Imports System.Drawing.Drawing2D

''' <summary>
''' OnとOffの状態が見やすいトグルスイッチ
''' </summary>
Public Class ToggleSwitch
    Inherits PictureBox

    Public Event CheckedChanged As EventHandler

    Private _IsChecked As Boolean = False

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
            Me.Invalidate()
            If blChanged Then
                RaiseEvent CheckedChanged(Me, New EventArgs())
            End If
        End Set
    End Property

    ''' <summary>
    ''' IsCheckedがTrueのときのキャプション
    ''' </summary>
    Public Property TrueText As String

    ''' <summary>
    ''' IsCheckedがTrueのときの背景色
    ''' </summary>
    Public Property TrueColor As Color

    ''' <summary>
    ''' IsCheckedがFalseのときのキャプション
    ''' </summary>
    Public Property FalseText As String

    ''' <summary>
    ''' IsCheckedがFalseのときの背景色
    ''' </summary>
    Public Property FalseColor As Color

    Private _FontName As String

    Public Property FontName As String
        Get
            Return _FontName
        End Get
        Set
            _FontName = Value
            Me.Invalidate()
        End Set
    End Property

    Private _FontSize As Single
    Public Property FontSize As Single
        Get
            Return _FontSize
        End Get
        Set
            _FontSize = Value
            Me.Invalidate()
        End Set
    End Property

    Private _FontStyle As FontStyle

    Public Property FontStyle As FontStyle
        Get
            Return _FontStyle
        End Get
        Set
            _FontStyle = Value
            Me.Invalidate()
        End Set
    End Property

    Sub New()
        ' デザイン時の初期値
        Me.Height = 20
        Me.Width = 60
        TrueColor = Color.Green
        FalseColor = Color.Red
        Me.Cursor = Cursors.Hand
    End Sub

    ''' <summary>
    ''' 円形の画像を描画する
    ''' </summary>
    ''' <param name="width"></param>
    ''' <param name="height"></param>
    ''' <param name="brush"></param>
    ''' <returns></returns>
    Private Function createCircleImage(width As Integer, height As Integer, brush As Brush) As Bitmap
        Dim canvas = New Bitmap(width, height)
        Dim x As Single = 0
        Dim y As Single = 0
        Dim rectWidth As Single = width - height

        Using g As Graphics = Graphics.FromImage(canvas)
            g.SmoothingMode = SmoothingMode.AntiAlias
            g.FillPie(brush, x, y, height, height, 90, 180)
            If rectWidth > 0 Then
                g.FillRectangle(brush, x + CSng(height) / 2 - 1, y, rectWidth, CSng(height))
            End If
            g.FillPie(brush, x + rectWidth - 2, y, height, height, 270, 180)
        End Using

        Return canvas
    End Function

    ''' <summary>
    ''' 文字を描画する
    ''' </summary>
    ''' <param name="width"></param>
    ''' <param name="height"></param>
    ''' <param name="brush"></param>
    ''' <param name="measuredSize"></param>
    ''' <returns></returns>
    Private Function createTextImage(width As Integer, height As Integer, brush As Brush, ByRef measuredSize As Size) As Bitmap
        Dim canvas As New Bitmap(width, height)
        Using g As Graphics = Graphics.FromImage(canvas)
            g.SmoothingMode = SmoothingMode.AntiAlias
            Dim text As String
            If IsChecked Then
                text = TrueText
            Else
                text = FalseText
            End If

            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias
            Dim font As Font = Me.Font
            If Not String.IsNullOrWhiteSpace(FontName) And FontSize > 0 Then
                font = New Font(FontName, FontSize, FontStyle)
            End If
            g.DrawString(text, font, brush, 0, 0)
            measuredSize = TextRenderer.MeasureText(g, text, font, New Size(1000, 1000), TextFormatFlags.NoPadding)
        End Using
        Return canvas
    End Function

    Protected Overrides Sub OnPaint(pe As PaintEventArgs)

        Dim g As Graphics = pe.Graphics
        g.Clear(Control.DefaultBackColor)

        Dim circle As Bitmap = createCircleImage(CInt(Height * 0.9), CInt(Height * 0.9), Brushes.White)
        Dim top As Single = CSng(Height * 0.1 / 2.0)
        If IsChecked Then
            g.DrawImage(createCircleImage(Width, Height, New SolidBrush(TrueColor)), 0, 0)
            Dim left As Single = Width - Height
            g.DrawImage(circle, left, top)
            Dim textSize As Size
            Dim label As Bitmap = createTextImage(Width, Height, Brushes.White, textSize)
            g.DrawImage(label, CSng(Width - Height - textSize.Width) / 2, CSng(Height - textSize.Height) / 2)
        Else
            g.DrawImage(createCircleImage(Width, Height, New SolidBrush(FalseColor)), 0, 0)
            Dim left As Single = CSng(Height * 0.1)
            g.DrawImage(circle, left, top)
            Dim textSize As Size
            Dim label As Bitmap = createTextImage(Width, Height, Brushes.White, textSize)
            g.DrawImage(label, CSng(Width - Height - textSize.Width) / 2 + CSng(Height * 0.9), CSng(Height - textSize.Height) / 2)
        End If
    End Sub

    Protected Overrides Sub OnClick(e As EventArgs)
        IsChecked = Not IsChecked
        Me.Invalidate()
        MyBase.OnClick(e)
    End Sub



End Class
