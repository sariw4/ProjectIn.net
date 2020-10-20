using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BL;
using BE;
namespace UI
{
    /// <summary>
    /// Interaction logic for GuestRequest.xaml
    /// </summary>
    public partial class Guestrequest : UserControl
    {
        
        GuestRequest myReq ;
        public Guestrequest()
        {
            InitializeComponent();
            myReq = new GuestRequest()
            {
                //GuestRequestKey = Configuration.guestRequestKeyS++,
                EntryDate = DateTime.Now,
                RegistrationDate = DateTime.Now,
                ReleaseDate = DateTime.Now.AddDays(1),
            };
            MYGRID.DataContext = myReq;
            this.TAYP.ItemsSource = Enum.GetValues(typeof(BE.TypeStatus));
            this.AREA.ItemsSource = Enum.GetValues(typeof(BE.AreaStatus));
            this.GARDEN.ItemsSource = Enum.GetValues(typeof(BE.Additions));
            this.JACUZZI.ItemsSource = Enum.GetValues(typeof(BE.Additions));
            this.ChildrensAttractions.ItemsSource = Enum.GetValues(typeof(BE.Additions));
            this.POOL.ItemsSource = Enum.GetValues(typeof(BE.Additions));

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                if ((Privet.Text.Length == 0) || !(Privet.Text.All(x => (x == ' ') || char.IsLetter(x))) ||
                    (Famliy.Text.Length == 0) || !(Famliy.Text.All(x => (x == ' ') || char.IsLetter(x))) ||
                    (Adults.Text.Length == 0) || !(Adults.Text.All(x => (x == ' ') || char.IsDigit(x))) ||
                    (Mail .Text.Length == 0) ||
                    (Children.Text.Length == 0) || !(Children.Text.All(x =>( x == ' ' )|| char.IsDigit(x))))

                {
                    MessageBox.Show("the details are not correct!", "Error", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.None,
                               MessageBoxOptions.RtlReading | MessageBoxOptions.RightAlign);
                    return;
                }
                if (!Mail.Text .EndsWith ("@gmail.com"))
                {
                    MessageBox.Show("the mail address is not correct!", "Error", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.None,
                                                  MessageBoxOptions.RtlReading | MessageBoxOptions.RightAlign);
                    return;
                }

                MainWindow.bl.AddClientRequest(myReq);
                MessageBox.Show("the guest request successfully added!");
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                //"error", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.Cancel,
                //MessageBoxOptions.RightAlign);
            }

        }








        //private void back_Click(object sender, RoutedEventArgs e)
        //{
        //        MainWindow  w = new MainWindow();
        //        MYGRID.Children.Clear();
        //        MYGRID.Children.Add(w);
        //}
    }
}
