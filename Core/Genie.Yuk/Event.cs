using System;
using System.Collections.Generic;
using System.Text;

namespace Genie.Yuk
{
    public enum EventType
    {
        Graphics,
        Stop
    }

    public abstract class Event
    {
        protected EventType _type;

        public EventType type   // property
        {
            get { return _type; }   // get method
            set { _type = value; }  // set method
        }
    }

    public class GraphicsEvent : Event
    {
        public GraphicsEvent() : base()
        {
            _type = EventType.Graphics;
        }
    }

    public class StopEvent : Event
    {
        public StopEvent() : base()
        {
            _type = EventType.Stop;
        }
    }
}
