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
        MainScene scene;

        Image bullet = new Image("../../../Assets/playerBullet.png");
        private float v;
        private Tags player;

        public Bullet(MainScene _scene, bool _IsEnemy, float _MoveSpeed, Vector2 pos)

        BoxCollider collider;

        public Bullet(MainScene _scene, float _MoveSpeed, Vector2 pos, Tags tag)

        {
            scene = _scene;
            Position = pos;
            MoveSpeed = _MoveSpeed;

            AddGraphic(bullet);

            collider = new BoxCollider(3, 7, tag);

            AddCollider(collider);
            AddGraphic(Image.CreateRectangle(3, 7, Color.White));
        }

        public Bullet(MainScene scene, float v, Vector2 position, Tags player)
        {
            this.scene = scene;
            this.v = v;
            Position = position;
            this.player = player;
        }

        public override void Update()
        {
            base.Update();

            Y += MoveSpeed;

            if (collider.Overlap(X, Y, Tags.Player))
            {
                RemoveSelf();
            }

            if (collider.Overlap(X, Y, Tags.Enemy))
            {
                RemoveSelf();
            }
        }
    }
}
