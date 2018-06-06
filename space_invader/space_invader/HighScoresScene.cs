﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;

namespace space_invader
{
    class HighScoresScene : Scene
    {
        // Stuff in the HighScores Scene
        public HighScoresScene() : base()
        {
            var scene = new Scene();
            Program.game.MouseVisible = true;
            scene.Add(new TextBox(100, 100));
            Program.game.AddScene(scene);
        }
        // Updates Scene

    }

    class TextBox : Entity
    {
        public Text text = new Text();
        public Image imageBox = Image.CreateRectangle(400, 20);
        public bool hasFocus;
        public int charLimit = 12;
        public string inputString = "";

        public TextBox(float x, float y) : base(x, y)
        {
            imageBox.OutlineColor = Color.Green;
            imageBox.OutlineThickness = 2;
            text.Color = Color.Black;

            AddGraphic(imageBox);
            AddGraphic(text);
        }

        public override void Update()
        {
            base.Update();

            if (hasFocus)
            {
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
                if (Util.InRect(Scene.MouseX, Scene.MouseY, X, Y, 400, 50))
                {
                    if (!hasFocus)
                    {
                        
                        hasFocus = true;
                        imageBox.OutlineColor = Color.Green;
                    }
                }
                else
                {
                    if (hasFocus)
                    {
                        hasFocus = false;
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
