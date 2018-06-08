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
    static class SceneLoader
    {
        static IDictionary<string, Func<Graphic>> NodeTypes = new Dictionary<string, Func<Otter.Graphic>>();

        public static void Initialize()
        {
            NodeTypes.Add(new KeyValuePair<string, Func<Graphic>("texture", XMLLoader.LoadTexture()>)
        }

        public static void 

        public static Scene Load(string filename)
        {
            // Create scene
            Scene scene = new Scene();

            // open XML document
            XDocument doc = new XDocument(Program.game.Filepath + filename);

            // Get all nodes
            IEnumerable<XElement> nodes = doc.Root.Elements();

            foreach(XElement node in nodes)
            {
                foreach (XAttribute type in node.Attributes())
                {
                    if (NodeTypes.ContainsKey(type.Value))
                    {
                        Console.WriteLine("test");
                    }
                }
            }

            return scene;
        }
    }
}
