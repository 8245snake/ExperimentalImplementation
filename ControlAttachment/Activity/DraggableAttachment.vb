Option Explicit On
Option Strict On

Imports System.ComponentModel
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
        Private Const WM_MOUSELEAVE = &H2A3
        Private Const WM_LBUTTONDOWN = &H201
        Private Const WM_LBUTTONUP = &H202

        Private _highlightingStrategy As IHighlightingStrategy
        Private _draggingMotion As IDraggingMotionStrategy
        Private _ChildNativeWindows As List(Of ChildNativeWindow)

        Public Event BeforeDrag As EventHandler(Of CancelEventArgs)

        Public Sub New(targetControl As Control, draggingMotionStrategy As IDraggingMotionStrategy, Optional isHookChildren As Boolean = True)
            _TargetControl = targetControl
            _TargetControl.Cursor = Cursors.CustomCursor.Hand_Open
            _draggingMotion = draggingMotionStrategy

            If _TargetControl.IsHandleCreated Then
                AssignHandle(_TargetControl.Handle)
            Else
                AddHandler _TargetControl.HandleCreated, AddressOf OnHandleCreated
            End If

            AddHandler _TargetControl.HandleDestroyed, AddressOf OnHandleDestroyed

            ' 最上位の親を探す
            Dim parent As Control = _TargetControl.Parent
            While parent IsNot Nothing
                _draggingMotion.TopParent = parent
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


        Public Sub New(targetControl As Control, draggingMotionStrategy As IDraggingMotionStrategy, highlightingStrategy As IHighlightingStrategy)
            MyClass.New(targetControl, draggingMotionStrategy)
            _highlightingStrategy = highlightingStrategy
        End Sub

        Public Overrides Sub ReleaseHandle()
            RemoveHandler _TargetControl.HandleCreated, AddressOf OnHandleCreated
            RemoveHandler _TargetControl.HandleDestroyed, AddressOf OnHandleDestroyed

            _TargetControl = Nothing
            _draggingMotion = Nothing
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
                    ' ドラッグ開始してよいか判定
                    Dim args As CancelEventArgs = New CancelEventArgs(False)
                    RaiseEvent BeforeDrag(Me, args)
                    If args.Cancel Then Return
                    ' ドラッグ開始
                    _draggingMotion.BeginDrag()
                    _IsGrabbed = True
                    ' ドラッグ対象のハイライト開始
                    _highlightingStrategy?.BeginHighlight(_TargetControl)

                Case WM_MOUSEMOVE
                    If Not _IsGrabbed Then Return
                    _draggingMotion.DragMoving()

                Case WM_LBUTTONUP, WM_MOUSELEAVE
                    If Not _IsGrabbed Then Return
                    ' ドラッグ終了
                    _draggingMotion.EndDrag()
                    _IsGrabbed = False
                    ' ドラッグ対象のハイライト終了
                    _highlightingStrategy?.EndHighlight(_TargetControl)
                    _TargetControl.Invalidate()

                Case WM_PAINT, WM_NCPAINT
                    If Not _IsGrabbed Then Return
                    ' ドラッグ対象のハイライト
                    _highlightingStrategy?.Highlight(_TargetControl)

            End Select

        End Sub

        Public Sub AddDropTarget(droppableAttachment As DroppableAttachment)
            _draggingMotion.DropTargets.Add(droppableAttachment)
        End Sub

    End Class


End Namespace