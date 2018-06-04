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
        AutoTimer ShootingCooldown;
        float ShootingCooldownTime = 100.0f;
        bool CanShoot = true;

        public Player(MainScene _scene)
        {
            scene = _scene;

            // Set position
            SetPosition(new Vector2(scene.PlayPosition.X + scene.PlayWidth.X,
                              scene.PlayPosition.Y + scene.PlayWidth.Y));

            // Set image
            AddGraphic(Image.CreateCircle(12, Color.Red));

            //Initiate shootingCooldown
            ShootingCooldown = new AutoTimer(ShootingCooldownTime);
        }

        public void UpdateShooting()
        {
            ShootingCooldown.Update();
            if (ShootingCooldown.AtMax)
                CanShoot = true;
        }

        public override void Update()
        {
            base.Update();

            // Check if shootingtimer is at max
            UpdateShooting();

            // Check if player is moving left
            if (Input.KeyDown(Key.A) || Input.KeyDown(Key.Left))
                X -= MoveSpeed;

            // Check if player is moving right
            if (Input.KeyDown(Key.D) || Input.KeyDown(Key.Right))
                X += MoveSpeed;

            // Check if player is shooting
            if (Input.KeyDown(Key.Space) && CanShoot)
                Shoot();

            // Check if player is in play area
            if (X < scene.PlayPosition.X)
                X = scene.PlayPosition.X;
            else if (X > scene.PlayPosition.X + scene.PlayWidth.X)
                X = scene.PlayPosition.X + scene.PlayWidth.X;
        }

        void Shoot()
        {
            Bullet bullet = new Bullet(scene, false, -3.0f, Position);
            CanShoot = false;
            ShootingCooldown.Reset();

            scene.Add(bullet);
        }
    }
}
