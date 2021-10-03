Option Explicit On
Option Strict On

Imports System.Drawing
Imports System.Windows.Forms

Namespace Decoration

    ''' <summary>
    ''' チェックボックスを大きくするアタッチメント
    ''' </summary>
    <Attachment(AllowMultiple:=False)>
    Friend Class CheckBoxEnlargeAttachment
        Inherits NativeWindow

        Private _CheckBox As CheckBox
        Private _IsMouseHovering As Boolean

        Private Const WM_PAINT = &HF
        Private Const WM_MOUSEMOVE = &H200
        Private Const WM_MOUSELEAVE = &H2A3

        Public Property HoverColor As Color

        Public Sub New(checkBox As CheckBox)
            _CheckBox = checkBox
            AddHandler _CheckBox.HandleCreated, AddressOf OnHandleCreated
            AddHandler _CheckBox.HandleDestroyed, AddressOf OnHandleDestroyed

            HoverColor = ColorTranslator.FromHtml("#88d9ebf9")
        End Sub


        Private Sub OnHandleCreated(sender As Object, e As EventArgs)
            Dim checkBox = TryCast(sender, CheckBox)
            AssignHandle(checkBox.Handle)
        End Sub

        Private Sub OnHandleDestroyed(sender As Object, e As EventArgs)
            ReleaseHandle()
        End Sub

        Public Overrides Sub ReleaseHandle()
            RemoveHandler _CheckBox.HandleCreated, AddressOf OnHandleCreated
            RemoveHandler _CheckBox.HandleDestroyed, AddressOf OnHandleDestroyed
            _CheckBox = Nothing

            MyBase.ReleaseHandle()
        End Sub

        Protected Overrides Sub WndProc(ByRef m As Message)
            MyBase.WndProc(m)

            Select Case m.Msg
                Case WM_PAINT
                    PaintCheckBox()
                Case WM_MOUSEMOVE
                    _IsMouseHovering = True
                Case WM_MOUSELEAVE
                    _IsMouseHovering = False
            End Select

        End Sub

        Private Sub PaintCheckBox()

            Using g As Graphics = Graphics.FromHwnd(Me.Handle)
                ' オリジナルの描画を消して自前で描く
                g.Clear(_CheckBox.BackColor)
                g.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias

                If _IsMouseHovering Then
                    Dim rect = New Rectangle(0, 0, _CheckBox.Width, _CheckBox.Height)
                    g.FillRectangle(New SolidBrush(HoverColor), rect)
                End If

                Dim sideLength As Integer = _CheckBox.Height
                Dim margin As Integer = CInt(sideLength / 4)
                Select Case _CheckBox.CheckState
                    Case CheckState.Unchecked
                        ControlPaint.DrawCheckBox(g, 0, 0, sideLength, sideLength, ButtonState.Normal Or ButtonState.Flat)
                    Case CheckState.Checked
                        ControlPaint.DrawCheckBox(g, 0, 0, sideLength, sideLength, ButtonState.Checked Or ButtonState.Flat)
                    Case CheckState.Indeterminate
                        ControlPaint.DrawCheckBox(g, 0, 0, sideLength, sideLength, ButtonState.Normal Or ButtonState.Flat)
                        g.FillRectangle(Brushes.Black, margin, margin, sideLength - 2 * margin, sideLength - 2 * margin)
                End Select

                If Not _CheckBox.Enabled Then
                    g.FillRectangle(New SolidBrush(ColorTranslator.FromHtml("#AAF0F0F0")), 0, 0, sideLength, sideLength)
                End If

                Dim textSize As Size = TextRenderer.MeasureText(_CheckBox.Text, _CheckBox.Font)
                Dim top As Integer = CInt((_CheckBox.Height - textSize.Height) / 2)
                Dim left As Integer = sideLength
                Select Case _CheckBox.TextAlign
                    Case ContentAlignment.MiddleLeft
                    Case Else
                        '    Throw New Exception("LargeCheckBox.TextAlignにはMiddleLeftのみ設定可能です")
                End Select

                Dim fontColor As Color = If(_CheckBox.Enabled, _CheckBox.ForeColor, Color.Gray)
                TextRenderer.DrawText(g, _CheckBox.Text, _CheckBox.Font, New Point(left, top), fontColor)

            End Using

        End Sub

    End Class

End Namespace