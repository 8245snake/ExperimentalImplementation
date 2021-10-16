Option Explicit On
Option Strict On

Imports System.Runtime.CompilerServices
Imports System.Windows.Forms

Namespace DataHolder

    ''' <summary>
    ''' 任意のクラスをグリッドの行に紐付けるための補助クラス
    ''' </summary>
    ''' <typeparam name="T">紐付けるクラス</typeparam>
    Public Class GridDataModelHolder(Of T As {Class})

        Private _Grid As DataGridView
        Private _Table As ConditionalWeakTable(Of DataGridViewRow, T)

        ''' <summary>
        ''' 選択中のデータを取得または設定する
        ''' </summary>
        Public Property SelectedData As T
            Get
                For Each row As DataGridViewRow In _Grid.SelectedRows
                    Return GetRowDataOrNull(row)
                Next
                Return Nothing
            End Get
            Set
                For Each row As DataGridViewRow In _Grid.SelectedRows
                    Dim model As T
                    If _Table.TryGetValue(row, model) AndAlso model Is Value Then
                        row.Selected = True
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
                Return GetRowDataOrNull(_Grid.Rows(index))
            End Get
            Set
                SetRowData(_Grid.Rows(index), Value)
            End Set
        End Property

        ''' <summary>
        ''' 指定した行のデータを取得または設定する
        ''' </summary>
        ''' <param name="row">行オブジェクト</param>
        Public Property Data(row As DataGridViewRow) As T
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
                For Each row As DataGridViewRow In _Grid.Rows
                    Dim model = GetRowDataOrNull(row)
                    If model IsNot Nothing Then Yield model
                Next
            End Get
        End Property

        ''' <summary>
        ''' 紐付けるグリッドを指定して初期化する
        ''' </summary>
        ''' <param name="grid">グリッド</param>
        Public Sub New(grid As DataGridView)
            _Table = New ConditionalWeakTable(Of DataGridViewRow, T)()
            _Grid = grid
            AddHandler _Grid.HandleDestroyed, AddressOf HandleDestroyed
        End Sub

        ''' <summary>
        ''' 紐付けるグリッドのHandleDestroyedイベントのハンドラ。
        ''' </summary>
        Private Sub HandleDestroyed(sender As Object, e As EventArgs)
            ' 後始末
            RemoveHandler _Grid.HandleDestroyed, AddressOf HandleDestroyed
            _Grid = Nothing
        End Sub

        ''' <summary>
        ''' 行にデータをセットする
        ''' </summary>
        ''' <param name="row">行オブジェクト</param>
        ''' <param name="model">データ</param>
        Private Sub SetRowData(row As DataGridViewRow, model As T)
            _Table.Remove(row)
            _Table.Add(row, model)
        End Sub

        ''' <summary>
        ''' 行のデータを取得する
        ''' </summary>
        ''' <param name="row">行オブジェクト</param>
        ''' <returns>データ。なかった場合はNull</returns>
        Private Function GetRowDataOrNull(row As DataGridViewRow) As T
            Dim model As T
            _Table.TryGetValue(row, model)
            Return model
        End Function

    End Class
End Namespace