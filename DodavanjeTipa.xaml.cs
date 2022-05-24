using Aplikacija.Modeli;
using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Aplikacija
{

    public partial class DodajTipForma : Window
    {
        private MainWindow mw;
        private BazaPodataka baza;
        
        public static Tip trenutniTip;
        private string slika;
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
    

        public DodajTipForma()
        {
            this.DataContext = this;
            InitializeComponent();
            img.Source = new BitmapImage(new Uri("images/download.png", UriKind.Relative));
            trenutniTip = null;
        }

        
        public DodajTipForma(Tip tip, MainWindow mwi)
        {
            this.DataContext = this;
            trenutniTip = tip;
            InitializeComponent();
            
            
            Oznaka.Text = tip.Oznaka;
            Ime.Text = tip.Naziv;
            Opis.Text = tip.Opis;
            img.Source = new BitmapImage(new Uri(tip.Ikonica));
            mw = mwi;
        }

        public static ObservableCollection<Tip> Tipovi
        {
            get;
            set;
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


        private void OdustaniButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            Application.Current.MainWindow.Show();
        }

        private void hoverSacuvaj(object sender, MouseEventArgs e)
        {
            SacuvajButton.Foreground = new SolidColorBrush(Colors.Black);
        }

        private void otpustiSacuvaj(object sender, MouseEventArgs e)
        {
            SacuvajButton.Foreground = new SolidColorBrush(Colors.White);
        }
        
        private void SacuvajButton_Click(object sender, RoutedEventArgs e)
        {
            bool vOznaka = false;
            bool vIme = false;

            if (Oznaka.Text == "" || Ime.Text == "")
            {
                vOznaka = false;
                vIme = false;
                Oznaka.Background = Brushes.PaleVioletRed;
                Ime.Background = Brushes.PaleVioletRed;
                MessageBox.Show("Morate uneti oznaku i ime!");
                return;

            }
            else
            {
                vOznaka = true;
                vIme = true;
            }

            if (trenutniTip == null)
            {
                foreach (Tip t in MainWindow.Tipovi)
                {

                    if (t.Oznaka.Equals(Oznaka.Text))
                    {
                        MessageBox.Show("Već postoji tip sa ovom oznakom!");
                        return;
                    }
                }

                
                Tip tip = new Tip { Oznaka = Oznaka.Text, Naziv = Ime.Text, Opis = Opis.Text, Ikonica = img.Source.ToString() };
                MainWindow.Tipovi.Add(tip);
                MessageBox.Show("Uspešno dodat tip!");
            }

            else if(trenutniTip != null)
            {
                foreach (Tip t in MainWindow.Tipovi)
                {
                    if (t.Oznaka.Equals(trenutniTip.Oznaka) && !(t.Oznaka.Equals(trenutniTip.Oznaka)))
                    {
                        vOznaka = true;
                        vIme = true;
                    }
                }

                trenutniTip.Oznaka = Oznaka.Text;
                trenutniTip.Naziv = Ime.Text;
                trenutniTip.Opis = Opis.Text;
                trenutniTip.Ikonica = img.Source.ToString();
            }
         
            this.Close();          
         }

        private void OdustaniButton_MouseEnter(object sender, MouseEventArgs e)
        {
            OdustaniButton.Foreground = new SolidColorBrush(Colors.Black);
        }

        private void OdustaniButton_MouseLeave(object sender, MouseEventArgs e)
        {
            OdustaniButton.Foreground = new SolidColorBrush(Colors.White);
        }

        private void BtnOdaberi_MouseLeave(object sender, MouseEventArgs e)
        {
            btnOdaberi.Foreground = new SolidColorBrush(Colors.White);
        }

        private void BtnOdaberi_MouseEnter(object sender, MouseEventArgs e)
        {
            btnOdaberi.Foreground = new SolidColorBrush(Colors.Black);
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
            if (!Ime.Text.Equals(""))
            {
                ValIme.Visibility = Visibility.Hidden;
                Ime.Background = Brushes.White;

            }
            else
            {
                ValIme.Visibility = Visibility.Visible;
                Ime.Background = Brushes.PaleVioletRed;
            }
        }
    }
}
