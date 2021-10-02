Namespace State
    Public Interface IControlEventSession

        ReadOnly Property Handle As IntPtr
        ReadOnly Property IsRunning As Boolean
        ReadOnly Property StartTime As DateTime
    End Interface
End Namespace