using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;

namespace space_invader
{
    /// <summary>
    /// Class for Squid aka small invader
    /// </summary>
    class Squid : Enemy
    {
        // Small Invader
        static Image EnemyImage = new Image("../../../Assets/enemy1.png");
        // Adds squid to the scene
        public Squid()
        {
            AddGraphic(EnemyImage);
            Score = 20;
        }
        //Gives squid its position
        public Squid(int X, int Y)
        {
            AddGraphic(EnemyImage);
            SetPosition(X, Y);
        }
    }
}
