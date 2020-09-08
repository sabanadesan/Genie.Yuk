using System;
using System.Collections.Generic;
using System.Text;

using Genie3D.DirectX12;
using Genie3D.Vulkan;

namespace Genie.Yuk
{
    public enum GraphicsBackend
    {
        Vulkan,
        DirectX12
    }

    public class GameGraphics
    {
        private Graphics cls;

        public GameGraphics(System.Object swapChainPanel, int width, int height)
        {
            cls = new DirectX(swapChainPanel, width, height);
        }

        public GameGraphics()
        {
            cls = new Vulkan();
        }

        public void Run()
        {
            cls.Run();
        }
    }

    public abstract class Graphics
    {
        public virtual void Run()
        {
        }
    }

    public class Vulkan : Graphics
    {
        private Genie3D.Vulkan.Class1 cls;

        public Vulkan()
        {
            cls = new Genie3D.Vulkan.Class1();
        }

        public override void Run()
        {
            cls.Run();
        }
    }

    public class DirectX : Graphics
    {
        private Genie3D.DirectX12.Class1 cls;

        public DirectX(System.Object swapChainPanel, int width, int height)
        {
            cls = new Genie3D.DirectX12.Class1(swapChainPanel, width, height);
        }

        public override void Run()
        {
            cls.Run();
        }
    }

}
