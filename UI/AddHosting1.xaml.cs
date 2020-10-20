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
    /// Interaction logic for AddHosting1.xaml
    /// </summary>
    public partial class AddHosting1 : Window
    {
        HostingUnit Myhu;
        public AddHosting1(Host h)
        {
            InitializeComponent();
            Myhu = new HostingUnit()
            {
                //HostingUnitKey = Configuration.HostingUnitKeyS,
                Owner = h,
            };
            ad1.DataContext = Myhu;
            this.Type.ItemsSource = Enum.GetValues(typeof(BE.TypeStatus));
            this.Area.ItemsSource = Enum.GetValues(typeof(BE.AreaStatus));
        }
        private void Button_Add(object sender, RoutedEventArgs e)
        {
            try
            {
                if ((HostingUnitName.Text.Length == 0) || !(HostingUnitName.Text.All(x => (x == ' ') || char.IsLetter(x))) ||
                   ((Price.Text.Length == 0 ||  !(Price.Text.All(x => (x == ' ') || char.IsDigit(x))))))
                {
                    MessageBox.Show("You forget a somthing or the details are not correct!", "Error", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.None,
                               MessageBoxOptions.RtlReading | MessageBoxOptions.RightAlign);

                }
                else
                {
                    MainWindow.bl.AddHostingUnit(Myhu);
                    MessageBox.Show("the hosting unit successfully added!\n" +"your hosting unit key is: "+Myhu.HostingUnitKey);
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
