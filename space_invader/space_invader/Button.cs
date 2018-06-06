using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;

namespace space_invader
{
    class Button : Entity
    {
        public Button (float x, float y) : base(x, y)
        {
            var submitTxt = new RichText("Submit", 16);
            
            AddGraphic(Image.CreateRectangle(80, 30, Color.Blue));
            submitTxt.SetPosition(100,100);
            
            AddGraphic(submitTxt);
        }
        
    }
}
