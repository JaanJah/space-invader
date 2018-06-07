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
    class Button : Entity
    {
        
        public Button (float x, float y) : base(x, y)
        {
            Image buttonOutline = new Image("../../../Assets/buttonOutline.png");
            AddGraphic(buttonOutline);
        }

        public override void Update()
        {
            base.Update();

            if (Input.MouseButtonPressed(MouseButton.Left))
            {
                if (Util.InRect(Scene.MouseX, Scene.MouseY, X,Y, 80, 30))
                {
                    var inputText = Scene.GetEntity<TextBox>().inputString;
                    MainScene scene = Program.game.GetScene<MainScene>();
                    var a = Program.game.Scene;
                    Leaderboard.AddScore(inputText, Program.curScoreTxt);
                }
            }
        }

    }
}
