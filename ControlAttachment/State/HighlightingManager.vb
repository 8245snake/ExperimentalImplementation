Option Explicit On
Option Strict On

Imports System.Windows.Forms
Imports ControlAttachment.Strategies

Namespace State

    Public Class HighlightingManager

        Private _HighlightingControl As Control
        Private _HighlightingAction As IHighlightingActionStrategy

        Public Property HighlightingControl As Control
            Get
                Return _HighlightingControl
            End Get
            Set

                If _HighlightingControl IsNot Nothing Then
                    ' ハイライト停止
                    _HighlightingAction?.EndHighlight(_HighlightingControl)
                End If

                _HighlightingControl = Value

                If _HighlightingControl IsNot Nothing Then
                    ' ハイライト開始
                    _HighlightingAction?.BeginHighlight(_HighlightingControl)
                End If
            End Set
        End Property

        Public Sub New(highlightingAction As IHighlightingActionStrategy)
            _HighlightingAction = highlightingAction
        End Sub

    End Class
End Namespace