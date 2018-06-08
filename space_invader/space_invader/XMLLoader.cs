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
    static class XMLLoader
    {
        public static void LoadImage(XElement xNode)
        {
            // Load image and set name
            Image image = new Image(xNode.Attribute("path").Value);
            image.Name = xNode.Attribute("name").Value;

            // Add image to resourceManager
            Program.resourceManager.AddGraphic(image);
        }
    }
}
