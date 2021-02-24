using System;
using System.Collections.Generic;
using System.Text;

using System.Threading;
using System.Threading.Tasks;

namespace Genie.Yuk
{

    public static class WriteServer
    {
        public static readonly object balanceLock = new object();
    }

    public static class EventQueueServer
    {
        private static Queue<Genie.Yuk.Event> _registeredEvents = new Queue<Genie.Yuk.Event>();

        public static void Enqueue(Genie.Yuk.Event toRegister)
        {
            _registeredEvents.Enqueue(toRegister);
        }

        public static Genie.Yuk.Event Dequeue()
        {
            return _registeredEvents.Dequeue();
        }
    }

    public static class EventQueueClient
    {
        private static Queue<Genie.Yuk.Event> _registeredEvents = new Queue<Genie.Yuk.Event>();

        public static void Enqueue(Genie.Yuk.Event toRegister)
        {
            _registeredEvents.Enqueue(toRegister);
        }

        public static Genie.Yuk.Event Dequeue()
        {
            return _registeredEvents.Dequeue();
        }
    }

    public class EventManager
    {
        private string m_path;

        public EventManager(String path)
        {
            m_path = path;
        }

        public virtual void Start(Object swapChainPanel, int Width, int Height)
        {
            GameGraphics gg = new GameGraphics(swapChainPanel, Width, Height);
            Service.Register<GameGraphics>(gg);

            StartAfter();
        }

        public virtual void Start()
        {
            GameGraphics gg = new GameGraphics();
            Service.Register<GameGraphics>(gg);

            StartAfter();
        }

        protected void StartAfter()
        {
            GameManager gm = new GameManager(m_path);
            gm.Startup();
            Service.Register<GameManager>(gm);

            WinUtility win = new WinUtility();

            CancellationTokenSource source1 = new CancellationTokenSource();
            CancellationToken token1 = source1.Token;

            Service.Register<WinUtility>(win);
            Service.Register<CancellationToken>(source1.Token);

            Calculate();
        }


        public void Stop()
        {
            EventQueueClient.Enqueue(new StopEvent());
        }

        public void Calculate()
        {
            WinUtility win = Service.Resolve<WinUtility>();
            CancellationToken token = Service.Resolve<CancellationToken>();

            Clock timer = new Clock();
            timer.Start();
            int lastTime = timer.MsElapsed();

            Genie.Yuk.Event _event = null;

            double fps;

            Action myAction = (Action)(async () =>
            {
                while (!token.IsCancellationRequested)
                {
                    int current = timer.MsElapsed();
                    int elapsed = current - lastTime;
                    fps = timer.FPS(elapsed);

                    Console.WriteLine(fps);
                    try
                    {
                        _event = EventQueueClient.Dequeue();
                        Loop(_event);
                    }
                    catch (InvalidOperationException e)
                    {
                        //DoWhile = false;
                    }

                    try
                    {
                        await Task.Delay(500, token);
                    }
                    catch (TaskCanceledException)
                    {
                        System.Console.WriteLine("Request was cancelled");
                    };

                    lastTime = current;
                }

                timer.Stop();
            });

            win.OnUiThread(myAction);
        }

        public virtual Boolean Loop(Genie.Yuk.Event _event)
        {
            WinUtility win = Service.Resolve<WinUtility>();
            GameGraphics gg = Service.Resolve<GameGraphics>();
            CancellationToken token = Service.Resolve<CancellationToken>();

            if (_event != null)
            {
                if(_event.GetType() == typeof(StartEvent))
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
}