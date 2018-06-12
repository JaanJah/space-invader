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
        public static void LoadImage(XElement xNode, Scene scene)
        {
            // Load image
            Image image = new Image(Program.game.GameFolder + xNode.Attribute("path").Value);
            image.Name = xNode.Attribute("name").Value;

            // Add image to resourceManager
            Program.resourceManager.AddGraphic(image);
        }

        public static void LoadBarricade(XElement xNode, Scene scene)
        {
            // Spawn 4 barricades
            for (int i = 0; i < 4; i++)
            {
                // Load Barricade
                Barricade barricade = new Barricade();

                // Set barricade variables
                barricade.SetPosition(float.Parse((xNode.Attribute("posx").Value)) + (i * 200),
                                      float.Parse((xNode.Attribute("posy").Value)));

                // Add barricade to scene
                scene.Add(barricade);
            }
        }

        public static void LoadEnemy(XElement xNode)
        {
            // Load enemy


            //Program.game.
        }
    }
}
