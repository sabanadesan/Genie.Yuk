using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Genie.Yuk;

namespace Genie.Sample.RPG
{

    public class RpgGraphicsEvent : Event
    {
    }

    public class RpgEventManager : EventManager
    {
        public RpgEventManager() : base()
        {
        }

        public override Boolean Loop()
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
                if (_event.GetType() == typeof(RpgGraphicsEvent))
                {
                    System.Console.WriteLine("Draw");
                    EventQueue.Enqueue(new RpgGraphicsEvent());
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
