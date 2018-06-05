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
        BoxCollider collider;

        public Bullet(MainScene _scene, float _MoveSpeed, Vector2 pos, Tags tag)
        {
            scene = _scene;
            Position = pos;
            MoveSpeed = _MoveSpeed;

            collider = new BoxCollider(3, 7, tag);

            AddCollider(collider);
        }

        public override void Update()
        {
            base.Update();

            Y += MoveSpeed;

            if (collider.Tags[0] == (int)Tags.Player)
                if (collider.CollideEntities(X, Y, Tags.Enemy).Count > 0)
                {
                    collider.CollideEntities(X, Y, Tags.Enemy)[0].RemoveSelf();
                    RemoveSelf();
                    scene.player.ScoreAmount += 10;
                    //debug
                    Console.WriteLine("Score amount: {0}",scene.player.ScoreAmount);
                }

            if (collider.Tags[0] == (int)Tags.Enemy)
                if (collider.CollideEntities(X, Y, Tags.Player).Count > 0)
                {
                    RemoveSelf();
                    scene.player.playerLives -= 1;
                    scene.player.SetPosition(new Vector2(scene.PlayPosition.X + scene.PlayWidth.X,
                    scene.PlayPosition.Y + scene.PlayWidth.Y));
                    //debug
                    Console.WriteLine("Lives left: {0}",scene.player.playerLives);
                }
        }
    }
}
