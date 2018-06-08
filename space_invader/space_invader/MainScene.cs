using Otter;

namespace space_invader
{
    /// <summary>
    /// Main scene for the game
    /// </summary>
    class MainScene : Scene
    {
        //Sets basic variables
        public Vector2 PlayPosition = new Vector2(20, 60);
        public Vector2 PlayWidth = new Vector2(736, 500);
        public Player player;
        public RichText livesLeftTxt;
        public RichText curScoreTxt;
        public RichText highScoreTxt;
        UFO Ufo;
        public int CurLevel = 1;

        public MainScene()
        {
            
        }

        /// <summary>
        /// Called to initialize the game
        /// Note: must be added to Game.FirstScene before calling
        /// </summary>
        public void Initialize()
        {
            OnBegin = delegate
            {
                Enemy.Initialize();
                Barricade.Initialize();
                MainScene scene = Program.game.GetScene<MainScene>();

                Ufo = new UFO();

                Program.game.GetScene<MainScene>().Add(Ufo);

                // Create player and add to scene
                player = new Player();
                Add(player);
                // Gametext for the entire game pretty much
                #region gameText

                var background = new Image("../../../Assets/background.png");
                background.Alpha = 0.4f;
                scene.AddGraphic(background);

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

                var highScoreTxtLabel = new RichText("Highscore", txtConfig);
                highScoreTxtLabel.SetPosition(350, 15);
                
                highScoreTxt = new RichText(ReadXML.MainScreenXML(), txtConfig);
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
                scene.AddGraphic(scene.highScoreTxt);
                scene.AddGraphic(scene.curScoreTxt);

                #endregion gameText
            };   
        }

        //Update scene
        
        public override void Update()
        {
            base.Update();

            //Debug - Switches Scene if input is H
            if (Input.KeyPressed(Key.H))
            {
                Game.SwitchScene(new HighScoresScene());
            }
        }

        /// <summary>
        /// Sets next level, if CurLevel == 6 then ends the game
        /// </summary>
        public void NextLevel()
        {
            CurLevel++;

            if (CurLevel == 5)
                Game.SwitchScene(new HighScoresScene());

            Enemy.LoadEnemies("level" + CurLevel.ToString() + ".xml");
        }
        //Gets play area
        public Vector2 GetPlayArea()
        {
            return PlayPosition + PlayWidth;
        }
    }
}
