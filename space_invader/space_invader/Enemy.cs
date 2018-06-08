using System;
using System.Collections.Generic;
using System.Xml;
using Otter;

namespace space_invader
{
    /// <summary>
    /// Class that handles enemies.
    /// </summary>
    class Enemy : Entity
    {
        static float EnemySize = 32.0f;
        static float MoveSpeed = 0.8f;
        static Vector2 MoveDir = new Vector2(MoveSpeed, 0.0f);
        static Vector2 NextMoveDir;
        static IDictionary<string, Func<Enemy>> AllEnemies = new Dictionary<string, Func<Enemy>>(); //https://codereview.stackexchange.com/questions/4174/better-way-to-create-objects-from-strings
        static Random rnd = new Random();
        static AutoTimer ShootingCooldown = new AutoTimer(rnd.Next(700, 1500));
        static float HeightToMove = 24.0f;
        public static bool hasMoved = false;
        static Image enemyBullet = new Image("Assets/enemyBullet.png");

        public int Score;
        
        /// <summary>
        /// Initializes new enemy.
        /// </summary>
        public Enemy()
        {
            BoxCollider collider = new BoxCollider(24, 24, Tags.Enemy);
            AddCollider(collider);
            ShootingCooldown.Start();
        }

        /// <summary>
        /// Loads enemies.
        /// </summary>
        public static void Initialize()
        {
            InitializeAllEnemies();
            LoadEnemies("level1.xml");
        }

        /// <summary>
        /// Adds enemies to list so they can be loaded.
        /// </summary>
        public static void InitializeAllEnemies()
        {
            AllEnemies.Add("squid", () => { return new Squid(); });
            AllEnemies.Add("crab", () => { return new Crab(); });
            AllEnemies.Add("octopus", () => { return new Octopus(); });
        }

        /// <summary>
        /// Handles enemy movement.
        /// </summary>
        void UpdateMovement()
        {
            MainScene scene = Program.game.GetScene<MainScene>();
            List<Enemy> enemies = Scene.GetEntities<Enemy>();

            SetPosition(Position + MoveDir);

            // Check if enemy has moved 24p in Y axis, if true moves them in the X axis
            if (HeightToMove <= 0)
            {
                MoveDir = NextMoveDir;
                HeightToMove = 24;
                hasMoved = true;
            }

            // Check if enemy is on the right side of the screen, if true moves them lower
            if (Position.X > scene.GetPlayArea().X)
            {
                SetPosition(Position - MoveDir);
                MoveDir = new Vector2(0.0f, MoveSpeed);
                NextMoveDir = new Vector2(-1.0f * MoveSpeed, 0.0f);
            }

            // Check if enemy is on the left side of the screen, if true moves them lower
            if (Position.X < scene.PlayPosition.X)
            {
                SetPosition(Position - MoveDir);
                MoveDir = new Vector2(0.0f, MoveSpeed);
                NextMoveDir = new Vector2(1.0f * MoveSpeed, 0.0f);
            }

            // Check if enemy is in the end of the level, if true ends game
            if (Position.Y >= scene.GetPlayArea().Y - 100)
                Game.SwitchScene(new HighScoresScene());

            // Moves enemy
            HeightToMove -= MoveDir.Y / enemies.Count;
        }

        /// <summary>
        /// Update shooting.
        /// </summary>
        void UpdateShooting()
        {
            // Check if ShootingCooldown is at max
            if (ShootingCooldown.AtMax)
            {
                ShootingCooldown.Stop();

                // Chooses the enemy that shoots
                List<Enemy> enemies = Scene.GetEntities<Enemy>();
                int EnemyNumber = rnd.Next(1, enemies.Count);

                // Create bullet
                BoxCollider collider = new BoxCollider(enemyBullet.Width, enemyBullet.Height, Tags.Enemy);
                Bullet bullet = new Bullet(6.0f, enemies[EnemyNumber - 1].Position, collider);
                bullet.AddGraphic(enemyBullet);
                Scene.Add(bullet);

                // Reset ShootingCooldown
                ShootingCooldown.Max = rnd.Next(2000, 5000);
                ShootingCooldown.Start();
            }
        }

        /// <summary>
        /// Update.
        /// </summary>
        public override void Update()
        {
            base.Update();

            UpdateShooting();
            UpdateMovement();
            ShootingCooldown.Update();
        }

        /// <summary>
        /// Load enemies from file.
        /// Loads from "/levels/", you only need to put "level1.xml"
        /// </summary>
        /// <param name="file">file name to load</param>
        public static void LoadEnemies(string file)
        {
            MainScene scene = Program.game.GetScene<MainScene>();

            // Open document
            XmlDocument doc = new XmlDocument();
            doc.Load("levels/" + file);

            // Current enemy position to load
            Vector2 CurPos = new Vector2(scene.PlayPosition.X, scene.PlayPosition.Y);

            foreach (XmlElement node in doc.DocumentElement.ChildNodes)
            {
                // Create new enemy and add to scene
                Enemy enemy = AllEnemies[node.GetAttribute("type")]();
                enemy.Position = CurPos;
                scene.Add(enemy);

                // Set enemy position
                CurPos.X += EnemySize;
                if (CurPos.X > 420)
                {
                    CurPos.X = scene.PlayPosition.X;
                    CurPos.Y += EnemySize;
                }
            }
        }
    }
}
