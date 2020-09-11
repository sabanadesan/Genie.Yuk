using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Windows.UI.Core;

namespace Genie.Win10.Utility
{
    public class WinUtility
    {
        private CoreDispatcher dispatcher;

        public WinUtility()
        {
            this.dispatcher = CoreWindow.GetForCurrentThread().Dispatcher;
        }

        public async Task OnUiThread(Action action)
        {
            await this.dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => action());
        }
    }
}
