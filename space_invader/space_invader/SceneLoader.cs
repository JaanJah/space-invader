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
        static IDictionary<string, Action<XElement>> NodeTypes = new Dictionary<string, Action<XElement>>();
        static IDictionary<string, Func<Scene>> SceneTypes = new Dictionary<string, Func<Scene>>();

        public static void Initialize()
        {
            InitializeNodeTypes();
            InitializeSceneTypes();
        }

        static void InitializeNodeTypes()
        {
            NodeTypes["image"] = new Action<XElement>(XMLLoader.LoadImage);
            NodeTypes["barricade"] = new Action<XElement>(XMLLoader.LoadBarricade);
        }

        static void InitializeSceneTypes()
        {
            SceneTypes["mainscene"] = new Func<Scene>(add)
        }

        public static Scene Load(string filepath)
        {
            // open XML document
            XDocument doc = XDocument.Load(Program.game.GameFolder + filepath);

            // Get scene type
            string SceneName = "space_invader." + doc.Root.Attribute("class").Value;
            Type sceneType = Type.GetType(SceneName);
            var a = sceneType.GetType().GetRuntimeMethods();
            // Create scene
            dynamic scene = Convert.ChangeType(Assembly.GetExecutingAssembly().CreateInstance(SceneName), sceneType);
            Scene convertedSene = (Scene)scene;
            Program.game.AddScene(scene);

            // Get all nodes
            IEnumerable<XElement> nodes = doc.Descendants();

            // Loop through each node in nodes
            foreach (XElement node in nodes)
            {
                // Loop through each node type
                foreach (KeyValuePair<string, Action<XElement>> type in NodeTypes)
                {
                    // Check if node name is equal to node type
                    if (node.Name == type.Key)
                    {
                        // Call load function
                        type.Value(node);
                    }
                }
            }

            return scene;
        }
    }
}
