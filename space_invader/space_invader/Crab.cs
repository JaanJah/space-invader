using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;

namespace space_invader
{
    /// <summary>
    /// Enemy crab
    /// </summary>
    class Crab : Enemy
    {
        // Medium Invader
        static Image EnemyImage = new Image("../../../Assets/enemy2.png");

        /// <summary>
        /// Initializes new Crab
        /// </summary>
        public Crab()
        {
            AddGraphic(EnemyImage);
            Score = 40;
        }
    }
}
