using System;
using System.Collections.Generic;
using System.Text;

namespace Genie3D.Net
{
    public class Game
    {
        public Game(GraphicsBackend backend)
        {
            GameManager.Backend = backend;
        }

        public void SetWindow(System.Object swapChainPanel, int width, int height)
        {
            GameManager.SwapChainPanel = swapChainPanel;
            GameManager.Width = width;
            GameManager.Height = height;
        }

        public void Run()
        {
            GameManager mng = GameManager.Instance;
        }
    }
}
