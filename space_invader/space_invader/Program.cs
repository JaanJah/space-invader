using Otter;
using System.Runtime.InteropServices;
namespace space_invader
{
    class Program
    {
        public static Game game;
        public static ResourceManager resourceManager;
        public static string curScoreTxt;

        static void Main(string[] args)
        {
            // Create game window
            game = new Game("Space Invader", 800, 600, 60, false);
            game.Color = Color.Black;

            // Set Asset filepath
            game.GameFolder = "../../../Assets";

            // Create resource manager
            resourceManager = new ResourceManager();

            //Initialize scene loader
            SceneLoader.Initialize();

            // Create game scene
            MainScene scene = new MainScene();
            game.AddScene(scene);
            scene.Initialize();

            // Change window variables
            game.WindowResize = false;
            // TODO: game.SetIcon("Assets/windowPicture.png");

            // Handle args
            foreach (var arg in args)
            {
                // Set Asset folder
                if (arg == "gf")
                    game.GameFolder = arg;
            }

            // Start game
            game.Start();
        }
    }
}