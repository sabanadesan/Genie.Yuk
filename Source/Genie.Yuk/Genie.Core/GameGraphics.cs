using System;
using System.Collections.Generic;
using System.Text;

using System.Threading;
using System.Threading.Tasks;

namespace Genie.Yuk
{
    public enum GraphicsBackend
    {
        Vulkan,
        DirectX12
    }

    public class GameGraphics : Do
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

        public void Thread()
        {
            Process BackgroundWorker = new Process("GraphicsWorker");
            Task t = BackgroundWorker.Run(this);
        }

        public override void Run(CancellationToken token)
        {
            cls.Run(token);
        }

        public override void Stop()
        {
            cls.Stop();
        }
    }

    public abstract class Graphics
    {
        public abstract void Run(CancellationToken token);

        public abstract void Stop();
    }

    public class Vulkan : Graphics
    {
        private Genie3D.Vulkan.Class1 cls;

        public Vulkan()
        {
            cls = new Genie3D.Vulkan.Class1();
        }

        public override void Run(CancellationToken token)
        {
            cls.Run(token);
        }

        public override void Stop()
        {
            cls.Stop();
        }
    }

    public class DirectX : Graphics
    {
        private Genie3D.DirectX12.Class1 cls;

        public DirectX(System.Object swapChainPanel, int width, int height)
        {
            cls = new Genie3D.DirectX12.Class1(swapChainPanel, width, height);
        }

        public override void Run(CancellationToken token)
        {
            cls.Run(token);
        }

        public override void Stop()
        {
            cls.Stop();
        }
    }

}
