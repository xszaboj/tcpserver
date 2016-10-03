using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MouseProject
{
    public class Win32
    {
        [Flags]
        public enum MouseEventFlags
        {
            LeftDown = 0x00000002,
            LeftUp = 0x00000004,
            MiddleDown = 0x00000020,
            MiddleUp = 0x00000040,
            Move = 0x00000001,
            Absolute = 0x00008000,
            RightDown = 0x00000008,
            RightUp = 0x00000010,
            MouseWheel = 0x0800
        }

        [DllImport("User32.Dll")]
        public static extern long SetCursorPos(int x, int y);

        [DllImport("User32.Dll")]
        public static extern bool ClientToScreen(IntPtr hWnd, ref POINT point);

        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(out POINT lpPoint);

        [DllImport("user32.dll")]
        private static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);

        public static void MouseEvent(MouseEventFlags value)
        {
            POINT position = GetCursorPosition();
            mouse_event((int) value,position.x, position.y,0,0);
        }

        public static void Scroll(int value)
        {
            mouse_event((int)MouseEventFlags.MouseWheel,0, 0, value, 0);
        }

        public static POINT GetCursorPosition()
        {
            POINT currentMousePoint;
            var gotPoint = GetCursorPos(out currentMousePoint);
            if (!gotPoint) { currentMousePoint = new POINT(0, 0); }
            return currentMousePoint;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public POINT(int x, int y)
            {
                this.x = x;
                this.y = y;
            }

            public int x;
            public int y;
        }
    }
}
