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
            Scene scene = new MainScene();

            #region gameText
            //Setting a default config file for the RichText to use
            var txtConfig = new RichTextConfig()
            {
                TextAlign = TextAlign.Center,
                CharColor = Color.Green,
                FontSize = 16,
                SineAmpX = 3,
                SineAmpY = 2,
                SineRateX = 1,
            };
            // Writing the text graphics and setting position
            var livesLeftTxt = new RichText("Lives", txtConfig);
            livesLeftTxt.SetPosition(50, 16);
            var highScoreTxt = new RichText("Highscore",txtConfig);
            highScoreTxt.SetPosition(350, 15);
            var curScoreTxt = new RichText("Score", txtConfig);
            curScoreTxt.SetPosition(650, 15);
            // Adds Graphic to Scene
            scene.AddGraphic(livesLeftTxt);
            scene.AddGraphic(highScoreTxt);
            scene.AddGraphic(curScoreTxt);
            #endregion gameText

            // Starts the game
            game.Start(scene);
        }
    }
}
