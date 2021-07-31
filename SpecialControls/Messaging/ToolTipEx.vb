Imports System.ComponentModel
Imports System.Runtime.CompilerServices
Imports SpecialControls.Messaging

Public Class ToolTipEx
    Inherits ToolTip

    Private _Images As ConditionalWeakTable(Of Control, Bitmap)

    Public Sub New()
        MyBase.New()
        _Images = New ConditionalWeakTable(Of Control, Bitmap)()
        Me.OwnerDraw = True
        AddHandler Me.Popup, AddressOf OnPopup
        AddHandler Me.Draw, AddressOf OnDraw

        Me.ShowAlways = True
    End Sub

    Public Sub SetToolTipEx(control As Control, caption As String, bmp As Bitmap)
        Me.SetToolTip(control, caption)

        If bmp IsNot Nothing Then
            _Images.Add(control, bmp)
        End If

    End Sub

    Public Sub SetToolTipEx(control As Control, bmp As Bitmap)
        SetToolTipEx(control, "image", bmp)
    End Sub

    Public Sub SetToolTipEx(control As Control, caption As String, font As Font, Optional fontColor As Color = Nothing, Optional backColor As Color = Nothing)
        Dim bmp = SplashMessage.CreateTextImage("クリアしました", font, fontColor, backColor)
        SetToolTipEx(control, caption, bmp)
    End Sub


    Public Sub ShowBitmap(bitmap As Bitmap, window As IWin32Window, x As Integer, y As Integer, duration As Integer)
        Dim control = TryCast(window, Control)
        If control IsNot Nothing Then
            _Images.Remove(control)
            _Images.Add(control, bitmap)
        End If
        Me.Show("image", window, x, y, duration)
    End Sub


    Private Sub OnPopup(sender As Object, e As PopupEventArgs)
        Dim bmp As Bitmap
        If _Images.TryGetValue(e.AssociatedControl, bmp) Then
            e.ToolTipSize = bmp.Size
        End If
    End Sub

    Private Sub OnDraw(sender As Object, e As DrawToolTipEventArgs)
        Dim bmp As Bitmap
        If _Images.TryGetValue(e.AssociatedControl, bmp) Then
            e.Graphics.DrawImage(bmp, e.Bounds.X, e.Bounds.Y)
        Else
            ' 画像なしなら普段通りの表示
            e.DrawText()
        End If
    End Sub

    Protected Overrides Sub Dispose(disposing As Boolean)
        RemoveHandler Me.Popup, AddressOf OnPopup
        RemoveHandler Me.Draw, AddressOf OnDraw
        MyBase.Dispose(disposing)
    End Sub
End Class