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
    static class ReadXML
    {

        public static string MainScreenXML()
        {
            MainScene scene = (MainScene)Program.game.FirstScene;

            XmlDocument xdoc = new XmlDocument();
            xdoc.Load("savefile.xml");

            var scores = xdoc.DocumentElement.ChildNodes;
            var curScore = scores[0].Attributes["score"].Value;

            foreach (XmlNode i in scores)
                if (Int32.Parse(i.Attributes["score"].Value) > Int32.Parse(curScore))
                    curScore = i.Attributes["score"].Value;

            return curScore;
        }
        public static void WriteScores()
        {
            HighScoresScene scene = Program.game.GetScene<HighScoresScene>();

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("savefile.xml");

            XmlNodeList xmlElements = xmlDoc.DocumentElement.ChildNodes;
            List<XmlNode> xmlnodes = new List<XmlNode>();

            foreach(XmlNode i in xmlElements)
                xmlnodes.Add(i);

            List<XmlNode> sortednodes = (from x in xmlnodes
                               orderby int.Parse(x.Attributes["score"].Value) ascending
                               select x).ToList();

            for (int i = 0; i < 5; i++)
            {
                RichText name = new RichText();
                RichText score = new RichText();
                
                name.String = sortednodes[i].Attributes["name"].Value;
                name.SetPosition(8, i * 50 + 116);
                scene.hslb.AddGraphic(name);

                score.String = sortednodes[i].Attributes["score"].Value;
                score.SetPosition(64, i * 50 + 116);
                scene.hslb.AddGraphic(score);
            }
        }
    }
}
