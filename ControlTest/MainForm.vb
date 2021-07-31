Option Explicit On
Option Strict On

Imports SpecialControls
Imports SpecialControls.CollectionViews
Imports SpecialControls.GenericWrappers
Imports SpecialControls.Inputting
Imports SpecialControls.Messaging
Imports SpecialControls.Services


Public Class MainForm

    Dim combDurationWrapper As GenericComboBoxWrapper(Of SplashMessage.DisplayDuration)

    Dim combSampleWrapper As GenericComboBoxWrapper(Of Country)
    Dim lstSampleWrapper As GenericListBoxWrapper(Of City)

    Dim combTriggerWrapper As GenericComboBoxWrapper(Of ExTextBox.ValidationTriggerType)
    Dim combErrorPositionWrapper As GenericComboBoxWrapper(Of ExTextBox.ErrorDisplayPositionType)

    Dim radioButtons As GenericRadioButtonWrapper(Of CheckState) = New GenericRadioButtonWrapper(Of CheckState)()

    Dim hightlightingService As ControlHighlightingService = New ControlHighlightingService()

    Sub New()

        ' この呼び出しはデザイナーで必要です。
        InitializeComponent()

        ' InitializeComponent() 呼び出しの後で初期化を追加します。
        Dim writer As TextBoxWriter = New TextBoxWriter()
        writer.TargetTextBox = txtDebug
        Console.SetOut(writer)

        txtTrue.Text = ToggleSwitch1.TrueText
        txtFalse.Text = ToggleSwitch1.FalseText
        btnTrueColor.BackColor = ToggleSwitch1.TrueColor
        btnFalseColor.BackColor = ToggleSwitch1.FalseColor
        txtToggleWidth.Text = ToggleSwitch1.Width.ToString()
        txtToggleHeight.Text = ToggleSwitch1.Height.ToString()
        lblFont.Text = $"{ToggleSwitch1.FontName}, {ToggleSwitch1.FontSize}, {ToggleSwitch1.FontStyle}"
        lblFontSplash.Text = $"{FontDialogTooltip.Font.Name}, {FontDialogTooltip.Font.Size}, {FontDialogTooltip.Font.Style}"

        combDurationWrapper = New GenericComboBoxWrapper(Of SplashMessage.DisplayDuration)(combDuration)
        combDurationWrapper.Add("1秒", SplashMessage.DisplayDuration.ShortTime)
        combDurationWrapper.Add("3秒", SplashMessage.DisplayDuration.LongTime)
        combDurationWrapper.Add("無限", SplashMessage.DisplayDuration.Endless)

        combTriggerWrapper = New GenericComboBoxWrapper(Of ExTextBox.ValidationTriggerType)(DirectCast(combTrigger, ComboBox))
        combTriggerWrapper.Add("フォーカスが外れたとき", ExTextBox.ValidationTriggerType.FocusLeave)
        combTriggerWrapper.Add("テキストが変わったとき", ExTextBox.ValidationTriggerType.TextChange)
        combTriggerWrapper.SelectedValue = ExTextBox.ValidationTriggerType.TextChange
        combErrorPositionWrapper = New GenericComboBoxWrapper(Of ExTextBox.ErrorDisplayPositionType)(DirectCast(combErrorPosition, ComboBox))
        combErrorPositionWrapper.Add("下", ExTextBox.ErrorDisplayPositionType.Bottom)
        combErrorPositionWrapper.Add("右", ExTextBox.ErrorDisplayPositionType.Right)
        combErrorPositionWrapper.Add("右（アイコン）", ExTextBox.ErrorDisplayPositionType.RightToolTip)
        combErrorPositionWrapper.SelectedValue = ExTextBox.ErrorDisplayPositionType.Bottom

        txtNumber.ValidationTrigger = combTriggerWrapper.SelectedValue
        txtNumber.ErrorDisplayPosition = combErrorPositionWrapper.SelectedValue
        txtNumber.ValidateFunction = AddressOf checkNumber

        radioButtons.AddBinding(DirectCast(optChecked, RadioButton), CheckState.Checked)
        radioButtons.AddBinding(DirectCast(optUnchecked, RadioButton), CheckState.Unchecked)
        radioButtons.AddBinding(DirectCast(optIndeterminate, RadioButton), CheckState.Indeterminate)

        hightlightingService.HighlightingControl = btnGenericSet


        Using frm = New ToolTipSampleForm()
            Dim bitmap As Bitmap = frm.GetBitmap()
            ToolTipMain.SetToolTipEx(lblTooltipForm, bitmap)
            ToolTipMain.InitialDelay = 100
        End Using
    End Sub


#Region "ToggleSwitch"

    Private Sub txtTrue_Validated(sender As Object, e As EventArgs) Handles txtTrue.Validated
        ToggleSwitch1.TrueText = txtTrue.Text
        ToggleSwitch1.Invalidate()
    End Sub

    Private Sub txtFalse_Validated(sender As Object, e As EventArgs) Handles txtFalse.Validated
        ToggleSwitch1.FalseText = txtFalse.Text
        ToggleSwitch1.Invalidate()
    End Sub

    Private Sub btnTrueColor_Click(sender As Object, e As EventArgs) Handles btnTrueColor.Click
        ColorDialog1.ShowDialog()
        btnTrueColor.BackColor = ColorDialog1.Color
        ToggleSwitch1.TrueColor = ColorDialog1.Color
        ToggleSwitch1.Invalidate()
    End Sub

    Private Sub btnFalseColor_Click(sender As Object, e As EventArgs) Handles btnFalseColor.Click
        ColorDialog1.ShowDialog()
        btnFalseColor.BackColor = ColorDialog1.Color
        ToggleSwitch1.FalseColor = ColorDialog1.Color
        ToggleSwitch1.Invalidate()
    End Sub

    Private Sub txtToggleWidth_Validated(sender As Object, e As EventArgs) Handles txtToggleWidth.Validated
        Dim val As Integer
        If Integer.TryParse(txtToggleWidth.Text, val) Then
            ToggleSwitch1.Width = val
            ToggleSwitch1.Invalidate()
        End If
    End Sub

    Private Sub txtToggleHeight_Validated(sender As Object, e As EventArgs) Handles txtToggleHeight.Validated
        Dim val As Integer
        If Integer.TryParse(txtToggleHeight.Text, val) Then
            ToggleSwitch1.Height = val
            ToggleSwitch1.Invalidate()
        End If
    End Sub

    Private Sub btnToggleFonts_Click(sender As Object, e As EventArgs) Handles btnToggleFonts.Click
        FontDialogToggle.ShowDialog()
        Dim font As Font = FontDialogToggle.Font
        lblFont.Text = $"{font.Name}, {font.Size}, {font.Style}"
        ToggleSwitch1.FontName = font.Name
        ToggleSwitch1.FontSize = font.Size
        ToggleSwitch1.FontStyle = font.Style
        ToggleSwitch1.Invalidate()
    End Sub

    Private Sub ToggleSwitch1_CheckedChanged(sender As Object, e As EventArgs) Handles ToggleSwitch1.CheckedChanged
        Console.WriteLine($"トグルスイッチチェック状態={ToggleSwitch1.IsChecked}")
    End Sub
#End Region

#Region "SplashMessage"
    Private Sub btnTooltip_Click(sender As Object, e As EventArgs) Handles btnTooltip.Click
        Dim bitmap As Bitmap = SplashMessage.CreateTextImage(txtTooltip.Text, FontDialogTooltip.Font)
        ToolTipMain.ShowBitmap(bitmap, Me, Integer.Parse(txtX.Text), Integer.Parse(txtY.Text), combDurationWrapper.SelectedValue)
    End Sub

    Private Sub btnFont_Click(sender As Object, e As EventArgs) Handles btnFont.Click
        FontDialogTooltip.ShowDialog()
        Dim font As Font = FontDialogTooltip.Font
        lblFontSplash.Text = $"{font.Name}, {font.Size}, {font.Style}"
    End Sub


    Private Sub btnFormTooltip_Click(sender As Object, e As EventArgs) Handles btnFormTooltip.Click
        Dim bitmap As Bitmap
        Using frm = New ToolTipSampleForm()
            bitmap = frm.GetBitmap()
        End Using

        ToolTipMain.ShowBitmap(bitmap, Me, 0, 0, Integer.MaxValue)
    End Sub

    Private Sub btnFormTooltipClose_Click(sender As Object, e As EventArgs) Handles btnFormTooltipClose.Click
        ToolTipMain.Hide(Me)
    End Sub

#End Region

#Region "ラッパー"
    Private Sub btnGenericSet_Click(sender As Object, e As EventArgs) Handles btnGenericSet.Click
        combSampleWrapper = New GenericComboBoxWrapper(Of Country)(combGeneric)
        For Each country As Country In SampleClass.CreateCountries()
            combSampleWrapper.Add(country.Name, country)
        Next
        combSampleWrapper.SelectedIndex = -1
        combSampleWrapper.SelectedIndex = 0
        hightlightingService.HighlightingControl = combGeneric
    End Sub

    Private Sub combGeneric_SelectedIndexChanged(sender As Object, e As EventArgs) Handles combGeneric.SelectedIndexChanged
        lstSampleWrapper = New GenericListBoxWrapper(Of City)(lstGeneric)
        If combSampleWrapper.SelectedValue Is Nothing Then Return
        For Each city As City In combSampleWrapper.SelectedValue.Cities
            lstSampleWrapper.Add(city.Name, city)
        Next
        hightlightingService.HighlightingControl = lstGeneric
    End Sub
    Private Sub lstGeneric_SelectedValueChanged(sender As Object, e As EventArgs) Handles lstGeneric.SelectedValueChanged
        If lstSampleWrapper?.SelectedValue Is Nothing Then Return
        Dim city As City = lstSampleWrapper.SelectedValue
        Console.WriteLine($"City{{ Name = {city.Name} }}")
        hightlightingService.HighlightingControl = txtDebug
    End Sub
    Private Sub checkedChanged(sender As Object, e As EventArgs) Handles optChecked.CheckedChanged, optUnchecked.CheckedChanged, optIndeterminate.CheckedChanged
        chkLarge.CheckState = radioButtons.SelectedValue
    End Sub
#End Region

#Region "StrictComboBox"
    Private Sub StrictComboBox1_SelectedIndexChangedAutomatically(sender As StrictComboBox, e As EventArgs, lastIndex As Integer, currentIndex As Integer) Handles StrictComboBox1.SelectedIndexChangedAutomatically
        Console.WriteLine($"自動でインデックスが変更されました。{lastIndex}→{currentIndex}")
    End Sub

    Private Sub StrictComboBox1_SelectedIndexChangedManually(sender As StrictComboBox, e As EventArgs, lastIndex As Integer, currentIndex As Integer) Handles StrictComboBox1.SelectedIndexChangedManually
        Console.WriteLine($"手動でインデックスを変更しました。{lastIndex}→{currentIndex}")
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim maxIndex As Integer = StrictComboBox1.Items.Count - 1
        StrictComboBox1.SelectedIndex = If(StrictComboBox1.SelectedIndex = maxIndex, 0, StrictComboBox1.SelectedIndex + 1)
    End Sub
#End Region

#Region "ExTextBox"

    ''' <summary>
    ''' テキストボックスのチェック関数
    ''' </summary>
    ''' <param name="text">検証対象の文字列</param>
    ''' <param name="result">結果返却用オブジェクト</param>
    Private Sub checkNumber(ByVal text As String, ByRef result As ValidationResult)
        If String.IsNullOrWhiteSpace(text) Then
            result.Pass = True
            Return
        End If

        Dim num As Integer
        If Not Integer.TryParse(text, num) Then
            result.Pass = False
            result.ErrorText = "整数値ではありません"
            Return
        End If

        If num < 0 Then
            result.Pass = False
            result.ErrorText = "0以上の値を入力してください"
            Return
        End If

    End Sub

    Private Sub combTrigger_SelectedIndexChanged(sender As Object, e As EventArgs) Handles combTrigger.SelectedIndexChanged
        txtNumber.ValidationTrigger = combTriggerWrapper.SelectedValue
    End Sub
    Private Sub combErrorPosition_SelectedIndexChanged(sender As Object, e As EventArgs) Handles combErrorPosition.SelectedIndexChanged
        txtNumber.ErrorDisplayPosition = combErrorPositionWrapper.SelectedValue
    End Sub

    Private Sub txtNumber_Validated(sender As Object, e As EventArgs) Handles txtNumber.Validated
        If txtNumber.IsError Then
            Console.WriteLine($"{txtNumber.Text} が入力されたが不正だったので前回の有効な値を復元")
            txtNumber.Text = txtNumber.LastValidValue
        End If
    End Sub
#End Region

#Region "ハイライト"
    Private Sub btnHighlight_Click(sender As Object, e As EventArgs) Handles btnHighlight.Click
        hightlightingService.HighlightingControl = btnLogClear
    End Sub

    Private Sub btnHighlightEnd_Click(sender As Object, e As EventArgs) Handles btnHighlightEnd.Click
        hightlightingService.HighlightingControl = Nothing
    End Sub

#End Region

    Private Sub btnLogClear_Click(sender As Object, e As EventArgs) Handles btnLogClear.Click
        txtDebug.Text = ""
    End Sub


    Private Sub btnDialog_Click(sender As Object, e As EventArgs) Handles btnDialog.Click
        ExMessageBox.FileDirectory = "C:\Users\USER\Downloads\"
        ExMessageBox.Compile()
        ExMessageBox.Show("なんかが入力されました。保存しますか？")
    End Sub

End Class
