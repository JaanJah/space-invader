﻿using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Otter;

namespace space_invader
{
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
        static int hasMoved = 0;

        public int Score;
        

        public Enemy()
        {
            BoxCollider collider = new BoxCollider(24, 24, Tags.Enemy);
            AddCollider(collider);
            ShootingCooldown.Start();
            

            
        }

        public static void Initialize()
        {
            InitializeAllEnemies();
            LoadEnemies("level1.xml");
        }

        public static void InitializeAllEnemies()
        {
            AllEnemies.Add("squid", () => { return new Squid(); });
            AllEnemies.Add("crab", () => { return new Crab(); });
            AllEnemies.Add("octopus", () => { return new Octopus(); });
        }

        void UpdateMovement()
        {
            MainScene scene = (MainScene)Program.game.FirstScene;
            List<Enemy> enemies = Scene.GetEntities<Enemy>();

            SetPosition(Position + MoveDir);

            if (HeightToMove <= 0)
            {
                MoveDir = NextMoveDir;
                HeightToMove = 24;
            }

            if (Position.X > scene.GetPlayArea().X)
            {
                SetPosition(Position - MoveDir);
                MoveDir = new Vector2(0.0f, MoveSpeed);
                NextMoveDir = new Vector2(-1.0f * MoveSpeed, 0.0f);
            }

            if (Position.X < scene.PlayPosition.X)
            {
                SetPosition(Position - MoveDir);
                MoveDir = new Vector2(0.0f, MoveSpeed);
                NextMoveDir = new Vector2(1.0f * MoveSpeed, 0.0f);
            }

            if (Position.Y >= scene.GetPlayArea().Y - 100)
                Game.SwitchScene(new HighScoresScene());

            HeightToMove -= MoveDir.Y / enemies.Count;
        }

        void UpdateShooting()
        {
            if (ShootingCooldown.AtMax)
            {
                ShootingCooldown.Stop();

                Image enemyBullet = new Image("../../../Assets/enemyBullet.png");

                List<Enemy> enemies = Scene.GetEntities<Enemy>();

                int EnemyNumber = rnd.Next(1, enemies.Count);

                BoxCollider collider = new BoxCollider(enemyBullet.Width, enemyBullet.Height, Tags.Enemy);
                Bullet bullet = new Bullet(6.0f, enemies[EnemyNumber - 1].Position, collider);
                bullet.AddGraphic(enemyBullet);

                Scene.Add(bullet);

                ShootingCooldown.Max = rnd.Next(2000, 5000);
                ShootingCooldown.Start();
            }
        }

        public override void Update()
        {
            base.Update();

            UpdateShooting();
            UpdateMovement();
            ShootingCooldown.Update();
        }

        public static void LoadEnemies(string file)
        {
            MainScene scene = (MainScene)Program.game.FirstScene;
            XmlDocument doc = new XmlDocument();
            doc.Load("../../../levels/" + file);
            Vector2 CurPos = new Vector2(scene.PlayPosition.X, scene.PlayPosition.Y);

            foreach (XmlElement node in doc.DocumentElement.ChildNodes)
            {
                Enemy enemy = AllEnemies[node.GetAttribute("type")]();
                enemy.Position = CurPos;

                scene.Add(enemy);

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
