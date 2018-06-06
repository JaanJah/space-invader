using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;

namespace space_invader
{
    class HighScoresScene : Scene
    {
        // Stuff in the HighScores Scene
        public HighScoresScene() : base()
        {
            AddGraphic(Image.CreateRectangle(Game.Instance.Width, Game.Instance.Height, Color.Green));  
        }

        // Updates Scene

        public override void Update()
        {
            base.Update();

            //Debug - Switches Scene if Input is H
            if (Input.KeyPressed(Key.H))
            {
                Game.SwitchScene(new MainScene());
            }
        }
    }
}
