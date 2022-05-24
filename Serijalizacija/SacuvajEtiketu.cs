using Aplikacija.Modeli;
using System.Collections.ObjectModel;
using System.IO;
using System.Xml.Serialization;

namespace Aplikacija.Serijalizacija
{
    public class SacuvajEtiketu
    {
        private static string file = "etikete.xml";

        public SacuvajEtiketu()
        { }

        public static void serijalizacijaEtikete()
        {
            using (Stream s = new FileStream(file, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<Etiketa>));
                serializer.Serialize(s, MainWindow.Etikete);
            }



        }

        public static void deserijalizacijaEtikete()
        {
            if (File.Exists(file) == false)

                serijalizacijaEtikete();

            XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<Etiketa>));

            using (FileStream fs = File.OpenRead(file))
            {
                MainWindow.Etikete = (ObservableCollection<Etiketa>)serializer.Deserialize(fs);
            }
        }
    }
}
