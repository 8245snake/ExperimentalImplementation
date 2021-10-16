﻿Option Explicit On
Option Strict On

Imports System.ComponentModel
Imports System.Windows.Forms

Namespace Validation

    <Attachment(AllowMultiple:=True)>
    Friend Class ValidationAttachment
        Inherits NativeWindow

        Private _TargetControl As Control
        Private _ValidationStrategy As IValidationStrategy
        Private _ErrorActionStrategy As IErrorActionStrategy
        Private _IsError As Boolean = False

        Public ReadOnly Property IsError As Boolean
            Get
                Return _IsError
            End Get
        End Property

        Private Const WM_PAINT = &HF

        Public Sub New(targetControl As Control, validationStrategy As IValidationStrategy, errorActionStrategy As IErrorActionStrategy)
            _TargetControl = targetControl
            _ValidationStrategy = validationStrategy
            _ErrorActionStrategy = errorActionStrategy

            If _TargetControl.IsHandleCreated Then
                AssignHandle(_TargetControl.Handle)
            Else
                AddHandler _TargetControl.HandleCreated, AddressOf OnHandleCreated
            End If

            AddHandler _TargetControl.HandleDestroyed, AddressOf OnHandleDestroyed
            AddHandler _TargetControl.Validating, AddressOf OnValidating
            AddHandler _TargetControl.Validated, AddressOf OnValidated
            AddHandler _TargetControl.TextChanged, AddressOf OnTextChanged
        End Sub

        Private Sub OnHandleCreated(sender As Object, e As EventArgs)
            AssignHandle(_TargetControl.Handle)
            RemoveHandler _TargetControl.HandleCreated, AddressOf OnHandleCreated
        End Sub

        Private Sub OnHandleDestroyed(sender As Object, e As EventArgs)
            ReleaseHandle()
        End Sub

        Public Overrides Sub ReleaseHandle()
            RemoveHandler _TargetControl.HandleCreated, AddressOf OnHandleCreated
            RemoveHandler _TargetControl.HandleDestroyed, AddressOf OnHandleDestroyed
            RemoveHandler _TargetControl.Validating, AddressOf OnValidating
            RemoveHandler _TargetControl.Validated, AddressOf OnValidated
            RemoveHandler _TargetControl.TextChanged, AddressOf OnTextChanged

            _TargetControl = Nothing

            MyBase.ReleaseHandle()
        End Sub

        ''' <summary>
        ''' 強制的にバリデーションを実行します
        ''' </summary>
        Public Sub ForceValidate()
            If _ValidationStrategy Is Nothing Then Return
            ValidationProc(_ValidationStrategy.ValidationTrigger)
        End Sub

        Protected Overrides Sub WndProc(ByRef m As Message)
            MyBase.WndProc(m)

            If m.Msg = WM_PAINT AndAlso _IsError Then
                _ErrorActionStrategy?.ErrorPainting(_TargetControl)
            End If

        End Sub

        Private Sub OnValidated(sender As Object, e As EventArgs)
            ValidationProc(IValidationStrategy.ValidationTriggerType.Validated)
        End Sub

        Private Sub OnValidating(sender As Object, e As CancelEventArgs)
            ValidationProc(IValidationStrategy.ValidationTriggerType.Validating)
        End Sub

        Private Sub OnTextChanged(sender As Object, e As EventArgs)
            ValidationProc(IValidationStrategy.ValidationTriggerType.TextChanged)
        End Sub

        Private Sub ValidationProc(trigger As IValidationStrategy.ValidationTriggerType)
            ' 対象イベントじゃなければ処理しない
            If _ValidationStrategy?.ValidationTrigger <> trigger Then Return

            ' ValidationAttachmentが複数紐付いている場合自身より上位でエラーになっていたら処理しない
            For Each attachment As ValidationAttachment In _TargetControl.GetValidationAttachments()
                ' 自身が最上位なので判定に進む
                If attachment Is Me Then Exit For
                ' 自身より上位でエラーになっていたら処理せず終了
                If attachment.IsError Then Return
            Next

            ' エラー判定
            _IsError = ValidateAll()

            If _IsError Then
                ' エラー時のアクション
                ErrorAll()
            Else
                ' 成功時のアクション
                SucceedAll()
            End If

            _TargetControl.Invalidate()
        End Sub

        Private Function ValidateAll() As Boolean
            Dim isEroor = False
            Dim strategy = _ValidationStrategy

            While strategy IsNot Nothing
                isEroor = Not strategy.Validate(_TargetControl)
                strategy = strategy.Composit
                If isEroor Then Exit While
            End While

            Return isEroor

        End Function

        Private Sub SucceedAll()

            Dim strategy = _ErrorActionStrategy

            While strategy IsNot Nothing
                strategy.SuccessAction(_TargetControl)
                strategy = strategy.Composit
            End While

        End Sub

        Private Sub ErrorAll()

            Dim strategy = _ErrorActionStrategy

            While strategy IsNot Nothing
                strategy.ErrorAction(_TargetControl)
                strategy = strategy.Composit
            End While

        End Sub


    End Class

End Namespace