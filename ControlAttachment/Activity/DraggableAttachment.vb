Option Explicit On
Option Strict On

Imports System.Windows.Forms
Imports ControlAttachment.State

Namespace Activity

    Public Class DraggableAttachment
        Inherits NativeWindow

        Private _TargetControl As Control

        Private _IsGrabbed As Boolean = False

        Private Const WM_PAINT = &HF
        Private Const WM_NCPAINT = &H85
        Private Const WM_MOUSEMOVE = &H200
        Private Const WM_LBUTTONDOWN = &H201
        Private Const WM_LBUTTONUP = &H202

        Private _HighlightingAction As IHighlightingActionStrategy
        Private _DragAction As IDragActionStrategy
        Private _ChildNativeWindows As List(Of ChildNativeWindow)

        Public Sub New(targetControl As Control, dragActionStrategy As IDragActionStrategy, Optional isHookChildren As Boolean = True)
            _TargetControl = targetControl
            _TargetControl.Cursor = Cursors.CustomCursor.Hand_Open
            _DragAction = dragActionStrategy

            If _TargetControl.IsHandleCreated Then
                AssignHandle(_TargetControl.Handle)
            Else
                AddHandler _TargetControl.HandleCreated, AddressOf OnHandleCreated
            End If

            AddHandler _TargetControl.HandleDestroyed, AddressOf OnHandleDestroyed

            ' 最上位の親を探す
            Dim parent As Control = _TargetControl.Parent
            While parent IsNot Nothing
                _DragAction.TopParent = parent
                parent = parent.Parent
            End While

            ' 子コントロールのウィンドウメッセージを受け取る
            If isHookChildren Then
                _ChildNativeWindows = New List(Of ChildNativeWindow)()
                For Each control As Control In _TargetControl.Controls
                    _ChildNativeWindows.Add(New ChildNativeWindow(_TargetControl.Handle, control))
                Next
            End If

        End Sub


        Public Sub New(targetControl As Control, dragActionStrategy As IDragActionStrategy, highlightingAction As IHighlightingActionStrategy)
            MyClass.New(targetControl, dragActionStrategy)
            _HighlightingAction = highlightingAction
        End Sub

        Public Overrides Sub ReleaseHandle()
            RemoveHandler _TargetControl.HandleCreated, AddressOf OnHandleCreated
            RemoveHandler _TargetControl.HandleDestroyed, AddressOf OnHandleDestroyed

            _TargetControl = Nothing
            _DragAction = Nothing
            _ChildNativeWindows?.Clear()

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
                Case WM_LBUTTONDOWN
                    ' ドラッグ開始
                    _DragAction.BiginDrag()
                    _IsGrabbed = True
                    ' ドラッグ対象のハイライト開始
                    _HighlightingAction?.BeginHighlight(_TargetControl)

                Case WM_MOUSEMOVE
                    If Not _IsGrabbed Then Return
                    _DragAction.DragMoving()

                Case WM_LBUTTONUP
                    If Not _IsGrabbed Then Return
                    ' ドラッグ終了
                    _DragAction.EndDrag()
                    _IsGrabbed = False
                    ' ドラッグ対象のハイライト終了
                    _HighlightingAction?.EndHighlight(_TargetControl)
                    _TargetControl.Invalidate()

                Case WM_PAINT, WM_NCPAINT
                    If Not _IsGrabbed Then Return
                    ' ドラッグ対象のハイライト
                    _HighlightingAction?.Highlight(_TargetControl)

            End Select

        End Sub

        Public Sub AddDropTarget(droppableAttachment As DroppableAttachment)
            _DragAction.DropTargets.Add(droppableAttachment)
        End Sub

    End Class


End Namespace