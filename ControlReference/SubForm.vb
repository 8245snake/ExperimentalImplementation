Imports ControlAttachment
Imports ControlAttachment.Activity
Imports ControlAttachment.State
Imports ControlAttachment.Strategies

Public Class SubForm

    Private _highlightingManager As HighlightingManager

    Public Sub New()

        ' この呼び出しはデザイナーで必要です。
        InitializeComponent()

        ' InitializeComponent() 呼び出しの後で初期化を追加します。

        _highlightingManager = New HighlightingManager(New BorderDrawStrategy())
        TextBox1.AttachWaterMark("なにか入力してください")

        ' ドラッグ可能コントロール
        Dim a1 = New DraggableAttachment(Panel1, New MovingDraggingMotionStrategy(Panel1), New BorderDrawStrategy(drawOutside:=False))
        Dim a2 = New DraggableAttachment(Panel2, New MovingDraggingMotionStrategy(Panel2), New BorderDrawStrategy(drawOutside:=False))
        Dim a3 = New DraggableAttachment(Panel3, New MovingDraggingMotionStrategy(Panel3), New BorderDrawStrategy(drawOutside:=False))

        ' ドロップ先
        Dim drop1 = New DroppableAttachment(frameDest1, New BorderDrawStrategy())
        Dim drop2 = New DroppableAttachment(frameDest2, New BorderDrawStrategy())
        Dim source = New DroppableAttachment(frameSource, New BorderDrawStrategy())

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
        Panel1.SetHoverColor(Color.AliceBlue)
        Panel2.SetHoverColor(Color.AliceBlue)
        Panel3.SetHoverColor(Color.AliceBlue)

    End Sub

End Class