﻿using System;
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
        Barricade,
        Ufo
    }

    class Bullet : Entity
    {
        static int playerLives = 3;

        public float MoveSpeed;

        public Bullet(float _MoveSpeed, Vector2 pos, BoxCollider collider)
        {
            Position = pos;
            MoveSpeed = _MoveSpeed;

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
            if (Collider.Tags[0] == (int)Tags.Player)
                if (Position.Y < 0)
                {
                    Visible = false;
                    Collidable = false;
                }

            // Check if enemy bullet is out of bounds and removes it
            if (Collider.Tags[0] == (int)Tags.Enemy)
                if (Position.Y > Game.Height)
                    RemoveSelf();
        }

        void CheckBulletCollision()
        {
            MainScene scene = (MainScene)Program.game.FirstScene;

            CheckPlayerBullet(scene);
            CheckEnemyBullet(scene);
            CheckBarricade(scene);
            CheckUfo(scene);
        }

        void CheckPlayerBullet(MainScene scene)
        {
            if (Collider.Tags[0] == (int)Tags.Player)
                if (Collider.CollideEntities(X, Y, Tags.Enemy).Count > 0)
                    // Bullet hits bullet
                    if ((Collider.CollideEntities(X, Y, Tags.Enemy)[0].GetType() == typeof(Bullet)))
                    {
                        Collider.CollideEntities(X, Y, Tags.Enemy)[0].RemoveSelf();
                        Visible = false;
                        Collidable = false;
                    }
                    // Bullet hits enemy
                    else
                    {
                        Enemy enemy = (Enemy)Collider.CollideEntities(X, Y, Tags.Enemy)[0];

                        enemy.RemoveSelf();
                        Visible = false;
                        Collidable = false;

                        if (scene.GetEntities<Enemy>().Count <= 1)
                            scene.NextLevel();

                        scene.player.ScoreAmount += enemy.Score;
                        scene.curScoreTxt.String = scene.player.ScoreAmount.ToString();
                        scene.curScoreTxt.Refresh();

                        Console.WriteLine(scene.player.ScoreAmount);
                    }
        }

        void CheckEnemyBullet(MainScene scene)
        {
            if (Collider.Tags[0] == (int)Tags.Enemy)
                if (Collider.CollideEntities(X, Y, Tags.Player).Count > 0)
                    if (!(Collider.CollideEntities(X, Y, Tags.Player)[0].GetType() == typeof(Bullet)))
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
        }

        void CheckBarricade(MainScene scene)
        {
            if (Collider.CollideEntity(X, Y, Tags.Barricade) != null)
            {
                Barricade barricade = (Barricade)Collider.CollideEntity(X, Y, Tags.Barricade);

                if (Collider.Tags[0] == (int)Tags.Player)
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

        void CheckUfo(MainScene scene)
        {
            if (Collider.CollideEntity(X, Y, Tags.Ufo) != null)
            {
                UFO ufo = (UFO)Collider.CollideEntity(X, Y, Tags.Ufo);

                ufo.Die();
                Visible = false;
                Collidable = false;
                scene.player.ScoreAmount += ufo.Score;
            }
        }

    }
}
