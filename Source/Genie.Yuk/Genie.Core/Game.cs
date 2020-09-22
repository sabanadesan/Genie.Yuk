using System;
using System.Collections.Generic;
using System.Text;

using System.Threading;
using System.Threading.Tasks;

namespace Genie.Yuk
{
    public class Game : Do
    {
        public Game(String path)
        {
            GameManager.Path = path;
        }


        public override void Run(CancellationToken token)
        {
            GameManager mng = GameManager.Instance;
        }

        public override void Stop()
        {

        }
    }
}
