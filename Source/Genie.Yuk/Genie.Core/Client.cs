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
        private Game m_Game;
        private EventManagerClient m_Events;

        public Client(String path, EventManagerClient events = null, String IPAddress = "127.0.0.1")
        {
            m_IPAddress = IPAddress;
            m_Game = new Game(path);

            if (events == null)
            {
                m_Events = new EventManagerClient();
            }
            else
            {
                m_Events = events;
            }

            EventQueueClient.Enqueue(new GraphicsEvent());
        }

        public CancellationTokenSource Handler()
        {
            WinUtility win = new WinUtility();

            GameGraphics gg = new GameGraphics();

            CancellationTokenSource source1 = new CancellationTokenSource();
            CancellationToken token1 = source1.Token;

            Action myAction = (Action)(async () =>
            {
                while (!token1.IsCancellationRequested)
                {
                    ComponentManager.Update();
                    gg.Run(token1);

                    try
                    {
                        await Task.Delay(500, token1);
                    }
                    catch (TaskCanceledException)
                    {
                        System.Console.WriteLine("Request was cancelled");
                    };
                }
            });

            win.OnUiThread(myAction);

            return source1;
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
