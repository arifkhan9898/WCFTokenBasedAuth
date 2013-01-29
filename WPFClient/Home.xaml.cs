using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Facade;

namespace WPFClient
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : Window
    {
        public Home()
        {
            InitializeComponent();
        }

        private void GetServerTime(object sender, RoutedEventArgs e)
        {
            try
            {
                Message.Text = ServiceProxyFactory.Create<IUserService>().GetCurrentTime().ToLongTimeString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void Logout(object sender, RoutedEventArgs e)
        {
            ServiceProxyFactory.AuthenticationToken = null;
        }
    }
}
