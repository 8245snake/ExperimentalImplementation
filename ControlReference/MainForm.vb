Option Explicit On
Option Strict On

Imports ControlAttachment
Imports ControlAttachment.State
Imports ControlAttachment.Validation.Embedded

Public Class MainForm

    Private _SessionManeger As SessionManeger = New SessionManeger()

    Public Sub New()
        InitializeComponent()
        TextBox1.AttachValidation(New NumericCheckStrategy(), New BorderDrawActionStrategy(True))
        TextBox1.AttachWaterMark("数値を書いてください")

        ComboBox1.AttachValidation(New NumericCheckStrategy(), New BorderDrawActionStrategy())

        '_SessionManeger.Register(Button1)
        CheckBox1.Enlarge()

    End Sub

    Private Sub MainForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub


End Class




