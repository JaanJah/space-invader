﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;

namespace space_invader
{
    class Octopus : Entity
    {
        static Image EnemyImage = new Image("../../../enemy3.png");

        public Octopus()
        {
            AddGraphic(EnemyImage);
        }
    }
}