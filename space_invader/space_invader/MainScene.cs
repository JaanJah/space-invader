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
        public Vector2 PlayPosition = new Vector2(20, 20);
        public Vector2 PlayWidth = new Vector2(700, 500);
        public Player player;
        List<Enemy> Enemies;
        public List<Texture> textures;

        public MainScene()
        {
            Enemy.scene = this;
            LoadEnemies("level1.xml");

            // Create player and add to scene
            player = new Player(this);
            Add(player);
        }

        //Update scene
        public override void Update()
        {
            base.Update();

            Enemy.FindEnemies();

            //Debug - Switches Scene if input is H
            if (Input.KeyPressed(Key.H))
            {
                Game.SwitchScene(new HighScoresScene());
            }
        }

        void LoadEnemies(string file)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("../../../levels/" + file);
            Vector2 CurPos = new Vector2(PlayPosition.X, PlayPosition.Y);

            foreach(XmlElement node in doc.DocumentElement.ChildNodes)
            {
                Enemy enemy = new Enemy();
                
                enemy.AddGraphic(new Image("../../../textures/" + node.GetAttribute("texture")));
                enemy.Position = CurPos;

                Add(enemy);

                CurPos.X += Enemy.EnemySize;
                if (CurPos.X > 420)
                {
                    CurPos.X = PlayPosition.X;
                    CurPos.Y += Enemy.EnemySize;
                }
            }
        }
    }
}
