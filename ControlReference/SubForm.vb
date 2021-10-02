Imports ControlAttachment

Public Class SubForm
    Public Sub New()

        ' この呼び出しはデザイナーで必要です。
        InitializeComponent()

        ' InitializeComponent() 呼び出しの後で初期化を追加します。


        TextBox1.AttachWaterMark("なにか入力してください")




    End Sub


End Class