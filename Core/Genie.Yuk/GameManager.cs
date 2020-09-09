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

        private static String _path;

        private GameManager() {
            Run();
        }

        private void Run()
        {
            var tasks = new List<Task>();

            Process BackgroundWorker = new Process("BackgroundWorker");
            Task t = BackgroundWorker.Run(Startup);
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
                Thread.Sleep(10000);
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

        public void Startup(CancellationToken token)
        {
            Service.Register<Setting>(new Setting());
            Setting setting = Service.Resolve<Setting>();

            setting.Load("Config.ini", _path);

            CreateDefaultSettings(setting);

            setting.Save("Configuration.ini", _path);
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
