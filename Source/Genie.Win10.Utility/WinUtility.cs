using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Windows.System.Threading;
using Windows.Foundation;

namespace Genie.Win10.Utility
{
    public class WinUtility
    {

        public WinUtility()
        {

        }

        public void OnUiThread(Action action)
        {
            IAsyncAction asyncAction = Windows.System.Threading.ThreadPool.RunAsync((workItem) => action());
        }
    }
}
