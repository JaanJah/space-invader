using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;

namespace space_invader
{
    class Program
    {
        static void Main(string[] args)
        {
            // Creates game window
            var game = new Game("Space Invader", 800, 600, 60, false);

            // Creates game scene
            Scene scene = new Scene();

            // Starts the game
            game.Start();

        }
    }
}
