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

            Game game = new Game("Output");
            //game.Run();

            GameGraphics gg = new GameGraphics();
            gg.Thread();

            EventQueue.Enqueue(new GraphicsEvent());


            var tasks = new List<Task>();
            Action<object> action = (object obj) =>
            {
                //Thread.Sleep(5000);
                Process p = ProcessServer.Resolve("GraphicsWorker");
                //p.Stop();
                p.Wait();

            };

            Task t2 = new Task(action, "Wait");
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
    }
}
