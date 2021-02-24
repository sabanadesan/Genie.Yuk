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
using System.Threading;
using Genie.Yuk;
using Genie.Win10.Utility;
using Genie.Make;

namespace Genie.Sample.RPG
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private CancellationTokenSource cancel;
        private string m_path;

        public MainPage()
        {
            this.InitializeComponent();
            StorageFolder localFolder = ApplicationData.Current.LocalFolder;
            m_path = localFolder.Path;

            RpgEventManagerServer serverEvents = new RpgEventManagerServer(m_path);

            Script script = new Script();
            Server s = new Server(m_path, serverEvents);
        }

        private void swapChainPanel_Loaded(object sender, RoutedEventArgs e)
        {
            RpgEventManagerClient clientEvents = new RpgEventManagerClient(m_path);

            Genie.Win10.Utility.Client client = new Genie.Win10.Utility.Client(m_path);
            client.Start(swapChainPanel, (int)swapChainPanel.RenderSize.Width, (int)swapChainPanel.RenderSize.Height, clientEvents);
        }

        private void swapChainPanel_SizeChanged(object sender, SizeChangedEventArgs e)
        {
        }

        private void swapChainPanel_Unloaded(object sender, RoutedEventArgs e)
        {
        }
    }
}
