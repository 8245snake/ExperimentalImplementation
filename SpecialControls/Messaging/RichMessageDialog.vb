Option Explicit On
Option Strict On

Imports System.IO
Imports System.Text.RegularExpressions

Namespace Messaging

    ''' <summary>
    ''' リッチテキスト形式対応ダイアログフォーム
    ''' </summary>
    Public Class RichMessageDialog

        Public Property MessageText As String
        Public Property Template As TemplatePattern

        Private Sub RichTextDialog_Load(sender As Object, e As EventArgs) Handles MyBase.Load
            Me.Width = Template.FormWidth
            Me.Height = Template.FormHeight

            If File.Exists(Template.TemplatePath) Then
                Try
                    LoadRichText()
                Catch ex As Exception
                    rtxtMain.Text = MessageText
                End Try
            Else
                rtxtMain.Text = MessageText
            End If

            Me.ActiveControl = btnCancel

        End Sub

        ''' <summary>
        ''' リッチテキスト形式を画面に表示する
        ''' </summary>
        Private Sub LoadRichText()
            ' ファイル読み込み
            rtxtMain.LoadFile(Template.TemplatePath)

            Dim result = Template.Pattern.Match(MessageText)
            If result.Success Then
                Dim matches As GroupCollection = result.Groups
                For nI As Integer = 0 To matches.Count - 1
                    ' 正規表現置換の仕様に倣って${数値}をマッチしたグループで置換する
                    Dim newChar As String = matches(nI).Value
                    Dim oldChar As String = "$" & nI
                    Dim foundPosition As Integer = rtxtMain.Find(oldChar)
                    ' 検出したものすべて置換する
                    While foundPosition > -1
                        rtxtMain.Select(foundPosition, oldChar.Length)
                        rtxtMain.SelectedText = newChar
                        foundPosition = rtxtMain.Find(oldChar, foundPosition + oldChar.Length, RichTextBoxFinds.None)
                    End While

                Next
            End If
        End Sub

        Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
            Me.DialogResult = DialogResult.Cancel
            Me.Close()
        End Sub

        Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
            Me.DialogResult = DialogResult.OK
            Me.Close()
        End Sub
    End Class

End Namespace
