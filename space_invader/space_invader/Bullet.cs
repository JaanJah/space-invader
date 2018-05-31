using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;

namespace space_invader
{
    class Bullet : Entity
    {
        float MoveSpeed = 3.0f;
        bool IsEnemy;
        MainScene scene;

        public Bullet(MainScene _scene, bool _IsEnemy)
        {
            scene = _scene;
            IsEnemy = _IsEnemy;

            AddGraphic(Image.CreateRectangle(2, 5, Color.White));
        }

        public override void Update()
        {
            base.Update();

            Y += MoveSpeed;

            if (IsEnemy)
                if (scene.player.Collide(Position.X, Position.Y))
                {

                }
        }
    }
}
