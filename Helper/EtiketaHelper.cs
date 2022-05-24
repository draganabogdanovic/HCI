using Aplikacija.Modeli;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.IO;

namespace Aplikacija.Helper
{
    public class EtiketaHelper
    {
        public void JsonSerialize(ObservableCollection<Etiketa> events, string fileName)
        {
            JsonSerializer serializer = new JsonSerializer();
            serializer.NullValueHandling = NullValueHandling.Ignore;

            using (StreamWriter sw = new StreamWriter(fileName))
            {
                using (JsonWriter writer = new JsonTextWriter(sw))
                {
                    serializer.Serialize(writer, events);
                }
            }
        }

        public ObservableCollection<Etiketa> JsonDeserialize(string fileName)
        {
            using (StreamReader file = File.OpenText(fileName))
            {
                JsonSerializer serializer = new JsonSerializer();
                ObservableCollection<Etiketa> e = (ObservableCollection<Etiketa>)serializer.Deserialize(file, typeof(ObservableCollection<Etiketa>));
                return e;
            }
        }
    }
}
