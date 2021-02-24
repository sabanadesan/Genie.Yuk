using System;

using Genie.Yuk;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Genie.Make;

namespace Genie.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            ScriptingEngine e = new ScriptingEngine();
            e.GenerateAssembly();

            Script script = new Script();

            Server s = new Server();

            Client client = new Client("Output");
            client.Start();

            while (true)
            {

            }
        }
    }
}
