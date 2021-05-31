Option Explicit On
Option Strict On

Imports SpecialControls


Public Class MainForm

    Dim frm As MessageForm = New MessageForm()
    Dim tooltipForm As SplashMessage = New SplashMessage()
    Dim combDurationWrapper As GenericComboBoxWrapper(Of SplashMessage.DisplayDuration)

    Dim combSampleWrapper As GenericComboBoxWrapper(Of Country)
    Dim lstSampleWrapper As GenericListBoxWrapper(Of City)

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
        combDurationWrapper.Add("約1秒", SplashMessage.DisplayDuration.ShortTime)
        combDurationWrapper.Add("約3秒", SplashMessage.DisplayDuration.LongTime)
        combDurationWrapper.Add("無限", SplashMessage.DisplayDuration.Endless)
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
        Dim tooltip As SplashMessage = New SplashMessage()
        Dim bmt As Bitmap = SplashMessage.CreateTextImage(txtTooltip.Text, FontDialogTooltip.Font)
        tooltip.Show(bmt, Integer.Parse(txtX.Text), Integer.Parse(txtY.Text), combDurationWrapper.GetSelectedValue())
    End Sub

    Private Sub btnFont_Click(sender As Object, e As EventArgs) Handles btnFont.Click
        FontDialogTooltip.ShowDialog()
        Dim font As Font = FontDialogTooltip.Font
        lblFontSplash.Text = $"{font.Name}, {font.Size}, {font.Style}"
    End Sub


    Private Sub btnFormTooltip_Click(sender As Object, e As EventArgs) Handles btnFormTooltip.Click
        frm.ShowSplash(Integer.Parse(txtX.Text), Integer.Parse(txtY.Text), SplashMessage.DisplayDuration.Endless)
    End Sub

    Private Sub btnFormTooltipClose_Click(sender As Object, e As EventArgs) Handles btnFormTooltipClose.Click
        tooltipForm.Hide()
        frm.HideSplash()
    End Sub

    Private Sub lblTooltipForm_MouseEnter(sender As Object, e As EventArgs) Handles lblTooltipForm.MouseEnter
        frm.ShowSplash(Integer.Parse(txtX.Text), Integer.Parse(txtY.Text), SplashMessage.DisplayDuration.Endless)
    End Sub

    Private Sub lblTooltipForm_MouseLeave(sender As Object, e As EventArgs) Handles lblTooltipForm.MouseLeave
        frm.HideSplash()
    End Sub

#End Region


#Region "ラッパー"
    Private Sub btnGenericSet_Click(sender As Object, e As EventArgs) Handles btnGenericSet.Click
        combSampleWrapper = New GenericComboBoxWrapper(Of Country)(combGeneric)
        For Each country As Country In SampleClass.CreateCountries()
            combSampleWrapper.Add(country.Name, country)
        Next
        combGeneric.SelectedIndex = -1
        combGeneric.SelectedIndex = 0
    End Sub

    Private Sub combGeneric_SelectedIndexChanged(sender As Object, e As EventArgs) Handles combGeneric.SelectedIndexChanged
        lstSampleWrapper = New GenericListBoxWrapper(Of City)(lstGeneric)
        If combSampleWrapper.GetSelectedValue() Is Nothing Then Return
        For Each city As City In combSampleWrapper.GetSelectedValue().Cities
            lstSampleWrapper.Add(city.Name, city)
        Next
    End Sub
    Private Sub lstGeneric_SelectedValueChanged(sender As Object, e As EventArgs) Handles lstGeneric.SelectedValueChanged
        If lstSampleWrapper?.GetSelectedValue() Is Nothing Then Return
        Dim city As City = lstSampleWrapper.GetSelectedValue()
        Console.WriteLine($"City{{ Name = {city.Name} }}")
    End Sub
#End Region

    Private Sub btnLogClear_Click(sender As Object, e As EventArgs) Handles btnLogClear.Click
        txtDebug.Text = ""
    End Sub


End Class
