using System;
using System.ComponentModel;



namespace Aplikacija.Modeli
{
    [Serializable]
    public class Etiketa : INotifyPropertyChanged
    {
        private string oznaka;
        private string opis;
        private string boja;

        public Etiketa(String o, String op, String c)
        {

            oznaka = o;
            opis = op;
            boja = c;

        }
        public Etiketa()
        {

        }

        public string Oznaka
        {
            get
            {
                return oznaka;
            }
            set
            {
                if (value != oznaka)
                {
                    oznaka = value;
                    OnPropertyChanged("Oznaka");
                }
            }
        }

        public String Boja
        {
            get
            {
                return boja;
            }
            set
            {
                if (value != boja)
                {
                    boja = value;
                    OnPropertyChanged("Boja");
                }
            }
        }

        public string Opis
        {
            get
            {
                return opis;
            }
            set
            {
                if (value != opis)
                {
                    opis = value;
                    OnPropertyChanged("Opis");
                }
            }
        }



        [field:NonSerialized]public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

     
    }
}
