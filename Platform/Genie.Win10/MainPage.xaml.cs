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
using System.Threading;
using System.Threading.Tasks;
using Windows.UI.Core;

namespace Genie3D.Win10
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private CoreDispatcher dispatcher;

        public MainPage()
        {
            this.InitializeComponent();
        }

        private void swapChainPanel_Loaded(object sender, RoutedEventArgs e)
        {
            var tasks = new List<Task>();

            this.dispatcher = Windows.UI.Core.CoreWindow.GetForCurrentThread().Dispatcher;

            StorageFolder localFolder = ApplicationData.Current.LocalFolder;

            Game game = new Game(localFolder.Path);

            /*
            Process BackgroundWorker = new Process("BackgroundWorker3");
            Task t = BackgroundWorker.Run(game);
            tasks.Add(t);
            */

            CancellationTokenSource source1;
            source1 = Handler();

            //tasks.Add(t);

            Action<object> action2 = (object obj) =>
            {
                Thread.Sleep(5000);
                Console.WriteLine("Enter input:"); // Prompt
                source1.Cancel();
            };

            Task t2 = new Task(action2, "alpha2");
            t2.Start();
            tasks.Add(t2);


            var continuation = Task.WhenAll(tasks);
            try
            {
                continuation.Wait();
            }
            catch
            { }
            
        }

        private CancellationTokenSource Handler()
        {
            GameGraphics gg = new GameGraphics(swapChainPanel, (int)(swapChainPanel.RenderSize.Width), (int)(swapChainPanel.RenderSize.Height));

            CancellationTokenSource source1;
            CancellationToken token1;

            source1 = new CancellationTokenSource();
            token1 = source1.Token;

            Action myAction = (Action)(() =>
            {
                gg.Run(token1);
            });

            Action myAction2 = (Action)(() =>
            {
                Console.WriteLine("hello");
            });

            Process BackgroundWorker1 = new Process("BackgroundWorker1");

            Task t = OnUiThread(myAction);
            //System.Runtime.CompilerServices.TaskAwaiter a = t.GetAwaiter();
            //a.OnCompleted(myAction2);

            return source1;
        }

        private async Task OnUiThread(Action action)
        {
            await this.dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => action());
        }

        private void swapChainPanel_SizeChanged(object sender, SizeChangedEventArgs e)
        {
        }

        private void swapChainPanel_Unloaded(object sender, RoutedEventArgs e)
        {
        }
    }
}
