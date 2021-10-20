Namespace Validation
    Public Structure ValidationResult
        Public Success As Boolean
        Public Message As String

        Public Sub New(success As Boolean, message As String)
            Me.Success = success
            Me.Message = message
        End Sub
    End Structure
End Namespace