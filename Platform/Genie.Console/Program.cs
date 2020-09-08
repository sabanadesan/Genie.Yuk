using System;

using Genie.Yuk;

namespace Genie3D.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game(GraphicsBackend.Vulkan, "Output");
            game.Run();

            System.Console.WriteLine("Hello World!");
        }
    }
}
