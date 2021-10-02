Option Explicit On
Option Strict On

Imports System.Windows.Forms

Namespace State
    Public Class SessionManager
        Private Sessions As List(Of IControlEventSession)

        Private prevTime As DateTime
        Private prevHandle As IntPtr

        Sub New()
            Sessions = New List(Of IControlEventSession)()
        End Sub

        Public Sub Register(control As Control)
            Sessions.Add(New EventSessionListener(control))
        End Sub

        Private Function GetCurrentSession() As IControlEventSession
            Return Sessions.Where(Function(session) session.IsRunning).OrderBy(Function(session) session.StartTime).LastOrDefault()
        End Function

        Public Function IsFirstCalledInSession(Optional onlyCheck As Boolean = False) As Boolean
            Dim current = GetCurrentSession()
            If current Is Nothing Then Return True

            If current.Handle = prevHandle AndAlso current.StartTime = prevTime Then
                Return False
            End If

            If Not onlyCheck Then
                prevHandle = current.Handle
                prevTime = current.StartTime
            End If

            Return True

        End Function


    End Class

End Namespace