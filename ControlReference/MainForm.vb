Option Explicit On
Option Strict On

Imports ControlAttachment
Imports ControlAttachment.DataHolder
Imports ControlAttachment.State
Imports ControlAttachment.Strategies
Imports ControlAttachment.Text

Public Class MainForm

    Private _LimitSizes As ComboItemModelHolder(Of LimitSetterCommand)
    Private _highlightingManager As HighlightingManager = New HighlightingManager(New BorderDrawActionStrategy())

    Public Sub New()
        InitializeComponent()
        ' ウォーターマーク
        txtWaterMark.AttachWaterMark($"ウォーターマーク（英語: watermark）は、本来、紙の透かし（すかし）のことを指すが、著作権表示などのために静止画像や動画に写し込まれる小さな図案や文字を指すことが比較的多い。")
        ' バイト数制限
        SetupLimitedTextBox()
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



