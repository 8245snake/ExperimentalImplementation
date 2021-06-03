Option Explicit On
Option Strict On

Namespace CollectionViews

    ''' <summary>
    ''' ユーザーが手で選択を変えたかプログラムによって自動でSelectedIndexを変化されたかを判定できるコンボボックス
    ''' </summary>
    Public Class StrictComboBox
        Inherits ComboBox

        ''' <summary>
        ''' インデックスが本当に変化したときにSelectedIndexChangedが発火するかを設定または取得します
        ''' </summary>
        ''' <returns></returns>
        Public Property IsStrictOccuration As Boolean = False

        ''' <summary>
        ''' 前回選択したインデックス
        ''' </summary>
        Private _LastIndex As Integer
        ''' <summary>
        ''' SelectionChangeCommittedイベントが発火したかのフラグ
        ''' </summary>
        Private _IsCommited As Boolean

        ''' <summary>
        ''' 手動でインデックスを変更したら発火する
        ''' </summary>
        Public Event SelectedIndexChangedManually As SelectedIndexStrictChanged
        ''' <summary>
        ''' 自動でインデックスを変更したら発火する
        ''' </summary>
        Public Event SelectedIndexChangedAutomatically As SelectedIndexStrictChanged

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <param name="lastIndex"></param>
        ''' <param name="currentIndex"></param>
        Delegate Sub SelectedIndexStrictChanged(sender As StrictComboBox, e As EventArgs, lastIndex As Integer, currentIndex As Integer)

        Protected Overrides Sub OnSelectedIndexChanged(e As EventArgs)

            ' インデックスが変わっていなければ発火しないモード
            If IsStrictOccuration AndAlso _LastIndex = Me.SelectedIndex Then
                Return
            End If

            MyBase.OnSelectedIndexChanged(e)

            If _IsCommited Then
                ' 手入力のときは必ずSelectionChangeCommittedが先に発火するため
                RaiseEvent SelectedIndexChangedManually(Me, New EventArgs(), _LastIndex, Me.SelectedIndex)
            Else
                RaiseEvent SelectedIndexChangedAutomatically(Me, New EventArgs(), _LastIndex, Me.SelectedIndex)
            End If

            _LastIndex = Me.SelectedIndex
            _IsCommited = False
        End Sub

        Protected Overrides Sub OnSelectionChangeCommitted(e As EventArgs)
            MyBase.OnSelectionChangeCommitted(e)
            _IsCommited = True
        End Sub


    End Class

End Namespace
