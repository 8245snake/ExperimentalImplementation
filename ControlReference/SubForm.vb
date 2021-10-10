Imports ControlAttachment
Imports ControlAttachment.Activity
Imports ControlAttachment.Decoration
Imports ControlAttachment.State
Imports ControlAttachment.Strategies

Public Class SubForm

    Private _highlightingManager As HighlightingManager

    Public Sub New()

        ' この呼び出しはデザイナーで必要です。
        InitializeComponent()

        ' InitializeComponent() 呼び出しの後で初期化を追加します。

        _highlightingManager = New HighlightingManager(New HighlightingAttachment())
        TextBox1.AttachWaterMark("なにか入力してください")

        ' ドラッグ可能コントロール
        Dim a1 = New DraggableAttachment(Panel1, New StandardDragActionStrategy(Panel1), New BorderDrawActionStrategy(drawOutside:=False))
        Dim a2 = New DraggableAttachment(Panel2, New StandardDragActionStrategy(Panel2), New BorderDrawActionStrategy(drawOutside:=False))
        Dim a3 = New DraggableAttachment(Panel3, New StandardDragActionStrategy(Panel3), New BorderDrawActionStrategy(drawOutside:=False))

        ' ドロップ先
        Dim drop1 = New DroppableAttachment(frameDest1, New BorderDrawActionStrategy())
        Dim drop2 = New DroppableAttachment(frameDest2, New BorderDrawActionStrategy())
        Dim source = New DroppableAttachment(frameSource, New BorderDrawActionStrategy())

        ' 紐付け
        a1.AddDropTarget(drop1)
        a1.AddDropTarget(drop2)
        a1.AddDropTarget(source)

        a2.AddDropTarget(drop1)
        a2.AddDropTarget(drop2)
        a2.AddDropTarget(source)

        a3.AddDropTarget(drop1)
        a3.AddDropTarget(drop2)
        a3.AddDropTarget(source)

        ' ホバー設定
        Panel1.AttachHoverColor(Color.AliceBlue)
        Panel2.AttachHoverColor(Color.AliceBlue)
        Panel3.AttachHoverColor(Color.AliceBlue)

    End Sub

End Class