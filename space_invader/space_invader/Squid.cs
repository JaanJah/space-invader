using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;

namespace space_invader
{
    class Squid : Enemy
    {
        static Image image = new Image("../../../Assets/enemy1.png");

        public Squid(Vector2 pos)
        {
            AddGraphic(image);
            Position = pos;
        }


    }
}
