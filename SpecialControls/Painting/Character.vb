Option Explicit On
Option Strict On

Namespace Painting

    ''' <summary>
    ''' 文字列の描画用オブジェクト
    ''' </summary>
    Public Class Character
        Inherits Shape

        Public Enum HorizontalAlignmentType
            Left
            Center
            Right
        End Enum

        Public Enum VerticalAlignmentType
            Top
            Center
            Bottom
        End Enum

        Private _Font As Font
        Private _Text As String

        Public Property Font As Font
            Get
                Return _Font
            End Get
            Set
                _Font = Value
            End Set
        End Property

        Public Property Text As String
            Get
                Return _Text
            End Get
            Set
                _Text = Value
            End Set
        End Property

        Public Property TextHorizontalAlignment As HorizontalAlignmentType
        Public Property TextVerticalAlignment As VerticalAlignmentType

        Public Overrides Sub Draw(ByRef g As Graphics)
            MyBase.Draw(g)
            g.TextRenderingHint = Drawing.Text.TextRenderingHint.AntiAlias

            Dim textSize As Size = GetMeasuredSize(g)
            Dim left As Single = 0.0
            Select Case TextHorizontalAlignment
                Case HorizontalAlignmentType.Left
                    ' そのまま
                Case HorizontalAlignmentType.Center
                    left = CSng(Me.Width - textSize.Width) / 2
                Case HorizontalAlignmentType.Right
                    left = Me.Width - textSize.Width
                Case Else
            End Select

            Dim top As Single = 0.0
            Select Case TextVerticalAlignment
                Case VerticalAlignmentType.Top
                    ' そのまま
                Case VerticalAlignmentType.Center
                    top = CSng(Me.Height - textSize.Height) / 2
                Case VerticalAlignmentType.Bottom
                    top = Me.Height - textSize.Height
            End Select

            g.DrawString(Text, Font, New SolidBrush(BrushColor), left + Me.X, top + Me.Y)
        End Sub

        Public Function GetMeasuredSize(ByRef g As Graphics) As Size
            Dim size As Size = TextRenderer.MeasureText(g, Text, Font, New Size(Integer.MaxValue, Integer.MaxValue), TextFormatFlags.NoPadding)
            size.Width += CInt(size.Width * 0.05)
            Return size
        End Function

    End Class

End Namespace

