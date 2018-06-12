using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Reflection;
using System.Xml.Linq;
using Otter;

namespace space_invader
{
    static class SceneLoader
    {
        static IDictionary<string, Action<XElement, Scene>> NodeTypes = new Dictionary<string, Action<XElement, Scene>>();
        static IDictionary<string, Func<Scene>> SceneTypes = new Dictionary<string, Func<Scene>>();

        public static void Initialize()
        {
            InitializeNodeTypes();
            InitializeSceneTypes();
        }

        static void InitializeNodeTypes()
        {
            NodeTypes["image"] = new Action<XElement, Scene>(XMLLoader.LoadImage);
            NodeTypes["barricade"] = new Action<XElement, Scene>(XMLLoader.LoadBarricade);
        }

        static void InitializeSceneTypes()
        {
            SceneTypes.Add("MainScene", () => { return new MainScene(); });
        }

        public static Scene Load(string filepath)
        {
            // open XML document
            XDocument doc = XDocument.Load(Program.game.GameFolder + filepath);

            // Create scene
            Scene scene = null;

            foreach (KeyValuePair<string, Func<Scene>> type in SceneTypes)
            {
                if (type.Key == doc.Root.Attribute("class").Value)
                {
                    scene = type.Value();
                    break;
                }

                throw new Exception("Scene not found.");
            }

            // Get all nodes
            IEnumerable<XElement> nodes = doc.Descendants();

            // Loop through each node in nodes
            foreach (XElement node in nodes)
            {
                // Loop through each node type
                foreach (KeyValuePair<string, Action<XElement, Scene>> type in NodeTypes)
                {
                    // Check if node name is equal to node type
                    if (node.Name == type.Key)
                    {
                        // Call load function
                        type.Value(node, scene);
                    }
                }
            }

            return scene;
        }
    }
}
