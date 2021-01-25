using System;
using System.Collections.Generic;
using System.Text;

using System.Threading;
using System.Threading.Tasks;

namespace Genie.Yuk
{
    public class Game : Do
    {
        private GameManager mng;

        public Game(String path)
        {
            GameManager.Path = path;

            mng = GameManager.Instance;
        }


        public override void Run(CancellationToken token)
        {
            
        }

        public override void Stop()
        {

        }

        public void AddEntity()
        {
            mng.AddEntity();
        }
    }
}
