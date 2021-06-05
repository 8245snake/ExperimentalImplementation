using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Utility
{
    public sealed class Win32
    {
        // for WndProc.
        public delegate int WndProc(IntPtr hwnd, uint msg, uint wParam, int lParam);

        [DllImport("user32.dll")]
        public extern static IntPtr SetWindowLong(
            IntPtr hwnd, int nIndex, IntPtr dwNewLong);
        public const int GWL_WNDPROC = -4;

        [DllImport("user32.dll")]
        public extern static int CallWindowProc(
            IntPtr lpPrevWndFunc, IntPtr hwnd, uint msg, uint wParam, int lParam);

        [DllImport("user32.dll")]
        public extern static int DefWindowProc(
            IntPtr hwnd, uint msg, uint wParam, int lParam);

        // Windows メッセージ
        public const uint WM_PAINT = 0x000F;
        public const uint WM_ERASEBKGND = 0x0014;
        public const uint WM_KEYDOWN = 0x0100;
        public const uint WM_KEYUP = 0x0101;
        public const uint WM_MOUSEMOVE = 0x0200;
        public const uint WM_LBUTTONDOWN = 0x0201;
        public const uint WM_LBUTTONUP = 0x0202;
        public const uint WM_NOTIFY = 0x4E;

        // for WM_Paint
        [DllImport("user32.dll")]
        public extern static IntPtr GetDC(IntPtr hwnd);

        [DllImport("user32.dll")]
        public extern static bool ReleaseDC(IntPtr hwnd, IntPtr hdc);

        [DllImport("user32.dll")]
        public extern static IntPtr BeginPaint(IntPtr hwnd, ref PAINTSTRUCT ps);

        [DllImport("user32.dll")]
        public extern static bool EndPaint(IntPtr hwnd, ref PAINTSTRUCT ps);

        public struct PAINTSTRUCT
        {
            private IntPtr hdc;
            public bool fErase;
            public Rectangle rcPaint;
            public bool fRestore;
            public bool fIncUpdate;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
            public byte[] rgbReserved;
        }

        public static Point LParamToPoint(int lParam)
        {
            uint ulParam = (uint)lParam;
            return new Point(
                (int)(ulParam & 0x0000ffff),
                (int)((ulParam & 0xffff0000) >> 16));
        }
    }
}
