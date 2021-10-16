Option Explicit On
Option Strict On

Imports System.Drawing
Imports System.Windows.Forms
Imports ControlAttachment.State

Namespace Activity
    Public Class DroppableAttachment
        Inherits NativeWindow

        Private _TargetControl As Control
        Private _readyToDrop As Boolean

        Private Const WM_PAINT = &HF
        Private Const WM_NCPAINT = &H85

        Private _HighlightingAction As IHighlightingActionStrategy

        Public Property ReadyToDrop As Boolean
            Get
                Return _readyToDrop
            End Get
            Set
                _readyToDrop = Value
                _HighlightingAction?.BeginHighlight(_TargetControl)
                _TargetControl.Refresh()
            End Set
        End Property

        Public Property DropObjectPadding As Padding

        Public Property CanDrop As Boolean

        Public ReadOnly Property TargetControl As Control
            Get
                Return _TargetControl
            End Get
        End Property

        Public Sub New(targetControl As Control)
            _TargetControl = targetControl

            AddHandler _TargetControl.HandleCreated, AddressOf OnHandleCreated
            AddHandler _TargetControl.HandleDestroyed, AddressOf OnHandleDestroyed

            If _TargetControl.IsHandleCreated Then
                AssignHandle(_TargetControl.Handle)
            End If

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
            Dim ctrl = TryCast(sender, Control)
            AssignHandle(ctrl.Handle)
        End Sub

        Private Sub OnHandleDestroyed(sender As Object, e As EventArgs)
            ReleaseHandle()
        End Sub

        Protected Overrides Sub WndProc(ByRef m As Message)
            MyBase.WndProc(m)

            Select Case m.Msg
                Case WM_PAINT, WM_NCPAINT

                    If ReadyToDrop AndAlso IsRegionEnterd() Then
                        CanDrop = True
                        _HighlightingAction?.Highlight(_TargetControl)
                    Else
                        CanDrop = False
                        _HighlightingAction?.EndHighlight(_TargetControl)
                    End If

                    If m.Msg <> WM_PAINT Then
                        _TargetControl.Refresh()
                    End If

            End Select

        End Sub


        Private Function IsRegionEnterd() As Boolean
            Dim pos = Cursor.Position
            Dim targetScreenPos = _TargetControl.Parent.PointToScreen(_TargetControl.Location)

            ' 右端がフレームの左端より左
            If pos.X + DropObjectPadding.Right < targetScreenPos.X Then Return False
            ' 左端がフレームの右端より右
            If pos.X - DropObjectPadding.Left > targetScreenPos.X + _TargetControl.Width Then Return False
            ' 下端がフレームの上端より上
            If pos.Y + DropObjectPadding.Bottom < targetScreenPos.Y Then Return False
            ' 上端がフレームの下端より下
            If pos.Y + DropObjectPadding.Top > targetScreenPos.Y + _TargetControl.Height Then Return False

            Return True

        End Function

        Public Sub Drop(chiled As Control, dropPosition As Point)
            chiled.Location = dropPosition
            _TargetControl.Controls.Add(chiled)
            CanDrop = False
        End Sub

    End Class
End Namespace