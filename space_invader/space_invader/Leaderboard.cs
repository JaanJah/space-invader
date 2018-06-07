using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;


namespace space_invader
{
    public static class Leaderboard
    {
        public static string SaveDirectory = "savefile.xml";

        public static void AddScore(string Name, string Score)
        {
            XmlDocument xmlDoc = new XmlDocument();
<<<<<<< HEAD
            XmlElement root;
            if (!System.IO.File.Exists(SaveDirectory))
            {
                root = xmlDoc.CreateElement("leaderboard");
                xmlDoc.AppendChild(root);
            }

            else
            {
                xmlDoc.Load(SaveDirectory);
                root = xmlDoc.DocumentElement;
            }
            
            XmlElement playerNode = xmlDoc.CreateElement("player");

            playerNode.SetAttribute("name", Name);
            playerNode.SetAttribute("score", Score);

            root.AppendChild(playerNode);

            xmlDoc.Save(SaveDirectory);
=======
            XmlNode rootNode = xmlDoc.CreateElement("leaderboard");
            xmlDoc.AppendChild(rootNode);

            XmlNode playerNode = xmlDoc.CreateElement("player");

            XmlAttribute nameAttribute = xmlDoc.CreateAttribute("name");
            XmlAttribute scoreAttribute = xmlDoc.CreateAttribute("score");
            nameAttribute.Value = Name;
            scoreAttribute.Value = Score;
            playerNode.Attributes.Append(nameAttribute);
            playerNode.Attributes.Append(scoreAttribute);
            rootNode.AppendChild(playerNode);
>>>>>>> aftergame
        }
    }
}
