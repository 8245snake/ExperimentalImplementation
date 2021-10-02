Option Explicit On
Option Strict On

Imports System.Drawing
Imports System.Windows.Forms

Namespace Text

    ''' <summary>
    ''' テキストボックスの描画処理をフックしてウォーターマークを表示するためのクラス
    ''' </summary>
    <Attachment(AllowMultiple:=False)>
    Friend Class WaterMarkAttachment
        Inherits NativeWindow

        Private _TextBox As System.Windows.Forms.TextBox

        ''' <summary>
        ''' ウォーターマーク
        ''' </summary>
        Public Property WatermarkText As String

        Private Const WM_PAINT = &HF

        Public Sub New(txt As TextBox, watermark As String)
            _TextBox = txt
            WatermarkText = watermark
            AddHandler _TextBox.HandleCreated, AddressOf OnHandleCreated
            AddHandler _TextBox.HandleDestroyed, AddressOf OnHandleDestroyed
        End Sub


        Private Sub OnHandleCreated(sender As Object, e As EventArgs)
            Dim txt = TryCast(sender, System.Windows.Forms.TextBox)
            AssignHandle(txt.Handle)
        End Sub

        Private Sub OnHandleDestroyed(sender As Object, e As EventArgs)
            ReleaseHandle()
        End Sub

        Public Overrides Sub ReleaseHandle()
            RemoveHandler _TextBox.HandleCreated, AddressOf OnHandleCreated
            RemoveHandler _TextBox.HandleDestroyed, AddressOf OnHandleDestroyed
            _TextBox = Nothing

            MyBase.ReleaseHandle()
        End Sub

        Protected Overrides Sub WndProc(ByRef m As Message)
            MyBase.WndProc(m)

            If m.Msg = WM_PAINT Then
                ' ウォーターマーク表示
                TryWriteWatermark()
            End If

        End Sub

        ''' <summary>
        ''' 可能ならばウォーターマークを表示する
        ''' </summary>
        Private Sub TryWriteWatermark()

            If _TextBox Is Nothing Then Return

            If (_TextBox.Enabled AndAlso String.IsNullOrEmpty(_TextBox.Text) AndAlso Not String.IsNullOrEmpty(WatermarkText)) Then
                Using g As Graphics = Graphics.FromHwnd(Me.Handle)
                    Dim rect As Rectangle = _TextBox.ClientRectangle
                    rect.Offset(1, 1)
                    TextRenderer.DrawText(g, WatermarkText, _TextBox.Font, rect, Color.LightSlateGray, TextFormatFlags.Left)
                End Using
            End If
        End Sub

    End Class

End Namespace