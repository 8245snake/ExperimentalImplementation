Option Explicit On
Option Strict On

Imports SpecialControls.Painting
Imports Utility

Namespace Services

    ''' <summary>
    ''' コントロールをハイライトする機能のマネージャクラス（未完成）
    ''' </summary>
    Public Class ControlHighlightingService
        Private _HighlightingControl As Control

        ''' <summary>
        ''' ハイライトするコントロールが変化したときに発火する
        ''' </summary>
        Public Event HighlightingControlChanged As EventHandler


        Public Property HighlightLineColor As Color
        Public Property HighlightLineWidth As Integer
        Public Property BlinkStyle As ErrorBlinkStyle


        Public Property HighlightingControl As Control
            Get
                Return _HighlightingControl
            End Get
            Set
                If _HighlightingControl IsNot Value Then
                    If _HighlightingControl IsNot Nothing Then
                        WndProcHooker.UnhookWndProc(_HighlightingControl, True)
                        _HighlightingControl.Invalidate()
                    End If
                    _HighlightingControl = Value
                    WndProcHooker.HookWndProc(_HighlightingControl, AddressOf Highlight, &HF, WndProcHooker.HookedProcInformation.BaseWndProcTiming.BeforeNew)
                    _HighlightingControl.Invalidate()
                    RaiseEvent HighlightingControlChanged(Me, New EventArgs())
                End If
            End Set
        End Property


        Public Function Highlight(hwnd As IntPtr, msg As UInteger, wParam As UInteger, lParam As Integer, ByRef handled As Boolean) As Integer
            If _HighlightingControl Is Nothing Then Return 0

            Dim tetragon As Tetragon = New Tetragon()
            tetragon.X = 0
            tetragon.Y = 0
            tetragon.Width = _HighlightingControl.Size.Width - 1
            tetragon.Height = _HighlightingControl.Size.Height - 1
            tetragon.Coloring = Shape.ColoringType.Outline
            tetragon.BorderColor = Color.Red
            tetragon.BorderWidth = 4

            Using g As Graphics = Graphics.FromHwnd(_HighlightingControl.Handle)
                tetragon.Draw(g)
            End Using

            Return 0
        End Function

    End Class
End Namespace


