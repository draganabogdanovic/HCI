
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.IO;

namespace Aplikacija.Modeli
{
    class BazaPodataka
    {

        
        

        public ObservableCollection<Tip> Tipovi
        {
            get;
            set;
        }

        public ObservableCollection<Dogadjaj> Dogadjaji
        {
            get;
            set;
        }

        public ObservableCollection<Etiketa> Etikete
        {
            get;
            set;
        }

      
    }
}
