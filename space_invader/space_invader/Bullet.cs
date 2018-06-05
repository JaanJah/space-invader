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

            if (Visible)
            {
                Y += MoveSpeed;

                // Check if player bullet is out of bounds and removes it
                if (collider.Tags[0] == (int)Tags.Player)
                    if (Position.Y < 0)
                    {
                        Visible = false;
                        Collidable = false;
                        scene.player.ScoreAmount += 10;
                    }

                // Check if enemy bullet is out of bounds and removes it
                if (collider.Tags[0] == (int)Tags.Enemy)
                    if (Position.Y > Game.Height)
                        RemoveSelf();

                // Check if player bullet hits enemy
                if (collider.Tags[0] == (int)Tags.Player)
                    if (collider.CollideEntities(X, Y, Tags.Enemy).Count > 0)
                        if (!(collider.CollideEntities(X, Y, Tags.Enemy)[0].GetType() == typeof(Bullet)))
                        {
                            collider.CollideEntities(X, Y, Tags.Enemy)[0].RemoveSelf();
                            Visible = false;
                            Collidable = false;
                        }

                // Check if player lives is 0, if true end game
                if (playerLives == 0)
                    Game.SwitchScene(new HighScoresScene());

                // Check if enemy bullet hits player
                if (collider.Tags[0] == (int)Tags.Enemy)
                    if (collider.CollideEntities(X, Y, Tags.Player).Count > 0)
                        if (!(collider.CollideEntities(X, Y, Tags.Player)[0].GetType() == typeof(Bullet)))
                        {
                            Enemy.FindEnemies();
                            RemoveSelf();
                            scene.player.playerLives -= 1;
                            scene.player.SetPosition(new Vector2(scene.PlayPosition.X + scene.PlayWidth.X,
                            scene.PlayPosition.Y + scene.PlayWidth.Y));

                            scene.livesLeftTxt.String = scene.player.playerLives.ToString();
                            scene.livesLeftTxt.Refresh();
                        }
            }
        }
    }
}
