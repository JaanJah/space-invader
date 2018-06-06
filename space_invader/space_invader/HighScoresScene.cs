using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;

namespace space_invader
{
    class HighScoresScene : Scene
    {
        // Stuff in the HighScores Scene
        public HighScoresScene() : base()
        {

            var scene = new Scene();
            Program.game.MouseVisible = true;
            scene.Add(new TextBox(200, 100));
            scene.Add(new Button(400, 95));
            Program.game.AddScene(scene);
            
        }
    }
}
