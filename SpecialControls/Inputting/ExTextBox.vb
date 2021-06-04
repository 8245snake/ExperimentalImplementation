Option Explicit On
Option Strict On

Namespace Inputting

    ''' <summary>
    ''' 機能拡張したテキストボックス。
    ''' ウォーターマーク表示やエラーチェック機能を有する。
    ''' </summary>
    Public Class ExTextBox
        Inherits TextBox

        ' 描画時
        Private Const WM_PAINT = &HF
        ' ウォーターマーク文言
        Private _WatermarkText As String = ""
        ' エラーか否か
        Private _IsError As Boolean = False
        ' エラー時の先の太さ
        Private _LineWidth As Integer = 3

        ''' <summary>
        ''' チェックエラーが検出されたときに発火します。
        ''' チェックが走るタイミングはValidationTriggerプロパティによります。
        ''' </summary>
        Public Event ErrorDetected As EventHandler

        ''' <summary>
        ''' Textが空のときに表示されるヒントテキスト
        ''' </summary>
        Public Property WatermarkText As String
            Get
                Return _WatermarkText
            End Get
            Set
                _WatermarkText = Value
                Me.Invalidate()
            End Set
        End Property

        ''' <summary>
        ''' チェックエラーが発生しているか否か
        ''' </summary>
        ''' <returns>True；エラー、False：正常</returns>
        Public ReadOnly Property IsError As Boolean
            Get
                Return _IsError
            End Get
        End Property

        ''' <summary>
        ''' エラーチェックのタイミング
        ''' </summary>
        Public Enum ValidationTriggerType
            ''' <summary>
            ''' テキストが変更されたとき
            ''' </summary>
            TextChange
            ''' <summary>
            ''' フォーカスが外れたとき
            ''' </summary>
            FocusLeave
        End Enum

        ''' <summary>
        ''' エラーチェックのタイミングを取得または設定します
        ''' </summary>
        Public Property ValidationTrigger As ValidationTriggerType

        ''' <summary>
        ''' エラーチェック関数
        ''' </summary>
        Private _ValidateFunction As ValidateFunctionDelegate = Nothing

        ''' <summary>
        ''' エラーチェック関数。
        ''' エラーの場合はFalseを返す。
        ''' </summary>
        ''' <remarks>Nothingの場合はエラーチェックしない</remarks>
        Public WriteOnly Property ValidateFunction As ValidateFunctionDelegate
            Set
                _ValidateFunction = Value
            End Set
        End Property

        ''' <summary>
        ''' エラーチェック関数のデリゲート
        ''' </summary>
        ''' <param name="text">検証対象のテキスト</param>
        ''' <param name="result">結果返却用</param>
        Delegate Sub ValidateFunctionDelegate(ByVal text As String, ByRef result As ValidationResult)

        ''' <summary>
        ''' チェックエラーの際にテキストボックスの近くに表示されるメッセージ
        ''' </summary>
        Private _ErrorText As String = ""

        ''' <summary>
        ''' エラーテキストの表示位置
        ''' </summary>
        Public Enum ErrorDisplayPositionType
            ''' <summary>
            ''' テキストボックスの下
            ''' </summary>
            Bottom
            ''' <summary>
            ''' テキストボックスの右
            ''' </summary>
            Right
            ''' <summary>
            ''' テキストボックスの右にアイコンを表示し、内容はツールチップで出す
            ''' </summary>
            RightToolTip
        End Enum

        ''' <summary>
        ''' エラーテキストの表示位置
        ''' </summary>
        Public Property ErrorDisplayPosition As ErrorDisplayPositionType


        ''' <summary>
        ''' エラーテキストを表示すべきかを判定する
        ''' </summary>
        ''' <returns>True：必要、False：不要</returns>
        Private ReadOnly Property NeedDisplayError As Boolean
            Get
                Return Not String.IsNullOrWhiteSpace(_ErrorText) AndAlso _IsError
            End Get
        End Property

        ' エラー表示用
        Private _ErrorProvider As ErrorProvider = New ErrorProvider()


        Private _LastValue As String

        ''' <summary>
        ''' 前回入力値。
        ''' Validatedのたびに更新される。
        ''' ValidatingやValidatedのHandlerで参照することで前回の値との比較ができる。
        ''' </summary>
        Public ReadOnly Property LastValue As String
            Get
                Return _LastValue
            End Get
        End Property

        Private _LastValidValue As String

        ''' <summary>
        ''' 前回の有効な入力値。
        ''' Validatedのたびに更新される。
        ''' ValidatingやValidatedのHandlerで参照することで前回の値との比較ができる。
        ''' </summary>
        Public ReadOnly Property LastValidValue As String
            Get
                Return _LastValidValue
            End Get
        End Property

        Protected Overrides Sub WndProc(ByRef m As Message)
            MyBase.WndProc(m)

            Select Case m.Msg
                Case WM_PAINT
                    ' ウォーターマーク表示
                    tryToDisplayWatermark()
                    ' 赤枠で囲う
                    tryToHighlight()
                    ' エラーテキスト表示
                    tryToDisplayError()
                Case Else
            End Select
        End Sub

        ''' <summary>
        ''' 可能ならばウォーターマークを表示する
        ''' </summary>
        Private Sub tryToDisplayWatermark()
            If (Me.Enabled AndAlso String.IsNullOrEmpty(Me.Text) AndAlso Not String.IsNullOrEmpty(WatermarkText)) Then
                Using g As Graphics = Graphics.FromHwnd(Me.Handle)
                    Dim rect As Rectangle = Me.ClientRectangle
                    rect.Offset(1, 1)
                    TextRenderer.DrawText(g, WatermarkText, Me.Font, rect, Color.LightGray, TextFormatFlags.Left)
                End Using
            End If
        End Sub

        ''' <summary>
        ''' エラーが出ていたらテキストボックスの外周に赤い枠線を描く
        ''' </summary>
        Private Sub tryToHighlight()

            If Not _IsError Then Return

            ' テキストボックス内は狭いので親コントロールに描画する
            Using g As Graphics = Me.Parent.CreateGraphics
                Dim pen As Pen = New Pen(Color.Red, _LineWidth)
                g.DrawRectangle(pen, Me.Left - _LineWidth + 1, Me.Top - _LineWidth + 1, Me.Width + _LineWidth, Me.Height + _LineWidth)
            End Using
        End Sub


        ''' <summary>
        ''' エラーテキストを表示する
        ''' </summary>
        Private Sub tryToDisplayError()

            If Not NeedDisplayError Then
                ' エラーなしならアイコンを外して終了
                _ErrorProvider.SetError(Me, "")
                Return
            End If

            Dim rect As Rectangle = getErrorMessageRectangle()

            If ErrorDisplayPosition = ErrorDisplayPositionType.RightToolTip Then
                '' アイコン表示
                _ErrorProvider.BlinkStyle = ErrorBlinkStyle.NeverBlink
                _ErrorProvider.SetIconPadding(Me, _LineWidth)
                _ErrorProvider.SetError(Me, _ErrorText)
            Else
                ' テキスト表示
                Using g As Graphics = Me.Parent.CreateGraphics
                    TextRenderer.DrawText(g, _ErrorText, Me.Font, rect, Color.Red, TextFormatFlags.Left)
                End Using
            End If


        End Sub

        ''' <summary>
        ''' エラーテキストを表示する領域を取得する
        ''' </summary>
        ''' <returns>エラーテキストを表示する領域</returns>
        Private Function getErrorMessageRectangle() As Rectangle
            Dim rect As Rectangle = New Rectangle(Me.Left, Me.Top, 1000, 1000)

            Select Case ErrorDisplayPosition
                Case ErrorDisplayPositionType.Bottom
                    rect.Y = Me.Bottom + _LineWidth
                Case ErrorDisplayPositionType.Right, ErrorDisplayPositionType.RightToolTip
                    rect.X = Me.Right + _LineWidth
            End Select

            Return rect
        End Function

        Protected Overrides Sub OnValidated(e As EventArgs)
            MyBase.OnValidated(e)
            If _ValidateFunction IsNot Nothing AndAlso ValidationTrigger = ValidationTriggerType.FocusLeave Then
                errorCheck()
            End If
            ' 値を憶えておく
            _LastValue = Me.Text
            If Not _IsError Then
                _LastValidValue = Me.Text
            End If
        End Sub

        Protected Overrides Sub OnTextChanged(e As EventArgs)
            MyBase.OnTextChanged(e)
            If _ValidateFunction IsNot Nothing AndAlso ValidationTrigger = ValidationTriggerType.TextChange Then
                errorCheck()
            End If
        End Sub

        ''' <summary>
        ''' エラーチェックして結果を描画する
        ''' </summary>
        Private Sub errorCheck()
            Dim result As ValidationResult = New ValidationResult()
            ' 判定
            _ValidateFunction(Me.Text, result)
            _IsError = Not result.Pass
            _ErrorText = result.ErrorText
            ' 再描画
            UpdateDisplay()
            ' エラーが検出されたらイベント発火
            If _IsError Then
                RaiseEvent ErrorDetected(Me, New EventArgs())
            End If
        End Sub

        ''' <summary>
        ''' 表示を更新する
        ''' </summary>
        Private Sub UpdateDisplay()
            ' 自身を再描画
            Me.Invalidate()
            ' 親（赤枠）を再描画
            Me.Parent.Invalidate(New Rectangle(Me.Left - _LineWidth, Me.Top - _LineWidth, Me.Width + _LineWidth * 2, Me.Height + _LineWidth * 2), False)
            ' 親（テキスト）を再描画
            Dim rect As Rectangle = getErrorMessageRectangle()
            Me.Parent.Invalidate(rect, False)
        End Sub

    End Class

    ''' <summary>
    ''' エラーチェック結果を格納するクラス
    ''' </summary>
    Public Class ValidationResult

        ''' <summary>
        ''' チェック結果
        ''' </summary>
        ''' <returns>True：OK、False：NG</returns>
        Public Property Pass As Boolean = True

        ''' <summary>
        ''' エラーの文言
        ''' </summary>
        ''' <returns>エラーとして表示される文字列</returns>
        Public Property ErrorText As String = ""

    End Class

End Namespace