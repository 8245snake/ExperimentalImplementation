Option Explicit On
Option Strict On

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
        AttachInternal(textbox, New WaterMarkAttachment(textbox, watermark))
    End Sub

    <Extension()>
    Public Sub AttachValidation(target As Control, validationStrategy As IValidationStrategy, errorActionStrategy As IErrorActionStrategy)
        AttachInternal(target, New ValidationAttachment(target, validationStrategy, errorActionStrategy))
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
        AttachInternal(target, New CheckBoxEnlargeAttachment(target))
    End Sub

    Private Function AttachInternal(Of T As NativeWindow)(control As Control, obj As T) As List(Of NativeWindow)
        Dim list As List(Of NativeWindow) = Nothing
        If Not _ConditionalWeakTable.TryGetValue(control, list) Then
            ' なければ新規エントリを追加して終わり
            list = New List(Of NativeWindow)({obj})
            _ConditionalWeakTable.Add(control, list)
            Return list
        End If

        '' あった場合は重複していいか判断して追加か置換する

        Dim item = list.OfType(Of T)?.FirstOrDefault()
        If item Is Nothing Then
            ' 含まれてないので追加して終了
            list.Add(obj)
            Return list
        End If

        Dim attribute As AttachmentAttribute = GetType(T).GetCustomAttributes(False)?.OfType(Of AttachmentAttribute)?.FirstOrDefault()
        If attribute Is Nothing Then
            ' 属性なしは重複を許すことにして終了
            list.Add(obj)
            Return list
        End If

        If attribute.AllowMultiple Then
            ' 重複を許す指定なので追加して終了
            list.Add(obj)
            Return list
        End If

        ' 重複を許さない場合は置き換える
        item.ReleaseHandle()
        list.Remove(item)
        list.Add(obj)

        Return list

    End Function
End Module