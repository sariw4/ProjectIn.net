using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Collections.ObjectModel;
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
    enum Filter { All, Grop_By_Area, Grop_By_type,Sort_By_price,Group_By_Vacationers }
    enum Filterg { All, Grop_By_Area, Group_By_Vacationers }

    /// <summary>
    /// Interaction logic for Manager.xaml
    /// </summary>
    public partial class Manager : UserControl
    {
        private ObservableCollection<HostingUnit> mainList =
        new ObservableCollection<HostingUnit>(MainWindow.bl.GetHostingUnits());
        ObservableCollection<IGrouping<AreaStatus, HostingUnit>> GroupHostingUnitsByArea;
        ObservableCollection<IGrouping<TypeStatus, HostingUnit>> TypeOfHostingUnits;
        ObservableCollection<IGrouping<int, HostingUnit>> NumOfVacationers;
        private ObservableCollection<HostingUnit> listToFilter;
        private ObservableCollection<GuestRequest> mainListg =
        new ObservableCollection<GuestRequest>(MainWindow.bl.GetGuestRequests());
        ObservableCollection<IGrouping<AreaStatus, GuestRequest>> GroupByArea;
        ObservableCollection<IGrouping<int, GuestRequest>> GroupByVacationers;
        private ObservableCollection<GuestRequest> listToFilterg;

        public Manager()
        {
            InitializeComponent();
            ManagerPassword.Visibility = Visibility.Visible;
            ManagerMain.Visibility = Visibility.Collapsed;
            hostingdata.DataContext = mainList;
            orderdata.DataContext = MainWindow.bl.GetOrders();
            guestdata.DataContext = mainListg;
            this.combo_hosting.ItemsSource = Enum.GetValues(typeof(Filter));
            this.combo_guest.ItemsSource = Enum.GetValues(typeof(Filterg));
            combo_hosting_name.IsEnabled = false;
            combo_guest_name.IsEnabled = false;
        }
        private void Button_Click_Manager(object sender, RoutedEventArgs e)
        {
            if (Configuration.Manager == long.Parse(PasswordManager.Password))
            {
                ManagerPassword.Visibility = Visibility.Collapsed;
                ManagerMain.Visibility = Visibility.Visible;
            }
            else MessageBox.Show("Oops you are not the manager!!");
        }
        private void Combo_hosting_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (combo_hosting.SelectedIndex)
            {
                case (int)Filter.All:
                    combo_hosting_name.IsEnabled = false;
                    combo_hosting_name.SelectedItem = null;
                    hostingdata.DataContext = mainList;
                    break;
                case (int)Filter.Grop_By_Area:
                    GroupHostingUnitsByArea = new ObservableCollection<IGrouping<AreaStatus, HostingUnit>>(MainWindow.bl.GroupHostingUnitsByArea());
                    AreaStatus[] keysOfarea = (from item in GroupHostingUnitsByArea select item.Key).ToArray();
                    combo_hosting_name.ItemsSource = keysOfarea;
                    break;
                case (int)Filter.Grop_By_type:
                    TypeOfHostingUnits = new ObservableCollection<IGrouping<TypeStatus , HostingUnit>>(MainWindow.bl.TypeOfHostingUnits());
                    TypeStatus[] keysOftype = (from item in TypeOfHostingUnits select item.Key).ToArray();               
                    combo_hosting_name.ItemsSource = keysOftype;
                    break;
                case (int)Filter.Sort_By_price:
                    combo_hosting_name.IsEnabled = false;
                    combo_hosting_name.SelectedItem = null;
                    hostingdata.DataContext = MainWindow.bl.OrderByPrice();
                    break;             
                case (int)Filter.Group_By_Vacationers:
                    NumOfVacationers = new ObservableCollection<IGrouping<int, HostingUnit>>(MainWindow.bl.NumOfVacationers());
                    int[] keysOfvac = (from item in NumOfVacationers select item.Key).ToArray();
                    combo_hosting_name.ItemsSource = keysOfvac;
                    break;
            }
            if ((int)Filter.All != combo_hosting.SelectedIndex && combo_hosting.SelectedIndex != -1)
            {
                combo_hosting_name.IsEnabled = true;
                //if (combo_hosting_name.SelectedItem == null) { }
                //    //combo_hosting_name.SelectedIndex = 0;
            }
        }



        private void Combo_hosting_name_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (combo_hosting.SelectedIndex)
            {
                case (int)Filter.All:
                    mainList = new ObservableCollection<HostingUnit>(MainWindow.bl.GetHostingUnits ());
                    listToFilter = mainList;
                    break;
                case (int)Filter.Grop_By_Area:
                    foreach (var item in GroupHostingUnitsByArea )
                        if (combo_hosting_name.SelectedItem != null && item.Key == (AreaStatus)combo_hosting_name.SelectedItem)
                        {
                            listToFilter = new ObservableCollection<HostingUnit>(item.ToList());
                            break;
                        }
                    break;
                case (int)Filter.Grop_By_type:
                    foreach (var item in TypeOfHostingUnits)
                        if (combo_hosting_name.SelectedItem != null && item.Key == (TypeStatus)combo_hosting_name.SelectedItem)
                        {
                            listToFilter = new ObservableCollection<HostingUnit>(item.ToList());
                            break;
                        }
                    break;
                case (int)Filter.Group_By_Vacationers:
                    foreach (var item in NumOfVacationers)
                        if (combo_hosting_name.SelectedItem != null && item.Key == (int)combo_hosting_name.SelectedItem)
                        {
                            listToFilter = new ObservableCollection<HostingUnit>(item.ToList());
                            break;
                        }
                    break; 
            }
            hostingdata.DataContext = listToFilter;
           
        }
        private void more_details_clickH(object sender, RoutedEventArgs e)
        {
            HostingUnit HU = this.hostingdata.SelectedItem as HostingUnit;
            int n1 = MainWindow.bl.GetAnnualBusyDays(HU);
            if (HU != null)
            {
                MessageBox.Show("Annual Busy Days: " + n1, "Hosting Unit's details", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        private void more_details_clickH1(object sender, RoutedEventArgs e)
        {
            HostingUnit HU = this.hostingdata.SelectedItem as HostingUnit;
            float n2 = MainWindow.bl.GetAnnualBusyPercentage(HU);
            if (HU != null)
            {
                MessageBox.Show( "\n" + "Annual Busy Percentage: " + n2 + "\n", "Hosting Unit's details", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        private void more_details_clickH2(object sender, RoutedEventArgs e)
        {
            HostingUnit HU = this.hostingdata.SelectedItem as HostingUnit;
            int n3 = MainWindow.bl.ClosedOrSentOrders(HU);

            if (HU != null)
            {
                MessageBox.Show(  "Closed Or SentOrders: " + n3 + "\n", "Hosting Unit's details", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        //guest
        private void Combo_guest_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (combo_guest.SelectedIndex)
            {
                case (int)Filterg.All:
                    combo_guest_name.IsEnabled = false;
                    combo_guest_name.SelectedItem = null;
                    guestdata.DataContext = mainListg;
                    break;
                case (int)Filterg.Grop_By_Area:
                    GroupByArea = new ObservableCollection<IGrouping<AreaStatus, GuestRequest>>(MainWindow.bl.GroupByArea());
                    AreaStatus[] keysOfarea = (from item in GroupByArea select item.Key).ToArray();
                    combo_guest_name.ItemsSource = keysOfarea;
                    break;
                case (int)Filterg.Group_By_Vacationers:
                    GroupByVacationers = new ObservableCollection<IGrouping<int, GuestRequest>>(MainWindow.bl.GroupByVacationers());
                    int[] keysOfvac = (from item in GroupByVacationers select item.Key).ToArray();
                    combo_guest_name.ItemsSource = keysOfvac;
                    break;
            }
            if ((int)Filterg.All != combo_guest.SelectedIndex && combo_guest.SelectedIndex != -1)
            {
                combo_guest_name.IsEnabled = true;

            }
        }
        private void Combo_guest_name_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (combo_guest.SelectedIndex)
            {
                case (int)Filterg.All:
                    mainListg = new ObservableCollection<GuestRequest>(MainWindow.bl.GetGuestRequests());
                    listToFilterg = mainListg;
                    break;
                case (int)Filterg.Grop_By_Area:
                    foreach (var item in GroupByArea)
                        if (combo_guest_name.SelectedItem != null && item.Key == (AreaStatus)combo_guest_name.SelectedItem)
                        {
                            listToFilterg = new ObservableCollection<GuestRequest>(item.ToList());
                            break;
                        }
                    break;
                case (int)Filterg.Group_By_Vacationers:
                    foreach (var item in GroupByVacationers)
                        if (combo_guest_name.SelectedItem != null && item.Key == (int)combo_guest_name.SelectedItem)
                        {
                            listToFilterg = new ObservableCollection<GuestRequest>(item.ToList());
                            break;
                        }
                    break;
            }
            guestdata.DataContext = listToFilterg;

        }
        private void more_details_clickg(object sender, RoutedEventArgs e)
        {
            GuestRequest g = this.guestdata.SelectedItem as GuestRequest;
            int n1 = MainWindow.bl.NumOfOrders(g);
            if (g != null)
            {
                MessageBox.Show("Num Of Orders: " + n1, "Guest request's details", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
    
}
