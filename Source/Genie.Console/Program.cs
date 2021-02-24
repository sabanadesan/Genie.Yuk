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

            string m_path = "output";

            Server s = new Server(m_path);

            Client client = new Client(m_path);
            client.Start();

            while (true)
            {

            }
        }
    }
}
