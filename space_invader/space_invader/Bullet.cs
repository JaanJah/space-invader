using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;

namespace space_invader
{
    enum Tags
    {
        Player,
        Enemy
    }

    class Bullet : Entity
    {
        public float MoveSpeed;
        static MainScene scene;
        BoxCollider collider;

        static int playerLives = 3;

        public Bullet(MainScene _scene, float _MoveSpeed, Vector2 pos, BoxCollider _collider)

        {
            scene = _scene;
            Position = pos;
            MoveSpeed = _MoveSpeed;
            collider = _collider;

            AddCollider(collider);
        }

        public override void Update()
        {
            base.Update();

<<<<<<< HEAD
            var txtConfig = new RichTextConfig()
            {
                TextAlign = TextAlign.Center,
                CharColor = Color.Green,
                FontSize = 16,
                SineAmpX = 3,
                SineAmpY = 2,
                SineRateX = 1,
            };

            Y += MoveSpeed;

            if (collider.Tags[0] == (int)Tags.Player)
                if (collider.CollideEntities(X, Y, Tags.Enemy).Count > 0)
                {
                    collider.CollideEntities(X, Y, Tags.Enemy)[0].RemoveSelf();
                    RemoveSelf();
                    scene.player.ScoreAmount += 10;
                    //debug
                    Console.WriteLine("Score amount: {0}",scene.player.ScoreAmount);
                }

            if (collider.Tags[0] == (int)Tags.Enemy)
                if (collider.CollideEntities(X, Y, Tags.Player).Count > 0)
                {
                    RemoveSelf();
                    scene.player.playerLives -= 1;
                    scene.player.SetPosition(new Vector2(scene.PlayPosition.X + scene.PlayWidth.X,
                    scene.PlayPosition.Y + scene.PlayWidth.Y));
                    //debug
                    Console.WriteLine("Lives left: {0}",scene.player.playerLives);

                    scene.livesLeftTxt.Visible = false;

                    var livesLeftTxt = new RichText(scene.player.playerLives.ToString(), txtConfig);
                    livesLeftTxt.Name = "livesLeftTxt";
                    livesLeftTxt.SetPosition(70, 32);
                    livesLeftTxt.Refresh();
                    livesLeftTxt.String = "te";
                    scene.AddGraphic(livesLeftTxt);
                }
=======
            if (Visible)
            {
                Y += MoveSpeed;

                if (collider.Tags[0] == (int)Tags.Player)
                    if (Position.Y < 0)
                    {
                        Visible = false;
                        Collidable = false;
                        scene.player.ScoreAmount += 10;
                        Console.WriteLine("Score amount: {0}", scene.player.ScoreAmount);
                    }

                if (collider.Tags[0] == (int)Tags.Enemy)
                    if (Position.Y > Game.Height)
                        RemoveSelf();

                if (collider.Tags[0] == (int)Tags.Player)
                    if (collider.CollideEntities(X, Y, Tags.Enemy).Count > 0)
                    {
                        collider.CollideEntities(X, Y, Tags.Enemy)[0].RemoveSelf();
                        Visible = false;
                        Collidable = false;
                    }
                if (playerLives == 0)
                    Game.SwitchScene(new HighScoresScene());

                if (collider.Tags[0] == (int)Tags.Enemy)
                    if (collider.CollideEntities(X, Y, Tags.Player).Count > 0)
                    {
                        Enemy.FindEnemies();
                        RemoveSelf();
                        scene.player.playerLives -= 1;
                        scene.player.SetPosition(new Vector2(scene.PlayPosition.X + scene.PlayWidth.X,
                        scene.PlayPosition.Y + scene.PlayWidth.Y));
                        //debug
                        Console.WriteLine("Lives left: {0}", scene.player.playerLives);
                    }
            }
>>>>>>> 6baf0e3c2f3feec73adcfea3d9b8474da4d88a9e
        }
    }
}
