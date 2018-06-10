using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Otter;

namespace space_invader
{
    static class XMLLoader
    {
        public static void LoadImage(XElement xNode)
        {
            // Load image
            Image image = new Image(Program.game.GameFolder + xNode.Attribute("path").Value);
            image.Name = xNode.Attribute("name").Value;

            // Add image to resourceManager
            Program.resourceManager.AddGraphic(image);
        }

        public static void LoadBarricade(XElement xNode)
        {
            // Add barricade to scene
            Program.game.GetScene<MainScene>().OnBegin = delegate
            {
                // Load Barricade
                Barricade barricade = new Barricade();

                Program.game.GetScene<MainScene>().Add(barricade);
            };
        }

        public static void LoadEnemy(XElement xNode)
        {
            // Load enemy


            //Program.game.
        }
    }
}
