namespace space_invader
{
    /// <summary>
    /// Enemy crab.
    /// </summary>
    class Crab : Enemy
    {
        // Enemy image
        static Image Image = new Image("Assets/enemy2.png");

        /// <summary>
        /// Create Crab.
        /// </summary>
        public Crab()
        {
            AddGraphic(Image);
            Score = 40;
        }
    }
}
