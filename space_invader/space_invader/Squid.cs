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
        // Small Invader
        static Image EnemyImage = new Image("../../../Assets/enemy1.png");

        public Squid()
        {
            AddGraphic(EnemyImage);
            Score = 10;
        }
        
        public Squid(int X, int Y)
        {
            AddGraphic(EnemyImage);
            SetPosition(X, Y);
        }
    }
}
