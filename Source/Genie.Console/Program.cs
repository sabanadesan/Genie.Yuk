using System;

using Genie.Yuk;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Genie.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            EventManager mgr = new EventManager();

            Process BackgroundWorker = new Process("Events");
            Task t = BackgroundWorker.Run(mgr);

            var tasks = new List<Task>();

            Game game = new Game("Output");
            //game.Run();

            GameGraphics gg = new GameGraphics();

            Process BackgroundWorker1 = new Process("BackgroundWorker");
            Task t1 = BackgroundWorker1.Run(gg.Run);
            tasks.Add(t1);

            Action<object> action = (object obj) =>
            {
                Thread.Sleep(5000);
                Process p = ProcessServer.Resolve("BackgroundWorker");
                p.Stop();
            };

            Task t2 = new Task(action, "alpha");
            t2.Start();
            tasks.Add(t2);

            var continuation = Task.WhenAll(tasks);
            try
            {
                continuation.Wait();
            }
            catch
            { }

            System.Console.WriteLine("Hello World!");
        }
    }
}
