Option Explicit On
Option Strict On

Imports System.Windows.Forms

Namespace State

    Friend Class EventSessionListener
        Inherits NativeWindow
        Implements IControlEventSession

        Private Const WM_KILLFOCUS = 8
        Private Const WM_LBUTTONUP = 514


        Private _TargetControl As Control
        Private _IsRunning As Boolean
        Private _StartTime As DateTime
        Private _Messages As Integer() = {}

        Public ReadOnly Property StartTimeProperty As Date
            Get
                Return _StartTime
            End Get
        End Property


        Public ReadOnly Property IsRunning As Boolean Implements IControlEventSession.IsRunning
            Get
                Return _IsRunning
            End Get
        End Property

        Public ReadOnly Property StartTime As Date Implements IControlEventSession.StartTime
            Get
                Return _StartTime
            End Get
        End Property

        Private ReadOnly Property ISessionManegaer_Handle As IntPtr Implements IControlEventSession.Handle
            Get
                Return _TargetControl.Handle
            End Get
        End Property

        Public Sub New(targetControl As Control)
            _TargetControl = targetControl
            AddHandler _TargetControl.HandleCreated, AddressOf OnHandleCreated
            AddHandler _TargetControl.HandleDestroyed, AddressOf OnHandleDestroyed

            If TypeOf _TargetControl Is Button OrElse TypeOf _TargetControl Is CheckBox Then
                ' ボタンは押したときにセッションスタート
                _Messages = {WM_LBUTTONUP}
            Else
                ' その他はフォーカス外したときとする
                _Messages = {WM_KILLFOCUS}
            End If

            _IsRunning = False
        End Sub

        Private Sub OnHandleCreated(sender As Object, e As EventArgs)
            Dim control = TryCast(sender, Control)
            AssignHandle(control.Handle)
        End Sub

        Private Sub OnHandleDestroyed(sender As Object, e As EventArgs)
            ReleaseHandle()
        End Sub

        Public Overrides Sub ReleaseHandle()
            RemoveHandler _TargetControl.HandleCreated, AddressOf OnHandleCreated
            RemoveHandler _TargetControl.HandleDestroyed, AddressOf OnHandleDestroyed
            _TargetControl = Nothing

            MyBase.ReleaseHandle()
        End Sub

        Protected Overrides Sub WndProc(ByRef m As Message)
            If _Messages.Contains(m.Msg) Then
                _IsRunning = True
                _StartTime = DateTime.Now
            End If

            MyBase.WndProc(m)

            If _Messages.Contains(m.Msg) Then
                _IsRunning = False
            End If
        End Sub

    End Class
End Namespace