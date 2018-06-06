using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;
using System.IO;

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
                    File.WriteAllText("../../../savefile/save.txt", inputText);
                    // debug
                    Console.WriteLine("MouseX: {0}, MouseY: {1}", Scene.MouseX, Scene.MouseY);
                }
            }
        }

    }
}
