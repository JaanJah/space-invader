using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;


namespace space_invader
{
    public class Leaderboard
    {
        public string Name { get; set; }
        public string Score { get; set; }
        public Leaderboard()
        {
            XmlDocument xmlDoc = new XmlDocument();
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
        }
    }
}
