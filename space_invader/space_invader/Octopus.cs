using Otter;

namespace space_invader
{
    /// <summary>
    /// Class for octupus aka large invader in the game
    /// </summary>
    class Octopus : Enemy
    {
        // Large Invader
        static Image EnemyImage = new Image("../../../Assets/enemy3.png");
        // Adds octupus graphic to the game and sets the scoreamount.
        public Octopus()
        {
            AddGraphic(EnemyImage);
            Score = 10;
        }
    }
}
