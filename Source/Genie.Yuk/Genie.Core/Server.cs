using System;
using System.Collections.Generic;
using System.Text;

using System.IO;
using System.Net;
using System.Net.Sockets;

namespace Genie.Yuk
{
    public class Server
    {
        private String m_IPAddress;
        private EventManager m_Events;
        protected string m_path;

        public Server(String path, EventManager events = null, String IPAddress = "127.0.0.1")
        {
            m_IPAddress = IPAddress;
            m_path = path;

            if (events == null)
            {
                m_Events = new EventManager(m_path);
            }
            else
            {
                m_Events = events;
            }

            //EventQueueServer.Enqueue(new GraphicsEvent());
        }

        public void StartServer()
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
