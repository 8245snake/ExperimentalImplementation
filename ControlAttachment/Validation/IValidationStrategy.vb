Imports System.Windows.Forms

Namespace Validation

    Public Interface IValidationStrategy
        Enum ValidationTriggerType
            Validating
            Validated
            TextChanged
        End Enum

        Property ValidationTrigger As ValidationTriggerType
        Property Component As IValidationStrategy
        Function Validate(control As Control) As Boolean

    End Interface
End Namespace