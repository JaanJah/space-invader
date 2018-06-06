using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace space_invader
{
    class Button : Entity
    {
        
        public Button (float x, float y) : base(x, y)
        {
            Image buttonOutline = new Image("../../../Assets/buttonOutline.png");
            AddGraphic(buttonOutline);
        }

        public override void Update()
        {
            base.Update();

            if (Input.MouseButtonPressed(MouseButton.Left))
            {
                if (Util.InRect(Scene.MouseX, Scene.MouseY, X,Y, 80, 30))
                {
                    var inputText = Scene.GetEntity<TextBox>().inputString;
                    File.WriteAllText("../../../savefile/save.txt", inputText);
                    MainScene scene = (MainScene)Program.game.FirstScene;

                    if (!File.Exists("savefile.xml"))
                    {
                        var place = new Leaderboard() { Name = inputText, Score = scene.curScoreTxt.String};
                        var serializer = new XmlSerializer(place.GetType());
                        using (var writer = XmlWriter.Create("savefile.xml"))
                        {
                            serializer.Serialize(writer, place);
                        }
                        
                    }
                    else
                    {
                        var newSerializer = new XmlSerializer(typeof(Leaderboard));
                        var newPlace = new Leaderboard() { Name = inputText, Score = scene.curScoreTxt.String };
                        using (var reader = XmlReader.Create("savefile.xml"))
                        {
                            newPlace = (Leaderboard)newSerializer.Deserialize(reader);
                        }
                        
                        using (var writer = XmlWriter.Create("savefile.xml"))
                        {
                            newSerializer.Serialize(writer, newPlace);
                        }
                    }

                    // debug
                    Console.WriteLine("MouseX: {0}, MouseY: {1}", Scene.MouseX, Scene.MouseY);
                }
            }
        }

    }
}
