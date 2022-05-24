using Aplikacija.Dijalozi;
using Aplikacija.Modeli;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace Aplikacija.Tabele
{

    public partial class PregledVrste : Window
    {
        private MainWindow mw;
        public PregledVrste()
        {
            InitializeComponent();
            this.DataContext = this;
            Dogadjaji = MainWindow.Dogadjaji;
        }

        public ObservableCollection<Dogadjaj> Dogadjaji
        {
            get;
            set;
        }


        private void IzmeniBtn_Click(object sender, RoutedEventArgs e)
        {
            Dogadjaj s = (Dogadjaj)dgDogadjaj.SelectedItem;
            if (s == null)
            {
                MessageBox.Show("Morate prvo selektovati događaj da biste ga izmenili.");
            }
            else
            {
                DodajVrstuForma izmena = new DodajVrstuForma(s, mw);
                izmena.ShowDialog();
            }          
        }

        private void ObrisiBtn_Click(object sender, RoutedEventArgs e)
        {

            Dogadjaj s = (Dogadjaj)dgDogadjaj.SelectedItem;
            if (s == null)
            {
                MessageBox.Show("Morate prvo selektovati događaj da biste ga obrisali.");
            }
            else
            {
                Dogadjaji.Remove(s);
            }           
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

        private void hoverIzmeni(object sender, MouseEventArgs e)
        {
            IzmeniBtn.Foreground = new SolidColorBrush(Colors.Black);
        }

        private void otpustiIzmeni(object sender, MouseEventArgs e)
        {
            IzmeniBtn.Foreground = new SolidColorBrush(Colors.White);
        }

        private void hoverObrisi(object sender, MouseEventArgs e)
        {
            ObrisiBtn.Foreground = new SolidColorBrush(Colors.Black);
        }

        private void otpustiObrisi(object sender, MouseEventArgs e)
        {
            ObrisiBtn.Foreground = new SolidColorBrush(Colors.White);
        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            System.Windows.Controls.TextBox textbox = sender as System.Windows.Controls.TextBox;
            string filter = textbox.Text;
            ICollectionView cv = CollectionViewSource.GetDefaultView(Dogadjaji);
            if (filter == "")
                cv.Filter = null;
            else
            {
                cv.Filter = o =>
                {
                    Dogadjaj dog = o as Dogadjaj;
                    string[] words = filter.Split(' ');
                    if (words.Contains(""))
                        words = words.Where(word => word != "").ToArray();
                    return words.Any(word => dog.Naziv.ToUpper().Contains(word.ToUpper()));
                };

                dgDogadjaj.ItemsSource = Dogadjaji;
            }
        } 
    }
}
