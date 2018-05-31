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

        public Player(MainScene _scene)
        {
            scene = _scene;

            // Set position
            SetPosition(new Vector2(scene.PlayPosition.X + scene.PlayWidth.X,
                              scene.PlayPosition.Y + scene.PlayWidth.Y));

            // Set image
            AddGraphic(Image.CreateCircle(12, Color.Red));
        }

        public override void Update()
        {
            base.Update();

            // Check if player is moving left
            if (Input.KeyDown(Key.A) || Input.KeyDown(Key.Left))
            {
                X -= MoveSpeed;
            }
            
            // Check if player is moving right
            if (Input.KeyDown(Key.D) || Input.KeyDown(Key.Right))
            {
                X += MoveSpeed;
            }

            // Check if player is in play area
            if (X < scene.PlayPosition.X)
                X = scene.PlayPosition.X;
            else if (X > scene.PlayPosition.X + scene.PlayWidth.X)
                X = scene.PlayPosition.X + scene.PlayWidth.X;
        }
    }
}
