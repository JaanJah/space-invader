﻿using System;
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
            game.FirstScene = scene;
            scene.Initialize();

            //Change window variables
            game.WindowResize = false;
            game.SetIcon("../../../Assets/enemy1.png");
            // Game background
            var background = new Image("../../../Assets/background.png");
            background.Alpha = 0.4f;
            scene.AddGraphic(background);
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
            var livesLeftTxtLabel = new RichText("Lives", txtConfig);
            livesLeftTxtLabel.SetPosition(50, 16);

            scene.livesLeftTxt = new RichText(scene.player.playerLives.ToString(), txtConfig);
            scene.livesLeftTxt.Name = "livesLeftTxt";
            scene.livesLeftTxt.SetPosition(70, 32);

            var highScoreTxtLabel = new RichText("Highscore",txtConfig);
            highScoreTxtLabel.SetPosition(350, 15);

            var highScoreTxt = new RichText(ReadXML.MainScreenXML(),txtConfig);
            highScoreTxt.Name = "highScoreTxt";
            highScoreTxt.SetPosition(380, 30);

            var curScoreTxtLabel = new RichText("Score", txtConfig);
            curScoreTxtLabel.SetPosition(650, 15);

            scene.curScoreTxt = new RichText(scene.player.ScoreAmount.ToString(), txtConfig);
            scene.curScoreTxt.Name = "curScoreTxt";
            scene.curScoreTxt.SetPosition(670, 32);
            // Adds Graphic to Scene
            scene.AddGraphic(livesLeftTxtLabel);
            scene.AddGraphic(highScoreTxtLabel);
            scene.AddGraphic(curScoreTxtLabel);

            scene.AddGraphic(scene.livesLeftTxt);
            scene.AddGraphic(highScoreTxt);
            scene.AddGraphic(scene.curScoreTxt);

            #endregion gameText

            // Starts the game
            game.Start();
        }
    }
}