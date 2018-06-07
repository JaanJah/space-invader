using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;

namespace space_invader
{
    class HighScoreLeaderboard : Entity
    {
        public HighScoreLeaderboard(float x, float y) : base(x,y)
        {
            Image highScoreTableTitle = new Image("../../../Assets/highScoreTableTitle.png");
            AddGraphic(highScoreTableTitle);
            AddGraphic(Image.CreateRectangle(1, 350, Color.Blue));
        }
    }
}
