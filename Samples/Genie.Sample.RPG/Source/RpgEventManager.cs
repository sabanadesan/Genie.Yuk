using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading;
using System.Threading.Tasks;

using Genie.Yuk;

namespace Genie.Sample.RPG
{

    public class RpgGraphicsEvent : Event
    {
    }

    public class RpgEventManagerClient : EventManager
    {
        public RpgEventManagerClient(String path) : base(path)
        {

        }

        public override void Start(Object swapChainPanel, int Width, int Height)
        {
            GameGraphics gg = new GameGraphics(swapChainPanel, Width, Height);
            Service.Register<GameGraphics>(gg);

            StartAfter();
        }

        public override Boolean Loop(Genie.Yuk.Event _event)
        {
            WinUtility win = Service.Resolve<WinUtility>();
            GameGraphics gg = Service.Resolve<GameGraphics>();
            CancellationToken token = Service.Resolve<CancellationToken>();

            if (_event != null)
            {
                if (_event.GetType() == typeof(StartEvent))
                {
                    Action myAction0 = (Action)(() =>
                    {
                        gg.Start();
                    });

                    Task taskA = Task.Run(myAction0);
                    taskA.Wait();

                    Action myAction1 = (Action)(() =>
                    {
                        while (!token.IsCancellationRequested)
                        {
                            gg.AlwaysRun();
                        }
                    });

                    win.OnUiThread(myAction1);

                    EventQueueClient.Enqueue(new GraphicsEvent());
                }
                else if (_event.GetType() == typeof(GraphicsEvent))
                {
                    lock (WriteServer.balanceLock)
                    {
                        ComponentManager.Update();
                        gg.Run(token);

                        System.Console.WriteLine("Draw Client");
                    }

                    EventQueueClient.Enqueue(new GraphicsEvent());
                }
                else if (_event.GetType() == typeof(JobEvent))
                {
                    JobEvent job = (JobEvent)_event;

                    Boolean doLoop = true;

                    while (doLoop)
                    {
                        try
                        {
                            Genie.Yuk.Event tmpEvent = job.Dequeue();
                            Loop(tmpEvent);
                        }
                        catch (InvalidOperationException e)
                        {
                            doLoop = false;
                        }
                    }
                }
                else if (_event.GetType() == typeof(StopEvent))
                {
                    System.Console.WriteLine("Stop");
                    return false;
                }
            }

            return true;
        }
    }

    public class RpgEventManagerServer : EventManager
    {
        public RpgEventManagerServer(String path) : base(path)
        {
        }
    }
}
