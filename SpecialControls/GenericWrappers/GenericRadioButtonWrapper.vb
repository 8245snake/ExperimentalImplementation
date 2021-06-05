Option Explicit On
Option Strict On

Namespace GenericWrappers


    Public Class GenericRadioButtonWrapper(Of T)
        Private _RadioButtons As List(Of RadioButton) = New List(Of RadioButton)()
        Private _Values As List(Of T) = New List(Of T)()

        Public Property SelectedValue As T
            Get
                For index = 0 To _RadioButtons.Count - 1
                    If _RadioButtons(index).Checked Then
                        Return _Values(index)
                    End If
                Next
                Return Nothing
            End Get
            Set(value As T)
                For index = 0 To _Values.Count - 1
                    If _Values(index).Equals(index) Then
                        _RadioButtons(index).Checked = True
                    End If
                Next
            End Set
        End Property

        Public Sub AddBinding(ByRef radioButton As RadioButton, ByRef value As T)
            _RadioButtons.Add(radioButton)
            _Values.Add(value)
        End Sub

        Public Sub RemoveBinding(ByRef radioButton As RadioButton)
            Dim index = _RadioButtons.IndexOf(radioButton)
            _RadioButtons.RemoveAt(index)
            _Values.RemoveAt(index)
        End Sub

        Public Function GetValue(ByRef radioButton As RadioButton) As T
            Dim index = _RadioButtons.IndexOf(radioButton)
            Return _Values(index)
        End Function

    End Class


End Namespace