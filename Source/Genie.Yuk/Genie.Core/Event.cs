using System;
using System.Collections.Generic;
using System.Text;

namespace Genie.Yuk
{
    public abstract class Event
    {
    }

    public class GraphicsEvent : Event
    {
    }

    public class StopEvent : Event
    {
    }

    public class JobEvent : Event
    {
        private Queue<Genie.Yuk.Event> _jobEvents;

        public JobEvent()
        {
            _jobEvents = new Queue<Genie.Yuk.Event>();
        }

        public void Enqueue(Genie.Yuk.Event toRegister)
        {
            _jobEvents.Enqueue(toRegister);
        }

        public Genie.Yuk.Event Dequeue()
        {
            return _jobEvents.Dequeue();
        }
    }
}
