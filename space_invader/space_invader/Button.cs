using System;
using System.Threading;

namespace space_invader
{
    /// <summary>
    /// Class for highscore button.
    /// </summary>
    class Button : Entity
    {
        // Check if button is pressed
        bool pressed = false;

        /// <summary>
        /// Create Button.
        /// </summary>
        /// <param name="x">Position X</param>
        /// <param name="y">Position Y</param>
        public Button (float x, float y) : base(x, y)
        {
            Image buttonOutline = new Image("Assets/buttonOutline.png");
            AddGraphic(buttonOutline);
        }

        /// <summary>
        /// Update button.
        /// </summary>
        public override void Update()
        {
            base.Update();

            //Check if mouse is pressed
            if (Input.MouseButtonReleased(MouseButton.Left))
            {
                // Check if button is pressed before
                if (!pressed)
                {
                    // Check if cursor position is in button bounds
                    if (Util.InRect(Scene.MouseX, Scene.MouseY, X, Y, 80, 30))
                    {
                        pressed = true;

                        // Get player input and adds it to leaderboard
                        var inputText = Scene.GetEntity<TextBox>().inputString;

                        // Remove previous scores
                        Program.game.GetScene<HighScoresScene>().hslb.RemoveGraphics(Program.game.GetScene<HighScoresScene>().hslb.GetGraphic<RichText>());

                        // Add score to save file
                        Leaderboard.AddScore(inputText, Program.curScoreTxt);

                        // Load new scores
                        ReadXML.WriteScores();
                    }
                }
            }
        }
    }
}
