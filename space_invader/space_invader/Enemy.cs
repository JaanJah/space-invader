using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;

namespace space_invader
{
    class Enemy : Entity
    {
        public static float EnemySize = 32.0f;
        static float MoveSpeed = 0.005f;
        public static MainScene scene;
        public static Enemy EnemyRight;
        public static Enemy EnemyLeft;
        public static Enemy EnemyBottom;
        public static float HeightCheck = 1000;
        public static Vector2 MoveDir = new Vector2(MoveSpeed, 0.0f);
        public static Vector2 NextDir;

        BoxCollider collider = new BoxCollider(24, 24, Tags.Enemy);
        AutoTimer ShootingCooldown;
        Random rnd = new Random();

        public Enemy()
        {
            ShootingCooldown = new AutoTimer(rnd.Next(100, 300));
            ShootingCooldown.Start();

            AddCollider(collider);
        }

        void UpdateMovement()
        {
            List<Enemy> enemies = scene.GetEntities<Enemy>();

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

            //if (EnemyBottom.Y > scene.player.Y)
                //End game
        }

        void UpdateShooting()
        {
            if (ShootingCooldown.AtMax)
            {
                Image enemyBullet = new Image("../../../Assets/enemyBullet.png");

                List<Enemy> enemies = scene.GetEntities<Enemy>();

                int EnemyNumber = rnd.Next(1, enemies.Count);

                BoxCollider collider = new BoxCollider(enemyBullet.Width, enemyBullet.Height, Tags.Enemy);
                Bullet bullet = new Bullet(scene, 3.0f, enemies[EnemyNumber].Position, collider);
                bullet.AddGraphic(enemyBullet);

                scene.Add(bullet);

                ShootingCooldown.Max = rnd.Next(100, 250);
                ShootingCooldown.Reset();
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
            List<Enemy> enemies = scene.GetEntities<Enemy>();
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
    }
}
