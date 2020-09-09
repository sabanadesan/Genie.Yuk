using SharpVk.Spirv;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using Utility.Yuk;
using System.Threading;

namespace Genie.Yuk
{
    internal class GameManager
    {
        private static GameManager _instance;
        private Boolean _is_screen;
        private static GraphicsBackend _backend;
        private static System.Object _swapChainPanel;
        private static int? _width;
        private static int? _height;
        private static String _path;

        private GameManager() {
            Run();
        }

        private void Run()
        {
            var tasks = new List<Task>();

            if (_backend == GraphicsBackend.Vulkan)
            {
                GameGraphics g = Startup();

                Process BackgroundWorker = new Process("BackgroundWorker");
                Task t = BackgroundWorker.Run(g);
                tasks.Add(t);

                Action<object> action = (object obj) =>
                {

                    Thread.Sleep(5000);
                    Process p = ProcessServer.Resolve("BackgroundWorker");
                    p.Stop();
                };

                Task t1 = new Task(action, "alpha");
                t1.Start();
                tasks.Add(t1);

                Action<object> action2 = (object obj) =>
                {

                    Console.WriteLine("Enter input:"); // Prompt
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
            else if (_backend == GraphicsBackend.DirectX12)
            {
                GameGraphics g = Startup();
                g.Run();
            }
        }

        private GameGraphics Startup()
        {
            Service.Register<Setting>(new Setting());
            Setting setting = Service.Resolve<Setting>();

            setting.Load("Config.ini", _path);

            CreateDefaultSettings(setting);

            setting.Save("Configuration.ini", _path);

            GameGraphics cls = null; 

            if (_backend == GraphicsBackend.Vulkan)
            {
                Service.Register<GameGraphics>(new GameGraphics());
            }
            else if (_backend == GraphicsBackend.DirectX12)
            {
                Utility.Yuk.Exception.CheckIsIntializedOrThrow(_swapChainPanel, _width, _height);

                Service.Register<GameGraphics>(new GameGraphics(_swapChainPanel, (int)_width, (int)_height));
            }

            cls = Service.Resolve<GameGraphics>();

            Utility.Yuk.Exception.CheckIsIntializedOrThrow(cls);

            return cls;
        }

        private void CreateDefaultSettings(Setting setting)
        {
            Menu menu = new Menu("Graphics");

            setting.Add(menu);

            MenuEntryBool entry = new MenuEntryBool("Fullscreen", false);
            menu.Add(entry);

            MenuEntryNumber entry1 = new MenuEntryNumber("Height", 10.00);
            menu.Add(entry1);
        }

        public static GameManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GameManager();
                }
                return _instance;
            }
        }

        public Boolean IsOnScreen
        {
            get
            {
                _is_screen = true;
                return _is_screen;
            }
        }

        public static GraphicsBackend Backend
        {
            get
            {
                return _backend;
            }
            set 
            {
                _backend = value; 
            }
        }

        public static int? Height
        {
            get
            {
                return _height;
            }
            set
            {
                _height = value;
            }
        }
        public static int? Width
        {
            get
            {
                return _width;
            }
            set
            {
                _width = value;
            }
        }

        public static System.Object SwapChainPanel
        {
            get
            {
                return _swapChainPanel;
            }
            set
            {
                _swapChainPanel = value;
            }
        }

        public static string Path
        {
            get
            {
                return _path;
            }
            set
            {
                _path = value;
            }
        }
    }
}
