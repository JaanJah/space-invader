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
        public Bullet bullet;
        public int ScoreAmount = 0;
        public int playerLives = 3;
        Image playerImage = new Image("../../../assets/player.png");
        BoxCollider Collider = new BoxCollider(30, 30, Tags.Player);

        /// <summary>
        /// class used for the player
        /// </summary>
        public Player()
        {
            MainScene scene = (MainScene)Program.game.FirstScene;

            // Set position
            SetPosition(new Vector2(scene.PlayPosition.X + scene.PlayWidth.X,
                              scene.PlayPosition.Y + scene.PlayWidth.Y));

            // Set image
            AddGraphic(playerImage);

            // Add collider
            AddCollider(Collider);

            // Initialize bullet
            Image playerBullet = new Image("../../../Assets/playerBullet.png");
            BoxCollider bulletCollider = new BoxCollider(playerBullet.Width, playerBullet.Height, Tags.Player);
            bullet = new Bullet(-3.0f, new Vector2(0, 0), bulletCollider);
            bullet.Visible = false;
            bullet.Collidable = false;
            bullet.AddGraphic(playerBullet);
            scene.Add(bullet);
        }

        /// <summary>
        /// Called every frame
        /// </summary>
        public override void Update()
        {
            base.Update();

            UpdateMovement();

            //If playerLives are 0, then switch to highscore screen
            if (playerLives == 0)
                Game.SwitchScene(new HighScoresScene());
        }

        /// <summary>
        /// Handles player movement
        /// </summary>
        void UpdateMovement()
        {
            MainScene scene = (MainScene)Program.game.FirstScene;

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
            bullet.Collidable = true;
            bullet.Visible = true;
            bullet.Position = Position;
        }

        public void playerDeath()
        {

        }
    }
}
