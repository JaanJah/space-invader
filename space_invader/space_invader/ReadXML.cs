using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Otter;

namespace space_invader
{
    static class ReadXML
    {
        public static void WriteScores(XmlNodeList xmlElements)
        {
            HighScoresScene scene = Program.game.GetScene<HighScoresScene>();

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("savefile.xml");

            xmlElements = xmlDoc.DocumentElement.ChildNodes;

            for (int i = 0; i > 5; i++)
            {
                RichText name = new RichText();
                RichText score = new RichText();

                name.String = xmlElements[i].Attributes["name"].Value;
                name.SetPosition(8, i * 50);
                scene.hslb.AddGraphic(name);

                score.String = xmlElements[i].Attributes["score"].Value;
                name.SetPosition(64, i * 50);
                scene.hslb.AddGraphic(score);
            }
        }
    }
}
