using System;
using System.Collections.Generic;
using System.Xml;
using Otter;

namespace space_invader
{
    static class ReadXML
    {
        /// <summary>
        /// Reads XML and creates HighScore and HighScoreLeaderboard with it
        /// </summary>
        /// <returns>HighScore values</returns>
        public static string MainScreenXML()
        {
            
            MainScene scene = (MainScene)Program.game.FirstScene;
            //Loads in XML
            XmlDocument xmlDoc = new XmlDocument();

            if (!System.IO.File.Exists("savefile.xml"))
            {
                XmlElement root = xmlDoc.CreateElement("leaderboard");
                xmlDoc.AppendChild(root);
            }
            else
            {
                xmlDoc.Load("savefile.xml");
            }

            var xmlnodes = xmlDoc.DocumentElement.ChildNodes;
            //Gets the score value from the "score" attribute
            int curScore;
            if (xmlnodes.Count == 0)
                curScore = 0;
            else
                curScore = Int32.Parse(xmlnodes[0].Attributes["score"].Value);


            //Checks if the attributes are empty, if not, then curscore gets highest score value.
            for (int i = 0; i < xmlnodes.Count; i++)
                if (Int32.Parse(xmlnodes[i].Attributes["score"].Value) > curScore)
                    if (xmlnodes[i].Attributes["name"].Value != "")
                        curScore = Int32.Parse(xmlnodes[i].Attributes["score"].Value);
            //Returns the score as a string
            return curScore.ToString();
        }

        public static void WriteScores()
        {
            HighScoresScene scene = Program.game.GetScene<HighScoresScene>();
            //Checks if XML exists, if so, loads it
            if (!System.IO.File.Exists("savefile.xml"))
                return;
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("savefile.xml");
            
            XmlNodeList xmlElements = xmlDoc.DocumentElement.ChildNodes;
            List<XmlElement> xmlnodes = new List<XmlElement>();
            //Adds xml elements to a list
            foreach (XmlElement i in xmlElements)
                xmlnodes.Add(i);

            int count;
            //Checks list count
            if (xmlnodes.Count < 5)
                count = xmlnodes.Count;
            else
                count = 5;
            //For loop for going through top 5 scores
            for (int i = 0; i < count; i++)
            {
                XmlElement curElement = xmlnodes[0];

                RichText name = new RichText();
                RichText score = new RichText();
                //Checks if the attributes are empty, if not, then sets current element the score value
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

                //Sets position and value for the names and scores displayd on the screen.
                name.String = curElement.Attributes["name"].Value;
                name.SetPosition(8, i * 50 + 116);
                scene.hslb.AddGraphic(name);
                score.String = curElement.Attributes["score"].Value;
                score.SetPosition(200, i * 50 + 116);
                scene.hslb.AddGraphic(score);
                xmlnodes.Remove(curElement);
            }
        }
    }
}
