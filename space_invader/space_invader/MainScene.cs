using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace space_invader
{
    /// <summary>
    /// Main scene for the game
    /// </summary>
    class MainScene : Scene
    {
        public Vector2 PlayPosition = new Vector2(20, 60);
        public Vector2 PlayWidth = new Vector2(736, 500);
        public Player player;
        public RichText livesLeftTxt;
        public RichText curScoreTxt;

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
            Barricade.Initialize();
            Enemy.Initialize();

            // Create player and add to scene
            player = new Player();
            Add(player);
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

            if (CurLevel == 6)
                Game.SwitchScene(new HighScoresScene());

            Enemy.LoadEnemies("level" + CurLevel.ToString() + ".xml");
        }

        public Vector2 GetPlayArea()
        {
            return PlayPosition + PlayWidth;
        }
    }
}
