Option Explicit On
Option Strict On


Namespace Painting
    Public Class Paint

        ''' <summary>
        ''' 指定した画像を指定した明るさにして新しい画像を作成する
        ''' </summary>
        ''' <param name="img">基になる画像</param>
        ''' <param name="brightness">明るさ（-255～255）</param>
        ''' <returns>明るさが変更された画像</returns>
        Public Shared Function AdjustBrightness(ByVal img As Image, ByVal brightness As Integer) As Image
            '明るさを変更した画像の描画先となるImageオブジェクトを作成
            Dim newImg As New Bitmap(img.Width, img.Height)

            'ImageAttributesオブジェクトの作成
            Dim ia As Imaging.ImageAttributes = GetBrightnessImageAttributes(brightness)

            'newImgのGraphicsオブジェクトを取得
            Using g As Graphics = Graphics.FromImage(newImg)
                'ImageAttributesを使用して描画
                g.DrawImage(img, New Rectangle(0, 0, img.Width, img.Height),
                            0, 0, img.Width, img.Height, GraphicsUnit.Pixel, ia)
            End Using

            Return newImg
        End Function

        Public Shared Function GetBrightnessImageAttributes(ByVal brightness As Integer) As Imaging.ImageAttributes
            'ColorMatrixオブジェクトの作成
            '指定された値をRBGの各成分にプラスする
            Dim plusVal As Single = CSng(brightness) / 255.0F
            Dim cm As New System.Drawing.Imaging.ColorMatrix(New Single()() _
                {New Single() {1, 0, 0, 0, 0},
                 New Single() {0, 1, 0, 0, 0},
                 New Single() {0, 0, 1, 0, 0},
                 New Single() {0, 0, 0, 1, 0},
                 New Single() {plusVal, plusVal, plusVal, 0, 1}})

            'ImageAttributesオブジェクトの作成
            Dim ia As New Imaging.ImageAttributes()
            'ColorMatrixを設定する
            ia.SetColorMatrix(cm)

            Return ia
        End Function


        ''' <summary>
        ''' 指定した画像の彩度を変更した画像を作成する
        ''' </summary>
        ''' <param name="img">基になる画像</param>
        ''' <param name="saturation">彩度</param>
        ''' <returns>作成された画像</returns>
        Public Shared Function ChangeSaturation(ByVal img As Image, ByVal saturation As Single) As Image
            '彩度を変更した画像の描画先となるImageオブジェクトを作成
            Dim newImg As New Bitmap(img.Width, img.Height)
            'ImageAttributesオブジェクトの作成
            Dim ia As System.Drawing.Imaging.ImageAttributes = GetSaturationImageAttributes(saturation)

            Using g As Graphics = Graphics.FromImage(newImg)
                'ImageAttributesを使用して描画
                g.DrawImage(img, New Rectangle(0, 0, img.Width, img.Height),
                            0, 0, img.Width, img.Height, GraphicsUnit.Pixel, ia)
            End Using

            Return newImg
        End Function

        Public Shared Function GetSaturationImageAttributes(ByVal saturation As Single) As Imaging.ImageAttributes
            'ColorMatrixオブジェクトの作成
            Dim cm As New System.Drawing.Imaging.ColorMatrix()
            Const rwgt As Single = 0.3086F
            Const gwgt As Single = 0.6094F
            Const bwgt As Single = 0.082F
            cm.Matrix01 = (1.0F - saturation) * rwgt
            cm.Matrix02 = cm.Matrix01
            cm.Matrix00 = cm.Matrix01 + saturation
            cm.Matrix10 = (1.0F - saturation) * gwgt
            cm.Matrix12 = cm.Matrix10
            cm.Matrix11 = cm.Matrix10 + saturation
            cm.Matrix20 = (1.0F - saturation) * bwgt
            cm.Matrix21 = cm.Matrix20
            cm.Matrix22 = cm.Matrix20 + saturation
            cm.Matrix33 = 1
            cm.Matrix44 = 1

            'ImageAttributesオブジェクトの作成
            Dim ia As New System.Drawing.Imaging.ImageAttributes()
            'ColorMatrixを設定する
            ia.SetColorMatrix(cm)

            Return ia
        End Function
    End Class
End Namespace


