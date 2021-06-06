Option Explicit On
Option Strict On
Imports System.Drawing.Drawing2D

Namespace Painting

    Public MustInherit Class Shape

        Public Enum ColoringType
            Fill
            Outline
        End Enum

        Private _Rect As Rectangle
        Private _Coloring As ColoringType
        Private _BrushColor As Color
        Private _BorderColor As Color
        Private _BorderWidth As Single
        Private _Shapes As List(Of Shape) = New List(Of Shape)()

        Public Property Coloring As ColoringType
            Get
                Return _Coloring
            End Get
            Set(value As ColoringType)
                _Coloring = value
            End Set
        End Property

        Public Property BrushColor As Color
            Get
                Return _BrushColor
            End Get
            Set
                _BrushColor = Value
            End Set
        End Property

        Public Property BorderColor As Color
            Get
                Return _BorderColor
            End Get
            Set
                _BorderColor = Value
            End Set
        End Property

        Public Property BorderWidth As Single
            Get
                Return _BorderWidth
            End Get
            Set
                _BorderWidth = Value
            End Set
        End Property

        Public Property Rect As Rectangle
            Get
                Return _Rect
            End Get
            Set
                _Rect = Value
            End Set
        End Property

        Public Property X As Integer
            Get
                Return _Rect.X
            End Get
            Set
                _Rect.X = Value
            End Set
        End Property

        Public Property Y As Integer
            Get
                Return _Rect.Y
            End Get
            Set
                _Rect.Y = Value
            End Set
        End Property

        Public Property Width As Integer
            Get
                Return _Rect.Width
            End Get
            Set
                _Rect.Width = Value
            End Set
        End Property

        Public Property Height As Integer
            Get
                Return _Rect.Height
            End Get
            Set
                _Rect.Height = Value
            End Set
        End Property

        Public Property Shapes As List(Of Shape)
            Get
                Return _Shapes
            End Get
            Set(value As List(Of Shape))
                _Shapes = value
            End Set
        End Property


        Public Overridable Sub Draw(ByRef g As Graphics)
            g.SmoothingMode = SmoothingMode.AntiAlias
            For Each shape As Shape In _Shapes
                shape.Draw(g)
            Next
        End Sub

        Public Overridable Function DeepCopy() As Shape
            Dim clone As Shape = DirectCast(Me.MemberwiseClone(), Shape)
            Dim shapes As List(Of Shape) = New List(Of Shape)()
            For Each item As Shape In _Shapes
                shapes.Add(item.DeepCopy())
            Next
            clone.Shapes = shapes
            Return clone
        End Function

    End Class

End Namespace