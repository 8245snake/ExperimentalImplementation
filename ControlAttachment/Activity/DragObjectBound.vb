Option Explicit On
Option Strict On

Imports System.Drawing

Namespace Activity
    Public Structure DragObjectBound

        Public X As Integer
        Public Y As Integer
        Public Width As Integer
        Public Height As Integer
        ''' <summary>
        ''' クリックした左端からの位置
        ''' </summary>
        Public OffsetLeft As Integer
        ''' <summary>
        ''' クリックした上端からの位置
        ''' </summary>
        Public OffsetTop As Integer

        Public ReadOnly Property Left As Integer
            Get
                Return Me.X
            End Get
        End Property

        Public ReadOnly Property Right As Integer
            Get
                Return Me.X + Me.Width
            End Get
        End Property

        Public ReadOnly Property Top As Integer
            Get
                Return Me.Y
            End Get
        End Property

        Public ReadOnly Property Bottom As Integer
            Get
                Return Me.Y + Me.Height
            End Get
        End Property

        Public Property Location As Point
            Get
                Return New Point(Me.X, Me.Y)
            End Get
            Set(value As Point)
                Me.X = value.X
                Me.Y = value.Y
            End Set
        End Property

        Public Sub New(location As Point, size As Size, offsetLeft As Integer, offsetTop As Integer)
            Me.X = location.X
            Me.Y = location.Y
            Me.Width = size.Width
            Me.Height = size.Height
            Me.OffsetLeft = offsetLeft
            Me.OffsetTop = offsetTop
        End Sub

        Public Sub New(x As Integer, y As Integer, width As Integer, height As Integer, offsetLeft As Integer, offsetTop As Integer)
            Me.X = x
            Me.Y = y
            Me.Width = width
            Me.Height = height
            Me.OffsetLeft = offsetLeft
            Me.OffsetTop = offsetTop
        End Sub

        Public Function MoveToMousePoint(mousePoint As Point) As DragObjectBound
            Dim clone = New DragObjectBound(X, Y, Width, Height, OffsetLeft, OffsetTop)
            clone.X = mousePoint.X - OffsetLeft
            clone.Y = mousePoint.Y - OffsetTop
            Return clone
        End Function

    End Structure
End Namespace