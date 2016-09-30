using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseProject
{
    public class MouseManager
    {
        public void MoveCursor(int x, int y)
        {
            Win32.POINT p = new Win32.POINT();
            p.x = x;
            p.y = y;

            //Win32.ClientToScreen(this.Handle, ref p);
            Win32.SetCursorPos(p.x, p.y);
        }
    }
}
