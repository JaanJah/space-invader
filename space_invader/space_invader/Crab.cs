using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;

namespace space_invader
{
    class Crab : Entity
    {
        static Image EnemyImage = new Image("../../../enemy2.png");

        public Crab()
        {
            AddGraphic(EnemyImage);
        }
    }
}
