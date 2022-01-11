using System;
using System.Runtime.InteropServices;

namespace ReactiveProfiler
{
    static class NativeMethods
    {

        [DllImport("user32")]
        public static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);
        [DllImport("user32.dll")]
        public static extern bool SendMessageCallback(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam, SendAsyncProc lpCallback, UIntPtr dwData);
        public delegate void SendAsyncProc(IntPtr hwnd, uint uMsg, UIntPtr dwData, IntPtr lResult);

    }
}