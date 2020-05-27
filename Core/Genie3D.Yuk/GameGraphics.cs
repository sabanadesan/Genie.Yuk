using System;
using System.Collections.Generic;
using System.Text;

using Genie3D.DirectX12;
using Genie3D.Vulkan;

namespace Genie3D.Net
{
    public enum GraphicsBackend
    {
        Vulkan,
        DirectX12
    }

    public class GameGraphics
    {

        public GameGraphics(System.Object swapChainPanel, int width, int height)
        {
            Genie3D.DirectX12.Class1 cls = new Genie3D.DirectX12.Class1(swapChainPanel, width, height);
            cls.Run();
        }

        public GameGraphics()
        {
            Genie3D.Vulkan.Class1 cls = new Genie3D.Vulkan.Class1();
            cls.Run();
        }
    }
}
