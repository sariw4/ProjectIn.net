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
using BE;
using BL;
namespace UI
{
    /// <summary>
    /// Interaction logic for use1.xaml
    /// </summary>
    public partial class use1 : UserControl
    {
        Host Myho;
        HostingUnit hu;
        Host ho;
        public use1()
        {
            InitializeComponent();
            Myho = new Host()
            {
                HostKey = Configuration.HostKeyS,

            };
            this.banknu.ItemsSource = MainWindow.bl.getAllBankBranches();
            ne.DataContext = Myho;
          
        
        }
        bool degel = true;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if ((pn.Text.Length == 0 || !(pn.Text.All(x => (x == ' ') || char.IsLetter(x)))) ||
                    (fn.Text.Length == 0 || !(fn.Text.All(x => (x == ' ') || char.IsLetter(x)))) ||
                    (fon.Text.Length == 0 || !(fon.Text.All(x => (x == ' ') || char.IsDigit(x)))) ||
                    (ma.Text.Length == 0 || (ma.Text.All(x => (x == ' ')))) ||
                    (pass.Text.Length == 0 || !(pass.Text.All(x => (x == ' ') || char.IsDigit(x)))) ||
                    //(banknu.Text.Length == 0 || !(banknu.Text.All(x => (x == ' ') || char.IsDigit(x)))) ||
                   // (BranchNumber.Text.Length == 0 || !(BranchNumber.Text.All(x => (x == ' ') || char.IsDigit(x)))) ||
                   // (BranchAddress.Text.Length == 0 || !(BranchAddress.Text.All(x => (x == ' ') || char.IsLetter(x)))) ||
                   // ((BankName.Text.Length == 0) || !(BankName.Text.All(x => (x == ' ') || char.IsLetter(x)))) ||
                   // ((BranchCity.Text.Length == 0) || !(BranchCity.Text.All(x => (x == ' ') || char.IsLetter(x)))) ||
                   ((BankAccountNumber.Text.Length == 0) || !(BankAccountNumber.Text.All(x => (x == ' ') || char.IsDigit(x)))))
            {
                MessageBox.Show("You forget a somthing or the details are not correct!", "Error", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.None,
                           MessageBoxOptions.RtlReading | MessageBoxOptions.RightAlign);
                degel = false;

            }
            if (!ma.Text.EndsWith("@gmail.com"))
            {
                MessageBox.Show("the mail address is not correct!", "Error", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.None,
                                              MessageBoxOptions.RtlReading | MessageBoxOptions.RightAlign);
                return;
            }
            else if (degel == true)
            {
                AddHosting1 n = new AddHosting1(Myho);
                n.ShowDialog();
                pn.IsReadOnly = true;
                fn.IsReadOnly = true;
                fon.IsReadOnly = true;
                ma.IsReadOnly = true;
                pass.IsReadOnly = true;
                banknu.IsReadOnly = true;
                //BranchAddress.IsReadOnly = true;
                //BranchNumber.IsReadOnly = true;
                //BankName.IsReadOnly = true;
                //BranchCity.IsReadOnly = true;
                BankAccountNumber.IsReadOnly = true;
                cc.IsEnabled = false;
            }
        }
        private void Button_Click_Add_Hostingunit(object sender, RoutedEventArgs e)
        {
   
                hu = MainWindow.bl.GetHostingUnits().Find(item => item.Owner.Password == long.Parse(PassHostkey.Password));
                ho = hu.Owner;
                AddHosting1 u = new AddHosting1(ho);
                u.ShowDialog();
            
        }



        private void Delethosting(object sender, RoutedEventArgs e)
        {
            Delete1 u = new Delete1(PassHostkey.Password);
            u.ShowDialog();
        }

        private void Button_Order(object sender, RoutedEventArgs e)
        {
            orders1 n = new orders1(PassHostkey.Password);
            n.ShowDialog();
        }

       

        private void Button_Click_EnterAsAdmin(object sender, RoutedEventArgs e)
        {
            if (MainWindow.bl.GetHostingUnits().Exists(item => item.Owner.Password == long.Parse(PassHostkey.Password)))
            {
                AdminPasswordBorder.Visibility = Visibility.Collapsed;
                AdminMainWindowBorder.Visibility = Visibility.Visible;
                
            }
            else MessageBox.Show("לא קיים");
        }
        private void Button_Click_Update(object sender, RoutedEventArgs e)
        {
            Update1 n = new Update1();
            n.ShowDialog();
        }

    }
}
