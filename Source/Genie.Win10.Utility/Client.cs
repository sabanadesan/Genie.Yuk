using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

using Genie.Yuk;

namespace Genie.Win10.Utility
{
    public class Client : Genie.Yuk.Client
    {
        public Client(String path, EventManagerClient events = null, String IPAddress = "127.0.0.1") : base(path, events, IPAddress)
        {
        }

        public void Start(Object swapChainPanel, int Width, int Height)
        {
            gg = new GameGraphics(swapChainPanel, Width, Height);
        }
    }
}
