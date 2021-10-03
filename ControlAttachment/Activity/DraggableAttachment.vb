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
        Private _Parent As Control
        Private _TopParent As Control

        Private _IsGrabbed As Boolean = False
        Private _LocalPosition As Point
        Private _BeforeChildIndex As Integer

        Private Const WM_PAINT = &HF
        Private Const WM_NCPAINT = &H85
        Private Const WM_MOUSEMOVE = &H200
        Private Const WM_LBUTTONDOWN = &H201
        Private Const WM_LBUTTONUP = &H202

        Private DropTargets As List(Of DroppableAttachment) = New List(Of DroppableAttachment)()

        Private _HighlightingAction As IHighlightingActionStrategy

        Private _ChildNativeWindows As List(Of ChildNativeWindow) = New List(Of ChildNativeWindow)()

        Public Sub New(targetControl As Control)
            _TargetControl = targetControl

            AddHandler _TargetControl.HandleCreated, AddressOf OnHandleCreated
            AddHandler _TargetControl.HandleDestroyed, AddressOf OnHandleDestroyed

            If _TargetControl.IsHandleCreated Then
                AssignHandle(_TargetControl.Handle)
            End If

            ' 最上位の親を探す
            Dim parent As Control = _TargetControl.Parent
            While parent IsNot Nothing
                _TopParent = parent
                parent = parent.Parent
            End While

            ' 子コントロールのウィンドウメッセージを受け取るため
            For Each control As Control In _TargetControl.Controls
                _ChildNativeWindows.Add(New ChildNativeWindow(_TargetControl.Handle, control))
            Next

        End Sub

        Public Sub New(targetControl As Control, highlightingAction As IHighlightingActionStrategy)
            MyClass.New(targetControl)
            _HighlightingAction = highlightingAction
            _HighlightingAction?.BeginHighlight(_TargetControl)

        End Sub

        Public Overrides Sub ReleaseHandle()
            RemoveHandler _TargetControl.HandleCreated, AddressOf OnHandleCreated
            RemoveHandler _TargetControl.HandleDestroyed, AddressOf OnHandleDestroyed

            _TargetControl = Nothing
            _TopParent = Nothing
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
                Case WM_MOUSEMOVE
                    If Not _IsGrabbed Then Return
                    OnMouseMove()
                Case WM_LBUTTONDOWN
                    OnMouseDown()
                Case WM_LBUTTONUP
                    If Not _IsGrabbed Then Return
                    OnMouseUp()
                Case WM_PAINT, WM_NCPAINT
                    If Not _IsGrabbed Then Return
                    _HighlightingAction?.Highlight(_TargetControl)
            End Select

        End Sub

        Private Sub OnMouseDown()
            ' 位置を保存
            _LocalPosition = _TargetControl.PointToClient(Cursor.Position)
            _BeforeChildIndex = _TargetControl.Parent.Controls.GetChildIndex(_TargetControl)
            ' 親を保存
            _Parent = _TargetControl.Parent
            _Parent.Controls.Remove(_TargetControl)
            _TopParent.Controls.Add(_TargetControl)
            ' 一番上に持っていく
            _TopParent.Controls.SetChildIndex(_TargetControl, 0)
            ' マウスの位置に動かしておく
            OnMouseMove()
            ' フラグセット
            _IsGrabbed = True
            NotifyReadyToDrop(True)
            _TargetControl.Invalidate()
        End Sub

        Private Sub OnMouseUp()

            Dim dest = DropTargets.Where(Function(item) item.CanDrop).FirstOrDefault()
            If dest IsNot Nothing Then
                Dim screenPos = Cursor.Position
                Dim clientPos = dest.TargetControl.PointToClient(screenPos)
                dest.Drop(_TargetControl, clientPos)
            Else
                ' だめなら戻す
                _Parent.Controls.Add(_TargetControl)
                _Parent.Controls.SetChildIndex(_TargetControl, _BeforeChildIndex)
            End If

            _IsGrabbed = False
            NotifyReadyToDrop(False)
            _HighlightingAction?.EndHighlight(_TargetControl)
        End Sub

        Private Sub OnMouseMove()
            ' マウスの動きに追従させる
            Dim screenPos = Cursor.Position
            Dim clientPos = _TopParent.PointToClient(screenPos)
            clientPos.X -= _LocalPosition.X
            clientPos.Y -= _LocalPosition.Y
            _TargetControl.Location = clientPos
        End Sub


        ''' <summary>
        ''' ドロップ先コントロールのアタッチメントを追加すｓる
        ''' </summary>
        ''' <param name="drop"></param>
        Public Sub AddDropTarget(drop As DroppableAttachment)
            DropTargets.Add(drop)
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