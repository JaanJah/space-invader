using Otter;

namespace space_invader
{
    /// <summary>
    /// Player class
    /// </summary>
    class Player : Entity
    {
        //Sets basic variables
        float MoveSpeed = 5.0f;
        public Bullet bullet;
        public int ScoreAmount = 0;
        public int playerLives = 3;
        public bool Alive = true;
        Image playerImage = new Image("Assets/player.png");
        Image playerDieImage = new Image("Assets/playerDead.png");
        AutoTimer Ressurrection = new AutoTimer(50.0f);

        /// <summary>
        /// class used for the player
        /// </summary>
        public Player()
        {
            MainScene scene = Program.game.GetScene<MainScene>();

            // Set position
            SetPosition(new Vector2(scene.PlayPosition.X + scene.PlayWidth.X,
                              scene.PlayPosition.Y + scene.PlayWidth.Y));

            // Set image 
            AddGraphic(playerImage);

            // Add collider
            BoxCollider Collider = new BoxCollider(30, 30, Tags.Player);
            AddCollider(Collider);

            // Initialize bullet
            Image playerBullet = new Image("Assets/playerBullet.png");
            BoxCollider bulletCollider = new BoxCollider(playerBullet.Width, playerBullet.Height, Tags.Player);
            bullet = new Bullet(-6.0f, new Vector2(0, 0), bulletCollider);
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

            Ressurrection.Update();

            if (!Alive && Ressurrection.AtMax)
            {
                MainScene scene = Program.game.GetScene<MainScene>();

                Alive = true;
                RemoveGraphic(playerDieImage);
                AddGraphic(playerImage);
                scene.player.SetPosition(new Vector2(scene.PlayPosition.X + scene.PlayWidth.X,
                        scene.PlayPosition.Y + scene.PlayWidth.Y));
            }

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
            MainScene scene = Program.game.GetScene<MainScene>();

            if (Alive)
            {
                // Check if player is moving left
                if (Input.KeyDown(Key.A) || Input.KeyDown(Key.Left))
                    X -= MoveSpeed;

                // Check if player is moving right
                if (Input.KeyDown(Key.D) || Input.KeyDown(Key.Right))
                    X += MoveSpeed;

                // Check if player is shooting
                if (Input.KeyDown(Key.Space) && !bullet.Visible)
                    Shoot();
            }

            // Check if player is in play area
            if (X < scene.PlayPosition.X)
                X = scene.PlayPosition.X;
            else if (X > scene.PlayPosition.X + scene.PlayWidth.X)
                X = scene.PlayPosition.X + scene.PlayWidth.X;
        }

        // Player shooting
        void Shoot()
        {
            bullet.Collidable = true;
            bullet.Visible = true;
            bullet.Position = Position;
        }
        // Player death
        public void Die()
        {
            Alive = false;
            Ressurrection.Reset();
            Ressurrection.Start();
            RemoveGraphic(playerImage);
            AddGraphic(playerDieImage);
        }
    }
}
