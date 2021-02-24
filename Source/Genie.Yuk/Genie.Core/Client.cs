using System;
using System.Collections.Generic;
using System.Text;

using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace Genie.Yuk
{
    public class Client
    {
        private String m_IPAddress;
        protected EventManager m_Events;
        protected string m_path;
        
        public Client(String path, String IPAddress = "127.0.0.1")
        {
            m_IPAddress = IPAddress;
            m_path = path;
        }

        public void Start(EventManager events = null)
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

            m_Events.Start();
        }

        public void ConnectServer()
        {
            try
            {
                String server = Dns.GetHostName();
                IPAddress iIPAddress = IPAddress.Parse(m_IPAddress);
            }
            catch (Exception e)
            {
                Console.WriteLine("An Exception Occurred while Listening :" + e.ToString());
            }
        }
    }
}
