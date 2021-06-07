Option Explicit On
Option Strict On

Namespace Win32
    Module GraphicAPI

        ''' <summary>
        '''     Specifies a raster-operation code. These codes define how the color data for the
        '''     source rectangle is to be combined with the color data for the destination
        '''     rectangle to achieve the final color.
        ''' </summary>
        Enum TernaryRasterOperations As UInteger
            ''' <summary>dest = source</summary>
            SRCCOPY = &HCC0020
            ''' <summary>dest = source OR dest</summary>
            SRCPAINT = &HEE0086
            ''' <summary>dest = source AND dest</summary>
            SRCAND = &H8800C6
            ''' <summary>dest = source XOR dest</summary>
            SRCINVERT = &H660046
            ''' <summary>dest = source AND (NOT dest)</summary>
            SRCERASE = &H440328
            ''' <summary>dest = (NOT source)</summary>
            NOTSRCCOPY = &H330008
            ''' <summary>dest = (NOT src) AND (NOT dest)</summary>
            NOTSRCERASE = &H1100A6
            ''' <summary>dest = (source AND pattern)</summary>
            MERGECOPY = &HC000CA
            ''' <summary>dest = (NOT source) OR dest</summary>
            MERGEPAINT = &HBB0226
            ''' <summary>dest = pattern</summary>
            PATCOPY = &HF00021
            ''' <summary>dest = DPSnoo</summary>
            PATPAINT = &HFB0A09
            ''' <summary>dest = pattern XOR dest</summary>
            PATINVERT = &H5A0049
            ''' <summary>dest = (NOT dest)</summary>
            DSTINVERT = &H550009
            ''' <summary>dest = BLACK</summary>
            BLACKNESS = &H42
            ''' <summary>dest = WHITE</summary>
            WHITENESS = &HFF0062
            ''' <summary>
            ''' Capture window as seen on screen.  This includes layered windows
            ''' such as WPF windows with AllowsTransparency="true"
            ''' </summary>
            CAPTUREBLT = &H40000000
        End Enum


        Declare Function GetDC Lib "user32.dll" (ByVal hWnd As IntPtr) As IntPtr
        Declare Function CreateCompatibleDC Lib "gdi32.dll" (hdc As IntPtr) As IntPtr
        Declare Function DeleteDC Lib "gdi32" (ByVal hDC As IntPtr) As IntPtr
        Declare Function ReleaseDC Lib "user32" (ByVal hwnd As IntPtr, ByVal hDC As IntPtr) As IntPtr

        Declare Function BitBlt Lib "gdi32.dll" (ByVal hdc As IntPtr, ByVal nXDest As Integer,
                                                 ByVal nYDest As Integer,
                                                 ByVal nWidth As Integer,
                                                 ByVal nHeight As Integer,
                                                 ByVal hdcSrc As IntPtr,
                                                 ByVal nXSrc As Integer,
                                                 ByVal nYSrc As Integer,
                                                 ByVal dwRop As TernaryRasterOperations) As Boolean



        Declare Function AlphaBlend Lib "msimg32.dll" _
            (ByVal hdcDest As IntPtr _
            , ByVal nXDest As Long _
            , ByVal nYDest As Long _
            , ByVal nWidthDest As Long _
            , ByVal nHeightDest As Long _
            , ByVal hdcSrc As IntPtr _
            , ByVal nXSrc As Long _
            , ByVal nYSrc As Long _
            , ByVal nWidthSrc As Long _
            , ByVal nHeightSrc As Long _
            , ByVal nBlendFunc As Long) As Long

        Declare Function SelectObject Lib "gdi32.dll" (ByVal prmlngHDc As IntPtr, ByVal hObject As IntPtr) As IntPtr

        Declare Function InvalidateRect Lib "User32" (ByVal hWnd As IntPtr, ByRef lpRect As RECT, ByVal bErase As Boolean) As Boolean
        Declare Function InvalidateRect Lib "User32" (ByVal hWnd As IntPtr, ByRef lpRect As IntPtr, ByVal bErase As Boolean) As Boolean


        <System.Runtime.InteropServices.DllImport("User32.dll")>
        Public Function PrintWindow(ByVal hwnd As IntPtr, ByVal hDC As IntPtr, ByVal nFlags As Integer) As Boolean
        End Function

        Public Structure RECT
            Public Left As Long
            Public Top As Long
            Public Right As Long
            Public Bottom As Long
        End Structure


    End Module
End Namespace


