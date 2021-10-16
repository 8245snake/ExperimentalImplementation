Option Explicit On
Option Strict On

Imports System.Runtime.CompilerServices
Imports System.Windows.Forms

Namespace DataHolder

    ''' <summary>
    ''' 任意のクラスをコンボボックスに紐付けるための補助クラス
    ''' </summary>
    ''' <typeparam name="T">紐付けるクラス</typeparam>
    Public Class ComboItemModelHolder(Of T As {Class})

        Private _ComboBox As ComboBox
        Private _Table As ConditionalWeakTable(Of Object, T)

        ''' <summary>
        ''' 選択中のデータを取得または設定する
        ''' </summary>
        Public Property SelectedData As T
            Get
                Return GetRowDataOrNull(_ComboBox.SelectedItem)
            End Get
            Set
                For Each item As Object In _ComboBox.Items
                    Dim model As T
                    If _Table.TryGetValue(item, model) AndAlso model Is Value Then
                        _ComboBox.SelectedItem = model
                        Return
                    End If
                Next
            End Set
        End Property

        ''' <summary>
        ''' 指定したインデックスのデータを取得または設定する
        ''' </summary>
        ''' <param name="index">インデックス</param>
        Public Property Data(index As Integer) As T
            Get
                Return GetRowDataOrNull(_ComboBox.Items(index))
            End Get
            Set
                SetRowData(_ComboBox.Items(index), Value)
            End Set
        End Property

        ''' <summary>
        ''' 指定したItemのデータを取得または設定する
        ''' </summary>
        ''' <param name="row">行オブジェクト</param>
        Public Property Data(row As Object) As T
            Get
                Return GetRowDataOrNull(row)
            End Get
            Set
                SetRowData(row, Value)
            End Set
        End Property


        ''' <summary>
        ''' 保持しているすべての要素を列挙する。Nullは除く。
        ''' </summary>
        Public ReadOnly Iterator Property Models As IEnumerable(Of T)
            Get
                For Each item As Object In _ComboBox.Items
                    Dim model = GetRowDataOrNull(item)
                    If model IsNot Nothing Then Yield model
                Next
            End Get
        End Property

        ''' <summary>
        ''' 紐付けるグリッドを指定して初期化する
        ''' </summary>
        ''' <param name="comboBox">グリッド</param>
        Public Sub New(comboBox As ComboBox)
            _Table = New ConditionalWeakTable(Of Object, T)()
            _ComboBox = comboBox
            AddHandler _ComboBox.HandleDestroyed, AddressOf HandleDestroyed
        End Sub

        ''' <summary>
        ''' 紐付けるグリッドのHandleDestroyedイベントのハンドラ。
        ''' </summary>
        Private Sub HandleDestroyed(sender As Object, e As EventArgs)
            ' 後始末
            RemoveHandler _ComboBox.HandleDestroyed, AddressOf HandleDestroyed
            _ComboBox = Nothing
        End Sub

        ''' <summary>
        ''' コンボボックスItemにデータをセットする
        ''' </summary>
        ''' <param name="item">コンボボックスItem</param>
        ''' <param name="model">データ</param>
        Private Sub SetRowData(item As Object, model As T)
            If item Is Nothing Then Return
            _Table.Remove(item)
            If model Is Nothing Then Return
            _Table.Add(item, model)
        End Sub

        ''' <summary>
        ''' コンボボックスItemのデータを取得する
        ''' </summary>
        ''' <param name="item">コンボボックスItem</param>
        ''' <returns>データ。なかった場合はNull</returns>
        Private Function GetRowDataOrNull(item As Object) As T
            If item Is Nothing Then Return Nothing
            Dim model As T
            _Table.TryGetValue(item, model)
            Return model
        End Function

    End Class
End Namespace