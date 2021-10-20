Option Explicit On
Option Strict On

Imports ControlAttachment.Validation

Namespace Strategies

    ''' <summary>
    ''' ３の倍数かをチェックするストラテジ
    ''' </summary>
    Public Class ThreeMultipleValidationStrategy
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
            Dim result As ValidationResult

            If String.IsNullOrWhiteSpace(control.Text) Then
                result.Success = True
                result.Message = ""
                Return result
            End If

            Dim val As Integer
            If Not Integer.TryParse(control.Text, val) Then
                result.Success = False
                result.Message = "整数ではありません"
                Return result
            End If

            If val Mod 3 = 0 Then
                result.Success = True
                result.Message = ""
                Return result
            End If

            result.Success = False
            result.Message = "3の倍数ではありません"
            Return result

        End Function
    End Class
End Namespace