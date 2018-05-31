using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;

namespace space_invader
{
    class MainScene : Scene
    {
        //Update scene
        public override void Update()
        {
            base.Update();

            //Debug - Switches Scene if input is H
            if (Input.KeyPressed(Key.H))
            {
                Game.SwitchScene(new HighScoresScene());
            }
        }
    }
}
