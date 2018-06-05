using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;

namespace space_invader
{
    class Barricade : Entity
    {
        static Vector2 Size = new Vector2(24, 24);
        Image block = Image.CreateRectangle((int) Size.X, (int) Size.Y, Color.Green);

        public Barricade()
        {
            AddGraphic(block);
        }


    }
}
