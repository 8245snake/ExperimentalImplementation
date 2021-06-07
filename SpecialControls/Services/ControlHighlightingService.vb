Option Explicit On
Option Strict On

Imports SpecialControls.Painting
Imports SpecialControls.Win32

Namespace Services

    ''' <summary>
    ''' コントロールをハイライトする機能のマネージャクラス
    ''' </summary>
    Public Class ControlHighlightingService
        Private _HighlightingControl As Control

        ''' <summary>
        ''' ハイライトするコントロールが変化したときに発火する
        ''' </summary>
        Public Event HighlightingControlChanged As EventHandler

        Public Property HighlightLineColor As Color = Color.Red
        Public Property HighlightLineWidth As Integer = 4

        ''' <summary>
        ''' ハイライト対象のコントロール
        ''' </summary>
        Public Property HighlightingControl As Control
            Get
                Return _HighlightingControl
            End Get
            Set
                If _HighlightingControl IsNot Value Then
                    If _HighlightingControl IsNot Nothing Then
                        RemoveHandler _HighlightingControl.Parent.Paint, AddressOf TargetPaint
                        _HighlightingControl.Parent.Invalidate()
                    End If

                    _HighlightingControl = Value

                    If _HighlightingControl IsNot Nothing Then
                        AddHandler _HighlightingControl.Parent.Paint, AddressOf TargetPaint
                        _HighlightingControl.Parent.Invalidate()
                    End If

                    RaiseEvent HighlightingControlChanged(Me, New EventArgs())

                End If
            End Set
        End Property

        Private Sub TargetPaint(sender As Object, e As PaintEventArgs)
            Dim tetragon As Tetragon = New Tetragon()
            tetragon.X = _HighlightingControl.Location.X - 2
            tetragon.Y = _HighlightingControl.Location.Y - 2
            tetragon.Width = _HighlightingControl.Size.Width + HighlightLineWidth
            tetragon.Height = _HighlightingControl.Size.Height + HighlightLineWidth
            tetragon.Coloring = Shape.ColoringType.Outline
            tetragon.BorderColor = HighlightLineColor
            tetragon.BorderWidth = HighlightLineWidth
            tetragon.Draw(e.Graphics)
        End Sub

    End Class
End Namespace


