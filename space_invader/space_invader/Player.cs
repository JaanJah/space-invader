﻿using System;
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

        public int ScoreAmount = 0;
        public int playerLives = 3;

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

            //If playerLives are 0, then switch to highscore screen
            if (playerLives == 0)
            {
                Game.SwitchScene(new HighScoresScene());
            }
        }

        void Shoot()
        {
            Image playerBullet = new Image("../../../Assets/playerBullet.png");

            Bullet bullet = new Bullet(scene, -3.0f, Position, Tags.Player);
            bullet.AddGraphic(playerBullet);

            CanShoot = false;
            ShootingCooldown.Reset();

            scene.Add(bullet);
        }

        public void playerDeath()
        {
        }
    }
}
