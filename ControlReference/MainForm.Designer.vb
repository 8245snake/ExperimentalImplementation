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
        Me.panelMain = New System.Windows.Forms.FlowLayoutPanel()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.txtWaterMark = New System.Windows.Forms.TextBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.lblSizeError = New System.Windows.Forms.Label()
        Me.combSizeLimit = New System.Windows.Forms.ComboBox()
        Me.txtSizeLimited = New System.Windows.Forms.TextBox()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.chkAction2 = New System.Windows.Forms.CheckBox()
        Me.chkAction1 = New System.Windows.Forms.CheckBox()
        Me.chkValidate2 = New System.Windows.Forms.CheckBox()
        Me.chkValidate1 = New System.Windows.Forms.CheckBox()
        Me.lblValidate = New System.Windows.Forms.Label()
        Me.txtValidation = New System.Windows.Forms.TextBox()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.btnEnlarge = New System.Windows.Forms.Button()
        Me.chkDecoration = New System.Windows.Forms.CheckBox()
        Me.panelMain.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.SuspendLayout()
        '
        'panelMain
        '
        Me.panelMain.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.panelMain.AutoScroll = True
        Me.panelMain.Controls.Add(Me.GroupBox1)
        Me.panelMain.Controls.Add(Me.GroupBox2)
        Me.panelMain.Controls.Add(Me.GroupBox3)
        Me.panelMain.Controls.Add(Me.GroupBox4)
        Me.panelMain.FlowDirection = System.Windows.Forms.FlowDirection.TopDown
        Me.panelMain.Location = New System.Drawing.Point(12, 12)
        Me.panelMain.Name = "panelMain"
        Me.panelMain.Size = New System.Drawing.Size(1078, 513)
        Me.panelMain.TabIndex = 0
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txtWaterMark)
        Me.GroupBox1.Location = New System.Drawing.Point(3, 3)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(348, 100)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "ウォーターマーク表示"
        '
        'txtWaterMark
        '
        Me.txtWaterMark.Location = New System.Drawing.Point(6, 22)
        Me.txtWaterMark.Multiline = True
        Me.txtWaterMark.Name = "txtWaterMark"
        Me.txtWaterMark.Size = New System.Drawing.Size(336, 57)
        Me.txtWaterMark.TabIndex = 1
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.lblSizeError)
        Me.GroupBox2.Controls.Add(Me.combSizeLimit)
        Me.GroupBox2.Controls.Add(Me.txtSizeLimited)
        Me.GroupBox2.Location = New System.Drawing.Point(3, 109)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(348, 118)
        Me.GroupBox2.TabIndex = 2
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "バイト数制限"
        '
        'lblSizeError
        '
        Me.lblSizeError.AutoSize = True
        Me.lblSizeError.ForeColor = System.Drawing.Color.Red
        Me.lblSizeError.Location = New System.Drawing.Point(99, 86)
        Me.lblSizeError.Name = "lblSizeError"
        Me.lblSizeError.Size = New System.Drawing.Size(45, 15)
        Me.lblSizeError.TabIndex = 3
        Me.lblSizeError.Text = "Label1"
        Me.lblSizeError.Visible = False
        '
        'combSizeLimit
        '
        Me.combSizeLimit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.combSizeLimit.FormattingEnabled = True
        Me.combSizeLimit.Location = New System.Drawing.Point(7, 86)
        Me.combSizeLimit.Name = "combSizeLimit"
        Me.combSizeLimit.Size = New System.Drawing.Size(86, 23)
        Me.combSizeLimit.TabIndex = 2
        '
        'txtSizeLimited
        '
        Me.txtSizeLimited.Location = New System.Drawing.Point(6, 22)
        Me.txtSizeLimited.Multiline = True
        Me.txtSizeLimited.Name = "txtSizeLimited"
        Me.txtSizeLimited.Size = New System.Drawing.Size(336, 57)
        Me.txtSizeLimited.TabIndex = 1
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.chkAction2)
        Me.GroupBox3.Controls.Add(Me.chkAction1)
        Me.GroupBox3.Controls.Add(Me.chkValidate2)
        Me.GroupBox3.Controls.Add(Me.chkValidate1)
        Me.GroupBox3.Controls.Add(Me.lblValidate)
        Me.GroupBox3.Controls.Add(Me.txtValidation)
        Me.GroupBox3.Location = New System.Drawing.Point(3, 233)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(348, 131)
        Me.GroupBox3.TabIndex = 3
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "バリデーション"
        '
        'chkAction2
        '
        Me.chkAction2.AutoSize = True
        Me.chkAction2.Location = New System.Drawing.Point(136, 100)
        Me.chkAction2.Name = "chkAction2"
        Me.chkAction2.Size = New System.Drawing.Size(78, 19)
        Me.chkAction2.TabIndex = 8
        Me.chkAction2.Text = "ラベル表示"
        Me.chkAction2.UseVisualStyleBackColor = True
        '
        'chkAction1
        '
        Me.chkAction1.AutoSize = True
        Me.chkAction1.Checked = True
        Me.chkAction1.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkAction1.Location = New System.Drawing.Point(136, 75)
        Me.chkAction1.Name = "chkAction1"
        Me.chkAction1.Size = New System.Drawing.Size(74, 19)
        Me.chkAction1.TabIndex = 7
        Me.chkAction1.Text = "赤枠表示"
        Me.chkAction1.UseVisualStyleBackColor = True
        '
        'chkValidate2
        '
        Me.chkValidate2.AutoSize = True
        Me.chkValidate2.Location = New System.Drawing.Point(7, 100)
        Me.chkValidate2.Name = "chkValidate2"
        Me.chkValidate2.Size = New System.Drawing.Size(103, 19)
        Me.chkValidate2.TabIndex = 6
        Me.chkValidate2.Text = "３の倍数チェック"
        Me.chkValidate2.UseVisualStyleBackColor = True
        '
        'chkValidate1
        '
        Me.chkValidate1.AutoSize = True
        Me.chkValidate1.Checked = True
        Me.chkValidate1.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkValidate1.Location = New System.Drawing.Point(9, 75)
        Me.chkValidate1.Name = "chkValidate1"
        Me.chkValidate1.Size = New System.Drawing.Size(81, 19)
        Me.chkValidate1.TabIndex = 5
        Me.chkValidate1.Text = "整数チェック"
        Me.chkValidate1.UseVisualStyleBackColor = True
        '
        'lblValidate
        '
        Me.lblValidate.AutoSize = True
        Me.lblValidate.ForeColor = System.Drawing.Color.Red
        Me.lblValidate.Location = New System.Drawing.Point(6, 53)
        Me.lblValidate.Name = "lblValidate"
        Me.lblValidate.Size = New System.Drawing.Size(45, 15)
        Me.lblValidate.TabIndex = 4
        Me.lblValidate.Text = "Label1"
        Me.lblValidate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblValidate.Visible = False
        '
        'txtValidation
        '
        Me.txtValidation.Location = New System.Drawing.Point(6, 22)
        Me.txtValidation.Multiline = True
        Me.txtValidation.Name = "txtValidation"
        Me.txtValidation.Size = New System.Drawing.Size(336, 28)
        Me.txtValidation.TabIndex = 1
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.btnEnlarge)
        Me.GroupBox4.Controls.Add(Me.chkDecoration)
        Me.GroupBox4.Location = New System.Drawing.Point(3, 370)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(348, 56)
        Me.GroupBox4.TabIndex = 9
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "チェックボックスのデコレーション"
        '
        'btnEnlarge
        '
        Me.btnEnlarge.Location = New System.Drawing.Point(258, 24)
        Me.btnEnlarge.Name = "btnEnlarge"
        Me.btnEnlarge.Size = New System.Drawing.Size(75, 23)
        Me.btnEnlarge.TabIndex = 6
        Me.btnEnlarge.Text = "縮小"
        Me.btnEnlarge.UseVisualStyleBackColor = True
        '
        'chkDecoration
        '
        Me.chkDecoration.AutoSize = True
        Me.chkDecoration.Font = New System.Drawing.Font("Meiryo UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkDecoration.Location = New System.Drawing.Point(9, 24)
        Me.chkDecoration.Name = "chkDecoration"
        Me.chkDecoration.Size = New System.Drawing.Size(110, 23)
        Me.chkDecoration.TabIndex = 5
        Me.chkDecoration.Text = "チェックボックス"
        Me.chkDecoration.UseVisualStyleBackColor = True
        '
        'MainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1102, 537)
        Me.Controls.Add(Me.panelMain)
        Me.Font = New System.Drawing.Font("Meiryo UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "MainForm"
        Me.Text = "Form1"
        Me.panelMain.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents panelMain As FlowLayoutPanel
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents txtWaterMark As TextBox
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents txtSizeLimited As TextBox
    Friend WithEvents combSizeLimit As ComboBox
    Friend WithEvents lblSizeError As Label
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents txtValidation As TextBox
    Friend WithEvents lblValidate As Label
    Friend WithEvents chkAction2 As CheckBox
    Friend WithEvents chkAction1 As CheckBox
    Friend WithEvents chkValidate2 As CheckBox
    Friend WithEvents chkValidate1 As CheckBox
    Friend WithEvents GroupBox4 As GroupBox
    Friend WithEvents chkDecoration As CheckBox
    Friend WithEvents btnEnlarge As Button
End Class
