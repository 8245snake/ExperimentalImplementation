Option Explicit On
Option Strict On

Imports System.Drawing
Imports System.Runtime.CompilerServices
Imports System.Windows.Forms
Imports ControlAttachment.Decoration
Imports ControlAttachment.Text
Imports ControlAttachment.Validation

Public Class AttachmentManager

    ''' <summary>
    ''' コントロールとそれに紐付くアタッチメントを保持する
    ''' </summary>
    Private _ConditionalWeakTable As ConditionalWeakTable(Of Control, List(Of NativeWindow))

    Public Sub New()
        _ConditionalWeakTable = New ConditionalWeakTable(Of Control, List(Of NativeWindow))()
    End Sub

    Public Sub AttachWaterMark(textbox As TextBox, watermark As String)
        AttachInternal(textbox, New WaterMarkAttachment(textbox, watermark), _ConditionalWeakTable)
    End Sub

    Public Sub AttachMaxByteSize(textbox As TextBox, maxByteSize As Integer, Optional errorActionStrategy As IErrorActionStrategy = Nothing)
        AttachInternal(textbox, New TextSizeLimitAttachment(textbox, maxByteSize, errorActionStrategy), _ConditionalWeakTable)
    End Sub

    Public Sub AttachHoverColor(target As Control, color As Color)
        AttachInternal(target, New HoverActionAttachment(target, color), _ConditionalWeakTable)
    End Sub

    Public Sub AttachValidation(target As Control, validationStrategy As IValidationStrategy, errorActionStrategy As IErrorActionStrategy)
        AttachInternal(target, New ValidationAttachment(target, validationStrategy, errorActionStrategy), _ConditionalWeakTable)
    End Sub

    Friend Iterator Function GetValidationAttachments(target As Control) As IEnumerable(Of ValidationAttachment)
        Dim list As List(Of NativeWindow) = Nothing
        If Not _ConditionalWeakTable.TryGetValue(target, list) Then
            Return
        End If

        For Each attachment As ValidationAttachment In list.OfType(Of ValidationAttachment)
            Yield attachment
        Next
    End Function

    Public Sub ForceValidate(target As Control)
        For Each attachment As ValidationAttachment In target.GetValidationAttachments()
            attachment.ForceValidate()
        Next
    End Sub

    Public Sub Enlarge(target As CheckBox)
        AttachInternal(target, New CheckBoxEnlargeAttachment(target), _ConditionalWeakTable)
    End Sub


    ''' <summary>
    ''' ConditionalWeakTableにNativeWindow派生のアタッチメントを追加する
    ''' </summary>
    ''' <typeparam name="T">任意のアタッチメントの型</typeparam>
    ''' <param name="control">対象のコントロール</param>
    ''' <param name="attachment">アタッチメント</param>
    ''' <param name="table">追加するConditionalWeakTable</param>
    ''' <returns>追加したConditionalWeakTableのValueにあたるListを返す</returns>
    Friend Shared Function AttachInternal(Of T As NativeWindow)(control As Control, attachment As T, table As ConditionalWeakTable(Of Control, List(Of NativeWindow))) As List(Of NativeWindow)

        Dim list As List(Of NativeWindow) = Nothing

        If Not table.TryGetValue(control, list) Then
            ' なければ新規エントリを追加して終わり
            list = New List(Of NativeWindow)()
            If attachment IsNot Nothing Then
                list.Add(attachment)
            End If
            table.Add(control, list)
            Return list
        End If

        If attachment Is Nothing Then
            Return list
        End If

        '' あった場合は重複していいか判断して追加か置換する

        Dim item = list.OfType(Of T)?.FirstOrDefault()
        If item Is Nothing Then
            ' 含まれてないので追加して終了
            list.Add(attachment)
            Return list
        End If

        Dim attribute As AttachmentAttribute = GetType(T).GetCustomAttributes(False)?.OfType(Of AttachmentAttribute)?.FirstOrDefault()
        If attribute Is Nothing Then
            ' 属性なしは重複を許すことにして終了
            list.Add(attachment)
            Return list
        End If

        If attribute.AllowMultiple Then
            ' 重複を許す指定なので追加して終了
            list.Add(attachment)
            Return list
        End If

        ' 重複を許さない場合は置き換える
        item.ReleaseHandle()
        list.Remove(item)
        list.Add(attachment)

        Return list

    End Function

    Friend Shared Iterator Function GetAttachments(Of T As NativeWindow)(target As Control, table As ConditionalWeakTable(Of Control, List(Of NativeWindow))) As IEnumerable(Of T)
        Dim list As List(Of NativeWindow) = Nothing
        If Not table.TryGetValue(target, list) Then
            Return
        End If

        For Each attachment As T In list.OfType(Of T)
            Yield attachment
        Next
    End Function


    Friend Shared Sub ClearAttachments(Of T As NativeWindow)(target As Control, table As ConditionalWeakTable(Of Control, List(Of NativeWindow)))
        Dim list As List(Of NativeWindow) = Nothing
        If Not table.TryGetValue(target, list) Then
            Return
        End If

        Dim deleteList = GetAttachments(Of T)(target, table).ToArray()
        For Each attachment As T In deleteList
            attachment.ReleaseHandle()
            list.Remove(attachment)
        Next
    End Sub

    Friend Shared Function CompositActionStrategy(strategies As IEnumerable(Of IErrorActionStrategy)) As IErrorActionStrategy

        Dim errorActionStrategy As IErrorActionStrategy
        Dim errorActionStrategyList = strategies.ToList()

        If errorActionStrategyList.Count < 1 Then Return Nothing

        Dim tmpstrategy As IErrorActionStrategy
        For Each strategy As IErrorActionStrategy In errorActionStrategyList

            If errorActionStrategy Is Nothing Then
                errorActionStrategy = strategy
                tmpstrategy = strategy
                Continue For
            End If

            If tmpstrategy.Composit IsNot Nothing Then
                tmpstrategy = tmpstrategy.Composit
            End If

            tmpstrategy.Composit = strategy
            tmpstrategy = tmpstrategy.Composit
        Next
        Return errorActionStrategy
    End Function

    Friend Shared Function CompositValidationStrategy(strategies As IEnumerable(Of IValidationStrategy)) As IValidationStrategy

        Dim validationStrategy As IValidationStrategy
        Dim validationStrategiesList = strategies.ToList()

        If validationStrategiesList.Count < 1 Then Return Nothing

        Dim tmpstrategy As IValidationStrategy
        For Each strategy As IValidationStrategy In validationStrategiesList

            If validationStrategy Is Nothing Then
                validationStrategy = strategy
                tmpstrategy = strategy
                Continue For
            End If

            If tmpstrategy.Component IsNot Nothing Then
                tmpstrategy = tmpstrategy.Component
            End If

            tmpstrategy.Component = strategy
            tmpstrategy = tmpstrategy.Component
        Next
        Return validationStrategy
    End Function

End Class