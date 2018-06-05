using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;

namespace space_invader
{
    /// <summary>
    /// Player class
    /// </summary>
    class Player : Entity
    {
        float MoveSpeed = 2.0f;
        MainScene scene;
        Bullet bullet;
        
        Image playerImage = new Image("../../../assets/player.png");

        BoxCollider collider = new BoxCollider(30, 30, Tags.Player);
        public Player(MainScene _scene)
        {
            scene = _scene;

            // Set position
            SetPosition(new Vector2(scene.PlayPosition.X + scene.PlayWidth.X,
                              scene.PlayPosition.Y + scene.PlayWidth.Y));

            // Set image
            AddGraphic(playerImage);

            // Add collider
            AddCollider(collider);

            // Initiate bullet
            Image playerBullet = new Image("../../../Assets/playerBullet.png");
            BoxCollider bulletCollider = new BoxCollider(playerBullet.Width, playerBullet.Height, Tags.Player);
            bullet = new Bullet(scene, -3.0f, new Vector2(0, 0), bulletCollider);
            bullet.Visible = false;
            bullet.Collidable = false;
            bullet.AddGraphic(playerBullet);
            scene.Add(bullet);
        }

        public override void Update()
        {
            base.Update();

            // Check if player is moving left
            if (Input.KeyDown(Key.A) || Input.KeyDown(Key.Left))
                X -= MoveSpeed;

            // Check if player is moving right
            if (Input.KeyDown(Key.D) || Input.KeyDown(Key.Right))
                X += MoveSpeed;

            // Check if player is shooting
            if (Input.KeyDown(Key.Space) && !bullet.Visible)
                Shoot();

            // Check if player is in play area
            if (X < scene.PlayPosition.X)
                X = scene.PlayPosition.X;
            else if (X > scene.PlayPosition.X + scene.PlayWidth.X)
                X = scene.PlayPosition.X + scene.PlayWidth.X;
        }

        void Shoot()
        {
            bullet.Visible = true;
            bullet.Collidable = true;
            bullet.Position = Position;
        }
    }
}
