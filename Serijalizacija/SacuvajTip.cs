using Aplikacija.Modeli;
using System.Collections.ObjectModel;
using System.IO;
using System.Xml.Serialization;

namespace Aplikacija.Serijalizacija
{
    public class SacuvajTip
    {
        private static string file = "tipovi.xml";

        public SacuvajTip()
        { }

        public static void serijalizacijaTipa()
        {
            using (Stream s = new FileStream(file, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<Tip>));
                serializer.Serialize(s, MainWindow.Tipovi);
            }



        }

        public static void deserijalizacijaTipa()
        {
            if (File.Exists(file) == false)

                serijalizacijaTipa();

            XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<Tip>));

            using (FileStream fs = File.OpenRead(file))
            {
                MainWindow.Tipovi = (ObservableCollection<Tip>)serializer.Deserialize(fs);
            }
        }
    }
}
