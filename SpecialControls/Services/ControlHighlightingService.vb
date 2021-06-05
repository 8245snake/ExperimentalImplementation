Option Explicit On
Option Strict On

Imports SpecialControls.Painting

Namespace Services

    ''' <summary>
    ''' コントロールをハイライトする機能のマネージャクラス（未完成）
    ''' </summary>
    Public Class ControlHighlightingService
        Private _HighlightingControl As Control

        ''' <summary>
        ''' ハイライトするコントロールが変化したときに発火する
        ''' </summary>
        Public Event HighlightingControlChanged As EventHandler

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
                        RemoveHandler _HighlightingControl.Paint, AddressOf TargetPaint
                        _HighlightingControl.Invalidate()
                    End If
                    _HighlightingControl = Value
                    AddHandler _HighlightingControl.Paint, AddressOf TargetPaint
                    _HighlightingControl.Invalidate()
                    RaiseEvent HighlightingControlChanged(Me, New EventArgs())
                End If
            End Set
        End Property

        Private Sub TargetPaint(sender As Object, e As PaintEventArgs)
            Dim tetragon As Tetragon = New Tetragon()
            tetragon.X = 0
            tetragon.Y = 0
            tetragon.Width = _HighlightingControl.Size.Width - 1
            tetragon.Height = _HighlightingControl.Size.Height - 1
            tetragon.Coloring = Shape.ColoringType.Outline
            tetragon.BorderColor = Color.Red
            tetragon.BorderWidth = 4
            tetragon.Draw(e.Graphics)
        End Sub

    End Class
End Namespace


