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
using BE;
using BL;
namespace UI
{
    /// <summary>
    /// Interaction logic for Delete1.xaml
    /// </summary>
    public partial class Delete1 : Window
    {
        long hostPassword;
        HostingUnit h;
        public Delete1(string hostp)
        {
            InitializeComponent();
            hostPassword = int.Parse(hostp);

        }
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (MainWindow.bl.GetHostingUnits().Exists(item => item.HostingUnitKey == long.Parse(hostingunitk.Password)))
            {
                deletehostingkey.Visibility = Visibility.Collapsed;
                DeleteHosting.Visibility = Visibility.Visible;
                this.Type.ItemsSource = Enum.GetValues(typeof(BE.TypeStatus));
                this.Area.ItemsSource = Enum.GetValues(typeof(BE.AreaStatus));
                h = MainWindow.bl.GetHostingUnits().Find(item => item.HostingUnitKey == long.Parse(hostingunitk.Password));
                DeleteHosting.DataContext = h;

            }
            else
            {
                MessageBox.Show("לא קיים");
            }

        }
        private void deletec(object sender, RoutedEventArgs e)
        {
            try
            {
                h = MainWindow.bl.GetHostingUnits().Find(item => item.HostingUnitKey == long.Parse(hostingunitk.Password));
                if (hostPassword == h.Owner.Password)
                {
                    MessageBoxResult r = MessageBox.Show("Are you sure you want delete this hosting unit?", "", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    switch (r)
                    {
                        case MessageBoxResult.Yes:
                            {
                                MainWindow.bl.RemoveHostingUnit(h);
                                MessageBox.Show("The hosting unit successfully deleted!");
                                this.Close();
                                break;
                            }
                        case MessageBoxResult.No:
                            {
                                this.Close();
                                break;
                            }
                    }

                }
                else
                {
                    MessageBox.Show("Oops you are not the owner of thise hosting unit", "Error", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.None,
                                   MessageBoxOptions.RtlReading | MessageBoxOptions.RightAlign);
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.None,
                               MessageBoxOptions.RtlReading | MessageBoxOptions.RightAlign);
            }
        }
    }

}

