Option Explicit On
Option Strict On

Namespace GenericWrappers

    ''' <summary>
    ''' 任意の型をコンボボックスと紐付けるためのラッパークラス
    ''' </summary>
    ''' <typeparam name="T">コンボボックスと紐付ける型</typeparam>
    Public Class GenericComboBoxWrapper(Of T)

        Private _ComboBox As ComboBox
        Private _DataTable As DataTable

        Private Const DispColName = "disp"
        Private Const ValueColName = "value"

        Public Property ComboBox As ComboBox
            Get
                Return _ComboBox
            End Get
            Set(value As ComboBox)
                _ComboBox = value
            End Set
        End Property

        Public Property SelectedValue As T
            Get
                Return DirectCast(_ComboBox.SelectedValue, T)
            End Get
            Set(value As T)
                _ComboBox.SelectedValue = value
            End Set
        End Property

        Public Property SelectedIndex As Integer
            Get
                Return _ComboBox.SelectedIndex
            End Get
            Set(value As Integer)
                _ComboBox.SelectedIndex = value
            End Set
        End Property


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


    End Class

End Namespace