using Aplikacija.Modeli;
using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Aplikacija.Dijalozi
{
    
    public partial class DodajVrstuForma : Window, INotifyPropertyChanged
    {
        
        public static Dogadjaj trenutniDogadjaj;
        private MainWindow mw;
        private string slika;

        public DodajVrstuForma()
        {
            this.DataContext = this;
            Tipovi = MainWindow.Tipovi;
            Etikete = MainWindow.Etikete;
            Dogadjaji = MainWindow.Dogadjaji;
            IzabraneEtikete = new ObservableCollection<Etiketa>();
            SlobodneEtikete = new ObservableCollection<Etiketa>();

            foreach(Etiketa et in MainWindow.Etikete)
            {
                SlobodneEtikete.Add(et);
            }

            InitializeComponent();
            Tip.SelectedIndex = 0;
            Tip_SelectionChanged(null, null);

            trenutniDogadjaj = null;

        }

        public DodajVrstuForma(Dogadjaj s, MainWindow mwi)
        {
            
            this.DataContext = this;
            Tipovi = MainWindow.Tipovi;
            Etikete = MainWindow.Etikete;
            Dogadjaji = MainWindow.Dogadjaji;
            SlobodneEtikete = new ObservableCollection<Etiketa>();

            IzabraneEtikete = s.Etikete;

            
            foreach(Etiketa et in MainWindow.Etikete)
            {
                SlobodneEtikete.Add(et);
            }

            
            foreach(Etiketa et in s.Etikete)
            {
                SlobodneEtikete.Remove(et);
            }

            trenutniDogadjaj = s;
            InitializeComponent();

            Oznaka.Text = s.Oznaka;
            Naziv.Text = s.Naziv;
            Opis.Text = s.Opis;
            Istorija.Text = s.Istorija;

            if(s.Humanitarno == true)
            {
                HumanitarnoDA.IsChecked = true;
            }
            else if(s.Humanitarno == false)
            {
                HumanitarnoNE.IsChecked = true;
            }
            Posecenost.SelectedItem = s.Posecenost;
            Mesto.Text = s.Mesto;
            Cena.Text = s.CenaOdrzavanja;
            Datum.SelectedDate = DateTime.Parse(s.Datum);

            img.Source = new BitmapImage(new Uri(s.Ikonica));
            Tip.SelectedItem = s.Tip;
            mw = mwi;


        }

        private System.Diagnostics.Process _p = null;
        private void cmdToggle_Click(object sender, RoutedEventArgs e)
        {
            if (_p == null)
                _p = System.Diagnostics.Process.Start("osk.exe");
            else
            {
                _p.Kill();
                _p.Dispose();
                _p = null;
            }
        }

        private void BtnIzaberi_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dijalog = new OpenFileDialog();
            dijalog.Title = "Odaberite sliku";
            dijalog.Filter = "Slike|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (dijalog.ShowDialog() == true)
            {
                img.Source = new BitmapImage(new Uri(dijalog.FileName));
                slika = dijalog.FileName;
            }
        }

        private void Tip_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Tip.SelectedValue != null && img != null)
            {
                Tip t = (Tip)Tip.SelectedValue;
                img.Source = t.Ikona;
                
                Slika = t.Ikonica;
            }
        }

        private void OdustaniBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        private void BtnDodaj_Click(object sender, RoutedEventArgs e)
        {
            //validacija
            bool vOznaka = false;
            if(Oznaka.Text == "")
            {
                vOznaka = false;
                Oznaka.Background = Brushes.PaleVioletRed;
                System.Windows.MessageBox.Show("Morate uneti oznaku!");
                return;
            }
            else
            {
                vOznaka = true;
            }
            bool vNaziv = false;
            if (Naziv.Text == "")
            {
                vNaziv = false;
                Naziv.Background = Brushes.PaleVioletRed;
                System.Windows.MessageBox.Show("Morate uneti naziv!");
                return;
            }
            else
            {
                vNaziv = true;
            }

            bool humanitarno;
 
            if(HumanitarnoDA.IsChecked == true)
            {
                humanitarno = true;
            }
            else
            {
                humanitarno = false;
            }

            if(trenutniDogadjaj == null)
            {
                Dogadjaj s = new Dogadjaj
                {
                    Naziv = Naziv.Text,
                    Oznaka = Oznaka.Text,
                    Opis = Opis.Text,
                    Tip = (Tip)Tip.SelectedItem,
                    Etikete = IzabraneEtikete,
                    Mesto = Mesto.Text,
                    Datum = Datum.ToString(),
                    CenaOdrzavanja = Cena.Text,
                    Humanitarno = humanitarno,
                    Istorija = Istorija.Text,
                    Ikonica = img.Source.ToString(),
                };

                foreach (Dogadjaj sp in MainWindow.Dogadjaji)
                {
                    if (sp.Oznaka.Equals(Oznaka.Text))
                    {
                        System.Windows.MessageBox.Show("Događaj s ovom oznakom već postoji!");
                        return;
                    }
                }

                Tip t = (Tip)Tip.SelectedItem;

                MainWindow.Dogadjaji.Add(s);

                System.Windows.MessageBox.Show("Uspešno dodat događaj!");

            }
            else
            {
                
                foreach (Dogadjaj sp in MainWindow.Dogadjaji)
                {
                    if (sp.Oznaka.Equals(Oznaka.Text) && !(sp.Oznaka.Equals(trenutniDogadjaj.Oznaka)))
                    {
                        System.Windows.MessageBox.Show("Događaj s ovom oznakom već postoji!");
                        return;
                    }
                }

                trenutniDogadjaj.Naziv = Naziv.Text;
                trenutniDogadjaj.Ikonica = Slika;
                trenutniDogadjaj.Oznaka = Oznaka.Text;
                trenutniDogadjaj.Opis = Opis.Text;

                trenutniDogadjaj.Mesto = Mesto.Text;
                trenutniDogadjaj.Humanitarno = humanitarno;
                trenutniDogadjaj.Istorija = Istorija.Text;
                trenutniDogadjaj.Datum = Datum.ToString();
                trenutniDogadjaj.Etikete = IzabraneEtikete;
                trenutniDogadjaj.CenaOdrzavanja = Cena.Text;
                trenutniDogadjaj.Tip = (Tip)Tip.SelectedItem;

            }
           
            this.Close();
        }


        private void BtnOdaberi_MouseEnter(object sender, MouseEventArgs e)
        {
            btnOdaberi.Foreground = new SolidColorBrush(Colors.Black);
        }

        private void BtnOdaberi_MouseLeave(object sender, MouseEventArgs e)
        {
            btnOdaberi.Foreground = new SolidColorBrush(Colors.White);
        }


        private void BtnDodaj_MouseEnter(object sender, MouseEventArgs e)
        {
            btnDodaj.Foreground = new SolidColorBrush(Colors.Black);
        }

        private void BtnDodaj_MouseLeave(object sender, MouseEventArgs e)
        {
            btnDodaj.Foreground = new SolidColorBrush(Colors.White);
        }

        private void OdustaniBtn_MouseEnter(object sender, MouseEventArgs e)
        {
            OdustaniBtn.Foreground = new SolidColorBrush(Colors.Black);
        }

        private void OdustaniBtn_MouseLeave(object sender, MouseEventArgs e)
        {
            OdustaniBtn.Foreground = new SolidColorBrush(Colors.White);
        }

        private void DodajUIzabrane_Click(object sender, RoutedEventArgs e)
        {
            if (Slobodne.SelectedValue != null)
            {
                Etiketa et = (Etiketa)Slobodne.SelectedValue;
                IzabraneEtikete.Add(et);
                SlobodneEtikete.Remove(et);
            }
        }

        private void DodajUSlobodne_Click(object sender, RoutedEventArgs e)
        {
            if (Izabrane.SelectedValue != null)
            {
                Etiketa et = (Etiketa)Izabrane.SelectedValue;
                SlobodneEtikete.Add(et);
                IzabraneEtikete.Remove(et);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }



        public string Slika
        {
            get
            {
                return slika;
            }
            set
            {
                if (value != slika)
                {
                    slika = value;
                    OnPropertyChanged("Slika");
                }
            }
        }



        public ObservableCollection<Etiketa> SlobodneEtikete
        {
            get;
            set;
        }
        public ObservableCollection<Etiketa> IzabraneEtikete
        {
            get;
            set;
        }

        public ObservableCollection<Tip> Tipovi
        {
            get;
            set;
        }

        public ObservableCollection<Etiketa> Etikete
        {
            get;
            set;
        }

        public ObservableCollection<Dogadjaj> Dogadjaji
        {
            get;
            set;
        }

        private void Oznaka_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!Oznaka.Text.Equals(""))
            {
                ValOznaka.Visibility = Visibility.Hidden;
                Oznaka.Background = Brushes.White;

            }
            else
            {
                ValOznaka.Visibility = Visibility.Visible;
                Oznaka.Background = Brushes.PaleVioletRed;
            }
        }

        private void Ime_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!Naziv.Text.Equals(""))
            {
                ValIme.Visibility = Visibility.Hidden;
                Naziv.Background = Brushes.White;

            }
            else
            {
                ValIme.Visibility = Visibility.Visible;
                Naziv.Background = Brushes.PaleVioletRed;
            }
        }

        private void Cena_LostFocus(object sender, RoutedEventArgs e)
        {
            int broj;

            if (int.TryParse(Cena.Text, out broj) != false)
            {
                ValCena.Visibility = Visibility.Hidden;
                Cena.Background = Brushes.White;

            }
            else
            {
                ValCena.Visibility = Visibility.Visible;
                Cena.Background = Brushes.PaleVioletRed;
                System.Windows.MessageBox.Show("Ne smete unositi slova, iskljucivo brojeve");
                return;

            }

        }
    }
}
