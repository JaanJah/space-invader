using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;

namespace space_invader
{
    class Crab : Enemy
    {
        // Medium Invader
        static Image EnemyImage = new Image("../../../Assets/enemy2.png");

        public Crab()
        {
            AddGraphic(EnemyImage);
            Score = 20;
        }
    }
}
