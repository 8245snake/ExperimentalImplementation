Option Explicit On
Option Strict On

Imports System.Drawing
Imports System.Windows.Forms
Imports ControlAttachment.Activity

Namespace Strategies

    Public Class StandardDragActionStrategy
        Implements IDragActionStrategy

        Public Property TopParent As Control Implements IDragActionStrategy.TopParent
        Public Property DropTargets As List(Of DroppableAttachment) Implements IDragActionStrategy.DropTargets

        Private _TargetControl As Control
        Private _Parent As Control
        Private _LocalPosition As Point
        Private _BeforePosition As Point
        Private _BeforeChildIndex As Integer

        Public Sub New(targetControl As Control)
            _TargetControl = targetControl
            DropTargets = New List(Of DroppableAttachment)()
        End Sub

        Public Sub BiginDrag() Implements IDragActionStrategy.BiginDrag
            ' 位置を保存
            _BeforeChildIndex = _TargetControl.Parent.Controls.GetChildIndex(_TargetControl)
            _BeforePosition = _TargetControl.Location
            _LocalPosition = _TargetControl.PointToClient(Cursor.Position)

            ' 親を保存
            _Parent = _TargetControl.Parent
            _Parent.Controls.Remove(_TargetControl)
            TopParent.Controls.Add(_TargetControl)

            ' 一番上に持っていく
            TopParent.Controls.SetChildIndex(_TargetControl, 0)

            ' マウスの位置に動かしておく
            DragMoving()
            NotifyReadyToDrop(True)
            _TargetControl.Invalidate()

            _TargetControl.Cursor = Cursors.CustomCursor.Hand_Close
        End Sub

        Public Sub DragMoving() Implements IDragActionStrategy.DragMoving
            ' マウスの動きに追従させる
            Dim screenPos = Cursor.Position
            Dim clientPos = TopParent.PointToClient(screenPos)
            clientPos.X -= _LocalPosition.X
            clientPos.Y -= _LocalPosition.Y
            _TargetControl.Location = clientPos
        End Sub

        Public Sub EndDrag() Implements IDragActionStrategy.EndDrag

            Dim dest = DropTargets.FirstOrDefault(Function(item) item.CanDrop)
            If dest IsNot Nothing Then
                ' ドロップ先があればドロップする
                Dim screenPos = Cursor.Position
                Dim clientPos = dest.TargetControl.PointToClient(screenPos)
                clientPos.X -= _LocalPosition.X
                clientPos.Y -= _LocalPosition.Y
                dest.Drop(_TargetControl, clientPos)
            Else
                ' だめなら戻す
                _Parent.Controls.Add(_TargetControl)
                _TargetControl.Location = _BeforePosition
                _Parent.Controls.SetChildIndex(_TargetControl, _BeforeChildIndex)
            End If

            NotifyReadyToDrop(False)

            _TargetControl.Cursor = Cursors.CustomCursor.Hand_Open
        End Sub


        ''' <summary>
        ''' ドロップ先コントロールにドロップ待受状態にするかを通知する
        ''' </summary>
        ''' <param name="isReady">ドロップ待受状態にするか</param>
        Private Sub NotifyReadyToDrop(isReady As Boolean)
            Dim padding As Padding

            If isReady Then
                padding = New Padding(_LocalPosition.X, _LocalPosition.Y, 0, 0)
                padding.Right = _TargetControl.Width - padding.Left
                padding.Bottom = _TargetControl.Height - padding.Top
            End If

            For Each attachment As DroppableAttachment In DropTargets
                If attachment.TargetControl Is Me._Parent Then
                    ' 自分が所属するフレームにはドロップできなくする
                    attachment.ReadyToDrop = False
                Else
                    attachment.ReadyToDrop = isReady
                End If

                attachment.DropObjectPadding = padding
            Next
        End Sub

    End Class
End Namespace