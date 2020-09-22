using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409
using Windows.Storage;
using Genie.Yuk;
using Genie.Win10.Utility;

using System.Threading.Tasks;
using System.Threading;

namespace Genie.Sample.RPG
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private WinUtility win;

        public MainPage()
        {
            this.win = new WinUtility();

            this.InitializeComponent();

            HandleEvents();
        }

        private void swapChainPanel_Loaded(object sender, RoutedEventArgs e)
        {
            StorageFolder localFolder = ApplicationData.Current.LocalFolder;
            Game game = new Game(localFolder.Path);

            CancellationTokenSource source1 = Handler();
            source1.Cancel();
        }

        private void HandleEvents()
        {
            RpgEventManager mgr = new RpgEventManager();

            EventQueue.Enqueue(new GraphicsEvent());
        }

        private CancellationTokenSource Handler()
        {
            GameGraphics gg = new GameGraphics(swapChainPanel, (int)(swapChainPanel.RenderSize.Width), (int)(swapChainPanel.RenderSize.Height));
            //game.Run();

            CancellationTokenSource source1 = new CancellationTokenSource();
            CancellationToken token1 = source1.Token;

            Action myAction = (Action)(() =>
            {
                gg.Run(token1);
            });

            Task t = this.win.OnUiThread(myAction);

            return source1;
        }

        private void swapChainPanel_SizeChanged(object sender, SizeChangedEventArgs e)
        {
        }

        private void swapChainPanel_Unloaded(object sender, RoutedEventArgs e)
        {
        }
    }
}
