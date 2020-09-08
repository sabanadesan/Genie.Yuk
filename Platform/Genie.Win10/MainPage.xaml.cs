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

namespace Genie3D.Win10
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void swapChainPanel_Loaded(object sender, RoutedEventArgs e)
        {
            
            StorageFolder localFolder = ApplicationData.Current.LocalFolder;

            Game game = new Game(GraphicsBackend.DirectX12, localFolder.Path);
            game.SetWindow(swapChainPanel, (int)(swapChainPanel.RenderSize.Width), (int)(swapChainPanel.RenderSize.Height));
            game.Run();
        }

        private void swapChainPanel_SizeChanged(object sender, SizeChangedEventArgs e)
        {
        }

        private void swapChainPanel_Unloaded(object sender, RoutedEventArgs e)
        {
        }
    }
}
