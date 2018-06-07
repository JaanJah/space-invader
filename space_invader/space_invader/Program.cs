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
        public static Game game;

        static void Main(string[] args)
        {
            // Creates game window
            game = new Game("Space Invader", 800, 600, 60, false);
            game.Color = Color.Black;

            // Creates game scene
            MainScene scene = new MainScene();
            game.AddScene(scene);

            var a = game.Scenes;

            scene.Initialize();

            //Change window variables
            game.WindowResize = false;
            game.SetIcon("../../../Assets/enemy1.png");

            // Starts the game
            game.Start();
        }
    }
}