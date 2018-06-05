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
        static MainScene scene;
        BoxCollider collider;

        public Bullet(MainScene _scene, float _MoveSpeed, Vector2 pos, BoxCollider _collider)
        {
            scene = _scene;
            Position = pos;
            MoveSpeed = _MoveSpeed;
            collider = _collider;

            AddCollider(collider);
        }

        public override void Update()
        {
            base.Update();

            if (Visible)
            {
                Y += MoveSpeed;

                if (collider.Tags[0] == (int)Tags.Player)
                    if (Position.Y < 0)
                    {
                        Visible = false;
                        Collidable = false;
                    }

                if (collider.Tags[0] == (int)Tags.Enemy)
                    if (Position.Y > Game.Height)
                        RemoveSelf();

                if (collider.Tags[0] == (int)Tags.Player)
                    if (collider.CollideEntities(X, Y, Tags.Enemy).Count > 0)
                    {
                        collider.CollideEntities(X, Y, Tags.Enemy)[0].RemoveSelf();
                        Visible = false;
                        Collidable = false;
                    }

                if (collider.Tags[0] == (int)Tags.Enemy)
                    if (collider.CollideEntities(X, Y, Tags.Player).Count > 0)
                    {
                        Enemy.FindEnemies();
                        RemoveSelf();
                    }
            }
        }
    }
}
