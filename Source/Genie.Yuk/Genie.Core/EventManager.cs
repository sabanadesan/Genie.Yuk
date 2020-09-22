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

        public override void Run()
        {
            Calculate();
        }

        public override void Stop()
        {
            EventQueue.Enqueue(new StopEvent());
        }

        private void Calculate()
        {
            Boolean DoWhile = true;
            EventQueue.Enqueue(new GraphicsEvent());

            while (DoWhile)
            {
                DoWhile = Loop();
            }
        }

        private void Calculate(CancellationToken token)
        {
            Boolean DoWhile = true;
            EventQueue.Enqueue(new GraphicsEvent());

            while (DoWhile)
            {
                token.ThrowIfCancellationRequested();

                DoWhile = Loop();
            }
        }

        private Boolean Loop()
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
                if (_event.type == EventType.Graphics)
                {
                    System.Console.WriteLine("Draw");
                    EventQueue.Enqueue(new GraphicsEvent());
                }
                else if (_event.type == EventType.Stop)
                {
                    System.Console.WriteLine("Stop");
                    return false;
                }
            }

            return true;
        }
    }
}