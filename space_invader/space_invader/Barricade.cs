﻿using System;
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
        static Vector2 Size = new Vector2(24, 24);
        static List<Image> Images = new List<Image>();
        
        int CurImage = 0;

        public Barricade()
        {
            Collider collider = new BoxCollider(Images[0].Width, Images[0].Height, Tags.Barricade);
            
            AddGraphic(Images[0]);
            AddCollider(collider);
        }

        /// <summary>
        /// Removes barricade health.
        /// </summary>
        public void TakeDamage()
        {
            CurImage++;

            if (CurImage > 3)
            {
                RemoveSelf();
                return;
            }
                
            // Change Image
            RemoveGraphic(Images[CurImage - 1]);
            AddGraphic(Images[CurImage]);
        }

        /// <summary>
        /// Initializes images and barricades.
        /// </summary>
        public static void Initialize()
        {
            InitializeImages();
            InitializeBarricades();
        }
        

        /// <summary>
        /// Loads all images.
        /// </summary>
        static void InitializeImages()
        {
            Image block100 = new Image("Assets/wall100.png");
            Image block75 = new Image("Assets/wall75.png");
            Image block50 = new Image("Assets/wall50.png");
            Image block25 = new Image("Assets/wall25.png");

            Images.Add(block100);
            Images.Add(block75);
            Images.Add(block50);
            Images.Add(block25);
        }

        /// <summary>
        /// Loads in barricades
        /// </summary>
        static void InitializeBarricades()
        {
            // Open XML document
            XmlDocument doc = new XmlDocument();
            doc.Load("barricades.xml");

            // Loop through each node to spawn barricade
            
            foreach (XmlElement node in doc.DocumentElement.ChildNodes)

                // Spawn 4 barricades
                for (int i = 0; i < 4; i++)
                {
                    Barricade barricade = new Barricade();

                    barricade.Position = new Vector2(Convert.ToSingle(node.GetAttribute("posx")) + i * 200, Convert.ToSingle(node.GetAttribute("posy")));

                    Program.game.GetScene<MainScene>().Add(barricade);
                }
        }
    }
}
