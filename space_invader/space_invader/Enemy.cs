using System;
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
        public static float EnemySize = 32.0f;
        static float MoveSpeed = 0.005f;
        public static Enemy EnemyRight;
        public static Enemy EnemyLeft;
        public static Enemy EnemyBottom;
        public static float HeightCheck = 1000;
        public static Vector2 MoveDir = new Vector2(MoveSpeed, 0.0f);
        public static Vector2 NextDir;
        public static IDictionary<string, Func<Enemy>> AllEnemies = new Dictionary<string, Func<Enemy>>(); //https://codereview.stackexchange.com/questions/4174/better-way-to-create-objects-from-strings
        static Random rnd = new Random();
        static AutoTimer ShootingCooldown = new AutoTimer(rnd.Next(700, 1500));

        BoxCollider collider = new BoxCollider(24, 24, Tags.Enemy);

        public Enemy()
        {
            ShootingCooldown.Start();

            AddCollider(collider);
        }

        public static void Initialize()
        {
            InitializeAllEnemies();
            LoadEnemies("level1.xml");
        }

        public static void InitializeAllEnemies()
        {
            AllEnemies.Add("squid", () => { return new Squid(); });
        }

        void UpdateMovement()
        {
            MainScene scene = (MainScene)Program.game.FirstScene;
            List<Enemy> enemies = Scene.GetEntities<Enemy>();

            foreach (Enemy enemy in enemies)
                enemy.SetPosition(enemy.Position + MoveDir);

            if (EnemyRight.Position.X >= scene.PlayPosition.X + scene.PlayWidth.X &&
                MoveDir != new Vector2(0.0f, MoveSpeed))
            {
                MoveDir = new Vector2(0, MoveSpeed);
                NextDir = new Vector2(MoveSpeed * -1, 0);
                HeightCheck = EnemyBottom.Y + EnemySize;
            }

            if (EnemyLeft.Position.X <= scene.PlayPosition.X &&
                MoveDir != new Vector2(0.0f, MoveSpeed))
            {
                MoveDir = new Vector2(0, MoveSpeed);
                NextDir = new Vector2(MoveSpeed * 1, 0);
                HeightCheck = EnemyBottom.Y + EnemySize;
            }
                
            if (EnemyBottom.Y > HeightCheck)
                MoveDir = NextDir;

            if (EnemyBottom.Y > scene.player.Y)
                Game.SwitchScene(new HighScoresScene());
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
                Bullet bullet = new Bullet(2.0f, enemies[EnemyNumber].Position, collider);
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

        public static void FindEnemies()
        {
            List<Enemy> enemies = Program.game.FirstScene.GetEntities<Enemy>();
            EnemyRight = enemies[0];
            EnemyLeft = enemies[0];
            EnemyBottom = enemies[0];

            foreach(Enemy enemy in enemies)
            {
                // Find Rightmost enemy
                if (enemy.Position.X > EnemyRight.Position.X)
                    EnemyRight = enemy;

                // Find Leftmost enemy
                if (enemy.Position.X < EnemyLeft.Position.X)
                    EnemyLeft = enemy;

                // Find Bottommost enemy
                if (enemy.Position.Y > EnemyBottom.Position.Y)
                    EnemyBottom = enemy;
            }
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
