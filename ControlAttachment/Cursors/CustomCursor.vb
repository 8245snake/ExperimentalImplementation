Imports System.Reflection
Imports System.Windows.Forms

Namespace Cursors
    Public Class CustomCursor

        Private Shared asm As Assembly = Assembly.GetExecutingAssembly()
        Private Shared openHand As System.Windows.Forms.Cursor
        Private Shared closehand As System.Windows.Forms.Cursor

        Public Shared ReadOnly Property Hand_Open As System.Windows.Forms.Cursor
            Get
                If openHand Is Nothing Then
                    openHand = GetCursor(".Hand_Open.cur")
                End If
                Return openHand
            End Get
        End Property

        Public Shared ReadOnly Property Hand_Close As System.Windows.Forms.Cursor
            Get
                If closehand Is Nothing Then
                    closehand = GetCursor(".Hand_Close.cur")
                End If
                Return closehand
            End Get
        End Property

        Private Shared Function GetCursor(cursorName As String) As System.Windows.Forms.Cursor
            Dim name = asm.GetName().Name & cursorName
            Return New System.Windows.Forms.Cursor(asm.GetManifestResourceStream(name))
        End Function

    End Class
End Namespace