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

            Server s = new Server();
            s.HandleEvents();

            Client c = new Client();
            c.Handler();
            c.Wait();
        }
    }
}
