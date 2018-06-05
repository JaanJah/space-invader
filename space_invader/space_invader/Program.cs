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

            // Creates game scene
            MainScene scene = new MainScene();



            // Writing the text graphics and setting position
            var livesLeftTxtLabel = new RichText("Lives", txtConfig);
            scene.livesLeftTxt = new RichText(scene.player.playerLives.ToString(), txtConfig);
            scene.livesLeftTxt.Name = "livesLeftTxt";
            livesLeftTxtLabel.SetPosition(50, 16);
            scene.livesLeftTxt.SetPosition(70, 32);
            var highScoreTxtLabel = new RichText("Highscore",txtConfig);
            highScoreTxtLabel.SetPosition(350, 15);
            var curScoreTxtLabel = new RichText("Score", txtConfig);
            curScoreTxtLabel.SetPosition(650, 15);
            // Adds Graphic to Scene
            scene.AddGraphic(livesLeftTxtLabel);
            scene.AddGraphic(scene.livesLeftTxt);
            scene.AddGraphic(highScoreTxtLabel);
            scene.AddGraphic(curScoreTxtLabel);
            #endregion gameText

            // Starts the game
            game.Start(scene);
        }
    }
}
