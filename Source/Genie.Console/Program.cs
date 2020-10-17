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
            Server s = new Server();

            EventQueue.Enqueue(new GraphicsEvent());

            Client c = new Client("Output");
            c.Handler();
            c.Wait();
        }
    }
}
