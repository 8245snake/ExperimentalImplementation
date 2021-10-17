Option Explicit On
Option Strict On

Imports ControlAttachment
Imports ControlAttachment.Activity
Imports ControlAttachment.DataHolder
Imports ControlAttachment.State
Imports ControlAttachment.Strategies
Imports ControlAttachment.Validation
Imports ControlReference.Strategies

Public Class MainForm

    Private _LimitSizes As ComboItemModelHolder(Of LimitSetterCommand)
    Private _highlightingManager As HighlightingManager = New HighlightingManager(New BorderDrawActionStrategy())

    Public Sub New()
        InitializeComponent()
        ' ウォーターマーク
        txtWaterMark.AttachWaterMark($"ウォーターマーク（英語: watermark）は、本来、紙の透かし（すかし）のことを指すが、著作権表示などのために静止画像や動画に写し込まれる小さな図案や文字を指すことが比較的多い。")
        ' バイト数制限
        SetupLimitedTextBox()
        ' バリデーション(数値チェックと３の倍数チェックを複合し、赤枠とメッセージボックスのアクションを複合する)
        AddHandler chkValidate1.CheckedChanged, AddressOf chkValidate_CheckedChanged
        AddHandler chkValidate2.CheckedChanged, AddressOf chkValidate_CheckedChanged
        AddHandler chkAction1.CheckedChanged, AddressOf chkValidate_CheckedChanged
        AddHandler chkAction2.CheckedChanged, AddressOf chkValidate_CheckedChanged
        AddHandler chkAction3.CheckedChanged, AddressOf chkValidate_CheckedChanged
        chkValidate_CheckedChanged(txtValidation, EventArgs.Empty)
        ' チェックボックスデコレーション
        btnEnlarge_Click(btnEnlarge, EventArgs.Empty)
        ' テキストボックスをサイズ変更可能にする
        txtReizaeable.AttachResizeable()
        ' ドラッグ可能にする
        Dim moving = New DraggableAttachment(panelMove1, New StandardDragActionStrategy(panelMove1))
        Dim target1 = New DroppableAttachment(panelTarget1, New BorderDrawActionStrategy())
        Dim target2 = New DroppableAttachment(panelTarget2, New BorderDrawActionStrategy())
        moving.AddDropTarget(target1)
        moving.AddDropTarget(target2)

    End Sub




    Private Sub MainForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub SetupLimitedTextBox()

        _LimitSizes = New ComboItemModelHolder(Of LimitSetterCommand)(combSizeLimit)
        combSizeLimit.Items.Clear()
        For Each size As Integer In New Integer() {5, 10, 30}
            Dim index = combSizeLimit.Items.Add($"{size} byte")
            _LimitSizes.Data(index) = New LimitSetterCommand(size)
        Next
        AddHandler combSizeLimit.SelectedIndexChanged, Sub(sender, args)
                                                           _LimitSizes.SelectedData.SetLimit(txtSizeLimited, lblSizeError)
                                                       End Sub

        combSizeLimit.SelectedIndex = 0
    End Sub

    Private Sub chkValidate_CheckedChanged(sender As Object, e As EventArgs)
        txtValidation.ClearValidationAttachments()

        Dim validationStrategy As IValidationStrategy = GetValidationStrategy().CompositValidationStrategy()
        If validationStrategy Is Nothing Then Return

        Dim errorActionStrategy As IErrorActionStrategy = GetErrorActionStrategy().CompositActionStrategy()
        If errorActionStrategy Is Nothing Then Return

        txtValidation.AttachValidation(validationStrategy, errorActionStrategy)
        txtValidation.ForceValidate()
    End Sub

    Private Iterator Function GetValidationStrategy() As IEnumerable(Of IValidationStrategy)
        If chkValidate1.Checked Then
            Yield New IntegerCheckStrategy()
        End If
        If chkValidate2.Checked Then
            Yield New ThreeMultipleValidationStrategy()
        End If
    End Function

    Private Iterator Function GetErrorActionStrategy() As IEnumerable(Of IErrorActionStrategy)
        If chkAction1.Checked Then
            Yield New BorderDrawActionStrategy()
        End If
        If chkAction2.Checked Then
            Yield New LabelWritingErrorActionStrategy(lblValidate, "エラー")
        End If
        If chkAction3.Checked Then
            Yield New FillActionStrategy()
        End If
    End Function



    Private Sub btnEnlarge_Click(sender As Object, e As EventArgs)
        chkDecoration.Enlarge()
        btnEnlarge.Text = "縮小"
        chkDecoration.Text = "大きなチェックボックス"
        RemoveHandler btnEnlarge.Click, AddressOf btnEnlarge_Click
        AddHandler btnEnlarge.Click, AddressOf btnShrink_Click
    End Sub

    Private Sub btnShrink_Click(sender As Object, e As EventArgs)
        chkDecoration.Shrink()
        btnEnlarge.Text = "拡大"
        chkDecoration.Text = "通常のチェックボックス"
        RemoveHandler btnEnlarge.Click, AddressOf btnShrink_Click
        AddHandler btnEnlarge.Click, AddressOf btnEnlarge_Click
    End Sub

    Private Sub btnHighlight_Click(sender As Object, e As EventArgs) Handles btnHighlight.Click
        ' 可視コントロールの中からランダムに１つ選んでハイライトする
        Dim visibleControlsArray = ControlHelper.GetChildControls(Me).Where(Function(ctl) ctl.Visible).ToArray()
        Dim rand = New Random()
        Dim index = rand.Next(0, visibleControlsArray.Length - 1)
        _highlightingManager.HighlightingControl = visibleControlsArray(index)
    End Sub

    Private Sub chkBlink_CheckedChanged(sender As Object, e As EventArgs) Handles chkBlink.CheckedChanged
        Dim ctrl As Control = _highlightingManager.HighlightingControl
        If chkBlink.Checked Then
            _highlightingManager.HighlightingActionStrategy = New BorderDrawActionStrategy(isBlinkEnable:=True)
        Else
            _highlightingManager.HighlightingActionStrategy = New BorderDrawActionStrategy()
        End If
        _highlightingManager.HighlightingControl = ctrl
    End Sub
End Class


Class LimitSetterCommand

    Private Size As Integer

    Public Sub New(size As Integer)
        Me.Size = size
    End Sub

    Public Sub SetLimit(textbox As TextBox, label As Label)
        textbox.AttachMaxByteSize(Me.Size, New LabelWritingErrorActionStrategy(label, $"バイト数の最大値({Me.Size})を超えることはできません"))
    End Sub

End Class


Class ControlHelper
    Public Shared Iterator Function GetChildControls(control As Control) As IEnumerable(Of Control)
        If control Is Nothing Then Return

        For Each child As Control In control.Controls
            Yield child
            For Each childControl As Control In GetChildControls(child)
                Yield childControl
            Next
        Next

    End Function
End Class
