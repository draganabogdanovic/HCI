using System;
using System.ComponentModel;
using System.Windows.Media.Imaging;

namespace Aplikacija.Modeli
{
    [Serializable]
    public class Tip : INotifyPropertyChanged
    {
        private string oznaka;
        private string naziv;
        private string opis;
        private string ikonica;
        

        public Tip(String o, String n, String op, String ik)
        {
            oznaka = o;
            naziv = n;
            opis = op;
            ikonica = ik;
            
        }

        public Tip()
        {
           
        }


        public BitmapImage Ikona
        {
            get
            {
                Uri uri = new Uri(ikonica);
                BitmapImage bmpimg = null;
                try
                {
                    bmpimg = new BitmapImage(uri);
                }
                catch (Exception e)
                {

                }
                return bmpimg;
            }
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

        public string Naziv
        {
            get
            {
                return naziv;
            }
            set
            {
                if(value != naziv)
                {
                    naziv = value;
                    OnPropertyChanged("Naziv");
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

        public String Ikonica
        {
            get
            {
                return ikonica;
            }
            set
            {
                if (value != ikonica)
                {
                    ikonica = value;
                    OnPropertyChanged("Ikonica");
                }
            }
        }


        public static implicit operator Tip(string v)
        {
            throw new NotImplementedException();
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
