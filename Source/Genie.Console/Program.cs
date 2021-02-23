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
            ScriptingEngine e = new ScriptingEngine();
            e.GenerateAssembly();

            Server s = new Server();

            Client c = new Client("Output");
            CancellationTokenSource cancel = c.Handler();
            CancellationToken token1 = cancel.Token;

            while (!token1.IsCancellationRequested)
            {

            }
        }
    }
}
