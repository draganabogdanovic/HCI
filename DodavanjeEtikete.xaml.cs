using Aplikacija.Modeli;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Aplikacija.Dijalozi
{

    public partial class DodajEtiketuForma : Window
    {
        private MainWindow mw;
        public static Etiketa trenutnaEtiketa;
        private Color mojaBoja;

        public static ObservableCollection<Etiketa> Etikete
        {
            get;
            set;
        }

        public DodajEtiketuForma()
        {
            this.DataContext = this;
            InitializeComponent();
            trenutnaEtiketa = null;
            
        }

        public DodajEtiketuForma(Etiketa e, MainWindow mwi)
        {
            this.DataContext = this;
            trenutnaEtiketa = e;
            InitializeComponent();
            Etikete = MainWindow.Etikete;

            Oznaka.Text = e.Oznaka;
            Opis.Text = e.Opis;
            ClrPcker_Background.SelectedColor = (Color)ColorConverter.ConvertFromString(e.Boja);
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

        private String boja;

        public String Boja
        {
            get { return boja; }
            set { boja = value; }
        }

   
        private void OdustaniBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        
        private void ClrPcker_Background_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {

            if(ClrPcker_Background.SelectedColor.HasValue)
            {
                mojaBoja = ClrPcker_Background.SelectedColor.Value;
                byte red = mojaBoja.R;
                byte green = mojaBoja.G;
                byte blue = mojaBoja.B;
            }
            else
            {
                mojaBoja = new Color();
            }
            
        }

        private void Sacuvaj_Click(object sender, RoutedEventArgs e)
        {
            
            bool vOznaka = false;
            if(Oznaka.Text == "")
            {
                vOznaka = false;
                Oznaka.Background = Brushes.PaleVioletRed;
                MessageBox.Show("Morate uneti oznaku!");
                return;

            }
            else
            {
                vOznaka = true;
                
            }

            if(!vOznaka)
            {
                
                VOznaka.Visibility = Visibility.Visible;
                return;   
            }

            Color boja = (Color)ClrPcker_Background.SelectedColor;
           
            string myclr = boja.ToString();

            

            if (trenutnaEtiketa != null)
            {

                foreach (Etiketa et in MainWindow.Etikete)
                {
                    if (et.Oznaka == Oznaka.Text && !(et.Oznaka.Equals(trenutnaEtiketa.Oznaka)))
                    {
                        System.Windows.MessageBox.Show("Vec postoji etiketa sa ovakvom oznakom");
                        return;
                    }
                }
                trenutnaEtiketa.Oznaka = Oznaka.Text;
                trenutnaEtiketa.Opis = Opis.Text;
                trenutnaEtiketa.Boja = myclr;
            }
            else
            {


                Etiketa etiketa = new Etiketa();
                etiketa.Oznaka = Oznaka.Text;
                etiketa.Opis = Opis.Text;
                etiketa.Boja = myclr;

                foreach (Etiketa et in MainWindow.Etikete)
                {
                    if (et.Oznaka == Oznaka.Text)
                    {
                        System.Windows.MessageBox.Show("Već postoji etiketa sa ovakvom oznakom!");
                        return;
                    }
                }

                MainWindow.Etikete.Add(etiketa);
                System.Windows.MessageBox.Show("Uspešno dadata etiketa!");
            }
            
            this.Close();
        }

        private void SacuvajBtn_MouseEnter(object sender, MouseEventArgs e)
        {
            SacuvajBtn.Foreground = new SolidColorBrush(Colors.Black);
        }

        private void SacuvajBtn_MouseLeave(object sender, MouseEventArgs e)
        {
            SacuvajBtn.Foreground = new SolidColorBrush(Colors.White);
        }

        private void OdustaniBtn_MouseEnter(object sender, MouseEventArgs e)
        {
            OdustaniBtn.Foreground = new SolidColorBrush(Colors.Black);
        }

        private void OdustaniBtn_MouseLeave(object sender, MouseEventArgs e)
        {
            OdustaniBtn.Foreground = new SolidColorBrush(Colors.White);
        }

        private void Oznaka_LostFocus(object sender, RoutedEventArgs e)
        {

        }
    }
}
