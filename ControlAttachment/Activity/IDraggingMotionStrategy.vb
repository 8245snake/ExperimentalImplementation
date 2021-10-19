Imports System.Windows.Forms

Namespace Activity
    Public Interface IDraggingMotionStrategy
        Property TopParent As Control
        Property DropTargets As List(Of DroppableAttachment)
        Sub BeginDrag()
        Sub DragMoving()
        Sub EndDrag()
    End Interface
End Namespace