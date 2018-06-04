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
        public static MainScene scene;
        Enemy EnemyRight;
        Enemy EnemyLeft;
        Enemy EnemyBottom;

        public Enemy()
        {

        }

        void UpdateMovement()
        {
            
        }

        public override void Update()
        {
            base.Update();


        }

        void FindEnemies()
        {
            List<Enemy> enemies = scene.GetEntities<Enemy>();
            Enemy curEnemy = enemies[0];

            foreach(Enemy enemy in enemies)
            {
                //if (cur)
            }

        }
    }
}
