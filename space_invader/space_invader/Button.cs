using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace space_invader
{
    /// <summary>
    /// Class for highscore button
    /// </summary>
    class Button : Entity
    {
        bool pressed = false;

        /// <summary>
        /// Initializes new Button
        /// </summary>
        /// <param name="x">positionX</param>
        /// <param name="y">positionY</param>
        public Button (float x, float y) : base(x, y)
        {
            Image buttonOutline = new Image("../../../Assets/buttonOutline.png");
            AddGraphic(buttonOutline);
        }

        /// <summary>
        /// Updates button
        /// </summary>
        public override void Update()
        {
            base.Update();

            //Check if mouse is pressed
            if (Input.MouseButtonPressed(MouseButton.Left))
            {
                // Check if button is pressed before
                if (!pressed)
                {
                    // Check if cursor position is in button bounds
                    if (Util.InRect(Scene.MouseX, Scene.MouseY, X, Y, 80, 30))
                    {
                        // Get player input and adds it to leaderboard
                        var inputText = Scene.GetEntity<TextBox>().inputString;
                        MainScene scene = Program.game.GetScene<MainScene>();

                        Leaderboard.AddScore(inputText, Program.curScoreTxt);

                        pressed = true;
                    }
                }
            }
        }

    }
}
