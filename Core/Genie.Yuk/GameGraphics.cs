using System;
using System.Collections.Generic;
using System.Text;

using Genie3D.DirectX12;
using Genie3D.Vulkan;

using System.Threading;
using System.Threading.Tasks;

namespace Genie.Yuk
{
    public enum GraphicsBackend
    {
        Vulkan,
        DirectX12
    }

    /*
    public class Gme : Do
    {
        private Queue<Event> _events;

        public Gme()
        {
            _events = new Queue<Event>();
        }

        public override void Run(CancellationToken token)
        {
            try
            {
                Calculate(token);
            }
            catch (OperationCanceledException ex) when (ex.CancellationToken == token) // includes TaskCanceledException
            {
                Console.WriteLine("Cancelled Exception.");
            }
         }

        private void Calculate(CancellationToken token)
        {
            _events.Enqueue(new GraphicsEvent());

            while (true)
            {
                token.ThrowIfCancellationRequested();

                Event _event = null;

                try
                {
                    // raise Error
                    //log.write('task: ' + str(i), error.Standard);
                    _event = _events.Dequeue();
                }
                catch (InvalidOperationException e)
                {
                    break;
                }

                if (_event != null)
                {
                    if (_event.type == EventType.Graphics)
                    {
                        //System.Console.WriteLine("Draw");
                        _events.Enqueue(new GraphicsEvent());
                    }
                    else if (_event.type == EventType.Stop)
                    {
                        System.Console.WriteLine("Stop");
                        break;
                    }
                }
            }
        }

        public override void Stop()
        {
            _events.Enqueue(new StopEvent());
        }
    }

    */

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

        public override void Run(CancellationToken token)
        {
            cls.Run(token);
        }

        public override void Run()
        {
            cls.Run();
        }

        public override void Stop()
        {
            cls.Stop();
        }
    }

    public abstract class Graphics
    {
        public abstract void Run(CancellationToken token);

        public abstract void Run();

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

        public override void Run()
        {
            cls.Run();
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

        public override void Run()
        {
            cls.Run();
        }

        public override void Stop()
        {
            cls.Stop();
        }
    }

}
