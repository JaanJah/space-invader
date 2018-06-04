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
        public float MoveSpeed;
        bool IsEnemy;
        MainScene scene;

        public Bullet(MainScene _scene, bool _IsEnemy, float _MoveSpeed, Vector2 pos)
        {
            scene = _scene;
            IsEnemy = _IsEnemy;
            Position = pos;
            MoveSpeed = _MoveSpeed;

            AddGraphic(Image.CreateRectangle(3, 7, Color.White));
        }

        public override void Update()
        {
            base.Update();


            Y += MoveSpeed;

            /*if (IsEnemy)
                if (scene.player.Collide(Position.X, Position.Y))
                {

                }*/
        }
    }
}
