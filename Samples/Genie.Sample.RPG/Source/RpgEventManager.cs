﻿using System;
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

    public class RpgEventManager : EventManagerClient
    {
        public RpgEventManager() : base()
        {
        }

        public override Boolean Loop(Genie.Yuk.Event _event)
        {
            if (_event != null)
            {
                if (_event.GetType() == typeof(RpgGraphicsEvent))
                {
                    System.Console.WriteLine("Draw RPG Client");
                    EventQueueClient.Enqueue(new RpgGraphicsEvent());
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
