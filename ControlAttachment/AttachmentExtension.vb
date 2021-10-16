Option Explicit On
Option Strict On

Imports System.Drawing
Imports System.Runtime.CompilerServices
Imports System.Windows.Forms
Imports ControlAttachment.Decoration
Imports ControlAttachment.Text
Imports ControlAttachment.Validation

Public Module AttachmentExtension

    ''' <summary>
    ''' コントロールとそれに紐付くアタッチメントを保持する
    ''' </summary>
    Private _ConditionalWeakTable As ConditionalWeakTable(Of Control, List(Of NativeWindow)) = New ConditionalWeakTable(Of Control, List(Of NativeWindow))()


    <Extension()>
    Public Sub AttachWaterMark(textbox As TextBox, watermark As String)
        AttachmentManager.AttachInternal(textbox, New WaterMarkAttachment(textbox, watermark), _ConditionalWeakTable)
    End Sub

    <Extension()>
    Public Sub AttachMaxByteSize(textbox As TextBox, maxByteSize As Integer, Optional errorActionStrategy As IErrorActionStrategy = Nothing)
        AttachmentManager.AttachInternal(textbox, New TextSizeLimitAttachment(textbox, maxByteSize, errorActionStrategy), _ConditionalWeakTable)
    End Sub

    <Extension()>
    Public Sub SetHoverColor(target As Control, color As Color)
        AttachmentManager.AttachInternal(target, New HoverActionAttachment(target, color), _ConditionalWeakTable)
    End Sub

    <Extension()>
    Public Sub AttachValidation(target As Control, validationStrategy As IValidationStrategy, errorActionStrategy As IErrorActionStrategy)
        AttachmentManager.AttachInternal(target, New ValidationAttachment(target, validationStrategy, errorActionStrategy), _ConditionalWeakTable)
    End Sub

    <Extension()>
    Friend Iterator Function GetValidationAttachments(target As Control) As IEnumerable(Of ValidationAttachment)
        Dim list As List(Of NativeWindow) = Nothing
        If Not _ConditionalWeakTable.TryGetValue(target, list) Then
            Return
        End If

        For Each attachment As ValidationAttachment In list.OfType(Of ValidationAttachment)
            Yield attachment
        Next
    End Function

    <Extension()>
    Public Sub ForceValidate(target As Control)
        For Each attachment As ValidationAttachment In target.GetValidationAttachments()
            attachment.ForceValidate()
        Next
    End Sub

    <Extension()>
    Public Sub Enlarge(target As CheckBox)
        AttachmentManager.AttachInternal(target, New CheckBoxEnlargeAttachment(target), _ConditionalWeakTable)
    End Sub

End Module