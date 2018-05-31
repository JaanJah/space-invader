using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;

namespace space_invader
{
    /// <summary>
    /// Main scene for the game
    /// </summary>
    class MainScene : Scene
    {
        public Vector2 PlayPosition = new Vector2(20, 20);
        public Vector2 PlayWidth = new Vector2(700, 500);
        public Player player;

        public MainScene()
        {

            // Create player and add to scene
            player = new Player(this);
            this.Add(player);
        }

        
    }
}
