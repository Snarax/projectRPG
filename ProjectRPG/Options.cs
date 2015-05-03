using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectRPG
{
    public class Options
    {
        public static bool CursorViewportLock = true;
        public static bool Debug = true;
        public static byte WindowMode = 1;                  // 0 - Fullscreen, 1 - FullWindowed, 2 - Window
        public static bool HardwareMouse;
        public static bool VerticalSynchronize = true;
    }
}
