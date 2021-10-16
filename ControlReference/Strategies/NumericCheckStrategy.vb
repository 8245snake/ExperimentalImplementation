﻿Option Explicit On
Option Strict On

Imports ControlAttachment.Strategies

Namespace Strategies

    ''' <summary>
    ''' 数値チェック用のストラテジー
    ''' </summary>
    Public Class NumericCheckStrategy
        Implements IValidationStrategy

        Public Property ValidationTrigger As IValidationStrategy.ValidationTriggerType Implements IValidationStrategy.ValidationTrigger
        Public Property Composit As IValidationStrategy Implements IValidationStrategy.Composit

        Public Sub New()
            ValidationTrigger = IValidationStrategy.ValidationTriggerType.TextChanged
        End Sub

        Public Sub New(composit As IValidationStrategy)
            Me.Composit = composit
        End Sub

        Public Function Validate(control As Control) As Boolean Implements IValidationStrategy.Validate

            If String.IsNullOrWhiteSpace(control.Text) Then
                Return True
            End If

            ' 整数かチェック
            Return Integer.TryParse(control.Text, 1)
        End Function

    End Class
End Namespace