Imports System.Windows.Forms

Namespace Strategies

    Public Interface IValidationStrategy
        Enum ValidationTriggerType
            Validating
            Validated
            TextChanged
        End Enum

        Property ValidationTrigger As ValidationTriggerType
        Property Composit As IValidationStrategy
        Function Validate(control As Control) As Boolean

    End Interface
End Namespace