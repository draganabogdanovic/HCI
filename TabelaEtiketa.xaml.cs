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

    public partial class PregledEtikete : Window
    {
        private MainWindow mw;
      
        public static ObservableCollection<Etiketa> Etikete
        {
            get;
            set;
        }

        public PregledEtikete()
        {           
            InitializeComponent();
            this.DataContext = this;
            Etikete = MainWindow.Etikete;          
        }

        private void ObrisiBtn_Click(object sender, RoutedEventArgs e)
        {
            
            Etiketa etiketa = (Etiketa)dgEtiketa.SelectedItem;
            if (etiketa == null)
            {
                MessageBox.Show("Potrebno je prvo selektovati etiketu koju zelite da obrisete.");
            }
            else
            {
                Etikete.Remove(etiketa);
            }
        }

        private void IzmeniBtn_Click(object sender, RoutedEventArgs e)
        {
            Etiketa selektovana = (Etiketa)dgEtiketa.SelectedItem;

            if (selektovana == null)
            {
                MessageBox.Show("Potrebno je prvo selektovati etiketu koju zelite da izmenite.");
            }
            else
            {
                DodajEtiketuForma izmena = new DodajEtiketuForma(selektovana, mw);
                izmena.ShowDialog();
            }
            
        }

        private void ObrisiBtn_MouseEnter(object sender, MouseEventArgs e)
        {
            ObrisiBtn.Foreground = new SolidColorBrush(Colors.Black);
        }

        private void ObrisiBtn_MouseLeave(object sender, MouseEventArgs e)
        {
            ObrisiBtn.Foreground = new SolidColorBrush(Colors.White);
        }

        private void IzmeniBtn_MouseEnter(object sender, MouseEventArgs e)
        {
            IzmeniBtn.Foreground = new SolidColorBrush(Colors.Black);
        }

        private void IzmeniBtn_MouseLeave(object sender, MouseEventArgs e)
        {
            IzmeniBtn.Foreground = new SolidColorBrush(Colors.White);
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

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textbox = sender as TextBox;
            string filter = textbox.Text;
            ICollectionView cv = CollectionViewSource.GetDefaultView(Etikete);
            if (filter == "")
                cv.Filter = null;
            else
            {
                string[] words = filter.Split(' ');
                if (words.Contains(""))
                    words = words.Where(word => word != "").ToArray();
                cv.Filter = o =>
                {
                    Etiketa etiketa = o as Etiketa;
                    return words.Any(word => etiketa.Oznaka.ToUpper().Contains(word.ToUpper()));
                };

                dgEtiketa.ItemsSource = Etikete;
            }
        }
    }
}
