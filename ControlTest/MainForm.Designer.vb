<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class MainForm
    Inherits System.Windows.Forms.Form

    'フォームがコンポーネントの一覧をクリーンアップするために dispose をオーバーライドします。
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Windows フォーム デザイナーで必要です。
    Private components As System.ComponentModel.IContainer

    'メモ: 以下のプロシージャは Windows フォーム デザイナーで必要です。
    'Windows フォーム デザイナーを使用して変更できます。  
    'コード エディターを使って変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.txtDebug = New System.Windows.Forms.TextBox()
        Me.btnTooltip = New System.Windows.Forms.Button()
        Me.BackgroundWorker1 = New System.ComponentModel.BackgroundWorker()
        Me.btnFont = New System.Windows.Forms.Button()
        Me.txtY = New System.Windows.Forms.TextBox()
        Me.txtX = New System.Windows.Forms.TextBox()
        Me.lblX = New System.Windows.Forms.Label()
        Me.lblY = New System.Windows.Forms.Label()
        Me.FontDialogTooltip = New System.Windows.Forms.FontDialog()
        Me.txtTooltip = New System.Windows.Forms.TextBox()
        Me.btnFormTooltip = New System.Windows.Forms.Button()
        Me.panelSplash = New System.Windows.Forms.Panel()
        Me.combDuration = New System.Windows.Forms.ComboBox()
        Me.lblFontSplash = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.lblTooltipForm = New System.Windows.Forms.Label()
        Me.btnFormTooltipClose = New System.Windows.Forms.Button()
        Me.btnLogClear = New System.Windows.Forms.Button()
        Me.panelToggle = New System.Windows.Forms.Panel()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.lblFont = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtToggleHeight = New System.Windows.Forms.TextBox()
        Me.ToggleSwitch1 = New SpecialControls.Switches.ToggleSwitch()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtToggleWidth = New System.Windows.Forms.TextBox()
        Me.btnToggleFonts = New System.Windows.Forms.Button()
        Me.btnFalseColor = New System.Windows.Forms.Button()
        Me.btnTrueColor = New System.Windows.Forms.Button()
        Me.txtFalse = New System.Windows.Forms.TextBox()
        Me.txtTrue = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ColorDialog1 = New System.Windows.Forms.ColorDialog()
        Me.FontDialogToggle = New System.Windows.Forms.FontDialog()
        Me.panelWrappers = New System.Windows.Forms.Panel()
        Me.btnGenericSet = New System.Windows.Forms.Button()
        Me.combGeneric = New System.Windows.Forms.ComboBox()
        Me.lstGeneric = New System.Windows.Forms.ListBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.StrictComboBox1 = New SpecialControls.CollectionViews.StrictComboBox()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.combErrorPosition = New SpecialControls.CollectionViews.StrictComboBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.combTrigger = New SpecialControls.CollectionViews.StrictComboBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtNumber = New SpecialControls.Inputting.ExTextBox()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.optIndeterminate = New SpecialControls.Switches.LargeRadioButton()
        Me.optUnchecked = New SpecialControls.Switches.LargeRadioButton()
        Me.optChecked = New SpecialControls.Switches.LargeRadioButton()
        Me.chkLarge = New SpecialControls.Switches.LargeCheckBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.btnHighlight = New System.Windows.Forms.Button()
        Me.btnHighlightEnd = New System.Windows.Forms.Button()
        Me.btnDialog = New System.Windows.Forms.Button()
        Me.ToolTipMain = New SpecialControls.ToolTipEx()
        Me.panelSplash.SuspendLayout()
        Me.panelToggle.SuspendLayout()
        CType(Me.ToggleSwitch1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.panelWrappers.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtDebug
        '
        Me.txtDebug.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtDebug.Location = New System.Drawing.Point(13, 432)
        Me.txtDebug.Margin = New System.Windows.Forms.Padding(4)
        Me.txtDebug.Multiline = True
        Me.txtDebug.Name = "txtDebug"
        Me.txtDebug.ReadOnly = True
        Me.txtDebug.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtDebug.Size = New System.Drawing.Size(1130, 143)
        Me.txtDebug.TabIndex = 4
        '
        'btnTooltip
        '
        Me.btnTooltip.Location = New System.Drawing.Point(297, 108)
        Me.btnTooltip.Margin = New System.Windows.Forms.Padding(4)
        Me.btnTooltip.Name = "btnTooltip"
        Me.btnTooltip.Size = New System.Drawing.Size(149, 29)
        Me.btnTooltip.TabIndex = 6
        Me.btnTooltip.Text = "テキストツールチップ表示"
        Me.btnTooltip.UseVisualStyleBackColor = True
        '
        'btnFont
        '
        Me.btnFont.Location = New System.Drawing.Point(145, 35)
        Me.btnFont.Name = "btnFont"
        Me.btnFont.Size = New System.Drawing.Size(80, 29)
        Me.btnFont.TabIndex = 10
        Me.btnFont.Text = "フォント設定"
        Me.btnFont.UseVisualStyleBackColor = True
        '
        'txtY
        '
        Me.txtY.Location = New System.Drawing.Point(39, 66)
        Me.txtY.Name = "txtY"
        Me.txtY.Size = New System.Drawing.Size(100, 23)
        Me.txtY.TabIndex = 11
        Me.txtY.Text = "500"
        '
        'txtX
        '
        Me.txtX.Location = New System.Drawing.Point(39, 37)
        Me.txtX.Name = "txtX"
        Me.txtX.Size = New System.Drawing.Size(100, 23)
        Me.txtX.TabIndex = 12
        Me.txtX.Text = "500"
        '
        'lblX
        '
        Me.lblX.AutoSize = True
        Me.lblX.Location = New System.Drawing.Point(18, 45)
        Me.lblX.Name = "lblX"
        Me.lblX.Size = New System.Drawing.Size(15, 15)
        Me.lblX.TabIndex = 13
        Me.lblX.Text = "X"
        '
        'lblY
        '
        Me.lblY.AutoSize = True
        Me.lblY.Location = New System.Drawing.Point(18, 69)
        Me.lblY.Name = "lblY"
        Me.lblY.Size = New System.Drawing.Size(15, 15)
        Me.lblY.TabIndex = 14
        Me.lblY.Text = "Y"
        '
        'txtTooltip
        '
        Me.txtTooltip.Location = New System.Drawing.Point(14, 97)
        Me.txtTooltip.Multiline = True
        Me.txtTooltip.Name = "txtTooltip"
        Me.txtTooltip.Size = New System.Drawing.Size(276, 40)
        Me.txtTooltip.TabIndex = 15
        Me.txtTooltip.Text = "クリップボードにコピーしました"
        '
        'btnFormTooltip
        '
        Me.btnFormTooltip.Location = New System.Drawing.Point(123, 156)
        Me.btnFormTooltip.Name = "btnFormTooltip"
        Me.btnFormTooltip.Size = New System.Drawing.Size(86, 23)
        Me.btnFormTooltip.TabIndex = 16
        Me.btnFormTooltip.Text = "永続表示"
        Me.btnFormTooltip.UseVisualStyleBackColor = True
        '
        'panelSplash
        '
        Me.panelSplash.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.panelSplash.Controls.Add(Me.combDuration)
        Me.panelSplash.Controls.Add(Me.lblFontSplash)
        Me.panelSplash.Controls.Add(Me.Label6)
        Me.panelSplash.Controls.Add(Me.lblTooltipForm)
        Me.panelSplash.Controls.Add(Me.btnFormTooltipClose)
        Me.panelSplash.Controls.Add(Me.txtTooltip)
        Me.panelSplash.Controls.Add(Me.btnFormTooltip)
        Me.panelSplash.Controls.Add(Me.btnTooltip)
        Me.panelSplash.Controls.Add(Me.btnFont)
        Me.panelSplash.Controls.Add(Me.lblY)
        Me.panelSplash.Controls.Add(Me.txtY)
        Me.panelSplash.Controls.Add(Me.lblX)
        Me.panelSplash.Controls.Add(Me.txtX)
        Me.panelSplash.Location = New System.Drawing.Point(18, 202)
        Me.panelSplash.Name = "panelSplash"
        Me.panelSplash.Size = New System.Drawing.Size(452, 194)
        Me.panelSplash.TabIndex = 17
        '
        'combDuration
        '
        Me.combDuration.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.combDuration.FormattingEnabled = True
        Me.combDuration.Location = New System.Drawing.Point(145, 69)
        Me.combDuration.Name = "combDuration"
        Me.combDuration.Size = New System.Drawing.Size(80, 23)
        Me.combDuration.TabIndex = 23
        '
        'lblFontSplash
        '
        Me.lblFontSplash.AutoSize = True
        Me.lblFontSplash.Location = New System.Drawing.Point(231, 42)
        Me.lblFontSplash.Name = "lblFontSplash"
        Me.lblFontSplash.Size = New System.Drawing.Size(70, 15)
        Me.lblFontSplash.TabIndex = 22
        Me.lblFontSplash.Text = "フォント名など"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Meiryo UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label6.Location = New System.Drawing.Point(3, 6)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(180, 26)
        Me.Label6.TabIndex = 21
        Me.Label6.Text = "スプラッシュメッセージ"
        '
        'lblTooltipForm
        '
        Me.lblTooltipForm.AutoSize = True
        Me.lblTooltipForm.Location = New System.Drawing.Point(30, 160)
        Me.lblTooltipForm.Name = "lblTooltipForm"
        Me.lblTooltipForm.Size = New System.Drawing.Size(85, 15)
        Me.lblTooltipForm.TabIndex = 18
        Me.lblTooltipForm.Text = "ツールチップ表示"
        '
        'btnFormTooltipClose
        '
        Me.btnFormTooltipClose.Location = New System.Drawing.Point(215, 156)
        Me.btnFormTooltipClose.Name = "btnFormTooltipClose"
        Me.btnFormTooltipClose.Size = New System.Drawing.Size(75, 23)
        Me.btnFormTooltipClose.TabIndex = 17
        Me.btnFormTooltipClose.Text = "閉じる"
        Me.btnFormTooltipClose.UseVisualStyleBackColor = True
        '
        'btnLogClear
        '
        Me.btnLogClear.Location = New System.Drawing.Point(13, 402)
        Me.btnLogClear.Name = "btnLogClear"
        Me.btnLogClear.Size = New System.Drawing.Size(75, 23)
        Me.btnLogClear.TabIndex = 18
        Me.btnLogClear.Text = "クリア"
        Me.btnLogClear.UseVisualStyleBackColor = True
        '
        'panelToggle
        '
        Me.panelToggle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.panelToggle.Controls.Add(Me.Label5)
        Me.panelToggle.Controls.Add(Me.lblFont)
        Me.panelToggle.Controls.Add(Me.Label3)
        Me.panelToggle.Controls.Add(Me.txtToggleHeight)
        Me.panelToggle.Controls.Add(Me.ToggleSwitch1)
        Me.panelToggle.Controls.Add(Me.Label4)
        Me.panelToggle.Controls.Add(Me.txtToggleWidth)
        Me.panelToggle.Controls.Add(Me.btnToggleFonts)
        Me.panelToggle.Controls.Add(Me.btnFalseColor)
        Me.panelToggle.Controls.Add(Me.btnTrueColor)
        Me.panelToggle.Controls.Add(Me.txtFalse)
        Me.panelToggle.Controls.Add(Me.txtTrue)
        Me.panelToggle.Controls.Add(Me.Label2)
        Me.panelToggle.Controls.Add(Me.Label1)
        Me.panelToggle.Location = New System.Drawing.Point(18, 12)
        Me.panelToggle.Name = "panelToggle"
        Me.panelToggle.Size = New System.Drawing.Size(452, 184)
        Me.panelToggle.TabIndex = 19
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Meiryo UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label5.Location = New System.Drawing.Point(3, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(184, 26)
        Me.Label5.TabIndex = 20
        Me.Label5.Text = "モダンなトグルスイッチ"
        '
        'lblFont
        '
        Me.lblFont.AutoSize = True
        Me.lblFont.Location = New System.Drawing.Point(325, 95)
        Me.lblFont.Name = "lblFont"
        Me.lblFont.Size = New System.Drawing.Size(70, 15)
        Me.lblFont.TabIndex = 20
        Me.lblFont.Text = "フォント名など"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(236, 156)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(27, 15)
        Me.Label3.TabIndex = 19
        Me.Label3.Text = "高さ"
        '
        'txtToggleHeight
        '
        Me.txtToggleHeight.Location = New System.Drawing.Point(269, 153)
        Me.txtToggleHeight.Name = "txtToggleHeight"
        Me.txtToggleHeight.Size = New System.Drawing.Size(100, 23)
        Me.txtToggleHeight.TabIndex = 16
        Me.txtToggleHeight.Text = "200"
        '
        'ToggleSwitch1
        '
        Me.ToggleSwitch1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ToggleSwitch1.EnableAnimation = True
        Me.ToggleSwitch1.FalseColor = System.Drawing.Color.DarkGray
        Me.ToggleSwitch1.FalseText = "無効"
        Me.ToggleSwitch1.FontName = "Meiryo UI"
        Me.ToggleSwitch1.FontSize = 14.0!
        Me.ToggleSwitch1.FontStyle = System.Drawing.FontStyle.Bold
        Me.ToggleSwitch1.IsChecked = True
        Me.ToggleSwitch1.Location = New System.Drawing.Point(17, 29)
        Me.ToggleSwitch1.Name = "ToggleSwitch1"
        Me.ToggleSwitch1.Size = New System.Drawing.Size(97, 39)
        Me.ToggleSwitch1.TabIndex = 8
        Me.ToggleSwitch1.TabStop = False
        Me.ToggleSwitch1.TrueColor = System.Drawing.Color.Lime
        Me.ToggleSwitch1.TrueText = "有効"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(236, 132)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(19, 15)
        Me.Label4.TabIndex = 18
        Me.Label4.Text = "幅"
        '
        'txtToggleWidth
        '
        Me.txtToggleWidth.Location = New System.Drawing.Point(269, 124)
        Me.txtToggleWidth.Name = "txtToggleWidth"
        Me.txtToggleWidth.Size = New System.Drawing.Size(100, 23)
        Me.txtToggleWidth.TabIndex = 17
        Me.txtToggleWidth.Text = "100"
        '
        'btnToggleFonts
        '
        Me.btnToggleFonts.Location = New System.Drawing.Point(239, 88)
        Me.btnToggleFonts.Name = "btnToggleFonts"
        Me.btnToggleFonts.Size = New System.Drawing.Size(80, 29)
        Me.btnToggleFonts.TabIndex = 15
        Me.btnToggleFonts.Text = "フォント設定"
        Me.btnToggleFonts.UseVisualStyleBackColor = True
        '
        'btnFalseColor
        '
        Me.btnFalseColor.Location = New System.Drawing.Point(123, 124)
        Me.btnFalseColor.Name = "btnFalseColor"
        Me.btnFalseColor.Size = New System.Drawing.Size(75, 23)
        Me.btnFalseColor.TabIndex = 14
        Me.btnFalseColor.Text = "OFFの色"
        Me.btnFalseColor.UseVisualStyleBackColor = True
        '
        'btnTrueColor
        '
        Me.btnTrueColor.Location = New System.Drawing.Point(15, 124)
        Me.btnTrueColor.Name = "btnTrueColor"
        Me.btnTrueColor.Size = New System.Drawing.Size(75, 23)
        Me.btnTrueColor.TabIndex = 13
        Me.btnTrueColor.Text = "ONの色"
        Me.btnTrueColor.UseVisualStyleBackColor = True
        '
        'txtFalse
        '
        Me.txtFalse.Location = New System.Drawing.Point(120, 94)
        Me.txtFalse.Name = "txtFalse"
        Me.txtFalse.Size = New System.Drawing.Size(100, 23)
        Me.txtFalse.TabIndex = 12
        '
        'txtTrue
        '
        Me.txtTrue.Location = New System.Drawing.Point(14, 94)
        Me.txtTrue.Name = "txtTrue"
        Me.txtTrue.Size = New System.Drawing.Size(100, 23)
        Me.txtTrue.TabIndex = 11
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(120, 76)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(57, 15)
        Me.Label2.TabIndex = 10
        Me.Label2.Text = "OFFのとき"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(14, 76)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(52, 15)
        Me.Label1.TabIndex = 9
        Me.Label1.Text = "ONのとき"
        '
        'panelWrappers
        '
        Me.panelWrappers.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.panelWrappers.Controls.Add(Me.btnGenericSet)
        Me.panelWrappers.Controls.Add(Me.combGeneric)
        Me.panelWrappers.Controls.Add(Me.lstGeneric)
        Me.panelWrappers.Controls.Add(Me.Label7)
        Me.panelWrappers.Location = New System.Drawing.Point(476, 13)
        Me.panelWrappers.Name = "panelWrappers"
        Me.panelWrappers.Size = New System.Drawing.Size(529, 117)
        Me.panelWrappers.TabIndex = 20
        '
        'btnGenericSet
        '
        Me.btnGenericSet.Location = New System.Drawing.Point(8, 87)
        Me.btnGenericSet.Name = "btnGenericSet"
        Me.btnGenericSet.Size = New System.Drawing.Size(75, 23)
        Me.btnGenericSet.TabIndex = 24
        Me.btnGenericSet.Text = "データセット"
        Me.btnGenericSet.UseVisualStyleBackColor = True
        '
        'combGeneric
        '
        Me.combGeneric.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.combGeneric.FormattingEnabled = True
        Me.combGeneric.Location = New System.Drawing.Point(8, 44)
        Me.combGeneric.Name = "combGeneric"
        Me.combGeneric.Size = New System.Drawing.Size(163, 23)
        Me.combGeneric.TabIndex = 23
        '
        'lstGeneric
        '
        Me.lstGeneric.FormattingEnabled = True
        Me.lstGeneric.ItemHeight = 15
        Me.lstGeneric.Location = New System.Drawing.Point(186, 44)
        Me.lstGeneric.Name = "lstGeneric"
        Me.lstGeneric.Size = New System.Drawing.Size(216, 64)
        Me.lstGeneric.TabIndex = 22
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Meiryo UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label7.Location = New System.Drawing.Point(3, 9)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(352, 26)
        Me.Label7.TabIndex = 21
        Me.Label7.Text = "任意の型のコレクションを紐付けるラッパー"
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(240, 51)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(116, 23)
        Me.Button1.TabIndex = 21
        Me.Button1.Text = "インデックス＋１"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.Label8)
        Me.Panel1.Controls.Add(Me.StrictComboBox1)
        Me.Panel1.Controls.Add(Me.Button1)
        Me.Panel1.Location = New System.Drawing.Point(476, 137)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(529, 89)
        Me.Panel1.TabIndex = 23
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Meiryo UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label8.Location = New System.Drawing.Point(4, 8)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(494, 26)
        Me.Label8.TabIndex = 23
        Me.Label8.Text = "IndexChangedが手動か自動か区別できるコンボボックス"
        '
        'StrictComboBox1
        '
        Me.StrictComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.StrictComboBox1.FormattingEnabled = True
        Me.StrictComboBox1.IntegralHeight = False
        Me.StrictComboBox1.IsStrictOccuration = True
        Me.StrictComboBox1.Items.AddRange(New Object() {"1", "2", "3", "4", "5", "6", "7"})
        Me.StrictComboBox1.Location = New System.Drawing.Point(9, 51)
        Me.StrictComboBox1.Name = "StrictComboBox1"
        Me.StrictComboBox1.Size = New System.Drawing.Size(199, 23)
        Me.StrictComboBox1.TabIndex = 22
        '
        'Panel2
        '
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Controls.Add(Me.Label11)
        Me.Panel2.Controls.Add(Me.combErrorPosition)
        Me.Panel2.Controls.Add(Me.Label10)
        Me.Panel2.Controls.Add(Me.combTrigger)
        Me.Panel2.Controls.Add(Me.Label9)
        Me.Panel2.Controls.Add(Me.txtNumber)
        Me.Panel2.Location = New System.Drawing.Point(476, 232)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(529, 98)
        Me.Panel2.TabIndex = 25
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(267, 34)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(103, 15)
        Me.Label11.TabIndex = 29
        Me.Label11.Text = "エラーメッセージ位置"
        '
        'combErrorPosition
        '
        Me.combErrorPosition.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.combErrorPosition.FormattingEnabled = True
        Me.combErrorPosition.IntegralHeight = False
        Me.combErrorPosition.IsStrictOccuration = False
        Me.combErrorPosition.Location = New System.Drawing.Point(382, 31)
        Me.combErrorPosition.Name = "combErrorPosition"
        Me.combErrorPosition.Size = New System.Drawing.Size(129, 23)
        Me.combErrorPosition.TabIndex = 28
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(267, 8)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(111, 15)
        Me.Label10.TabIndex = 27
        Me.Label10.Text = "バリデーションタイミング"
        '
        'combTrigger
        '
        Me.combTrigger.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.combTrigger.FormattingEnabled = True
        Me.combTrigger.IntegralHeight = False
        Me.combTrigger.IsStrictOccuration = False
        Me.combTrigger.Location = New System.Drawing.Point(382, 5)
        Me.combTrigger.Name = "combTrigger"
        Me.combTrigger.Size = New System.Drawing.Size(129, 23)
        Me.combTrigger.TabIndex = 26
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Meiryo UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label9.Location = New System.Drawing.Point(4, 9)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(196, 26)
        Me.Label9.TabIndex = 25
        Me.Label9.Text = "便利なテキストボックス"
        '
        'txtNumber
        '
        Me.txtNumber.ErrorDisplayPosition = SpecialControls.Inputting.ExTextBox.ErrorDisplayPositionType.Bottom
        Me.txtNumber.Location = New System.Drawing.Point(8, 55)
        Me.txtNumber.Name = "txtNumber"
        Me.txtNumber.Size = New System.Drawing.Size(199, 23)
        Me.txtNumber.TabIndex = 24
        Me.txtNumber.ValidationTrigger = SpecialControls.Inputting.ExTextBox.ValidationTriggerType.FocusLeave
        Me.txtNumber.WatermarkText = "整数値を入力してください"
        '
        'Panel3
        '
        Me.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel3.Controls.Add(Me.Label12)
        Me.Panel3.Controls.Add(Me.optIndeterminate)
        Me.Panel3.Controls.Add(Me.optUnchecked)
        Me.Panel3.Controls.Add(Me.optChecked)
        Me.Panel3.Controls.Add(Me.chkLarge)
        Me.Panel3.Controls.Add(Me.Label14)
        Me.Panel3.Location = New System.Drawing.Point(476, 336)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(529, 89)
        Me.Panel3.TabIndex = 31
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Meiryo UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label12.Location = New System.Drawing.Point(235, 9)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(163, 26)
        Me.Label12.TabIndex = 34
        Me.Label12.Text = "大きなラジオボタン"
        '
        'optIndeterminate
        '
        Me.optIndeterminate.AutoSize = True
        Me.optIndeterminate.CircleBorderWidth = 1
        Me.optIndeterminate.Cursor = System.Windows.Forms.Cursors.Hand
        Me.optIndeterminate.Font = New System.Drawing.Font("Meiryo UI", 12.0!)
        Me.optIndeterminate.HoverColor = System.Drawing.Color.FromArgb(CType(CType(17, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.optIndeterminate.Location = New System.Drawing.Point(311, 46)
        Me.optIndeterminate.Name = "optIndeterminate"
        Me.optIndeterminate.Size = New System.Drawing.Size(59, 24)
        Me.optIndeterminate.TabIndex = 33
        Me.optIndeterminate.Text = "中間"
        Me.optIndeterminate.UseVisualStyleBackColor = True
        '
        'optUnchecked
        '
        Me.optUnchecked.AutoSize = True
        Me.optUnchecked.CircleBorderWidth = 1
        Me.optUnchecked.Cursor = System.Windows.Forms.Cursors.Hand
        Me.optUnchecked.Font = New System.Drawing.Font("Meiryo UI", 12.0!)
        Me.optUnchecked.HoverColor = System.Drawing.Color.FromArgb(CType(CType(17, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.optUnchecked.Location = New System.Drawing.Point(382, 46)
        Me.optUnchecked.Name = "optUnchecked"
        Me.optUnchecked.Size = New System.Drawing.Size(75, 24)
        Me.optUnchecked.TabIndex = 32
        Me.optUnchecked.Text = "非選択"
        Me.optUnchecked.UseVisualStyleBackColor = True
        '
        'optChecked
        '
        Me.optChecked.AutoSize = True
        Me.optChecked.Checked = True
        Me.optChecked.CircleBorderWidth = 1
        Me.optChecked.Cursor = System.Windows.Forms.Cursors.Hand
        Me.optChecked.Font = New System.Drawing.Font("Meiryo UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.optChecked.HoverColor = System.Drawing.Color.FromArgb(CType(CType(17, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.optChecked.Location = New System.Drawing.Point(240, 46)
        Me.optChecked.Name = "optChecked"
        Me.optChecked.Size = New System.Drawing.Size(59, 24)
        Me.optChecked.TabIndex = 31
        Me.optChecked.TabStop = True
        Me.optChecked.Text = "選択"
        Me.optChecked.UseVisualStyleBackColor = True
        '
        'chkLarge
        '
        Me.chkLarge.Cursor = System.Windows.Forms.Cursors.Hand
        Me.chkLarge.Font = New System.Drawing.Font("Meiryo UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkLarge.HoverColor = System.Drawing.Color.FromArgb(CType(CType(17, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.chkLarge.Location = New System.Drawing.Point(9, 38)
        Me.chkLarge.Name = "chkLarge"
        Me.chkLarge.Size = New System.Drawing.Size(127, 32)
        Me.chkLarge.TabIndex = 30
        Me.chkLarge.Text = "高さMax"
        Me.chkLarge.UseVisualStyleBackColor = True
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Meiryo UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label14.Location = New System.Drawing.Point(4, 9)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(183, 26)
        Me.Label14.TabIndex = 25
        Me.Label14.Text = "大きなチェックボックス"
        '
        'btnHighlight
        '
        Me.btnHighlight.Location = New System.Drawing.Point(94, 402)
        Me.btnHighlight.Name = "btnHighlight"
        Me.btnHighlight.Size = New System.Drawing.Size(118, 23)
        Me.btnHighlight.TabIndex = 32
        Me.btnHighlight.Text = "←ハイライトする"
        Me.btnHighlight.UseVisualStyleBackColor = True
        '
        'btnHighlightEnd
        '
        Me.btnHighlightEnd.Location = New System.Drawing.Point(220, 402)
        Me.btnHighlightEnd.Name = "btnHighlightEnd"
        Me.btnHighlightEnd.Size = New System.Drawing.Size(118, 23)
        Me.btnHighlightEnd.TabIndex = 33
        Me.btnHighlightEnd.Text = "ハイライト削除"
        Me.btnHighlightEnd.UseVisualStyleBackColor = True
        '
        'btnDialog
        '
        Me.btnDialog.Location = New System.Drawing.Point(1011, 13)
        Me.btnDialog.Name = "btnDialog"
        Me.btnDialog.Size = New System.Drawing.Size(138, 39)
        Me.btnDialog.TabIndex = 34
        Me.btnDialog.Text = "カスタムメッセージボックス"
        Me.btnDialog.UseVisualStyleBackColor = True
        '
        'ToolTipMain
        '
        Me.ToolTipMain.OwnerDraw = True
        Me.ToolTipMain.ShowAlways = True
        '
        'MainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1161, 588)
        Me.Controls.Add(Me.btnDialog)
        Me.Controls.Add(Me.btnHighlightEnd)
        Me.Controls.Add(Me.btnHighlight)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.panelWrappers)
        Me.Controls.Add(Me.panelToggle)
        Me.Controls.Add(Me.btnLogClear)
        Me.Controls.Add(Me.panelSplash)
        Me.Controls.Add(Me.txtDebug)
        Me.Font = New System.Drawing.Font("Meiryo UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "MainForm"
        Me.Text = "コントロール見本"
        Me.panelSplash.ResumeLayout(False)
        Me.panelSplash.PerformLayout()
        Me.panelToggle.ResumeLayout(False)
        Me.panelToggle.PerformLayout()
        CType(Me.ToggleSwitch1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.panelWrappers.ResumeLayout(False)
        Me.panelWrappers.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtDebug As TextBox
    Friend WithEvents btnTooltip As Button
    Friend WithEvents BackgroundWorker1 As System.ComponentModel.BackgroundWorker
    Friend WithEvents ToggleSwitch1 As SpecialControls.Switches.ToggleSwitch
    Friend WithEvents btnFont As Button
    Friend WithEvents txtY As TextBox
    Friend WithEvents txtX As TextBox
    Friend WithEvents lblX As Label
    Friend WithEvents lblY As Label
    Friend WithEvents FontDialogTooltip As FontDialog
    Friend WithEvents txtTooltip As TextBox
    Friend WithEvents btnFormTooltip As Button
    Friend WithEvents panelSplash As Panel
    Friend WithEvents btnLogClear As Button
    Friend WithEvents btnFormTooltipClose As Button
    Friend WithEvents panelToggle As Panel
    Friend WithEvents btnToggleFonts As Button
    Friend WithEvents btnFalseColor As Button
    Friend WithEvents btnTrueColor As Button
    Friend WithEvents txtFalse As TextBox
    Friend WithEvents txtTrue As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents ColorDialog1 As ColorDialog
    Friend WithEvents FontDialogToggle As FontDialog
    Friend WithEvents Label3 As Label
    Friend WithEvents txtToggleHeight As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents txtToggleWidth As TextBox
    Friend WithEvents lblFont As Label
    Friend WithEvents lblTooltipForm As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents lblFontSplash As Label
    Friend WithEvents combDuration As ComboBox
    Friend WithEvents panelWrappers As Panel
    Friend WithEvents Label7 As Label
    Friend WithEvents combGeneric As ComboBox
    Friend WithEvents lstGeneric As ListBox
    Friend WithEvents btnGenericSet As Button
    Friend WithEvents Button1 As Button
    Friend WithEvents StrictComboBox1 As SpecialControls.CollectionViews.StrictComboBox
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Label8 As Label
    Friend WithEvents txtNumber As SpecialControls.Inputting.ExTextBox
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Label10 As Label
    Friend WithEvents combTrigger As SpecialControls.CollectionViews.StrictComboBox
    Friend WithEvents Label9 As Label
    Friend WithEvents Label11 As Label
    Friend WithEvents combErrorPosition As SpecialControls.CollectionViews.StrictComboBox
    Friend WithEvents chkLarge As SpecialControls.Switches.LargeCheckBox
    Friend WithEvents Panel3 As Panel
    Friend WithEvents optIndeterminate As SpecialControls.Switches.LargeRadioButton
    Friend WithEvents optUnchecked As SpecialControls.Switches.LargeRadioButton
    Friend WithEvents optChecked As SpecialControls.Switches.LargeRadioButton
    Friend WithEvents Label14 As Label
    Friend WithEvents Label12 As Label
    Friend WithEvents btnHighlight As Button
    Friend WithEvents btnHighlightEnd As Button
    Friend WithEvents btnDialog As Button
    Friend WithEvents ToolTipMain As SpecialControls.ToolTipEx
End Class
