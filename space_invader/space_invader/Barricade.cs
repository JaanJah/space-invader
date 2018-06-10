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
    }
}
