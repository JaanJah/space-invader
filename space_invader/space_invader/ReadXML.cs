﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Otter;

namespace space_invader
{
    static class ReadXML
    {
        public static string highScoreTxt;

        public static string MainScreenXML(RichText highScoreTxt)
        {
            MainScene scene = (MainScene)Program.game.FirstScene;

            XmlDocument xdoc = new XmlDocument();
            xdoc.Load("savefile.xml");

            var xmlnodes = xdoc.DocumentElement.ChildNodes;
            int curScore = Int32.Parse(xmlnodes[0].Attributes["score"].Value);

            for (int i = 0; i < xmlnodes.Count; i++)
            {

                if (Int32.Parse(xmlnodes[i].Attributes["score"].Value) > curScore)
                {
                    curScore = Int32.Parse(xmlnodes[i].Attributes["score"].Value);
                }

            }
            highScoreTxt.String = curScore.ToString();
            return curScore.ToString();
        }

        public static void WriteScores()
        {
            HighScoresScene scene = Program.game.GetScene<HighScoresScene>();

            if (!System.IO.File.Exists("savefile.xml"))
                return;
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("savefile.xml");

            XmlNodeList xmlElements = xmlDoc.DocumentElement.ChildNodes;
            List<XmlElement> xmlnodes = new List<XmlElement>();

            foreach (XmlElement i in xmlElements)
                xmlnodes.Add(i);

            int count;
            if (xmlnodes.Count < 5)
                count = xmlnodes.Count;
            else
                count = 5;

            for (int i = 0; i < count; i++)
            {
                XmlElement curElement = xmlnodes[0];

                RichText name = new RichText();
                RichText score = new RichText();

                for (int j = 0; j < xmlnodes.Count; j++)
                {
                    if (xmlnodes[j].Attributes["score"].Value != "")
                    {
                        if (xmlnodes[j].Attributes["name"].Value != "")
                        {

                            if (Int32.Parse(curElement.Attributes["score"].Value) < Int32.Parse(xmlnodes[j].Attributes["score"].Value))
                            {
                                curElement = xmlnodes[j];
                            }
                        }
                    }
                }


                name.String = curElement.Attributes["name"].Value;
                name.SetPosition(8, i * 50 + 116);
                scene.hslb.AddGraphic(name);
                if (i == 0)
                {
                    highScoreTxt = score.ToString();
                }
                score.String = curElement.Attributes["score"].Value;
                score.SetPosition(200, i * 50 + 116);
                scene.hslb.AddGraphic(score);
                xmlnodes.Remove(curElement);
            }
        }
    }
}
