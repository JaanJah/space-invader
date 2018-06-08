using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Otter;

namespace space_invader
{
    static class SceneLoader
    {
        static IDictionary<string, Func<Graphic>> NodeTypes = new Dictionary<string, Func<Otter.Graphic>>();

        public static void Initialize()
        {

        }

           

        public static Scene Load(string filename)
        {
            // Create scene
            Scene scene = new Scene();

            // Create XML document
            XmlDocument doc = new XmlDocument();
            doc.Load(Program.game.Filepath + filename);

            // Get root element
            XmlElement root = doc.DocumentElement;

            // Get all nodes
            XmlNodeList nodes = root.SelectNodes()

            while ()

            return scene;
        }
    }
}
