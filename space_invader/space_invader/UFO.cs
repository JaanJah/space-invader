using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;

namespace space_invader
{
    class UFO : Entity
    {
        static Image EnemyImage = new Image("../../../Assets/enemy4.png");
        AutoTimer AppearTimer;
        Vector2 MovementDir;
        public int Score;
        public int[] scoreArray = new int[] { 50, 100, 150 };
        public UFO()
        {
            Random rnd = new Random();

            AppearTimer = new AutoTimer(rnd.Next(500, 1000));
            AppearTimer.Start();

            Visible = false;
            Collidable = false;

            BoxCollider collider = new BoxCollider(24, 24, Tags.Ufo);
            Random rndScore = new Random();

            Score = rndScore.Next(scoreArray.Length);

            AddCollider(collider);
            AddGraphic(EnemyImage);
        }

        public void Die()
        {
            Visible = false;
            Collidable = false;
        }

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

        public void UpdateMovement()
        {
            SetPosition(Position + MovementDir);
        }

        public override void Update()
        {
            base.Update();

            AppearTimer.Update();

            UpdateMovement();
            CheckAppear();
        }
    }
}
