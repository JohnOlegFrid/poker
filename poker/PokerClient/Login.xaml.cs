using ClientPoker.Communication;
using ClientPoker.ServiceLayer;
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

namespace ClientPoker
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : UserControl
    {
        public Login()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {

            if (!MainInfo.Instance.ConnectToServer())
            {
                MessageBox.Show("Cannot Connect To Server...");
                return;
            }

            Service.Instance.DoLogin(usernameBox.Text, passwordBox.Password);
            loginButton.IsEnabled = false;        

        }

        public void LoginFaild()
        {
            this.Dispatcher.Invoke(() =>
            {
               MainWindow.ShowMessage("Error with loggin, please try again");
                loginButton.IsEnabled = true;
            });

        }

        private void newUserButton_Click(object sender, RoutedEventArgs e)
        {
            Register reg = new Register();
            this.Content = reg;
        }
    }
}
