Option Explicit On
Option Strict On

Imports System.Drawing
Imports System.Windows.Forms
Imports ControlAttachment.State

Namespace Activity
    Public Class DroppableAttachment
        Inherits NativeWindow

        Private _TargetControl As Control

        Private Const WM_PAINT = &HF

        Private _HighlightingAction As IHighlightingActionStrategy
        Private _canDrop As Boolean

        Public Property CanDrop As Boolean
            Get
                Return _canDrop
            End Get
            Set
                _canDrop = Value
                If _canDrop Then
                    _HighlightingAction?.BeginHighlight(_TargetControl)
                Else
                    _HighlightingAction?.EndHighlight(_TargetControl)
                End If
                _TargetControl.Invalidate()
            End Set
        End Property

        Public ReadOnly Property TargetControl As Control
            Get
                Return _TargetControl
            End Get
        End Property

        Public Sub New(targetControl As Control)
            _TargetControl = targetControl

            If _TargetControl.IsHandleCreated Then
                AssignHandle(_TargetControl.Handle)
            Else
                AddHandler _TargetControl.HandleCreated, AddressOf OnHandleCreated
            End If

            AddHandler _TargetControl.HandleDestroyed, AddressOf OnHandleDestroyed

        End Sub

        Public Sub New(targetControl As Control, highlightingAction As IHighlightingActionStrategy)
            MyClass.New(targetControl)
            _HighlightingAction = highlightingAction
        End Sub

        Public Overrides Sub ReleaseHandle()
            RemoveHandler _TargetControl.HandleCreated, AddressOf OnHandleCreated
            RemoveHandler _TargetControl.HandleDestroyed, AddressOf OnHandleDestroyed
            _TargetControl = Nothing

            MyBase.ReleaseHandle()
        End Sub


        Private Sub OnHandleCreated(sender As Object, e As EventArgs)
            AssignHandle(_TargetControl.Handle)
            RemoveHandler _TargetControl.HandleCreated, AddressOf OnHandleCreated
        End Sub

        Private Sub OnHandleDestroyed(sender As Object, e As EventArgs)
            ReleaseHandle()
        End Sub

        Protected Overrides Sub WndProc(ByRef m As Message)
            MyBase.WndProc(m)

            Select Case m.Msg
                Case WM_PAINT
                    If CanDrop Then
                        _HighlightingAction?.Highlight(_TargetControl)
                    End If
            End Select

        End Sub


        Public Function IsRegionEnterd(dropObjectBound As DragObjectBound) As Boolean

            ' クライアント座標で比較
            Dim clientPos = _TargetControl.Parent.PointToClient(Cursor.Position)
            Dim draggingBound = dropObjectBound.MoveToMousePoint(clientPos)

            If draggingBound.Right < _TargetControl.Left OrElse draggingBound.Left > _TargetControl.Right Then Return False
            If draggingBound.Bottom < _TargetControl.Top OrElse draggingBound.Top > _TargetControl.Bottom Then Return False

            Return True

        End Function

        Public Sub Drop(chiled As Control, dropPosition As Point)
            ' TODO ドロップ処理を汎用的に（ストラテジーに出す？）
            _TargetControl.Controls.Add(chiled)
            chiled.Location = dropPosition
            CanDrop = False
        End Sub

    End Class
End Namespace