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

        _highlightingManager = New HighlightingManager(New HighlightingAttachment())
        TextBox1.AttachWaterMark("なにか入力してください")

        Dim a1 = New DraggableAttachment(Panel1, New BorderDrawActionStrategy(drawOutside:=False))
        Dim a2 = New DraggableAttachment(Panel2, New BorderDrawActionStrategy(drawOutside:=False))
        Dim a3 = New DraggableAttachment(Panel3, New BorderDrawActionStrategy(drawOutside:=False))
        Dim attachment2 = New DroppableAttachment(frameDest, New BorderDrawActionStrategy())
        Dim attachment3 = New DroppableAttachment(frameSource, New BorderDrawActionStrategy())
        a1.AddDropTarget(attachment2)
        a1.AddDropTarget(attachment3)
        a2.AddDropTarget(attachment2)
        a2.AddDropTarget(attachment3)
        a3.AddDropTarget(attachment2)
        a3.AddDropTarget(attachment3)


    End Sub

End Class