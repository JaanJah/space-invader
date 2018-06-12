using Otter;
using System.Runtime.InteropServices;
namespace space_invader
{
    class Program
    {
        public static bool pressed = false;
        public static Game game;
        public static string curScoreTxt;
        //Hides console
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool FreeConsole();

        static void Main(string[] args)
        {
            // Creates game window
            game = new Game("Space Invader", 800, 600, 60, false);
            game.Color = Color.Black;
            FreeConsole();

            // Creates game scene
            MainScene scene = new MainScene();
            game.AddScene(scene);

            var a = game.Scenes;

            scene.Initialize();

            //Change window variables
            game.WindowResize = false;
            game.SetIcon("Assets/windowPicture.png");

            // Starts the game
            game.Start();
        }
    }
}