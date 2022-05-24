using Aplikacija.Modeli;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.IO;

namespace Aplikacija.Helper
{
    public class DogadjajHelper
    {
        public void JsonSerialize(ObservableCollection<Dogadjaj> events, string fileName)
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

        public ObservableCollection<Dogadjaj> JsonDeserialize(string fileName)
        {
            using (StreamReader file = File.OpenText(fileName))
            {
                JsonSerializer serializer = new JsonSerializer();
                ObservableCollection<Dogadjaj> d = (ObservableCollection<Dogadjaj>)serializer.Deserialize(file, typeof(ObservableCollection<Dogadjaj>));
                return d;
            }
        }

        public void saveMapIndex(int mapIndex, string fileName)
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(fileName))
            {
                file.WriteLine(mapIndex);
            }
        }

        public int loadMapIndex(string fileName)
        {
            string index;
            using (System.IO.StreamReader file = new System.IO.StreamReader(fileName))
            {
                index = file.ReadToEnd();
            }
            return int.Parse(index);
        }
    }
}
