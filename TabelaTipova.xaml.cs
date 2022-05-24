using Aplikacija.Modeli;
using System.Collections.Generic;
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

    public partial class PregledTipa : Window
    {
        private MainWindow mw;
        private DodajTipForma forma = new DodajTipForma();

        public PregledTipa()
        {          
            InitializeComponent();
            this.DataContext = this;
            Tipovi = MainWindow.Tipovi;
        }

        public ObservableCollection<Tip> Tipovi
        {
            get;
            set;
        }
       
        private void IzmeniBtn_Click(object sender, RoutedEventArgs e)
        {
            Tip selektovani = (Tip)dgTip.SelectedItem;
            if(selektovani == null)
            {
                MessageBox.Show("Potrebno je prvo selektovati tip");
            }

            else
            {
                DodajTipForma izmenaTipa = new DodajTipForma(selektovani, mw);              
                izmenaTipa.ShowDialog();
            }
        }

        private void ObrisiBtn_Click(object sender, RoutedEventArgs e)
        {
            Tip tip = (Tip)dgTip.SelectedItem;
            if (tip == null)
            {
                MessageBox.Show("Potrebno je prvo selektovati tip");
            }
            else
            {
                if(MessageBox.Show("Ako obrisete tip, obrisace se i svi događaji sa tim tipom", "Brisanje tipa gogađaja", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    List<Dogadjaj> dogadjaji = MainWindow.Dogadjaji.ToList();
                    foreach (Dogadjaj d in dogadjaji)
                    {
                        if (d.Tip.Equals(tip))
                        {
                            MainWindow.Dogadjaji.Remove(d);
                        }
                    }
                    MainWindow.Tipovi.Remove(tip);
                }
                else
                {
                    return;
                }
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
            System.Windows.Controls.TextBox textbox = sender as System.Windows.Controls.TextBox;
            string filter = textbox.Text;
            ICollectionView cv = CollectionViewSource.GetDefaultView(Tipovi);
            if (filter == "")
                cv.Filter = null;
            else
            {
                string[] words = filter.Split(' ');
                if (words.Contains(""))
                    words = words.Where(word => word != "").ToArray();
                cv.Filter = o =>
                {
                    Tip tip = o as Tip;
                    return words.Any(word => tip.Oznaka.ToUpper().Contains(word.ToUpper()));
                };
                dgTip.ItemsSource = Tipovi;
            }

        }
    }
}
