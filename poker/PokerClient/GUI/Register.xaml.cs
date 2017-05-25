using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using PokerClient.ServiceLayer;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using PokerClient.Communication;

namespace PokerClient.GUI
{
    /// <summary>
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : UserControl
    {
        public Register()
        {
            InitializeComponent();
            IPadress.Text = Client.GetLocalIPAddress();
        }

        private void goBackButton_Click(object sender, RoutedEventArgs e)
        {
            Login log = new Login();
            this.Content = log;
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            //RegisterButton.IsEnabled = false;
            String pass = passwordBox.Password;
            String confPass = confirmPasswordBox.Password;
            if (pass.CompareTo(confPass) != 0)
            {              
                MessageBox.Show("The Passwords aren't the same.\nRe-enter Please.", "Wrong Password", MessageBoxButton.OK, MessageBoxImage.Warning);
               // RegisterButton.IsEnabled = true;
            }
            else
            {
                if (!MainInfo.Instance.ConnectToServer(IPadress.Text))
                {
                    MessageBox.Show("Cannot connect to server.", "Connection Error", MessageBoxButton.OK, MessageBoxImage.Error); ;
                    //RegisterButton.IsEnabled = true;
                    return;
                }
                Service.Instance.DoRegister(usernameBox.Text, pass, emailBox.Text);
            }
        }

        public void RegisterFaild(string msg)
        {
            
            this.Dispatcher.Invoke(() =>
            {
                this.IsEnabled = true;
            });         

            });
        }
    }
}
