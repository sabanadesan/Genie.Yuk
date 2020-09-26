using System;

using Genie.Yuk;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Genie.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game("Output");

            EventManager e = new EventManager();
            EventQueue.Enqueue(new GraphicsEvent());

            Server s = new Server();

            Client c = new Client();
            c.Handler();
            c.Wait();
        }
    }
}
