using Otter;

namespace space_invader
{
    /// <summary>
    /// TextBox class
    /// </summary>
    class TextBox : Entity
    {
        //Basic variables for textbox
        public Text text = new Text();
        public Image imageBox = Image.CreateRectangle(150, 20);
        public bool hasFocus;
        public int charLimit = 16;
        public string inputString = "";
        public int firstTime = 0;
        /// <summary>
        /// Creates textbox
        /// </summary>
        /// <param name="x">X coordinate for textbox</param>
        /// <param name="y">Y coordinate for textbox</param>
        public TextBox(float x, float y) : base(x, y)
        {
            imageBox.OutlineColor = Color.Blue;
            imageBox.OutlineThickness = 2;
            text.Color = Color.Black;

            AddGraphic(imageBox);
            AddGraphic(text);
        }
        //Updates textbox
        public override void Update()
        {
            base.Update();

            if (hasFocus)
            {
                firstTime++;
                //Checks if the textbox hasFocus for the first time, if has, then clears textbox strings.
                if (firstTime == 1)
                {
                    Input.ClearKeystring();
                }
                inputString = Input.KeyString;
                // If we exceed the character limit
                if (inputString.Length > charLimit)
                {
                    inputString = inputString.Substring(0, charLimit);
                    Input.KeyString = inputString;
                }
                // If the character limit isn't reached
                if (inputString.Length < charLimit)
                {
                    // Displays a blinking pipe character
                    if (Timer % 30 >= 15)
                    {
                        text.String = inputString + "|";
                    }
                    else
                    {
                        text.String = inputString;
                    }
                }
                else
                {
                    // If at limit, show the string
                    text.String = inputString;
                }
            }
            else
            {
                // If hasFocus == false then display string normally
                text.String = inputString;
            }

            // If the left mouse button is pressed, check if scene was clicked on
            if (Input.MouseButtonPressed(MouseButton.Left))
            {
                if (Util.InRect(Scene.MouseX, Scene.MouseY, X, Y, 150, 20))
                {
                    //If textbox doesn't have focus when pressed on, then give focus and change outlinecolor
                    if (!hasFocus)
                    {

                        hasFocus = true;
                        imageBox.OutlineColor = Color.Green;
                    }
                }
                else
                {
                    //If textbox has focus but not on textbox area, then take away focus and change outlinecolor
                    if (hasFocus)
                    {
                        hasFocus = false;
                        imageBox.OutlineColor = Color.Blue;
                    }
                }
            }

            if (hasFocus)
            {
                // If we have focus check for return key
                if (Input.KeyPressed(Key.Return))
                {
                    // if pressed then unfocus
                    hasFocus = false;
                }
            }
        }
    }
}
