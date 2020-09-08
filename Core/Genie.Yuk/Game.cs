using System;
using System.Collections.Generic;
using System.Text;

namespace Genie.Yuk
{
    public class Game
    {
        public Game(GraphicsBackend backend, String path)
        {
            GameManager.Backend = backend;
            GameManager.Path = path;
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
