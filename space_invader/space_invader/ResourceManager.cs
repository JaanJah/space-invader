using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Otter;

namespace space_invader
{
    /// <summary>
    /// Handle Otter resources
    /// </summary>
    class ResourceManager
    {
        // Hold all file loaded graphics
        public List<Graphic> graphics;

        /// <summary>
        /// Create ResourceManager.
        /// </summary>
        public ResourceManager()
        {
            graphics = new List<Graphic>();
        }

        /// <summary>
        /// Add graphic to graphics.
        /// </summary>
        /// <param name="graphic">Graphic to add</param>
        /// 

        public void AddGraphic(Graphic graphic)
        {
            graphics.Add(graphic);
        }

        /// <summary>
        /// Returns graphic with the same name.
        /// Returns first instance when name is found.
        /// Throws excpetion with message "Graphic not found" if graphic is not found.
        /// </summary>
        /// <param name="name">Graphic name to search</param>
        /// <returns>returns found graphic</returns>
        public Graphic GetGraphic(string name)
        {
            // Loop through each graphic in graphics
            for (int i = 0; i < graphics.Count; i++)
            {
                // Current graphic to check
                Graphic curGraphic = graphics[i];

                // Check if curGraphic.name is equal to name
                if (curGraphic.Name == name)
                {
                    return curGraphic;
                }
            }
            
            // Throw error if graphic not found
            throw new Exception("Graphic not found");
        }
    }
}
