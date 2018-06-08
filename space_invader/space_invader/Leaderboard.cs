using System.Xml;


namespace space_invader
{
    /// <summary>
    /// Leaderboard class
    /// </summary>
    public static class Leaderboard
    {
        public static string SaveDirectory = "savefile.xml";

        public static void AddScore(string Name, string Score)
        {
            //Writes the XML elements and attributes
            XmlDocument xmlDoc = new XmlDocument();
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
        }
    }
}
