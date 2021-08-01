
Imports SpecialControls.Win32

Namespace Messaging

    ''' <summary>
    ''' BitmapにしたいFormが継承する基底クラス
    ''' </summary>
    Public MustInherit Class ToolTipForm
        Inherits Form

        Private _CalledByGetBitmap As Boolean = False
        Private _Bitmap As Bitmap

        Protected Overrides ReadOnly Property ShowWithoutActivation As Boolean
            Get
                ' フォーカスを奪わないためにTrueとしたい
                Return True
            End Get
        End Property

        Sub New()
            Me.StartPosition = FormStartPosition.Manual
            Me.Location = New Point(-10000, -10000)
            Me.ShowInTaskbar = False
        End Sub

        Protected Overrides Sub OnShown(e As EventArgs)
            MyBase.OnShown(e)
            If _CalledByGetBitmap Then
                ' Shown後じゃないと画面のスクショが撮れないためここで実行する
                _Bitmap = CreateFormImage(Me)
                Me.Close()
            End If
        End Sub

        ''' <summary>
        ''' フォームを実体化しスクリーンショット（Bitmap）を撮影する
        ''' </summary>
        ''' <returns>画面のスクリーンショット</returns>
        Public Function GetBitmap() As Bitmap
            _CalledByGetBitmap = True
            ' 同期処理にしたいのでShowDialogとする。OnShownですぐに閉じるのでフリーズはしないはず。
            Me.ShowDialog()
            _CalledByGetBitmap = False
            Return _Bitmap
        End Function


        ''' <summary>
        ''' 指定したフォームのビットマップを作成する
        ''' </summary>
        ''' <param name="form">フォーム</param>
        ''' <param name="clientAreaOnly">クライアント領域のみに限定するか</param>
        ''' <returns>ビットマップ</returns>
        Private Shared Function CreateFormImage(form As Form, Optional clientAreaOnly As Boolean = True) As Bitmap

            Dim bmt As Bitmap = CaptureControl(form)

            If clientAreaOnly Then
                ' クライアント領域に切り詰める
                Dim rect As Rectangle
                rect.X = CInt((form.Size.Width - form.ClientSize.Width) / 2)
                rect.Y = form.Size.Height - form.ClientSize.Height - rect.X
                rect.Width = form.ClientSize.Width
                rect.Height = form.ClientSize.Height
                bmt = bmt.Clone(rect, bmt.PixelFormat)
            End If

            Return bmt
        End Function

        ''' <summary>
        ''' コントロールをキャプチャする。
        ''' DrawToBitmapでは不可能なリッチテキストコントロールにも対応。
        ''' </summary>
        ''' <param name="ctrl">コントロール</param>
        ''' <returns>ビットマップ</returns>
        Private Shared Function CaptureControl(ByVal ctrl As Control) As Bitmap
            Dim img As New Bitmap(ctrl.Width, ctrl.Height)
            Using memg As Graphics = Graphics.FromImage(img)
                Dim dc As IntPtr = memg.GetHdc()
                PrintWindow(ctrl.Handle, dc, 0)
                memg.ReleaseHdc(dc)
            End Using
            Return img
        End Function


    End Class
End Namespace