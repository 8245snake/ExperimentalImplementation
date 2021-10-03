﻿Imports System.Windows.Forms

Namespace Strategies

    Public Interface IValidationStrategy
        Enum ValidationTriggerType
            Unknown
            Validating
            Validated
            TextChanged
        End Enum

        Property ValidationTrigger As ValidationTriggerType
        Function Validate(control As Control) As Boolean

    End Interface
End Namespace