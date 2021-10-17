Option Explicit On
Option Strict On

Imports System.Text
Imports System.Windows.Forms
Imports ControlAttachment.Validation

Namespace Text

    ''' <summary>
    ''' テキストの入力文字数をバイト単位で指定するアタッチメント
    ''' </summary>
    <Attachment(AllowMultiple:=False)>
    Friend Class TextSizeLimitAttachment
        Inherits NativeWindow

        Private _TargetControl As TextBox

        Private Const WM_PASTE = &H302
        Private Const WM_CHAR = &H102
        Private _Encoding As Encoding = Encoding.GetEncoding("Shift_JIS")

        ''' <summary>
        ''' バイト数の最大値を取得または設定する
        ''' </summary>
        Public Property MaxByteLength As Integer

        ''' <summary>
        ''' <see cref="MaxByteLength"/>を超えて入力しようとしたときに実行されるアクション
        ''' </summary>
        Public Property ErrorActionStrategy As IErrorActionStrategy


        Public Sub New(targetControl As TextBox, maxByteLength As Integer, Optional errorActionStrategy As IErrorActionStrategy = Nothing)
            _TargetControl = targetControl
            Me.MaxByteLength = maxByteLength
            Me.ErrorActionStrategy = errorActionStrategy

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

        ''' <summary>
        ''' バイト数上限を鑑みて文字入力をキャンセルすべきかを判定する
        ''' '参考 http://jeanne.wankuma.com/library/maxbytelengthtextbox/source.html
        ''' </summary>
        ''' <param name="chr">あらたに入力された文字</param>
        ''' <returns>True:キャンセルすべし、False:通してOK</returns>
        Private Function ShouldCancel(chr As Char) As Boolean

            If Char.IsControl(chr) AndAlso chr <> vbCr Then
                Return False
            End If

            Dim textByteCount As Integer = _Encoding.GetByteCount(_TargetControl.Text)
            Dim inputByteCount As Integer = _Encoding.GetByteCount(chr.ToString())
            Dim selectedTextByteCount As Integer = _Encoding.GetByteCount(_TargetControl.SelectedText)

            If textByteCount + inputByteCount - selectedTextByteCount > MaxByteLength Then
                Return True
            End If

            Return False

        End Function

        ''' <summary>
        ''' クリップボードの文字列を入る分だけ入れる
        ''' </summary>
        Private Sub SubstringPaste()
            Dim clipboardText As Object = Clipboard.GetDataObject().GetData(DataFormats.Text)

            If clipboardText Is Nothing Then
                Return
            End If

            Dim inputText As String = clipboardText.ToString()
            Dim textByteCount As Integer = _Encoding.GetByteCount(_TargetControl.Text)
            Dim inputByteCount As Integer = _Encoding.GetByteCount(inputText)
            Dim selectedTextByteCount As Integer = _Encoding.GetByteCount(_TargetControl.SelectedText)
            Dim remainByteCount As Integer = Me.MaxByteLength - (textByteCount - selectedTextByteCount)

            If remainByteCount <= 0 Then
                Return
            End If

            If remainByteCount >= inputByteCount Then
                _TargetControl.SelectedText = inputText
            Else
                _TargetControl.SelectedText = inputText.Substring(0, remainByteCount)
                ' 切られたときにアラートを出す
                ErrorActionStrategy?.ErrorAction(_TargetControl)
            End If
        End Sub


        Protected Overrides Sub WndProc(ByRef m As Message)

            Select Case m.Msg
                Case WM_PASTE
                    Call SubstringPaste()
                    Return
                Case WM_CHAR
                    If ShouldCancel(ChrW(m.WParam.ToInt32())) Then
                        ' キャンセルされたときにアラートを出す
                        ErrorAll()
                        Return
                    Else
                        SucceedAll()
                    End If
            End Select

            MyBase.WndProc(m)

        End Sub

        Private Sub SucceedAll()

            Dim strategy = ErrorActionStrategy

            While strategy IsNot Nothing
                strategy.SuccessAction(_TargetControl)
                strategy = strategy.Composit
            End While

        End Sub

        Private Sub ErrorAll()

            Dim strategy = ErrorActionStrategy

            While strategy IsNot Nothing
                strategy.ErrorAction(_TargetControl)
                strategy = strategy.Composit
            End While

        End Sub

    End Class

End Namespace