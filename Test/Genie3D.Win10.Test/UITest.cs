using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Windows.UI.Input.Preview.Injection;

namespace Genie3D.Win10.Test
{
    public static class UITest
    {
        public static void MouseMove(int x, int y)
        {
            var info = new InjectedInputMouseInfo();
            info.MouseOptions = InjectedInputMouseOptions.Move;
            info.DeltaY = 100;

            InputInjector inputInjector = InputInjector.TryCreate();
            inputInjector.InjectMouseInput(new[] { info });
        }   
    }
}
