using Aplikacija.Dijalozi;
using Aplikacija.Modeli;
using Aplikacija.Serijalizacija;
using Aplikacija.Tabele;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Aplikacija
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged

    {
        
        public MainWindow()
        {
            this.DataContext = this;
            Tipovi = new ObservableCollection<Tip>();
            Etikete = new ObservableCollection<Etiketa>();
            Dogadjaji = new ObservableCollection<Dogadjaj>();
            MapaLista = new ObservableCollection<Dogadjaj>();
            MapaLista = Dogadjaji;
            InitializeComponent();
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

        private void DodajTip_Click(object sender, RoutedEventArgs e)
        {
            DodajTipForma prozor = new DodajTipForma();
            
            prozor.ShowDialog();
        }

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private Point startPoint;

        private void DodajDogadjaj_Click(object sender, RoutedEventArgs e)
        {
            DodajVrstuForma prozor = new DodajVrstuForma();
            if(Tipovi.Count() == 0)
            {
                MessageBox.Show("Morate prvo dodati tip");
                return;
            }
            else
            {
                prozor.ShowDialog();
            }
            
        }

        private void DodajEtiketuButton_Click(object sender, RoutedEventArgs e)
        {
            DodajEtiketuForma prozor = new DodajEtiketuForma();
            prozor.ShowDialog();
        }

        private void TabelaTipova_Click(object sender, RoutedEventArgs e)
        {
            PregledTipa prozor = new PregledTipa();
            prozor.ShowDialog();
        }

        private void TabelaDogadjaja_Click(object sender, RoutedEventArgs e)
        {
            PregledVrste prozor = new PregledVrste();
            prozor.ShowDialog();
        }       

        private void TabelaEtiketa_Click(object sender, RoutedEventArgs e)
        {
            PregledEtikete prozor = new PregledEtikete();
            prozor.ShowDialog();
        }
       
        public static ObservableCollection<Tip> Tipovi
        {
            get;
            set;
        }

        public static ObservableCollection<Etiketa> Etikete
        {
            get;
            set;
        }

        public static ObservableCollection<Dogadjaj> Dogadjaji
        {
            get;
            set;
        }

        private ObservableCollection<Dogadjaj> mapalista;

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<Dogadjaj> MapaLista
        {

            get { return mapalista; }

            set
            {
                if(mapalista != value)
                {
                    mapalista = value;
                    OnPropertyChanged("MapaLista");

                }
            }
        }

        //kad prevlacimo da se stvori tackasti pravougaonik
        private void Image_MouseMove(object sender, MouseEventArgs e)
        {
            System.Windows.Point mousePos = e.GetPosition(null);
            Vector diff = startPoint - mousePos;
            //stvori transparenti pravougaonik oko nase trenutne drag pozicije
            //i hocemo da proverimo da li smo u okviru tog pravougaonika
            if (e.LeftButton == MouseButtonState.Pressed &&
               (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance ||
               Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance))
            {
                try
                {
                    Dogadjaj selektovana = (Dogadjaj)dgDogadjaji.SelectedValue;
                    if (selektovana != null)
                    {
                        DataObject dragData = new DataObject("dogadjaj", selektovana);
                        //prikaz - ono sto ce drag-ovati
                        //dodragdrop - metoda koja proverava sta je to iznad cega je kursor
                        //a zatim proverava da li je validno za drag-ovanje
                        DragDrop.DoDragDrop(prikaz, dragData, DragDropEffects.Link);
                    }
                }
                catch
                {
                    return;
                }
            }
        }

        //pocetna pozicija
        private void Image_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            startPoint = e.GetPosition(null);
        }

        private void TabelaDogadjaja_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgDogadjaji.SelectedValue is Dogadjaj)
            {
                Dogadjaj s = (Dogadjaj)dgDogadjaji.SelectedValue;
                if (!s.Ikonica.Equals(""))
                    prikaz.Source = new BitmapImage(new Uri(s.Ikonica));
                else
                    prikaz.Source = new BitmapImage(new Uri(s.Tip.Ikonica));

            }
            else { prikaz.Source = null; }
        }

       
        private void Mapa_DragEnter(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent("dogadjaj") || sender == e.Source)
            {
                
                e.Effects = DragDropEffects.None;
            }
        }

        private void Mapa_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("dogadjaj"))
            {
                Dogadjaj s = e.Data.GetData("dogadjaj") as Dogadjaj;

                bool result = mapa.Children.Cast<Image>()
                           .Any(x => x.Tag != null && x.Tag.ToString() == s.Oznaka);
                //  bool preklapanje = false;
                System.Windows.Point d0 = e.GetPosition(mapa);
                foreach (Dogadjaj r0 in Dogadjaji)
                {
                    if (s.Oznaka != r0.Oznaka)
                    {
                        if (r0.X != -1 && r0.Y != -1)
                        {
                            if (Math.Abs(r0.X - d0.X) <= 30 && Math.Abs(r0.Y - d0.Y) <= 30)
                            {
                                System.Windows.MessageBox.Show("Događaji se ne mogu preklapati!", "Preklapanje");
                                return;
                            }
                        }
                    }
                }
                if (result == false)
                {

                    Image img = new Image();
                    if (!s.Ikonica.Equals(""))
                        img.Source = new BitmapImage(new Uri(s.Ikonica));
                    else
                        img.Source = new BitmapImage(new Uri(s.Tip.Ikonica));

                    img.Width = 50;
                    img.Height = 50;
                    img.Tag = s.Oznaka;
                    var positionX = e.GetPosition(mapa).X;
                    var positionY = e.GetPosition(mapa).Y;
                    
                    s.X = positionX;
                    s.Y = positionY;
              
                    ObservableCollection<Dogadjaj> dogList = Dogadjaji;
                    int idx = 0;
                    foreach (Dogadjaj dd in dogList)
                    {
                        if (dd.Oznaka.Equals(s.Oznaka))
                            break;
                        idx++;
                    }
                    dogList[idx] = s;
                    mapa.Children.Add(img);
                    Canvas.SetLeft(img, positionX - 20);
                    Canvas.SetTop(img, positionY - 20);

                }
                else
                {
                    System.Windows.MessageBox.Show("Ovaj događaj već postoji na mapi.", "Dodavanje");

                }
            }
        }

        private void TabelaDogadjaja_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            startPoint = e.GetPosition(null);
        }

        private void Sacuvaj_Click(object sender, RoutedEventArgs e)
        {
            SacuvajEtiketu.serijalizacijaEtikete();
            SacuvajTip.serijalizacijaTipa();
            SacuvajDogadjaj.serijalizacijaDogadjaja();
        }

        private void Ucitaj_Click(object sender, RoutedEventArgs e)
        {
            SacuvajEtiketu.deserijalizacijaEtikete();
            SacuvajTip.deserijalizacijaTipa();
            SacuvajDogadjaj.deserijalizacijaDogadjaja();

            dgDogadjaji.ItemsSource = null;
            dgDogadjaji.ItemsSource = Dogadjaji;
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

                dgDogadjaji.ItemsSource = Dogadjaji;
            }
        }

        private void Filtriranje_Text(object sender, TextChangedEventArgs e)
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

                    bool d1 = words.Any(word => dog.Naziv.ToUpper().Contains(word.ToUpper()));
                    bool d2 = words.Any(word => dog.Oznaka.ToUpper().Contains(word.ToUpper()));
                    bool d3 = words.Any(word => dog.Opis.ToUpper().Contains(word.ToUpper()));
                    bool d4 = words.Any(word => dog.CenaOdrzavanja.ToUpper().Contains(word.ToUpper()));
                    bool d5 = words.Any(word => dog.Mesto.ToUpper().Contains(word.ToUpper()));
                    bool d6 = words.Any(word => dog.Tip.Naziv.ToUpper().Contains(word.ToUpper()));
                    bool d7 = words.Any(word => dog.Datum.ToUpper().Contains(word.ToUpper()));
                    bool d8 = words.Any(word => dog.Istorija.ToUpper().Contains(word.ToUpper()));

                    if (d1 == true)
                    {
                        return d1;
                    }else if(d2 == true)
                    {
                        return d2;
                    }else if(d3 == true)
                    {
                        return d3;
                    }else if(d4 == true)
                    {
                        return d4;
                    }else if(d5 == true)
                    {
                        return d5;
                    }else if(d6 == true)
                    {
                        return d6;
                    }else if(d7 == true)
                    {
                        return d7;
                    }else if(d8 == true)
                    {
                        return d8;
                    }
                    else
                    {
                        return false;
                    }

                };

                dgDogadjaji.ItemsSource = Dogadjaji;
            }
        }

        private void ShowTutMenu(object sender, RoutedEventArgs e)
        {
            if (tutorialMenu.Visibility == Visibility.Visible)
                tutorialMenu.Visibility = Visibility.Hidden;
            else
                tutorialMenu.Visibility = Visibility.Visible;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            HelpProvider.ShowHelp("index", this);
        }

        private void Tutorial_Button_Click(object sender, RoutedEventArgs e)
        {
            TutorialWindow tutorialWindow = new TutorialWindow((sender as Button).Name);
            tutorialWindow.ShowDialog();
        }

        private void ObrisiSaMape_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Dogadjaj selektovano = (Dogadjaj)dgDogadjaji.SelectedItem;
                if(selektovano != null)
                {
                    bool postoji = mapa.Children.Cast<Image>()
                           .Any(x => x.Tag != null && x.Tag.ToString() == selektovano.Oznaka);
                    if (postoji)
                    {
                        Image img = null;
                        foreach (Image i in mapa.Children)
                        {
                            
                                img = i;
                            
    
                        }
                        mapa.Children.Remove(img);
                        int idx = 0;
                        foreach (Dogadjaj s in MapaLista)
                        {
                            if (selektovano.Oznaka.Equals(s.Oznaka))
                                break;
                            idx++;
                        }
                        Dogadjaji[idx].X = -1;
                        Dogadjaji[idx].Y = -1;


                        MapaLista = Dogadjaji;

                    }
                }
                else
                {
                    System.Windows.MessageBox.Show("Događaj nije selektovan.");
                }
            } catch
            {
                return;
            }
        }
    }
}
