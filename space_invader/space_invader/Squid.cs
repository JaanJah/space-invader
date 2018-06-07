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
        static Image EnemyImage = new Image("../../../Assets/enemy1.png");

        public Squid()
        {
            AddGraphic(EnemyImage);
        }
        
        public Squid(int X, int Y)
        {
            AddGraphic(EnemyImage);
            SetPosition(X, Y);
        }
    }
}
