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

        public Client(String path, String IPAddress = "127.0.0.1")
        {
            m_IPAddress = IPAddress;
            m_Game = new Game(path);
        }

        public void Wait()
        {
            var tasks = new List<Task>();
            Action<object> action = (object obj) =>
            {
                //Thread.Sleep(5000);
                Process p = ProcessServer.Resolve("GraphicsWorker");
                //p.Stop();
                p.Wait();

            };

            Task t2 = new Task(action, "Wait");
            t2.Start();
            tasks.Add(t2);

            var continuation = Task.WhenAll(tasks);
            try
            {
                continuation.Wait();
            }
            catch
            { }
        }

        public void Handler()
        {
            GameGraphics gg = new GameGraphics();
            gg.Thread();
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
