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

        public Server(EventManager events = null, String IPAddress = "127.0.0.1")
        {
            m_IPAddress = IPAddress;

            if (events == null)
            {
                m_Events = new EventManager();
            }
            else
            {
                m_Events = events;
            }
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
