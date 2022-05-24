using System;
using System.Collections.ObjectModel;
using System.ComponentModel;


namespace Aplikacija.Modeli
{
    [Serializable]
    public class Dogadjaj : INotifyPropertyChanged
    {
        private string oznaka;
        private string naziv;
        private Tip tip = new Tip();
        private ObservableCollection<Etiketa> etikete = new ObservableCollection<Etiketa>();
        private string opis;
        private string ikonica;
        private string posecenost;
        private string cenaOdrzavanja;
        private int status;
        private string mesto;
        private String datum;
        private string istorija;
        private bool humanitarno = false;
        private double x = -1;
        private double y = -1;

        public double X
        {
            get
            {
                return x;
            }
            set
            {
                if (value != x)
                    x = value;
                OnPropertyChanged("X");
            }
        }

        public double Y
        {
            get
            {
                return y;
            }
            set
            {
                if (value != y)
                    y = value;
                OnPropertyChanged("Y");
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
                if(value != oznaka)
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

        public Tip Tip
        {
            get
            {
                return tip;
            }
            set
            {
                if(value != tip)
                {
                    tip = value;
                    OnPropertyChanged("Tip");
                }
            }

        }


        public ObservableCollection<Etiketa> Etikete
        {
            get
            {
                return etikete;
            }
            set
            {
                if (value != etikete)
                {
                    etikete = value;
                    OnPropertyChanged("Etikete");
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

        public string Posecenost
        {
            get
            {
                return posecenost;
            }
            set
            {
                if (value != posecenost)
                {
                    posecenost = value;
                    OnPropertyChanged("Posecenost");
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

       

        public string CenaOdrzavanja
        {
            get
            {
                return cenaOdrzavanja;
            }
            set
            {
                if (value != cenaOdrzavanja)
                {
                    cenaOdrzavanja = value;
                    OnPropertyChanged("Cena odrzavanja");
                }
                    
            }
        }

        public string Istorija
        {
            get
            {
                return istorija;
            }
            set
            {
                if (value != istorija)
                {
                    istorija = value;
                    OnPropertyChanged("Istorija");
                }

            }
        }

        public int TuristickiStatus
        {
            get
            {
                return status;
            }
            set
            {
                if (value != status)
                {
                    status = value;
                    OnPropertyChanged("TuristickiStatus");
                }
            }
        }

        public string Mesto
        {
            get
            {
                return mesto;
            }

            set
            {

                if (value != mesto)
                {
                    mesto = value;
                    OnPropertyChanged("Mesto");
                }
            }
        }

        public string Datum
        {
            get
            {
                return datum;
            }
            set
            {
                if (value != datum)
                {
                    datum = value;
                    OnPropertyChanged("Datum");
                }
            }
        }

        public bool Humanitarno
        {
            get
            {
                return humanitarno;
            }
            set
            {
                if(value != humanitarno)
                {
                    humanitarno = value;
                    OnPropertyChanged("Humanitarno");
                }
            }
        }

        public Dogadjaj()
        {


        }

        public Dogadjaj(Dogadjaj resource)
        {
            oznaka = resource.oznaka;
            naziv = resource.naziv;
            opis = resource.opis;
            datum = resource.datum;
         
            cenaOdrzavanja = resource.cenaOdrzavanja;
            
            etikete = new ObservableCollection<Etiketa>(resource.etikete);
            x = resource.X;
            y = resource.Y;
        }


        public Dogadjaj(string oz, string n, Tip t, ObservableCollection<Etiketa> e, string op, string ik, string p, string co, int s, string m, String d, bool h, string ist)
        {
            this.oznaka = oz;
            this.naziv = n;
            this.tip = t;
            this.etikete = e;
            this.opis = op;
            this.ikonica = ik;
            this.posecenost = p;
            this.cenaOdrzavanja = co;
            this.status = s;
            this.mesto = m;
            this.datum = d;
            this.humanitarno = h;
            this.istorija = ist;

        }

       
        [field: NonSerialized]public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        public static implicit operator Dogadjaj(string v)
        {
            throw new NotImplementedException();
        }
    }
}
