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
using System.Collections.ObjectModel;
using BE;
using BL;

namespace UI
{
    enum Filter1 { All, Grop_By_Area, Group_By_Vacationers }

    /// <summary>
    /// Interaction logic for orders1.xaml
    /// </summary>
    public partial class orders1 : Window
    {
        private ObservableCollection<GuestRequest> mainList =
        new ObservableCollection<GuestRequest>(MainWindow.bl.GetGuestRequests());
        ObservableCollection<IGrouping<AreaStatus, GuestRequest>> GroupByArea;
        ObservableCollection<IGrouping<int, GuestRequest>> GroupByVacationers;
        private ObservableCollection<GuestRequest> listToFilter;
        long hostPassword;
        public orders1(string hostp)
        {
            InitializeComponent();
            hostPassword = int.Parse(hostp);
            Orderlist.DataContext = MainWindow.bl.GetOrders();
            //Guestlist.DataContext = MainWindow.bl.GetGuestRequests();
            this.combo_gues.ItemsSource = Enum.GetValues(typeof(Filter1));
            Guestlist.DataContext = mainList;
            combo_gues_name.IsEnabled = false;


        }
        private void Combo_guest__SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (combo_gues.SelectedIndex)
            {
                case (int)Filter1.All:
                    combo_gues_name.IsEnabled = false;
                    combo_gues_name.SelectedItem = null;
                    Guestlist.DataContext = mainList;
                    break;
                case (int)Filter1.Grop_By_Area:
                    GroupByArea = new ObservableCollection<IGrouping<AreaStatus, GuestRequest>>(MainWindow.bl.GroupByArea());
                    AreaStatus[] keysOfarea = (from item in GroupByArea select item.Key).ToArray();
                    combo_gues_name.ItemsSource = keysOfarea;
                    break;
                case (int)Filter1.Group_By_Vacationers:
                    GroupByVacationers = new ObservableCollection<IGrouping<int, GuestRequest>>(MainWindow.bl.GroupByVacationers());
                    int[] keysOfvac = (from item in GroupByVacationers select item.Key).ToArray();
                    combo_gues_name.ItemsSource = keysOfvac;
                    break;
            }
            if ((int)Filter1.All != combo_gues.SelectedIndex && combo_gues.SelectedIndex != -1)
            {
                combo_gues_name.IsEnabled = true;
                //if (combo_hosting_name.SelectedItem == null) { }
                //    //combo_hosting_name.SelectedIndex = 0;
            }
        }



        private void Combo_guest_name_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (combo_gues.SelectedIndex)
            {
                case (int)Filter1.All:
                    mainList = new ObservableCollection<GuestRequest>(MainWindow.bl.GetGuestRequests());
                    listToFilter = mainList;
                    break;
                case (int)Filter1.Grop_By_Area:
                    foreach (var item in GroupByArea)
                        if (combo_gues_name.SelectedItem != null && item.Key == (AreaStatus)combo_gues_name.SelectedItem)
                        {
                            listToFilter = new ObservableCollection<GuestRequest>(item.ToList());
                            break;
                        }
                    break;
                case (int)Filter1.Group_By_Vacationers:
                    foreach (var item in GroupByVacationers)
                        if (combo_gues_name.SelectedItem != null && item.Key == (int)combo_gues_name.SelectedItem)
                        {
                            listToFilter = new ObservableCollection<GuestRequest>(item.ToList());
                            break;
                        }
                    break;
            }
            Guestlist.DataContext = listToFilter;

        }
        private void more_details_clickG(object sender, RoutedEventArgs e)
        {
            GuestRequest gr = this.Guestlist.SelectedItem as GuestRequest;
            if (gr != null)
            {
                MessageBox.Show($"Details of guest request:\n{gr} ", "Guest request's details", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        private void more_details_clickU(object sender, RoutedEventArgs e)
        {
            Order OR = this.Orderlist.SelectedItem as Order;
            if (OR != null)
            {
                MessageBox.Show($"Details of order:\n{OR} ", "Order's details", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        private void Add_Order_click(object sender, RoutedEventArgs e)
        {
            try
            {
                bool flag = true;
                bool flag1 = false;
                HostingUnit myhu = new HostingUnit();
                GuestRequest OR = this.Guestlist.SelectedItem as GuestRequest;
                List<HostingUnit> myhus = MainWindow.bl.GetHostingUnits().FindAll(item => item.Owner.Password == hostPassword);
                foreach (var item in myhus)
                {
                    flag = MainWindow.bl.matchingGrAndHu(OR, item);
                    myhu = item;

                    if (OR != null && flag == true)
                    {
                        flag1 = true;
                        Order o = new Order
                        {
                            HostingUnitKey = myhu.HostingUnitKey,
                            GuestRequestKey = OR.GuestRequestKey,
                            //OrderKey = Configuration.OrderKeyS,
                            Status = OrderStatus.NotYetAddressed,
                            CreateDate = DateTime.Now,
                            OrderDate = DateTime.Now
                        };
                        MainWindow.bl.AddOrder(o);
                        Guestlist.DataContext = MainWindow.bl.GetGuestRequests();
                        Orderlist.DataContext = MainWindow.bl.GetOrders();
                    }

                }
                if (flag1 == false)
                {
                    MessageBox.Show("You do not have a hosting unit that meets the customer's requirement", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.None,
                                                          MessageBoxOptions.RtlReading | MessageBoxOptions.RightAlign);
            }
        }
        private void Update_click(object sender, RoutedEventArgs e)
        {
            Order OR = this.Orderlist.SelectedItem as Order;
            UpdateOrder upo = new UpdateOrder(OR);
            upo.ShowDialog();
            Orderlist.DataContext = MainWindow.bl.GetOrders();
            Guestlist.DataContext = MainWindow.bl.GetGuestRequests();
        }
    }
}


