Option Explicit On
Option Strict On

Imports System.Windows.Forms
Imports ControlAttachment.Strategies

Namespace State

    ''' <summary>
    ''' コントロールをハイライトするためのマネージャクラス
    ''' </summary>
    Public Class HighlightingManager
        Inherits NativeWindow

        Private _HighlightingControl As Control
        Private _HighlightingActionStrategy As IHighlightingActionStrategy

        ''' <summary>
        ''' ハイライトするロジックを取得または設定する
        ''' </summary>
        Public Property HighlightingActionStrategy As IHighlightingActionStrategy
            Get
                Return _HighlightingActionStrategy
            End Get
            Set
                If _HighlightingActionStrategy IsNot Nothing Then
                    ' ハイライト停止
                    EndHighlight(_HighlightingControl)
                End If
                _HighlightingActionStrategy = Value
                If _HighlightingActionStrategy IsNot Nothing AndAlso _HighlightingControl IsNot Nothing Then
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
        ''' <param name="highlightingActionStrategy">ハイライトロジック</param>
        Public Sub New(highlightingActionStrategy As IHighlightingActionStrategy)
            _HighlightingActionStrategy = highlightingActionStrategy
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
                _HighlightingControl = Nothing
            End If

            MyBase.ReleaseHandle()
        End Sub

        Protected Overrides Sub WndProc(ByRef m As Message)
            MyBase.WndProc(m)

            If m.Msg = WM_PAINT Then
                ' ハイライト描画する
                _HighlightingActionStrategy?.Highlight(_HighlightingControl)
            End If

        End Sub

        ''' <summary>
        ''' ハイライトを開始する
        ''' </summary>
        ''' <param name="control">対象コントロール</param>
        Private Sub BeginHighlight(control As Control)
            _HighlightingControl = control
            AddHandler _HighlightingControl.HandleDestroyed, AddressOf OnHandleDestroyed

            If _HighlightingControl.IsHandleCreated Then
                ' ハンドル紐付け＆再描画によりハイライトする
                AssignToHighlight(_HighlightingControl)
            Else
                ' コンストラクタで呼ばれたときなど、まだハンドルができていなのでできたときにハイライトするように予約する
                AddHandler _HighlightingControl.HandleCreated, AddressOf OnHandleCreated
            End If

        End Sub

        ''' <summary>
        ''' ウィンドウプロシージャのフックを開始し、再描画させることでハイライトされるようにする
        ''' </summary>
        ''' <param name="control">対象コントロール</param>
        Private Sub AssignToHighlight(control As Control)
            AssignHandle(control.Handle)
            _HighlightingActionStrategy.BeginHighlight(control)
            control.Parent.Refresh()
        End Sub

        ''' <summary>
        ''' ハイライトを終了する
        ''' </summary>
        ''' <param name="control">対象コントロール</param>
        Private Sub EndHighlight(control As Control)
            If control IsNot Nothing Then
                _HighlightingActionStrategy.EndHighlight(control)
            End If
            ReleaseHandle()
        End Sub

    End Class
End Namespace