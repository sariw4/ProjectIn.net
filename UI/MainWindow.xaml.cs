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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static IBL bl = BLFactory.getBL();
        public static use1 U = new use1();
        public static Guestrequest  G = new Guestrequest();
        public static Manager  M = new Manager();

        public MainWindow()
        {
            InitializeComponent();
            bl.UpdateThread();
        }

        //private void back_use(object sender, RoutedEventArgs e)
        //{
        //    main.Children.Clear();
        //    main.Children.Add(U);
        //}
            
        //public  void back_New(object sender, RoutedEventArgs e)
        //{
        //    main.Children.Clear();
        //    main.Children.Add(use.n);
        //}


        private void back_mainwindow(object sender, RoutedEventArgs e)
        {
            main.Children.Clear();
            main.Children.Add(gr);
        }

        
        private void guest(object sender, RoutedEventArgs e)
        {
            main.Children.Clear();
            G = new Guestrequest();
            G.backgu.Click += back_mainwindow;
            main.Children.Add(G);

        }

        private void host(object sender, RoutedEventArgs e)
        {
           
            main.Children.Clear();
            U = new use1();
            U.backn1.Click += back_mainwindow;
            main.Children.Add(U);
        }

        private void manager(object sender, RoutedEventArgs e)
        {
            main.Children.Clear();
            M = new Manager();
            M.backm.Click += back_mainwindow;
            main.Children.Add(M);
        }
    }
}
