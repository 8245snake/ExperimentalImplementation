
Namespace Messaging

    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
    Partial Class RichMessageDialog
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
            Me.FlowLayoutPanel1 = New System.Windows.Forms.FlowLayoutPanel()
            Me.btnCancel = New System.Windows.Forms.Button()
            Me.btnOK = New System.Windows.Forms.Button()
            Me.rtxtMain = New SpecialControls.Inputting.TransparentRichTextBox()
            Me.FlowLayoutPanel1.SuspendLayout()
            Me.SuspendLayout()
            '
            'FlowLayoutPanel1
            '
            Me.FlowLayoutPanel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.FlowLayoutPanel1.Controls.Add(Me.btnCancel)
            Me.FlowLayoutPanel1.Controls.Add(Me.btnOK)
            Me.FlowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft
            Me.FlowLayoutPanel1.Location = New System.Drawing.Point(280, 222)
            Me.FlowLayoutPanel1.Name = "FlowLayoutPanel1"
            Me.FlowLayoutPanel1.Size = New System.Drawing.Size(365, 36)
            Me.FlowLayoutPanel1.TabIndex = 1
            '
            'btnCancel
            '
            Me.btnCancel.Location = New System.Drawing.Point(274, 3)
            Me.btnCancel.Name = "btnCancel"
            Me.btnCancel.Size = New System.Drawing.Size(88, 30)
            Me.btnCancel.TabIndex = 1
            Me.btnCancel.Text = "Cancel"
            Me.btnCancel.UseVisualStyleBackColor = True
            '
            'btnOK
            '
            Me.btnOK.Location = New System.Drawing.Point(180, 3)
            Me.btnOK.Name = "btnOK"
            Me.btnOK.Size = New System.Drawing.Size(88, 30)
            Me.btnOK.TabIndex = 0
            Me.btnOK.Text = "OK"
            Me.btnOK.UseVisualStyleBackColor = True
            '
            'rtxtMain
            '
            Me.rtxtMain.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.rtxtMain.BackColor = System.Drawing.SystemColors.Control
            Me.rtxtMain.BorderStyle = System.Windows.Forms.BorderStyle.None
            Me.rtxtMain.IsTransparent = True
            Me.rtxtMain.Location = New System.Drawing.Point(12, 12)
            Me.rtxtMain.Name = "rtxtMain"
            Me.rtxtMain.ReadOnly = True
            Me.rtxtMain.Size = New System.Drawing.Size(639, 196)
            Me.rtxtMain.TabIndex = 0
            Me.rtxtMain.Text = ""
            '
            'RichMessageDialog
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.BackColor = System.Drawing.SystemColors.Control
            Me.ClientSize = New System.Drawing.Size(657, 270)
            Me.Controls.Add(Me.FlowLayoutPanel1)
            Me.Controls.Add(Me.rtxtMain)
            Me.Name = "RichMessageDialog"
            Me.ShowIcon = False
            Me.ShowInTaskbar = False
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.FlowLayoutPanel1.ResumeLayout(False)
            Me.ResumeLayout(False)

        End Sub

        Friend WithEvents rtxtMain As Inputting.TransparentRichTextBox
        Friend WithEvents FlowLayoutPanel1 As FlowLayoutPanel
        Friend WithEvents btnCancel As Button
        Friend WithEvents btnOK As Button
    End Class

End Namespace