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
        static IDictionary<string, Action<XElement>> NodeTypes = new Dictionary<string, Action<XElement>>();
        static IDictionary<string, Func<>>

        public static void Initialize()
        {
            NodeTypes["texture"] = new Action<XElement>(XMLLoader.LoadImage);
        }

        public static Scene Load(string filepath)
        {
            // Create scene
            Scene scene = new Scene();

            // open XML document
            XDocument doc = XDocument.Load(Program.game.GameFolder + filepath);

            // Get all nodes
            IEnumerable<XElement> nodes = doc.Root.Elements();

            foreach(XElement node in nodes)
            {
                foreach (KeyValuePair<string, Action<XElement>> type in NodeTypes)
                {
                    if (node.Name == type.Key)
                    {
                        type.Value(node);
                    }
                }
            }

            return scene;
        }
    }
}
