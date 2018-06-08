using System;
using Otter;

namespace space_invader
{
    class UFO : Entity
    {
        /// <summary>
        /// UFO enemy in the game
        /// </summary>
        static Image EnemyImage = new Image("../../../Assets/enemy4.png");
        AutoTimer AppearTimer;
        Vector2 MovementDir;
        public int Score;

        //Sets a random timer when UFO appears
        public UFO()
        {
            
            Random rnd = new Random();
            MainScene scene = Program.game.GetScene<MainScene>();

            AppearTimer = new AutoTimer(rnd.Next(1000, 2000));
            if (Enemy.hasMoved)
            {
                AppearTimer.Start();
            }

            BoxCollider collider = new BoxCollider(24, 24, Tags.Ufo);

            Visible = false;
            Collidable = false;

            Score = rnd.Next(0, 3) * 50;

            AddCollider(collider);
            AddGraphic(EnemyImage);
        }
        //UFO death
        public void Die()
        {
            Visible = false;
            Collidable = false;
        }

        //If UFO appears then it starts moving
        void CheckAppear()
        {
            Random rnd = new Random();

            if (AppearTimer.AtMax)
            {
                int direction = rnd.Next(0, 2);
                Visible = true;
                Collidable = true;

                SetPosition(new Vector2(850 * direction - 50, 64));

                if (direction == 1)
                    MovementDir = new Vector2(-2.0f, 0.0f);
                else
                    MovementDir = new Vector2(2.0f, 0.0f);

                AppearTimer.Reset();
            }
        }
        //Update UFO's movement
        public void UpdateMovement()
        {
            SetPosition(Position + MovementDir);
        }
        //Update functions
        public override void Update()
        {
            base.Update();

            AppearTimer.Update();

            CheckAppear();
            UpdateMovement();
        }
    }
}
