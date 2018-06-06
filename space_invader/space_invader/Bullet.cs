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
        Enemy,
        Barricade
    }

    class Bullet : Entity
    {
        public float MoveSpeed;
        BoxCollider collider;
        
        static int playerLives = 3;

        public Bullet(float _MoveSpeed, Vector2 pos, BoxCollider _collider)

        {
            Position = pos;
            MoveSpeed = _MoveSpeed;
            collider = _collider;

            AddCollider(collider);
        }

        public override void Update()
        {
            base.Update();

            if (Visible)
            {
                Y += MoveSpeed;

                CheckBulletBounds();
                CheckBulletCollision();
                
                // Check if player lives is 0, if true end game
                if (playerLives == 0)
                    Game.SwitchScene(new HighScoresScene());
            }
        }

        void CheckBulletBounds()
        {
            // Check if player bullet is out of bounds and removes it
            if (collider.Tags[0] == (int)Tags.Player)
                if (Position.Y < 0)
                {
                    Visible = false;
                    Collidable = false;
                }

            // Check if enemy bullet is out of bounds and removes it
            if (collider.Tags[0] == (int)Tags.Enemy)
                if (Position.Y > Game.Height)
                    RemoveSelf();
        }

        void CheckBulletCollision()
        {
            MainScene scene = (MainScene)Program.game.FirstScene;

            // Check if player bullet hits enemy
            if (collider.Tags[0] == (int)Tags.Player)
                if (collider.CollideEntities(X, Y, Tags.Enemy).Count > 0)
                    if ((collider.CollideEntities(X, Y, Tags.Enemy)[0].GetType() == typeof(Bullet)))
                    {
                        collider.CollideEntities(X, Y, Tags.Enemy)[0].RemoveSelf();
                        Visible = false;
                        Collidable = false;
<<<<<<< HEAD
=======
                        
                        scene.player.ScoreAmount += 10;
                        scene.curScoreTxt.String = scene.player.ScoreAmount.ToString();
                        scene.curScoreTxt.Refresh();
>>>>>>> master
                    }
                    else
                    {
                        collider.CollideEntities(X, Y, Tags.Enemy)[0].RemoveSelf();
                        Visible = false;
                        Collidable = false;
                        Console.WriteLine(scene.GetEntities<Enemy>().Count);
                        
                        if (scene.GetEntities<Enemy>().Count <= 1)
                            scene.NextLevel();
                        scene.player.ScoreAmount += 10;
                        scene.curScoreTxt.String = scene.player.ScoreAmount.ToString();
                        scene.curScoreTxt.Refresh();
                    }

            // Check if enemy bullet hits player
            if (collider.Tags[0] == (int)Tags.Enemy)
                if (collider.CollideEntities(X, Y, Tags.Player).Count > 0)
                    if (!(collider.CollideEntities(X, Y, Tags.Player)[0].GetType() == typeof(Bullet)))
                    {
                        RemoveSelf();
                        scene.player.playerLives -= 1;
                        scene.player.SetPosition(new Vector2(scene.PlayPosition.X + scene.PlayWidth.X,
                        scene.PlayPosition.Y + scene.PlayWidth.Y));

                        scene.livesLeftTxt.String = scene.player.playerLives.ToString();
                        scene.livesLeftTxt.Refresh();
                    }
                    else
                    {
                        RemoveSelf();
                        scene.player.bullet.Visible = false;
                        scene.player.bullet.Collidable = false;
                    }

            //Check if bullet hits barricade
            if (collider.CollideEntity(X, Y, Tags.Barricade) != null)
            {
                Barricade barricade = (Barricade)collider.CollideEntity(X, Y, Tags.Barricade);

                if (collider.Tags[0] == (int)Tags.Player)
                {
                    barricade.TakeDamage();
                    Visible = false;
                    Collidable = false;
                }
                else
                {
                    barricade.TakeDamage();
                    RemoveSelf();
                }
            }
        }
    }
}
