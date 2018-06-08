using System;
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

    /// <summary>
    /// Class that handles bullets.
    /// </summary>
    class Bullet : Entity
    {
        static int playerLives = 3;

        public float MoveSpeed;

        /// <summary>
        /// Creates new bullet
        /// </summary>
        /// <param name="_MoveSpeed">Bullet speed. You can use negative values to move up and down</param>
        /// <param name="pos">Bullet position</param>
        /// <param name="collider">collider</param>
        public Bullet(float _MoveSpeed, Vector2 pos, BoxCollider collider)
        {
            Position = pos;
            MoveSpeed = _MoveSpeed;

            AddCollider(collider);
        }

        /// <summary>
        /// Update bullet.
        /// </summary>
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

        /// <summary>
        /// Check if bullet is in game bounds and removes it if false.
        /// </summary>
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

        /// <summary>
        /// Check if bullet collides with entities.
        /// </summary>
        void CheckBulletCollision()
        {
            MainScene scene = Program.game.GetScene<MainScene>();
            
            CheckPlayerBullet(scene);
            CheckEnemyBullet(scene);
            CheckBarricade(scene);
            CheckUfo(scene);
        }

        /// <summary>
        /// Check if player bullet collides with entities.
        /// </summary>
        /// <param name="scene">scene</param>
        void CheckPlayerBullet(MainScene scene)
        {
            // Check if bullet is player bullet
            if (Collider.Tags[0] == (int)Tags.Player)
                // Check if bullet collides with player tags
                if (Collider.CollideEntity(X, Y, Tags.Enemy) != null)
                    // Check if bullet hits bullet
                    if ((Collider.CollideEntities(X, Y, Tags.Enemy)[0].GetType() == typeof(Bullet)))
                    {
                        Collider.CollideEntities(X, Y, Tags.Enemy)[0].RemoveSelf();
                        Visible = false;
                        Collidable = false;
                    }
                    // Check if bullet hits enemy
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
                        Program.curScoreTxt = scene.player.ScoreAmount.ToString();
                        scene.curScoreTxt.Refresh();

                    }
        }


        /// <summary>
        /// Check if enemy bullet collides with entities
        /// </summary>
        /// <param name="scene">scene</param>
        void CheckEnemyBullet(MainScene scene)
        {
            // Check if bullet is enemy bullet
            if (Collider.Tags[0] == (int)Tags.Enemy)
            {
                // Check if bullet collides with player tags
                if (Collider.CollideEntity(X, Y, Tags.Player) != null)
                {
                    // Check if bullet collides with player
                    if (!(Collider.CollideEntities(X, Y, Tags.Player)[0].GetType() == typeof(Bullet)))
                        if (scene.player.Alive)
                        {
                            RemoveSelf();
                            scene.player.playerLives -= 1;

                            scene.player.Die();

                            scene.livesLeftTxt.String = scene.player.playerLives.ToString();
                            scene.livesLeftTxt.Refresh();
                        }

                    // Check if enemy bullet collides with player bullet
                        else
                            RemoveSelf();
                }
            }
                
                    
        }

        /// <summary>
        /// Check if bullet collides with barricade.
        /// </summary>
        /// <param name="scene">scene</param>
        void CheckBarricade(MainScene scene)
        {
            Barricade barricade = (Barricade)Collider.CollideEntity(X, Y, Tags.Barricade);

            if (barricade != null)
            {
                // Check if player bullet hits barricade
                if (Collider.Tags[0] == (int)Tags.Player)
                {
                    barricade.TakeDamage();
                    Visible = false;
                    Collidable = false;
                }

                // Check if enemy bullet hits barricade
                else
                {
                    barricade.TakeDamage();
                    RemoveSelf();
                }
            }
        }

        /// <summary>
        /// Check if bullet collides with ufo.
        /// </summary>
        /// <param name="scene">scene</param>
        void CheckUfo(MainScene scene)
        {
            UFO ufo = (UFO)Collider.CollideEntity(X, Y, Tags.Ufo);

            // If collidable is ufo then removes it
            if (ufo != null)
            {
                ufo.Die();
                scene.player.ScoreAmount += ufo.Score;
                scene.player.bullet.Visible = false;
                scene.player.bullet.Collidable = false;
            }
        }

    }
}
