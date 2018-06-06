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
            Image otherWall = Image.CreateRectangle(1, 350);
            otherWall.SetPosition(550, 150);
            otherWall.Color = Color.Blue;
            AddGraphic(otherWall);

            Image bottomWall = Image.CreateRectangle(300, 1);
            bottomWall.SetPosition(250, 500);
            bottomWall.Color = Color.Blue;
            AddGraphic(bottomWall);

            var scene = new Scene();
            Program.game.MouseVisible = true;
            scene.Add(new TextBox(200, 100));
            scene.Add(new Button(400, 95));
            scene.Add(new HighScoreLeaderboard(250, 150));
            
            Program.game.AddScene(scene);
        }
    }
}
