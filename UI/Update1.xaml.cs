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
    /// Interaction logic for Update1.xaml
    /// </summary>
    public partial class Update1 : Window
    {
        HostingUnit h1;
        public Update1()
        {
            InitializeComponent();
        }
        private void Update_Click(object sender, RoutedEventArgs e)
        {
            if (MainWindow.bl.GetHostingUnits().Exists(item => item.HostingUnitKey == long.Parse(hostingunitk.Password)))
            {
                hostingkey.Visibility = Visibility.Collapsed;
                UpdatHosting.Visibility = Visibility.Visible;
                this.Type.ItemsSource = Enum.GetValues(typeof(BE.TypeStatus));
                this.Area.ItemsSource = Enum.GetValues(typeof(BE.AreaStatus));
                h1 = MainWindow.bl.GetHostingUnits().Find(item => item.HostingUnitKey == long.Parse(hostingunitk.Password));
                UpdatHosting.DataContext = h1;

            }
            else
            {
                MessageBox.Show("לא קיים");
            }
        }

        private void Updat(object sender, RoutedEventArgs e)
        {
            try
            {
                if ((HostingUnitName.Text.Length == 0) || !(HostingUnitName.Text.All(x => (x == ' ') || char.IsLetter(x))) ||
                   ((Price.Text.Length == 0) || !(Price.Text.All(x => (x == ' ') || char.IsDigit(x)))))
                {
                    MessageBox.Show("You forget a somthing or the details are not correct!", "Error", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.None,
                               MessageBoxOptions.RtlReading | MessageBoxOptions.RightAlign);

                }
                else
                {
                    MainWindow.bl.UpdateHostingUnit(h1);
                    MessageBox.Show("The hosting unit successfully updated!");
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}
