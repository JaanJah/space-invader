using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;
using System.Xml;

namespace space_invader
{
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

        public void TakeDamage()
        {
            CurImage++;

            if (CurImage > 3)
            {
                RemoveSelf();
                return;
            }
                
            RemoveGraphic(Images[CurImage - 1]);
            AddGraphic(Images[CurImage]);
        }

        public static void Initialize()
        {
            InitializeImages();
            InitializeBarricades();
        }
        
        static void InitializeImages()
        {
            Image block100 = new Image("../../../Assets/wall100.png");
            Image block75 = new Image("../../../Assets/wall75.png");
            Image block50 = new Image("../../../Assets/wall50.png");
            Image block25 = new Image("../../../Assets/wall25.png");

            Images.Add(block100);
            Images.Add(block75);
            Images.Add(block50);
            Images.Add(block25);
        }

        static void InitializeBarricades()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("../../../barricades.xml");

            foreach (XmlElement node in doc.DocumentElement.ChildNodes)
                for (int i = 0; i < 4; i++)
                {
                    Barricade barricade = new Barricade();

                    barricade.Position = new Vector2(Convert.ToSingle(node.GetAttribute("posx")) + i * 200, Convert.ToSingle(node.GetAttribute("posy")));
                    
                    Console.WriteLine(Program.game.Scene);
                    Program.game.FirstScene.Add(barricade);
                }
        }
    }
}
