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
        Private _Brush As Brush = Brushes.LightGray
        Private _isWatermarkWritten As Boolean = False

        ''' <summary>
        ''' ウォーターマーク
        ''' </summary>
        Public Property WatermarkText As String

        Private Const WM_PAINT = &HF

        Public Sub New(textBox As TextBox, watermarkText As String)
            _TextBox = textBox
            Me.WatermarkText = watermarkText

            If _TextBox.IsHandleCreated Then
                AssignHandle(_TextBox.Handle)
            Else
                AddHandler _TextBox.HandleCreated, AddressOf OnHandleCreated
            End If

            AddHandler _TextBox.KeyDown, AddressOf OnKeyDown
            AddHandler _TextBox.HandleDestroyed, AddressOf OnHandleDestroyed
        End Sub

        Private Sub OnKeyDown(sender As Object, e As KeyEventArgs)
            If _isWatermarkWritten Then
                ' 複数行のとき編集行以外のウォーターマークが消えないため再描画を促す
                _TextBox.Invalidate()
            End If
        End Sub

        Public Sub New(textBox As TextBox, watermarkText As String, fontColor As Color)
            MyClass.New(textBox, watermarkText)
            _Brush = New SolidBrush(fontColor)
        End Sub


        Private Sub OnHandleCreated(sender As Object, e As EventArgs)
            AssignHandle(_TextBox.Handle)
            RemoveHandler _TextBox.HandleCreated, AddressOf OnHandleCreated
        End Sub

        Private Sub OnHandleDestroyed(sender As Object, e As EventArgs)
            ReleaseHandle()
        End Sub

        Public Overrides Sub ReleaseHandle()
            RemoveHandler _TextBox.KeyDown, AddressOf OnKeyDown
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
            _isWatermarkWritten = False

            If _TextBox Is Nothing Then Return

            If (_TextBox.Enabled AndAlso String.IsNullOrEmpty(_TextBox.Text) AndAlso Not String.IsNullOrEmpty(WatermarkText)) Then
                Using g As Graphics = Graphics.FromHwnd(Me.Handle)
                    Dim rect As Rectangle = _TextBox.ClientRectangle
                    rect.Offset(1, 1)
                    g.DrawString(WatermarkText, _TextBox.Font, _Brush, rect)
                End Using

                _isWatermarkWritten = True
            End If
        End Sub

    End Class

End Namespace