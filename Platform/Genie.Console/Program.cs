using System;

using Genie.Yuk;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Genie3D.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var tasks = new List<Task>();

            Game game = new Game("Output");
            //game.Run();

            GameGraphics gg = new GameGraphics();

            Process BackgroundWorker = new Process("BackgroundWorker");
            Task t = BackgroundWorker.Run(gg.Run);
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
