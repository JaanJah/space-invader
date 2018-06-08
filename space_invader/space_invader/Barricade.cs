using System;
using System.Collections.Generic;
using Otter;
using System.Xml;

namespace space_invader
{
    /// <summary>
    /// Barricades stop bullets.
    /// </summary>
    class Barricade : Entity
    {
		// Size of barricade block
        static Vector2 Size = new Vector2(24, 24);
        
		// Current barricade block Image Counter
        int CurImage = 0;

		/// <summary>
		/// Create Barricade.
		/// </summary>
        public Barricade()
        {
            // Get barrricade image
            Image image = (Image)Program.resourceManager.GetGraphic("wall0");

			// Create collider
            Collider collider = new BoxCollider(image.Width, image.Height, Tags.Barricade);
            
			// Add image and collider
            AddGraphic(image);
            AddCollider(collider);
        }

        /// <summary>
        /// Remove barricade health and change image.
        /// </summary>
        public void TakeDamage()
        {
            CurImage++;

            // Remove barricade
            if (CurImage > 3)
            {
                RemoveSelf();
                return;
            }

            // Change Image
            RemoveGraphic(Graphic);
            AddGraphic(Program.resourceManager.GetGraphic("wall" + CurImage));
        }

        /// <summary>
        /// Initialize images and barricades.
        /// </summary>
        public static void Initialize()
        {
            InitializeImages();
            InitializeBarricades();
        }
        

        /// <summary>
        /// Load barricade images.
        /// </summary>
        static void InitializeImages()
        {
            for (int i = 0; i < 4; i++)
            {
                // Load image
                Image image = new Image(Program.game.GameFolder + "/Barricades/wall" + i + ".png");

                // Set image name
                image.Name = "wall" + i.ToString(); 

                // Add image to resourceManager
                Program.resourceManager.AddGraphic(image);
            }
        }

        /// <summary>
        /// Spawn barricades.
        /// </summary>
        static void InitializeBarricades()
        {
            // Open XML document
            XmlDocument doc = new XmlDocument();
            doc.Load("barricades.xml");

            // Loop through each node
            foreach (XmlElement node in doc.DocumentElement.ChildNodes)
			{
                // Spawn 4 barricades
                for (int i = 0; i < 4; i++)
                {
					// Create barricade
                    Barricade barricade = new Barricade();

					// Set barricade position
                    barricade.Position = new Vector2(Convert.ToSingle(node.GetAttribute("posx")) + i * 200,
													 Convert.ToSingle(node.GetAttribute("posy")));

					// Add barricade to scene
                    Program.game.GetScene<MainScene>().Add(barricade);
                }
			}
        }
    }
}
