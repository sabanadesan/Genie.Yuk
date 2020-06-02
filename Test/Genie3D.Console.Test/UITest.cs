using System;
using System.Collections.Generic;
using System.Text;

using SharpVk.Glfw;

namespace Genie3D.Console.Test
{
    public static class UITest
    {
        public static void MouseMove(WindowHandle window, int x, int y)
        {
            Glfw3.SetCursorPosition(window, x, y);
        }
    }
}
