using System;

using Genie.Yuk;

namespace Genie.Make
{
    public class Script
    {
        public Script()
        {
            PlayerComponent player = new PlayerComponent();
        }
    }

    public class PlayerComponent : Component
    {
        // Use this for initialization
        public override void Start()
        {
            System.Console.WriteLine("Player 1 Created");
        }

        public override void Update()
        {
            if (this.position.X < 10)
            {
                this.position.X += 1;

                System.Console.WriteLine(this.position.X);
            }

        }

        ~PlayerComponent()  // finalizer
        {
            // cleanup statements...
        }
    }
}
