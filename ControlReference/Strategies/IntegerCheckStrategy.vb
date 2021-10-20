Option Explicit On
Option Strict On

Imports ControlAttachment.Validation

Namespace Strategies

    ''' <summary>
    ''' 整数チェック用のストラテジー
    ''' </summary>
    Public Class IntegerCheckStrategy
        Implements IValidationStrategy

        Public Property ValidationTrigger As IValidationStrategy.ValidationTriggerType Implements IValidationStrategy.ValidationTrigger
        Public Property Component As IValidationStrategy Implements IValidationStrategy.Component

        Public Sub New()
            ValidationTrigger = IValidationStrategy.ValidationTriggerType.TextChanged
        End Sub

        Public Sub New(composit As IValidationStrategy)
            MyClass.New()
            Me.Component = composit
        End Sub

        Public Function Validate(control As Control) As ValidationResult Implements IValidationStrategy.Validate

            If String.IsNullOrWhiteSpace(control.Text) Then
                Return New ValidationResult(True, "")
            End If

            ' 整数かチェック
            Dim success = Integer.TryParse(control.Text, 1)
            Dim msg = If(success, "成功", "整数ではありません")
            Return New ValidationResult(success, msg)
        End Function

    End Class
End Namespace