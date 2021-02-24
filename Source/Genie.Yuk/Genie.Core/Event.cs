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

    public class StartEvent : Event
    {
    }

    public class JobEvent : Event
    {
        private Queue<Event> _jobEvents;

        public JobEvent()
        {
            _jobEvents = new Queue<Event>();
        }

        public void Enqueue(Event toRegister)
        {
            _jobEvents.Enqueue(toRegister);
        }

        public Event Dequeue()
        {
            return _jobEvents.Dequeue();
        }
    }
}
