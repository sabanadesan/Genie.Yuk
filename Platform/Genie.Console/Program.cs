using System;

using Genie.Yuk;

using System.Threading.Tasks;

namespace Genie3D.Console
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Game game = new Game(GraphicsBackend.Vulkan, "Output");
            game.Run();

            System.Console.WriteLine("Hello World!");
        }
    }
}
