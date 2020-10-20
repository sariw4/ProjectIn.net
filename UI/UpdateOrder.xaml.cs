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
using System.Windows.Shapes;
using System.ComponentModel;
using BE;
using BL;
using System.Threading;
using System.Globalization;

namespace UI
{
    /// <summary>
    /// Interaction logic for UpdateOrder.xaml
    /// </summary>
    public partial class UpdateOrder : Window
    {
        Order or,or1;
        public UpdateOrder(Order o)
        {
            InitializeComponent();
            or = new Order();
            or1 = o;
            this.Status.ItemsSource = Enum.GetValues(typeof(BE.OrderStatus));
            UpdatO.DataContext = or;
        }

        private void update_click(object sender, RoutedEventArgs e)
        {
            try
            {
                MainWindow.bl.UpdateOrder(or1, or.Status);
                BackgroundWorker backgroundWorker = new BackgroundWorker();
                backgroundWorker.DoWork += BackgroundWorker_DoWork;
                backgroundWorker.RunWorkerAsync();
               
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.None,
                                              MessageBoxOptions.RtlReading | MessageBoxOptions.RightAlign);
            }
            this.Close();
        }
        private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (or.Status == OrderStatus.MailHasBeenSent )
            {
                try
                {
                    MainWindow.bl.SentMail(or, MainWindow.bl.GetGuestRequests().Find(x => x.GuestRequestKey == or1.GuestRequestKey));
                    
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message + "\n the mail could not been sent", "Error", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.None,
                                              MessageBoxOptions.RtlReading | MessageBoxOptions.RightAlign);
                }
            }

        }
    }
 
}
