Option Explicit On
Option Strict On

''' <summary>
''' 任意の型をコンボボックスと紐付けるためのラッパークラス
''' </summary>
''' <typeparam name="T">コンボボックスと紐付ける型</typeparam>
Public Class GenericComboBoxWrapper(Of T)

    Private _ComboBox As ComboBox
    Private _DataTable As DataTable

    Private Const DispColName = "disp"
    Private Const ValueColName = "value"

    Sub New(ByRef combobox As ComboBox)
        _ComboBox = combobox
        ' バインド用のデータテーブルを作成する
        _DataTable = New DataTable
        _DataTable.Columns.Add(DispColName, GetType(String))
        _DataTable.Columns.Add(ValueColName, GetType(T))
        ' バインドする
        _ComboBox.DataSource = _DataTable
        _ComboBox.DisplayMember = DispColName
        _ComboBox.ValueMember = ValueColName
    End Sub

    Protected Overrides Sub Finalize()
        _ComboBox = Nothing
        _DataTable = Nothing
        MyBase.Finalize()
    End Sub

    ''' <summary>
    ''' 要素を追加する
    ''' </summary>
    ''' <param name="dispText">表示されるテキスト</param>
    ''' <param name="value">内部の値</param>
    Public Sub Add(ByVal dispText As String, ByRef value As T)
        _DataTable.Rows.Add(dispText, value)
    End Sub

    ''' <summary>
    ''' 選択されている値を取得する
    ''' </summary>
    ''' <returns>選択項目に紐付くオブジェクト</returns>
    Public Function GetSelectedValue() As T
        Return DirectCast(_ComboBox.SelectedValue, T)
    End Function

End Class
