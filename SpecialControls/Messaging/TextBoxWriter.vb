Option Explicit On
Option Strict On

Imports System.IO

''' <summary>
''' テキストボックスに出力するTextWriter
''' </summary>
Public Class TextBoxWriter
    Inherits TextWriter

    ''' <summary>
    ''' Console.WriteLineしたときに出力されるテキストボックス
    ''' </summary>
    Public TargetTextBox As TextBox

    Public Overrides ReadOnly Property Encoding As System.Text.Encoding
        Get
            Return System.Text.Encoding.GetEncoding("shift_jis")
        End Get
    End Property

    Public Overrides Sub WriteLine(value As String)
        If TargetTextBox Is Nothing Then
            MyBase.WriteLine(value)
        Else
            TargetTextBox.Text &= value & vbCrLf
        End If
    End Sub
End Class