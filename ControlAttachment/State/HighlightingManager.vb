Option Explicit On
Option Strict On

Imports System.Windows.Forms

Namespace State

    ''' <summary>
    ''' コントロールをハイライトするためのマネージャクラス
    ''' </summary>
    Public Class HighlightingManager
        Inherits NativeWindow

        Private _HighlightingControl As Control
        Private _highlightingStrategy As IHighlightingStrategy

        ''' <summary>
        ''' ハイライトするロジックを取得または設定する
        ''' </summary>
        Public Property HighlightingStrategy As IHighlightingStrategy
            Get
                Return _highlightingStrategy
            End Get
            Set
                If _highlightingStrategy IsNot Nothing Then
                    ' ハイライト停止
                    EndHighlight(_HighlightingControl)
                End If
                _highlightingStrategy = Value
                If _highlightingStrategy IsNot Nothing AndAlso _HighlightingControl IsNot Nothing Then
                    ' ハイライト開始
                    BeginHighlight(_HighlightingControl)
                End If
            End Set
        End Property

        ''' <summary>
        ''' ハイライト中のコントロールを取得または設定する
        ''' </summary>
        Public Property HighlightingControl As Control
            Get
                Return _HighlightingControl
            End Get
            Set
                ' ハイライト停止
                EndHighlight(_HighlightingControl)
                ' ハイライト開始
                BeginHighlight(Value)
            End Set
        End Property

        Private Const WM_PAINT = &HF

        ''' <summary>
        ''' ハイライトロジックを指定してオブジェクトを初期化する
        ''' </summary>
        ''' <param name="highlightingStrategy">ハイライトロジック</param>
        Public Sub New(highlightingStrategy As IHighlightingStrategy)
            _highlightingStrategy = highlightingStrategy
        End Sub

        Private Sub OnHandleCreated(sender As Object, e As EventArgs)
            ' ハンドルができたので、予約されていたハイライト処理を実行する
            AssignToHighlight(_HighlightingControl)
            RemoveHandler _HighlightingControl.HandleCreated, AddressOf OnHandleCreated
        End Sub

        Private Sub OnHandleDestroyed(sender As Object, e As EventArgs)
            ReleaseHandle()
        End Sub

        Public Overrides Sub ReleaseHandle()
            If _HighlightingControl IsNot Nothing Then
                RemoveHandler _HighlightingControl.HandleCreated, AddressOf OnHandleCreated
                RemoveHandler _HighlightingControl.HandleDestroyed, AddressOf OnHandleDestroyed
                RemoveHandler _HighlightingControl.LocationChanged, AddressOf OnLocationOrSizeChanged
                RemoveHandler _HighlightingControl.SizeChanged, AddressOf OnLocationOrSizeChanged
                _HighlightingControl = Nothing
            End If

            MyBase.ReleaseHandle()
        End Sub

        Protected Overrides Sub WndProc(ByRef m As Message)
            MyBase.WndProc(m)

            If m.Msg = WM_PAINT Then
                ' ハイライト描画する
                _highlightingStrategy?.Highlight(_HighlightingControl)
            End If

        End Sub

        ''' <summary>
        ''' ハイライトを開始する
        ''' </summary>
        ''' <param name="control">対象コントロール</param>
        Private Sub BeginHighlight(control As Control)
            _HighlightingControl = control
            If _HighlightingControl Is Nothing Then Return

            If _HighlightingControl.IsHandleCreated Then
                ' ハンドル紐付け＆再描画によりハイライトする
                AssignToHighlight(_HighlightingControl)
            Else
                ' コンストラクタで呼ばれたときなど、まだハンドルができていなのでできたときにハイライトするように予約する
                AddHandler _HighlightingControl.HandleCreated, AddressOf OnHandleCreated
            End If

            AddHandler _HighlightingControl.HandleDestroyed, AddressOf OnHandleDestroyed
            AddHandler _HighlightingControl.LocationChanged, AddressOf OnLocationOrSizeChanged
            AddHandler _HighlightingControl.SizeChanged, AddressOf OnLocationOrSizeChanged

        End Sub

        Private Sub OnLocationOrSizeChanged(sender As Object, e As EventArgs)
            ' 位置かサイズが変化したら再描画する
            Dim ctl As Control = TryCast(sender, Control)
            ctl?.Parent?.Refresh()
            ctl?.Refresh()
        End Sub

        ''' <summary>
        ''' ウィンドウプロシージャのフックを開始し、再描画させることでハイライトされるようにする
        ''' </summary>
        ''' <param name="control">対象コントロール</param>
        Private Sub AssignToHighlight(control As Control)
            AssignHandle(control.Handle)
            _highlightingStrategy.BeginHighlight(control)
            control.Parent.Refresh()
        End Sub

        ''' <summary>
        ''' ハイライトを終了する
        ''' </summary>
        ''' <param name="control">対象コントロール</param>
        Private Sub EndHighlight(control As Control)
            If control IsNot Nothing Then
                _highlightingStrategy.EndHighlight(control)
                control.Invalidate()
            End If
            ReleaseHandle()
        End Sub

    End Class
End Namespace