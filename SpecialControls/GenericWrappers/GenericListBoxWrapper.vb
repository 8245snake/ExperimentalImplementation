Option Explicit On
Option Strict On

Namespace GenericWrappers

    ''' <summary>
    ''' 任意の型をリストボックスと紐付けるためのラッパークラス
    ''' </summary>
    ''' <typeparam name="T">コンボボックスと紐付ける型</typeparam>
    Public Class GenericListBoxWrapper(Of T)
        Private _ListBox As ListBox
        Private _DataTable As DataTable

        Private Const DispColName = "disp"
        Private Const ValueColName = "value"

        Public Property ListBox As ListBox
            Get
                Return _ListBox
            End Get
            Set(value As ListBox)
                _ListBox = value
            End Set
        End Property

        Public Property SelectedValue As T
            Get
                Return DirectCast(_ListBox.SelectedValue, T)
            End Get
            Set(value As T)
                _ListBox.SelectedValue = value
            End Set
        End Property

        Public Property SelectedIndex As Integer
            Get
                Return _ListBox.SelectedIndex
            End Get
            Set(value As Integer)
                _ListBox.SelectedIndex = value
            End Set
        End Property
        Sub New(ByRef listbox As ListBox)
            _ListBox = listbox
            ' バインド用のデータテーブルを作成する
            _DataTable = New DataTable
            _DataTable.Columns.Add(DispColName, GetType(String))
            _DataTable.Columns.Add(ValueColName, GetType(T))
            ' バインドする
            _ListBox.DataSource = _DataTable
            _ListBox.DisplayMember = DispColName
            _ListBox.ValueMember = ValueColName
        End Sub

        Protected Overrides Sub Finalize()
            _ListBox = Nothing
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

    End Class

End Namespace
