using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.IO;

namespace Aplikacija.Helper
{
    public class TipHelper
    {
        public void JsonSerialize(ObservableCollection<Modeli.Tip> events, string fileName)
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

        public ObservableCollection<Modeli.Tip> JsonDeserialize(string fileName)
        {
            using (StreamReader file = File.OpenText(fileName))
            {
                JsonSerializer serializer = new JsonSerializer();
                ObservableCollection<Modeli.Tip> types = (ObservableCollection<Modeli.Tip>)serializer.Deserialize(file, typeof(ObservableCollection<Modeli.Tip>));
                return types;
            }
        }
    }
}
