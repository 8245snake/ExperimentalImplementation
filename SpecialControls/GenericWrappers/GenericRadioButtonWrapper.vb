Option Explicit On
Option Strict On

Namespace GenericWrappers

    ''' <summary>
    ''' 任意の型をラジオボタンと紐付けるためのラッパークラス
    ''' </summary>
    ''' <typeparam name="T">ラジオボタンと紐付ける型</typeparam>
    Public Class GenericRadioButtonWrapper(Of T)
        Private _RadioButtons As List(Of RadioButton) = New List(Of RadioButton)()
        Private _Values As List(Of T) = New List(Of T)()

        ''' <summary>
        ''' チェックされたラジオボタンに紐付くデータを取得もしくは設定する
        ''' </summary>
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

        ''' <summary>
        ''' ラジオボタンとデータの紐付けを追加する
        ''' </summary>
        ''' <param name="radioButton">ラジオボタン</param>
        ''' <param name="value">データ</param>
        Public Sub AddBinding(ByRef radioButton As RadioButton, ByRef value As T)
            _RadioButtons.Add(radioButton)
            _Values.Add(value)
        End Sub

        ''' <summary>
        ''' ラジオボタンとデータの紐付けを削除する
        ''' </summary>
        ''' <param name="radioButton">ラジオボタン</param>
        Public Sub RemoveBinding(ByRef radioButton As RadioButton)
            Dim index = _RadioButtons.IndexOf(radioButton)
            _RadioButtons.RemoveAt(index)
            _Values.RemoveAt(index)
        End Sub

        ''' <summary>
        ''' ラジオボタンに紐付くデータを取得する
        ''' </summary>
        ''' <param name="radioButton">ラジオボタン</param>
        ''' <returns>データ</returns>
        Public Function GetValue(ByRef radioButton As RadioButton) As T
            Dim index = _RadioButtons.IndexOf(radioButton)
            Return _Values(index)
        End Function

        Protected Overrides Sub Finalize()
            _RadioButtons = Nothing
            _Values = Nothing
            MyBase.Finalize()
        End Sub

    End Class


End Namespace