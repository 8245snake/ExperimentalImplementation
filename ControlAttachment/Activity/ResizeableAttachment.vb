Option Explicit On
Option Strict On

Imports System.Drawing
Imports System.Windows.Forms

Namespace Activity

    ''' <summary>
    ''' コントロールをサイズ変更可能にするアタッチメント
    ''' </summary>
    <Attachment(AllowMultiple:=False)>
    Friend Class ResizeableAttachment
        Inherits NativeWindow

        Private _TargetControl As Control
        Private _IsResizing As Boolean = False
        Private _OriginalParentSize As Size
        Private _OriginalMargin As Padding
        Private _OriginalCursor As Cursor

        Private Const WM_PAINT = &HF
        Private Const WM_MOUSEMOVE = &H200
        Private Const WM_LBUTTONDOWN = &H201
        Private Const WM_LBUTTONUP = &H202

        Public Sub New(targetControl As Control)
            _TargetControl = targetControl
            If _TargetControl.Parent IsNot Nothing Then
                _OriginalParentSize = _TargetControl.Parent.Size
                _OriginalMargin = New Padding(_TargetControl.Left, _TargetControl.Top, _OriginalParentSize.Width - _TargetControl.Right, _OriginalParentSize.Height - _TargetControl.Bottom)
            End If
            _OriginalCursor = _TargetControl.Cursor

            If _TargetControl.IsHandleCreated Then
                AssignHandle(_TargetControl.Handle)
            Else
                AddHandler _TargetControl.HandleCreated, AddressOf OnHandleCreated
            End If

            AddHandler _TargetControl.HandleDestroyed, AddressOf OnHandleDestroyed

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
                    ' コントロールの右下にサイズ変更できそうなマークを描く
                    Dim pen = New Pen(Color.Gray, 1)
                    Dim rect = _TargetControl.ClientRectangle
                    Using g As Graphics = Graphics.FromHwnd(Me.Handle)
                        g.DrawLine(pen, New Point(rect.Right - 5, rect.Bottom), New Point(rect.Right, rect.Bottom - 5))
                        g.DrawLine(pen, New Point(rect.Right - 10, rect.Bottom), New Point(rect.Right, rect.Bottom - 10))
                    End Using

                Case WM_MOUSEMOVE
                    If IsMousePointerInRegion() Then
                        _TargetControl.Cursor = System.Windows.Forms.Cursors.SizeNWSE
                    Else
                        _TargetControl.Cursor = _OriginalCursor
                    End If

                    If _IsResizing Then
                        Resize()
                    End If

                Case WM_LBUTTONDOWN
                    _IsResizing = IsMousePointerInRegion()

                Case WM_LBUTTONUP
                    _IsResizing = False
            End Select

        End Sub

        Private Sub Resize()

            Dim pos = _TargetControl.PointToClient(Cursor.Position)
            Dim clientRect = _TargetControl.ClientRectangle
            Dim deltaX = pos.X - clientRect.Width
            Dim deltaY = pos.Y - clientRect.Height

            '' コントロールのサイズ変更
            clientRect.Width = pos.X
            clientRect.Height = pos.Y
            _TargetControl.ClientSize = clientRect.Size

            '' 親コントロールのサイズ変更
            If _TargetControl.Parent Is Nothing Then Return

            Dim parentNewWidth = _TargetControl.Parent.Width + deltaX
            Dim parentNewHeight = _TargetControl.Parent.Height + deltaY
            Dim newMarginRight = parentNewWidth - _TargetControl.Right
            Dim newMarginBottom = parentNewHeight - _TargetControl.Bottom

            If parentNewWidth > _OriginalParentSize.Width AndAlso newMarginRight < _OriginalMargin.Right Then
                ' 親コントロールは元の大きさより縮小する方にはリサイズしない
                _TargetControl.Parent.Width = parentNewWidth
            End If

            If parentNewHeight > _OriginalParentSize.Height AndAlso newMarginBottom < _OriginalMargin.Bottom Then
                ' 親コントロールは元の大きさより縮小する方にはリサイズしない
                _TargetControl.Parent.Height = parentNewHeight
            End If
        End Sub

        Private Function IsMousePointerInRegion() As Boolean
            Dim pos = _TargetControl.PointToClient(Cursor.Position)
            Dim client = _TargetControl.ClientRectangle
            Dim rect = New Rectangle(client.Right - 10, client.Bottom - 10, 10, 10)
            If pos.X < rect.Left OrElse pos.X > rect.Right Then Return False
            If pos.Y < rect.Top OrElse pos.Y > rect.Bottom Then Return False
            Return True
        End Function

    End Class
End Namespace