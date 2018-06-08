using Otter;

namespace space_invader
{
    class Button : Entity
    {
        bool pressed = false;

        public Button (float x, float y) : base(x, y)
        {
            Image buttonOutline = new Image("../../../Assets/buttonOutline.png");
            AddGraphic(buttonOutline);
        }

        public override void Update()
        {
            base.Update();

            if (Input.MouseButtonPressed(MouseButton.Left))
            {
                if (!pressed)
                {
                    if (Util.InRect(Scene.MouseX, Scene.MouseY, X, Y, 80, 30))
                    {
                        var inputText = Scene.GetEntity<TextBox>().inputString;
                        MainScene scene = Program.game.GetScene<MainScene>();
                        var a = Program.game.Scene;
                        Leaderboard.AddScore(inputText, Program.curScoreTxt);


                        pressed = true;
                    }
                }
            }
        }

    }
}
