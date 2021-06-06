Option Explicit On
Option Strict On

Namespace Inputting

    ''' <summary>
    ''' 背景色透過可能なリッチテキストボックス
    ''' </summary>
    ''' <remarks>厳密には透過ではなく親コントロールと同じ背景色にしているだけ</remarks>
    Public Class TransparentRichTextBox
        Inherits RichTextBox

        Private Const WM_PAINT = &HF

        ''' <summary>
        ''' 透過するか
        ''' </summary>
        Public Property IsTransparent As Boolean

        Protected Overrides Sub WndProc(ByRef m As Message)
            MyBase.WndProc(m)

            Select Case m.Msg
                Case WM_PAINT
                    If IsTransparent Then
                        ' 親と同じ色にする
                        Me.BackColor = Me.Parent.BackColor
                    End If
                Case Else

            End Select

        End Sub

    End Class

End Namespace

