using System;
using Otter;
using System.Threading;

namespace space_invader
{
    /// <summary>
    /// Class for highscore button
    /// </summary>
    class Button : Entity
    {
        

        /// <summary>
        /// Initializes new Button
        /// </summary>
        /// <param name="x">positionX</param>
        /// <param name="y">positionY</param>
        public Button (float x, float y) : base(x, y)
        {
            Image buttonOutline = new Image("Assets/buttonOutline.png");
            AddGraphic(buttonOutline);
        }

        /// <summary>
        /// Updates button
        /// </summary>
        public override void Update()
        {
            base.Update();

            //Check if mouse is pressed
            if (Input.MouseButtonReleased(MouseButton.Left))
            {
                // Check if button is pressed before
                if (!Program.pressed)
                {
                    // Check if cursor position is in button bounds
                    if (Util.InRect(Scene.MouseX, Scene.MouseY, X, Y, 80, 30))
                    {
                        Program.pressed = true;

                        // Get player input and adds it to leaderboard
                        var inputText = Scene.GetEntity<TextBox>().inputString;

                        Leaderboard.AddScore(inputText, Program.curScoreTxt);

                        ReadXML.WriteScores();

                        Scene scene = new HighScoresScene();

                        Program.game.SwitchScene(scene);
                    }
                }
            }
        }
    }
}
