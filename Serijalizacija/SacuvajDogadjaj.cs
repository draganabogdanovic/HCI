using Aplikacija.Modeli;
using System.Collections.ObjectModel;
using System.IO;
using System.Xml.Serialization;

namespace Aplikacija.Serijalizacija
{
    public class SacuvajDogadjaj
    {
        private static string file = "spomenici.xml";

        public SacuvajDogadjaj()
        { }

        public static void serijalizacijaDogadjaja()
        {
            using (Stream s = new FileStream(file, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<Dogadjaj>));
                serializer.Serialize(s, MainWindow.Dogadjaji);
            }



        }

        public static void deserijalizacijaDogadjaja()
        {
            if (File.Exists(file) == false)

                serijalizacijaDogadjaja();

            XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<Dogadjaj>));

            using (FileStream fs = File.OpenRead(file))
            {
                MainWindow.Dogadjaji = (ObservableCollection<Dogadjaj>)serializer.Deserialize(fs);
            }
        }
    }
}
