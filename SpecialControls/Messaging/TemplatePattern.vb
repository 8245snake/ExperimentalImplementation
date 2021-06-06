
Option Explicit On
Option Strict On

Imports System.Text.RegularExpressions
Imports System.IO

Namespace Messaging

    Public Class TemplatePattern
        Public Pattern As Regex
        Public TemplateFileName As String
        Public FormWidth As Integer
        Public FormHeight As Integer

        Public ReadOnly Property TemplatePath As String
            Get
                Return Path.Combine(ExMessageBox.FileDirectory, TemplateFileName)
            End Get
        End Property




        Sub New()
        End Sub

        Sub New(regex As String, file As String, width As Integer, height As Integer)
            Pattern = New Regex(regex, RegexOptions.Compiled)
            TemplateFileName = file
            FormWidth = width
            FormHeight = height
        End Sub

    End Class

End Namespace

