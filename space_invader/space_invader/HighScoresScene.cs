using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;

namespace space_invader
{
    /// <summary>
    /// Highscore Scene, happens after the game ends
    /// </summary>
    class HighScoresScene : Scene
    {
        public HighScoreLeaderboard hslb;

        // Stuff in the HighScores Scene
        public HighScoresScene() : base()
        {
            OnBegin = delegate
            {
                HighScoresScene _scene = Program.game.GetScene<HighScoresScene>();
                //Highscore walls
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
                //Adds Textbox and button and highscore leaderboard (hslb)
                Program.game.MouseVisible = true;
                _scene.Add(new TextBox(250, 100));
                _scene.Add(new Button(420, 95));

                hslb = new HighScoreLeaderboard(250, 150);
                _scene.Add(hslb);

                ReadXML.WriteScores();
            };
            Program.game.AddScene(this);
        }
    }
}
