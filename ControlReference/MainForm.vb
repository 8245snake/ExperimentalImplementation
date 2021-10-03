Option Explicit On
Option Strict On

Imports ControlAttachment
Imports ControlAttachment.State
Imports ControlAttachment.Strategies

Public Class MainForm

    Private _sessionManager As SessionManager = New SessionManager()

    Private _highlightingManager As HighlightingManager

    Public Sub New()
        InitializeComponent()
        TextBox1.AttachValidation(New NumericCheckStrategy(), New BorderDrawActionStrategy(True))
        TextBox1.AttachWaterMark("数値を書いてください")


        ComboBox1.AttachValidation(New NumericCheckStrategy(), New BorderDrawActionStrategy())

        '_SessionManeger.Register(Button1)
        CheckBox1.Enlarge()

        _highlightingManager = New HighlightingManager(New HighlightingAttachment())

    End Sub

    Private Sub MainForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Using frm = New SubForm
            frm.ShowDialog()
        End Using
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        _highlightingManager.HighlightingControl = TextBox1
    End Sub

End Class




