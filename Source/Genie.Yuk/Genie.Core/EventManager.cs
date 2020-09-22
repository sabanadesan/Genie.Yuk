using System;
using System.Collections.Generic;
using System.Text;

using System.Threading;
using System.Threading.Tasks;

namespace Genie.Yuk
{
    public static class EventQueue
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

    public class EventManager : Do
    {
        public EventManager()
        {
            Process BackgroundWorker = new Process("EventsWorker");
            Task t = BackgroundWorker.Run(this);
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

        public override void Stop()
        {
            EventQueue.Enqueue(new StopEvent());
        }

        private void Calculate(CancellationToken token)
        {
            Boolean DoWhile = true;

            Clock timer = new Clock();
            timer.Start();
            int lastTime = timer.MsElapsed();

            while (DoWhile)
            {
                token.ThrowIfCancellationRequested();

                int current = timer.MsElapsed();
                int elapsed = current - lastTime;

                DoWhile = Loop();

                lastTime = current;
            }

            timer.Stop();
        }

        public virtual Boolean Loop()
        {
            Genie.Yuk.Event _event = null;

            try
            {
                _event = EventQueue.Dequeue();
            }
            catch (InvalidOperationException e)
            {
                return false;
            }

            if (_event != null)
            {
                if (_event.GetType() == typeof(GraphicsEvent))
                {
                    System.Console.WriteLine("Draw");
                    EventQueue.Enqueue(new GraphicsEvent());
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