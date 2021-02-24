using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

using Genie.Yuk;

namespace Genie.Win10.Utility
{
    public class Client : Genie.Yuk.Client
    {
        public Client(String path, String IPAddress = "127.0.0.1") : base(path, IPAddress)
        {
            m_path = path;
        }

        public void Start(Object swapChainPanel, int Width, int Height, EventManager events = null)
        {
            if (events == null)
            {
                m_Events = new EventManager(m_path);
            }
            else
            {
                m_Events = events;
            }

            EventQueueClient.Enqueue(new StartEvent());

            m_Events.Start(swapChainPanel, Width, Height);
        }
    }
}
