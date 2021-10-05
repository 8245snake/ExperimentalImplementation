Option Explicit On
Option Strict On

Imports System.Drawing
Imports System.Runtime.InteropServices
Imports System.Windows.Forms
Imports ControlAttachment.Strategies

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
        Private _ChildNativeWindows As List(Of ChildNativeWindow) = New List(Of ChildNativeWindow)()

        Public Sub New(targetControl As Control, dragActionStrategy As IDragActionStrategy)
            _TargetControl = targetControl
            _TargetControl.Cursor = Cursors.CustomCursor.Hand_Open
            _DragAction = dragActionStrategy

            AddHandler _TargetControl.HandleCreated, AddressOf OnHandleCreated
            AddHandler _TargetControl.HandleDestroyed, AddressOf OnHandleDestroyed

            If _TargetControl.IsHandleCreated Then
                AssignHandle(_TargetControl.Handle)
            End If

            ' 最上位の親を探す
            Dim parent As Control = _TargetControl.Parent
            While parent IsNot Nothing
                _DragAction.TopParent = parent
                parent = parent.Parent
            End While

            ' 子コントロールのウィンドウメッセージを受け取るため
            For Each control As Control In _TargetControl.Controls
                _ChildNativeWindows.Add(New ChildNativeWindow(_TargetControl.Handle, control))
            Next

        End Sub



        Public Sub New(targetControl As Control, dragActionStrategy As IDragActionStrategy, highlightingAction As IHighlightingActionStrategy)
            MyClass.New(targetControl, dragActionStrategy)
            _HighlightingAction = highlightingAction
            _HighlightingAction?.BeginHighlight(_TargetControl)

        End Sub

        Public Overrides Sub ReleaseHandle()
            RemoveHandler _TargetControl.HandleCreated, AddressOf OnHandleCreated
            RemoveHandler _TargetControl.HandleDestroyed, AddressOf OnHandleDestroyed

            _TargetControl = Nothing
            _DragAction = Nothing
            _ChildNativeWindows.Clear()

            MyBase.ReleaseHandle()
        End Sub


        Private Sub OnHandleCreated(sender As Object, e As EventArgs)
            Dim ctrl = TryCast(sender, Control)
            AssignHandle(ctrl.Handle)
        End Sub

        Private Sub OnHandleDestroyed(sender As Object, e As EventArgs)
            ReleaseHandle()
        End Sub

        Protected Overrides Sub WndProc(ByRef m As Message)
            MyBase.WndProc(m)

            Select Case m.Msg
                Case WM_LBUTTONDOWN
                    _DragAction.BiginDrag()
                    ' フラグセット
                    _IsGrabbed = True

                Case WM_MOUSEMOVE
                    If Not _IsGrabbed Then Return
                    _DragAction.DragMoving()

                Case WM_LBUTTONUP
                    If Not _IsGrabbed Then Return
                    _DragAction.EndDrag()
                    _IsGrabbed = False
                    _HighlightingAction?.EndHighlight(_TargetControl)

                Case WM_PAINT, WM_NCPAINT
                    If Not _IsGrabbed Then Return
                    _HighlightingAction?.Highlight(_TargetControl)

            End Select

        End Sub

        Public Sub AddDropTarget(droppableAttachment As DroppableAttachment)
            _DragAction.DropTargets.Add(droppableAttachment)
        End Sub


        ''' <summary>
        ''' 子コントロールのウィンドウメッセージを親に伝えるためのインナークラス
        ''' </summary>
        Private Class ChildNativeWindow
            Inherits NativeWindow

            Private _ChildControl As Control
            Private _ParentHandle As IntPtr

            <DllImport("user32.dll", CharSet:=CharSet.Auto)>
            Private Shared Function SendMessage(ByVal hWnd As IntPtr, ByVal Msg As Integer, ByVal wParam As IntPtr, ByRef lParam As IntPtr) As IntPtr
            End Function

            Private Const WM_PAINT = &HF
            Private Const WM_NCPAINT = &H85
            Private Const WM_MOUSEMOVE = &H200
            Private Const WM_LBUTTONDOWN = &H201
            Private Const WM_LBUTTONUP = &H202

            Public Sub New(handle As IntPtr, childControl As Control)
                _ParentHandle = handle
                _ChildControl = childControl

                AddHandler _ChildControl.HandleCreated, AddressOf OnHandleCreated
                AddHandler _ChildControl.HandleDestroyed, AddressOf OnHandleDestroyed

                If _ChildControl.IsHandleCreated Then
                    AssignHandle(_ChildControl.Handle)
                End If

            End Sub
            Public Overrides Sub ReleaseHandle()
                RemoveHandler _ChildControl.HandleCreated, AddressOf OnHandleCreated
                RemoveHandler _ChildControl.HandleDestroyed, AddressOf OnHandleDestroyed

                _ChildControl = Nothing

                MyBase.ReleaseHandle()
            End Sub

            Private Sub OnHandleCreated(sender As Object, e As EventArgs)
                Dim ctrl = TryCast(sender, Control)
                AssignHandle(ctrl.Handle)
            End Sub

            Private Sub OnHandleDestroyed(sender As Object, e As EventArgs)
                ReleaseHandle()
            End Sub

            Protected Overrides Sub WndProc(ByRef m As Message)
                MyBase.WndProc(m)
                ' 親に受け流す
                Select Case m.Msg
                    Case WM_MOUSEMOVE, WM_LBUTTONDOWN, WM_LBUTTONUP, WM_PAINT, WM_NCPAINT
                        SendMessage(_ParentHandle, m.Msg, m.WParam, m.LParam)
                End Select
            End Sub
        End Class

    End Class


End Namespace