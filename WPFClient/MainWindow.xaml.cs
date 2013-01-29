using System.ServiceModel;
using System.Windows;
using Facade;

namespace WPFClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void LoginClick(object sender, RoutedEventArgs e)
        {
            var proxy = ServiceProxyFactory.Create<IAuthService>();
            try
            {
                var token = proxy.AuthenticateUser(UserName.Text, Password.Password);
                ServiceProxyFactory.AuthenticationToken = token;
                var home = new Home();
                home.Show();
                Application.Current.MainWindow = home;
                Close();
            }
            catch (FaultException fault)
            {
                MessageBox.Show(fault.Message);
            }
        }
    }
}
