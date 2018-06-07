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
        public HighScoreLeaderboard hslb;

        // Stuff in the HighScores Scene
        public HighScoresScene() : base()
        {
            #region HighScoreWalls
            Image otherWall = Image.CreateRectangle(1, 350);
            otherWall.SetPosition(549, 150);
            otherWall.Color = Color.Blue;
            AddGraphic(otherWall);

            Image bottomWall = Image.CreateRectangle(300, 1);
            bottomWall.SetPosition(250, 500);
            bottomWall.Color = Color.Blue;
            AddGraphic(bottomWall);
            #endregion

            var scene = new Scene();
            Program.game.MouseVisible = true;
            scene.Add(new TextBox(250, 100));
            scene.Add(new Button(420, 95));

            hslb = new HighScoreLeaderboard(250, 150);
            scene.Add(hslb);

            Program.game.SwitchScene(scene);
        }
    }
}
