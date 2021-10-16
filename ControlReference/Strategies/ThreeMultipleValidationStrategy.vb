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
        Public Property Composit As IValidationStrategy Implements IValidationStrategy.Composit

        Public Sub New()
            ValidationTrigger = IValidationStrategy.ValidationTriggerType.TextChanged
        End Sub

        Public Sub New(composit As IValidationStrategy)
            MyClass.New()
            Me.Composit = composit
        End Sub


        Public Function Validate(control As Control) As Boolean Implements IValidationStrategy.Validate

            If String.IsNullOrWhiteSpace(control.Text) Then
                Return True
            End If

            Dim val As Integer
            If Not Integer.TryParse(control.Text, val) Then
                Return False
            End If

            If val Mod 3 = 0 Then
                Return True
            End If

            Return False

        End Function
    End Class
End Namespace