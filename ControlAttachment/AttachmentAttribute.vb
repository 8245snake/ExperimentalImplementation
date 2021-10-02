Option Explicit On
Option Strict On


<AttributeUsage(AttributeTargets.Class, AllowMultiple:=False)>
Friend Class AttachmentAttribute
    Inherits Attribute

    Public Property AllowMultiple() As Boolean = True


End Class