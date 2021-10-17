
Imports System.Runtime.InteropServices
Imports System.Windows.Forms

Namespace Activity

    ''' <summary>
    ''' 子コントロールのウィンドウメッセージを親に伝えるためのクラス
    ''' </summary>
    Friend Class ChildNativeWindow
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
End Namespace