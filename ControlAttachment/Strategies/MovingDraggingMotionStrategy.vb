Option Explicit On
Option Strict On

Imports System.Drawing
Imports System.Windows.Forms
Imports ControlAttachment.Activity

Namespace Strategies

    ''' <summary>
    ''' ドラッグ移動のストラテジ。
    ''' 複製せず常に１つのオブジェクトを移動させる操作に使用する。
    ''' </summary>
    Public Class MovingDraggingMotionStrategy
        Implements IDraggingMotionStrategy

        Public Property TopParent As Control Implements IDraggingMotionStrategy.TopParent
        Public Property DropTargets As List(Of DroppableAttachment) Implements IDraggingMotionStrategy.DropTargets

        Private _TargetControl As Control
        Private _Parent As Control
        Private _BeforeBound As DragObjectBound
        Private _BeforeChildIndex As Integer

        Public Sub New(targetControl As Control)
            _TargetControl = targetControl
            DropTargets = New List(Of DroppableAttachment)()
        End Sub

        Public Sub BeginDrag() Implements IDraggingMotionStrategy.BeginDrag

            ' 位置を保存
            _BeforeChildIndex = _TargetControl.Parent.Controls.GetChildIndex(_TargetControl)
            Dim localPos = _TargetControl.PointToClient(Cursor.Position)
            _BeforeBound = New DragObjectBound(_TargetControl.Location, _TargetControl.Size, localPos.X, localPos.Y)

            ' 親を保存
            _Parent = _TargetControl.Parent
            _Parent.Controls.Remove(_TargetControl)

            ' トップレベルのコントロール配下に置き、一番上に持っていく。ちらつくのを防ぐため一回画面外の座標に飛ばす
            _TargetControl.Location = New Point(-100, -100)
            TopParent.Controls.Add(_TargetControl)
            TopParent.Controls.SetChildIndex(_TargetControl, 0)

            ' マウスポインタの位置に持ってくる
            DragMoving()
            _TargetControl.Invalidate()
            _TargetControl.Cursor = Cursors.CustomCursor.Hand_Close
        End Sub

        Public Sub DragMoving() Implements IDraggingMotionStrategy.DragMoving
            ' マウスの動きに追従させる
            Dim clientPos = TopParent.PointToClient(Cursor.Position)
            clientPos.X -= _BeforeBound.OffsetLeft
            clientPos.Y -= _BeforeBound.OffsetTop
            _TargetControl.Location = clientPos

            ' ドロップ可能なコントロールを探す
            Dim targetFound As Boolean = False
            For Each target As DroppableAttachment In DropTargets
                If targetFound Then
                    ' ドロップ先が見つかった以降は全てドロップ不可
                    target.CanDrop = False
                Else
                    ' ドロップエリア内に入っているか判定
                    targetFound = target.IsRegionEnterd(_BeforeBound)
                    target.CanDrop = targetFound
                End If
            Next

        End Sub

        Public Sub EndDrag() Implements IDraggingMotionStrategy.EndDrag

            Dim dest = DropTargets.FirstOrDefault(Function(item) item.CanDrop)
            If dest IsNot Nothing Then
                ' ドロップ先があればドロップする
                Dim screenPos = Cursor.Position
                Dim clientPos = dest.TargetControl.PointToClient(screenPos)
                clientPos.X -= _BeforeBound.OffsetLeft
                clientPos.Y -= _BeforeBound.OffsetTop
                dest.Drop(_TargetControl, clientPos)
            Else
                ' だめなら戻す
                _Parent.Controls.Add(_TargetControl)
                _TargetControl.Location = _BeforeBound.Location
                _Parent.Controls.SetChildIndex(_TargetControl, _BeforeChildIndex)
            End If

            _TargetControl.Cursor = Cursors.CustomCursor.Hand_Open
        End Sub


    End Class
End Namespace